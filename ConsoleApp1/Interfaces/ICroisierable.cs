using System.Collections.Generic;
using NavireHeritage.ClassesMetier;

namespace Station.Interfaces
{
    interface ICroisierable
    {
        void embarquer(List<Passager> passager);

        void debarquer(List<Passager> passager);
    }
}
