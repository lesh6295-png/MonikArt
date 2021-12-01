using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

static class WindowScale
{
    internal static class DllImports
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {

            public short X;
            public short Y;
            public COORD(short x, short y)
            {
                this.X = x;
                this.Y = y;
            }

        }
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetStdHandle(int handle);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleDisplayMode(
            IntPtr ConsoleOutput
            , uint Flags
            , out COORD NewScreenBufferDimensions
            );
    }
    public static void Resize(uint pos)
    {
        IntPtr hConsole = DllImports.GetStdHandle(-11);
        DllImports.COORD xy = new DllImports.COORD(100, 100);
        DllImports.SetConsoleDisplayMode(hConsole, pos, out xy);
    } 
}