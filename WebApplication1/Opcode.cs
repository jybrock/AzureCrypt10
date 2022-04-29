using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1 {
    /// <summary>
    /// Contains the values of the opcodes that denotes the frame type of the WebSocket frame.
    /// </summary>
    /// <remarks>
    /// The <b>Opcode</b> enumeration contains the values of the opcodes defined in
    /// <see href="http://tools.ietf.org/html/rfc6455#section-5.2">RFC 6455</see> for the WebSocket protocol.
    /// </remarks>
    public enum Opcode : byte {
        /// <summary>
        /// Equivalent to numeric value 0. Indicates a continuation frame.
        /// </summary>
        CONT = 0x0,
        /// <summary>
        /// Equivalent to numeric value 1. Indicates a text frame.
        /// </summary>
        TEXT = 0x1,
        /// <summary>
        /// Equivalent to numeric value 2. Indicates a binary frame.
        /// </summary>
        BINARY = 0x2,
        /// <summary>
        /// Equivalent to numeric value 8. Indicates a connection close frame.
        /// </summary>
        CLOSE = 0x8,
        /// <summary>
        /// Equivalent to numeric value 9. Indicates a ping frame.
        /// </summary>
        PING = 0x9,
        /// <summary>
        /// Equivalent to numeric value 10. Indicates a pong frame.
        /// </summary>
        PONG = 0xa
    }
}