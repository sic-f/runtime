using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PortableAppWithLongPath
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(string.Join(Environment.NewLine, args));

            // Create a path that is longer than MAX_PATH
            DirectoryInfo newDir = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "longPath"));
            string longPath = Path.Combine(newDir.FullName, new string('x', 255));

            bool success = CreateDirectoryW(longPath, IntPtr.Zero);
            Console.WriteLine($"CreateDirectoryW with long path {(success ? "succeeded" : $"failed with {Marshal.GetLastWin32Error()}" )}");

            Directory.Delete(newDir.FullName, true /*recursive*/);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool CreateDirectoryW(string path, IntPtr securityAttributes);
    }
}
