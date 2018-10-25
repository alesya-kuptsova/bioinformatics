using System;
using System.Collections.Generic;

public class MainClass
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


        public static string FindCyclospectrum(string pept)
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
            //List<int> kek = new List<int>();
            //foreach (var k in res)
            //{
            //    kek.Add(k);
            //    kek.Add(' ');
            //}
            string gnirts = "";
            for (int l = 0; l < res.Count; l++)
                gnirts += Convert.ToString(res[l]) + ' ';
            return gnirts;
        }
        static void Main(string[] args)
        {
            string lenpept_ = Console.ReadLine();
            string res_ = FindCyclospectrum(lenpept_);
            Console.WriteLine(res_);
        }
}