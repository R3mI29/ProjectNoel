using System.ComponentModel;
using System.Linq.Expressions;
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
        //Auteur : Rémi, Tancrède
        //Utilité : La class Param sert à demander à l'utilisateur de definir les valeurs qui vont servir le programme durant la suite 
        // de son fonctionement.
        public class Param
        {
            public int NBLutins { get; set; } // Nombre maximum de Lutins que l'utilisateur autorise
            public int NBNains { get; set; } // Nombre maximum de Nains que l'utilisateur autorise
            public int NBJouetsParTraineau { get; set; } // Nombre maximum de jouets par traineau autorisés par l'utilisateur
            public int NBEnfants { get; set; } // Nombre d'enfants qui demandent des cadeaux au Père Noël
            public int NBLettresParHeures { get; set; } // Nombre maximum de lettres que le Père Noël reçoit par heure
            public Param()
            {
                Console.WriteLine("Veuillez donner le nombre maximum de Lutins souhaité :");
                NBLutins = DemandeInt(); // Demande la valeur de la variable à l'utilisateur en appelant la fonction DemandeInt() 
                Console.WriteLine("Veuillez donner le nombre maximum de Nains souhaité :");
                NBNains = DemandeInt();
                Console.WriteLine("Veuillez donner la capactié des traineaux (le nombre maximum de jouets qu'ils peuvent transporter) :");
                NBJouetsParTraineau = DemandeInt();
                Console.WriteLine("Veuillez donner le nombre d'enfants envoyant des lettres au Père Noël :");
                NBEnfants = DemandeInt();
                bool temp = false;
                while(temp != true)
                {
                    Console.WriteLine("Veuillez donner le nombre maximum de lettres par heure reçu par les Lutins :");
                    int temp1 = DemandeInt();
                    if(temp1 > NBEnfants)
                    {
                        Console.Clear();
                        Console.WriteLine("Le nombre de lettres par heure est supérieur au nombres d'enfants, veuillez rentrer une autres valeurs.");
                    }
                    else
                    {
                        NBLettresParHeures = temp1;
                        temp = true;
                    }
                }
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
            public Lettre? Travaille()
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


        //---------------------------------------------Classe Traineau---------------------------------------------//
        // Auteur : Rémi, Tancrède
        // Utilité : La classe Traineau sert à Initialiser les traineaux qui serviront pour la livraison des cadeaux
        public class Traineau
        {
            public Continents Continent { get; set; }
            public int CapaciteMax { get; set; }
            public Pile<Lettre> PileCadeaux { get; set; }

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

        }   


        //---------------------------------------------Classe Elfe---------------------------------------------//
        // Auteur : Rémi, Tancrède
        // Utilité : La classe Elfe sert à créer les elfes qui vont servir à charger les traineaux. Chaque elfe a son continent.
        public class Elfe
        {
            //Continent duquel l'Elfe s'occupe
            public Continents Continent {get; set;}
            //Traineau de l'Elfe
            public Traineau TraineauCont {get; set;}
            //Entrepot auquel l'Elfe est rattaché
            public Entrepot EntrepotElfe {get; set;}
            //Pile d'attente des lettres que l'elfe traite
            public Pile<Lettre> PileAtttente {get; set;}
            //Etat  de travail de l'elfe
            public EtatTravail Statut {get; set;}
            //Temps restant au voyage avant le retour de l'elfe
            public int TempsAvantRetour { get; set; }

            //Constructeur
            public Elfe(Continents continent, Entrepot entrepot)
            {
                Continent = continent;
                //On choisit la capacité des traineaux en lettres ici.
                TraineauCont = new Traineau(10, continent);
                EntrepotElfe = entrepot;
                PileAtttente = new Pile<Lettre> {};
                Statut = EtatTravail.ChargementCadeaux;
            }

            //----Méthode Depart----//
            //Auteur : Tancrède, Rémi
            //Description : Prépare le départ de l'elfe et de son traineau pour le voyage
            public void Depart()
            {
                Statut = EtatTravail.EnVoyage;
                TempsAvantRetour = 6;
            }

            //Auteur : Rémi, Tancrède
            //Fonction/Class : EnVoyages
            //Renvoie : Void
            //Utilité : Simule une heure de voyage : décompte une heure si le voyage n'est pas fini, sinon on vide les cadeaux dans l'entrepôt et 
            //On fait revenir l'elfe
            public void Voyage()
            {
                if (Statut == EtatTravail.EnVoyage && TempsAvantRetour <= 0)     // test si le voyage est fini.
                {
                    if(TempsAvantRetour <= 0)
                    {
                        //On livre les cadeaux dans l'entrepôt              
                        while (TraineauCont.PileCadeaux.Taille > 0)
                        {
                            EntrepotElfe.AjouterStock(TraineauCont.PileCadeaux.Depile());
                        }
                        //L'elfe revient
                        Statut = EtatTravail.ChargementCadeaux;    
                    }
                    else
                    {
                        //Sinon on décompte une heure du temps de voyage
                        TempsAvantRetour--;
                    }
                    
                }
                
            }

            //Auteur : Rémi, Tancrède
            //Fonction/Class : AjouteTraineau
            //Paramètres : lettre (Lettre)
            //Renvoie : Void
            //Utilité : La fonction ajoute au traineau le cadeau que l'elfe a avec lui et l'envoie en voyage si le traineau est plein.
            public void AjouteTraineau()
            {
                //On ajoute un maximum de cadeaux au traineau (tant que le traineaux n'est pas plein et qu'il y a des cadeaux à ajouter)
                while(PileAtttente.Taille > 0 && TraineauCont.PileCadeaux.Taille < TraineauCont.CapaciteMax)
                    {
                        TraineauCont.ChargeTraineau(PileAtttente.Depile());
                    }
                
                //Si on a rempli le traineau, on part en voyage
                if(TraineauCont.Plein() == true)
                {
                    Depart();
                }
            }

            //----Méthode Travaille----//
            //Auteur : Tancrède
            //Description : Fonction qui fait travailler l'elfe : qui lance le voyage si le traineau est plein et sinon remplit le traineau
            public void Travaille()
            {
                if (Statut == EtatTravail.EnVoyage)
                {
                    Voyage();
                }
                else
                {
                    AjouteTraineau();
                }
            }
        }


        //---------------------------------------------Classe Entrepot---------------------------------------------//
        // Auteur : Rémi
        // Utilité : La classe entrepot sert à fabriquer et stocker les jouets qui arrivent dans les entrepôts des 5 continents.
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
            public void AjouterStock(Lettre lettre)
            {
                StockJouet.Empile(lettre); // Ajoute les jouets du traineau à la pile de l'entrepôt
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
            public Param ParamSimulation;

            //Variable aléatoire de la simulation
            Random Randomizator = new Random(); // Initialise random

            //Pile de lettres sur le bureau du Père Noël
            public Pile<Lettre> LettresBureauPereNoel {get; set;}

            //File des jouets fabriqués par les lutins mais en attente des nains
            public File<Lettre> FileAttenteNain {get; set;}

            //File d'attente des lettres envoyées aux Lutins chaque heure
            public File<Lettre> FileAttenteLutin {get; set;}


            //Jouets stockés dans l'entrepot d'Asie
            public Entrepot EntrepotAsie {get; set;}

            //Jouets stockés dans l'entrepot d'Europe
            public Entrepot EntrepotEurope {get; set;}
            
            //Jouets stockés dans l'entrepot d'Amerique
            public Entrepot EntrepotAmerique {get; set;}

            //Jouets stockés dans l'entrepot d'Afrique
            public Entrepot EntrepotAfrique {get; set;}

            //Jouets stockés dans l'entrepot d'Oceanie
            public Entrepot EntrepotOceanie {get; set;}

            //File des Nains
            public File<Nain> FileNains {get; set;}

            //File des Lutins
            public File<Lutin> FileLutins {get; set;}

            //File des Elfes
            public File<Elfe> FileElfes ;

            //Coût des travailleurs
            public double CoutHeure;
            public double CoutTotal; //Pour le bilan à la fin

            //Compteur des jours qui s'écoulent dans la simulation.
            public int NBJour {get; set;}


            //Constructeur
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

                    LettresBureauPereNoel = new Pile<Lettre>{};

                    FileLutins = new File<Lutin> {} ;

                    FileNains = new File<Nain> {};

                    FileElfes = new File<Elfe> {};

                    FileAttenteNain = new File<Lettre> {};

                    FileAttenteLutin = new File<Lettre>{};

                    EntrepotAsie = new Entrepot(Continents.Asie);
                    EntrepotAfrique = new Entrepot(Continents.Afrique);
                    EntrepotAmerique = new Entrepot(Continents.Amerique);
                    EntrepotEurope = new Entrepot(Continents.Europe);
                    EntrepotOceanie = new Entrepot(Continents.Oceanie);


                    
                }
            }
            //----Méthode CreationTravailleurs----//
            //Auteur : Tancrède
            //Description : Fonction remplissant les différentes files de travailleurs (Nains et Lutins)
            public void CreationTravailleurs()
            {
                //Ajout du nombre de Lutins nécessaires dans la file des lutins
                for (int i = 0; i < ParamSimulation.NBLutins; i++)
                {
                    FileLutins.Enfile(new Lutin());
                }

                //On fait la même chose pour les Nains
                for (int i = 0; i < ParamSimulation.NBNains; i++)
                {
                    FileNains.Enfile(new Nain());
                }
                
                //Les elfes ont étés initialisés dans le constructeur
                //Et finalement de même pour les Elfes
                FileElfes.Enfile(new Elfe(Continents.Europe, EntrepotEurope));
                FileElfes.Enfile(new Elfe(Continents.Asie, EntrepotAsie));
                FileElfes.Enfile(new Elfe(Continents.Amerique, EntrepotAmerique));
                FileElfes.Enfile(new Elfe(Continents.Oceanie, EntrepotOceanie));
                FileElfes.Enfile(new Elfe(Continents.Afrique, EntrepotAfrique));
            }

            //----Méthode CreationLettres----//
            //Auteur : Tancrède
            //Description : Fonction qui créer les lettres utilisées par la simulation.
            public void CreationLettres()
            {
                int nbLettresAleatoire = Randomizator.Next(ParamSimulation.NBLettresParHeures);
                
                while(nbLettresAleatoire > 0 && ParamSimulation.NBEnfants > 0)
                {
                    LettresBureauPereNoel.Empile(CreerLettre());
                    ParamSimulation.NBEnfants --;
                    nbLettresAleatoire--;
                }
            }


            //----Méthode CompteCoutHeure----//
            //Auteur : Rémi
            //Description : Fonction qui renvoie combien les travailleurs coûtent au Père Noël pendant l'heure actuelle.
            public void CompteCoutHeure()
            {
                //Variable qui stocke le coût
                double Cout = 0.0;
                
                //On défile la file des Lutins NBLutin fois et on ajoute leur coût.
                for(int i = 0; i < ParamSimulation.NBLutins; i++)
                {
                    Lutin l = FileLutins.Defile();
                    if(l.Statut == EtatTravail.Travail)
                    {
                        Cout = Cout + 1.5;
                    }
                    else
                    {
                        Cout = Cout + 1.0;
                    }
                    FileLutins.Enfile(l);
                }
                //On défile la file des Nains NBNain fois et on ajoute leur coût.
                for(int i = 0; i < ParamSimulation.NBNains; i++)
                {
                    Nain n = FileNains.Defile();
                    if(n.Statut == EtatTravail.Travail)
                    {
                        Cout = Cout + 1.0;
                    }
                    else
                    {
                        Cout = Cout + 0.5;
                    }
                    FileNains.Enfile(n);
                }
                //On défile la file des Elfes 5 fois et on ajoute leur coût.
                for(int i = 0; i < 4 ; i++)
                {
                    Elfe e = FileElfes.Defile();
                    if(e.Statut == EtatTravail.ChargementCadeaux)
                    {
                        Cout = Cout + 1.5;
                    }
                    else if(e.Statut == EtatTravail.EnVoyage)
                    {
                        Cout = Cout + 2.0;
                    }
                }
            }
        

            //----Méthode PasserHeure----//
            //Auteur : Rémi, Tancrède
            //Description : Fonction qui fait passer une heure aux fonctions.
            public void PasserHeure()
            {
                //--Gestion des Enfants
                CreationLettres();

                //--Gestion des Lutins
                for(int i = 0; i < ParamSimulation.NBLutins; i++)
                {
                    Lutin l = FileLutins.Defile(); // Selectionne un lutin
                    if(l.Statut == EtatTravail.Attente && !LettresBureauPereNoel.EstVide())//Si le lutin est disponible et qu'il y a une lettre à traiter, le faire travailler
                    {
                        Lettre LettresATraiter = LettresBureauPereNoel.Depile();            // Prendre une lettre sur le bureau du père noël
                        l.DebutFabricationJouet(LettresATraiter);                           // Le lutin commence la fabrication du jouet
                    }
                    Lettre? JouetFini = l.Travaille(); // Le jouet du lutin une fois fini

                    if(JouetFini != null)
                    {
                        FileAttenteNain.Enfile(JouetFini); // Ajoute le jouet du lutin à la file des jouets que les nains doivent traiter
                    }
                    FileLutins.Enfile(l);   // Renvoie le lutin dans la pile des lutins qui ne travaillent pas
                }

                //--Gestion Nains
                Pile<Lettre> CadeauxEmballesCeTour = new Pile<Lettre>{}; // Liste des cadeaux emballés par les nains durant ce tour
                for(int i = 0; i < ParamSimulation.NBNains; i++)
                {
                    Nain n = FileNains.Defile(); // Selectionne un nain
                    if(n.Statut == EtatTravail.Attente && !FileAttenteNain.EstVide()) //Si le nain est disponible et qu'il y a un jouet à emballer, le faire travailler.
                    {
                        n.InitEmballage(FileAttenteNain.Defile()); //Lance le travail du nain sur le cadeau
                    }
                    Lettre cadeauEmballe = n.Emballage();
                    if(cadeauEmballe != null) // Si le cadeau est emballé
                    {
                        CadeauxEmballesCeTour.Empile(cadeauEmballe); //On l'ajoute aux cadeaux emballés ce tour
                    }

                    FileNains.Enfile(n); //Renvoie le nain en état d'attente après qu'il ait travaillé
                }

                //--Gestion Elfes
                for(int i = 0; i < 5; i++)
                {
                    Pile<Lettre> CadeauxAutreContinent = new Pile<Lettre>{}; // Créer une pile de cadeaux pour les cadeaux qui n'ont pas encore été récupérés par l'elfe de leur continent.
                    Elfe e = FileElfes.Defile(); //Prends l'elfe qui est à la fin de la file
                    while(!CadeauxEmballesCeTour.EstVide()) // Tant que la première pile de cadeaux n'est pas vide, continuer de vérifier si l'elfe actuel ne peut pas les prendre
                    {
                        Lettre cadeau = CadeauxEmballesCeTour.Depile(); // Sélectionne le cadeau le plus en haut de la pile
                        if(e.Continent == cadeau.Continent) // on regarde si l'elfe a le même continent que le cadeau
                        {
                            e.PileAtttente.Empile(cadeau); // Si oui, on ajoute le cadeau à la pile de l'elfe
                        }
                        else
                        {
                            CadeauxAutreContinent.Empile(cadeau); //Si non, on ajoute le cadeau à la deuxième pile de cadeaux
                        }
                    }
                    FileElfes.Enfile(e); // On remet l'elfe dans la file des elfes
                    while(!CadeauxAutreContinent.EstVide())
                    {
                        CadeauxEmballesCeTour.Empile(CadeauxAutreContinent.Depile()); // On remet les cadeaux qui étaient dans la deuxième pile, dans la première.
                    }
                }
            }

            //----Méthode EstTermine----//
            //Auteur : Tancrède 
            //Description : Fonction qui renvoie false dans que la simulation n'est pas terminée et true sinon.
            //La simulation est terminée si :
            //      - Il n'y a plus de nouvelles lettres à envoyer
            //      - Les files d'attentes sont vides
            //      - Tous les travailleurs sont soit au repos, soit en attente
            //      - Tous les traineau ont complété leur dernière livraison (et sont donc vides)
            public bool EstTermine()
            {
                //Vérifie si aucun des lutins ne sont en train de travailler
                for(int i = 0 ; i < ParamSimulation.NBLutins ; i++)
                {
                    Lutin l = FileLutins.Defile();
                    FileLutins.Enfile(l);
                    if (l.Statut == EtatTravail.Travail){return false;}
                }

                //Vérifie si aucun des nains ne sont en train de travailler
                for(int i = 0 ; i < ParamSimulation.NBNains ; i++)
                {
                    Nain n = FileNains.Defile();
                    FileNains.Enfile(n);
                    if (n.Statut == EtatTravail.Travail){return false;}
                }

                //Vérifie si aucun des elfes ne sont en train de travailler
                for(int i = 0 ; i < 5 ; i++)
                {
                    Elfe e = FileElfes.Defile();
                    FileElfes.Enfile(e);
                    if (e.TraineauCont.PileCadeaux.Taille > 0 //Le traineau est-il vide
                    ||  e.Statut != EtatTravail.EnVoyage //L'elfe est-il toujours en voyage ?
                    ||  e.PileAtttente.Taille > 0)//Reste-t-il des cadeaux à livrer
                        {return false;}
                    
                }
                return  ParamSimulation.NBEnfants <= 0 &&//Reste-t-il des lettres à traiter
                        FileAttenteLutin.EstVide() && //Y a-t-il des lettres en attente pour les lutins
                        FileAttenteNain.EstVide();//Y a-t-il des lettres en attente pour les nains
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
        //Description : Fonction qui génére une lettre aléatoire et la retourne
        public static Lettre CreerLettre()
        {
            Random random = new Random(); // Initialise random
            // Initialise une liste des prénoms (les prénoms les plus donnés en france en 2024)
            string[] ListePrenoms = {"Gabriel", "Léo", "Maël", "Noah", "Jules", "Adam", "Louis", "Jade", "Louise", "Lola", "Emma", "Lou", "Tibo"}; 
            // Initialise une liste des noms (noms aléatoire)
            string[] ListeNoms = {"Dupont", "Martin", "Inshape", "Papin", "Bernard", "Robert", "Leroy", "Lefèvre", "Millot", "Girard", "Moreau", "Simon", "Kirk", "Durand", "Dubois"}; 
            // Initialise une liste d'adresse (adresses aléatoire)
            string[] ListeAdresse = { "12 Rue de la République, 75001 Paris", "45 Avenue Jean Jaurès, 31000 Toulouse", "78 Boulevard de la Liberté, 69003 Lyon", "33 Rue des Fleurs, 13006 Marseille", "15 Place de la Comédie, 34000 Montpellier", "22 Rue du Commerce, 44000 Nantes", "56 Avenue des Champs-Élysées, 75008 Paris", "9 Rue de la Gare, 67000 Strasbourg", "101 Boulevard de la Mer, 06200 Nice", "8 Rue du Marché, 59800 Lille" };
            string prenom = ListePrenoms[random.Next(ListePrenoms.Length)]; // Prend un prénoms aléatoire dans la liste des prénoms
            string nom = ListeNoms[random.Next(ListeNoms.Length)];// Prend un nom aléatoire dans la liste des nom
            Continents continent = RandomContinent(random.Next(4));// Prend un nombre aléatoire et en fait un Continent
            int age = random.Next(18); // Prend un âge aléatoire entre 0 et 18 ans et le transforme en jouet
            string adresse = ListeAdresse[random.Next(ListeAdresse.Length)];// Prend une adresse aléatoire dans la liste des adresses
            Lettre lettreAléatoire = new Lettre(age,nom, prenom, continent, adresse); // Créer la lettre avec les valeurs aléatoires plus hauts
            //On retourne la lettre
            return lettreAléatoire;
        }


        public static void Main()
        {
            Simulation simulation = new Simulation(); 
            simulation.CreationTravailleurs();
            simulation.CreationLettres();
            while (simulation.EstTermine() == false)//Tant que toutes les lettres ne sont pas envoyées
            {
                simulation.PasserHeure();
            }
        }
    }

}
