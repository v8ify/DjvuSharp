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

namespace DjvuSharp.Enums
{
    /// <summary>
    /// Enumerated type for pixel formats. Useful when rendering a page.
    /// </summary>
    public enum PixelFormatStyle
    {
        /// <summary>
        /// truecolor 24 bits in BGR order
        /// </summary>
        BGR24,

        /// <summary>
        /// truecolor 24 bits in RGB order
        /// </summary>
        RGB24,

        /// <summary>
        /// truecolor 16 bits with masks
        /// </summary>
        RGBMASK16,

        /// <summary>
        /// truecolor 32 bits with masks
        /// </summary>
        RGBMASK32,

        /// <summary>
        /// greylevel 8 bits
        /// </summary>
        GREY8,

        /// <summary>
        /// paletized 8 bits (6x6x6 color cube)
        /// </summary>
        PALETTE8,

        /// <summary>
        /// packed bits, msb on the left
        /// </summary>
        MSBTOLSB,

        /// <summary>
        /// packed bits, lsb on the left
        /// </summary>
        LSBTOMSB
    }
}
