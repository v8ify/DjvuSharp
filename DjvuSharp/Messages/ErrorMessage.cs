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
    public class ErrorMessage
    {
        public AnyMessage AnyMessage { get; set; }
        public string Message { get; set; }
        public string Function { get; set; }
        public string FileName { get; set; }
        public int LineNo { get; set; }

        private ErrorMessage(NativeErrorMessageStruct errStruct)
        {
            var stringMarshaler = CustomStringMarshaler.GetInstance("");

            Message = (string)stringMarshaler.MarshalNativeToManaged(errStruct.Message);
            Function = (string)stringMarshaler.MarshalNativeToManaged(errStruct.Function);
            FileName = (string)stringMarshaler.MarshalNativeToManaged(errStruct.FileName);
            LineNo = errStruct.LineNo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class NativeErrorMessageStruct
        {
            public AnyMessage Any;
            public IntPtr Message;
            public IntPtr Function;
            public IntPtr FileName;
            public int LineNo;
        }

        public static ErrorMessage GetInstance(IntPtr nativeErrorMessage)
        {
            var messageStruct = Marshal.PtrToStructure<NativeErrorMessageStruct>(nativeErrorMessage);

            return new ErrorMessage(messageStruct);
        }

        public override string ToString()
        {
            return $"{Message}\nFile Name: {FileName}\nLine Number: {LineNo}\nFunction: {Function}";
        }
    }
}
