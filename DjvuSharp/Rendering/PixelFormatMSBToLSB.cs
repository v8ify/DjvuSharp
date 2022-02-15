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
using DjvuSharp.Enums;

namespace DjvuSharp.Rendering
{
    public class PixelFormatMSBToLSB: PixelFormat
    {
        public PixelFormatMSBToLSB(): base()
        {
            _bpp = 1;

            _djvu_format = Native.ddjvu_format_create(PixelFormatStyle.MSBTOLSB, 0, null);

            if (_djvu_format == IntPtr.Zero)
            {
                throw new ApplicationException($"Failed to create {nameof(PixelFormatMSBToLSB)}");
            }
        }
    }
}
