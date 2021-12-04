using System;
using System.Runtime.InteropServices;
using System.Text;
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
public static class WindowSize
{
    [DllImport("kernel32.dll")]
    static extern COORD GetLargestConsoleWindowSize(IntPtr hConsoleOutput);
    public static COORD GetConsoleSymbolSize()
    {
        return GetLargestConsoleWindowSize(WindowScale.DllImports.GetStdHandle(-11));
    }
}
public struct COORD
{
    public short X;
    public short Y;
}
public static class WindowWrite
{
    [DllImport("kernel32.dll", SetLastError =true)]
    internal static extern bool WriteConsoleOutputCharacter(
        IntPtr hConsoleOutput,
        StringBuilder lpCharacter,
        uint nLength,
        COORD dwWriteCoord,
        out uint lpNumberOfCharsWritten);
    public static void WriteConsole(string line)
    {
        StringBuilder sb = new StringBuilder(line);
        COORD st; st.X = 0; st.Y = 0;
        uint outp = 0;
        WriteConsoleOutputCharacter(WindowScale.DllImports.GetStdHandle(-11), sb, Convert.ToUInt32(line.Length), st, out outp);
    }
    public static void WriteConsole(string line, short y)
    {
        StringBuilder sb = new StringBuilder(line);
        COORD st; st.X = 0; st.Y = y;
        uint outp = 0;
        WriteConsoleOutputCharacter(WindowScale.DllImports.GetStdHandle(-11), sb, Convert.ToUInt32(line.Length), st, out outp);
    }
}
public static class SetCursor
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);
    public static void Set(int x, int y)
    {
        SetCursorPos(x, y);
    }

}
public static class SetBufferSize
{
    [DllImport("kernel32.dll")]
    static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD dwSize);
    public static void ConsoleBuffer()
    {
        COORD bs = WindowSize.GetConsoleSymbolSize();
        bs.X -= 3;
        SetConsoleScreenBufferSize(WindowScale.DllImports.GetStdHandle(-11), bs);
    }
}