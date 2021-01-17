using System;

namespace OnlineStore.Infrastructure.Exceptions
{
    public class OSPlatformException : Exception
    {
        public OSPlatformException() {}

        public OSPlatformException(string message): base(message) {}
    }
}
