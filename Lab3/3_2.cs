using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        public static Dictionary<char, int> CreateTable()
        {
            Dictionary<char, int> table = new Dictionary<char, int>();
            table.Add('G', 57);
            table.Add('A', 71);
            table.Add('S', 87);
            table.Add('P', 97);
            table.Add('V', 99);
            table.Add('T', 101);
            table.Add('C', 103);
            table.Add('I', 113);
            table.Add('L', 113);
            table.Add('N', 114);
            table.Add('D', 115);
            table.Add('K', 128);
            table.Add('Q', 128);
            table.Add('E', 129);
            table.Add('M', 131);
            table.Add('H', 137);
            table.Add('F', 147);
            table.Add('R', 156);
            table.Add('Y', 163);
            table.Add('W', 186);
            return table;
        }

        public static List<int> FindCyclospectrum(string pept)
        {
            Dictionary<char, int> TAmino = CreateTable();
            List<int> res = new List<int>();
            int i = 0;
            int n = 1;
            string strstr = "";

            while (n < pept.Length)
            {
                while (i < pept.Length)
                {
                    int tmp = 0;
                    int j = 0;
                    while (j < n)
                    {
                        tmp += TAmino[pept[(i + j) % pept.Length]];
                        j += 1;
                    }
                    res.Add(tmp);
                    i += 1;
                }
                i = 0;
                n += 1;
            }
            if (pept.Length != 0)
            {
                int tmp = 0;
                foreach (var a in pept)
                {
                    tmp += TAmino[a];
                }
                res.Add(tmp);
                res.Add(0);
            }
            res.Sort();
            return res;
        }
        
        public static int GetScore(string pept, List<int> spectr)
        {
            List <int> tSpectr = FindCyclospectrum(pept);
            List<int> eSpectr = new List<int>();
            foreach(char ch in spectr)
            {
                eSpectr.Add(ch);
            }
            int score = 0;
            int i = 0;
            int j = 0;
            while ((i < tSpectr.Count)&&(j< eSpectr.Count))
            {
                if (tSpectr[i] == eSpectr[j])
                {
                    i += 1;
                    j += 1;
                    score += 1;
                }
                else if (tSpectr[i] < eSpectr[j])
                {
                    i += 1;
                }

                else
                {
                    j += 1;
                }
            }

            return score;

        }

        static void Main(string[] args)
        {
            string pept_ = Console.ReadLine();

            List <int> spectr_ = new List<int>(1000);
            var line = Console.ReadLine();
            line += '\0';
            string num = " ";
            foreach (var ch in line)
            {
                num += ch;
                if (ch == ' ' || ch == '\0')
                {
                    spectr_.Add(Int32.Parse(num));
                    num = " ";
                    continue;
                }
            }
                
            
            int sc_res = GetScore(pept_, spectr_);
            Console.WriteLine(sc_res);
        }
    }
}
