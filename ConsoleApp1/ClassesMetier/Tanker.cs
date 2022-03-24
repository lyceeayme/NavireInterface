using GestionNavire.Exceptions;
using Station.Interfaces;
using System;
using System.Configuration;

namespace NavireHeritage.ClassesMetier
{
    public class Tanker : Navire, INavCommercable
    {
        private static readonly int valueTanker = Convert.ToInt32(ConfigurationManager.AppSettings["ValueLimitTanker"]);
        //private int ValueTanker = 130000;
        private readonly string typeFluide;

        public Tanker(string imo, string nom, string latitude, string longitude, int tonnageActuel, int tonnageDT, int tonnageDWT, string typeFluide)
            :base(imo, nom, latitude, longitude, tonnageActuel, tonnageDT, tonnageDWT)
        {
            this.typeFluide = typeFluide;
        }

        /// <summary>
        /// Gets le type de fluid de la cargaison.
        /// </summary>
        public string TypeFluide => typeFluide;

        public void decharger(int pQuantite)
        {
            if (pQuantite < 0)
            {
                throw new GestionPortException("Impossible de décharger une valeur négative.");
            }
            else
            {
                if (this.tonnageActuel + pQuantite > this.tonnageDT)
                {
                    throw new GestionPortException("Impossible de décharger plus que le tonnage actuel.");
                }
                this.tonnageActuel += pQuantite;
            }
        }

        public void charger(int pQuantite)
        {
            if (pQuantite < 0)
            {
                throw new GestionPortException("Impossible de charger une valeur négative.");
            }
            else
            {
                if (this.tonnageActuel - pQuantite < 0)
                {
                    throw new GestionPortException("Impossible de charger plus que le tonnage maximal.");
                }
                this.tonnageActuel -= pQuantite;
            }
        }

        public override string ToString()
        {
            return string.Format(base.ToString() + this.typeFluide);
        }
        public bool IsSuperTanker => this.TonnageDT > valueTanker;

        public static int ValueTanker => valueTanker;
    }
}
