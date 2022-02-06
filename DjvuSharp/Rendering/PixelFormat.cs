using System;
using System.Collections.Generic;
using System.Text;

namespace DjvuSharp.Rendering
{
    public class PixelFormat
    {
        protected IntPtr _djvu_format;
        protected int _bpp;

        private bool disposedValue;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                if (_djvu_format != IntPtr.Zero)
                {
                    Native.ddjvu_format_release(_djvu_format);
                    _djvu_format = IntPtr.Zero;
                }

                disposedValue = true;
            }
        }

        ~PixelFormat()
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
