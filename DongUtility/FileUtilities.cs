using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DongUtility
{
    public static class FileUtilities
    {
        static public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        static public string GetMainProjectDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
