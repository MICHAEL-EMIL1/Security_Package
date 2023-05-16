using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        private int ModInverse(int a, int m)
        {
            int m0 = m, t, q;
            int y = 0, x = 1;
            if (m == 1)
            {
                return 0;
            }
            while (a > 1)
            {
                // q is quotient
                q = a / m;
                t = m;
                // m is remainder now, process same as
                // Euclid's algo
                m = a % m;
                a = t;
                t = y;
                // Update x and y
                y = x - (q * y);
                x = t;
            }
            // Make x positive
            if (x < 0)
            {
                x += m0;
            }
            return x;
        }
        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            List<long> ans = new List<long>();
            long W = 1, c1 = 1, c2;
                
            for (int i = 0; i < k; i++)
            {
                W = (W * y) % q;
            }
           
            for (int i = 0; i < k; i++)
            {
                c1 = (c1 * alpha) % q;
            }
            c2 = (W * m) % q;
            ans.Add(c1);
            ans.Add(c2);
            return ans;

        }

        public int Decrypt(int c1, int c2, int x, int q)
        {
            int kinverse, k = 1, m;
            for (int i = 0; i < x; i++)
            {
                k = (k * c1) % q;
            }
            kinverse = ModInverse(k, q);
            m = (c2 * kinverse) % q;
            return m;
        }
    }
}
