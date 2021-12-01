using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
namespace MonikArt
{
    public static class Render
    {
        static MonarFile monar;
        static int spaceSize = 0;
        public static bool isDevMod = false;
        static ComputerInfo ci;
        static Process thisPr;
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
            WindowScale.Resize(1);
        play:
            spaceSize = (Console.BufferWidth - monar.widhtResolution) / 2;
            if (isDevMod) {
                thisPr = Process.GetCurrentProcess();
                ci = new ComputerInfo();
            }
            for (int i = 0; i < monar.frames.Count - 1; i++)
            {
                DateTime dateTime = DateTime.Now;
                Console.WriteLine(monar.frames[i]);
                System.Threading.Thread.Sleep(Convert.ToInt32(1000 / monar.fps));
                Console.SetCursorPosition(0, 0);
                if (isDevMod)
                    Console.WriteLine($"FRAME : {i + 1}, TARGET_FRAMERATE: {monar.fps}, RAM: {thisPr.PrivateMemorySize64/1024/1024}MB/{ci.TotalPhysicalMemory/1024/1024}MB, FRAME_RENDER_TIME: {DateTime.Now - dateTime}"); thisPr.Refresh();

            }
            if (monar.isLooping)
                goto play;
            else
            {
                loop:
                Console.Clear();
                Console.WriteLine("Do you want replay video?(y/n)");
                string inp = Console.ReadLine();
                switch (inp)
                {
                    case "y":
                        goto play;
                    case "n":
                        Program.MainMenuRender();
                        break;
                    default:
                        Console.WriteLine("Enter y if file looping, n if file not looping");
                        goto loop;

                }
            }
        }
    }
}
