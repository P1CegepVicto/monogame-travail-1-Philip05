using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenformatif3

{
    class Program
    {
        static bool[] moves = new bool[100];
        static int cpt2 = 1;
        static int cptaff10 =0;
       

        public static void Main(string[] args)
        {
            bool[] moves = new bool[104];
            Random de1 = new Random();
            int nbr;
            int cptfaux = 0;
            int cptessais = 0;





             

            for (int i = 1; i < moves.Length; i++)
            {
                nbr = de1.Next(0, 2);
                if (nbr == 0)
                {
                    moves[i] = true;
                }
                if (nbr == 1)
                {
                    moves[i] = false;
                }
             moves[0] = true;
             moves[99] = true;
            }


            while (cpt2 != 0)
            {

                Console.WriteLine("Touche");
                ConsoleKeyInfo info = Console.ReadKey();
                Console.ReadLine();
                Console.WriteLine();


                if (info.Key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }

                if (info.Key == ConsoleKey.A)
                {
                    cpt2 -= 3;
                    if (cpt2 < 0)
                    {
                        Console.WriteLine("Nombre inférieur a la capacité du tableau. !!!Game Over!!!");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    Console.WriteLine("La case d'arrivée , soit la case " + cpt2 + " contient un " + moves[cpt2]);
                    if (moves[cpt2] == false)
                    {
                        cptfaux++;
                    }
                    if (moves[cpt2] == true)
                    {
                        cptfaux = 0;
                    }
                    cptessais++;
                }
                if (info.Key == ConsoleKey.S)
                {
                    if (cpt2 < 0)
                    {
                        Console.WriteLine("Nombre inférieur a la capacité du tableau. !!!Game Over!!!");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    cpt2 -= 2;
                    Console.WriteLine("La case d'arrivée , soit la case " + cpt2 + " contient un " + moves[cpt2]);
                    if (moves[cpt2] == false)
                    {
                        cptfaux++;
                    }
                    if (moves[cpt2] == true)
                    {
                        cptfaux = 0;
                    }
                    cptessais++;
                }
                if (info.Key == ConsoleKey.D)
                {
                    if (cpt2 < 0)
                    {
                        Console.WriteLine("Nombre inférieur a la capacité du tableau. !!!Game Over!!!");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    cpt2 -= 1;
                    Console.WriteLine("La case d'arrivée , soit la case " + cpt2 + " contient un " + moves[cpt2]);
                    if (moves[cpt2] == false)
                    {
                        cptfaux++;
                    }
                    if (moves[cpt2] == true)
                    {
                        cptfaux = 0;
                    }
                    cptessais++;
                }
                if (info.Key == ConsoleKey.G)
                {
                    if (cpt2 < 0)
                    {
                        Console.WriteLine("Nombre inférieur a la capacité du tableau. !!!Game Over!!!");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    cpt2 += 2;
                    Console.WriteLine("La case d'arrivée , soit la case " + cpt2 + " contient un " + moves[cpt2]);
                    if (moves[cpt2] == false)
                    {
                        cptfaux++;
                    }
                    if (moves[cpt2] == true)
                    {
                        cptfaux = 0;
                    }
                    cptessais++;
                }
                if (info.Key == ConsoleKey.H)
                {
                    if (cpt2 < 0)
                    {
                        Console.WriteLine("Nombre inférieur a la capacité du tableau. !!!Game Over!!!");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    cpt2 += 4;
                    Console.WriteLine("La case d'arrivée , soit la case " + cpt2 + " contient un " + moves[cpt2]);
                    if (moves[cpt2] == false)
                    {
                        cptfaux++;
                    }
                    if (moves[cpt2] == true)
                    {
                        cptfaux = 0;
                    }
                    
                }
                if (cptfaux == 4 || cpt2 < 0)
                {
                    Console.WriteLine("!!!Game Over!!!.");
                    cpt2 = 0;
                }
                if (cpt2 < 0)
                {
                    Console.WriteLine("Nombre inférieur a la capacité du tableau. !!!Game Over!!!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                cptessais++;
                if (info.Key == ConsoleKey.Y)
                {
                    AffichageEntier();
                }
                if (info.Key == ConsoleKey.P)
                {
                    Affichage10();
                }
            
            if (cpt2 >= 99)
            {
                Console.WriteLine("Vous avex tenté  " + cptessais + " fois pour passer à travers du tableau, mais malgré tout, vous avez réussi!");
                    
            }
        }


            Console.ReadLine();

        }

        public static void AffichageEntier()
        {
            for (int y = 0; y < moves.Length; y++)
            {
                Console.WriteLine(moves[y]);
            }
        }

        public static void Affichage10()
        {
            for (int y = 0; y <= cpt2+10; y++)
            {
                if (cptaff10 == 0)
                {
                    y = cpt2;
                }
                cptaff10++;
                Console.WriteLine(moves[y]);
            }
        }
    }
}
            
            
            
 

            



       

