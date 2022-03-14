using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionNavire.Exceptions;

namespace NavireHeritage.ClassesMetier
{
    public class Stockage
    {
        private readonly int numero;
        private int capaciteMaxi;
        private int capaciteDispo;

        public Stockage(int numero, int capaciteMaxi, int capaciteDispo)
        {
            this.numero = numero;
            this.capaciteMaxi = capaciteMaxi;
            this.capaciteDispo = capaciteDispo;
        }

        /// <summary>
        /// Gets le numero de l'espace de stockage.
        /// </summary>
        public int Numero => this.numero;

        /// <summary>
        /// Gets or set la capacite maximal de la zone de stockage.
        /// </summary>
        public int CapaciteMaxi { get => capaciteMaxi;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("La capacité maximal de la zone doit être positive ou null");
                }
                this.capaciteMaxi = value;
            }
        }

        /// <summary>
        /// Gets or set la capacite disponible.
        /// </summary>
        public int CapaciteDispo { get => capaciteDispo;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("");
                }
            }
        }
    }
}
