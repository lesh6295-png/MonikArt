using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonikArt
{
    public static class Exc
    {
        public static bool IsOdd(this int n)
        {
            if (n % 2 == 0)
                return true;
            return false;
        }
    }
}
