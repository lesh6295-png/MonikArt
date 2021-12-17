using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MonikArt
{
    public static class VideoBuild
    {
        public static int frameRate;
        public static void Build()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Title = "Enter output video file",
                Filter = "Video files(*.mp4, *.mkv, *.avi, *.mpeg)|*.mpeg; *.mp4; *.avi; *.mkv"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string pathtosave = sfd.FileName;
                Directory.CreateDirectory("runtime\\vb");
                Render.VideoR = true;
                Render.Rend(null);
                Ffmpeg.MakeVideoFromMonar(frameRate, pathtosave);
                Console.CursorVisible = true;
                Program.MainMenuRender();
            }
        }
    }
}
