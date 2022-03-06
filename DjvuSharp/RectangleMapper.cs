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
using DjvuSharp.Enums;

namespace DjvuSharp
{
    public class RectangleMapper
    {
        private readonly IntPtr _rect_mapper;

        public RectangleMapper(Rectangle input, Rectangle output)
        {
            _rect_mapper = Native.ddjvu_rectmapper_create(input, output);

            if (_rect_mapper == IntPtr.Zero)
            {
                throw new ApplicationException($"Failed to create {nameof(RectangleMapper)}.");
            }
        }

        public void Modify(PageRotation rotation, int mirrorX, int mirrorY)
        {
            Native.ddjvu_rectmapper_modify(_rect_mapper, rotation, mirrorX, mirrorY);
        }

        public void MapPoint(ref int x, ref int y)
        {
            Native.ddjvu_map_point(_rect_mapper, ref x, ref y);
        }

        public void MapRectangle(Rectangle rectangle)
        {
            Native.ddjvu_map_rect(_rect_mapper, rectangle);
        }

        public void UnmapPoint(ref int x, ref int y)
        {
            Native.ddjvu_unmap_point(_rect_mapper, ref x, ref y);
        }

        public void UnmapRectangle(Rectangle rectangle)
        {
            Native.ddjvu_unmap_rect(_rect_mapper, rectangle);
        }

        ~RectangleMapper()
        {
            Native.ddjvu_rectmapper_release(_rect_mapper);
        }
    }
}
