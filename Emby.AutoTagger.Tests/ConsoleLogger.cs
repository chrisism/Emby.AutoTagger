using System;
using System.Text;
using MediaBrowser.Model.Logging;

namespace Emby.AutoTagger.Tests
{
    public class ConsoleLogger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(string message, params object[] paramList)
        {
            this.WriteLine(message);
        }

        public void Error(string message, params object[] paramList)
        {
            this.WriteLine(message);
        }

        public void Warn(string message, params object[] paramList)
        {
            this.WriteLine(message);
        }

        public void Debug(string message, params object[] paramList)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, params object[] paramList)
        {
            throw new NotImplementedException();
        }

        public void FatalException(string message, Exception exception, params object[] paramList)
        {
            throw new NotImplementedException();
        }

        public void ErrorException(string message, Exception exception, params object[] paramList)
        {
            this.WriteLine(message);
        }

        public void LogMultiline(string message, LogSeverity severity, StringBuilder additionalContent)
        {
            throw new NotImplementedException();
        }

        public void Log(LogSeverity severity, string message, params object[] paramList)
        {
            throw new NotImplementedException();
        }

        public void Log(LogSeverity severity, ReadOnlyMemory<char> message)
        {
            throw new NotImplementedException();
        }

        public void Error(ReadOnlyMemory<char> message)
        {
            throw new NotImplementedException();
        }

        public void Warn(ReadOnlyMemory<char> message)
        {
            throw new NotImplementedException();
        }

        public void Info(ReadOnlyMemory<char> message)
        {
            throw new NotImplementedException();
        }

        public void Debug(ReadOnlyMemory<char> message)
        {
            throw new NotImplementedException();
        }
    }
}