using GestionNavire.Exceptions;
namespace NavireHeritage.ClassesMetier
{
    public class Cargo : Navire
    {
        private readonly string typeFret;

        public Cargo(string imo, string nom, string latitude, string longitude, int tonnageActuel, int tonnageDT, int tonnageDWT, string typeFret)
            :base(imo, nom, latitude, longitude, tonnageActuel, tonnageDT, tonnageDWT)
        {
            this.typeFret = typeFret;
        }

        /// <summary>
        /// Gets le type du chargement.
        /// </summary>
        public string TypeFret1 => this.typeFret;

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

        public void charge(int pQuantite)
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
    }
}
