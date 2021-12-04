using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
namespace MonikArt
{
    class Program
    {
        public const string appVersion = "MonikArt 0.4.2.0"; 

        [STAThread]
        static void Main()
        {
#if DEBUG
            try
            {
                Directory.Delete("runtime", true);
            }
            catch
            {

            }
#endif
            MainMenuRender();
        }
        public static void MainMenuRender()
        {
            WindowMax();
            DirectoryCr();
        renderMenu:
            Render.isDevMod = false;
            Console.Clear();
            SetBufferSize.ConsoleBuffer();


            Console.WriteLine(appVersion);
            Console.WriteLine("Press 1 to open .monar file, 2 to create new .monar file, 3 to open .monar file with develop mode");
            
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Render.Rend();
                    break;
                case 2:
                    Builder.Build();
                    break;
                case 3:
                    Render.isDevMod = true;
                    Render.Rend();
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
