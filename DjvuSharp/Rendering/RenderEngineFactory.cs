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
using System.Runtime.InteropServices;

namespace DjvuSharp.Rendering
{
    public class RenderEngineFactory
    {
        public static RenderEngine CreateRenderEngine(PixelFormatStyle style, int[] mask = null)
        {
            int numArgs = 0;

            if (style == PixelFormatStyle.RGBMASK16 || style == PixelFormatStyle.RGBMASK32)
            {
                if (mask == null)
                {
                    throw new ArgumentException($"The argument {nameof(mask)} cannot be null when {nameof(style)} is" +
                        $" {nameof(PixelFormatStyle.RGBMASK16)} or {nameof(PixelFormatStyle.RGBMASK16)}",
                        nameof(mask));
                }
                
                if (mask.Length != 3 || mask.Length != 4)
                {
                    throw new ArgumentException($"The argument {nameof(mask)} should contain 3 or 4 elements when {nameof(style)} is" +
                        $" {nameof(PixelFormatStyle.RGBMASK16)} or {nameof(PixelFormatStyle.RGBMASK16)}",
                        nameof(mask));
                }

                numArgs = mask.Length;
            }
            else if (style == PixelFormatStyle.PALETTE8)
            {
                if (mask == null)
                {
                    throw new ArgumentException($"The argument {nameof(mask)} cannot be null when {nameof(style)} is {nameof(PixelFormatStyle.PALETTE8)}",
                        nameof(mask));
                }

                if (mask.Length != 216)
                {
                    throw new ArgumentException($"The argument {nameof(mask)} should contain 216 elements when {nameof(style)} is {nameof(PixelFormatStyle.PALETTE8)}",
                        nameof(mask));
                }

                numArgs = mask.Length;
            }

            IntPtr format = Native.ddjvu_format_create(style, numArgs, mask);

            if (format == IntPtr.Zero)
            {
                throw new ApplicationException($"Failed to create the render engine.");
            }

            return new RenderEngine(format, style);
        }
    }
}
