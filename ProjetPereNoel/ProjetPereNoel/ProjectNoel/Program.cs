using System.ComponentModel;
using System.Reflection.Metadata;
using ProjectNoel;

namespace ProjectNoel
{
    public class Program
    {
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------Types construits--------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        
        //Auteur : Tancrède
        //Type Jouet :
        //Type représentant les différents jouets qu'un enfant peut recevoir
        //La valeur de chaque élément du type représente le nombre d'heures
        //nécessaires à sa fabrication.
        public enum Jouet
        {
            Nounours = 3,
            Tricycle = 4,
            Jumelles = 6,
            Abonnement = 1,
            Ordinateur = 10

        }


        //Auteur : Rémi
        //Type : Continent
        //Ce type représente les différents continents que le père noël doit couvrir.
        public enum Continents
        {
            Afrique,
            Amerique,
            Asie,
            Europe,
            Oceanie
        }


        //Auteur : Rémi, Tancrède
        //Type : EtatTravail
        //Description : Type énuméré représentant les différents statuts qu'un travailleur peut avoir
        public enum EtatTravail
        {
            //Pour les Lutins et Nains s'ils sont au travail
            Travail,

            //Pour les Nains et Lutins qui n'ont pas de travail
            Attente,

            //Pour les Nains et Lutins qui sont au repos
            Repos,

            //Pour les Elfes en voyage
            EnVoyage,

            //Pour les Elfes en train de charger les cadeaux
            ChargementCadeaux
        }

        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------Classes-----------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//

        //---------------------------------------------Classe Param---------------------------------------------//
        //Auteur : Rémi
        //Utilité : La class Param sert à demander à l'utilisateur de definir les valeurs qui vont servir le programme durant la suite 
        // de son fonctionement.
        public class Param
        {
            public int NBLutins { get; set; } // Nombre maximum de Lutins que l'utilisateur autorise
            public int NBNains { get; set; } // Nombre maximum de Nains que l'utilisateur autorise
            public int NBJouetsParTraineau { get; set; } // Nombre maximum de jouets par traineau autorisés par l'utilisateur
            public int NBEnfants { get; set; } // Nombre d'enfants qui demandent des cadeaux au Père Noël
            public int NBLettresParHeures { get; set; } // Nombre de lettres que le Père Noël reçois par heures
            public Param()
            {
                Console.WriteLine("Veuillez donner Le nombres de lutins max :");
                NBLutins = DemandeInt(); // Demande la valeurs de la variables à l'utilisateur en appellant la fonction DemandeInt() 
                Console.WriteLine("Veuillez donner Le nombres de nains max :");
                NBNains = DemandeInt();
                Console.WriteLine("Veuillez donner Le nombres de jouets max par traineau :");
                NBJouetsParTraineau = DemandeInt();
                Console.WriteLine("Veuillez donner Le nombres d'enfants max :");
                NBEnfants = DemandeInt();
                Console.WriteLine("Veuillez donner Le nombres de lettres par heures :");
                NBLettresParHeures = DemandeInt();
            }
        }

        //---------------------------------------------Classe File---------------------------------------------//
        //Auteur : Tancrède 
        //Description : Classe représentant une file avec des listes chaînées.
        //Elle contient les méthodes : Enfile, Defile et EstVide
        public class File<T>
        {
            internal ListeChainee<T> Liste {get; set;}// Liste qui représente la file
            public int Taille;

            //Constructeur
            internal File()
            {
                Liste = new ListeChainee<T> {} ;
                Taille = 0;
            }

            //-----------------Méthode internales à la classe-----------------//
            //----Méthode Enfile----//
            //Auteur : Tancrède
            //Paramètres :
            //  pValeur, valeur à enfiler de type quelconque
            //Description : Enfile la valeur en paramètre.
            public void Enfile (T pValeur)
            {
                this.Liste.AddLast(pValeur);//La dernière position est la position d'arrivée dans la file
                Taille++;
            }

            //----Méthode Defile----//
            //Auteur : Tancrède
            //Description : Defile la file et renvoie la valeur défilée.
            public T Defile()
            {
                if (this.EstVide() == false)
                {
                    T returnValue = this.Liste.Head.Valeur;
                    this.Liste.RemoveFirst();//La première position de la liste est la sortie
                    Taille--;
                    return returnValue;
                }
                else
                {
                    throw new Exception("Erreur : La File est vide"); 
                }
                
            }

