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


using DjvuSharp.Marshaler;
using System;
using System.Runtime.InteropServices;

namespace DjvuSharp.Messages
{
    public class InfoMessage
    {
        public string Message { get; set; }

        private InfoMessage(NativeInfoMessageStruct infoMessage)
        {
            Message = (string)CustomStringMarshaler.GetInstance("").MarshalNativeToManaged(infoMessage.Message);
        }

        [StructLayout(LayoutKind.Sequential)]
        private class NativeInfoMessageStruct
        {
            public AnyMessage AnyMessage;
            public IntPtr Message;
        }

        public static InfoMessage GetInstance(IntPtr nativeMessageStruct)
        {
            var msg = Marshal.PtrToStructure<NativeInfoMessageStruct>(nativeMessageStruct);

            return new InfoMessage(msg);
        }
    }
}
