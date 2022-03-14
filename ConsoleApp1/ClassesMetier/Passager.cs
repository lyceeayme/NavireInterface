using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavireHeritage.ClassesMetier
{
    public class Passager
    {
        private readonly string numPasseport;
        private readonly string nom;
        private readonly string prenom;
        private readonly string nationalite;

        public Passager(string numPasseport, string nom, string prenom, string nationalite)
        {
            this.numPasseport = numPasseport;
            this.nom = nom;
            this.prenom = prenom;
            this.nationalite = nationalite;
        }

        /// <summary>
        /// Gets le numero de passeport.
        /// </summary>
        public string NumPasseport => this.numPasseport;

        /// <summary>
        /// Gets le nom du passager.
        /// </summary>
        public string Nom => this.nom;

        /// <summary>
        /// Gets le prenom du passager.
        /// </summary>
        public string Prenom => this.prenom;

        /// <summary>
        /// Gets la nationalite du passager.
        /// </summary>
        public string Nationalite => this.nationalite;
    }
}
