using System;
using System.Collections.Generic;

namespace Toggles
{
    internal static class DebugUtil
    {
        static List<string> LogTracker { get; set; } = new List<string>();

        internal static void Log(string str)
        {
            if (!LogTracker.Contains(str))
            {
                Verse.Log.Message(str);
                LogTracker.Add(str);
            }
        }

        internal static void Log(object p)
        {
            throw new NotImplementedException();
        }
    }
}