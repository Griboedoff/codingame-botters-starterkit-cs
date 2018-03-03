using System;

namespace botters
{
    public static class Logger
    {
        private const bool Debug = true;

        public static void Log(string s) => Console.Error.WriteLine(s);

        public static void LogDebug(string s)
        {
            if (Debug)
                Log(s);
        }
    }
}