using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonikArt
{
    public static class BitmapExc
    {
        public static void ToGray(this Bitmap bitmap)
        {
            for(int x = 0; x < bitmap.Width; x++)
            {
                for(int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int q = (color.R + color.B + color.G) / 3;
                    bitmap.SetPixel(x, y, Color.FromArgb(color.A, q, q, q));
                }
            }
        }
    }
}
