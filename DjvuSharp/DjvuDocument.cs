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

using Djvulibre.Internal;
using System; // IDisposable

namespace DjvuSharp
{
    /// <summary> Creates a decoder for a DjVu document and starts
    /// decoding.  This function returns immediately.  The
    /// decoding job then generates messages to request the raw
    /// data and to indicate the state of the decoding process.
    /// </summay>
    public class DjvuDocument: IDisposable
    {
        private SWIGTYPE_p_ddjvu_document_s _document;
        private bool disposedValue;

        /// <inheritdoc cref="DjvuDocument" />
        internal DjvuDocument(SWIGTYPE_p_ddjvu_context_s context, string filename, bool shouldCache)
        {
            // Since C code expects us to pass 1 or 0 instead or true or false.
            int cache = shouldCache ? 1 : 0;

            _document = djvulibre.ddjvu_document_create_by_filename_utf8(context, filename, cache);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DjvuDocument()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}