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
        // Todo - make CLS compliant by removing unsigned int from public api
        public PixelFormatRGBMask32(uint redMask, uint greenMask, uint blueMask, uint xor = 0) : base()
        {
            bppValue = 16;

            /*if (redMask < 0 || greenMask < 0 || blueMask < 0 || xor < 0)
            {
                throw new ArgumentException("Arguments must not be negative");
            }*/

            redMask &= 0xFFFFFFFF;
            greenMask &= 0xFFFFFFFF;
            blueMask &= 0xFFFFFFFF;
            xor &= 0xFFFFFFFF;

            uint[] args = new uint[4] { redMask, greenMask, blueMask, xor };

            djvu_format = Native.ddjvu_format_create(PixelFormatStyle.RGBMASK16, 4, args);

            if (djvu_format == IntPtr.Zero)
            {
                throw new ApplicationException($"Failed to create {nameof(PixelFormatRGBMask32)}");
            }
        }
    }
}
