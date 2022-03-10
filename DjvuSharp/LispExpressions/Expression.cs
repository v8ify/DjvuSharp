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
using System.Runtime.InteropServices;
using DjvuSharp.Interop;

namespace DjvuSharp.LispExpressions
{
    /// <summary>
    /// The most generic lisp-expression.
    /// It can be converted (or cast) to other more specific types of lisp-expression like Pair, etc
    /// But before doing that it's actual type must be checked.
    /// </summary>
    public class Expression
    {
        private IntPtr _expression;
        private IntPtr _document;

        public Expression(IntPtr expression, IntPtr document)
        {
            _expression = expression;
            _document = document;
        }

        /// <summary>
        /// Checks whether the current s-expression is a symbol.
        /// Essential when converting to more specific type like pair, IntExpression, etc
        /// </summary>
        /// <returns>True if this expression is symbol; false otherwise</returns>
        public bool IsSymbol()
        {
            long i = _expression.ToInt64();

            return (i & 3) == 2;
        }

        /// <summary>
        /// Checks whether the current s-expression is a pair.
        /// Essential when converting to more specific type like pair, IntExpression, etc
        /// </summary>
        /// <returns>True if this expression is pair; false otherwise</returns>
        public bool IsPair()
        {
            long i = _expression.ToInt64();

            return (i & 3) == 0;
        }

        /// <summary>
        /// Checks whether the current s-expression is a StringExpression.
        /// Essential when converting to more specific type like pair, IntExpression, etc
        /// </summary>
        /// <returns>True if this expression is StringExpression; false otherwise</returns>
        public bool IsStringExpression()
        {
            int result = Native.miniexp_stringp(_expression);

            return !(result == 0);
        }

        /// <summary>
        /// Checks whether the current s-expression is a FloatExpression.
        /// Essential when converting to more specific type like pair, IntExpression, etc
        /// </summary>
        /// <returns>True if this expression is FloatExpression; false otherwise</returns>
        public bool IsFloatExpression()
        {
            int result = Native.miniexp_floatnump(_expression);

            return !(result == 0);
        }

        /// <summary>
        /// Checks whether the current s-expression is a IntExpression.
        /// Essential when converting to more specific type like pair, IntExpression, etc
        /// </summary>
        /// <returns>True if this expression is IntExpression; false otherwise</returns>
        public bool IsIntExpression()
        {
            long i = _expression.ToInt64();

            return (i & 3) == 0;
        }
    }
}
