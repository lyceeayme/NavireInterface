﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionNavire.Exceptions;

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

        public Port(string nom, string latitude, string longitude, int nbPortique, int nbQuaisPassager, int nbQuaisTanker, int nbQuaisSuperTanker)
        {
            this.nom = nom;
            this.latitude = latitude;
            this.longitude = longitude;
            this.NbPortique = nbPortique;
            this.NbQuaisPassager = nbQuaisPassager;
            this.NbQuaisTanker = nbQuaisTanker;
            this.NbQuaisSuperTanker = nbQuaisSuperTanker;
            this.navireAttendus = new Dictionary<string, Navire>();
            this.navireArrives = new Dictionary<string, Navire>();
            this.navirePartis = new Dictionary<string, Navire>();
            this.navireEnAttente = new Dictionary<string, Navire>();
        }

        /// <summary>
        /// Gets le nom du port.
        /// </summary>
        public string Nom => this.nom;

        /// <summary>
        /// Gets la latitude du port.
        /// </summary>
        public string Latitude => this.latitude;

        /// <summary>
        /// Gets la longitude du port.
        /// </summary>
        public string Longitude => this.longitude;

        /// <summary>
        /// Gets or set le nombre de Portique du port.
        /// </summary>
        public int NbPortique { get => this.nbPortique;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("Le nombre de portique ne peut pas être négatif");
                }
                this.nbPortique = value;
            } 
        }

        /// <summary>
        /// Gets or sets le nombre de quais passager du port.
        /// </summary>
        public int NbQuaisPassager { get => this.nbQuaisPassager;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("Le nombre de quais passager ne peut pas être négatif");
                }
                this.nbPortique = value;
            }
        }

        /// <summary>
        /// Gets or set le nombre de quais de tanker du port.
        /// </summary>
        public int NbQuaisTanker { get => this.nbQuaisTanker;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("Le nombre de quais tanker ne peut pas être négatif");
                }
                this.nbPortique = value;
            }
        }

        /// <summary>
        /// Gets or set le nombre de super tanker du port.
        /// </summary>
        public int NbQuaisSuperTanker { get => this.nbQuaisSuperTanker;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("Le nombre de quais super tanker ne peut pas être négatif");
                }
                this.nbPortique = value;
            }
        }

        public void enregistrementArriveePrevue(Navire navire)
        {
            try
            {
                this.navireAttendus.Add(navire.Imo, navire);
            }
            catch(ArgumentException)
            {
                throw new GestionPortException("Le navire est déjà présent dans le dictionnaire.");
            }
        }

        public void enregistrementArrivee(string id)
        {
            if (this.navireAttendus.TryGetValue(id, out Navire value))
            {
                this.navireArrives.Add(id, value);
                this.navireAttendus.Remove(id);
            }
            else
            {
                throw new GestionPortException("Le navire n'est pas enregistré dans les arrivées prévues.");
            }
        }
        public void enregistrementArrivee(Navire navire)
        {
            enregistrementArrivee(navire.Imo);
        }

        public void enregistrerDepart(string id)
        {
            if (this.navireArrives.TryGetValue(id, out Navire value))
            {
                this.navirePartis.Add(id, value);
                this.navireArrives.Remove(id);
            }
            else
            {
                throw new GestionPortException("Le navire n'est pas présent dans le port.");
            }
        }

        public void enregistrerDepart(Navire navire)
        {
            enregistrerDepart(navire.Imo);
        }

        public void AjoutNavireEnAttente(Navire navire)
        {
            this.navireEnAttente.Add(navire.Imo, navire);
        }

        public bool estAttendu(string imo)
        {
            return this.navireAttendus.ContainsKey(imo);
        }

        public bool estPresent(string imo)
        {
            return this.navireArrives.ContainsKey(imo);
        }

        public bool estAttente(string imo)
        {
            return this.navireEnAttente.ContainsKey(imo);
        }
    }
}
