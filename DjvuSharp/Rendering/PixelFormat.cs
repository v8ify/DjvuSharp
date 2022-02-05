using System;
using System.Collections.Generic;
using System.Text;

namespace DjvuSharp.Rendering
{
    public class PixelFormat
    {
        protected IntPtr _djvu_format;
        protected int _bpp;

        protected PixelFormat()
        {

        }

        public void SetGamma(double gamma = 2.2)
        {
            Native.ddjvu_format_set_gamma(_djvu_format, gamma);
        }

        public void SetRowOrder(bool topToBottom = false)
        {
            Native.ddjvu_format_set_row_order(_djvu_format, topToBottom);
        }

        public void SetYDirection(bool topToBottom = false)
        {
            Native.ddjvu_format_set_y_direction(_djvu_format, topToBottom);
        }

        public void SetDitherBits(int bits)
        {
            Native.ddjvu_format_set_ditherbits(_djvu_format, bits);
        }

        public void SetWhite(byte blue, byte green, byte red)
        {
            Native.ddjvu_format_set_white(_djvu_format, blue, green, red);
        }
    }
}
