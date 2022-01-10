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
    /// <summary> Creates a decoder for a DjVu document and starts
    /// decoding.  This function returns immediately.  The
    /// decoding job then generates messages to request the raw
    /// data and to indicate the state of the decoding process.
    /// </summay>
    public class DjvuDocument: IDisposable
    {
        private IntPtr _djvu_document;
        private bool disposedValue;

        /// <inheritdoc cref="DjvuDocument" />
        internal DjvuDocument(IntPtr djvu_document)
        {
            _djvu_document = djvu_document;
        }

        /// <summary>
        /// Access the job object in charge of decoding the document header.
        /// </summary>
        public DjvuJob DjvuJob
        {
            get 
            {
                IntPtr job = Native.ddjvu_document_job(_djvu_document);

                if (job == IntPtr.Zero)
                    return null;

                return new DjvuJob(job);
            }
        }

        /// <summary>
        /// Returns the type of a DjVu document.
        /// <para>
        /// This function might return <see cref="DDjvuDocumentType.DDJVU_DOCTYPE_UNKNOWN" />
        /// when called before receiving a m_docinfo message.
        /// </para>
        /// </summary>
        /// <returns>The type of djvu document in form of an enum member.</returns>
        public DDjvuDocumentType GetDocumentType()
        {
            return (DDjvuDocumentType)Native.ddjvu_document_get_type(_djvu_document);
        }


        /// <summary>
        /// Returns the number of pages in a DjVu document.
        /// <para>
        /// This function might return 1 when called 
        /// before receiving a m_docinfo message.
        /// </para>
        /// </summary>
        /// <returns>An int representing number of pages.</returns>
        public int GetPageNumber()
        {
            return Native.ddjvu_document_get_pagenum(_djvu_document);
        }

        /// <summary>
        /// Returns the number of component files. 
        /// This function might return 0 when called
        /// before receiving a m_docinfo message
        /// </summary>
        /// <returns>The number of component files</returns>
        public int GetFileNumber()
        {
            return Native.ddjvu_document_get_filenum(_djvu_document);
        }

        /// <summary>
        /// <p>When we construct a document, djvulibre starts decoding it in background.<p>
        /// <p>Also djvulibre decodes a document in chunks. Hence, we don't have to wait till
        /// all of the document is decoded.
        /// </p>
        /// <p>We use this function to know if the decoding of document is complete.</p>
        /// </summary>
        /// <returns>A boolean. true if decoding of the document is finished. false otherwise.</returns>
        public bool IsDecodingDone()
        {
            return this.DjvuJob.IsDone();
        }


        /// <summary>
        /// This function returns a UTF8 encoded text describing the contents
        /// of entire document using the same format as command: <code>djvudump</code>
        /// </summary>
        /// <param name="json">If parameter json is set to true output will be json formatted</param>
        /// <returns>returns a UTF8 encoded text describing the contents
        /// of entire document. May return null if decoding of the document is not done yet.
        /// </returns>
        public string GetDump(bool json)
        {
            return Native.ddjvu_document_get_dump(_djvu_document, json);
        }


        /// <summary>
        ///     Each page of a document can be accessed by creating a
        ///     <see cref="DjvuPage" /> object with this function.
        ///     <para>
        ///         This function may return NULL when called before receiving the
        ///         M_DocInfo message.
        ///     </para>
        ///     <para>Error messages will be generated if the page does not exists.</para>
        ///     <para>
        ///         Calling this function also initiates the data transfer
        ///         and the decoding threads for the specified page. Various messages
        ///         will document the progress of these operations.
        ///     </para>
        /// </summary>
        /// 
        /// <param name="pageNum">An integer between 0 to (total_pages - 1)</param>
        /// 
        /// <returns>A <see cref="DjvuPage" /></returns>
        /// 
        public DjvuPage CreateDjvuPageByPageNo(int pageNum)
        {
            IntPtr djvuPagePtr = Native.ddjvu_page_create_by_pageno(_djvu_document, pageNum);

            if (djvuPagePtr == IntPtr.Zero)
                return null;

            return new DjvuPage(djvuPagePtr);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                Native.ddjvu_document_release(_djvu_document);
                _djvu_document = IntPtr.Zero;
                
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~DjvuDocument()
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