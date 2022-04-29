using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication1 {
    /// <summary>
    /// Contains the event data associated with a <see cref="WebSocket.OnMessage"/> event.
    /// </summary>
    /// <remarks>
    /// The <see cref="WebSocket.OnMessage"/> event occurs when the WebSocket receives a text or binary data frame.
    /// If you want to get the received data, you should access the <see cref="MessageEventArgs.Data"/> or
    /// <see cref="MessageEventArgs.RawData"/> properties.
    /// </remarks>
    public class MessageEventArgs : EventArgs {
        #region Private Fields

        private string _data;
        private Opcode _opcode;
        private byte[] _rawData;

        #endregion

        #region Internal Constructors

        internal MessageEventArgs( Opcode opcode, byte[] rawData ) {
            if ((ulong)rawData.LongLength > PayloadData.MaxLength)
                throw new WebSocketException(CloseStatusCode.TOO_BIG);

            _opcode = opcode;
            _rawData = rawData;
        }

        internal MessageEventArgs( Opcode opcode, PayloadData data ) {
            _opcode = opcode;
            _rawData = data.ApplicationData;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the received data as a <see cref="string"/>.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the received data.
        /// </value>
        public string Data {
            get {
                _data = _data != null
                      ? _data
                      : _rawData.LongLength == 0
                        ? String.Empty
                        : _opcode == Opcode.TEXT
                          ? Encoding.UTF8.GetString(_rawData)
                          : _opcode.ToString();

                return _data;
            }
        }

        /// <summary>
        /// Gets the received data as an array of <see cref="byte"/>.
        /// </summary>
        /// <value>
        /// An array of <see cref="byte"/> that contains the received data.
        /// </value>
        public byte[] RawData {
            get {
                return _rawData;
            }
        }

        /// <summary>
        /// Gets the type of the received data.
        /// </summary>
        /// <value>
        /// One of the <see cref="Opcode"/> values that indicates the type of the received data.
        /// </value>
        public Opcode Type {
            get {
                return _opcode;
            }
        }

        #endregion
    }
}