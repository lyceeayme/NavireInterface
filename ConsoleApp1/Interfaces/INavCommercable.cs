using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Interfaces
{
    interface INavCommercable
    {
        void decharger(int quantite);

        void charger(int quantite);
    }
}