            //----Méthode EstVide----//
            //Auteur : Tancrède
            //Description : Renvoie true si la file est vide et false sinon.
            public bool EstVide()
            {
                return this.Liste.IsEmpty();
            }

            //----Méthode Affiche----//
            //Auteur : Tancrède
            //Description : fonction qui affiche le contenu de la file et son sens de défilage
            public void Affiche()
            {
                if (this.EstVide()) {Console.WriteLine("La file est vide");}
                else
                {
                        
                    File<T> fileTemp = new File<T> {};
                    Console.Write("<--");
                    while (this.EstVide() != true)
                    {
                        T valTemp = this.Defile();
                        fileTemp.Enfile(valTemp);
                        Console.Write($" {valTemp} |");
                    }
                    while (fileTemp.EstVide() != true)
                    {
                        T valTemp = fileTemp.Defile();
                        this.Enfile(valTemp);
                    }
                    Console.WriteLine(" File");
                }
            }

            //----Méthode Vide----//
            //Auteur : Tancrède 
            //Description : Vide le contenu de la file
            public void Vide()
            {
                this.Liste.Clear();//Vide la liste qui sert à représenter la file
                this.Taille = 0;
            }
        }



        //---------------------------------------------Classe Pile---------------------------------------------//        
        // //Auteur : Tancrède 
        //Description : Classe représentant une pile avec des listes chaînées.
        //Elle contient les méthodes : Empile, Depile et EstVide

        public class Pile<T>
        {
            public ListeChainee<T> Liste {get; set;}//Liste représentant la pile
            public int Taille;//Attribut représentant la taille de la pile

            //Constructeur
            public Pile()
            {
                Liste = new ListeChainee<T> {} ;
                Taille = 0;
            }

            //----Méthode Empile----//
            //Auteur : Tancrède
            //Paramètres :
            //  pValeur, valeur à empiler de type quelconque
            //Description : Empile la valeur en paramètre.
            public void Empile (T pValeur)
            {
                this.Liste.AddFirst(pValeur);//La première position est la position d'arrivée dans la pile
                this.Taille ++;
            }

            //----Méthode Depile----//
            //Auteur : Tancrède
            //Description : Depile la pile et renvoie la valeur dépilée.
            public T Depile()
            {
                if (this.EstVide() == false){
                    T returnValue = this.Liste.Head.Valeur;
                    this.Liste.RemoveFirst();//On enlève la première valeur de la liste quand on dépile
                    this.Taille--;
                    return returnValue;
                }
                else
                {
                    throw new Exception("Erreur : La Pile est vide"); 
                }
            }

            //----Méthode EstVide----//
            //Auteur : Tancrède
            //Description : Renvoie true si la pile est vide et false sinon.
            public bool EstVide()
            {
                return this.Liste.IsEmpty();
            }

            //----Méthode Affiche----//
            //Auteur : Tancrède
            //Description : affiche le contenu de la pile et le sens dans laquelle elle va.
            public void Affiche()
            {
                if (this.EstVide()){Console.WriteLine("La pile est vide");}
                else
                {
                    Pile<T> pileTemp = new Pile<T> {};
                    Console.Write("<--");
                    while (this.EstVide() != true)
                    {
                        T valTemp = this.Depile();
                        Console.Write($" {valTemp} |");
                        pileTemp.Empile(valTemp);
                    }
                    while (pileTemp.EstVide() != true)
                    {
                        T valTemp = pileTemp.Depile();
                        this.Empile(valTemp);
                    }
                    Console.WriteLine(" Pile");
                }
            }

            //----Méthode Vide----//
            //Auteur : Tancrède 
            //Description : Vide le contenu de la pile
            public void Vide()
            {
                this.Liste.Clear();//Vide la liste qui sert à représenter la pile
                this.Taille = 0;
            }
        }



        //---------------------------------------------Classe Lettre---------------------------------------------//        
        //Auteur : Tancrède, Rémi
        //Description : classe qui représente une lettre avec trois attributs, le nom et le prénom de l'enfant et le jouet demandé par l'enfant
        public class Lettre
        {
            public string Nom {get; set;}
            public string Prenom {get; set;}
            public Continents Continent {get; set;}
            public string Adresse {get; set;}
            public int Age {get;set;}

