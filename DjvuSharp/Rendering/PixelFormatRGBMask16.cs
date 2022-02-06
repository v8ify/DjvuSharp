using System;
using System.Collections.Generic;
using System.Text;
using DjvuSharp.Enums;

namespace DjvuSharp.Rendering
{
    public class PixelFormatRGBMask16: PixelFormat
    {
        public PixelFormatRGBMask16(int redMask, int greenMask, int blueMask, int XORValue = 0) : base()
        {
            _bpp = 16;

            if (redMask < 0 || greenMask < 0 || blueMask < 0 || XORValue < 0)
            {
                throw new ArgumentException("Arguments must not be negative");
            }

            redMask &= 0xFFFF;
            greenMask &= 0xFFFF;
            blueMask &= 0xFFFF;
            XORValue &= 0xFFFF;

            int[] args = new int[4] { redMask, greenMask, blueMask, XORValue };

            _djvu_format = Native.ddjvu_format_create(PixelFormatStyle.RGBMASK16, 4, args);

            if (_djvu_format == IntPtr.Zero)
            {
                throw new ApplicationException($"Failed to create {nameof(PixelFormatRGBMask16)}");
            }
        }
    }
}