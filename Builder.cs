using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MonikArt
{
    public static class Builder
    {
        static MonarFile newFile;
        public static string thisRuntimeName = "";
        public static string grad = (string)"ÆÑÊŒØ@Jƒîrjv1lí=i<7[¦;’` ";
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public static void Build()
        {
            grad = new string(grad.Reverse().ToArray());
            Console.Clear();
            thisRuntimeName = randStr();
            Directory.CreateDirectory($"runtime/{thisRuntimeName}");
            newFile = new MonarFile();

            Console.WriteLine("Enter console font size:");
            string aw = Console.ReadLine();
            if (aw == "")
                aw = "12";
            int size = Convert.ToInt32(aw);

            newFile.fontSize = size;
            ConsoleHelper.SetCurrentFont("Lucida Console", Convert.ToInt16(size));

            

            Console.WriteLine($"Enter horisontal resolution({Console.LargestWindowHeight}):");
            newFile.widhtResolution = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Enter vertical resolution({Console.LargestWindowWidth}):");
            newFile.heightResolution = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter fps(null to default valve):");
            string q = Console.ReadLine();
            if (q != "")
                newFile.fps = Convert.ToInt32(q);
            loop:
            Console.WriteLine("Video is looping?(y/n)");
            string ans = Console.ReadLine();
            if (ans == "y")
                newFile.isLooping = true;
            else if (ans == "n")
                newFile.isLooping = false;
            else
            {
                Console.WriteLine("Enter y if file looping, n if file not looping"); goto loop;
            }

            Console.WriteLine("Enter name:");
            newFile.name = Console.ReadLine();

            

            // magic
            IntPtr ConsoleHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            SetWindowPos(ConsoleHandle, new IntPtr(-2), 0, 0, newFile.widhtResolution * 12, newFile.heightResolution * 12, 0x0040);

            Console.Clear();
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Select video",
                Filter = "Video and Photo files (*.mp4, *.mkv, *.avi, *.mpeg, *.gif)|*.mp4; *.mkv; *.avi; *.mpeg; *.gif"
            };
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                Ffmpeg.ChangeVideoSize(ofd.FileName, $"runtime/{thisRuntimeName}/vid.mkv", new System.Drawing.Size(newFile.widhtResolution, newFile.heightResolution));
                Ffmpeg.SliceToFrames($"runtime/{thisRuntimeName}/vid.mkv");
                DirectoryInfo s = new DirectoryInfo($"runtime/{thisRuntimeName}");
                var files = s.GetFiles();
                var frames = new List<FileInfo>();
                foreach(FileInfo fi in files)
                {
                    if (fi.Name.StartsWith("fram"))
                        frames.Add(fi);
                }
                GC.Collect();
                frames.Sort(StrCom);
                Console.WriteLine("Start build monar");
                foreach(FileInfo fi in frames)
                {
                    string frame = "";
                    Bitmap bitmap = new Bitmap(fi.FullName);
                    bitmap.ToGray();
                    for(int y = 0; y < bitmap.Height; y++)
                    {
                        for(int x = 0; x < bitmap.Width; x++)
                        {
                            Color color = bitmap.GetPixel(x, y);
                            frame += grad[Convert.ToInt32((grad.Length-1) * color.R / 255)];
                        }
                        frame += "\n";
                    }
                    newFile.frames.Add(frame);

                }
                File.WriteAllText($"Monar/{newFile.name}.monar", Newtonsoft.Json.JsonConvert.SerializeObject(newFile, Newtonsoft.Json.Formatting.Indented));
            }
        }
        static int StrCom(FileInfo x, FileInfo y)
        {
            string z = Path.GetFileNameWithoutExtension(x.Name).Remove(0, 4); string t = Path.GetFileNameWithoutExtension(y.Name).Remove(0, 4);
            int c = Convert.ToInt32(z); int u = Convert.ToInt32(t);
            if (c > u)
                return 1;
            else if (u > c)
                return -1;
            else
                return 0;
        }
        static string randStr(int q = 25)
        {
            string lib = "qwertyuioplkjhgfdsazxcvbnm1234567890QWERTYUIOPLKJHGFDSAZXCVBNM";
            string output = "";
            for(int i = 0; i < q; i++)
            {
                output += lib[new Random().Next(lib.Length)];
                System.Threading.Thread.Sleep(34);
            }
            return output;
        }
    }
}
