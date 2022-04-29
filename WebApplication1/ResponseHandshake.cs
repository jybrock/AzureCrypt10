using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebApplication1 {
    internal class ResponseHandshake : Handshake {
        #region Public Constructors

        public ResponseHandshake()
            : this(WebApplication1.Net.HttpStatusCode.SwitchingProtocols) {
            AddHeader("Upgrade", "websocket");
            AddHeader("Connection", "Upgrade");
        }

        public ResponseHandshake( WebApplication1.Net.HttpStatusCode code ) {
            StatusCode = ((int)code).ToString();
            Reason = code.GetDescription();
            AddHeader("Server", "websocket-sharp/1.0");
        }

        #endregion

        #region Public Properties

        public WebApplication1.Net.CookieCollection Cookies {
            get {
                return Headers.GetCookies(true);
            }
        }

       public bool IsWebSocketResponse {
            get {
                return ProtocolVersion < WebApplication1.Net.HttpVersion.Version11
                       ? false
                       : StatusCode != "101"
                         ? false
                         : !ContainsHeader("Upgrade", "websocket")
                           ? false
                           : !ContainsHeader("Connection", "Upgrade")
                             ? false
                             : ContainsHeader("Sec-WebSocket-Accept");
            }
        }

        public string Reason {
            get;
            private set;
        }

        public string StatusCode {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public static WebApplication1.ResponseHandshake CreateCloseResponse( WebApplication1.Net.HttpStatusCode code ) {
            var res = new WebApplication1.ResponseHandshake(code);
            res.AddHeader("Connection", "close");

            return res;
        }

        public static WebApplication1.ResponseHandshake Parse( string[] response ) {
            var statusLine = response[0].Split(' ');
            if (statusLine.Length < 3)
                throw new ArgumentException("Invalid status line.");

            var reason = new StringBuilder(statusLine[2], 64);
            for (int i = 3; i < statusLine.Length; i++)
                reason.AppendFormat(" {0}", statusLine[i]);

            var headers = new WebApplication1.Net.WebHeaderCollection();
            for (int i = 1; i < response.Length; i++)
                headers.SetInternal(response[i], true);

            return new WebApplication1.ResponseHandshake {
                Headers = headers,
                Reason = reason.ToString(),
                StatusCode = statusLine[1],
                ProtocolVersion = new Version(statusLine[0].Substring(5))
            };
        }

        public void SetCookies( WebApplication1.Net.CookieCollection cookies ) {
            if (cookies == null || cookies.Count == 0)
                return;

            foreach (var cookie in cookies.Sorted)
                AddHeader("Set-Cookie", cookie.ToResponseString());
        }

        public override string ToString() {
            var buffer = new StringBuilder(64);
            buffer.AppendFormat("HTTP/{0} {1} {2}{3}", ProtocolVersion, StatusCode, Reason, CrLf);
            foreach (string key in Headers.AllKeys)
                buffer.AppendFormat("{0}: {1}{2}", key, Headers[key], CrLf);

            buffer.Append(CrLf);
            return buffer.ToString();
        }

        #endregion
    }
}