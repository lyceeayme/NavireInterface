using NavireHeritage.ClassesTechniques;
using NavireHeritage.ClassesMetier;
using System;

namespace NavireHeritage
{
    internal class Program
    {
        static void Main()
        {
            Port port = new Port("Marseille", "43.2976N", "5.3471E", 4, 3, 2, 4);
            //Tests.TestInit();
            //Tests.chargementInitial();

            //Tests.TestEnregistrerArrivee(port, "IMO9241061");
            //Tests.TestEnregistrerArriveeV2();
            //Tests.TestEnregistrerTropTanker();
            Tests.TestDistance();

            Console.ReadKey();
        }
    }
}
