using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            string cipherText3 = "";
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            plainText = plainText.Replace(" ", "");
            cipherText = cipherText.Replace("x", "");

            int key = 0;
            while (cipherText != cipherText3)
            {

                key++;




                cipherText3 = "";



                float columns = (float)Math.Ceiling((float)plainText.Length / key);
                char[,] matrix = new char[(int)key, (int)columns];
                string plainText2 = plainText.PadRight((int)(key * columns), 'x');
                int index = 0;

                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < key; j++)
                    {
                        matrix[j, i] = plainText2[index++];
                    }
                }

                for (int i = 0; i < key; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[i, j].ToString();
                        cipherText3 = String.Concat(cipherText3, matrix[i, j]);

                    }
                }


                cipherText3 = cipherText3.Replace("x", "");
            }




            return key;
            //throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, int key)
        {
            cipherText = cipherText.ToLower();
            cipherText = cipherText.Replace(" ", "");
            cipherText = cipherText.Replace("x", "");
            cipherText = cipherText.Replace("X", "");
            string plainText = "";

            int check = (cipherText.Length % key);
            int var = (key - (check));


            float columns = (float)Math.Ceiling((float)cipherText.Length / key);
            char[,] matrix = new char[(int)key, (int)columns];
            string cipherText2 = cipherText.PadRight((int)(key * columns), 'x');
            int index = 0;


            if (check != 0)
            {

                for (int l = var; l > 0; l--)
                {
                    matrix[l, (int)columns - 1] = '#';

                }
            }

            for (int i = 0; i < key; i++)
            {

                for (int j = 0; j < columns; j++)
                {

                    if (check != 0)
                    {
                        if (matrix[i, j] != '#')
                        {
                            matrix[i, j] = cipherText2[index++];
                        }
                    }

                    else
                    {
                        matrix[i, j] = cipherText2[index++];
                    }
                }





            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    plainText = String.Concat(plainText, matrix[j, i]);
                }
            }
            plainText = plainText.Replace("#", "");


            return plainText;
            //throw new NotImplementedException();
        }

        public string Encrypt(string plainText, int key)
        {
            plainText = plainText.ToLower();
            plainText = plainText.Replace(" ", "");
            string cipherText = "";
            float columns = (float)Math.Ceiling((float)plainText.Length / key);
            char[,] matrix = new char[(int)key, (int)columns];
            string plainText2 = plainText.PadRight((int)(key * columns), 'x');
            int index = 0;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    matrix[j, i] = plainText2[index++];
                }
            }

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j].ToString();
                    cipherText = String.Concat(cipherText, matrix[i, j]);

                }
            }
            cipherText = cipherText.Replace("x", "");

            return cipherText;
            //throw new NotImplementedException();
        }
    }
}
