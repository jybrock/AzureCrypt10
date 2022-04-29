using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1 {
    /// <summary>
    /// Contains the values that indicate whether the byte order is a Little-endian or Big-endian.
    /// </summary>
    public enum ByteOrder : byte {
        /// <summary>
        /// Indicates a Little-endian.
        /// </summary>
        LITTLE,
        /// <summary>
        /// Indicates a Big-endian.
        /// </summary>
        BIG
    }
}
