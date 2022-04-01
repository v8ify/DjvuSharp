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
using DjvuSharp.Marshaler;

namespace DjvuSharp.LispExpressions
{
    /// <summary>
    /// The textual representation of a Symbol is a 
    /// sequence of printable characters forming an identifier.
    /// Each Symbol has a unique representation and remains
    /// permanently allocated.
    /// </summary>
    public class Symbol: Expression
    {
        /// <summary>
        /// Creates a Symbol with the specified name
        /// </summary>
        /// <param name="name"></param>
        public Symbol(string name)
        {
            _expression = Native.miniexp_symbol(name);
        }

        public Symbol(Expression expression)
        {
            if(!expression.IsSymbol())
            {
                throw new ArgumentException(
                    $"The parameter {nameof(expression)} doesn't point to a symbol lisp-expression.",
                    nameof(expression));
            }

            _expression = expression._expression;
        }

        /// <summary>
        /// Returns the symbol name as a string.
        /// Returns null if the expression is not a symbol.
        /// </summary>
        public string Name
        {
            get
            {
                IntPtr namePtr = Native.miniexp_to_name(_expression);
                return CustomStringMarshaler.GetInstance("").MarshalNativeToManaged(namePtr) as string;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
