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
using System.Runtime.InteropServices;

namespace DjvuSharp.Messages
{
    public class DjvuMessage
    {
        private IntPtr _nativeMessage;

        public DjvuMessage(IntPtr nativeMessage)
        {
            _nativeMessage = nativeMessage;
        }

        public AnyMessage Any { get { return GetInstance<AnyMessage>(); } }

        public ErrorMessage Error { get { return ErrorMessage.GetInstance(_nativeMessage); } }

        public InfoMessage Info { get { return InfoMessage.GetInstance(_nativeMessage); } }

        public NewStreamMessage NewStream { get { return NewStreamMessage.GetInstance(_nativeMessage); } }

        public DocInfoMessage DocInfo { get { return GetInstance<DocInfoMessage>(); } }

        public PageInfoMessage PageInfo { get { return GetInstance<PageInfoMessage>(); } }

        public ChunkMessage Chunk { get { return ChunkMessage.GetInstance(_nativeMessage); } }

        public RelayoutMessage Relayout { get { return GetInstance<RelayoutMessage>(); } }

        public RedisplayMessage Redisplay { get { return GetInstance<RedisplayMessage>(); } }

        public ThumbnailMessage Thumbnail { get { return GetInstance<ThumbnailMessage>(); } }

        public ProgressMessage Progress { get { return GetInstance<ProgressMessage>(); } }

        private T GetInstance<T>()
        {
            if (_nativeMessage == IntPtr.Zero)
            {
                return default(T);
            }

            return Marshal.PtrToStructure<T>(_nativeMessage);
        }
    }
}
