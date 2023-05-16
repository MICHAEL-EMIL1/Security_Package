using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();
            char[] alphapet = new char[26];
            for (int i = 0; i < 26; i++)
            {
                alphapet[i] = Convert.ToChar(i + 97);
            }

            char[] cipher = cipherText.ToCharArray();
            char[] plain = plainText.ToCharArray();
            char[] answer = new char[26];

            for (int i = 0; i < plain.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (plain[i] == alphapet[j] && answer[j] == '\0')
                    {
                        answer[j] = cipher[i];
                        break;
                    }
                    //break;
                }
            }

            int[] a = new int[26];
            //char[] s = new char[26];
            for (int i = 0; i < 26; i++)
            {
                if (answer[i] >= 'a' && answer[i] <= 'z')
                {
                    a[answer[i] - 'a']++;
                }
            }
            for (int i = 0; i < 26; i++)
            {
                if (answer[i] == '\0')
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (a[j] == 0)
                        {
                            answer[i] = Convert.ToChar(j + 'a');
                            a[j]++;
                            break;
                        }
                    }
                }
                //   ans += answer[i];
            }
            string str = new string(answer);
            return str;

            //throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            char[] alphapet = new char[50];
            for (int i = 0; i < 26; i++)
            {
                alphapet[i] = Convert.ToChar(i + 97);
            }

            char[] cipher = cipherText.ToCharArray();
            char[] keys = key.ToCharArray();
            char[] answer = new char[50000];

            for (int i = 0; i < cipher.Length; i++)
            {
                for (int j = 0; j < keys.Length; j++)
                {
                    if (cipher[i] == keys[j])
                    {
                        answer[i] = alphapet[j];
                        break;
                    }

                }
            }
            string str = new string(answer);
            
            return str;



            //throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
           plainText.ToLower();
           key.ToLower();
            char[] alphapet = new char[50];
            for (int i = 0; i < 26; i++)
            {
                alphapet[i] = Convert.ToChar(i + 97);
            }

            char[] plain = plainText.ToCharArray();
            char[] keys = key.ToCharArray();
            char[] answer = new char[50000];
            for (int i = 0; i < plain.Length; i++)
            {
                for (int j = 0; j < 26; j++ )
                {
                    if (plain[i] == alphapet[j])
                    {
                        answer[i] = keys[j];
                        break; 
                    }
                }
            }
            string str = new string(answer);
            return str;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            string Frequency_Information = "ETAOINSRHLDCUMFPGWYBVKXJQZ".ToLower();
            Dictionary<char, int> freq = new Dictionary<char, int>();
            SortedDictionary<char, char> frq = new SortedDictionary<char, char>();
            cipher = cipher.ToLower();
            int tmp = cipher.Length;
            string ans = "";
            for (int i = 0; i < tmp; i++)
            {
                if (!freq.ContainsKey(cipher[i]))
                {
                    freq.Add(cipher[i], 0);
                }
                else
                {
                    freq[cipher[i]]++;
                }
            }

            freq = freq.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            freq = freq.Reverse().ToDictionary(x => x.Key, x => x.Value);
            int ctr = 0;
            foreach (var item in freq)
            {
                if (!frq.ContainsKey(item.Key))
                {
                    frq.Add(item.Key, Frequency_Information[ctr]);
                    ctr++;
                }
            }

            for (int i = 0; i < tmp; i++)
                ans += frq[cipher[i]];

            return ans;

            //throw new NotImplementedException();
        }
    }
}

