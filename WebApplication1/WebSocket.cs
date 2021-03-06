using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using WebApplication1;
using WebApplication1.Net;
using WebApplication1.Net.WebSockets;
using WebApplication1.Net.Security;

namespace WebApplication1 {
    public class WebSocket : IDisposable {
        #region Private Const Fields

        private const int _fragmentLen = 1016; // Max value is int.MaxValue - 14.
        
          private const string _guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
         
         


        private const string _version = "13";

        #endregion

        #region Private Fields

        private string _base64key;
        private bool _client;
        private Action _closeContext;
        private WebApplication1.Net.CookieCollection _cookies;
        private CompressionMethod _compression;
        private WebApplication1.Net.WebSockets.WebSocketContext _context;
        private string _extensions;
        private AutoResetEvent _exitReceiving;
        private object _forClose;
        private object _forFrame;
        private object _forSend;
        private string _origin;
        private string _protocol;
        private string _protocols;
        private volatile WebApplication1.WsState _readyState;
        private AutoResetEvent _receivePong;
        private bool _secure;
        private TcpClient _tcpClient;
        private Uri _uri;
        private WebApplication1.WsStream _wsStream;

        #endregion

        #region Private Constructors

        private WebSocket() {
            _compression = WebApplication1.CompressionMethod.NONE;
            _cookies = new WebApplication1.Net.CookieCollection();
            _extensions = String.Empty;
            _forClose = new object();
            _forFrame = new object();
            _forSend = new object();
            _origin = String.Empty;
            _protocol = String.Empty;
            _readyState = WebApplication1.WsState.CONNECTING;
        }

        #endregion

        #region Internal Constructors

        internal WebSocket( WebApplication1.Net.WebSockets.HttpListenerWebSocketContext context )
            : this() {
            _wsStream = context.Stream;
            _closeContext = () => context.Close();
            init(context);
        }

