using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1 {
    public class ErrorEventArgs : EventArgs {
        
        #region Internal Constructors
        internal ErrorEventArgs( string message ) {
            Message = message;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains a error message.
        /// </value>
        public string Message { get; private set; }
        #endregion
    }
}