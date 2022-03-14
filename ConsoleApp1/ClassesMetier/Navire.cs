using GestionNavire.Exceptions;
using System.Text.RegularExpressions;

namespace NavireHeritage.ClassesMetier
{
    public abstract class Navire
    {
        protected readonly string imo;
        protected readonly string nom;
        protected string latitude;
        protected string longitude;
        protected int tonnageActuel;
        protected readonly int tonnageDT;
        protected readonly int tonnageDWT;

        protected Navire(string imo, string nom, string latitude, string longitude, int tonnageActuel, int tonnageGT, int tonnageDWT)
        {
            string pattern = @"IMO\d{7}";
            if (Regex.IsMatch(imo, pattern))
            {
                this.imo = imo;
            }
            else
            {
                throw new GestionPortException("Erreur, Le pattern ne correspond pas.");
            }
            this.nom = nom;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.TonnageActuel = tonnageActuel;
            this.tonnageDT = tonnageGT;
            this.tonnageDWT = tonnageDWT;
        }

        /// <summary>
        /// Gets l'imo du bateau.
        /// </summary>
        public string Imo => this.imo;

        /// <summary>
        /// Gets le nom du bateau.
        /// </summary>
        public string Nom => this.nom;

        /// <summary>
        /// Gets or sets la latitude de ses coordonnées.
        /// </summary>
        public string Latitude { get => this.latitude; set => this.latitude = value; }

        /// <summary>
        /// Gets or sets la longitude de ses coordonnées.
        /// </summary>
        public string Longitude { get => this.longitude; set => this.longitude = value; }

        /// <summary>
        /// Gets or sets la quantité en tonnes de son tonnage.
        /// </summary>
        public int TonnageActuel { get => this.tonnageActuel;
            set
            {
                if (this.tonnageActuel + value < 0 || this.tonnageActuel + value > this.tonnageDT)
                {
                    throw new GestionPortException("Le bateau ne peut pas contenir moins que 0 ou plus que sa capacité maximal.");
                }
                else
                {
                    this.tonnageActuel += value;
                }
            }
        }

        /// <summary>
        /// Gets le tonnage maximal du navire.
        /// </summary>
        public int TonnageGT => this.tonnageDT;

        /// <summary>
        /// Gets le poid total du navire rempli le plus possible(personnes, nourriture, tonnageactuelle).
        /// </summary>
        public int TonnageDWT { get => this.tonnageDWT; }
    }
}
