using System;

public class MainClass
{
        public static char ComplRule(char symbol)
        {
            if (symbol == 'A') { return 'T'; }
            else if (symbol == 'T') { return 'A'; }
            else if (symbol == 'C') { return 'G'; }
            else if (symbol == 'G') { return 'C'; }
            else { Console.WriteLine("Not found!"); return ' '; }
        }

        public static string ReverseCompl(string pat)
        {
            string rev_comp = System.String.Empty;
            foreach(var a in pat)
            {
                rev_comp += ComplRule(a);
            }
            char[] charArray = rev_comp.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static void Main()
        {
            string pattern = Console.ReadLine();
            string rcpat = ReverseCompl(pattern);
            Console.WriteLine(rcpat);
        }
}