            //Constructeur
            public Lettre( int age, string nom, string prenom, Continents continent, string adresse)
            {
                Nom = nom;                  //On utilisera la fonction CreeLettre pour ce paramètre
                Prenom = prenom;            //On utilisera la fonction CreeLettre pour ce paramètre
                Continent = continent;      //On utilisera la fonction CreeLettre pour ce paramètre
                Adresse = adresse;          //On utilisera la fonction CreeLettre pour ce paramètre
                Age = age;                  //Age généré aléatoirement
            }

            public void Affiche()
            {
                Console.WriteLine($"{Prenom} {Nom} du {Adresse} en {Continent} a {Age} ans");
            }

        }

        //---------------------------------------------Classe Lutin---------------------------------------------//
        //Auteur : Tancrède
        //Description : classe représentant les lutins, leurs attributs et leurs méthodes qui leurs sont utiles pour la fabrications des cadeaux
        public class Lutin
        {
            
            //Statut de travail du Lutin
            public EtatTravail Statut;

            //Compteur qui représente le temps restant au lutin pour fabriquer son cadeau
            public int HeureRestantes; 

            //Lettre en cours de traitement
            public Lettre? LettreEnCours;
            
            public Lutin()
            {
                Statut = EtatTravail.Attente;//Le lutin est par defaut en attente
                HeureRestantes = 0;
                LettreEnCours = null;
            }

            public void DebutFabricationJouet(Lettre pLettre)
            {
                this.LettreEnCours = pLettre;

                //On récupère le jouet correspondant à l'âge de l'enfant grâce à la fonction
                //AgeToJouet() et on récupère son temps de fabrication
                this.HeureRestantes = (int)AgeToJouet(pLettre.Age);

                //On change l'état du Lutin
                this.Statut = EtatTravail.Travail;
            }

            //----Méthode Travaille----//
            //Auteur : Tancrède
            //Description : renvoie la Lettre du Lutin s'il a fini de fabriquer le jouet 
            //sinon fait descendre le temps restant s'il est au travail et ne renvoie rien sinon
            public Lettre Travaille()
            {
                //Ne renvoie rien s'il n'est ni au travail ou au repos
                if (this.Statut != EtatTravail.Repos && this.Statut != EtatTravail.Travail)
                {
                    return null;
                }

                //On fait descendre le temps 
                this.HeureRestantes --;

                //Si la lettre est terminé
                if(this.HeureRestantes <= 0)
                {
                    //Pour pouvoir mettre l'attribut LettreEnCours comme nul
                    Lettre LettreTerminee = this.LettreEnCours;

                    this.Statut = EtatTravail.Attente;
                    this.LettreEnCours = null;

                    return LettreTerminee;// On renvoie la lettre finie
                }

                //Pour éviter les erreurs
                return null;
            }

        }
        
        //---------------------------------------------Classe Traineau---------------------------------------------//
        // Auteur : Rémi
        // Utilité : La classe Traineau sert à Initialiser les traineaux qui serviront pour la livraison des cadeaux
        public class Traineau
        {
            public Continents Continent { get; set; }
            public int CapaciteMax { get; set; }
            public Pile<Lettre> PileCadeaux { get; set; }
            public bool Parti { get; set; }
            public int TempsAvantRetour { get; set; }

            //Constructeur
            public Traineau(int capacite, Continents continent)
            {
                this.CapaciteMax = capacite;
                this.Continent = continent;
                this.PileCadeaux = new Pile<Lettre>(); // Initialise la pile de cadeau
            }

            //Auteur : Rémi
            //Fonction/Class : ChargeTraineau
            //Paramètres : lettre (Lettre)
            //Renvoie : void
            //Utilité : La fonction sert à ajouter les cadeaux au traineaux.
            public void ChargeTraineau(Lettre lettre)
            {
                PileCadeaux.Empile(lettre);                 // Ajoute la lettre à la pile du traîneau
            }

            //Auteur : Rémi
            //Fonction/Class : Plein
            //Renvoie : bool
            //Utilité : La fonction renvoie un bool qui nous dis si le traineau est plein
            public bool Plein()
            {
                return PileCadeaux.Taille >= CapaciteMax;    // teste si le traîneau est plein.
            }

