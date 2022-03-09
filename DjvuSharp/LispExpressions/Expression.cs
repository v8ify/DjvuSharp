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

        public bool IsSymbol()
        {
            throw new NotImplementedException();
        }

        public bool IsPair()
        {
            throw new NotImplementedException();
        }

        public bool IsStringExpression()
        {
            throw new NotImplementedException();
        }

        public bool IsFloatExpression()
        {
            throw new NotImplementedException();
        }

        public bool IsIntExpression()
        {
            throw new NotImplementedException();
        }
    }
}
