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
using DjvuSharp.Messages;
using DjvuSharp.Enums;

namespace DjvuSharp
{
    internal static class Utils
    {
        internal static List<object> ProcessMessages(IntPtr context, bool wait = true)
        {
            var list = new List<object>();

            IntPtr msg = IntPtr.Zero;

            if (wait)
            {
                msg = Native.ddjvu_message_wait(context);
            }

            msg = Native.ddjvu_message_peek(context);

            while (msg != IntPtr.Zero)
            {
                DjvuMessage message = new DjvuMessage(msg);

                switch (message.Any.Tag)
                {
                    case MessageTag.ERROR:
                        list.Add(message);
                        var errorMessage = message.Error;
                        throw new ApplicationException(errorMessage.ToString());
                    case MessageTag.INFO:
                        list.Add(message);
                        break;
                    case MessageTag.NEWSTREAM:
                        list.Add(message);
                        break;
                    case MessageTag.DOCINFO:
                        list.Add(message);
                        break;
                    case MessageTag.PAGEINFO:
                        list.Add(message);
                        break;
                    case MessageTag.RELAYOUT:
                        list.Add(message);
                        break;
                    case MessageTag.CHUNK:
                        list.Add(message);
                        break;
                    case MessageTag.THUMBNAIL:
                        list.Add(message);
                        break;
                    case MessageTag.PROGRESS:
                        list.Add(message);
                        break;
                }

                Native.ddjvu_message_pop(context);
                msg = Native.ddjvu_message_peek(context);
            }

            return list;
        }

        /// <summary>
        /// <p>When we construct a document, djvulibre starts decoding it in background.<p>
        /// <p>Also djvulibre decodes a document in chunks. Hence, we don't have to wait till
        /// all of the document is decoded.
        /// </p>
        /// <p>We use this function to know if the decoding of document is complete.</p>
        /// </summary>
        /// <returns>A boolean. true if decoding of the document is finished. false otherwise.</returns>
        internal static bool IsDecodingDone(JobStatus status)
        {
            return status >= JobStatus.JOB_OK;
        }
    }
}
