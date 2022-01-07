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

using Djvulibre.Internal; // SWIGTYPE_p_ddjvu_page_s
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
        private SWIGTYPE_p_ddjvu_page_s _djvu_page;
        private bool disposedValue;

        internal DjvuPage(SWIGTYPE_p_ddjvu_page_s djvu_page)
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
                var job = djvulibre.ddjvu_page_job(_djvu_page);

                if (job == null)
                    return null;

                return new DjvuJob(job);
            }
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
