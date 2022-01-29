using DjvuSharp.Marshaler;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DjvuSharp.Messages
{
    public class NewStreamMessage
    {
        public int StreamId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        private NewStreamMessage(NativeNewStreamMessageStruct newstreamMsgStruct)
        {
            var stringMarshaler = CustomStringMarshaler.GetInstance("");

            StreamId = newstreamMsgStruct.StreamId;
            Name = (string)stringMarshaler.MarshalNativeToManaged(newstreamMsgStruct.Name);
            Url = (string)stringMarshaler.MarshalNativeToManaged(newstreamMsgStruct.Url);
        }

        [StructLayout(LayoutKind.Sequential)]
        private class NativeNewStreamMessageStruct
        {
            public AnyMessage Any;
            public int StreamId;
            public IntPtr Name;
            public IntPtr Url;
        }

        public static NewStreamMessage GetInstance(IntPtr nativeMessageStruct)
        {
            var msg = Marshal.PtrToStructure<NativeNewStreamMessageStruct>(nativeMessageStruct);

            return new NewStreamMessage(msg);
        }
    }
}
