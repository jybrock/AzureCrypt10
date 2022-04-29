using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1 {
    /// <summary>
    /// Contains the values of the compression methods used to compress the payload data of the WebSocket Data frame.
    /// </summary>
    /// <remarks>
    /// The <b>CompressionMethod</b> enumeration contains the values of the compression methods defined in
    /// <see href="http://tools.ietf.org/html/draft-ietf-hybi-permessage-compression-09">Compression Extensions for WebSocket</see>.
    /// </remarks>
    public enum CompressionMethod : byte {
        /// <summary>
        /// Indicates non compression.
        /// </summary>
        NONE,
        /// <summary>
        /// Indicates using DEFLATE.
        /// </summary>
        DEFLATE
    }
}