using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman 
    {
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            // throw new NotImplementedException();
            List<int> ans = new List<int>();
            int  pa=1, pb=1, aa=1, ab=1;
            for (int i = 0; i < xa; i++)
            {
                pa = (pa * alpha) % q;
            }
            for (int i = 0; i < xb; i++)
            {
                pb = (pb * alpha) % q;
            }
            for (int i = 0; i <xb; i++)
            {
                aa = (aa * pa) % q;
            }
            for (int i = 0; i < xa; i++)
            {
                ab = (ab * pb) % q;
            }
            ans.Add(aa);
            ans.Add(ab);
            return ans;
        }
    }
}
