using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Program
    {
        public static Dictionary<char, int> CreateTableAminoMass()
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

        public static Dictionary<int, char> CreateTableMassAminoAcid()
        {
            Dictionary<int, char> table = new Dictionary<int, char>();
            table.Add(57, 'G');
            table.Add(71, 'A');
            table.Add(87, 'S');
            table.Add(97, 'P');
            table.Add(99, 'V');
            table.Add(101, 'T');
            table.Add(103, 'C');
            table.Add(113, 'I');
            table.Add(114, 'N');
            table.Add(115, 'D');
            table.Add(128, 'K');
            table.Add(129, 'E');
            table.Add(131, 'M');
            table.Add(137, 'H');
            table.Add(147, 'F');
            table.Add(156, 'R');
            table.Add(163, 'Y');
            table.Add(186, 'W');
            return table;
        }

        public static Dictionary<char, int> tableAminoMass = CreateTableAminoMass();
        public static Dictionary<int, char> tableMassAmino = CreateTableMassAminoAcid();
      
        public static List<int> Cyclespectrum(string peptide)
        {
            List<int> res = new List<int>();
            int i = 0, k = 1;
            while (k < peptide.Length)
            {
                while (i < peptide.Length)
                {
                    int j = 0, tmp = 0;
                    while (j < k)
                    {
                        tmp += tableAminoMass[peptide[(i + j) % peptide.Length]];
                        j += 1;
                    }
                    res.Add(tmp);
                    i += 1;
                }
                i = 0;
                k += 1;
            }
            if (peptide.Length != 0)
            {
                int tmp = 0;
                foreach(var c in peptide)
                {
                    tmp += tableAminoMass[c];
                }
                res.Add(tmp);
                res.Add(0);
            }
            res.Sort();
            return res;
        }

        public static List<int> Linearspectrum(string peptide)
        {
            List<int> res = new List<int>();
            int i = 0, k = 1;
            while (k < peptide.Length)
            {
                while (i < peptide.Length - k + 1)
                {
                    int j = 0, tmp = 0;
                    while (j < k)
                    {
                        tmp += tableAminoMass[peptide[i + j]];
                        j += 1;
                    }
                    res.Add(tmp);
                    i += 1;
                }
                i = 0;
                k += 1;
            }
            if (peptide.Length != 0)
            {
                int tmp = 0;
                foreach (var c in peptide)
                {
                    tmp += tableAminoMass[c];
                }
                res.Add(tmp);
                res.Add(0);
            }
            res.Sort();
            return res;
        }

        public static int GetPeptideMass(string peptide)
        {
            int mass = 0;
            foreach (var c in peptide)
            {
                mass += tableAminoMass[c];
            }
            return mass;
        }

        public static int ParentMass(List<int> spectrum)
        {
            return spectrum[spectrum.Count - 1];
        }

        public static bool IsConsistent(string peptide, List<int> spectrum)
        {
            foreach(var p in Linearspectrum(peptide))
            {
                if (!spectrum.Contains(p))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<string> Expand(List<string> peptides, List<string> aminoacids)
        {
            List<string> res = new List<string>();
            if (peptides.Count == 0)
            {
                foreach (var c in aminoacids)
                {
                    
                    res.Add(c);
                }
            }
            else
            {
                foreach (var i in peptides)
                {
                    foreach (var j in aminoacids)
                    {
                        res.Add(i + j);
                    }
                }
            }
            return res;
        }

        public static List<string> CyclopeptideSequencing(string spectre_str)
        {
            List<string> res = new List<string>();
            List<int> spectr = new List<int>();
            spectre_str += '\0';
            string num = " ";
            foreach (var ch in spectre_str)
            {
                num += ch;
                if (ch == ' ' || ch == '\0')
                {
                    spectr.Add(Int32.Parse(num));
                    num = " ";
                    continue;
                }
            }
            List<string> aminoacids = new List<string>() { "A", "C", "D", "E", "F", "G", "H", "I", "K", "M", "N", "P", "R", "S", "T", "V", "W", "Y" };
            bool flag = true;
            List<string> Peptides = new List<string>();
            while (flag || Peptides.Count != 0)
            {
                flag = false;
                List<string> remove_peptides = new List<string>();
                Peptides = Expand(Peptides, aminoacids);
                foreach (var p in Peptides)
                {
                    if (GetPeptideMass(p) == ParentMass(spectr))
                    {
                        List<int> csd = Cyclespectrum(p);
                        if (csd.SequenceEqual(spectr)) 
                        {
                            res.Add(p);
                        }
                        remove_peptides.Add(p);
                    } else
                    {
                        if (!IsConsistent(p,spectr))
                        {
                            remove_peptides.Add(p);
                        }
                    }
                }
                foreach (var p in remove_peptides)
                {
                    Peptides.Remove(p);
                }
            }
            return res;
        }

        public static string ConverPeptidesToMasses(List<string> peptides)
        {
            peptides.Sort();
            string res_str = "";
            foreach(var pep in peptides)
            {
                string tmp = "";
                foreach (var i in pep)
                {
                    tmp += Convert.ToString(tableAminoMass[i]) + '-';
                }
                string blya = "";
                for(var a = 0; a < tmp.Length - 1; ++a)
                {
                    blya += tmp[a];
                }
                res_str += blya + ' ';
            }
            return res_str;
        }

        static void Main(string[] args)
        {
            string sp = Console.ReadLine();
            List<string> list = CyclopeptideSequencing(sp);
            Console.WriteLine(ConverPeptidesToMasses(list));
            
        }
    }
}
