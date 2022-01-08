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
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


[assembly:InternalsVisibleTo("DjvuSharp.Tests")]
namespace DjvuSharp
{
    internal static class Native
    {
        private const string dllname = "libdjvulibre";

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
    }
}