            //Auteur : Rémi
            //Fonction/Class : EnVoyages
            //Renvoie : Void
            //Utilité : La fonction sert à savoir si le traineau à fini la tourné, si oui alors la pile des cadeaux est vidée et parti = false, sinon alors on enlève 1 heure du temps avant son retour et ont dis qu'il est encore en livraison.
            public void EnVoyage()
            {
                if (Parti == true && TempsAvantRetour <= 0)     // test si le voyage est fini.
                {
                    Parti = false;                          
                    PileCadeaux.Vide();                         // Le voyage est fini donc on vide la pile.
                }
                else if (Parti == true)
                {
                    Console.WriteLine($"Encore en Livraison, il reste encore {TempsAvantRetour}h");
                    TempsAvantRetour--;
                }
            }

            //Auteur : Rémi
            //Fonction/Class : Depart
            //Renvoie : Void
            //Utilité : La fonction prépare les valeurs du traineau pour son départ.
            public void Depart()
            {
                if (PileCadeaux.Taille > 0)
                {
                    Parti = true;
                    TempsAvantRetour = 6;
                }
            }
        }   

        //---------------------------------------------Classe Nain---------------------------------------------//
        // Auteur : Rémi
        // Utilité : La classe Nain sert à créer les nains, il sont ceux qui emballe les caadeau pour les donnés aux elfes.
        public class Nain
        {
            public EtatTravail Statut {get; set;}
            public int TempRestant {get; set;}
            public Lettre? LettreActuelle {get; set;}
            
            //Constructeur
            public Nain()
            {
                this.LettreActuelle = null;
                this.TempRestant = 0;
                this.Statut = EtatTravail.Attente;
            }

            //Auteur : Rémi
            //Fonction/Class : DebutEmballage
            //Paramètres : lettre (Lettre)
            //Renvoie : Void
            //Utilité : La fonction initialise les valeurs du nain pour qu'il puisse commencer à emballer.
            public void InitEmballage(Lettre lettre)
            {
                if(Statut == EtatTravail.Attente && lettre != null)
                {
                    LettreActuelle = lettre;
                    TempRestant = 2;
                    Statut = EtatTravail.Travail;
                }
            }

            //Auteur : Rémi
            //Fonction/Class : Emballage
            //Renvoie : Lettre
            //Utilité : La fonction renvoie les lettres fini et garde le nain occupé aussi longtemps que necessaire.
            public Lettre Emballage()
            {
                if(Statut == EtatTravail.Travail && LettreActuelle != null)
                {
                    TempRestant--;
                    if(TempRestant > 0)
                    {
                        return null;
                    }
                    else
                    {
                        Lettre lettreFini = LettreActuelle;
                        LettreActuelle = null;
                        Statut = EtatTravail.Attente;
                        return lettreFini;
                    }
                }
                else if (Statut == EtatTravail.Attente)
                {
                    return null;
                }
                return null;
            }
        }


        //---------------------------------------------Classe Elfe---------------------------------------------//
        // Auteur : Rémi
        // Utilité : La classe Elfe sert à créer les elfe qui vont servir à charger les traineaux. Chaque elfe à son continent.
        public class Elfe
        {
            public Continents Continent {get; set;}
            public Traineau TraineauCont {get; set;}

            //Constructeur
            public Elfe(Continents continent, int capaciteMax)
            {
                Continent = continent;
                TraineauCont = new Traineau(capaciteMax, continent);
            }

            //Auteur : Rémi
            //Fonction/Class : AjouteTraineau
            //Paramètres : lettre (Lettre)
            //Renvoie : Void
            //Utilité : La fonction ajoutre au traineau le cadeau que l'elfe a avec lui.
            public void AjouteTraineau(Lettre lettre)
            {
                TraineauCont.ChargeTraineau(lettre);
            }
        }


        //---------------------------------------------Classe Entrepot---------------------------------------------//
        // Auteur : Rémi
        // Utilité : La classe entrepot sert à fabriquer et stocker les jouets qu iarrivent dans les entrepôts des 5 continents.
        public class Entrepot
        {
            public Continents Continent {get; set;} //Le continent qui est attaché à l'entrepôt

            public Pile<Lettre> StockJouet {get; set;} //La pile de jouets de l'entrepôt

            public Entrepot(Continents continent)
            {
                Continent = continent;
                StockJouet = new Pile<Lettre>(); //Initialisation de la pile de jouets
            }

