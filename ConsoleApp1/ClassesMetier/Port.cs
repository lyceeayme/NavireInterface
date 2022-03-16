﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionNavire.Exceptions;
using Station.Interfaces;

namespace NavireHeritage.ClassesMetier
{
    public class Port : IStationnable
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
        public int NbPortique
        {
            get => this.nbPortique;
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
        public int NbQuaisPassager
        {
            get => this.nbQuaisPassager;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("Le nombre de quais passager ne peut pas être négatif");
                }
                this.nbQuaisPassager = value;
            }
        }

        /// <summary>
        /// Gets or set le nombre de quais de tanker du port.
        /// </summary>
        public int NbQuaisTanker
        {
            get => this.nbQuaisTanker;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("Le nombre de quais tanker ne peut pas être négatif");
                }
                this.nbQuaisTanker = value;
            }
        }

        /// <summary>
        /// Gets or set le nombre de super tanker du port.
        /// </summary>
        public int NbQuaisSuperTanker
        {
            get => this.nbQuaisSuperTanker;
            set
            {
                if (value < 0)
                {
                    throw new GestionPortException("Le nombre de quais super tanker ne peut pas être négatif");
                }
                this.nbQuaisSuperTanker = value;
            }
        }

        public void enregistrerArriveePrevue(Navire navire)
        {
            try
            {
                this.navireAttendus.Add(navire.Imo, navire);
            }
            catch (ArgumentException)
            {
                throw new GestionPortException("Le navire est déjà présent dans le dictionnaire.");
            }
        }

        public void enregistrerArrivee(string id)
        {

            if (estAttendu(id))
            {
                this.navireArrives.Add(id, this.navireAttendus[id]);
                this.navireAttendus.Remove(id);
            }
            else if (estAttente(id))
            {
                this.navireArrives.Add(id, this.navireEnAttente[id]);
                this.navireEnAttente.Remove(id);
            }
            else
            {
                throw new GestionPortException("Le navire n'est pas enregistré dans les arrivées prévues ni en attente.");
            }
        }
        public void enregistrerArrivee(Navire navire)
        {
            enregistrerArrivee(navire.Imo);
        }

        public void enregistrerDepart(string id)
        {
            if (this.navireArrives.TryGetValue(id, out Navire value))
            {
                this.navirePartis.Add(id, value);
                this.navireArrives.Remove(id);
                if (this.navireEnAttente.Count != 0)
                {
                    this.enregistrerArrivee(this.navireEnAttente.First().Key);
                }
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

        public bool estAttendu(string id)
        {
            return this.navireAttendus.ContainsKey(id);
        }

        public bool estPresent(string id)
        {
            return this.navireArrives.ContainsKey(id);
        }

        public bool estAttente(string id)
        {
            return this.navireEnAttente.ContainsKey(id);
        }

        public bool estParti(string id)
        {
            return this.navirePartis.ContainsKey(id);
        }

        public Navire getUnAttendu(string id)
        {
            if (!estAttendu(id))
            {
                throw new GestionPortException("Le navire n'est pas dans la liste des attendus");
            }
            return this.navireAttendus[id];
        }

        public Navire getUnEnAttente(string id)
        {
            if (!estAttente(id))
            {
                throw new GestionPortException("Le navire n'est pas dans la liste des en attente");
            }
            return this.navireEnAttente[id];
        }

        public Navire getUnArrive(string id)
        {
            if (!estPresent(id))
            {
                throw new GestionPortException("Le navire n'est pas présent dans le port");
            }
            return this.navireArrives[id];
        }

        public Navire getUnParti(string id)
        {
            if (!this.navirePartis.ContainsKey(id))
            {
                throw new GestionPortException("Le navire n'est pas dans la liste des navires partis récemment");
            }
            return this.navirePartis[id];
        }

        public int getNbTankerArrives()
        {
            int cpt = 0;
            foreach (Navire navire in this.navireArrives.Values)
            {
                if (navire is Tanker && navire.TonnageDT <= 130000)
                {
                    cpt++;
                }
            }
            return cpt;
        }

        public int getNbSuperTankerArrives()
        {
            int cpt = 0;
            foreach (Navire navire in this.navireArrives.Values)
            {
                if (navire is Tanker && navire.TonnageDT > 130000)
                {
                    cpt++;
                }
            }
            return cpt;
        }

        public int getNbCargoArrives()
        {
            int cpt = 0;
            foreach (Navire navire in this.navireArrives.Values)
            {
                if (navire is Cargo)
                {
                    cpt++;
                }
            }
            return cpt;
        }

        public override string ToString()
        {
            return string.Format(
                "--------------------------------------------------------\n" +
                this.nom + "\n" +
                "\t Coordonnées GPS : {0} / {1} \n" +
                "\t Nb portiques : {2} \n" +
                "\t Nb quais croisière : {3} \n" +
                "\t Nb quais tankers : {4} \n" +
                "\t Nb quais super tankers : {5} \n" +
                "\t Nb Navires à quais : {6} \n" +
                "\t Nb Navires attendus : {7} \n" +
                "\t Nb Navires à partis : {8} \n" +
                "\t Nb Navires en attente : {9} \n\n" +
                "Nombre de cargos dans le port : {10} \n" +
                "Nombre de tankers dans le port : {11} \n" +
                "Nombre de super tankers dans le port : {12} \n" +
                "\t-------------------------------\n" +
                listebateauattente(),
                this.latitude,
                this.longitude,
                this.nbPortique,
                this.nbQuaisPassager,
                this.nbQuaisTanker,
                this.nbQuaisSuperTanker,
                this.navireArrives.Count,
                this.navireAttendus.Count,
                this.navirePartis.Count,
                this.navireEnAttente.Count,
                this.getNbCargoArrives(),
                this.getNbTankerArrives(),
                this.getNbSuperTankerArrives()
                );
        }

        public string listebateauattente()
        {
            string message = "Liste des bateaux en attente de leur arrivée :\n";
            foreach (Navire navire in this.navireAttendus.Values)
            {
                message = message + "\t" + navire.ToString() + "\n";
            }
            return message;
        }

        public void enregistrerArriveee(string id)
        {
            if (!this.estAttendu(id) || this.estAttente(id))
            {
                throw new GestionPortException("Le navire n'est pas enregistré dans les arrivées prévues ni en attente.");
            }

            if (this.estAttente(id))
            {
                Navire navire = getUnEnAttente(id);
                if (navire is Cargo && this.NbPortique - this.getNbCargoArrives() > 0)
                {
                    this.navireArrives.Add(id, navire);
                    this.navireEnAttente.Remove(id);
                }
                else if (navire is Tanker)
                {
                    if (navire is Tanker && navire.TonnageDT > 130000 && this.nbQuaisSuperTanker - this.getNbSuperTankerArrives() > 0)
                    {
                        this.navireArrives.Add(id, navire);
                        this.navireEnAttente.Remove(id);
                    }
                    else if (navire is Tanker && navire.TonnageDT <= 130000 && this.nbQuaisSuperTanker - this.getNbSuperTankerArrives() > 0)
                    {
                        this.navireArrives.Add(id, navire);
                        this.navireEnAttente.Remove(id);
                    }
                }
                else
                {
                    navire = getUnAttendu(id);
                    if (navire is Croisiere)
                    {
                        this.navireArrives.Add(id, navire);
                    }
                    else if (navire is Cargo)
                    {
                        if (this.nbPortique - this.getNbCargoArrives() > 0)
                        {
                            this.navireArrives.Add(id, navire);
                        }
                        else
                        {
                            this.navireEnAttente.Add(id, navire);
                        }
                    }
                    else if (navire is Tanker)
                    {
                        if ( navire.TonnageDT > 130000)
                        // si super tanker
                        {
                            if(this.nbQuaisSuperTanker - this.getNbSuperTankerArrives() > 0)
                            {
                                this.navireArrives.Add(id, navire);
                            }
                            else
                            {
                                this.navireEnAttente.Add(id, navire);
                            }
                        }
                        else
                        // si tanker
                        {
                            if(this.nbQuaisTanker - this.getNbTankerArrives() > 0)
                            {
                                this.navireArrives.Add(id, navire);
                            }
                            else
                            {
                                this.navireEnAttente.Add(id, navire);
                            }
                        }
                    }
                    this.navireAttendus.Remove(id);
                }
            }
        }



    }
}
