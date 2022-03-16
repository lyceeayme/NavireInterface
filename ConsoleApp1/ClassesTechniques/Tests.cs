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

        public static void TestEnregistrerArrivee(Port port, string imo)
        {
            try
            {
                port.enregistrerArriveee(imo);
                Console.WriteLine("navire " + imo + " arrivé");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void TestEnregistrerArriveeV2()
        {
            try
            {
                // PORT
                Port port = new Port("Marseille", "43.2976N", "5.3471E", 4, 3, 2, 4);

                // NAVIRES
                port.enregistrerArriveePrevue(new Cargo("IMO9780859", "CMA CGM A. LINCOLN", "43.43279N", "134.76258W", 140872, 148992, 123000, "marchandises diverses"));
                port.enregistrerArriveePrevue(new Cargo("IMO9250098", "CONTAINERSHIPS VII", "54.35412N", "5.3644", 10499, 56000, 60000, "Porte-conteneurs"));
                port.enregistrerArriveePrevue(new Tanker("IMO9250097", "Super Tanker", "54.35412N", "5.3644", 140000, 150000, 0, "Hydrocarbure"));
                port.enregistrerArriveePrevue(new Tanker("IMO9250096", "Tanker", "54.35412N", "5.3644", 110000, 110000, 0, "Petrole"));

                // ENREGISTREMENT
                string imo = "IMO9780859";
                port.enregistrerArriveee(imo);
                imo = "IMO9250097";
                port.enregistrerArriveee(imo);
                imo = "IMO9250096";
                port.enregistrerArriveee(imo);

                Console.WriteLine(port);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void TestEnregistrerTropTanker()
        {
            try
            {
                // PORT
                Port port = new Port("Marseille", "43.2976N", "5.3471E", 4, 3, 2, 4);
                // NAVIRES
                port.enregistrerArriveePrevue(new Tanker("IMO9250096", "Tanker1", "54.35412N", "5.3644", 110000, 110000, 0, "Petrole"));
                port.enregistrerArriveePrevue(new Tanker("IMO9250095", "Tanker2", "54.35412N", "5.3644", 110000, 110000, 0, "Petrole"));
                port.enregistrerArriveePrevue(new Tanker("IMO9250094", "Tanker3", "54.35412N", "5.3644", 110000, 110000, 0, "Petrole"));
                // ENREGISTREMENT
                port.enregistrerArriveee("IMO9250096");
                port.enregistrerArriveee("IMO9250095");
                port.enregistrerArriveee("IMO9250094");

                Console.WriteLine(port);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
