using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionNavire.Exceptions;
using Station.Interfaces;
using System.Configuration;

namespace NavireHeritage.ClassesMetier
{
    public class Port : IStationnable
    {
        private static int ValueTanker = Convert.ToInt32(ConfigurationSettings.AppSettings.Get(ValueTanker));
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
        private Dictionary<int, Stockage> Stockages;

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
            this.Stockages = new Dictionary<int, Stockage>();
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

        //public void enregistrerArriveee(string id)
        //{

        //    if (estAttendu(id))
        //    {
        //        this.navireArrives.Add(id, this.navireAttendus[id]);
        //        this.navireAttendus.Remove(id);
        //    }
        //    else if (estAttente(id))
        //    {
        //        this.navireArrives.Add(id, this.navireEnAttente[id]);
        //        this.navireEnAttente.Remove(id);
        //    }
        //    else
        //    {
        //        throw new GestionPortException("Le navire n'est pas enregistré dans les arrivées prévues ni en attente.");
        //    }
        //}
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
                    int i = 0;
                    while (i < this.navireEnAttente.Count && this.navireEnAttente.ElementAt(i).Value.GetType().FullName == value.GetType().FullName)
                    {
                        i++;
                        if (this.navireEnAttente.ElementAt(i).Value.GetType() is Tanker
                            && value.GetType() is Tanker
                            && this.navireEnAttente.ElementAt(i).Value.TonnageDT <= ValueTanker != value.TonnageDT <= ValueTanker)
                        {
                            i++;
                        }
                    }
                    this.navireArrives.Add(this.navireEnAttente.ElementAt(i).Key, this.navireEnAttente.ElementAt(i).Value);
                    this.navireEnAttente.Remove(this.navireEnAttente.ElementAt(i).Key);
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
                if (navire is Tanker && navire.TonnageDT <= ValueTanker)
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
                if (navire is Tanker && navire.TonnageDT > ValueTanker)
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
                listeprevu()+
                "\t-------------------------------\n"+
                listeaquais()+
                "\t-------------------------------\n" +
                listeattente(),
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

        private string listeprevu()
        {
            string message = "Liste des bateaux en attente de leur arrivée :\n";
            foreach (Navire navire in this.navireAttendus.Values)
            {
                message = message + "\t" + navire.ToString() + "\n";
            }
            return message;
        }

        private string listeaquais()
        {
            string message = "Liste des bateaux présent dans le port :\n";
            foreach (Navire navire in this.navireArrives.Values)
            {
                message = message + "\t" + navire.ToString() + "\n";
            }
            return message;
        }

        private string listeattente()
        {
            string message = "Liste des bateaux en attente dans la zone du port :\n";
            foreach (Navire navire in this.navireEnAttente.Values)
            {
                message = message + "\t" + navire.ToString() + "\n";
            }
            return message;
        }

        private void AjoutNavireAttente(string id)
        {
            Navire navire = getUnEnAttente(id);
            if (navire is Cargo && this.NbPortique - this.getNbCargoArrives() > 0)
            {
                this.navireArrives.Add(id, navire);
                this.navireEnAttente.Remove(id);
            }
            else if (navire is Tanker)
            {
                if (navire is Tanker && navire.TonnageDT > ValueTanker && this.nbQuaisSuperTanker > this.getNbSuperTankerArrives())
                {
                    this.navireArrives.Add(id, navire);
                    this.navireEnAttente.Remove(id);
                }
                else if (navire is Tanker && navire.TonnageDT <= ValueTanker && this.nbQuaisSuperTanker > this.getNbSuperTankerArrives())
                {
                    this.navireArrives.Add(id, navire);
                    this.navireEnAttente.Remove(id);
                }
            }
        }

        private void AjoutNavireAttendu(string id)
        {
            Navire navire = getUnAttendu(id);
            if (navire is Croisiere)
            {
                this.navireArrives.Add(id, navire);
            }
            else if (navire is Cargo)
            {
                if (this.nbPortique > this.getNbCargoArrives())
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
                if (navire.TonnageDT > ValueTanker)
                // si super tanker
                {
                    if (this.nbQuaisSuperTanker > this.getNbSuperTankerArrives())
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
                    if (this.nbQuaisTanker > this.getNbTankerArrives())
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

        public void enregistrerArrivee(string id)
        {
            if (!this.estAttendu(id) || !this.estAttente(id))
            {
                throw new GestionPortException("Le navire n'est pas enregistré dans les arrivées prévues ni en attente.");
            }
            if (this.estAttente(id))
            {
                AjoutNavireAttente(id);
            }
            else
            {
                AjoutNavireAttendu(id);
            }
        }

        private bool EstDechargeable(Navire navire)
        {
            int capadispostock = 0;
            foreach (Stockage stock in this.Stockages.Values)
            {
                capadispostock += stock.CapaciteDispo;
            }
            return capadispostock > navire.TonnageActuel;
        }

        private int Getdistance(Navire navire, Stockage stock)
        {
            if (navire.TonnageActuel > stock.CapaciteDispo)
            {
                return navire.TonnageActuel - stock.CapaciteDispo;
            }
            else
            {
                return stock.CapaciteDispo - navire.TonnageActuel;
            }
        }

        private int GetZoneplusproche(Navire navire)
        {
            int zone = this.Stockages.First().Key;
            int distance = Getdistance(navire, this.Stockages[zone]);
            foreach (Stockage stock in this.Stockages.Values)
            {
                if (navire.TonnageActuel == stock.CapaciteDispo)
                {
                    return stock.Numero;
                }
                else if( Getdistance(navire,stock) < distance)
                {
                    zone = stock.Numero;
                }
            }
            return zone;
        }

        public void Dechargement(Navire navire)
        {
            if (!EstDechargeable(navire))
            {
                throw new GestionPortException("Le navire a une trop grosse cargaison pour le port");
            }

        }

        public double GetMilesToArrival(Navire navire)
        {
            GeoCoordinate Gpsnav = new GeoCoordinate(Convert.ToDouble(navire.Latitude), Convert.ToDouble(navire.Longitude));
            GeoCoordinate Gpsport = new GeoCoordinate(Convert.ToDouble(this.latitude), Convert.ToDouble(this.longitude));
            return Gpsnav.GetDistanceTo(Gpsport);
        }
    } 
}
