using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
namespace MonikArt
{
    public static class Render
    {
        static MonarFile monar;
        static double spaceSizeX = 0;
        static double spaceSizeY = 0;
        public static bool isDevMod = false;
        static ComputerInfo ci;
        static Process thisPr;
        static string buffer = "";
        static ulong totalFr = 0;
        static DateTime dateTime = DateTime.Now;
        const int xOffset = 17;

        public static bool ex = false;
        public static bool ps = false;

        public static bool VideoR = false;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        static string RenderBuffer(Frame fr)
        {

            ScreenControl.PressKey();
            buffer = "";
            COORD DS = WindowSize.GetConsoleSymbolSize();
            if (DS.X < monar.widhtResolution || DS.Y < monar.heightResolution)
                throw new Exception("Too low display size");
            if (isDevMod)
            {
                WindowWrite.WriteConsole($"FRAME : {fr.num}, TARGET_FRAMERATE: {monar.fps}, RAM: {thisPr.PrivateMemorySize64 / 1024 / 1024}MB/{ci.TotalPhysicalMemory / 1024 / 1024}MB, FRAME_RENDER_TIME: {DateTime.Now - dateTime}, TOTAL_FRAMES_RENDERING: {totalFr}, CONSOLE_SIZE: X: {DS.X}, Y: {DS.Y}, MONAR_SIZE: X: {monar.widhtResolution}, Y: {monar.heightResolution}, SPACE_SIZE : X: {spaceSizeX}, Y: {spaceSizeY}");
                dateTime = DateTime.Now;
                thisPr.Refresh();
            }
            for (int i = 0; i < spaceSizeY; i++)
            {
                for (int j = 0; j < DS.X - 4; j++)
                {
                    buffer += monar.defaultBackgr;
                }
            }
            for (int i = 0; i < fr.lines.Count; i++)
            {
                buffer += BackGroundRen(Convert.ToInt32(Math.Ceiling(spaceSizeX + 1 + xOffset)));
                buffer += fr.lines[i];
                buffer += BackGroundRen(Convert.ToInt32(Math.Floor(spaceSizeX - xOffset)));
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
        public static void UnpackMonar()
        {
            OpenFileDialog opf = new OpenFileDialog
            {
                Filter = "Monar file(*.monar)|*.monar"
            };
            if (opf.ShowDialog() == DialogResult.OK)
            {
                monar = Newtonsoft.Json.JsonConvert.DeserializeObject<MonarFile>(File.ReadAllText(opf.FileName));
            }
        }
        public static void Rend(string path)
        {
            WindowScale.Resize(1);
            ex = false; ps = false;
            if(path == string.Empty || path == null)
                UnpackMonar();
            else
                monar = Newtonsoft.Json.JsonConvert.DeserializeObject<MonarFile>(File.ReadAllText(path));
            ConsoleHelper.SetCurrentFont("Lucida Console", Convert.ToInt16(monar.fontSize));
            IntPtr ConsoleHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            SetWindowPos(ConsoleHandle, new IntPtr(-2), 0, 0, monar.widhtResolution * monar.fontSize, monar.heightResolution * monar.fontSize, 0x0040);
            WindowScale.Resize(1);
            SetCursor.Set(9999, 9999);
            Thread cur = new Thread(curset);
            cur.Name = "Cursor position setup";
            cur.IsBackground = false;
            cur.Start();
        play:
            spaceSizeX = (WindowSize.GetConsoleSymbolSize().X - monar.widhtResolution) / 2;
            spaceSizeY = (WindowSize.GetConsoleSymbolSize().Y - monar.heightResolution) / 2;
            if (VideoR)
            {
                monar.isLooping = false;
                VideoBuild.frameRate = Convert.ToInt32(monar.fps);
            }
            if (isDevMod)
            {
                thisPr = Process.GetCurrentProcess();
                ci = new ComputerInfo();
            }
            for (int i = 0; i < monar.frames.Count - 1; i++)
            {
                UpdateData();
                if (ex == false)
                {
                    Console.CursorVisible = false;
                    totalFr++;
                    if (isDevMod)
                    {
                        WindowWrite.WriteConsole(RenderBuffer(monar.frames[i]), 1);
                    }
                    else
                    {
                        WindowWrite.WriteConsole(RenderBuffer(monar.frames[i]));
                    }
                    if (VideoR)
                    {
                        string fn = "";
                        fn += "runtime\\vb\\qf";
                        for (int q = 0; q < 9 - i.NumberCount(); q++)
                            fn += "0";
                        fn += i;
                        fn += ".png";
                        Screenshot.Made(fn);
                    }
                    if (monar.fps != 0)
                    {
                        System.Threading.Thread.Sleep(Convert.ToInt32(1000 / monar.fps));
                    }
                }
                else
                {
                    MenuRend();
                }
            }
            if (monar.isLooping)
                goto play;
            else
            {
                if (!VideoR)
                {
                    MenuRend();
                }
            }
        }
        static void MenuRend()
        {
        loop:
            Console.Clear();
            Console.WriteLine("Do you want replay video?(y/n)");
            string inp = Console.ReadLine();
            switch (inp)
            {
                case "y":
                    Rend(null);
                    break;
                case "n":
                    Program.MainMenuRender();
                    break;
                default:
                    Console.WriteLine("Enter y if file looping, n if file not looping");
                    goto loop;

            }
        }
        public static void UpdateData()
        {
            if (Console.KeyAvailable)
                if (Console.ReadKey(false).Key == ConsoleKey.S)
                    ex = true;

        }
        static void curset()
        {
            SetCursor.Set(9999, 9999);
            Thread.Sleep(20000);
            curset();
        }
    }
}