            //Auteur : Rémi
            //Fonction/Class : AjouterStock
            //Paramètres : lettres (Pile<Lettre>)
            //Renvoie : Void
            //Utilité : La fonction ajoute les jouets du traineau à la pile de l'entrepôt.
            public void AjouterStock(Pile<Lettre> lettres)
            {
                StockJouet.Empile(lettres.Depile()); // Ajoute les jouets du traineau à la pile de l'entrepôt
            }


            //Auteur : Rémi
            //Fonction/Class : EntrepotAffiche
            //Renvoie : Void
            //Utilité : La fonction affiche les jouets dans la pile de l'entrepôt.
            public void EntrepotAffiche()
            {
                StockJouet.Affiche(); // Affiche la pile de l'entrepôt
            }
        }


        //---------------------------------------------Classe Simulation---------------------------------------------//
        // Auteur : Tancrède et Rémi
        // Utilité : La classe Simulation sert à lancer le logiciel et à faire marcher toutes les classes et fonctions ensembles.
        public class Simulation
        {
            //Paramètres de la simulation
            public Param? ParamSimulation {get; set;}

            //Pile de lettres sur le bureau du Père Noël
            public Pile<Lettre> lettresBureauPereNoel {get; set;}

            //File des jouets fabriqués par les lutins mais en attente des nains
            public File<Lettre> FileAttenteNain {get; set;}

            //Jouets emballés par les nains, en attente d’être chargés par les elfes
            public File<Lettre> FileAttenteElfes {get; set;}

            //Jouets stockés dans l'entrepot d'Asie
            public Pile<Lettre> EntrepotAsie {get; set;}

            //Jouets stockés dans l'entrepot d'Europe
            public Pile<Lettre> EntrepotEurope {get; set;}
            
            //Jouets stockés dans l'entrepot d'Amerique
            public Pile<Lettre> EntrepotAmerique {get; set;}

            //Jouets stockés dans l'entrepot d'Afrique
            public Pile<Lettre> EntrepotAfrique {get; set;}

            //Jouets stockés dans l'entrepot d'Oceanie
            public Pile<Lettre> EntrepotOceanie {get; set;}

            //File des Nains
            public File<Nain> FileNains {get; set;}

            //File des Lutins
            public File<Lutin> FileLutins {get; set;}

