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


namespace DjvuSharp
{
    /// <summary>
    /// The <see cref="DjvuPage" /> class represents the single page in
    /// a djvu document
    /// 
    /// <para>
    /// It can be created from the instance of <see cref="DjvuDocument" /> class
    /// with methods <see cref="DjvuDocument.CreateDjvuPageByPageNo(int)" /> 
    /// ToDo: Implement another method of creating a djvu page.
    /// </para>
    /// </summary>
    public class DjvuPage: IDisposable
    {
        private IntPtr _djvu_page;
        private bool disposedValue;

        internal DjvuPage(IntPtr djvu_page)
        {
            _djvu_page = djvu_page;
        }

        /// <summary>
        /// Access the job object in charge of decoding the document header.
        /// </summary>
        public DjvuJob PageJob
        {
            get
            {
                var job = Native.ddjvu_page_job(_djvu_page);

                if (job == null)
                    return null;

                return new DjvuJob(job);
            }
        }


        /// <summary>
        /// Returns the page height in pixels. Calling this function 
        /// before receiving a M_PageInfo message always yields 0.
        /// </summary>
        public int Height 
        { 
            get { return djvulibre.ddjvu_page_get_height(_djvu_page); } 
        }


        /// <summary>
        /// Returns the page width in pixels. Calling this function 
        /// before receiving a M_PageInfo message always yields 0.
        /// </summary>
        public int Width
        {
            get { return djvulibre.ddjvu_page_get_width(_djvu_page); }
        }


        /// <summary>
        /// Returns the page resolution in pixels per inch (dpi).
        /// Calling this function before receiving a M_PageInfo
        /// message yields a meaningless but plausible value.
        /// </summary>
        public int Resolution
        {
            get { return djvulibre.ddjvu_page_get_resolution(_djvu_page); }
        }


        /// <summary>
        /// Returns the gamma of the display for which this page was designed.
        /// Calling this function before receiving a M_PageInfo
        /// message yields a meaningless but plausible value.
        /// </summary>
        public double Gamma
        {
            get { return djvulibre.ddjvu_page_get_gamma(_djvu_page); }
        }

        
        /// <summary>
        /// Returns the version of the djvu file format.
        /// Calling this function before receiving a M_PageInfo
        /// message yields a meaningless but plausible value.
        /// </summary>
        public int Version
        {
            get { return djvulibre.ddjvu_page_get_version(_djvu_page); }
        }


        /// <summary>
        /// Returns the type of the page data.
        /// Calling this function before the termination of the
        /// decoding process might return <see cref="PageType.DDJVU_PAGETYPE_UNKNOWN" />.
        /// </summary>
        public PageType Type
        {
            get { return (PageType)djvulibre.ddjvu_page_get_type(_djvu_page); }
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

                djvulibre.ddjvu_page_release(_djvu_page);
                _djvu_page = null;
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
