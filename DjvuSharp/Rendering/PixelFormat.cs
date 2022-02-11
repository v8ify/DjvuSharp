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

namespace DjvuSharp.Rendering
{
    public class PixelFormat
    {
        protected IntPtr _djvu_format;
        protected int _bpp;

        private bool disposedValue;

        protected PixelFormat()
        {

        }

        public int Bpp { get { return _bpp; } }

        public IntPtr NativePtr { get { return _djvu_format; } }

        public void SetGamma(double gamma = 2.2)
        {
            Native.ddjvu_format_set_gamma(_djvu_format, gamma);
        }

        public void SetRowOrder(bool topToBottom = false)
        {
            Native.ddjvu_format_set_row_order(_djvu_format, topToBottom);
        }

        public void SetYDirection(bool topToBottom = false)
        {
            Native.ddjvu_format_set_y_direction(_djvu_format, topToBottom);
        }

        public void SetDitherBits(int bits)
        {
            Native.ddjvu_format_set_ditherbits(_djvu_format, bits);
        }

        public void SetWhite(byte blue, byte green, byte red)
        {
            Native.ddjvu_format_set_white(_djvu_format, blue, green, red);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                if (_djvu_format != IntPtr.Zero)
                {
                    Native.ddjvu_format_release(_djvu_format);
                    _djvu_format = IntPtr.Zero;
                }

                disposedValue = true;
            }
        }

        ~PixelFormat()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
