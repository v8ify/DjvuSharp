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
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DjvuSharp.Utility
{
    /// <summary>
    /// A utility class which holds helper methods for converting a byte array of pure pixels
    /// to a bitmap image, which is more widely used than a ppm image.
    /// </summary>
    internal static class Bitmap
    {
        private static readonly int BYTES_PER_PIXEL = 3;
        private static readonly int FILE_HEADER_SIZE = 14;
        private static readonly int INFO_HEADER_SIZE = 40;

        /// <summary>
        /// Generates a bitmap image array using pixel array returned by djvulibre render functions.
        /// </summary>
        /// <param name="image">Pixel array returned by render functions from djvulibre</param>
        /// <param name="width">Width of bitmap image specified by user.</param>
        /// <param name="height">Height of bitmap image specified by user</param>
        /// <returns>Bitmap image array. Might be appropriate to cast this array to a stream for general purpose use.</returns>
        static byte[] GenerateBitmapImage(byte[] image, int width, int height)
        {
            int widthInBytes = width * BYTES_PER_PIXEL;

            byte[] padding = new byte[]{ 0, 0, 0 };
            int paddingSize = (4 - (widthInBytes) % 4) % 4;

            int stride = (widthInBytes) + paddingSize;

            byte[] fileHeader = CreateBitmapFileHeader(height, stride);

            List<byte> imageFile = new List<byte>();

            imageFile.AddRange(fileHeader);

            byte[] infoHeader = CreateBitmapInfoHeader(width, height);

            imageFile.AddRange(infoHeader);
            
            for (int i = 0; i < height; i++)
            {
                int start = i * widthInBytes;
                int count = BYTES_PER_PIXEL * width;

                imageFile.AddRange(image.Skip(start).Take(count));

                imageFile.AddRange(padding.Take(paddingSize));
            }

            return imageFile.ToArray();
        }

        private static byte[] CreateBitmapInfoHeader(int width, int height)
        {
            byte[] infoHeader = new byte[]{
                0,0,0,0, /// header size
                0,0,0,0, /// image width
                0,0,0,0, /// image height
                0,0,     /// number of color planes
                0,0,     /// bits per pixel
                0,0,0,0, /// compression
                0,0,0,0, /// image size
                0,0,0,0, /// horizontal resolution
                0,0,0,0, /// vertical resolution
                0,0,0,0, /// colors in color table
                0,0,0,0, /// important color count
            };

            infoHeader[0] = (byte)(INFO_HEADER_SIZE);
            infoHeader[4] = (byte)(width);
            infoHeader[5] = (byte)(width >> 8);
            infoHeader[6] = (byte)(width >> 16);
            infoHeader[7] = (byte)(width >> 24);
            infoHeader[8] = (byte)(height);
            infoHeader[9] = (byte)(height >> 8);
            infoHeader[10] = (byte)(height >> 16);
            infoHeader[11] = (byte)(height >> 24);
            infoHeader[12] = 1;
            infoHeader[14] = (byte)(BYTES_PER_PIXEL * 8);

            return infoHeader;
        }

        private static byte[] CreateBitmapFileHeader(int height, int stride)
        {
            int fileSize = FILE_HEADER_SIZE + INFO_HEADER_SIZE + (stride * height);

            byte[] fileHeader = new byte[]{
                0,0,     /// signature
                0,0,0,0, /// image file size in bytes
                0,0,0,0, /// reserved
                0,0,0,0, /// start of pixel array
            };

            fileHeader[0] = (byte)('B');
            fileHeader[1] = (byte)('M');
            fileHeader[2] = (byte)(fileSize);
            fileHeader[3] = (byte)(fileSize >> 8);
            fileHeader[4] = (byte)(fileSize >> 16);
            fileHeader[5] = (byte)(fileSize >> 24);
            fileHeader[10] = (byte)(FILE_HEADER_SIZE + INFO_HEADER_SIZE);

            return fileHeader;
        }
    }
}
