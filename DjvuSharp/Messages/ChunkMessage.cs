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
using DjvuSharp.Marshaler;

namespace DjvuSharp.Messages
{
    public class ChunkMessage
    {
        public string ChunkId { get; set; }

        private ChunkMessage(NativeChunkMessageStruct chunkMsgStruct)
        {
            ChunkId = (string)CustomStringMarshaler.GetInstance("").MarshalNativeToManaged(chunkMsgStruct.ChunkId);
        }

        [StructLayout(LayoutKind.Sequential)]
        private class NativeChunkMessageStruct
        {
            public AnyMessage Any;
            public IntPtr ChunkId;
        }

        public static ChunkMessage GetInstance(IntPtr nativeMessageStruct)
        {
            var msg = Marshal.PtrToStructure<NativeChunkMessageStruct>(nativeMessageStruct);

            return new ChunkMessage(msg);
        }
    }
}
