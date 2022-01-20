using System;
using System.Collections.Generic;
using System.Text;

namespace DjvuSharp.Enums
{
    /// <summary>
    /// Enumerated type for pixel formats
    /// </summary>
    public enum DjvuFormatStyle
    {
        /// <summary>
        /// truecolor 24 bits in BGR order
        /// </summary>
        DDJVU_FORMAT_BGR24,

        /// <summary>
        /// truecolor 24 bits in RGB order
        /// </summary>
        DDJVU_FORMAT_RGB24,

        /// <summary>
        /// truecolor 16 bits with masks
        /// </summary>
        DDJVU_FORMAT_RGBMASK16,

        /// <summary>
        /// truecolor 32 bits with masks
        /// </summary>
        DDJVU_FORMAT_RGBMASK32,

        /// <summary>
        /// greylevel 8 bits
        /// </summary>
        DDJVU_FORMAT_GREY8,

        /// <summary>
        /// paletized 8 bits (6x6x6 color cube)
        /// </summary>
        DDJVU_FORMAT_PALETTE8,

        /// <summary>
        /// packed bits, msb on the left
        /// </summary>
        DDJVU_FORMAT_MSBTOLSB,

        /// <summary>
        /// packed bits, lsb on the left
        /// </summary>
        DDJVU_FORMAT_LSBTOMSB
    }
}