            public Simulation()
            {
                //Nettoie l'affichage
                Console.Clear();

                //Booléen pour sortir la boucle
                bool continuer = true;

                //Message de bienvenu
                Console.WriteLine("\n==================================================================================================");
                Console.WriteLine("                       BIENVENUE DANS LE SIMULATEUR DE GESTION DE PERE NOEL                         ");
                Console.WriteLine("==================================================================================================");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Veuillez choisir comment poursuivre : ");
                Console.WriteLine("    -'LANCEMENT' pour lancer une simulation");
                Console.WriteLine("    -'QUITTER' pour arrêter le programme");
                while (continuer)
                {
                    string? entreeUser = Console.ReadLine();
                    if (entreeUser == "LANCEMENT")
                    {
                        continuer = false;
                        //Initialisation des paramètres par l'utilisateur 
                        ParamSimulation = new Param();
                    }
                    else if (entreeUser == "QUITTER")
                    {
                        continuer = false;
                        //On laisse le programme se finir tout seul
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Veuillez rentrer une option valide parmi les choix ci dessus");
                    }

                    //Déclaration des variables globales indépendantes de l'utilisateur : 

                    //Files et piles que l'on va utiliser

                    lettresBureauPereNoel = new Pile<Lettre>{};

                    FileLutins = new File<Lutin> {} ;

                    FileNains = new File<Nain> {};

                    FileAttenteNain = new File<Lettre> {};

                    FileAttenteElfes = new File<Lettre>{};

                    EntrepotAsie = new Pile<Lettre>{};

                    EntrepotAfrique = new Pile<Lettre>{};

                    EntrepotAmerique = new Pile<Lettre>{};

                    EntrepotEurope = new Pile<Lettre>{};

                    EntrepotOceanie = new Pile<Lettre>{};
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------Méthodes--------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//

        //Auteur : Rémi
        //Fonction/Class : DemandeInt
        //Paramètres : Aucun
        //Renvoie : Int
        //Utilité : La fonction sert à verifier si ce que l'utilisateur à rentrer est bien un int, et si il est positif.
        //          Si ce n'est pas le cas, la fonction lui envoie un message d'erreur et lui demande une nouvelle valeur.
        public static int DemandeInt()
        {
            bool condition = false;
            int Valeur;
            do
            {
                string saisie = Console.ReadLine();
                condition = int.TryParse(saisie, out Valeur);
                if (!condition || int.Parse(saisie) < 1)
                {
                    Console.WriteLine("Erreur : veuillez entrer un entier valide !");
                    condition = false;
                }
                else
                {
                    Valeur = int.Parse(saisie);
                }
            } while (!condition);
            Console.Clear();
            return Valeur;
        }
        
        //---------------------------------------------Fonction AgeToJouet---------------------------------------------// 
        //Auteur : Rémi
        //Fonction/Class : AgetoJouet
        //Paramètres : age (int)
        //Renvoie : Jouet
        //Utilité : La fonction renvoie le type de jouet adapté en fonction de l'âge de l'enfant.
        //0 et 18 ans
        public static Jouet AgeToJouet(int age)
        {
            // Test si l'âge entrer par l'utisateur est dans compris entre 0 et 18 ans les deux compris, sinon renvoie un erreur.
            if (age < 0 || age > 18){throw new Exception("L'âge doit être compris entre 0 et 18 ans.");}
            else if(age < 3){return Jouet.Nounours;}
            else if(age < 6){return Jouet.Tricycle;}
            else if(age < 11){return Jouet.Jumelles;}
            else if(age < 16){return Jouet.Abonnement;}
            else{return Jouet.Ordinateur;}
        }

        //---------------------------------------------Fonction RandomContinent---------------------------------------------// 
        //Auteur : Rémi
        //Fonction/Class : RandomContinent
        //Paramètres : nbr (int)
        //Renvoie : Continents
        //Utilité : La fonction renvoie un continent en fonction du numero au hazard qui est en paramètre.
        public static Continents RandomContinent(int nbr)
        {
            if(nbr < 0 || nbr > 4){throw new Exception("Le numéro n'est pas convenable");}
            else if(nbr == 0){return Continents.Afrique;}
            else if(nbr == 1){return Continents.Amerique;}
            else if(nbr == 2){return Continents.Asie;}
            else if(nbr == 3){return Continents.Europe;}
            else{return Continents.Oceanie;}
        }

        //---------------------------------------------Fonction CreerLettre---------------------------------------------//        
        //Auteur : Tancrède, Rémi
        //Description : Fonction qui génére une lettre aléatoire et l'ajoute dans la pile de lettre sur le bureau du Père Noël prise en paramètre
        public static void CreerLettre(Pile<Lettre> PileDeLettre)
        {
            Random random = new Random(); // Initialise random
            // Initialise une liste des prénoms (les prénoms les plus donnés en france en 2024)
            string[] ListePrenoms = {"Gabriel", "Léo", "Maël", "Noah", "Jules", "Adam", "Louis", "Jade", "Louise", "Lola", "Emma", "Lou", "Tibo"}; 
            // Initialise une liste des noms (noms aléatoire)
            string[] ListeNoms = {"Dupont", "Martin", "Inshape", "Papin", "Bernard", "Robert", "Leroy", "Lefèvre", "Millot", "Girard", "Moreau", "Simon", "Durand", "Dubois"}; 
            // Initialise une liste d'adresse (adresses aléatoire)
            string[] ListeAdresse = { "12 Rue de la République, 75001 Paris", "45 Avenue Jean Jaurès, 31000 Toulouse", "78 Boulevard de la Liberté, 69003 Lyon", "33 Rue des Fleurs, 13006 Marseille", "15 Place de la Comédie, 34000 Montpellier", "22 Rue du Commerce, 44000 Nantes", "56 Avenue des Champs-Élysées, 75008 Paris", "9 Rue de la Gare, 67000 Strasbourg", "101 Boulevard de la Mer, 06200 Nice", "8 Rue du Marché, 59800 Lille" };
            string prenom = ListePrenoms[random.Next(ListePrenoms.Length)]; // Prend un prénoms aléatoire dans la liste des prénoms
            string nom = ListeNoms[random.Next(ListeNoms.Length)];// Prend un nom aléatoire dans la liste des nom
            Continents continent = RandomContinent(random.Next(4));// Prend un nombre aléatoire et en fait un Continent
            int age = random.Next(18); // Prend un âge aléatoire entre 0 et 18 ans et le transforme en jouet
            string adresse = ListeAdresse[random.Next(ListeAdresse.Length)];// Prend une adresse aléatoire dans la liste des adresses
            Lettre lettreAléatoire = new Lettre(age,nom, prenom, continent, adresse); // Créer la lettre avec les valeurs aléatoires plus hauts
            //Ajoute la lettre à la pile 
            PileDeLettre.Empile(lettreAléatoire);
        }


        public static void Main()
        {
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------Variables globales------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            
            //Paramètres entrés par l'utilisateur
            //Param Noel = new Param();

            //Pile des lettres sur le bureau du Père Noël
            

            

            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------Test unitaires----------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            Console.Clear();
            //------------Tancrède------------//
            //****Type Jouet****
            Console.WriteLine("-------------Tests type Jouet");
            Console.WriteLine($"Le jouet {Jouet.Abonnement} prend {(int)Jouet.Abonnement}h à fabriquer");

            //****Classe File****
            Console.WriteLine("-------------Tests classe File");
            //Créer une file vide d'entiers: 
            File<int> fileTest = new File<int> {};

            //Enfilement des valeurs :
            fileTest.Enfile(2);
            fileTest.Enfile(3);
            fileTest.Enfile(4);
            fileTest.Enfile(5);
            fileTest.Affiche();
            
            //Defilement d'une valeur : 
            Console.WriteLine(fileTest.Defile());
            fileTest.Affiche();
            Console.WriteLine($"{fileTest.EstVide()}");

            //Vidage de la file
            fileTest.Vide();
            fileTest.Affiche();
            //fileTest.Defile(); --> renvoie une erreur 

            //****Classe Pile****
            Console.WriteLine("-------------Tests classe Pile");
            //Créer une pile vide d'entiers :
            Pile<int> pileTest = new Pile<int> {};

            //Empilement des valeurs
            pileTest.Empile(2);
            pileTest.Empile(3);
            pileTest.Empile(4);
            pileTest.Empile(5);
            pileTest.Affiche();

            //Dépilement d'une valeur :
            Console.WriteLine(pileTest.Depile());
            pileTest.Affiche();
            Console.WriteLine($"{pileTest.EstVide()}");

            //Vidage de la pile :
            pileTest.Vide();
            pileTest.Affiche();
            //pileTest.Depile(); --> renvoie une erreur

            //****Classe Lettres****
            Console.WriteLine("-------------Tests classe Lettres");

            //Création 
            Lettre lettreTest = new Lettre(7, "Robert", "LEROI", Continents.Afrique, "8 rue Charle De Gaule"); 

            //Affichage
            lettreTest.Affiche();

            //Création et affichage d'une lettre dans la pile des lettres du père Noël
            CreerLettre(pileLettres);
            pileLettres.Affiche();



            //--------Rémi--------//


            //****Classe Nain****
            Console.WriteLine("-------------Tests classe Nain");

            //Création d'un nain test
            Nain nain1 = new Nain();

            //Teste
            nain1.InitEmballage(lettreTest);
            nain1.Emballage();
            nain1.LettreActuelle.Affiche();

            //****Classe Elfe et Traineau****
            Console.WriteLine("-------------Tests classe Elfe et Traineau");


            //Création d'un elfe et donc d'un traîneau
            Elfe elfe1 = new Elfe(Continents.Afrique, 230);

            //Ajoute une lettre emballée au traineau
            elfe1.AjouteTraineau(nain1.Emballage());
            elfe1.TraineauCont.PileCadeaux.Affiche();

            //Test si le traîneau est plein
            Console.WriteLine($"Le traîneau est'il plein ?  {elfe1.TraineauCont.Plein()}");

            //Teste si le traîneau part avec la fonction Depart
            elfe1.TraineauCont.Depart();
            Console.WriteLine($"Test si le traîneau est parti après la fonction Depart ?  {elfe1.TraineauCont.Parti}");

            // Teste de la fonction EnVoyage
            elfe1.TraineauCont.EnVoyage();
            elfe1.TraineauCont.TempsAvantRetour = 0;
            elfe1.TraineauCont.EnVoyage();
            Console.WriteLine($"Le traîneau est-il encore en voyage ? {elfe1.TraineauCont.Parti}");
            Console.WriteLine("Regardons la pile des lettres suite au retour du traîneau :");
            elfe1.TraineauCont.PileCadeaux.Affiche();
        }
    }

}

