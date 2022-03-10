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
    public class Pair: Expression
    {
        public Pair(IntPtr expression, IntPtr document): base(expression, document)
        {

        }

        public Expression Caar()
        {
            IntPtr result = Native.miniexp_caar(_expression);

            return new Expression(result, _document);
        }

        public Expression Cadr()
        {
            IntPtr result = Native.miniexp_cadr(_expression);

            return new Expression(result, _document);
        }

        public Expression Cddr()
        {
            IntPtr result = Native.miniexp_cddr(_expression);

            return new Expression(result, _document);
        }

        public Expression Caddr()
        {
            IntPtr result = Native.miniexp_caddr(_expression);

            return new Expression(result, _document);
        }

        public Expression Cdddr()
        {
            IntPtr result = Native.miniexp_cdddr(_expression);

            return new Expression(result, _document);
        }
    }
}
