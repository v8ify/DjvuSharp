/*
*   DjvuSharp - .NET bindings for DjvuLibre
*   Copyright (C) 2022 Prajwal Jadhav
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
using DjvuSharp.Marshaler;
using System.Runtime.InteropServices;

namespace DjvuSharp
{
    public class Annotation
    {
        private readonly IntPtr _annotation;
        private readonly ICustomMarshaler _stringMarshaler;

        internal Annotation(IntPtr annotation)
        {
            _annotation = annotation;
            _stringMarshaler = CustomStringMarshaler.GetInstance("");
        }

        /// <summary>
        /// Specify the color of the viewer area surrounding the DjVu image. 
        /// Colors are represented with the X11 hexadecimal syntax #RRGGBB.
        /// For instance, #000000 is black and #FFFFFF is white.
        /// 
        /// Returns null if this information is not specified.
        /// </summary>
        public string BackgroundColor
        {
            get
            {
                IntPtr ptr = Native.ddjvu_anno_get_bgcolor(_annotation);
                return _stringMarshaler.MarshalNativeToManaged(ptr) as string;
            }
        }

        /// <summary>
        /// Specify the initial zoom factor of the image. Argument zoomvalue
        /// can be one of stretch, one2one, width, page, or composed of the
        /// letter d followed by a number in range 1 to 999 representing a zoom
        /// factor (such as in d300 or d150 for instance.)
        ///
        /// Returns null if this information is not specified.
        /// </summary>
        public string Zoom
        {
            get
            {
                IntPtr ptr = Native.ddjvu_anno_get_zoom(_annotation);
                return _stringMarshaler.MarshalNativeToManaged(ptr) as string;
            }
        }

        /// <summary>
        /// Specify the initial display mode of the image. 
        /// It is one of color, bw, fore, or back.
        /// 
        /// Returns null if this information is not specified.
        /// </summary>
        public string Mode
        {
            get
            {
                IntPtr ptr = Native.ddjvu_anno_get_mode(_annotation);
                return _stringMarshaler.MarshalNativeToManaged(ptr) as string;
            }
        }

        /// <summary>
        /// Specify how the image should be aligned on the viewer surface.
        /// By default the image is located in the center. 
        /// The value of HorzAlign can be one of 'left', 'center', or 'right'.
        /// </summary>
        public string HorzAlign
        {
            get
            {
                IntPtr ptr = Native.ddjvu_anno_get_horizalign(_annotation);
                return _stringMarshaler.MarshalNativeToManaged(ptr) as string;
            }
        }

        /// <summary>
        /// Specify how the image should be aligned on the viewer surface.
        /// By default the image is located in the center. 
        /// The value of VertAlign can be one of 'top', 'center', or 'bottom'. 
        /// </summary>
        public string VertAlign
        {
            get
            {
                IntPtr ptr = Native.ddjvu_anno_get_vertalign(_annotation);
                return _stringMarshaler.MarshalNativeToManaged(ptr) as string;
            }
        }

        public Dictionary<string, string> Metadata
        {
            get
            {
                Dictionary<string, string> metadata = new Dictionary<string, string>();

                unsafe
                {
                    void** ptr = Native.ddjvu_anno_get_metadata_keys(_annotation);

                    while ((int)*ptr != 0)
                    {
                        IntPtr strPtr = Native.ddjvu_anno_get_metadata(ptr);
                        IntPtr keyPtr = new IntPtr(*ptr);

                        string value = _stringMarshaler.MarshalNativeToManaged(strPtr) as string;

                        string key = _stringMarshaler.MarshalNativeToManaged(Native.miniexp_to_name(keyPtr)) as string;

                        metadata.Add(key, value);

                        ptr++;
                    }

                    return metadata;
                }
            }
        }
    }
}