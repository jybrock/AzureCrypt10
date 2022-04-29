using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace WebApplication1 {
    internal class PayloadData : IEnumerable<byte> {
        #region Public Fields
        public const ulong MaxLength = long.MaxValue;
        #endregion

        #region Public Constructors
        public PayloadData()
            : this(new byte[] { }) {
        }

        public PayloadData( byte[] appData )
            : this(new byte[] { }, appData) {
        }

        public PayloadData( string appData )
            : this(Encoding.UTF8.GetBytes(appData)) {
        }

        public PayloadData( byte[] appData, bool masked )
            : this(new byte[] { }, appData, masked) {
        }

        public PayloadData( byte[] extData, byte[] appData )
            : this(extData, appData, false) {
        }

        public PayloadData( byte[] extData, byte[] appData, bool masked ) {
            if ((ulong)extData.LongLength + (ulong)appData.LongLength > MaxLength)
                throw new ArgumentOutOfRangeException(
                  "The length of 'extData' plus 'appData' must be less than MaxLength."
                  );

            ExtensionData = extData;
            ApplicationData = appData;
            IsMasked = masked;
        }
        #endregion

        #region Internal Properties
        internal bool ContainsReservedCloseStatusCode {
            get {
                if (Length >= 2) {
                    var code = ToByteArray().SubArray(0, 2).To<ushort>(ByteOrder.BIG);
                    if (code == (ushort)CloseStatusCode.UNDEFINED ||
                        code == (ushort)CloseStatusCode.NO_STATUS_CODE ||
                        code == (ushort)CloseStatusCode.ABNORMAL ||
                        code == (ushort)CloseStatusCode.TLS_HANDSHAKE_FAILURE)
                        return true;
                }

                return false;
            }
        }

        internal bool IsMasked {
            get;
            private set;
        }

        internal ulong Length {
            get {
                return (ulong)(ExtensionData.LongLength + ApplicationData.LongLength);
            }
        }
        #endregion

        #region Public Properties
        public byte[] ExtensionData {
            get;
            private set;
        }

        public byte[] ApplicationData {
            get;
            private set;
        }
        #endregion

        #region Private Methods
        private static void mask( byte[] src, byte[] key ) {
            for (long i = 0; i < src.LongLength; i++)
                src[i] = (byte)(src[i] ^ key[i % 4]);
        }
        #endregion

        #region Internal Methods
        #endregion

        #region Public Methods
        public IEnumerator<byte> GetEnumerator() {
            foreach (byte b in ExtensionData)
                yield return b;

            foreach (byte b in ApplicationData)
                yield return b;
        }

        public void Mask( byte[] maskingKey ) {
            if (ExtensionData.LongLength > 0)
                mask(ExtensionData, maskingKey);

            if (ApplicationData.LongLength > 0)
                mask(ApplicationData, maskingKey);

            IsMasked = !IsMasked;
        }

        public byte[] ToByteArray() {
            return ExtensionData.LongLength > 0
                   ? this.ToArray()
                   : ApplicationData;
        }

        public override string ToString() {
            return BitConverter.ToString(ToByteArray());
        }

        #endregion

        #region Explicitly Implemented Interface Members

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        #endregion
    }
}
