using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            int row = 0;
            int col = 0;
            int counter = 0;
            cipherText = cipherText.ToLower();


            for (int i = 2; i <= 7 ; i++)
            {
                if (plainText.Length % i == 0)
                {
                    col = i;
                }
            }

            row = plainText.Length / col;
            char[,] a = new char[row, col];
            char[,] b = new char[row, col];
            List<int> ans = new List<int>(col);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (counter < plainText.Length)
                    {
                        a[i, j] = plainText[counter];
                        counter++;

                    }


                }
            }

            counter = 0;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (counter < plainText.Length)
                    {
                        b[j, i] = cipherText[counter];
                        counter++;
                    }
                }
            }

            int ctr = 0;
            for (int i = 0; i < col; i++)
            {
                for (int k = 0; k < col; k++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        if (a[j, i] == b[j, k])
                        {
                            ctr++;
                        }
                        int x = k + 1;
                        if (ctr == row)
                            ans.Add(x);
                    }
                    ctr = 0;
                }
            }

            if (ans.Count == 0)
            {
                for (int i = 0; i < col + 2; i++)
                {
                    ans.Add(0);
                }
            }
            return ans;
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            Dictionary<int, string> m = new Dictionary<int, string>();
            List<char> list = new List<char>();
            List<String> s = new List<string>();
            String Text1 = "";
            String Text2 = "";

            String plainText = "";
            int keyCapacity = 0;

            foreach (var c in key)
            {
                keyCapacity++;
            }
            double row_size = (double)cipherText.Length / (double)(keyCapacity);


            for (int i = 0; i < keyCapacity; i++)
            {
                for (int j = 0; j < row_size; j++)
                {
                    Text1 += cipherText[i * (int)row_size + j];
                }
                s.Add(Text1);
                Text1 = "";
            }

            for (int i = 0; i < keyCapacity; i++)
            {
                m.Add(key.ElementAt(i), s.ElementAt(key.ElementAt(i) - 1));
            }
            foreach (var c in m)
            {
                plainText += c.Value;
            }
            for (int i = 0; i < row_size; i++)
            {
                for (int j = 0; j < keyCapacity; j++)
                {
                    Text2 += plainText[j * (int)row_size + i];

                }
            }
            return Text2;
        }


        public string Encrypt(string plainText, List<int> key)
        {
            List<char> list = new List<char>();
            String Text1 = "";
            String cypherText = "";
            List<String> s = new List<string>();
            Dictionary<int, String> m = new Dictionary<int, string>();
            int keyCapacity = 0;
            foreach (var c in key)
            {
                keyCapacity++;
            }
            int sCapacity = s.Capacity - 1;
            for (int i = 0; i < plainText.Length; i++)
            {
                list.Add(plainText[i]);
            }
            double row_size = Math.Ceiling((double)plainText.Length / (double)(keyCapacity));
            for (int i = 0; i < keyCapacity; i++)
            {
                for (int j = 0; j < row_size; j++)
                {
                    try
                    {
                        Text1 += list.ElementAt(j * keyCapacity + i);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        continue;
                    }
                }
                s.Add(Text1);
                Text1 = "";

            }
            for (int i = 0; i < keyCapacity; i++)
            {
                m.Add(key.ElementAt(i), s.ElementAt(i));
            }

            cypherText = string.Join("", m.OrderBy(x => x.Key).Select(x => x.Value));

            return cypherText;

        }
    }
}
