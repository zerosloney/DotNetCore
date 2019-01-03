using System;

namespace DotNetCore.Logging
{
    public interface ILogger
    {
        void Error(Exception exception);
    }
}
