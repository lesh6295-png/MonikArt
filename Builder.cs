using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonikArt
{
    public static class Builder
    {
        static MonarFile newFile;
        public static void Build()
        {
            Console.Clear();
            newFile = new MonarFile();
            Console.WriteLine("Enter vertical resolution:");
            newFile.heightResolution = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter horisontal resolution:");
            newFile.widhtResolution = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter fps(null to default valve):");
            string q = Console.ReadLine();
            if (q != "")
                newFile.fps = Convert.ToInt32(q);
            Console.SetBufferSize(newFile.widhtResolution, newFile.heightResolution);
            
        }
    }
}
