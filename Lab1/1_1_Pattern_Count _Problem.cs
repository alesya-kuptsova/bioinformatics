using System;

public class MainClass
{
    public static void Main()
    {
         string pat = Console.ReadLine();
            string genom = Console.ReadLine();
            int genom_count = 0;
            if (pat.Length <= genom.Length)
            {
                for (int i = 0; i < genom.Length - pat.Length + 1; ++i)
                {
                    if (pat == genom.Substring(i,  pat.Length))
                    {
                        genom_count++;
                    }
                }
            }
            Console.WriteLine(genom_count);        
    }
}