using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1 {
    /// <summary>
    /// Represents the exception that occurred when attempting to perform an operation on the WebSocket connection.
    /// </summary>
    public class WebSocketException : Exception {
        #region Internal Constructors

        internal WebSocketException()
            : this(CloseStatusCode.ABNORMAL) {
        }

        internal WebSocketException( CloseStatusCode code )
            : this(code, code.GetMessage()) {
        }

        internal WebSocketException( CloseStatusCode code, string message )
            : base(message) {
            Code = code;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the <see cref="CloseStatusCode"/> associated with a <see cref="WebSocketException"/>.
        /// </summary>
        /// <value>
        /// One of the <see cref="CloseStatusCode"/> values that indicates the cause of the exception.
        /// </value>
        public CloseStatusCode Code {
            get;
            private set;
        }

        #endregion
    }
}