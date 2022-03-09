/*
*   DjvuSharp - .NET bindings for DjvuLibre
*   Copyright (C) 2021 Prajwal Jadhav
*   
*   This program is free software; you can redistribute it and/or
*   modify it under the terms of the GNU General Public License
*   as published by the Free Software Foundation; either version 2
*   of the License, or (at your option) any later version.
*   
*   This program is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*   
*   You should have received a copy of the GNU General Public License
*   along with this program; if not, write to the Free Software
*   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System;
using System.Collections.Generic;
using System.Text;
using DjvuSharp.Interop;

namespace DjvuSharp.LispExpressions
{
    /// <summary>
    /// This class represents the lisp list aka a pair
    /// where:
    /// - the <code>car</code> represents the first element of a list.
    /// - the <code>cdr</code> usually is a pair representing the rest of the list.
    /// </summary>
    public class Pair
    {
        /// <summary>
        /// Pointer to native lisp expression (of type miniexp_t)
        /// </summary>
        private IntPtr _miniexp;
        
        /// <summary>
        /// Pointer to native document to which this s-expression belongs
        /// </summary>
        private IntPtr _document;

        public Pair(IntPtr miniexp, IntPtr document)
        {
            if (miniexp == IntPtr.Zero)
                throw new ArgumentException($"{nameof(miniexp)} cannot be equal to IntPtr.Zero.", nameof(miniexp));

            if (document == IntPtr.Zero)
                throw new ArgumentException($"{nameof(document)} cannot be equal to IntPtr.Zero.", nameof(document));

            _miniexp = miniexp;
            _document = document;
        }

        public Pair Caar()
        {
            IntPtr carResult = Native.miniexp_caar(_miniexp);
            var i = (ulong)carResult;

            throw new NotImplementedException();
        }

        ~Pair()
        {
            Native.ddjvu_miniexp_release(_document, _miniexp);
        }
    }
}
