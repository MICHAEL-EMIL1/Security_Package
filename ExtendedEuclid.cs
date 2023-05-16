using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            int a = number;
            int b = baseN;
            int x0 = 1;
            int y0 = 0;
            int x1 = 0;
            int y1 = 1;

            while (b > 0)
            {
                int quotient = a / b;
                int remainder = a % b;
                a = b;
                b = remainder;

                int tempX = x1;
                int tempY = y1;
                x1 = x0 - quotient * x1;
                y1 = y0 - quotient * y1;
                x0 = tempX;
                y0 = tempY;
            }

            if (a != 1)
            {
                return -1;
            }

            // to make sure the result is positive in the range [0, baseN)
            int result = (x0 % baseN + baseN) % baseN;
            return result;
        }


    }
}
