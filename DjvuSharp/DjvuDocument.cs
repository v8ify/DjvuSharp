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
using DjvuSharp.Enums; // DjvuDocumentType
using System.IO; // Path
using System.Diagnostics; // Process

namespace DjvuSharp
{
    /// <summary> Creates a decoder for a DjVu document and starts
    /// decoding.  This function returns immediately.  The
    /// decoding job then generates messages to request the raw
    /// data and to indicate the state of the decoding process.
    /// </summay>
    public class DjvuDocument : IDisposable
    {
        private IntPtr _document;

        private IntPtr _context;

        private string _filePath;

        private bool disposedValue;

        /// <inheritdoc cref="DjvuDocument"/>
        private DjvuDocument(IntPtr context, IntPtr document, string filePath)
        {
            _context = context;
            _document = document;
            _filePath = filePath;
        }

        public static DjvuDocument Create(string filePath)
        {
            IntPtr context = IntPtr.Zero;
            IntPtr document = IntPtr.Zero;

            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentNullException($"The value of {nameof(filePath)} cannot be null or empty");
                }

                if (!File.Exists(filePath))
                {
                    throw new ApplicationException($"The djvu file with path {nameof(filePath)} does not exist.");
                }

                Process process = Process.GetCurrentProcess();
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                string contextId = $"Id-{process.Id},Name-{process.ProcessName},File-{fileName},T-{DateTime.Now}";

                context = Native.ddjvu_context_create(contextId);

                if (context == IntPtr.Zero)
                {
                    throw new ApplicationException("Failed to create a new context");
                }

                document = Native.ddjvu_document_create_by_filename(context, filePath, 1);

                if (document == IntPtr.Zero)
                {
                    throw new ApplicationException($"Failed to load the djvu document with path: ${filePath}");
                }

                JobStatus status = JobStatus.JOB_NOTSTARTED;

                while (true)
                {
                    status = Native.ddjvu_document_decoding_status(document);

                    if (IsDecodingDone(status))
                    {
                        break;
                    }
                    else
                    {
                        Utils.ProcessMessages(context, true);
                    }
                }

                if (status == JobStatus.JOB_OK)
                {
                    return new DjvuDocument(context, document, filePath);
                }
                else if (status == JobStatus.JOB_FAILED)
                {
                    throw new ApplicationException($"Failed to decode the djvu document: {filePath}");
                }
                else if (status == JobStatus.JOB_STOPPED)
                {
                    throw new ApplicationException($"Decoding interrupted by the user. Filepath: {filePath}");
                }

                // If we reached here then some unexpected error has occured
                throw new ApplicationException($"An unexpected error occured while parsing the document: {filePath}");
            }
            catch (Exception ex)
            {
                if (document != IntPtr.Zero)
                {
                    Native.ddjvu_document_release(document);
                }

                if (context != IntPtr.Zero)
                {
                    Native.ddjvu_context_release(context);
                }

                throw ex;
            }
        }


        /// <summary>
        /// Access the job object in charge of decoding the document header.
        /// </summary>
        /*public DjvuJob DjvuJob
        {
            get 
            {
                IntPtr job = Native.ddjvu_document_job(_document);

                if (job == IntPtr.Zero)
                    return null;

                return new DjvuJob(job);
            }
        }*/

        /// <summary>
        /// Returns the type of a DjVu document.
        /// <para>
        /// This function might return <see cref="DocumentType.UNKNOWN" />
        /// when called before receiving a m_docinfo message.
        /// </para>
        /// </summary>
        /// <returns>The type of djvu document in form of an enum member.</returns>
        public DocumentType Type { get { return Native.ddjvu_document_get_type(_document); } }


        /// <summary>
        /// Returns the number of pages in a DjVu document.
        /// <para>
        /// This function might return 1 when called 
        /// before receiving a m_docinfo message.
        /// </para>
        /// </summary>
        /// <returns>An int representing number of pages.</returns>
        public int PageNumber { get { return Native.ddjvu_document_get_pagenum(_document); } }

        /// <summary>
        /// Returns the number of component files. 
        /// This function might return 0 when called
        /// before receiving a m_docinfo message
        /// </summary>
        /// <returns>The number of component files</returns>
        public int FileNumber { get { return Native.ddjvu_document_get_filenum(_document); } }

        /// <summary>
        /// <p>When we construct a document, djvulibre starts decoding it in background.<p>
        /// <p>Also djvulibre decodes a document in chunks. Hence, we don't have to wait till
        /// all of the document is decoded.
        /// </p>
        /// <p>We use this function to know if the decoding of document is complete.</p>
        /// </summary>
        /// <returns>A boolean. true if decoding of the document is finished. false otherwise.</returns>
        private static bool IsDecodingDone(JobStatus status)
        {
            return status >= JobStatus.JOB_OK;
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
            int flag = json ? 1 : 0;
            return Native.ddjvu_document_get_dump(_document, flag);
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
            IntPtr djvuPagePtr = Native.ddjvu_page_create_by_pageno(_document, pageNum);

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

                if (_document != IntPtr.Zero)
                {
                    Native.ddjvu_document_release(_document);
                    _document = IntPtr.Zero;
                }

                if (_context != IntPtr.Zero)
                {
                    Native.ddjvu_context_release(_context);
                    _context = IntPtr.Zero;
                }

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