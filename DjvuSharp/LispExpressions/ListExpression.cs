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
    public class ListExpression: Expression
    {
        public ListExpression(Expression car, Expression cdr)
        {
            if (car._document != cdr._document)
                throw new ArgumentException($"{nameof(car)} and {nameof(cdr)} must belong to the same document");

            IntPtr ptr = Native.miniexp_cons(car._expression, cdr._expression);

            _expression = ptr;
            _document = car._document;
        }

        public ListExpression(IntPtr expression, IntPtr document): base(expression, document)
        {

        }

        public Expression GetNthElement(int n)
        {
            if (n >= Length || n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), n, $"Please ensure you are not accessing list elements in valid range.");

            IntPtr ptr = Native.miniexp_nth(n, _expression);

            return new Expression(ptr, _document);
        }

        /// <summary>
        /// Returns length of the list. Returns 0 for non-lists and -1 for circular lists
        /// </summary>
        public int Length { get => Native.miniexp_length(_expression); }

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

        /// <summary>
        /// Replaces the 'car' of this ListExpression
        /// </summary>
        /// <param name="newCar">New value of the car</param>
        public void ReplaceCar(Expression newCar)
        {
            Native.miniexp_rplaca(_expression, newCar._expression);
        }

        /// <summary>
        /// Replaces the 'cdr' of this ListExpression
        /// </summary>
        /// <param name="newCdr">New value of the cdr</param>
        public void ReplaceCdr(Expression newCdr)
        {
            Native.miniexp_rplacd(_expression, newCdr._expression);
        }
    }
}
