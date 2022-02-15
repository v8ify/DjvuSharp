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

using System; // IDisposable
using DjvuSharp.Enums; // PageStatus
using DjvuSharp.Rendering;

namespace DjvuSharp
{
    /// <summary>
    /// This class represents the single page in a djvu document
    /// </summary>
    public class DjvuPage: IDisposable
    {
        private IntPtr _djvu_page;
        private bool disposedValue;

        /// <summary>
        /// Each page of a document can be accessed by creating a
        /// <see cref="DjvuPage" /> object with this function.
        /// </summary>
        /// <param name="document">The <see cref="DjvuDocument" /> to which this page belongs to.</param>
        /// <param name="pageNumber">An integer between 0 to (total_pages - 1)</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DjvuPage(DjvuDocument document, int pageNumber)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (pageNumber < 0 || (pageNumber >= document.PageNumber))
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            }

            _djvu_page = IntPtr.Zero;

            try
            {
                _djvu_page = Native.ddjvu_page_create_by_pageno(document.Document, pageNumber);

                if (_djvu_page == IntPtr.Zero)
                {
                    throw new ApplicationException($"Failed to create page from page number.");
                }

                JobStatus status = JobStatus.JOB_NOTSTARTED;

                while (true)
                {
                    status = Native.ddjvu_page_decoding_status(_djvu_page);

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
                    throw new ApplicationException($"Failed to decode page with page number: {pageNumber}");
                }
                else if (status == JobStatus.JOB_STOPPED)
                {
                    throw new ApplicationException($"Decoding interrupted by user. Page number: {pageNumber}");
                }

                Document = document;
                PageNumber = pageNumber;
            }
            catch (Exception)
            {
                if (_djvu_page != IntPtr.Zero)
                {
                    Native.ddjvu_page_release(_djvu_page);
                }

                throw;
            }
        }

        public DjvuDocument Document { get; private set; }
        public int PageNumber { get; private set; }
        public IntPtr NativePagePtr { get { return _djvu_page; } }

        /// <summary>
        /// Returns the page height in pixels. Calling this function 
        /// before receiving a M_PageInfo message always yields 0.
        /// </summary>
        public int Height 
        { 
            get { return Native.ddjvu_page_get_height(_djvu_page); } 
        }


        /// <summary>
        /// Returns the page width in pixels. Calling this function 
        /// before receiving a M_PageInfo message always yields 0.
        /// </summary>
        public int Width
        {
            get { return Native.ddjvu_page_get_width(_djvu_page); }
        }


        /// <summary>
        /// Returns the page resolution in pixels per inch (dpi).
        /// Calling this function before receiving a M_PageInfo
        /// message yields a meaningless but plausible value.
        /// </summary>
        public int Resolution
        {
            get { return Native.ddjvu_page_get_resolution(_djvu_page); }
        }


        /// <summary>
        /// Returns the gamma of the display for which this page was designed.
        /// Calling this function before receiving a M_PageInfo
        /// message yields a meaningless but plausible value.
        /// </summary>
        public double Gamma
        {
            get { return Native.ddjvu_page_get_gamma(_djvu_page); }
        }

        
        /// <summary>
        /// Returns the version of the djvu file format.
        /// Calling this function before receiving a M_PageInfo
        /// message yields a meaningless but plausible value.
        /// </summary>
        public int Version
        {
            get { return Native.ddjvu_page_get_version(_djvu_page); }
        }


        /// <summary>
        /// Returns the type of the page data.
        /// Calling this function before the termination of the
        /// decoding process might return <see cref="PageType.UNKNOWN" />.
        /// </summary>
        public PageType Type
        {
            get { return Native.ddjvu_page_get_type(_djvu_page); }
        }

        /// <summary>
        /// The counter-clockwise rotation angle for the DjVu page.
        /// Rotation is automatically taken into account by <see cref="Width"/> and <see cref="Height"/>
        /// </summary>
        public PageRotation Rotation
        {
            get { return Native.ddjvu_page_get_rotation(_djvu_page); }
            set { Native.ddjvu_page_set_rotation(_djvu_page, value); }
        }

        /// <summary>
        /// The original rotation value of the page as a counter-clockwise angle. 
        /// Specified by the orientation flags in the DjVu file.
        /// </summary>
        public PageRotation InitialRotation
        {
            get { return Native.ddjvu_page_get_initial_rotation(_djvu_page); }
        }


        public sbyte[] Render(RenderMode mode, Rectangle pageRect, Rectangle renderRect, PixelFormat pixelFormat, long rowAlignment=1)
        {
            if (rowAlignment <= 0)
            {
                throw new ArgumentException($"{nameof(rowAlignment)} must be a greater than 0", nameof(rowAlignment));
            }

            long rowSize = Utils.CalculateRowSize(renderRect.Width, rowAlignment, pixelFormat.Bpp);

            sbyte[] imageBuffer = Utils.AllocateImageMemory(rowSize, renderRect.Height);

            int success = Native.ddjvu_page_render(_djvu_page, mode, pageRect, renderRect, pixelFormat.NativePtr, (ulong)rowSize, imageBuffer);

            // Failed to render the page
            if (success == 0)
            {
                throw new ApplicationException($"Failed to render page. Page no.: {PageNumber}");
            }

            return imageBuffer;
        }


        /* 
            Implementing Dispose pattern below
        */
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {// TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
                    // TODO: dispose managed state (managed objects)
                }

                if (_djvu_page != IntPtr.Zero)
                {
                    Native.ddjvu_page_release(_djvu_page);
                    _djvu_page = IntPtr.Zero;
                }

                disposedValue = true;
            }
        }

        ~DjvuPage()
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
