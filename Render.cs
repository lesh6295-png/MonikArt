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
        static int spaceSizeX = 0;
        static int spaceSizeY = 0;
        public static bool isDevMod = false;
        static ComputerInfo ci;
        static Process thisPr;
        static string buffer = "";
        static ulong totalFr = 0;
        static DateTime dateTime = DateTime.Now;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        static string RenderBuffer(Frame fr)
        {
            buffer = "";
            COORD DS = WindowSize.GetConsoleSymbolSize();
            if (DS.X < monar.widhtResolution || DS.Y < monar.heightResolution)
                throw new Exception("Too low display size");
            if (isDevMod)
            {
                WindowWrite.WriteConsole($"FRAME : {fr.num}, TARGET_FRAMERATE: {monar.fps}, RAM: {thisPr.PrivateMemorySize64 / 1024 / 1024}MB/{ci.TotalPhysicalMemory / 1024 / 1024}MB, FRAME_RENDER_TIME: {DateTime.Now - dateTime}, TOTAL_FRAMES_RENDERING: {totalFr}, CONSOLE_SIZE: X: {DS.X}, Y: {DS.Y}, MONAR_SIZE: X: {monar.heightResolution}, Y: {monar.widhtResolution},");
                dateTime = DateTime.Now;
                thisPr.Refresh();
            }
            for(int i = 0; i < spaceSizeY; i++)
            {
                
            }
            for (int i = 0; i < fr.lines.Count; i++)
            {
                buffer += BackGroundRen(spaceSizeX-1);
                buffer += fr.lines[i];
                buffer += BackGroundRen(spaceSizeX-2);
            }
            return buffer;
        }
        static string BackGroundRen(int size)
        {
            string s = "";
            for (int i = 0; i < size; i++)
                s += monar.defaultBackgr;
            return s;
        }
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
            spaceSizeX = (WindowSize.GetConsoleSymbolSize().X - monar.widhtResolution) / 2;
            spaceSizeY = (WindowSize.GetConsoleSymbolSize().Y - monar.heightResolution) / 2;
            if (isDevMod) {
                thisPr = Process.GetCurrentProcess();
                ci = new ComputerInfo();
            }
            for (int i = 0; i < monar.frames.Count - 1; i++)
            {
                totalFr++;
                if (isDevMod)
                {
                    WindowWrite.WriteConsole(RenderBuffer(monar.frames[i]), 1);
                }
                else
                {
                    WindowWrite.WriteConsole(RenderBuffer(monar.frames[i]));
                }
                System.Threading.Thread.Sleep(Convert.ToInt32(1000 / monar.fps));
                //Console.SetCursorPosition(0, 0);

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
