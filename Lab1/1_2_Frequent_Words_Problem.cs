using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class MainClass
{
    public static void Main()
    {
                        Dictionary<string,int> table = new Dictionary<string, int>();
            string genom = Console.ReadLine();
            int pat_len = Convert.ToInt32(Console.ReadLine());
            if (pat_len <= genom.Length)
            {
                for (int i = 0; i < genom.Length - pat_len + 1; ++i)
                {
                    if (table.ContainsKey(genom.Substring(i, pat_len)))
                    {
                        table[genom.Substring(i, pat_len)]++;
                    } else
                    {
                        table.Add(genom.Substring(i, pat_len), 0);
                    }
                    
                }
            }
            int max_value = -1;
            foreach (var a in table)
            {

                if (a.Value >= max_value)
                {
                    max_value = a.Value;
                }
            }

            foreach( var a in table) {
                if (a.Value == max_value)
                {
                    Console.WriteLine(a.Key);
                }
            }
    }
}