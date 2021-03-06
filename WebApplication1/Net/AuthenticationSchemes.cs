//
// AuthenticationSchemes.cs
//	Copied from System.Net.AuthenticationSchemes.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2005 Novell, Inc. (http://www.novell.com)
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
	/// Contains the values of the schemes for authentication.
	/// </summary>
	[Flags]
	public enum AuthenticationSchemes {

		/// <summary>
		/// Indicates that no authentication is allowed.
		/// </summary>
		None,
		/// <summary>
		/// Indicates digest authentication.
		/// </summary>
		Digest                          = 1,
		/// <summary>
		/// Indicates negotiating with the client to determine the authentication scheme.
		/// </summary>
		Negotiate                       = 2,
		/// <summary>
		/// Indicates NTLM authentication.
		/// </summary>
		Ntlm                            = 4,
		/// <summary>
		/// Indicates Windows authentication.
		/// </summary>
		IntegratedWindowsAuthentication = 6,
		/// <summary>
		/// Indicates basic authentication.
		/// </summary>
		Basic                           = 8,
		/// <summary>
		/// Indicates anonymous authentication.
		/// </summary>
		Anonymous                       = 0x8000,
	}
}
