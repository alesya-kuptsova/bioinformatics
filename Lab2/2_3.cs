using System;

public class MainClass
{
        public static UInt32 FindNumOfSubpept(string lenpept)
        {
            UInt32 res = Convert.ToUInt32(lenpept) * (Convert.ToUInt32(lenpept) - 1);
              return res;
        }
  


        static void Main(string[] args)
        {
            string lenpept_ = Console.ReadLine();
            UInt32 res = FindNumOfSubpept(lenpept_);
            Console.WriteLine(res);


        }
}