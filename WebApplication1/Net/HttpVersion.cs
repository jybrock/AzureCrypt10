//
// HttpVersion.cs
//	Copied from System.Net.HttpVersion.cs
//
// Author:
//	Lawrence Pit (loz@cable.a2000.nl)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

namespace WebApplication1.Net {

	/// <summary>
	/// Provides the HTTP version numbers.
	/// </summary>
	public class HttpVersion {

		/// <summary>
		/// Provides a <see cref="Version"/> instance for HTTP 1.0.
		/// </summary>
		public static readonly Version Version10 = new Version (1, 0);

		/// <summary>
		/// Provides a <see cref="Version"/> instance for HTTP 1.1.
		/// </summary>
		public static readonly Version Version11 = new Version (1, 1);

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpVersion"/> class.
		/// </summary>
		public HttpVersion () {}
	}
}
