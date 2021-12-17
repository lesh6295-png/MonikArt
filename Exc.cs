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
        public static int NumberCount(this int n)
        {
            int count = 0;
            int ob = n;
            count++;
            do
            {
                count++;
                ob = ob / 10;
            } while (ob >= 10);
            return count;
        }


    }
}

