﻿/*
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
    public class PixelFormatPalette: PixelFormat
    {
        public PixelFormatPalette(Dictionary<ValueTuple<int, int, int>, int> pallete): base()
        {
            bppValue = 8;

            uint[] _palette = new uint[216];

            for (int i = 0; i < 6; ++i)
            {
                for (int j = 0; j < 6; ++j)
                {
                    for (int k = 0; k < 6; ++k)
                    {
                        int palleteValue = pallete[(i, j, k)];

                        if (palleteValue < 0)
                        {
                            throw new ArgumentException($"Values in {nameof(pallete)} must not be negative.", nameof(pallete));
                        }

                        if (palleteValue > 0x100)
                        {
                            throw new ArgumentException($"Values in {nameof(pallete)} must be less than 0x100", nameof(pallete));
                        }

                        uint n = (uint)palleteValue;

                        _palette[i * 6 * 6 + j * 6 + k] = n;
                    }
                }
            }

            djvu_format = Native.ddjvu_format_create(PixelFormatStyle.GREY8, 216, _palette);

            if (djvu_format == IntPtr.Zero)
            {
                throw new ApplicationException($"Failed to create {nameof(PixelFormatPalette)}");
            }
        }
    }
}
