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

using DjvuSharp.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using DjvuSharp.Interop;

namespace DjvuSharp.Rendering
{
    /// <summary>
    /// Used for rendering a page
    /// </summary>
    public class RenderEngine : IDisposable
    {
        private IntPtr _djvu_format;
        private readonly PixelFormatStyle _formatStyle;

        private bool disposedValue;

        internal RenderEngine(IntPtr djvu_format, PixelFormatStyle formatStyle)
        {
            _djvu_format = djvu_format;
            _formatStyle = formatStyle;
        }

        public byte[] RenderPage(DjvuPage page, RenderMode mode, Rectangle pageRect, Rectangle renderRect)
        {
            uint rowSize;

            if (_formatStyle == PixelFormatStyle.MSBTOLSB)
            {
                rowSize = ((renderRect.Width) + 7) / 8;
            }
            else if (_formatStyle == PixelFormatStyle.GREY8)
            {
                rowSize = renderRect.Width;
            }
            else
            {
                rowSize = renderRect.Width * 3;
            }

            byte[] imageBuffer = new byte[rowSize * renderRect.Height];  

            unsafe
            {
                fixed (byte* p = imageBuffer)
                {
                    IntPtr ptr = (IntPtr)p;
                    int success = Native.ddjvu_page_render(page.NativePagePtr, mode, pageRect, renderRect, _djvu_format, rowSize, ptr);

                    if (success == 0)
                    {
                        throw new ApplicationException($"Failed to render page. Page no.: {page.PageNumber}");
                    }

                    return imageBuffer;
                }
            }
        }

        public byte[] RenderPageThumbnail(Thumbnail thumbnail, ref int width, ref int height)
        {
            uint rowSize;

            if (_formatStyle == PixelFormatStyle.MSBTOLSB)
            {
                rowSize = ((((uint)width)) + 7) / 8;
            }
            else if (_formatStyle == PixelFormatStyle.GREY8)
            {
                rowSize = ((uint)width);
            }
            else
            {
                rowSize = ((uint)width) * 3;
            }

            byte[] imageBuffer = new byte[rowSize * height];

            unsafe
            {
                fixed (byte* p = imageBuffer)
                {
                    IntPtr ptr = (IntPtr)p;
                    int success = Native.ddjvu_thumbnail_render(thumbnail.Document.Document, thumbnail.PageNo, ref width, ref height,
                        _djvu_format, rowSize, ptr);

                    if (success == 0)
                    {
                        throw new ApplicationException($"Failed to render thumbnail. Page no.: {thumbnail.PageNo}");
                    }

                    return imageBuffer;
                }
            }
        }

        public Tuple<int, int> GetThumbailDimensions(IntPtr document, int pageNo)
        {
            int width = 10;
            int height = 0;

            int success = Native.ddjvu_thumbnail_render(document, pageNo, ref width, ref height,
                        _djvu_format, 100, IntPtr.Zero);

            if (success == 0)
            {
                throw new ApplicationException($"Failed to render thumbnail. Page no.: {pageNo}");
            }

            return new Tuple<int, int>(width, height);
        }

        public RenderEngine SetGamma(double gamma = 2.2)
        {
            Native.ddjvu_format_set_gamma(_djvu_format, gamma);
            return this;
        }
        public RenderEngine SetRowOrder(bool topToBottom = false)
        {
            Native.ddjvu_format_set_row_order(_djvu_format, topToBottom);
            return this;
        }
        public RenderEngine SetYDirection(bool topToBottom = false)
        {
            Native.ddjvu_format_set_y_direction(_djvu_format, topToBottom);
            return this;
        }
        public RenderEngine SetDitherBits(int bits)
        {
            Native.ddjvu_format_set_ditherbits(_djvu_format, bits);
            return this;
        }
        public RenderEngine SetWhite(byte blue, byte green, byte red)
        {
            Native.ddjvu_format_set_white(_djvu_format, blue, green, red);
            return this;
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
        ~RenderEngine()
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
