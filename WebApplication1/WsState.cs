using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1 {
    /// <summary>
    /// Contains the values of the state of the WebSocket connection.
    /// </summary>
    /// <remarks>
    /// The <b>WsState</b> enumeration contains the values of the state of the WebSocket connection defined in
    /// <a href="http://www.w3.org/TR/websockets/#dom-websocket-readystate">The WebSocket API</a>.
    /// </remarks>
    public enum WsState : ushort {
        /// <summary>
        /// Equivalent to numeric value 0. Indicates that the connection has not yet been established.
        /// </summary>
        CONNECTING = 0,
        /// <summary>
        /// Equivalent to numeric value 1. Indicates that the connection is established and communication is possible.
        /// </summary>
        OPEN = 1,
        /// <summary>
        /// Equivalent to numeric value 2. Indicates that the connection is going through the closing handshake, or the <b>Close</b> method has been invoked.
        /// </summary>
        CLOSING = 2,
        /// <summary>
        /// Equivalent to numeric value 3. Indicates that the connection has been closed or could not be opened.
        /// </summary>
        CLOSED = 3
    }
}