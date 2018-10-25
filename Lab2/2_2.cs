using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{
                public static char ComplRule(char symb)
        {
            if (symb == 'A')
                return 'T';
            if (symb == 'T')
                return 'A';
            if (symb == 'C')
                return 'G';
            if (symb == 'G')
                return 'C';
            else
            {
                Console.WriteLine("Not found!");
                return ' ';
            }
        }
        public static string TransRNA(string pat)
        {
            string res = "";
            foreach (var a in pat)
            {
                if (a == 'T')
                    res += 'U';
                else
                    res += a;
            }
            return res;
        }

        public static string UnTransRNA(string pat)
        {
            string res = "";
            foreach (var a in pat)
            {
                if (a == 'U')
                    res += 'T';
                else
                    res += a;
            }
            return res;
        }
        public static string RevCompl(string pat)
        {
            string revcompl = "";
            foreach(var a in pat)
            {
                revcompl += ComplRule(a);
            }
            return new string(revcompl.ToCharArray().Reverse().ToArray()); ;
        }

        public static Dictionary<string, char> CreateTable()
        {
            Dictionary<string, char> table = new Dictionary<string, char>();
            table.Add("AAA", 'K');
            table.Add("AAC", 'N');
            table.Add("AAG", 'K');
            table.Add("AAU", 'N');
            table.Add("ACA", 'T');
            table.Add("ACC", 'T');
            table.Add("ACG", 'T');
            table.Add("ACU", 'T');
            table.Add("AGA", 'R');
            table.Add("AGC", 'S');
            table.Add("AGG", 'R');
            table.Add("AGU", 'S');
            table.Add("AUA", 'I');
            table.Add("AUC", 'I');
            table.Add("AUG", 'M');
            table.Add("AUU", 'I');
            table.Add("CAA", 'Q');
            table.Add("CAC", 'H');
            table.Add("CAG", 'Q');
            table.Add("CAU", 'H');
            table.Add("CCA", 'P');
            table.Add("CCC", 'P');
            table.Add("CCG", 'P');
            table.Add("CCU", 'P');
            table.Add("CGA", 'R');
            table.Add("CGC", 'R');
            table.Add("CGG", 'R');
            table.Add("CGU", 'R');
            table.Add("CUA", 'L');
            table.Add("CUC", 'L');
            table.Add("CUG", 'L');
            table.Add("CUU", 'L');
            table.Add("GAA", 'E');
            table.Add("GAC", 'D');
            table.Add("GAG", 'E');
            table.Add("GAU", 'D');
            table.Add("GCA", 'A');
            table.Add("GCC", 'A');
            table.Add("GCG", 'A');
            table.Add("GCU", 'A');
            table.Add("GGA", 'G');
            table.Add("GGC", 'G');
            table.Add("GGG", 'G');
            table.Add("GGU", 'G');
            table.Add("GUA", 'V');
            table.Add("GUC", 'V');
            table.Add("GUG", 'V');
            table.Add("GUU", 'V');
            table.Add("UAA", ' ');
            table.Add("UAC", 'Y');
            table.Add("UAG", ' ');
            table.Add("UAU", 'Y');
            table.Add("UCA", 'S');
            table.Add("UCC", 'S');
            table.Add("UCG", 'S');
            table.Add("UCU", 'S');
            table.Add("UGA", ' ');
            table.Add("UGC", 'C');
            table.Add("UGG",'W');
            table.Add("UGU", 'C');
            table.Add("UUA", 'L');
            table.Add("UUC", 'F');
            table.Add("UUG", 'L');
            table.Add("UUU", 'F');
            return table;
        }

        public static Dictionary<string, char> CreateTabPepRNA(Dictionary<string, char> table1)
        {
            Dictionary<string, char> table2 = new Dictionary<string, char>();
            foreach (var k in table1)
            {
                table2[Convert.ToString(k.Value)] = Convert.ToChar(k.Key);
            }
            return table2;
        }
        public static string TranslateRNAToAminoKislot(string pat)
        {
            Dictionary<string, char> table = CreateTable();
            string pepti = "";
            for (int i= 0; i< pat.Length - 2; i+= 3)
            {
                pepti += table[pat.Substring(i,3)];
            }
            return pepti;
        }
        public static string FindInDNA(string dna, string pepti)
        {
            int lenOfPept = pepti.Length * 3;
            int i = 0;
            List<string> res = new List<string>();
            while (i < dna.Length - lenOfPept + 1)
            {
                string str = dna.Substring(i, lenOfPept);
                if (TranslateRNAToAminoKislot(TransRNA(str)) == pepti)
                    res.Add(str);
                i += 1;
            }
            dna = RevCompl(dna);
            int j = 0;
            while (j< dna.Length - lenOfPept + 1)
            {
                string str = dna.Substring(j, lenOfPept);
                if (TranslateRNAToAminoKislot(TransRNA(str)) == pepti)
                    res.Add(RevCompl(str));
                j += 1;
            }
            string words="";
            foreach (var f in res)
                words += f + " ";
            return words;

        }
   
        static void Main(string[] args)
        {
            string OurDna = Console.ReadLine();
            string OurPeptide = Console.ReadLine();
            string OurRes = FindInDNA(OurDna, OurPeptide);
            Console.WriteLine(OurRes);
        }
}