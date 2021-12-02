using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonikArt
{
    public class MonarFile
    {
        public int heightResolution, widhtResolution = 0;
        public double fps = 24;
        public List<Frame> frames = new List<Frame>();
        public string name = "";
        public bool isLooping = false;
        public int fontSize = 12;
        public char defaultBackgr = ' ';
    }
    public struct Frame 
    {
        public int num;
        public List<string> lines;
    }
}
