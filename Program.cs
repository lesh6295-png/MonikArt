using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MonikArt
{
    class Program
    {
        public const string appVersion = "MonikArt 0.2.3.0"; 

        [STAThread]
        static void Main()
        {
            Directory.Delete("runtime", true);
            MainMenuRender();
        }
        public static void MainMenuRender()
        {
            WindowMax();
            DirectoryCr();
        renderMenu:
            Render.isDevMod = false;
            Console.Clear();


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
