using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {

            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    for (int k = 0; k < 26; k++)
                    {
                        for (int l = 0; l < 26; l++)
                        {
                            List<int> ans = new List<int>() { i, j, k, l };
                            List<int> cipher = Encrypt(plainText, ans);
                            if (cipher.SequenceEqual(cipherText))
                            {
                                return ans;
                            }
                        }
                    }
                }
            }
          
            throw new InvalidAnlysisException();
            //throw new NotImplementedException();
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            //throw new NotImplementedException();
            //get key_inverse
            int temp, temp1, temp2, temp3, a = 0, b = 0, d = 0, dett = 0;
            var d_ = new List<int>();
            var d__ = new List<int>();
            var r = new List<int>();
            int keycount = key.Count, ciphercount = cipherText.Count, equvnum = 0, sumation = 0;
            var ciphertex = new List<int>();
            //finding determinant of the matrix
            if (keycount == 4)
            {
                a = ((key[0] * key[3]) - (key[1] * key[2]));
                a %= 26;
                while (a > 26)
                {
                    a %= 26;
                }
                while (a < 26)
                {
                    a += 26;
                }
                if (a % 2 == 0) throw new SystemException();
                key[1] = key[1] * (-1);
                key[2] = key[2] * (-1);
                temp = key[0];
                key[0] = key[3];
                key[3] = temp;
                for (int i = 0; i < 4; i++)
                {
                    if (key[i] < 26)
                    {
                        key[i] += 26;
                    }
                    d = (a * key[i]);
                    d %= 26;
                    d_.Add(d);
                }
                for (int i = 0; i < ciphercount; i += 2)
                {
                    for (int j = 0; j < keycount; j += 2)
                    {
                        equvnum = (cipherText[i] * d_[j] + cipherText[i + 1] * d_[j + 1]) % 26;
                        ciphertex.Add(equvnum);
                    }
                }
                return ciphertex;
            }
            else
            {
                //dett = (key[0] * ((key[4] * key[8]) - (key[5] * key[7])))
                //    - (key[1] * ((key[3] * key[8]) - (key[5] * key[6])))
                //    + (key[2] * ((key[3] * key[7]) - (key[4] * key[6])));



                //while (dett > 26)
                //{
                //    dett %= 26;
                //}
                //while (dett < 26)
                //{
                //    dett += 26;
                //}

                dett = (key[0] * (key[4] * key[8] - key[5] * key[7]) - key[1] * (key[3] * key[8] - key[5] * key[6]) + key[2] * (key[3] * key[7] - key[6] * key[4])) % 26;
                while (dett > 26)
                {
                    dett %= 26;
                }
                while (dett < 0)
                {
                    dett += 26;
                }




                Console.WriteLine("det=" + dett);

                for (int i = 1; i < 26; i++)
                {
                    if (((i * dett) % 26) == 1)
                    {
                        b = i;
                        break;
                    }
                }

                Console.WriteLine("b=" + b);
                //co factor
                List<int> mat = new List<int>();

                Console.WriteLine();

                List<int> l = new List<int>();
                Console.Write("l={");


                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        l.Add((int)Math.Pow(-1, i + j));
                        Console.Write(l[i] + ",");
                    }
                }


                mat.Add((((key[4] * key[8]) - (key[5] * key[7])) * b * l[0]) % 26);
                mat.Add((((key[3] * key[8]) - (key[5] * key[6])) * b * l[1]) % 26);
                mat.Add((((key[3] * key[7]) - (key[4] * key[6])) * b * l[2]) % 26);
                mat.Add((((key[1] * key[8]) - (key[2] * key[7])) * b * l[3]) % 26);
                mat.Add((((key[0] * key[8]) - (key[2] * key[6])) * b * l[4]) % 26);
                mat.Add((((key[0] * key[7]) - (key[1] * key[6])) * b * l[5]) % 26);
                mat.Add((((key[1] * key[5]) - (key[2] * key[4])) * b * l[6]) % 26);
                mat.Add((((key[0] * key[5]) - (key[2] * key[3])) * b * l[7]) % 26);
                mat.Add((((key[4] * key[0]) - (key[1] * key[3])) * b * l[8]) % 26);

                for (int i = 0; i < keycount; i++)
                {
                    while (mat[i] > 26) mat[i] %= 26;
                    while (mat[i] < 0) mat[i] += 26;

                }


                Console.Write("mat={");
                for (int i = 0; i < keycount; i++)
                {
                    Console.Write(mat[i] + ",");
                }



                //Console.Write("l before trans={");
                //for (int i = 0; i < key.Count; i++)
                //{
                //    int val = ((mat[i] * l[i]) % 26)%26;

                //    while (val > 26) val %= 26;
                //   // while (val < 0) val += 26;
                //    l.Add(val);

                //}



                for (int i = 0; i < keycount; i++)
                {
                    Console.Write(l[i] + ",");
                }



                //for (int i = 0; i < keycount; i++)
                //{
                //    while (r[i] < 0)
                //        r[i] += 26;
                //}

                //trans

                List<int> inv = new List<int>();
                for (int i = 0; i < keycount; i++)
                {

                    inv.Add(mat[0]);
                    inv.Add(mat[3]);
                    inv.Add(mat[6]);
                    inv.Add(mat[1]);
                    inv.Add(mat[4]);
                    inv.Add(mat[7]);
                    inv.Add(mat[2]);
                    inv.Add(mat[5]);
                    inv.Add(mat[8]);


                }
                for (int i = 0; i < ciphercount; i += 3)
                {
                    for (int j = 0; j < keycount; j += 3)
                    {
                        equvnum = (cipherText[i] * inv[j] + cipherText[i + 1] * inv[j + 1] + cipherText[i + 2] * inv[j + 2]) % 26;
                        ciphertex.Add(equvnum);
                    }
                }
                return ciphertex;
            }
            //throw new NotImplementedException();
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int keycount = key.Count, plaincount = plainText.Count, equvnum = 0, sumation = 0;
            var ciphertex = new List<int>();
            if (keycount == 4)
            {
                for (int i = 0; i < plaincount; i += 2)
                {
                    for (int j = 0; j < keycount; j += 2)
                    {
                        equvnum = (plainText[i] * key[j] + plainText[i + 1] * key[j + 1]) % 26;
                        ciphertex.Add(equvnum);
                    }
                }
                return ciphertex;
            }
            else
            {
                for (int i = 0; i < plaincount; i += 3)
                {
                    for (int j = 0; j < keycount; j += 3)
                    {
                        equvnum = (plainText[i] * key[j] + plainText[i + 1] * key[j + 1] + plainText[i + 2] * key[j + 2]) % 26;
                        ciphertex.Add(equvnum);
                    }
                }
                return ciphertex;
            }
            //throw new NotImplementedException();
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new InvalidAnlysisException();
            //throw new NotImplementedException();
        }

    }
}
