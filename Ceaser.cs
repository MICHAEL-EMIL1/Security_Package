using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        int keyy;
        string x;
        public string Encrypt(string plainText, int key)
        {
            string plainn = plainText.ToLower();
            for (int i = 0; i < plainText.Length; i++)
            {
                x += Convert.ToChar(((Convert.ToInt32(plainn[i] - 97) + key) % 26) + 97);
            }
            return x;
        }
        public string Decrypt(string cipherText, int key)
        {
            //throw new NotImplementedException();
            return Encrypt(cipherText, 26 - key);
        }
        public int Analyse(string plainText, string cipherText)
        {
            //throw new NotImplementedException();
            string plainn = plainText.ToLower();
            string cipherr = cipherText.ToLower();
            keyy = cipherr[0] - plainn[0];
            while (true)
            {
                if (keyy < 0)
                {
                    keyy += 26;
                }
                else
                {
                    return keyy;
                }
            }
        }
    }
}