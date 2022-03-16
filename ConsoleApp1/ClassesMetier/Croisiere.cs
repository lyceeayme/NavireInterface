using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GestionNavire.Exceptions;

namespace NavireHeritage.ClassesMetier
{
    class Croisiere : Navire
    {
        private readonly string typeNavireCroisiere;
        private readonly int nbPassagersMaxi;
        private readonly string typeFret;
        private Dictionary<string, Passager> Passagers;

        public Croisiere(string imo, string nom, string latitude, string longitude, string typeNavireCroisiere, int nbPassagersMaxi)
            :base(imo, nom, latitude, longitude, 0, 0, 0)
        {
            if (Regex.IsMatch(typeNavireCroisiere, @"V|M"))
            {
                this.typeNavireCroisiere = typeNavireCroisiere;
            }
            else
            {
                throw new Exception("Le type de navire de croisière n'est pas correct.");
            }
            if (nbPassagersMaxi <= 0)
            {
                throw new GestionPortException("Le nombre de passager maxi doit être positif");
            }
            this.nbPassagersMaxi = nbPassagersMaxi;
            this.typeFret = "Croisiere";
            this.Passagers = new Dictionary<string, Passager>();
        }

        public Croisiere(string imo, string nom, string latitude, string longitude, string typeNavireCroisiere, int nbPassagersMaxi, List<Passager>pPassagers)
            : this(imo, nom, latitude, longitude, typeNavireCroisiere, nbPassagersMaxi)
        {
            this.embarquer(pPassagers);
        }

        /// <summary>
        /// Gets le type de navire de croisière.
        /// </summary>
        public string TypeNavireCroisiere => typeNavireCroisiere;

        /// <summary>
        /// Gets le nombre de passager maximal.
        /// </summary>
        public int NbPassagersMaxi => nbPassagersMaxi;

        /// <summary>
        /// Gets le type de fret.
        /// </summary>
        public string TypeFret => typeFret;

        public void embarquer(List<Passager> pPassagers)
        {
            if (this.Passagers.Count + pPassagers.Count > this.nbPassagersMaxi)
            {
                throw new Exception("Le nombre de passager a embarqué est trop grand, aucun passager n'est embarqué.");
            }
            foreach (Passager pPassager in pPassagers)
            {
                this.Passagers.Add(pPassager.NumPasseport, pPassager);
            }
        }

        public List<Passager> debarquer(List<Passager> pPassagers)
        {
            List<Passager> inconnu = new List<Passager>();
            foreach (Passager pPassager in pPassagers)
            {
                if (this.Passagers.ContainsKey(pPassager.NumPasseport))
                {
                    this.Passagers.Remove(pPassager.NumPasseport);
                }
                else
                {
                    inconnu.Add(pPassager);
                }
            }
            return inconnu;
        }
        public override string ToString()
        {
            return string.Format(base.ToString() + this.typeFret);
        }

    }
}
