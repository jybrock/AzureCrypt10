using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WebApplication1.Net.WebSockets;

namespace WebApplication1 {
    internal class RequestHandshake : Handshake {
        #region Private Fields

        private NameValueCollection _queryString;

        #endregion

        #region Private Constructors

        private RequestHandshake() {
        }

        #endregion

        #region Public Constructors

        public RequestHandshake( string uriString ) {
            HttpMethod = "GET";
            RequestUri = uriString.ToUri();
            AddHeader("User-Agent", "MtGoxWebSocket/1.0");
            AddHeader("Upgrade", "websocket");
            AddHeader("Connection", "Upgrade");
        }

        #endregion

        #region Public Properties

        public WebApplication1.Net.CookieCollection Cookies {
            get {
                return Headers.GetCookies(false);
            }
        }

        public string HttpMethod {
            get;
            private set;
        }

        public bool IsWebSocketRequest {
            get {
                return HttpMethod != "GET"
                       ? false
                       : ProtocolVersion < HttpVersion.Version11
                         ? false
                         : !ContainsHeader("Upgrade", "websocket")
                           ? false
                           : !ContainsHeader("Connection", "Upgrade")
                             ? false
                             : !ContainsHeader("Host")
                               ? false
                               : !ContainsHeader("Sec-WebSocket-Key")
                                 ? false
                                 : ContainsHeader("Sec-WebSocket-Version");
            }
        }

        public NameValueCollection QueryString {
            get {
                if (_queryString == null) {
                    _queryString = new NameValueCollection();

                    var i = RawUrl.IndexOf('?');
                    if (i > 0) {
                        var query = RawUrl.Substring(i + 1);
                        var components = query.Split('&');
                        foreach (var c in components) {
                            var nv = c.GetNameAndValue("=");
                            if (nv.Key != null) {
                                var name = nv.Key.UrlDecode();
                                var val = nv.Value.UrlDecode();
                                _queryString.Add(name, val);
                            }
                        }
                    }
                }

                return _queryString;
            }
        }

        public string RawUrl {
            get {
                return RequestUri.IsAbsoluteUri
                       ? RequestUri.PathAndQuery
                       : RequestUri.OriginalString;
            }
        }

        public Uri RequestUri {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public static RequestHandshake Parse( string[] request ) {
            var requestLine = request[0].Split(' ');
            if (requestLine.Length != 3) {
                var msg = "Invalid HTTP Request-Line: " + request[0];
                throw new ArgumentException(msg, "request");
            }

            var headers = new WebApplication1.Net.WebHeaderCollection();
            for (int i = 1; i < request.Length; i++)
                headers.SetInternal(request[i], false);

            return new RequestHandshake {
                Headers = headers,
                HttpMethod = requestLine[0],
                RequestUri = requestLine[1].ToUri(),
                ProtocolVersion = new Version(requestLine[2].Substring(5))
            };
        }

        public static RequestHandshake Parse( WebSocketContext context ) {
            return new RequestHandshake {
                Headers = context.Headers,
                HttpMethod = "GET",
                RequestUri = context.RequestUri,
                ProtocolVersion = HttpVersion.Version11
            };
        }

        public void SetCookies( WebApplication1.Net.CookieCollection cookies ) {
            if (cookies == null || cookies.Count == 0)
                return;

            var sorted = cookies.Sorted.ToArray();
            var header = new StringBuilder(sorted[0].ToString(), 64);
            for (int i = 1; i < sorted.Length; i++)
                if (!sorted[i].Expired)
                    header.AppendFormat("; {0}", sorted[i].ToString());

            AddHeader("Cookie", header.ToString());
        }

        public override string ToString() {
            var buffer = new StringBuilder(64);
            buffer.AppendFormat("{0} {1} HTTP/{2}{3}", HttpMethod, RawUrl, ProtocolVersion, CrLf);
            foreach (string key in Headers.AllKeys)
                buffer.AppendFormat("{0}: {1}{2}", key, Headers[key], CrLf);

            buffer.Append(CrLf);
            return buffer.ToString();
        }

        #endregion
    }
}