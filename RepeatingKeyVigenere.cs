using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {

            string s, x, ans = "",now="";
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();
            s = plainText;
            x = cipherText;
            
            
            for (int i = 0; i < s.Length; i++)
            {
                int tmp1 = s[i] - 'a';
                int tmp2 = x[i] - 'a';
                int tmp3 = ((tmp2 - tmp1) + 26);
                tmp3 %= 26;
                ans += Convert.ToChar(tmp3 + 'a');
            }
            now = now + ans[0];
            for (int i = 1; i < ans.Length; i++)
            {
                if (cipherText.Equals(Encrypt(plainText, now)))
                {
                    return now;
                }
                now = now + ans[i];
            }
            return ans;


            //throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            string s, x, z, ans = "";
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            s = key;
            x = cipherText;
            int idx = 0;
            z = s;
            while (s.Length < x.Length)
            {
                s += z[idx];
                idx = (idx + 1) % z.Length;
            }
            for (int i = 0; i < s.Length; i++)
            {
                int tmp1 = s[i] - 'a';
                int tmp2 = x[i] - 'a';
                int tmp3 = ((tmp2 - tmp1)+26);
                tmp3 %= 26;
                ans += Convert.ToChar(tmp3 + 'a');
              
            }
            return ans;

            //throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            string s, x, z, ans = "";
            plainText = plainText.ToLower();
            key = key.ToLower();
            s = key;
            x = plainText;
            int idx = 0;
            z = s;

            while (s.Length < x.Length)
            {
                s += z[idx];
                idx = (idx + 1) % z.Length;
            }
            for (int i = 0; i < s.Length; i++)
            {
                int tmp1 = s[i] - 'a';
                int tmp2 = x[i] - 'a';
                int tmp3 = (tmp1 + tmp2);
                tmp3 %= 26;
                ans += Convert.ToChar(tmp3 + 'a');
            }
            return ans;


            //throw new NotImplementedException();
        }
    }
}