using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            plainText = new string(plainText.Where(char.IsLetter).Select(char.ToUpper).ToArray());
            cipherText = new string(cipherText.Where(char.IsLetter).Select(char.ToUpper).ToArray());

            int offset = 0;
            string key = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                offset = cipherText[i] - plainText[i];
                if (offset < 0)
                {
                    offset += 26;
                }
                key += (char)('A' + offset);
            }

            int count = 0;
            int l = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = count; j < key.Length; j++)
                {
                    if (plainText[i] == key[j])
                    {
                        count = j;
                        l++;
                        break;
                    }
                }
            }
            string result = "";
            if (l == 3)
            {
                result = key.Substring(0, count - 2);
            }


            // Console.WriteLine(result);
            return result.ToLower();
        }

        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            /*********************************************/
            char[,] vigenereTableau = new char[26, 26];
            // Fill the tableau with the alphabet shifted by each letter
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    vigenereTableau[i, j] = (char)('a' + (i + j) % 26);
                }
            }
            /*********************************************/
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            int length = cipherText.Length - key.Length;

            for (int i = 0; i < cipherText.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (vigenereTableau[j, alphabet.IndexOf(key[i])] == cipherText[i])
                    {
                        if (length > 0)
                        {
                            key += vigenereTableau[j, 0];
                            length--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            string text = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (vigenereTableau[j, alphabet.IndexOf(key[i])] == cipherText[i])
                    {
                        text += vigenereTableau[j, 0];
                    }
                }
            }
            return text;
        }

        public string Encrypt(string plainText, string key)
        {
            plainText = plainText.ToLower();
            key = key.ToLower();
            //Console.WriteLine(plainText.Length);
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            int appendx = plainText.Length - key.Length;
            string keyStream = "";
            String text = "";
            for (int i = 0; i < appendx; i++)
            {
                text += plainText[i];
            }
            keyStream = key + text;
            //Console.WriteLine(keyStream);


            /*********************************************************/

            char[,] vigenereTableau = new char[26, 26];
            // Fill the tableau with the alphabet shifted by each letter
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    vigenereTableau[i, j] = (char)('a' + (i + j) % 26);
                }
            }

            /*********************************************************/

            String cipherText = "";
            int row = 0;
            int col = 0;
            for (int i = 0; i < keyStream.Length; i++)
            {
                row = alphabet.IndexOf(plainText[i]);
                col = alphabet.IndexOf(keyStream[i]);

                cipherText += vigenereTableau[row, col];
            }

            /*********************************************************/

            return cipherText;
        }
    }
}
