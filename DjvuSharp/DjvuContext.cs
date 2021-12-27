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

namespace DjvuSharp
{
    public class DjvuContext: IDisposable
    {
        private SWIGTYPE_p_ddjvu_context_s _djvu_context_s;

        private bool _isDisposed;

        public DjvuContext(string programName)
        {
            // Create a djvu context
            _djvu_context_s = djvulibre.ddjvu_context_create(programName);
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