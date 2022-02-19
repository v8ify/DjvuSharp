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
    public class PixelFormatRGBMask32: PixelFormat
    {
        public PixelFormatRGBMask32(int redMask, int greenMask, int blueMask, int xor = 0) : base()
        {
            bppValue = 16;

            if (redMask < 0)
                throw new ArgumentOutOfRangeException(nameof(redMask), redMask, $"Argument value must be non-negative");

            if (greenMask < 0)
                throw new ArgumentOutOfRangeException(nameof(greenMask), greenMask, $"Argument value must be non-negative");

            if (blueMask < 0)
                throw new ArgumentOutOfRangeException(nameof(blueMask), blueMask, $"Argument value must be non-negative");

            if (xor < 0)
                throw new ArgumentOutOfRangeException(nameof(xor), xor, $"Argument value must be non-negative");

            uint red = ((uint)redMask);
            uint green = ((uint)greenMask);
            uint blue = ((uint)blueMask);
            uint XOR = ((uint)xor);

            red &= 0xFFFFFFFF;
            green &= 0xFFFFFFFF;
            blue &= 0xFFFFFFFF;
            XOR &= 0xFFFFFFFF;

            uint[] args = new uint[4] { red, green, blue, XOR };

            djvu_format = Native.ddjvu_format_create(PixelFormatStyle.RGBMASK16, 4, args);

            if (djvu_format == IntPtr.Zero)
            {
                throw new ApplicationException($"Failed to create {nameof(PixelFormatRGBMask32)}");
            }
        }
    }
}