        internal WebSocket( WebApplication1.Net.WebSockets.TcpListenerWebSocketContext context )
            : this() {
            _wsStream = context.Stream;
            _closeContext = () => context.Close();
            init(context);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocket"/> class with the specified WebSocket URL and subprotocols.
        /// </summary>
        /// <param name="url">
        /// A <see cref="string"/> that contains a WebSocket URL to connect.
        /// </param>
        /// <param name="protocols">
        /// An array of <see cref="string"/> that contains the WebSocket subprotocols if any.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="url"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="url"/> is not valid WebSocket URL.
        /// </exception>
        public WebSocket( string url, params string[] protocols )
            : this() {
            if (url == null)
                throw new ArgumentNullException("url");

            Uri uri;
            string msg;
            if (!url.TryCreateWebSocketUri(out uri, out msg))
                throw new ArgumentException(msg, "url");

            _uri = uri;
            _protocols = protocols.ToString(", ");
            _client = true;
            _secure = uri.Scheme == "wss"
                    ? true
                    : false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocket"/> class with the specified WebSocket URL,
        /// OnOpen, OnMessage, OnError, OnClose event handlers and subprotocols.
        /// </summary>
        /// <remarks>
        /// This constructor initializes a new instance of the <see cref="WebSocket"/> class and
        /// establishes a WebSocket connection.
        /// </remarks>
        /// <param name="url">
        /// A <see cref="string"/> that contains a WebSocket URL to connect.
        /// </param>
        /// <param name="onOpen">
        /// An <see cref="OnOpen"/> event handler.
        /// </param>
        /// <param name="onMessage">
        /// An <see cref="OnMessage"/> event handler.
        /// </param>
        /// <param name="onError">
        /// An <see cref="OnError"/> event handler.
        /// </param>
        /// <param name="onClose">
        /// An <see cref="OnClose"/> event handler.
        /// </param>
        /// <param name="protocols">
        /// An array of <see cref="string"/> that contains the WebSocket subprotocols if any.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="url"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="url"/> is not valid WebSocket URL.
        /// </exception>
        public WebSocket(
          string url,
          EventHandler onOpen,
          EventHandler<MessageEventArgs> onMessage,
          EventHandler<ErrorEventArgs> onError,
          EventHandler<CloseEventArgs> onClose,
          params string[] protocols )
            : this(url, protocols) {
            OnOpen = onOpen;
            OnMessage = onMessage;
            OnError = onError;
            OnClose = onClose;

            Connect();
        }

        #endregion

        #region Internal Properties

        internal WebApplication1.Net.CookieCollection CookieCollection {
            get {
                return _cookies;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the compression method used to compress the payload data of the WebSocket Data frame.
        /// </summary>
        /// <value>
        /// One of the <see cref="CompressionMethod"/> values that indicates the compression method to use.
        /// The default is <see cref="CompressionMethod.NONE"/>.
        /// </value>
        public WebApplication1.CompressionMethod Compression {
            get {
                return _compression;
            }

            set {
                if (isOpened(true))
                    return;

                _compression = value;
            }
        }

        /// <summary>
        /// Gets the cookies used in the WebSocket opening handshake.
        /// </summary>
        /// <value>
        /// An IEnumerable&lt;Cookie&gt; interface that provides an enumerator which supports the iteration
        /// over the collection of cookies.
        /// </value>
        public IEnumerable<WebApplication1.Net.Cookie> Cookies {
            get {
                lock (_cookies.SyncRoot) {
                    return from WebApplication1.Net.Cookie cookie in _cookies
                           select cookie;
                }
            }
        }

        /// <summary>
        /// Gets the WebSocket extensions selected by the server.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the extensions if any. The default is <see cref="String.Empty"/>.
        /// </value>
        public string Extensions {
            get {
                return _extensions;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the WebSocket connection is alive.
        /// </summary>
        /// <value>
        /// <c>true</c> if the WebSocket connection is alive; otherwise, <c>false</c>.
        /// </value>
        public bool IsAlive {
            get {
                return _readyState == WebApplication1.WsState.OPEN
                       ? ping(new byte[] { })
                       : false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the WebSocket connection is secure.
        /// </summary>
        /// <value>
        /// <c>true</c> if the connection is secure; otherwise, <c>false</c>.
        /// </value>
        public bool IsSecure {
            get {
                return _secure;
            }
        }

        /// <summary>
        /// Gets or sets the value of the Origin header used in the WebSocket opening handshake.
        /// </summary>
        /// <remarks>
        /// A <see cref="WebSocket"/> instance does not send the Origin header in the WebSocket opening handshake
        /// if the value of this property is <see cref="String.Empty"/>.
        /// </remarks>
        /// <value>
        ///   <para>
        ///   A <see cref="string"/> that contains the value of the <see href="http://tools.ietf.org/html/rfc6454#section-7">HTTP Origin header</see> to send.
        ///   The default is <see cref="String.Empty"/>.
        ///   </para>
        ///   <para>
        ///   The value of the Origin header has the following syntax: <c>&lt;scheme&gt;://&lt;host&gt;[:&lt;port&gt;]</c>
        ///   </para>
        /// </value>
        public string Origin {
            get {
                return _origin;
            }

            set {
                if (isOpened(true))
                    return;

                if (value.IsNullOrEmpty()) {
                    _origin = String.Empty;
                    return;
                }

                var origin = new Uri(value);
                if (!origin.IsAbsoluteUri || origin.Segments.Length > 1) {
                    onError("The syntax of value of Origin must be '<scheme>://<host>[:<port>]'.");
                    return;
                }

                _origin = value.TrimEnd('/');
            }
        }

        /// <summary>
        /// Gets the WebSocket subprotocol selected by the server.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the subprotocol if any. The default is <see cref="String.Empty"/>.
        /// </value>
        public string Protocol {
            get {
                return _protocol;
            }
        }

        /// <summary>
        /// Gets the state of the WebSocket connection.
        /// </summary>
        /// <value>
        /// One of the <see cref="WebApplication1.WsState"/> values. The default is <see cref="WsState.CONNECTING"/>.
        /// </value>
        public WebApplication1.WsState ReadyState {
            get {
                return _readyState;
            }
        }

        /// <summary>
        /// Gets the WebSocket URL to connect.
        /// </summary>
        /// <value>
        /// A <see cref="Uri"/> that contains the WebSocket URL to connect.
        /// </value>
        public Uri Url {
            get {
                return _uri;
            }

            internal set {
                if (_readyState == WebApplication1.WsState.CONNECTING && !_client)
                    _uri = value;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when the <see cref="WebSocket"/> receives a Close frame or the Close method is called.
        /// </summary>
        public event EventHandler<CloseEventArgs> OnClose;

        /// <summary>
        /// Occurs when the <see cref="WebSocket"/> gets an error.
        /// </summary>
        public event EventHandler<ErrorEventArgs> OnError;

        /// <summary>
        /// Occurs when the <see cref="WebSocket"/> receives a data frame.
        /// </summary>
        public event EventHandler<MessageEventArgs> OnMessage;

        /// <summary>
        /// Occurs when the WebSocket connection has been established.
        /// </summary>
        public event EventHandler OnOpen;

        #endregion

        #region Private Methods

        // As server
        private bool acceptHandshake() {
            if (!processRequestHandshake())
                return false;

            sendResponseHandshake();
            return true;
        }

        private void close( WebApplication1.PayloadData data ) {
#if DEBUG
      Console.WriteLine("WS: Info@close: Current thread IsBackground?: {0}", Thread.CurrentThread.IsBackground);
#endif
            lock (_forClose) {
                // Whether the closing handshake has been started already?
                if (_readyState == WebApplication1.WsState.CLOSING || _readyState == WebApplication1.WsState.CLOSED)
                    return;

                // Whether the closing handshake on server is started before the connection has been established?
                if (_readyState == WebApplication1.WsState.CONNECTING && !_client) {
                    sendResponseHandshake(WebApplication1.Net.HttpStatusCode.BadRequest);
                    onClose(new CloseEventArgs(data));

                    return;
                }

                _readyState = WebApplication1.WsState.CLOSING;
            }

            // Whether a payload data contains the close status code which must not be set for send?
            if (data.ContainsReservedCloseStatusCode) {
                onClose(new CloseEventArgs(data));
                return;
            }

            closeHandshake(data);
#if DEBUG
      Console.WriteLine("WS: Info@close: Exit close method.");
#endif
        }

        private void close( WebApplication1.Net.HttpStatusCode code ) {
            if (_readyState != WebApplication1.WsState.CONNECTING || _client)
                return;

            sendResponseHandshake(code);
            closeResources();
        }

        private void close( ushort code, string reason ) {
            using (var buffer = new MemoryStream()) {
                var tmp = code.ToByteArray(ByteOrder.BIG);
                buffer.Write(tmp, 0, tmp.Length);
                if (!reason.IsNullOrEmpty()) {
                    tmp = Encoding.UTF8.GetBytes(reason);
                    buffer.Write(tmp, 0, tmp.Length);
                }

                buffer.Close();
                var data = buffer.ToArray();
                if (data.Length > 125) {
                    var msg = "The payload length of a Close frame must be 125 bytes or less.";
                    onError(msg);

                    return;
                }

                close(new WebApplication1.PayloadData(data));
            }
        }

        private void closeHandshake( WebApplication1.PayloadData data ) {
            var args = new WebApplication1.CloseEventArgs(data);
            var frame = createControlFrame(WebApplication1.Opcode.CLOSE, data, _client);
            if (send(frame))
                args.WasClean = true;

            onClose(args);
        }

        private bool closeResources() {
            _readyState = WebApplication1.WsState.CLOSED;

            try {
                if (_client)
                    closeResourcesAsClient();
                else
                    closeResourcesAsServer();

                return true;
            } catch (Exception ex) {
                onError(ex.Message);
                return false;
            }
        }

        // As client
        private void closeResourcesAsClient() {
            if (_wsStream != null) {
                _wsStream.Dispose();
                _wsStream = null;
            }

            if (_tcpClient != null) {
                _tcpClient.Close();
                _tcpClient = null;
            }
        }

        // As server
        private void closeResourcesAsServer() {
            if (_context != null && _closeContext != null) {
                _closeContext();
                _wsStream = null;
                _context = null;
            }
        }

        private bool concatenateFragments( Stream dest ) {
            Func<WebApplication1.WsFrame, bool> processContinuation = contFrame => {
                if (!contFrame.IsContinuation)
                    return false;

                dest.WriteBytes(contFrame.PayloadData.ApplicationData);
                return true;
            };

            while (true) {
                var frame = _wsStream.ReadFrame();
                if (processAbnormal(frame))
                    return false;

                if (!frame.IsFinal) {
                    // MORE & CONT
                    if (processContinuation(frame))
                        continue;
                } else {
                    // FINAL & CONT
                    if (processContinuation(frame))
                        break;

                    // FINAL & PING
                    if (processPing(frame))
                        continue;

                    // FINAL & PONG
                    if (processPong(frame))
                        continue;

                    // FINAL & CLOSE
                    if (processClose(frame))
                        return false;
                }

                // ?
                processIncorrectFrame();
                return false;
            }

            return true;
        }

        private bool connect() {
            return _client
                   ? doHandshake()
                   : acceptHandshake();
        }

        // As client
        private static string createBase64Key() {
            var src = new byte[16];
            var rand = new Random();
            rand.NextBytes(src);

            return Convert.ToBase64String(src);
        }

        private static string createCompressionExtension( WebApplication1.CompressionMethod method ) {
            return createCurrentCompressionExtension(method);
        }

        private static WebApplication1.WsFrame createControlFrame( WebApplication1.Opcode opcode, WebApplication1.PayloadData payloadData, bool client ) {
            var mask = client ? WebApplication1.Mask.MASK : WebApplication1.Mask.UNMASK;
            var frame = new WebApplication1.WsFrame(WebApplication1.Fin.FINAL, opcode, mask, payloadData);

            return frame;
        }

        private static string createCurrentCompressionExtension( WebApplication1.CompressionMethod method ) {
            return method != WebApplication1.CompressionMethod.NONE
                   ? String.Format("permessage-{0}", method.ToString().ToLower())
                   : String.Empty;
        }

        private static string createDeprecatedCompressionExtension( WebApplication1.CompressionMethod method ) {
            return method != WebApplication1.CompressionMethod.NONE
                   ? String.Format("permessage-compress; method={0}", method.ToString().ToLower())
                   : String.Empty;
        }

        private static WebApplication1.WsFrame createFrame(
          WebApplication1.Fin fin, WebApplication1.Opcode opcode, WebApplication1.PayloadData payloadData, bool compressed, bool client ) {
              var mask = client ? WebApplication1.Mask.MASK : WebApplication1.Mask.UNMASK;
              var frame = new WebApplication1.WsFrame(fin, opcode, mask, payloadData, compressed);

            return frame;
        }

        // As client
        private string createRequestExtensions() {
            var extensions = new StringBuilder(64);
            var comp = createCompressionExtension(_compression);
            if (comp.Length > 0)
                extensions.Append(comp);

            return extensions.Length > 0
                   ? extensions.ToString()
                   : String.Empty;
        }

        // As client
        private WebApplication1.RequestHandshake createRequestHandshake() {
            var path = _uri.PathAndQuery;
            var host = _uri.Port == 80
                     ? _uri.DnsSafeHost
                     : _uri.Authority;

            var req = new WebApplication1.RequestHandshake(path);
            req.AddHeader("Host", host);

            if (_origin.Length > 0)
                req.AddHeader("Origin", _origin);

            req.AddHeader("Sec-WebSocket-Key", _base64key);

            if (!_protocols.IsNullOrEmpty())
                req.AddHeader("Sec-WebSocket-Protocol", _protocols);

            var extensions = createRequestExtensions();
            if (extensions.Length > 0)
                req.AddHeader("Sec-WebSocket-Extensions", extensions);

            req.AddHeader("Sec-WebSocket-Version", _version);

            if (_cookies.Count > 0)
                req.SetCookies(_cookies);

            return req;
        }

        // As server
        private WebApplication1.ResponseHandshake createResponseHandshake() {
            var res = new WebApplication1.ResponseHandshake();
            res.AddHeader("Sec-WebSocket-Accept", createResponseKey());
            if (_extensions.Length > 0)
                res.AddHeader("Sec-WebSocket-Extensions", _extensions);

            if (_cookies.Count > 0)
                res.SetCookies(_cookies);

            return res;
        }

        // As server
        private WebApplication1.ResponseHandshake createResponseHandshake( WebApplication1.Net.HttpStatusCode code ) {
            var res = WebApplication1.ResponseHandshake.CreateCloseResponse(code);
            res.AddHeader("Sec-WebSocket-Version", _version);

            return res;
        }

        private string createResponseKey() {
            var buffer = new StringBuilder(_base64key, 64);
            buffer.Append(_guid);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            var src = sha1.ComputeHash(Encoding.UTF8.GetBytes(buffer.ToString()));

            return Convert.ToBase64String(src);
        }

        // As client
        private bool doHandshake() {
            init();
            sendRequestHandshake();
            return processResponseHandshake();
        }

        private static WebApplication1.CompressionMethod getCompressionMethod( string value ) {
            var deprecated = createDeprecatedCompressionExtension(WebApplication1.CompressionMethod.DEFLATE);
            if (value.Equals(deprecated))
                return WebApplication1.CompressionMethod.DEFLATE;

            foreach (WebApplication1.CompressionMethod method in Enum.GetValues(typeof(WebApplication1.CompressionMethod)))
                if (isCompressionExtension(value, method))
                    return method;

            return WebApplication1.CompressionMethod.NONE;
        }

        // As client
        private void init() {
            _base64key = createBase64Key();

            var host = _uri.DnsSafeHost;
            var port = _uri.Port;
            _tcpClient = new TcpClient(host, port);
            _wsStream = WebApplication1.WsStream.CreateClientStream(_tcpClient, host, _secure);
        }

        // As server
        private void init( WebApplication1.Net.WebSockets.WebSocketContext context ) {
            _context = context;
            _uri = context.Path.ToUri();
            _secure = context.IsSecureConnection;
            _client = false;
        }

        private static bool isCompressionExtension( string value ) {
            return value.StartsWith("permessage-");
        }

        private static bool isCompressionExtension( string value, WebApplication1.CompressionMethod method ) {
            var expected = createCompressionExtension(method);
            return expected.Length > 0
                   ? value.Equals(expected)
                   : false;
        }

        private bool isOpened( bool errorIfOpened ) {
            if (_readyState != WebApplication1.WsState.OPEN && _readyState != WebApplication1.WsState.CLOSING)
                return false;

            if (errorIfOpened)
                onError("The WebSocket connection has been established already.");

            return true;
        }

        // As server
        private bool isValidHostHeader() {
            var authority = _context.Headers["Host"];
            if (authority.IsNullOrEmpty() || !_uri.IsAbsoluteUri)
                return true;

            var i = authority.IndexOf(':');
            var host = i > 0
                     ? authority.Substring(0, i)
                     : authority;
            var type = Uri.CheckHostName(host);

            return type != UriHostNameType.Dns
                   ? true
                   : Uri.CheckHostName(_uri.DnsSafeHost) != UriHostNameType.Dns
                     ? true
                     : host == _uri.DnsSafeHost;
        }

        // As server
        private bool isValidRequesHandshake() {
            return !_context.IsValid
                   ? false
                   : !isValidHostHeader()
                     ? false
                     : _context.Headers.Contains("Sec-WebSocket-Version", _version);
        }

        // As client
        private bool isValidResponseHandshake( WebApplication1.ResponseHandshake response ) {
            return !response.IsWebSocketResponse
                   ? false
                   : !response.ContainsHeader("Sec-WebSocket-Accept", createResponseKey())
                     ? false
                     : !response.ContainsHeader("Sec-WebSocket-Version") ||
                       response.ContainsHeader("Sec-WebSocket-Version", _version);
        }

        private void onClose( WebApplication1.CloseEventArgs eventArgs ) {
            if (!Thread.CurrentThread.IsBackground)
                if (_exitReceiving != null)
                    _exitReceiving.WaitOne(5 * 1000);

            if (!closeResources())
                eventArgs.WasClean = false;

            OnClose.Emit(this, eventArgs);
        }

        private void onError( string message ) {
#if DEBUG
      var callerFrame = new System.Diagnostics.StackFrame(1);
      var caller      = callerFrame.GetMethod();
      Console.WriteLine("WS: Error@{0}: {1}", caller.Name, message);
#endif
            OnError.Emit(this, new WebApplication1.ErrorEventArgs(message));
        }

        private void onMessage( WebApplication1.MessageEventArgs eventArgs ) {
            if (eventArgs != null)
                OnMessage.Emit(this, eventArgs);
        }

        private void onOpen() {
            _readyState = WebApplication1.WsState.OPEN;
            startReceiving();
            OnOpen.Emit(this, WebApplication1.MessageEventArgs.Empty);
        }

        private bool ping( byte[] data ) {
            var frame = createControlFrame(WebApplication1.Opcode.PING, new WebApplication1.PayloadData(data), _client);
            var timeOut = _client ? 5000 : 1000;

            return send(frame)
                   ? _receivePong.WaitOne(timeOut)
                   : false;
        }

        private void pong( WebApplication1.PayloadData data ) {
            var frame = createControlFrame(WebApplication1.Opcode.PONG, data, _client);
            send(frame);
        }

        private bool processAbnormal( WebApplication1.WsFrame frame ) {
            if (frame != null)
                return false;

#if DEBUG
      Console.WriteLine("WS: Info@processAbnormal: Start closing handshake.");
#endif
            var code = WebApplication1.CloseStatusCode.ABNORMAL;
            Close(code, code.GetMessage());

            return true;
        }

        private bool processClose( WebApplication1.WsFrame frame ) {
            if (!frame.IsClose)
                return false;

#if DEBUG
      Console.WriteLine("WS: Info@processClose: Start closing handshake.");
#endif
            close(frame.PayloadData);

            return true;
        }

        private bool processData( WebApplication1.WsFrame frame ) {
            if (!frame.IsData)
                return false;

            if (frame.IsCompressed && _compression == WebApplication1.CompressionMethod.NONE)
                return false;

            var args = frame.IsCompressed
                     ? new WebApplication1.MessageEventArgs(
                         frame.Opcode, frame.PayloadData.ApplicationData.Decompress(_compression))
                     : new WebApplication1.MessageEventArgs(frame.Opcode, frame.PayloadData);

            onMessage(args);
            return true;
        }

        private bool processFragmented( WebApplication1.WsFrame frame ) {
            // Not first fragment
            if (frame.IsContinuation)
                return true;

            // Not fragmented
            if (frame.IsFinal)
                return false;

            bool incorrect = !frame.IsData ||
                             (frame.IsCompressed && _compression == WebApplication1.CompressionMethod.NONE);

            if (!incorrect)
                processFragments(frame);
            else
                processIncorrectFrame();

            return true;
        }

        private void processFragments( WebApplication1.WsFrame first ) {
            using (var concatenated = new MemoryStream()) {
                concatenated.WriteBytes(first.PayloadData.ApplicationData);
                if (!concatenateFragments(concatenated))
                    return;

                byte[] data;
                if (_compression != WebApplication1.CompressionMethod.NONE) {
                    data = concatenated.DecompressToArray(_compression);
                } else {
                    concatenated.Close();
                    data = concatenated.ToArray();
                }

                onMessage(new WebApplication1.MessageEventArgs(first.Opcode, data));
            }
        }

        private void processFrame( WebApplication1.WsFrame frame ) {
            bool processed = processAbnormal(frame) ||
                             processFragmented(frame) ||
                             processData(frame) ||
                             processPing(frame) ||
                             processPong(frame) ||
                             processClose(frame);

            if (!processed)
                processIncorrectFrame();
        }

        private void processIncorrectFrame() {
#if DEBUG
      Console.WriteLine("WS: Info@processIncorrectFrame: Start closing handshake.");
#endif
            Close(WebApplication1.CloseStatusCode.INCORRECT_DATA);
        }

        private bool processPing( WebApplication1.WsFrame frame ) {
            if (!frame.IsPing)
                return false;

#if DEBUG
      Console.WriteLine("WS: Info@processPing: Return Pong.");
#endif
            pong(frame.PayloadData);

            return true;
        }

        private bool processPong( WebApplication1.WsFrame frame ) {
            if (!frame.IsPong)
                return false;

#if DEBUG
      Console.WriteLine("WS: Info@processPong: Receive Pong.");
#endif
            _receivePong.Set();

            return true;
        }

        // As server
        private void processRequestExtensions( string extensions ) {
            if (extensions.IsNullOrEmpty())
                return;

            var comp = false;
            var buffer = new List<string>();
            foreach (var extension in extensions.SplitHeaderValue(',')) {
                var e = extension.Trim();
                var tmp = e.RemovePrefix("x-webkit-");
                if (!comp && isCompressionExtension(tmp)) {
                    var method = getCompressionMethod(tmp);
                    if (method != WebApplication1.CompressionMethod.NONE) {
                        _compression = method;
                        comp = true;
                        buffer.Add(e);
                    }
                }
            }

            if (buffer.Count > 0)
                _extensions = buffer.ToArray().ToString(", ");
        }

        // As server
        private bool processRequestHandshake() {
#if DEBUG
      var req = RequestHandshake.Parse(_context);
      Console.WriteLine("WS: Info@processRequestHandshake: Request handshake from client:\n");
      Console.WriteLine(req.ToString());
#endif
            if (!isValidRequesHandshake()) {
                onError("Invalid WebSocket connection request.");
                close(WebApplication1.Net.HttpStatusCode.BadRequest);
                return false;
            }

            _base64key = _context.SecWebSocketKey;
            processRequestProtocols(_context.Headers["Sec-WebSocket-Protocol"]);
            processRequestExtensions(_context.Headers["Sec-WebSocket-Extensions"]);

            return true;
        }

        // As server
        private void processRequestProtocols( string protocols ) {
            if (!protocols.IsNullOrEmpty())
                _protocols = protocols;
        }

        // As client
        private void processResponseCookies( WebApplication1.Net.CookieCollection cookies ) {
            if (cookies.Count > 0)
                _cookies.SetOrRemove(cookies);
        }

        // As client
        private void processResponseExtensions( string extensions ) {
            var checkComp = _compression != WebApplication1.CompressionMethod.NONE
                          ? true
                          : false;

            var comp = false;
            if (!extensions.IsNullOrEmpty()) {
                foreach (var extension in extensions.SplitHeaderValue(',')) {
                    var e = extension.Trim();
                    if (checkComp &&
                        !comp &&
                        isCompressionExtension(e, _compression))
                        comp = true;
                }

                _extensions = extensions;
            }

            if (checkComp && !comp)
                _compression =  WebApplication1.CompressionMethod.NONE;
        }

        // As client
        private bool processResponseHandshake() {
            var res = receiveResponseHandshake();
            if (!isValidResponseHandshake(res)) {
                var msg = "Invalid response to this WebSocket connection request.";
                onError(msg);
                Close( WebApplication1.CloseStatusCode.ABNORMAL, msg);

                return false;
            }

            processResponseProtocol(res.Headers["Sec-WebSocket-Protocol"]);
            processResponseExtensions(res.Headers["Sec-WebSocket-Extensions"]);
            processResponseCookies(res.Cookies);

            return true;
        }

        // As client
        private void processResponseProtocol( string protocol ) {
            if (!protocol.IsNullOrEmpty())
                _protocol = protocol;
        }

        // As client
        private WebApplication1.ResponseHandshake receiveResponseHandshake() {
            var res = WebApplication1.ResponseHandshake.Parse(_wsStream.ReadHandshake());
#if DEBUG
      Console.WriteLine("WS: Info@receiveResponseHandshake: Response handshake from server:\n");
      Console.WriteLine(res.ToString());
#endif
            return res;
        }

        // As client
        private void send( WebApplication1.RequestHandshake request ) {
#if DEBUG
      Console.WriteLine("WS: Info@send: Request handshake to server:\n");
      Console.WriteLine(request.ToString());
#endif
            _wsStream.Write(request);
        }

        // As server
        private void send( WebApplication1.ResponseHandshake response ) {
#if DEBUG
      Console.WriteLine("WS: Info@send: Response handshake to client:\n");
      Console.WriteLine(response.ToString());
#endif
            _wsStream.Write(response);
        }

        private bool send( WebApplication1.WsFrame frame ) {
            lock (_forFrame) {
                var ready = _wsStream == null
                            ? false
                            : _readyState == WebApplication1.WsState.OPEN
                              ? true
                              : _readyState == WebApplication1.WsState.CLOSING
                                ? frame.IsClose
                                : false;

                if (!ready) {
                    onError("The WebSocket connection isn't established or has been closed.");
                    return false;
                }

                return _wsStream.Write(frame);
            }
        }

        private void send( WebApplication1.Opcode opcode, Stream stream ) {
            var data = stream;
            var compressed = false;
            try {
                if (_readyState != WebApplication1.WsState.OPEN) {
                    onError("The WebSocket connection isn't established or has been closed.");
                    return;
                }

                if (_compression != WebApplication1.CompressionMethod.NONE) {
                    data = data.Compress(_compression);
                    compressed = true;
                }

                var length = data.Length;
                lock (_forSend) {
                    if (length <= _fragmentLen)
                        send(WebApplication1.Fin.FINAL, opcode, data.ReadBytes((int)length), compressed);
                    else
                        sendFragmented(opcode, data, compressed);
                }
            } catch (Exception ex) {
                onError(ex.Message);
            } finally {
                if (compressed)
                    data.Dispose();

                stream.Dispose();
            }
        }

        private bool send( WebApplication1.Fin fin, WebApplication1.Opcode opcode, byte[] data, bool compressed ) {
            var frame = createFrame(fin, opcode, new WebApplication1.PayloadData(data), compressed, _client);
            return send(frame);
        }

        private void sendAsync( WebApplication1.Opcode opcode, Stream stream, Action completed ) {
            Action<WebApplication1.Opcode, Stream> sender = send;
            AsyncCallback callback = ar => {
                try {
                    sender.EndInvoke(ar);
                    if (completed != null)
                        completed();
                } catch (Exception ex) {
                    onError(ex.Message);
                }
            };

            sender.BeginInvoke(opcode, stream, callback, null);
        }

        private long sendFragmented( WebApplication1.Opcode opcode, Stream stream, bool compressed ) {
            var length = stream.Length;
            var quo = length / _fragmentLen;
            var rem = length % _fragmentLen;
            var count = rem == 0 ? quo - 2 : quo - 1;

            long readLen = 0;
            var tmpLen = 0;
            var buffer = new byte[_fragmentLen];

            // First
            tmpLen = stream.Read(buffer, 0, _fragmentLen);
            if (send(WebApplication1.Fin.MORE, opcode, buffer, compressed))
                readLen += tmpLen;
            else
                return 0;

            // Mid
            for (long i = 0; i < count; i++) {
                tmpLen = stream.Read(buffer, 0, _fragmentLen);
                if (send(WebApplication1.Fin.MORE, WebApplication1.Opcode.CONT, buffer, compressed))
                    readLen += tmpLen;
                else
                    return readLen;
            }

            // Final
            if (rem != 0)
                buffer = new byte[rem];
            tmpLen = stream.Read(buffer, 0, buffer.Length);
            if (send(WebApplication1.Fin.FINAL, WebApplication1.Opcode.CONT, buffer, compressed))
                readLen += tmpLen;

            return readLen;
        }

        // As client
        private void sendRequestHandshake() {
            var req = createRequestHandshake();
            send(req);
        }

        // As server
        private void sendResponseHandshake() {
            var res = createResponseHandshake();
            send(res);
        }

        // As server
        private void sendResponseHandshake( WebApplication1.Net.HttpStatusCode code ) {
            var res = createResponseHandshake(code);
            send(res);
        }

        private void startReceiving() {
            _exitReceiving = new AutoResetEvent(false);
            _receivePong = new AutoResetEvent(false);

            Action<WebApplication1.WsFrame> completed = null;
            completed = ( frame ) => {
                try {
                    processFrame(frame);
                    if (_readyState == WebApplication1.WsState.OPEN)
                        _wsStream.ReadFrameAsync(completed);
                    else
                        _exitReceiving.Set();
                } catch (WebApplication1.WebSocketException ex) {
                    Close(ex.Code, ex.Message);
                } catch (Exception) {
                    Close(WebApplication1.CloseStatusCode.ABNORMAL, "An exception has occured.");
                }
            };

            _wsStream.ReadFrameAsync(completed);
        }

        #endregion

        #region Internal Methods

        // As server
        internal void Close( WebApplication1.Net.HttpStatusCode code ) {
            close(code);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes the WebSocket connection and releases all associated resources.
        /// </summary>
        public void Close() {
            close(new WebApplication1.PayloadData());
        }

        /// <summary>
        /// Closes the WebSocket connection with the specified <paramref name="code"/> and
        /// releases all associated resources.
        /// </summary>
        /// <remarks>
        /// This Close method emits a <see cref="OnError"/> event if <paramref name="code"/> is not
        /// in the allowable range of the WebSocket close status code.
        /// </remarks>
        /// <param name="code">
        /// A <see cref="ushort"/> that indicates the status code for closure.
        /// </param>
        public void Close( ushort code ) {
            Close(code, String.Empty);
        }

        /// <summary>
        /// Closes the WebSocket connection with the specified <paramref name="code"/> and
        /// releases all associated resources.
        /// </summary>
        /// <param name="code">
        /// One of the <see cref="CloseStatusCode"/> values that indicates the status code for closure.
        /// </param>
        public void Close( WebApplication1.CloseStatusCode code ) {
            close((ushort)code, String.Empty);
        }

        /// <summary>
        /// Closes the WebSocket connection with the specified <paramref name="code"/> and
        /// <paramref name="reason"/>, and releases all associated resources.
        /// </summary>
        /// <remarks>
        /// This Close method emits a <see cref="OnError"/> event if <paramref name="code"/> is not
        /// in the allowable range of the WebSocket close status code.
        /// </remarks>
        /// <param name="code">
        /// A <see cref="ushort"/> that indicates the status code for closure.
        /// </param>
        /// <param name="reason">
        /// A <see cref="string"/> that contains the reason for closure.
        /// </param>
        public void Close( ushort code, string reason ) {
            if (!code.IsCloseStatusCode()) {
                var msg = String.Format("Invalid close status code: {0}", code);
                onError(msg);
                return;
            }

            close(code, reason);
        }

        /// <summary>
        /// Closes the WebSocket connection with the specified <paramref name="code"/> and
        /// <paramref name="reason"/>, and releases all associated resources.
        /// </summary>
        /// <param name="code">
        /// One of the <see cref="CloseStatusCode"/> values that indicates the status code for closure.
        /// </param>
        /// <param name="reason">
        /// A <see cref="string"/> that contains the reason for closure.
        /// </param>
        public void Close( WebApplication1.CloseStatusCode code, string reason ) {
            close((ushort)code, reason);
        }

        /// <summary>
        /// Establishes a WebSocket connection.
        /// </summary>
        public void Connect() {
            if (isOpened(true))
                return;

            try {
                if (connect())
                    onOpen();
            } catch {
                var msg = "An exception has occured.";
                onError(msg);
                Close(WebApplication1.CloseStatusCode.ABNORMAL, msg);
            }
        }

        /// <summary>
        /// Closes the WebSocket connection and releases all associated resources.
        /// </summary>
        /// <remarks>
        /// This method closes the WebSocket connection with the <see cref="CloseStatusCode.AWAY"/>.
        /// </remarks>
        public void Dispose() {
            Close(WebApplication1.CloseStatusCode.AWAY);
        }

        /// <summary>
        /// Sends a Ping using the WebSocket connection.
        /// </summary>
        /// <returns>
        /// <c>true</c> if a <see cref="WebSocket"/> instance receives a Pong in a time; otherwise, <c>false</c>.
        /// </returns>
        public bool Ping() {
            return ping(new byte[] { });
        }

        /// <summary>
        /// Sends a Ping with the specified <paramref name="message"/> using the WebSocket connection.
        /// </summary>
        /// <param name="message">
        /// A <see cref="string"/> that contains a message to send with a Ping.
        /// </param>
        /// <returns>
        /// <c>true</c> if a <see cref="WebSocket"/> instance receives a Pong in a time; otherwise, <c>false</c>.
        /// </returns>
        public bool Ping( string message ) {
            if (message.IsNullOrEmpty())
                return ping(new byte[] { });

            var data = Encoding.UTF8.GetBytes(message);
            if (data.Length > 125) {
                onError("The payload length of a Ping frame must be 125 bytes or less.");
                return false;
            }

            return ping(data);
        }

        /// <summary>
        /// Sends a binary <paramref name="data"/> using the WebSocket connection.
        /// </summary>
        /// <param name="data">
        /// An array of <see cref="byte"/> that contains a binary data to send.
        /// </param>
        public void Send( byte[] data ) {
            if (data == null) {
                onError("'data' must not be null.");
                return;
            }

            var stream = new MemoryStream(data);
            send(WebApplication1.Opcode.BINARY, stream);
        }

        /// <summary>
        /// Sends a text <paramref name="data"/> using the WebSocket connection.
        /// </summary>
        /// <param name="data">
        /// A <see cref="string"/> that contains a text data to send.
        /// </param>
        public void Send( string data ) {
            if (data == null) {
                onError("'data' must not be null.");
                return;
            }

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            send(WebApplication1.Opcode.TEXT, stream);
        }

        /// <summary>
        /// Sends a binary data using the WebSocket connection.
        /// </summary>
        /// <param name="file">
        /// A <see cref="FileInfo"/> that contains a binary data to send.
        /// </param>
        public void Send( FileInfo file ) {
            if (file == null) {
                onError("'file' must not be null.");
                return;
            }

            send(WebApplication1.Opcode.BINARY, file.OpenRead());
        }

        /// <summary>
        /// Sends a binary <paramref name="data"/> asynchronously using the WebSocket connection.
        /// </summary>
        /// <param name="data">
        /// An array of <see cref="byte"/> that contains a binary data to send.
        /// </param>
        /// <param name="completed">
        /// An <see cref="Action"/> delegate that references the method(s) called when
        /// the asynchronous operation completes.
        /// </param>
        public void SendAsync( byte[] data, Action completed ) {
            if (data == null) {
                onError("'data' must not be null.");
                return;
            }

            var stream = new MemoryStream(data);
            sendAsync(WebApplication1.Opcode.BINARY, stream, completed);
        }

        /// <summary>
        /// Sends a text <paramref name="data"/> asynchronously using the WebSocket connection.
        /// </summary>
        /// <param name="data">
        /// A <see cref="string"/> that contains a text data to send.
        /// </param>
        /// <param name="completed">
        /// An <see cref="Action"/> delegate that references the method(s) called when
        /// the asynchronous operation completes.
        /// </param>
        public void SendAsync( string data, Action completed ) {
            if (data == null) {
                onError("'data' must not be null.");
                return;
            }

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            sendAsync(WebApplication1.Opcode.TEXT, stream, completed);
        }

        /// <summary>
        /// Sends a binary data asynchronously using the WebSocket connection.
        /// </summary>
        /// <param name="file">
        /// A <see cref="FileInfo"/> that contains a binary data to send.
        /// </param>
        /// <param name="completed">
        /// An <see cref="Action"/> delegate that references the method(s) called when
        /// the asynchronous operation completes.
        /// </param>
        public void SendAsync( FileInfo file, Action completed ) {
            if (file == null) {
                onError("'file' must not be null.");
                return;
            }
            sendAsync(WebApplication1.Opcode.BINARY, file.OpenRead(), completed);
        }

        /// <summary>
        /// Sets a <see cref="Cookie"/> used in the WebSocket opening handshake.
        /// </summary>
        /// <param name="cookie">
        /// A <see cref="Cookie"/> that contains an HTTP Cookie to set.
        /// </param>
        public void SetCookie( WebApplication1.Net.Cookie cookie ) {
            if (isOpened(true))
                return;

            if (cookie == null) {
                onError("'cookie' must not be null.");
                return;
            }

            lock (_cookies.SyncRoot) {
                _cookies.SetOrRemove(cookie);
            }
        }

        #endregion
    }
}