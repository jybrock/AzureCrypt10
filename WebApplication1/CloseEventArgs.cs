using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication1 {

    /// <summary>
    /// Contains the event data associated with a <see cref="WebSocket.OnClose"/> event.
    /// </summary>
    /// <remarks>
    /// The <see cref="WebSocket.OnClose"/> event occurs when the WebSocket receives a close control frame or
    /// the <c>WebSocket.Close</c> method is called. If you want to get the reason for closure, you should access
    /// the <see cref="CloseEventArgs.Code"/> or <see cref="CloseEventArgs.Reason"/> properties.
    /// </remarks>
    public class CloseEventArgs : MessageEventArgs {
        #region Fields

        private bool _clean;
        private ushort _code;
        private string _reason;

        #endregion

        #region Constructors

        internal CloseEventArgs( PayloadData data )
            : base( Opcode.CLOSE, data ) {
            _code = getCodeFrom(data);
            _reason = getReasonFrom(data);
            _clean = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status code for closure.
        /// </summary>
        /// <value>
        /// A <see cref="ushort"/> that contains a status code for closure.
        /// </value>
        public ushort Code {
            get {
                return _code;
            }
        }

        /// <summary>
        /// Gets the reason for closure.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains a reason for closure.
        /// </value>
        public string Reason {
            get {
                return _reason;
            }
        }

        /// <summary>
        /// Indicates whether the WebSocket connection closed cleanly.
        /// </summary>
        /// <value>
        /// <c>true</c> if the WebSocket connection closed cleanly; otherwise, <c>false</c>.
        /// </value>
        public bool WasClean {
            get {
                return _clean;
            }

            internal set {
                _clean = value;
            }
        }

        #endregion

        #region Private Methods

        private static ushort getCodeFrom( PayloadData data ) {
            var appData = data.ApplicationData;
            return appData.Length >= 2
                   ? appData.SubArray(0, 2).To<ushort>(ByteOrder.BIG)
                   : (ushort)CloseStatusCode.NO_STATUS_CODE;
        }

        private static string getReasonFrom( PayloadData data ) {
            var appData = data.ApplicationData;
            var appDataLen = appData.Length;
            if (appDataLen <= 2)
                return String.Empty;

            var buffer = appData.SubArray(2, appDataLen - 2);
            return Encoding.UTF8.GetString(buffer);
        }

        #endregion
    }
}