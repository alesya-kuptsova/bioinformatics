using System;

public class MainClass
{
        public static UInt64 NumPeptWithGivenMass(int elemMass)
        {
            int[] aminKislotamassi = new int[18] { 57, 71, 87, 97, 99, 101, 103, 113, 114, 115, 128, 129, 131, 137, 147, 156, 163, 186 };
            UInt64[] mass = new UInt64[elemMass + 1];
            for(int i=0; i<mass.Length; i++)
            {
                mass[i] = 0;
            }
            mass[0] = 1;
            for (int j=0; j<elemMass+1; j++)
            {
                foreach(var k in aminKislotamassi)
                {
                    if (j >= k)
                        mass[j] += mass[j - k];
                }
            }
            return mass[elemMass];
        }
        
        static void Main(string[] args)
        {
            int elemMass_ = Convert.ToInt32(Console.ReadLine());
            UInt64 result = NumPeptWithGivenMass(elemMass_);
            Console.WriteLine(result);
        }
}