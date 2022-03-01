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
using DjvuSharp.Enums;
using DjvuSharp.Rendering;
using System.IO;
using DjvuSharp.Interop;
using DjvuSharp.Utility;

namespace DjvuSharp
{
    public class Thumbnail
    {
        private DjvuDocument _document;
        private int _pageNo;

        public Thumbnail(DjvuDocument document, int pageNo)
        {
            _document = document;
            _pageNo = pageNo;

            // We start decoding thumbail as well by passing 1 as last argument.
            JobStatus status = Native.ddjvu_thumbnail_status(_document.Document, pageNo, 1);

            while (true)
            {
                status = Native.ddjvu_thumbnail_status(_document.Document, pageNo, 0);

                if (Utils.IsDecodingDone(status))
                {
                    break;
                }
                else
                {
                    Utils.ProcessMessages(document.Context, true);
                }
            }

            if (status == JobStatus.JOB_FAILED)
            {
                throw new ApplicationException($"Failed to create page thumbail. Page no.: {_pageNo}");
            }
            else if (status == JobStatus.JOB_STOPPED)
            {
                throw new ApplicationException($"Page thumbnail creation interrupted by the user.  Page no.: {_pageNo}");
            }
        }

        public byte[] Render(int pageNo, ref int width, ref int height, PixelFormat pixelFormat, long rowAlignment = 1)
        {
            if (rowAlignment <= 0)
                throw new ArgumentOutOfRangeException(nameof(rowAlignment), rowAlignment, $"Must be a greater than 0");

            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), width, $"Must be a greater than 0");

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), height, $"Must be a greater than 0");

            long rowSize = Utils.CalculateRowSize(width, rowAlignment, pixelFormat.Bpp);

            byte[] buffer = Utils.AllocateImageMemory(rowSize, height);

            int result = Native.ddjvu_thumbnail_render(_document.Document, pageNo, ref width, ref height, pixelFormat.NativePtr, (ulong)rowSize, buffer);

            if (result == 0)
            {
                return null;
            }

            return buffer;
        }

        public Stream RenderBitmap(int pageNo, ref int width, ref int height, PixelFormat pixelFormat, long rowAlignment = 1)
        {
            byte[] imageBuffer = this.Render(pageNo, ref width, ref height, pixelFormat, rowAlignment);

            byte[] bitmapBuffer = Bitmap.GenerateBitmapImage(imageBuffer, width, height);

            Stream bitmap = new MemoryStream();

            using (BinaryWriter bw = new BinaryWriter(bitmap))
            {
                foreach(byte i in bitmapBuffer)
                {
                    bw.Write(i);
                }
            }

            return bitmap;
        }
    }
}
