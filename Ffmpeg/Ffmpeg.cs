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
        public static Size disS;
        public static void ChangeVideoSize(string inputPath, string outputPath, Size newSize, int fps)
        {
            Console.WriteLine("Start change video size:");
            string com = "/C ffmpeg -i " + inputPath + $" -filter:v fps={fps} -s " + newSize.Width + "x" + newSize.Height + " -c:a copy " + outputPath;
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
            string com = @"/C ffmpeg -i """ + inputPath + @""" -s " + newSize.Width + "x" + newSize.Height + $@" ""{outputPath}""";
            Process cmd = new Process();
            cmd.StartInfo.Arguments = com;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.WaitForExit();
        }
        public static void MakeVideoFromMonar(int fps, string output)
        {
            Console.WriteLine("Start build video:");
            string com = $@"/C ffmpeg -framerate {fps} -s {disS.Width}x{disS.Height} -i {Application.StartupPath}\runtime\vb\qf%08d.png {output}";
            Process cmd = new Process();
            cmd.StartInfo.Arguments = com;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.WaitForExit();
        }
    }
}
