using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebApplication1;
using WebApplication1.Net.WebSockets;
using WebApplication1.Net.Security;


namespace WebApplication1 {
    internal class WsStream : IDisposable {
        
        #region Private Fields
        private Stream _innerStream;
        private bool _isSecure;
        private Object _forRead;
        private Object _forWrite;
        #endregion

        #region Private Constructors
        private WsStream() {
            _forRead = new object();
            _forWrite = new object();
        }
        #endregion

        #region Public Constructors
        public WsStream( NetworkStream innerStream )
            : this() {
            if (innerStream.IsNull())
                throw new ArgumentNullException("innerStream");

            _innerStream = innerStream;
            _isSecure = false;
        }

        public WsStream( WebApplication1.Net.Security.SslStream innerStream )
            : this() {
            if (innerStream.IsNull())
                throw new ArgumentNullException("innerStream");

            _innerStream = innerStream;
            _isSecure = true;
        }
        #endregion

        #region Public Properties
        public bool DataAvailable {
            get {
                return _isSecure
                       ? ((WebApplication1.Net.Security.SslStream)_innerStream).DataAvailable
                       : ((NetworkStream)_innerStream).DataAvailable;
            }
        }

        public bool IsSecure {
            get {
                return _isSecure;
            }
        }
        #endregion

        #region Private Methods
        private int read( byte[] buffer, int offset, int size ) {
            var readLen = _innerStream.Read(buffer, offset, size);
            if (readLen < size) {
                var msg = String.Format("Data can not be read from {0}.", _innerStream.GetType().Name);
                throw new IOException(msg);
            }

            return readLen;
        }

        private int readByte() {
            return _innerStream.ReadByte();
        }

        private string[] readHandshake() {
            var buffer = new List<byte>();
            while (true) {
                if (readByte().EqualsAndSaveTo('\r', buffer) &&
                    readByte().EqualsAndSaveTo('\n', buffer) &&
                    readByte().EqualsAndSaveTo('\r', buffer) &&
                    readByte().EqualsAndSaveTo('\n', buffer))
                    break;
            }

            return Encoding.UTF8.GetString(buffer.ToArray())
                   .Replace("\r\n", "\n").Replace("\n\n", "\n").TrimEnd('\n')
                   .Split('\n');
        }

        private bool write( byte[] data ) {
            lock (_forWrite) {
                try {
                    _innerStream.Write(data, 0, data.Length);
                    return true;
                } catch {
                    return false;
                }
            }
        }
        #endregion

        #region Internal Methods

        internal static WebApplication1.WsStream CreateClientStream( TcpClient client, string host, bool secure ) {
            var netStream = client.GetStream();
            if (secure) {
                System.Net.Security.RemoteCertificateValidationCallback validationCb = ( sender, certificate, chain, sslPolicyErrors ) => {
                    // FIXME: Always returns true
                    return true;
                };

                var sslStream = new WebApplication1.Net.Security.SslStream(netStream, false, validationCb);
                sslStream.AuthenticateAsClient(host);

                return new WebApplication1.WsStream(sslStream);
            }

            return new WebApplication1.WsStream(netStream);
        }

        internal static WebApplication1.WsStream CreateServerStream( TcpClient client, bool secure ) {
            var netStream = client.GetStream();
            if (secure) {
                var sslStream = new WebApplication1.Net.Security.SslStream(netStream, false);
                var certPath = ConfigurationManager.AppSettings["ServerCertPath"];
                sslStream.AuthenticateAsServer(new X509Certificate2(certPath));

                return new WebApplication1.WsStream(sslStream);
            }

            return new WebApplication1.WsStream(netStream);
        }

        internal static WebApplication1.WsStream CreateServerStream( WebApplication1.Net.HttpListenerContext context ) {
            var conn = context.Connection;
            var stream = conn.Stream;

            return conn.IsSecure
                   ? new WebApplication1.WsStream((WebApplication1.Net.Security.SslStream)stream)
                   : new WebApplication1.WsStream((NetworkStream)stream);
        }
        #endregion

        #region Public Methods
        public void Close() {
            _innerStream.Close();
        }

        public void Dispose() {
            _innerStream.Dispose();
        }

        public WebApplication1.WsFrame ReadFrame() {
            lock (_forRead) {
                try {
                    return WebApplication1.WsFrame.Parse(_innerStream);
                } catch {
                    return null;
                }
            }
        }

        public void ReadFrameAsync( Action<WebApplication1.WsFrame> completed ) {
            WebApplication1.WsFrame.ParseAsync(_innerStream, completed);
        }

        public string[] ReadHandshake() {
            lock (_forRead) {
                try {
                    return readHandshake();
                } catch {
                    return null;
                }
            }
        }

        public bool Write( WebApplication1.WsFrame frame ) {
            return write(frame.ToByteArray());
        }

        public bool Write( WebApplication1.Handshake handshake ) {
            return write(handshake.ToByteArray());
        }
        #endregion
    }
}