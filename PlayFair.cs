using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{

    public class PlayFair : ICryptographic_Technique<string, string>
    {
        int z1, z2;
        char[,] a = new char[8, 8];
        public void getIndecies(char t1)
        {
            char t2 = t1;
            if (t1 == 'i')
            {
                t1 = 'j';
            }
            else if (t1 == 'j')
            {
                t1 = 'i';
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (a[i, j] == t1 || a[i, j] == t2)
                    {
                        z1 = i;
                        z2 = j;
                        return;
                    }
                }
            }

        }

        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            int k = 0;
            char x1 = new char();
            char x2 = new char();
            bool[] check = new bool[28];
            string ans = "";
            string tmp = "";
            for (char i = 'a'; i <= 'z'; i++)
            {
                key += i;
            }

            for (int i = 0; i < 5 && k < key.Length; i++)
            {
                for (int j = 0; j < 5 && k < key.Length;)
                {
                    if (check[key[k] - 'a'])
                    {
                        k++;

                    }
                    else
                    {
                        if (key[k] == 'i' || key[k] == 'j')
                        {
                            check['i' - 'a'] = check['j' - 'a'] = true;
                        }
                        check[key[k] - 'a'] = true;
                        a[i, j] = key[k];
                        k++;
                        j++;
                    }
                }
            }
            for (int i = 0; i < cipherText.Length; i += 2)
            {
                x1 = cipherText[i];
                x2 = cipherText[i + 1];
                int r1, y1, r2, y2;
                getIndecies(x1);

                r1 = z1;
                y1 = z2;
                Console.WriteLine(r1 + " " + y1);
                getIndecies(x2);
                r2 = z1;
                y2 = z2;
                Console.WriteLine(r2 + " " + y2);
                if (r1 == r2)
                {
                    ans += a[r1, (y1 - 1 + 5) % 5];
                    ans += a[r2, (y2 - 1 + 5) % 5];
                }
                else if (y1 == y2)
                {
                    ans += a[(r1 - 1 + 5) % 5, y1];
                    ans += a[(r2 - 1 + 5) % 5, y2];
                }
                else
                {
                    ans += a[r1, y2];
                    ans += a[r2, y1];
                }
            }
            for (int i = 0; i < ans.Length; i += 2)
            {
                tmp += ans[i];
                if ((i + 2 < ans.Length && ans[i + 2] == ans[i] && ans[i + 1] == 'x') || (i + 2 >= ans.Length && ans[i + 1] == 'x'))
                {
                    continue;
                }
                tmp += ans[i + 1];
            }

            return tmp;
        }


        public string Encrypt(string plainText, string key)
        {
            plainText = plainText.ToLower();
            key = key.ToLower();
            int k = 0;
            char x1 = new char();
            char x2 = new char();
            bool[] check = new bool[28];
            string ans = "";
            string tmp = "";

            for (char i = 'a'; i <= 'z'; i++)
            {
                key += i;
            }

            for (int i = 0; i < 5 && k < key.Length; i++)
            {
                for (int j = 0; j < 5 && k < key.Length;)
                {
                    if (check[key[k] - 'a'])
                    {
                        k++;

                    }
                    else
                    {
                        if (key[k] == 'i' || key[k] == 'j')
                        {
                            check['i' - 'a'] = check['j' - 'a'] = true;
                        }
                        check[key[k] - 'a'] = true;
                        a[i, j] = key[k];
                        k++;
                        j++;
                    }
                }
            }
            bool lastadded = false;
            for (int i = 1; i < plainText.Length; i += 2)
            {
                char c1, c2;
                c1 = plainText[i - 1];
                c2 = plainText[i];
                tmp += c1;
                if (c1 == c2)
                {
                    tmp += 'x';
                    i--;
                    continue;
                }
                tmp += plainText[i];
                if (i == plainText.Length - 1)
                {
                    lastadded = true;
                }
            }
            if (!lastadded)
            {
                tmp += plainText[plainText.Length - 1];
            }
            if (tmp.Length % 2 != 0)
            {
                tmp += 'x';
            }

            for (int i = 0; i < tmp.Length; i += 2)
            {
                x1 = tmp[i];
                x2 = tmp[i + 1];
                int r1, y1, r2, y2;
                getIndecies(x1);

                r1 = z1;
                y1 = z2;
                getIndecies(x2);
                r2 = z1;
                y2 = z2;
                if (r1 == r2)
                {
                    ans += a[r1, (y1 + 1) % 5];
                    ans += a[r2, (y2 + 1) % 5];
                }
                else if (y1 == y2)
                {
                    ans += a[(r1 + 1) % 5, y1];
                    ans += a[(r2 + 1) % 5, y2];
                }
                else
                {
                    ans += a[r1, y2];
                    ans += a[r2, y1];
                }
            }
            return ans;
        }
    }
}
