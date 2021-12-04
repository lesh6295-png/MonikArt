using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
namespace MonikArt
{
    public static class Ffmpeg
    {
        public static void ChangeVideoSize(string inputPath, string outputPath, Size newSize)
        {
            Console.WriteLine("Start change video size:");
            string com = "/C ffmpeg -i " + inputPath + " -s " + newSize.Width + "x" + newSize.Height + " -c:a copy " + outputPath;
            Process cmd = new Process();
            cmd.StartInfo.Arguments = com;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.WaitForExit();
        }
        public static void SliceToFrames(string inputPath)
        {
            Console.WriteLine("Start slicing video:");
            string com = "/C ffmpeg -i " + inputPath + $" runtime/{Builder.thisRuntimeName}/fram%06d.png";
            Process cmd = new Process();
            cmd.StartInfo.Arguments = com;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.WaitForExit();
        }
        public static void GifChangeVideoSize(string inputPath,string outputPath, Size newSize)
        {
            Console.WriteLine("Start change gif size:");
            string com = @"/K ffmpeg -i """ + inputPath + @""" -s " + newSize.Width + "x" + newSize.Height + $@" ""{outputPath}""";
            Process cmd = new Process();
            cmd.StartInfo.Arguments = com;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.WaitForExit();
        }
    }
}
