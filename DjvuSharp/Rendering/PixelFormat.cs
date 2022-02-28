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

namespace DjvuSharp.Rendering
{
    public class PixelFormat
    {
        protected IntPtr djvu_format;
        protected int bppValue;

        private bool disposedValue;

        protected PixelFormat()
        {

        }

        public int Bpp { get { return bppValue; } }

        public IntPtr NativePtr { get { return djvu_format; } }

        public void SetGamma(double gamma = 2.2)
        {
            Native.ddjvu_format_set_gamma(djvu_format, gamma);
        }

        public void SetRowOrder(bool topToBottom = false)
        {
            Native.ddjvu_format_set_row_order(djvu_format, topToBottom);
        }

        public void SetYDirection(bool topToBottom = false)
        {
            Native.ddjvu_format_set_y_direction(djvu_format, topToBottom);
        }

        public void SetDitherBits(int bits)
        {
            Native.ddjvu_format_set_ditherbits(djvu_format, bits);
        }

        public void SetWhite(byte blue, byte green, byte red)
        {
            Native.ddjvu_format_set_white(djvu_format, blue, green, red);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                if (djvu_format != IntPtr.Zero)
                {
                    Native.ddjvu_format_release(djvu_format);
                    djvu_format = IntPtr.Zero;
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
