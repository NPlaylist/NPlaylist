using System;
using System.Runtime.Serialization;

namespace NPlaylist.M3u.Exceptions
{
    [Serializable]
    public class MediaFormatException : FormatException
    {
        public MediaFormatException()
        {
        }

        public MediaFormatException(string message) : base(message)
        {
        }

        public MediaFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MediaFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
