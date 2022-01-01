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
using Djvulibre.Internal;
using DjvuSharp.Message;

namespace DjvuSharp
{
    /// <summary>
    /// There is usually only one DjvuContext object.  
    /// This object holds global data structures such as the 
    /// cache of decoded pages, or the list of pending 
    /// event messages.
    /// </summary>
    public class DjvuContext: IDisposable
    {
        private SWIGTYPE_p_ddjvu_context_s _djvu_context_s;

        private bool _isDisposed;

        public DjvuContext(string programName)
        {
            // Create a djvu context
            _djvu_context_s = djvulibre.ddjvu_context_create(programName);
        }

        /// <summary>
        /// <para>Gets and sets the maximum size of the cache of decoded page data.
        /// The value of argument is expressed in bytes, meaning if you set the value
        /// of this property 50 then the cache size is 50 bytes and so on.
        /// </para>
        /// 
        /// <para> The default value for this property is 10485760
        /// and you cannot assign value less than 1.
        /// </para>
        /// </summary>
        /// <exception cref="ArgumentException">Throws exception if set to value less than 1.</exception>
        public uint CacheSize 
        { 
            get 
            { 
                return djvulibre.ddjvu_cache_get_size(_djvu_context_s);
            } 
            set 
            {
                if (value <= 0)
                    throw new ArgumentException($"The value of {nameof(CacheSize)} should be greater than 0");

                djvulibre.ddjvu_cache_set_size(_djvu_context_s, value);
            }
        }

        /// <summary>Clears all cached data in a context.</summary>
        public void ClearCache()
        {
            djvulibre.ddjvu_cache_clear(_djvu_context_s);
        }

        /// <summary>Creates and returns a Djvu document from given file</summary>
        /// <param name="filename">The djvu file you want to process</param>
        /// <param name="shouldCache">Setting this argument to true indicates that decoded pages
        /// should be cached when possible.</param>
        public DjvuDocument CreateDjvuDocument(string filename, bool shouldCache)
        {
            return new DjvuDocument(_djvu_context_s, filename, shouldCache);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DDjvuMessage PeekMessage()
        {
            ddjvu_message_s message = djvulibre.ddjvu_message_peek(_djvu_context_s);

            if (message == null)
                return null;

            return new DDjvuMessage(djvulibre.ddjvu_message_peek(_djvu_context_s));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DDjvuMessage WaitMessage()
        {
            ddjvu_message_s message = djvulibre.ddjvu_message_wait(_djvu_context_s);

            if (message == null)
                return null;

            return new DDjvuMessage(djvulibre.ddjvu_message_wait(_djvu_context_s));
        }

        /// <summary>
        /// 
        /// </summary>
        public void PopMessage()
        {
            djvulibre.ddjvu_message_pop(_djvu_context_s);
        }

        ~DjvuContext()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                // Release the context
                djvulibre.ddjvu_context_release(_djvu_context_s);

                _isDisposed = true;
            }
        }
    }
}