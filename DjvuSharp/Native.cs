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


using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


[assembly:InternalsVisibleTo("DjvuSharp.Tests")]
namespace DjvuSharp
{
    internal static class Native
    {
        private const string dllname = "libdjvulibre";

#if X86
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr djvu_alloc(uint size);
#else
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr djvu_alloc(ulong size);
#endif

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void djvu_free(IntPtr pointer);


        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_get_version_string();

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_context_create(string programname);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_context_release(IntPtr ddjvu_context);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_cache_set_size(IntPtr context, ulong cachesize);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]

#if X86
        internal extern static uint ddjvu_cache_get_size(IntPtr context);
#else
        internal extern static ulong ddjvu_cache_get_size(IntPtr context);
#endif

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_cache_clear(IntPtr context);


        /// <summary>
        /// Returns a pointer to the next DDJVU message.
        /// This function returns null pointer (IntPtr.Zero) if no message is available.
        /// It does not remove the message from the queue.
        /// </summary>
        /// <param name="context">Pointer to djvu_context_t</param>
        /// <returns>Pointer to ddjvu_message_t</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_message_peek(IntPtr context);


        /// <summary>
        /// This function waits until a message is available.
        /// It does not remove the message from the queue.
        /// </summary>
        /// <param name="context">Pointer to djvu_context_t</param>
        /// <returns>A pointer to the next DDJVU message.</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_message_wait(IntPtr context);


        /// <summary>
        /// Removes one message from the queue.
        /// This function must be called after processing the message.
        /// Pointers returned by previous calls to ddjvu_message_peek
        /// or ddjvu_message_wait are no longer valid after 
        /// calling ddjvu_message_pop.
        /// </summary>
        /// <param name="context">Pointer to djvu_context_t</param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_message_pop(IntPtr context);


        /// <summary>
        /// Returns the status of the specified job.
        /// </summary>
        /// <param name="job">Pointer to ddjvu_job_t</param>
        /// <returns>Returns the status of the specified job in form 
        /// of enum <see cref="DDjvuStatus" />
        /// </returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static int ddjvu_job_status(IntPtr job);


        /// <summary>
        /// Attempts to cancel the specified job.
        /// This is a best effort function. 
        /// There no guarantee that the job will actually stop.
        /// </summary>
        /// <param name="job">Pointer to ddjvu_job_t</param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_job_stop(IntPtr job);


        /// <summary>
        /// Releases a reference to a job object and clears its user 
        /// data field.  This does not cause the job to stop executing.
        /// The calling program should no longer reference this object.
        /// The object itself will be destroyed as soon as no 
        /// other object or thread needs it. 
        /// </summary>
        /// <param name="job">A pointer to ddjvu_job_t</param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_job_release(IntPtr job);


        /// <summary>
        /// Creates a decoder for a DjVu document and starts
        /// decoding. This function returns immediately. The
        /// decoding job then generates messages to request the raw
        /// data and to indicate the state of the decoding process.
        /// </summary>
        /// <param name="context">A pointer to ddjvu_context_t</param>
        /// <param name="url">
        /// Argument url specifies an optional URL for the document.  
        /// The URL follows the usual syntax (&lt;"protocol://machine/path"&gt;). 
        /// It should not end with a slash. It only serves two purposes:
        /// The URL is used as a key for the cache of decoded pages.
        ///  The URL is used to document m_newstream messages.
        /// </param>
        /// <param name="cache">Setting argument cache to 1 indicates that decoded pages
        /// should be cached when possible.  This only works when
        /// argument url is not the null pointer.
        /// </param>
        /// <returns>A pointer to ddjvu_document_t</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_document_create(IntPtr context, string url, int cache);


        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_document_create_by_filename(IntPtr context, string filename, int cache);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_document_create_by_filename_utf8(IntPtr context, string filename, int cache);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_document_job(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_document_release(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_document_decoding_status(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static int ddjvu_document_get_type(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static int ddjvu_document_get_pagenum(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static int ddjvu_document_get_filenum(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr ddjvu_document_get_dump(IntPtr document, int json);

        internal static string ddjvu_document_get_dump(IntPtr document, bool json)
        {
            // dotnet and c++ bool type are represented differently
            // hence we need to convert from bool to int
            // here true --> 1 and false --> 0
            int requireJson = json ? 1 : 0;

            IntPtr docDump = ddjvu_document_get_dump(document, requireJson);

            string result = Marshal.PtrToStringUni(docDump);

            // must free since IntPtr points to dynamically allocated
            // char array
            djvu_free(docDump);

            return result;
        }

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr ddjvu_document_get_pagedump(IntPtr document, int pageno);

        internal static string ddjvu_document_get_page_dump(IntPtr document, int pageno)
        {

            IntPtr docDump = ddjvu_document_get_pagedump(document, pageno);

            string result = Marshal.PtrToStringUni(docDump);

            // must free since IntPtr points to dynamically allocated char array
            djvu_free(docDump);

            return result;
        }

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr ddjvu_document_get_pagedump_json(IntPtr document, int pageno, int json);

        internal static string ddjvu_document_get_page_dump_json(IntPtr document, int pageno, bool json)
        {
            // dotnet and c++ bool type are represented differently
            // hence we need to convert from bool to int
            // here true --> 1 and false --> 0
            int requireJson = json ? 1 : 0;

            IntPtr docDump = ddjvu_document_get_pagedump_json(document, pageno, requireJson);

            string result = Marshal.PtrToStringUni(docDump);

            // must free since IntPtr points to dynamically allocated
            // char array
            djvu_free(docDump);

            return result;
        }

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_page_create_by_pageno(IntPtr document, int pageno);
    }
}
