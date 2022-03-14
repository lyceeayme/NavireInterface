using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavireHeritage.ClassesMetier
{
    public class Port
    {
        private readonly string nom;
        private readonly string latitude;
        private readonly string longitude;
        private int nbPortique;
        private int nbQuaisPassager;
        private int nbQuaisTanker;
        private int nbQuaisSuperTanker;
        private Dictionary<string, Navire> navireAttendus;
        private Dictionary<string, Navire> navireArrives;
        private Dictionary<string, Navire> navirePartis;
        private Dictionary<string, Navire> navireEnAttente;

        /// <summary>
        /// Gets le nom du port.
        /// </summary>
        public string Nom => nom;

        /// <summary>
        /// Gets la latitude du port.
        /// </summary>
        public string Latitude => latitude;

        /// <summary>
        /// Gets la longitude du port.
        /// </summary>
        public string Longitude => longitude;

        /// <summary>
        /// Gets or set le nombre de Portique du port.
        /// </summary>
        public int NbPortique { get => nbPortique; set => nbPortique = value; }

        /// <summary>
        /// Gets or sets le nombre de quais passager du port.
        /// </summary>
        public int NbQuaisPassager { get => nbQuaisPassager; set => nbQuaisPassager = value; }

        /// <summary>
        /// Gets or set le nombre de quais de tanker du port.
        /// </summary>
        public int NbQuaisTanker { get => nbQuaisTanker; set => nbQuaisTanker = value; }

        /// <summary>
        /// Gets or set le nombre de super tanker du port.
        /// </summary>
        public int NbQuaisSuperTanker { get => nbQuaisSuperTanker; set => nbQuaisSuperTanker = value; }
    }
}
