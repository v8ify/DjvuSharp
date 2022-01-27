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

using DjvuSharp.Enums;
using System;
using System.Runtime.InteropServices;

namespace DjvuSharp.Messages
{
    [StructLayout(LayoutKind.Sequential)]
    public class AnyMessage
    {
        /// <summary>
        /// The kind of message corresponding to enum <see cref="Message.MessageTag"/>
        /// </summary>
        public MessageTag Tag;

        /* context, document, page, job fields may be IntPtr.Zero when not relevant.
         * These fields are also cleared when the corresponding object is 
         * released with ddjvu_{job,page,document}_release methods.
         */
        public IntPtr Context;
        public IntPtr Document;
        public IntPtr Page;
        public IntPtr Job;
    }
}
