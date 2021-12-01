using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace MonikArt
{
    public static class Render
    {
        static MonarFile monar;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public static void Rend()
        {
            OpenFileDialog opf = new OpenFileDialog
            {
                Filter = "Monar file(*.monar)|*.monar"
            };
            if(opf.ShowDialog() == DialogResult.OK)
            {
                monar = Newtonsoft.Json.JsonConvert.DeserializeObject<MonarFile>(File.ReadAllText(opf.FileName));
            }
            ConsoleHelper.SetCurrentFont("Lucida Console", Convert.ToInt16(monar.fontSize));
            IntPtr ConsoleHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            SetWindowPos(ConsoleHandle, new IntPtr(-2), 0, 0, monar.widhtResolution*monar.fontSize, monar.heightResolution*monar.fontSize, 0x0040);
            play:
            for (int i = 0; i < monar.frames.Count - 1; i++)
            {
                Console.WriteLine(monar.frames[i]);
                Console.SetCursorPosition(0, 0);
                System.Threading.Thread.Sleep(Convert.ToInt32(1000 / monar.fps));
            }
            if (monar.isLooping)
                goto play;
        }
    }
}
