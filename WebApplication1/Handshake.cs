using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebApplication1 {
    internal abstract class Handshake {

        #region Protected Const Fields

        protected const string CrLf = "\r\n";

        #endregion

        #region Protected Constructors

        protected Handshake() {
            ProtocolVersion = HttpVersion.Version11;
            Headers = new NameValueCollection();
        }

        #endregion

        #region Public Properties

        public NameValueCollection Headers {
            get;
            protected set;
        }

        public Version ProtocolVersion {
            get;
            protected set;
        }

        #endregion

        #region Public Methods

        public void AddHeader( string name, string value ) {
            Headers.Add(name, value);
        }

        public bool ContainsHeader( string name ) {
            return Headers.Contains(name);
        }

        public bool ContainsHeader( string name, string value ) {
            return Headers.Contains(name, value);
        }

        public string[] GetHeaderValues( string name ) {
            return Headers.GetValues(name);
        }

        public byte[] ToByteArray() {
            return Encoding.UTF8.GetBytes(ToString());
        }

        #endregion
    }
}