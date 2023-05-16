using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            //throw new NotImplementedException();
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            int d = 0;
            for (int i = 1; i < phi; i++)
            {
                if ((i * e) % phi == 1)
                {
                    d = i;
                    break;
                }
            }
            int C = 1;
            for (int i = 0; i < e; i++)
            {
                C = (C * M) % n;
            }
            return C;
        }

        // Computes the modular inverse of a modulo m using the extended Euclidean algorithm
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

        // Computes the modular exponentiation of base to the exponent mod m using the square-and-multiply algorithm
        private int ModPow(int baseValue, int exponent, int modulus)
        {
            if (modulus == 1) return 0;
            int result = 1;
            baseValue %= modulus;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                {
                    result = (result * baseValue) % modulus;
                }
                exponent = exponent >> 1;
                baseValue = (baseValue * baseValue) % modulus;

            }
            return result;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            //throw new NotImplementedException();
            // Compute n and the totient of n
            int n = p * q;
            int totient = (p - 1) * (q - 1);

            // Compute the private key exponent d
            int d = ModInverse(e, totient);

            // Check that d is the modular multiplicative inverse of e modulo totient
            if (d <= 0)
            {
                throw new ArgumentException("Invalid value of d.");
            }

            // Compute the values needed for the Chinese Remainder Theorem
            int dp = d % (p - 1);
            int dq = d % (q - 1);
            int qInv = ModInverse(q, p);

            // Decrypt the ciphertext using the Chinese Remainder Theorem
            int m1 = ModPow(C % p, dp, p);
            int m2 = ModPow(C % q, dq, q);
            int h = (qInv * (m1 - m2 + p)) % p;
            int M = m2 + h * q;

            return M;
        }
    }
}
