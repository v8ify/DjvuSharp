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
using DjvuSharp.Enums;
using DjvuSharp.Rendering;
using System.Linq;
using DjvuSharp.Interop;

namespace DjvuSharp
{
    public class Thumbnail
    {
        private RenderEngine _renderEngine;

        public Thumbnail(DjvuDocument document, int pageNo)
        {
            // We start decoding thumbail as well by passing 1 as last argument.
            JobStatus status = Native.ddjvu_thumbnail_status(document.Document, pageNo, 1);

            while (true)
            {
                status = Native.ddjvu_thumbnail_status(document.Document, pageNo, 0);

                if (Utils.IsDecodingDone(status))
                {
                    break;
                }
                else
                {
                    Utils.ProcessMessages(document.Context, true);
                }
            }

            if (status == JobStatus.JOB_FAILED)
            {
                throw new ApplicationException($"Failed to create page thumbail. Page no.: {pageNo}");
            }
            else if (status == JobStatus.JOB_STOPPED)
            {
                throw new ApplicationException($"Page thumbnail creation interrupted by the user.  Page no.: {pageNo}");
            }

            Document = document;
            PageNo = pageNo;

            _renderEngine = RenderEngineFactory.CreateRenderEngine(PixelFormatStyle.RGB24);
        }

        public DjvuDocument Document { get; }

        public int PageNo { get; }

        public int Width 
        {
            get
            {
                var dimensions = _renderEngine.GetThumbailDimensions(Document.Document, PageNo);

                return dimensions.Item1;
            }
        }

        public int Height
        {
            get
            {
                var dimensions = _renderEngine.GetThumbailDimensions(Document.Document, PageNo);

                return dimensions.Item2;
            }
        }
    }
}
