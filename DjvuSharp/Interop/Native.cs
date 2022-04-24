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
using DjvuSharp.Enums;
using DjvuSharp.Marshaler;
using DjvuSharp.Rendering;

[assembly: InternalsVisibleTo("DjvuSharp.Tests")]
namespace DjvuSharp.Interop
{

    /// <summary>
    /// This structure is a member of the union djvu_message.
    /// It represents the information common to all kinds of
    /// messages.
    /// If the message has not yet been passed to the user 
    /// with ddjvu_message_{peek,wait}, it is silently
    /// removed from the message queue.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_any_s
    {
        /// <summary>
        /// The kind of message corresponding to enum <see cref="Message.MessageTag"/>
        /// </summary>
        public int tag;

        /* context, document, page, job fields may be IntPtr.Zero when not relevant.
         * These fields are also cleared when the corresponding object is 
         * released with ddjvu_{job,page,document}_release methods.
         */
        public IntPtr context;
        public IntPtr document;
        public IntPtr miniexpage;
        public IntPtr job;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_error_s
    {
        public ddjvu_message_any_s any;
        public IntPtr message;
        public IntPtr function;
        public IntPtr filename;
        public int lineno;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_info_s
    {
        public ddjvu_message_any_s any;
        public IntPtr message;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_newstream_s
    {
        public ddjvu_message_any_s any;
        public int streamid;
        public IntPtr name;
        public IntPtr url;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_docinfo_s
    {
        public ddjvu_message_any_s any;
    }

    /// <summary>
    /// The page decoding process generates this message
    /// - when basic page information is available and 
    ///   before any m_relayout or m_redisplay message,
    /// - when the page decoding thread terminates.
    /// You can distinguish both cases using 
    /// function <see cref="Native.ddjvu_page_decoding_status(IntPtr)" />
    /// Messages m_pageinfo are also generated as a consequence of 
    /// functions such as ddjvu_document_get_pageinfo. 
    /// The field m_any.page of such message is null.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_pageinfo_s
    {
        public ddjvu_message_any_s any;
    }

    /// <summary>
    /// This message is generated when a DjVu viewer
    /// should recompute the layout of the page viewer
    /// because the page size and resolution information has
    /// been updated.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_relayout_s
    {
        public ddjvu_message_any_s any;
    }

    /// <summary>
    /// This message is generated when a DjVu viewer
    /// should call <see cref="ddjvu_page_render" /> and redisplay
    /// the page. This happens, for instance, when newly 
    /// decoded DjVu data provides a better image.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_redisplay_s
    {
        public ddjvu_message_any_s any;
    }

    /// <summary>
    /// This message indicates that an additional chunk
    /// of DjVu data has been decoded.  Member chunkid
    /// indicates the type of the DjVu chunk.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_chunk_s
    {
        public ddjvu_message_any_s any;
        public IntPtr chunkid;
    }

    /// <summary>
    /// This structure specifies the location of a rectangle.
    /// Coordinates are usually expressed in pixels relative to 
    /// the BOTTOM LEFT CORNER (but see ddjvu_format_set_y_direction).
    /// Members x and y indicate the position of the bottom left 
    /// corner of the rectangle. Members w and h indicate the 
    /// width and height of the rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct DjvuRect
    {
        /// <summary>
        /// Members x and y indicate the position of the bottom left 
        /// corner of the rectangle
        /// </summary>
        public int x;

        /// <summary>
        /// Members x and y indicate the position of the bottom left 
        /// corner of the rectangle
        /// </summary>
        public int y;

        /// <summary>
        /// Members w indicates the 
        /// width and height of the rectangle.
        /// </summary>
        public uint w;

        /// <summary>
        /// Members h indicates the 
        /// width and height of the rectangle.
        /// </summary> 
        public uint h;
    };


    /// <summary>
    /// This message is sent when additional thumbnails are available
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_thumbnail_s
    {
        public ddjvu_message_any_s any;
        public int pagenum;
    }

    /// <summary>
    /// These messages are generated to indicate progress 
    /// towards the completion of a print or save job.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ddjvu_message_progress_s
    {
        public ddjvu_message_any_s any;
        public int status;
        public int percent;
    };


    /* -------------------------------------------------- */
    /* DJVU_MESSAGE_T                                     */
    /* -------------------------------------------------- */

    /// <summary>
    /// Represents the ddjvu_message_s union in the unmanaged code
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ddjvu_message_s
    {
        [FieldOffset(0)]
        public ddjvu_message_any_s m_any;

        [FieldOffset(0)]
        public ddjvu_message_error_s m_error;

        [FieldOffset(0)]
        public ddjvu_message_info_s m_info;

        [FieldOffset(0)]
        public ddjvu_message_newstream_s m_newstream;

        [FieldOffset(0)]
        public ddjvu_message_docinfo_s m_docinfo;

        [FieldOffset(0)]
        public ddjvu_message_pageinfo_s m_pageinfo;

        [FieldOffset(0)]
        public ddjvu_message_chunk_s m_chunk;

        [FieldOffset(0)]
        public ddjvu_message_relayout_s m_relayout;

        [FieldOffset(0)]
        public ddjvu_message_redisplay_s m_redisplay;

        [FieldOffset(0)]
        public ddjvu_message_thumbnail_s m_thumbnail;

        [FieldOffset(0)]
        public ddjvu_message_progress_s m_progress;
    }


    internal static class Native
    {
        private const string dllname = "djvulibre-21";

        // #if X86
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_alloc(uint size);
        /*// 
                [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
                internal extern static IntPtr djvu_alloc(ulong size);
        #endif*/

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void ddjvu_free(IntPtr miniexpointer);


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

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ddjvu_job_release")]
        internal extern static void ddjvu_document_release(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ddjvu_job_status")]
        internal extern static JobStatus ddjvu_document_decoding_status(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static DocumentType ddjvu_document_get_type(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static int ddjvu_document_get_pagenum(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static int ddjvu_document_get_filenum(IntPtr document);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CustomStringMarshaler))]
        internal extern static string ddjvu_document_get_dump(IntPtr document, int json);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr ddjvu_document_get_pagedump(IntPtr document, int pageno);

        internal static string ddjvu_document_get_page_dump(IntPtr document, int pageno)
        {

            IntPtr docDump = ddjvu_document_get_pagedump(document, pageno);

            string result = Marshal.PtrToStringUni(docDump);

            // must free since IntPtr miniexpoints to dynamically allocated char array
            ddjvu_free(docDump);

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

            // must free since IntPtr miniexpoints to dynamically allocated
            // char array
            ddjvu_free(docDump);

            return result;
        }


        ////////////////////////////////////////////
        /// Djvu Page related methods
        ////////////////////////////////////////////

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr ddjvu_page_create_by_pageno(IntPtr document, int pageno);

        /// <summary>
        /// Release a reference to a ddjvu_page_t object.
        /// The calling program should no longer reference this object.
        /// The object itself will be destroyed as soon as no other object
        /// or thread needs it.
        /// </summary>
        /// <param name="page"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ddjvu_job_release")]
        internal extern static void ddjvu_page_release(IntPtr miniexpage);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ddjvu_job_status")]
        internal extern static JobStatus ddjvu_page_decoding_status(IntPtr document);

        /// <summary>
        /// Gets the job which corresponds to a page
        /// </summary>
        /// <param name="page">A pointer to the page whose job we want</param>
        /// <returns>A pointer to djvu_job. Could be null so check with IntPtr.Zero</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_page_job(IntPtr miniexpage);


        /// <summary>
        /// Gets the width of a page in pixel.
        /// Calling this function before receiving a m_pageinfo message always yields 0
        /// </summary>
        /// <param name="page">A pointer to page.</param>
        /// <returns>Page width in pixels.</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ddjvu_page_get_width(IntPtr miniexpage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ddjvu_page_get_height(IntPtr miniexpage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ddjvu_page_get_resolution(IntPtr miniexpage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ddjvu_page_get_gamma(IntPtr miniexpage);

        /// <summary>
        /// Returns the version of the djvu file format.
        /// Calling this function before receiving a m_pageinfo
        /// message yields a meaningless but plausible value
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ddjvu_page_get_version(IntPtr miniexpage);

        /// <summary>
        /// Returns the version of the djvu file format
        /// implemented by this library.More or less graceful
        /// degradation might arise if this is smaller than
        /// the number returned by ddjvu_page_get_version.
        /// </summary>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ddjvu_code_get_version();

        /// <summary>
        /// Returns the type of the page data.
        /// Calling this function before the termination of the
        /// decoding process might returns <see cref="PageType.UNKNOWN"/>.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern PageType ddjvu_page_get_type(IntPtr miniexpage);

        /// <summary>
        /// Changes the counter-clockwise rotation angle for a DjVu page.
        /// Calling this function before receiving a m_pageinfo
        /// message has no good effect.
        /// </summary>
        /// <param name="page">An IntPtr to ddjvu_page_t</param>
        /// <param name="rotation">
        /// One of the values from enum DjvuPageRotation.
        /// Remember to cast it to int before passing as an argument.
        /// </param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_page_set_rotation(IntPtr miniexpage, PageRotation rotation);

        /// <summary>
        /// Returns the counter-clockwise rotation angle for the DjVu page.
        /// The rotation is automatically taken into account
        /// by ddjvu_page_render, <see cref="ddjvu_page_get_width(IntPtr)"/>
        /// and <see cref="ddjvu_page_get_height(IntPtr)"/>
        /// </summary>
        /// <param name="page">An IntPtr to ddjvu_page_t</param>
        /// <returns>An integer which should be cast to the enum <see cref="PageRotation"/></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern PageRotation ddjvu_page_get_rotation(IntPtr miniexpage);

        /// <summary>
        /// Returns the page rotation specified by the 
        /// orientation flags in the DjVu file.
        /// [brain damage warning] This is useful because
        /// maparea coordinates in the annotation chunks
        /// are expressed relative to the rotated coordinates
        /// whereas text coordinates in the hidden text data
        /// are expressed relative to the unrotated coordinates.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern PageRotation ddjvu_page_get_initial_rotation(IntPtr miniexpage);


        /* -------------------------------------------------- */
        /* COORDINATE TRANSFORMS                              */
        /* -------------------------------------------------- */


        /// <summary>
        /// Creates a djvu_rectmapper_t data structure 
        /// representing an affine coordinate transformation that
        /// maps points from rectangle input to rectangle output.
        /// The transformation maintains the positions relative
        /// to the coordinates of the rectangle corners.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_rectmapper_create(Rectangle input, Rectangle output);


        /// <summary>
        /// Modifies the coordinate transform <paramref name="mapper"/> by redefining
        /// which corners of the output rectangle match those of the
        /// input rectangle. This function first applies a counter-clockwise
        /// rotation of <paramref name="rotation"/> quarter-turns, and then reverses the X
        /// (resp.Y) coordinates when <paramref name="mirrorx"/> (resp. <paramref name="mirrory"/>) is non zero.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="rotation"></param>
        /// <param name="mirrorx"></param>
        /// <param name="mirrory"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_rectmapper_modify(IntPtr mapper, PageRotation rotation, int mirrorx, int mirrory);


        /// <summary>
        /// Destroys the ddjvu_rectmapper_t structure
        /// returned by <see cref="ddjvu_rectmapper_release(IntPtr)"/>.
        /// </summary>
        /// <param name="mapper">Pointer to ddjvu_rectmapper_t that you want to release.</param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_rectmapper_release(IntPtr mapper);

        /// <summary>
        /// Applies the coordinate transform to a point
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_map_point(IntPtr mapper, ref int x, ref int y);

        /// <summary>
        /// Applies the coordinate transform to a rectangle
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="rect"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void ddjvu_map_rect(IntPtr mapper, Rectangle rect);

        /// <summary>
        /// Applies the inverse coordinate transform to a point
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_unmap_point(IntPtr mapper, ref int x, ref int y);

        /// <summary>
        /// Applies the inverse coordinate transform to a rectangle
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_unmap_rect(IntPtr mapper, Rectangle rect);


        /* -------------------------------------------------- */
        /* DJVU_FORMAT_T                                      */
        /* -------------------------------------------------- */

        /// <summary>
        /// Creates a ddjvu_format_t object describing a pixel format.
        /// </summary>
        /// <param name="style">Describes the generic pixel format.</param>
        /// <param name="nargs">
        /// Argument <paramref name="args"/> is an array of <paramref name="nargs"/> unsigned ints
        /// providing additionnal information:
        /// <list type="bullet">
        /// <item>
        /// When style is RGBMASK*, argument <paramref name="nargs"/> must be 3 or 4.
        /// The three first entries of array <paramref name="args"/> are three contiguous
        /// bit masks for the red, green, and blue components of each pixel.
        /// The resulting color is then xored with the optional fourth entry. 
        /// </item>
        /// <item>
        /// When style is PALLETE*, argument <paramref name="nargs"/> must be 216
        /// and array <paramref name="args"/> contains the 6*6*6 entries of a web
        /// color cube.
        /// </item>
        /// <item>
        /// Otherwise <paramref name="nargs"/> must be 0.
        /// </item>
        /// </list>
        /// </param>
        /// <param name="args">
        /// Argument <paramref name="args"/> is an array of <paramref name="nargs"/> unsigned ints
        /// providing additionnal information:
        /// <list type="bullet">
        /// <item>
        /// When style is RGBMASK*, argument <paramref name="nargs"/> must be 3 or 4.
        /// The three first entries of array <paramref name="args"/> are three contiguous
        /// bit masks for the red, green, and blue components of each pixel.
        /// The resulting color is then xored with the optional fourth entry. 
        /// </item>
        /// <item>
        /// When style is PALLETE*, argument <paramref name="nargs"/> must be 216
        /// and array <paramref name="args"/> contains the 6*6*6 entries of a web
        /// color cube.
        /// </item>
        /// <item>
        /// Otherwise <paramref name="nargs"/> must be 0.
        /// </item>
        /// </list>
        /// </param>
        /// <returns>A pointer to ddjvu_format_t</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_format_create(PixelFormatStyle style, int numberOfArgs, IntPtr args);


        /// <summary>
        /// Sets a flag indicating whether the rows in the pixel buffer
        /// are stored starting from the top or the bottom of the image.
        /// Default ordering starts from the bottom of the image.
        /// This is the opposite of the X11 convention
        /// </summary>
        /// <param name="format"></param>
        /// <param name="top_to_bottom"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        private static extern void ddjvu_format_set_row_order(IntPtr format, int top_to_bottom);

        /// <summary>
        /// Sets a flag indicating whether the rows in the pixel buffer
        /// are stored starting from the top or the bottom of the image.
        /// Default ordering starts from the bottom of the image.
        /// This is the opposite of the X11 convention
        /// </summary>
        /// <param name="format">A pointer to djvu_format_t.
        /// Normally gotten from <see cref="ddjvu_format_create(int, int, uint[])"/>
        /// </param>
        /// <param name="top_to_bottom">If true, rows pixel buffers are stored starting from top to bottom.</param>
        internal static void ddjvu_format_set_row_order(IntPtr format, bool topToBottom)
        {
            int top_to_bottom = topToBottom ? 1 : 0;

            ddjvu_format_set_row_order(format, top_to_bottom);
        }

        /// <summary>
        /// Sets a flag indicating whether the y coordinates in the drawing 
        /// area are oriented from bottom to top, or from top to botttom.
        /// The default is bottom to top, similar to PostScript.
        /// This is the opposite of the X11 convention.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="top_to_bottom"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        private static extern void ddjvu_format_set_y_direction(IntPtr format, int top_to_bottom);

        /// <summary>
        /// Sets a flag indicating whether the y coordinates in the drawing 
        /// area are oriented from bottom to top, or from top to botttom.
        /// The default is bottom to top, similar to PostScript.
        /// This is the opposite of the X11 convention.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="topToBottom"></param>
        internal static void ddjvu_format_set_y_direction(IntPtr format, bool topToBottom)
        {
            int top_to_bottom = topToBottom ? 1 : 0;

            ddjvu_format_set_y_direction(format, top_to_bottom);
        }

        /// <summary>
        /// Specifies the final depth of the image on the screen.
        /// This is used to decide which dithering algorithm should be used.
        /// The default is usually appropriate.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="bits"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_format_set_ditherbits(IntPtr format, int bits);

        /// <summary>
        /// Sets the gamma of the display for which the pixels are
        /// intended.  This will be combined with the gamma stored in
        /// DjVu documents in order to compute a suitable color
        /// correction.  The default value is 2.2.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="gamma"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_format_set_gamma(IntPtr format, double gamma);

        /// <summary>
        /// Sets the whitepoint of the display for which the pixels are
        /// intended.  This will be combined with the gamma stored in
        /// DjVu documents in order to compute a suitable color
        /// correction.  The default value is 0xff,0xff,0xff.
        /// </summary>
        /// <param name="format">A pointer to djvu_format_t</param>
        /// <param name="b"></param>
        /// <param name="g"></param>
        /// <param name="r"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_format_set_white(IntPtr format, byte b, byte g, byte r);

        /// <summary>
        /// Release a reference to a ddjvu_format_t object.
        /// The calling program should no longer reference this object.
        /// </summary>
        /// <param name="format"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_format_release(IntPtr format);


        /* ------- RENDER ------- */

        /// <summary>
        /// <para>Renders a segment of a page with arbitrary scale</para>
        /// <para>
        /// Conceptually this function renders the full page
        /// into a rectangle <paramref name="pagerect"/> and copies the
        /// pixels specified by rectangle <paramref name="renderrect"/>
        /// into the buffer starting at position <paramref name="imagebuffer"/>.
        /// The actual code is much more efficient than that.
        /// </para>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="mode"></param>
        /// <param name="pagerect"></param>
        /// <param name="renderrect"></param>
        /// <param name="pixelformat">specifies the expected pixel format.</param>
        /// <param name="rowsize">
        /// specifies the number of BYTES from 
        /// one row to the next in the buffer.The buffer must be
        /// large enough to accomodate the desired image.
        /// </param>
        /// <param name="imagebuffer">The final image is written into buffer</param>
        /// <returns>
        /// This function makes a best effort to compute an image
        /// that reflects the most recently decoded data.It might
        /// return 0 to indicate that no image could be
        /// computed at this point, and that nothing was written into
        /// the buffer.
        /// </returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ddjvu_page_render(
            IntPtr page,
            RenderMode mode,
            Rectangle pagerect,
            Rectangle renderrect,
            IntPtr pixelformat,
            uint rowsize,
            IntPtr imagebuffer);


        /* -------------------------------------------------- */
        /* THUMBNAILS                                         */
        /* -------------------------------------------------- */

        /// <summary>
        /// Determine whether a thumbnail is available for page pagenum.
        /// Calling this function with non zero argument start initiates
        /// a thumbnail calculation job. Regardless of its success,
        /// the completion of the job is signalled by a subsequent 
        /// <see cref="Messages.ThumbnailMessage"/> message.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="pageNo"></param>
        /// <param name="start">Calling this function with non zero argument start initiates
        /// a thumbnail calculation job.
        /// </param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern JobStatus ddjvu_thumbnail_status(IntPtr document, int pageNo, int start);


        /// <summary>
        /// Renders a thumbnail for page pagenum.
        /// Argument imagebuffer must be large enough to contain
        /// an image of size wptr by hptr using pixel format
        /// pixelformat.Argument rowsize specifies the number
        /// of BYTES from one row to the next in the buffer.
        /// This function returns FALSE when no thumbnail is available.
        /// Otherwise it returns TRUE, adjusts *wptr and *hptr to
        /// reflect the thumbnail size, and, if the pointer imagebuffer
        /// is non zero, writes the pixel data into the image buffer. 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="pageNo"></param>
        /// <param name="wptr"></param>
        /// <param name="hptr"></param>
        /// <param name="pixelFormat"></param>
        /// <param name="rowSize"></param>
        /// <param name="imageBuffer"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ddjvu_thumbnail_render(
            IntPtr document,
            int pageNo,
            ref int wptr,
            ref int hptr,
            IntPtr miniexpixelFormat,
            uint rowSize,
            IntPtr imageBuffer);



        /* -------------------------------------------------- */
        /* S-EXPRESSIONS                                      */
        /* -------------------------------------------------- */

        /// <summary>
        /// This function controls the allocation of the
        /// s-expressions returned by functions from the DDJVU
        /// API.It indicates that the s-expression<expr> is no
        /// longer needed and can be deallocated as soon as
        /// necessary.Otherwise the s-expression remains allocated
        /// as long as the document object exists.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="expr"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddjvu_miniexp_release(IntPtr document, IntPtr expr);

        /// <summary>
        /// This function tries to obtain the document outline.
        /// If this information is available, it returns a
        /// s-expression with the same syntax as function
        /// print-outline of program djvused.
        /// Otherwise it returns miniexp_dummy until
        /// the document header gets fully decoded.
        /// Typical synchronous usage:
        /// 
        ///  miniexp_t r;
        ///  while ((r=ddjvu_document_get_outline(doc))==miniexp_dummy)
        ///    handle_ddjvu_messages(ctx, TRUE);
        /// 
        ///      This function returns the empty list miniexp_nil when
        ///      the document contains no outline information.It can also
        ///      return symbols failed or stopped when an error occurs
        /// while accessing the desired information.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_document_get_outline(IntPtr document);

        /// <summary>
        /// This function returns the document-wide annotations.
        /// This corresponds to a proposed change in the djvu format.
        /// When no new-style document-wide annotations are available
        /// and compat is true, this function searches a shared
        /// annotation chunk and returns its contents.
        /// 
        /// This function returns miniexp_dummy if the information
        /// is not yet available.It may then cause the emission
        /// ofm_pageinfo messages with null m_any.page.
        /// 
        /// This function returns the empty list miniexp_nil when
        /// the document does not contain page annotations. It can also
        /// return symbols failed or stopped when an error occurs
        /// while accessing the desired information.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="compact"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_document_get_anno(IntPtr document, int compact);

        /// <summary>
        /// This function tries to obtain the text information for
        /// page  pageno.If this information is available, it
        /// returns a s-expression with the same syntax as function
        ///  print-txt of program djvused.Otherwise it starts
        /// fetching the page data and returns miniexp_dummy.
        /// This function causes the emission of m_pageinfo messages
        /// with zero in the m_any.page field.
        /// 
        /// Typical synchronous usage:
        ///  miniexp_t r;
        /// while ((r=ddjvu_document_get_pagetext(doc, pageno,0))==miniexp_dummy)
        ///     handle_ddjvu_messages(ctx, TRUE);
        ///
        /// This function returns the empty list miniexp_nil when
        /// the page contains no text information.It can also return
        /// 
        /// symbols failed or stopped when an error occurs while
        /// 
        /// accessing the desired information. 
        /// 
        /// 
        /// Argument maxdetail> controls the level of detail in the
        /// returned s-expression.Values "page", "column", "region", "para",
        /// "line", and "word" restrict the output to the specified granularity.
        /// All other values produce a s-expression that represents
        /// the hidden text data as finely as possible.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_document_get_pagetext(IntPtr document);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CustomStringMarshaler))]
        internal static extern string ddjvu_document_get_pagetext_utf8(
            IntPtr document,
            int pageNo,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CustomStringMarshaler))] string maxDetails);

        /// <summary>
        /// This function tries to obtain the annotations for
        /// page pageno.If this information is available, it
        /// returns a s-expression with the same syntax as function
        /// print-ant of program djvused.Otherwise it starts
        /// fetching the page data and returns miniexp_dummy.
        /// This function causes the emission of m_pageinfo messages
        /// with zero in the m_any.page field.
        /// Typical synchronous usage:
        /// 
        ///   miniexp_t r;
        ///   while ((r = ddjvu_document_get_pageanno(doc, pageno))==miniexp_dummy)
        ///     handle_ddjvu_messages(ctx, TRUE);
        /// 
        /// This function returns the empty list miniexp_nil when
        /// the page contains no annotations. It can also return
        /// symbols failed or stopped when an error occurs while
        /// accessing the desired information.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="pageno"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_document_get_pageanno(IntPtr document, int pageno);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_anno_get_bgcolor(IntPtr annotations);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_anno_get_zoom(IntPtr annotations);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_anno_get_mode(IntPtr annotations);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_anno_get_horizalign(IntPtr annotations);

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ddjvu_anno_get_vertalign(IntPtr annotations);

        /// <summary>
        /// Parse the annotations and returns a zero terminated
        /// array of key symbols for the page metadata.
        /// The caller should free this array with function<free>.
        /// See also<(metadata...)> in the djvused man page.
        /// </summary>
        /// <param name="annotations"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void** ddjvu_anno_get_metadata_keys(IntPtr annotations);

        /// <summary>
        /// Parse the annotations and returns the metadata string
        /// corresponding to the metadata key symbol<key>.
        /// The string remains allocated as long as the
        /// annotations s-expression remain allocated.
        /// Returns zero if no such key is present.
        /// </summary>
        /// <param name="annotations"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe IntPtr ddjvu_anno_get_metadata(void** annotations);

        /// <summary>
        /// Returns the length of a list.
        /// </summary>
        /// <param name="miniexp">lisp expression in question</param>
        /// <returns>Returns 0 for non lists, -1 for circular lists.</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int miniexp_length(IntPtr miniexp);


        /* Represent common combinations of car and cdr. */

        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_caar(IntPtr miniexp);
        
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_cadr(IntPtr miniexp);
        
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_cdar(IntPtr miniexp);
        
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_cddr(IntPtr miniexp);
        
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_caddr(IntPtr miniexp);
        
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_cdddr(IntPtr miniexp);

        /// <summary>
        /// Returns the n-th element of a list.
        /// </summary>
        /// <param name="n">The element which we want.</param>
        /// <param name="miniexp_list">The miniexp (list) on which we want to run this function</param>
        /// <returns>lisp s-expression at the position n</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_nth(int n, IntPtr miniexp_list);

        /// <summary>
        /// Constructs a pair
        /// </summary>
        /// <param name="car">The first part of list</param>
        /// <param name="cdr">The second part of the list</param>
        /// <returns>The s-expression pair created</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_cons(IntPtr car, IntPtr cdr);

        /// <summary>
        /// Replaces the car of the pair
        /// </summary>
        /// <param name="pair">The pair whose car we want to replace</param>
        /// <param name="newcar">The new value of car of type s-expression</param>
        /// <returns>The newly created s-expression with car replaced</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_rplaca(IntPtr pair, IntPtr newcar);

        /// <summary>
        /// Replaces the cdr of the pair
        /// </summary>
        /// <param name="pair">The pair whose cdr we want to replace</param>
        /// <param name="newcar">The new value of cdr of type s-expression</param>
        /// <returns>The newly created s-expression with cdr replaced</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_rplacd(IntPtr pair, IntPtr newcdr);

        /// <summary>
        /// Reverses a list in place.
        /// </summary>
        /// <param name="p">The targeted list (or pair)</param>
        /// <returns>The reversed list</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_reverse(IntPtr p);


        /* -------- S-EXPRESSION (STRINGS) -------- */

        /// <summary>
        /// Tests if an expression is a string.
        /// </summary>
        /// <param name="miniexp">The pointer to the s-expression.</param>
        /// <returns>0 if the expression is not a string.</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int miniexp_stringp(IntPtr miniexp);

        /// <summary>
        /// Returns the c string represented by the expression.
        /// Returns NULL if the expression is not a string.
        /// The c string remains valid as long as the corresponding lisp object exists.
        /// </summary>
        /// <param name="miniexp"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_to_str(IntPtr miniexp);

        /// <summary>
        /// Constructs a string expression by copying zero terminated string s.
        /// </summary>
        /// <param name="miniexp"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_string([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CustomStringMarshaler))] string miniexp);




        /* -------- S-EXPRESSION (FLOAT) -------- */

        /// <summary>
        /// Tests if an expression is a float.
        /// </summary>
        /// <param name="miniexp">The pointer to the s-expression.</param>
        /// <returns>0 if the expression is not a float.</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int miniexp_floatnump(IntPtr miniexp);

        /// <summary>
        /// Returns a new floating point number object.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_floatnum(double input);

        /// <summary>
        /// Returns a double precision number corresponding to a lisp expression.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double miniexp_to_double(IntPtr miniexp);

        /* -------- SYMBOLS -------- */

        /// <summary>
        /// Returns the symbol name as a string.
        /// </summary>
        /// <param name="miniexp"></param>
        /// <returns>Symbol name. Returns NULL if the expression is not a symbol.</returns>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_to_name(IntPtr miniexp);

        /// <summary>
        /// Returns the unique symbol expression with the specified name.
        /// </summary>
        /// <param name="name"></param>
        [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr miniexp_symbol([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CustomStringMarshaler))] string name);
    }
}
