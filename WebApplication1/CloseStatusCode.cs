using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1 {
    /// <summary>
    /// Contains the values of the status codes for the WebSocket connection closure.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>CloseStatusCode</b> enumeration contains the values of the status codes for the WebSocket connection closure
    /// defined in <a href="http://tools.ietf.org/html/rfc6455#section-7.4.1">RFC 6455</a> for the WebSocket protocol.
    /// </para>
    /// <para>
    /// "<b>Reserved value</b>" must not be set as a status code in a close control frame by an endpoint.
    /// It is designated for use in applications expecting a status code to indicate that connection
    /// was closed due to a system grounds.
    /// </para>
    /// </remarks>
    public enum CloseStatusCode : ushort {
        /// <summary>
        /// Equivalent to close status 1000. Indicates a normal closure.
        /// </summary>
        NORMAL = 1000,
        /// <summary>
        /// Equivalent to close status 1001. Indicates that an endpoint is "going away".
        /// </summary>
        AWAY = 1001,
        /// <summary>
        /// Equivalent to close status 1002. Indicates that an endpoint is terminating the connection
        /// due to a protocol error.
        /// </summary>
        PROTOCOL_ERROR = 1002,
        /// <summary>
        /// Equivalent to close status 1003. Indicates that an endpoint is terminating the connection
        /// because it has received a type of data it cannot accept.
        /// </summary>
        INCORRECT_DATA = 1003,
        /// <summary>
        /// Equivalent to close status 1004. Still undefined. Reserved value.
        /// </summary>
        UNDEFINED = 1004,
        /// <summary>
        /// Equivalent to close status 1005. Indicates that no status code was actually present. Reserved value.
        /// </summary>
        NO_STATUS_CODE = 1005,
        /// <summary>
        /// Equivalent to close status 1006. Indicates that the connection was closed abnormally. Reserved value.
        /// </summary>
        ABNORMAL = 1006,
        /// <summary>
        /// Equivalent to close status 1007. Indicates that an endpoint is terminating the connection
        /// because it has received data within a message that was not consistent with the type of the message.
        /// </summary>
        INCONSISTENT_DATA = 1007,
        /// <summary>
        /// Equivalent to close status 1008. Indicates that an endpoint is terminating the connection
        /// because it has received a message that violates its policy.
        /// </summary>
        POLICY_VIOLATION = 1008,
        /// <summary>
        /// Equivalent to close status 1009. Indicates that an endpoint is terminating the connection
        /// because it has received a message that is too big for it to process.
        /// </summary>
        TOO_BIG = 1009,
        /// <summary>
        /// Equivalent to close status 1010. Indicates that an endpoint (client) is terminating the connection
        /// because it has expected the server to negotiate one or more extension, but the server didn't return
        /// them in the response message of the WebSocket handshake.
        /// </summary>
        IGNORE_EXTENSION = 1010,
        /// <summary>
        /// Equivalent to close status 1011. Indicates that a server is terminating the connection because it encountered
        /// an unexpected condition that prevented it from fulfilling the request.
        /// </summary>
        SERVER_ERROR = 1011,
        /// <summary>
        /// Equivalent to close status 1015. Indicates that the connection was closed due to a failure to perform
        /// a TLS handshake. Reserved value.
        /// </summary>
        TLS_HANDSHAKE_FAILURE = 1015


    }
}