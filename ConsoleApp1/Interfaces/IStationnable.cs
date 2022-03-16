using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavireHeritage.ClassesMetier;

namespace Station.Interfaces
{
    interface IStationnable
    {
        void enregistrerArriveePrevue(Navire navire);

        void enregistrerArrivee(string id);

        void enregistrerDepart(string id);

        bool estAttendu(string id);

        bool estParti(string id);

        bool estPresent(string id);

        Navire getUnAttendu(string id);

        Navire getUnArrive(string id);

        Navire getUnParti(string id);
    }
}
