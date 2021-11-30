using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonikArt
{
    class Program
    {
        public const string appVersion = "MonikArt 0.0.0.1"; 

        
        static void Main(string[] args)
        {
            renderMenu:
            Console.Clear();
            Console.WriteLine(appVersion);
            Console.WriteLine("Press 1 to open .monar file, 2 to create new .monar file");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:

                    break;
                case 2:
                    Builder.Build();
                    break;
                default:
                    Console.WriteLine("Unknown command!");
                    System.Threading.Thread.Sleep(750);
                    goto renderMenu;
            }
        }
    }
}
