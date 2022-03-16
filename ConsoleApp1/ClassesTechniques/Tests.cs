using System;
using NavireHeritage.ClassesMetier;
using GestionNavire.Exceptions;

namespace NavireHeritage.ClassesTechniques
{
    internal abstract class Tests
    {
        public static void TestInit()
        {
            Port port = new Port("Marseille", "43.2976N", "5.3471E", 4, 3, 2, 4);
            Console.WriteLine(port);
        }

        public static void chargementInitial()
        {
            Port port = new Port("Marseille", "43.2976N", "5.3471E", 4, 3, 2, 4);
            try
            {
                port.enregistrerArriveePrevue(new Cargo("IMO9780859", "CMA CGM A. LINCOLN", "43.43279N", "134.76258W", 140872, 148992, 123000, "marchandises diverses"));
                port.enregistrerArriveePrevue(new Cargo("IMO9250098", "CONTAINERSHIPS VII", "54.35412N", "5.3644", 10499, 56000, 60000, "Porte-conteneurs"));
                Console.WriteLine(port);

            }
            catch (GestionPortException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
