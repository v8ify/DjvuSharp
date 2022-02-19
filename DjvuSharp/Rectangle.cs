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

namespace DjvuSharp
{
    /// <summary>
    /// This structure specifies the location of a rectangle.
    /// Coordinates are usually expressed in pixels relative to
    /// the BOTTOM LEFT CORNER(but see ddjvu_format_set_y_direction).
    /// Members <see cref="X"/> and <see cref="Y"/> indicate the position of the bottom left
    /// corner of the rectangle Members <see cref="Width"/> and <see cref="Height"/> indicate the
    /// width and height of the rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class Rectangle
    {
        internal int X;
        internal int Y;
        internal uint Width;
        internal uint Height;

        public Rectangle(int x, int y, int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException($"The arguments {nameof(width)} and {nameof(height)} must not be negative.");

            X = x;
            Y = y;
            Width = ((uint)width);
            Height = ((uint)height);
        }
    }
}
