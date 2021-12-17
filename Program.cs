using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
//Sorry for this code
namespace MonikArt
{
    class Program
    {
        public const string appVersion = "MonikArt 1.0.0.0";
        static string fn = "";
        static bool d = false;
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Directory.Delete("runtime", true);
            }
            catch
            {

            }
            
            MainMenuRender();
        }
        public static void MainMenuRender()
        {

            ConsoleHelper.SetCurrentFont("Lucida Console", 12);
            
            Console.SetCursorPosition(0, 0);
            WindowMax();
            DirectoryCr();
        renderMenu:
            Render.isDevMod = false;
            Console.Clear();
            SetBufferSize.ConsoleBuffer();

                Console.WriteLine(appVersion);
                Console.WriteLine("Press 1 to open .monar file, 2 to create new .monar file, 3 to open .monar file with develop mode, 4 to made video from monar file, 0 to quit from application");

                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Render.Rend(fn);
                        break;
                    case 2:
                        Builder.Build();
                        break;
                    case 3:
                        Render.isDevMod = true;
                        Render.Rend(fn);
                        break;
                    case 4:
                        VideoBuild.Build();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Unknown command!");
                        System.Threading.Thread.Sleep(750);
                        goto renderMenu;
                }
        }


        static void DirectoryCr()
        {
            Directory.CreateDirectory("runtime");
            Directory.CreateDirectory("Monar");
        }
        static void WindowMax()
        {
            WindowScale.Resize(1);
        }
    }
}
