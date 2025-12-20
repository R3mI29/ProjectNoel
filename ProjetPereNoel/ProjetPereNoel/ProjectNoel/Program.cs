using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
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
        //Utilité : La classe Param sert à demander à l'utilisateur de définir les valeurs qui vont servir le programme durant la suite 
        // de son fonctionnement.
        public class Param
        {
            public int NBLutins { get; set; } // Nombre maximum de Lutins que l'utilisateur autorise
            public int NBNains { get; set; } // Nombre maximum de Nains que l'utilisateur autorise
            public int NBJouetsParTraineau { get; set; } // Nombre maximum de jouets par traineau autorisés par l'utilisateur
            public int NBEnfants { get; set; } // Nombre d'enfants qui demandent des cadeaux au Père Noël
            public int NBLettresParHeures { get; set; } // Nombre maximum de lettres que le Père Noël reçoit par heure
            public Param()
            {
                Console.Clear();
                Console.WriteLine("Veuillez donner le nombre maximum de Lutins souhaité :");
                NBLutins = DemandeInt(); // Demande la valeur de la variable à l'utilisateur en appelant la fonction DemandeInt() 
                Console.WriteLine("Veuillez donner le nombre maximum de Nains souhaité :");
                NBNains = DemandeInt();
                Console.WriteLine("Veuillez donner la capacité des traineaux (le nombre maximum de jouets qu'ils peuvent transporter) :");
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
                        Console.WriteLine("Le nombre de lettres par heure est supérieur au nombre d'enfants, veuillez rentrer une autres valeurs.");
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
            //Description : fonction qui affiche le contenu de la file et son sens de défilement
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
            //Description : affiche le contenu de la pile et le sens dans lequel elle va.
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
        //Description : classe représentant les lutins, leurs attributs et leurs méthodes qui leur sont utiles pour la fabrication des cadeaux
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
        // Utilité : La classe Nain sert à créer les nains, ils sont ceux qui emballent les cadeau pour les donnés aux elfes.
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
            //Utilité : La fonction renvoie les lettres finies et garde le nain occupé aussi longtemps que necessaire.
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
        // Utilité : La classe Traineau sert à initialiser les traineaux qui serviront pour la livraison des cadeaux
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
            //Utilité : La fonction renvoie un bool qui nous dit si le traineau est plein
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
            public Pile<Lettre> PileAttente {get; set;}
            //Etat  de travail de l'elfe
            public EtatTravail Statut {get; set;}
            //Temps restant au voyage avant le retour de l'elfe
            public int TempsAvantRetour { get; set; }

            //Constructeur
            public Elfe(Continents continent, Entrepot entrepot, int capaciteTraineau)
            {
                Continent = continent;
                //On choisit la capacité des traineaux en lettres ici.
                TraineauCont = new Traineau(capaciteTraineau, continent);
                EntrepotElfe = entrepot;
                PileAttente = new Pile<Lettre> {};
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
                if (Statut == EtatTravail.EnVoyage)// test si le voyage est fini.
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
                while(PileAttente.Taille > 0 && TraineauCont.PileCadeaux.Taille < TraineauCont.CapaciteMax)
                    {
                        TraineauCont.ChargeTraineau(PileAttente.Depile());
                    }
                
                //Si on a rempli le traineau, on part en voyage
                if(TraineauCont.Plein() == true )
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
        // Auteur : Rémi, Tancrède
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


            //Auteur : Tancrède
            //Fonction/Class : EvaluationEntrepot
            //Description : Affiche le contenu d'un entrepôt 
            public void EvaluationEntrepot ()
            {
                Pile<Lettre> pileTemp = new Pile<Lettre> {};

                //On stocke les valeurs du nombre de jouet par tranche d'âge dans un tableau
                // position [0] : de 0 à 2 ans, position [1] de 3 à 5 ans et ainsi de suite...
                int[] tabNbJouetParTrancheDage = [0, 0, 0, 0, 0];//On initialise toutes les valeurs à 0.

                //On compte le nombre de jouets par tranche d'âge dans le stock de l'entrepôt
                while(StockJouet.Taille > 0)
                {
                    Lettre l = StockJouet.Depile();
                    Jouet j = AgeToJouet(l.Age);
                    if (j == Jouet.Nounours){tabNbJouetParTrancheDage[0] = tabNbJouetParTrancheDage[0] + 1;}
                    else if (j == Jouet.Tricycle){tabNbJouetParTrancheDage[1] = tabNbJouetParTrancheDage[1] + 1;}
                    else if (j == Jouet.Jumelles){tabNbJouetParTrancheDage[2] = tabNbJouetParTrancheDage[2] + 1;}
                    else if (j == Jouet.Abonnement){tabNbJouetParTrancheDage[3] = tabNbJouetParTrancheDage[3] + 1;}
                    else if (j == Jouet.Ordinateur){tabNbJouetParTrancheDage[4] = tabNbJouetParTrancheDage[4] + 1;}
                    pileTemp.Empile(l);
                }

                //On remet tous les jouets dans l'entrepôt
                while(pileTemp.Taille > 0)
                {
                    StockJouet.Empile(pileTemp.Depile());
                }

                //----Affichage des résultats
                Console.WriteLine($"----------Contenu de l'entrepôt en {Continent}");
                Console.WriteLine($"Nombre de jouets pour les enfants de 0 à 2 ans : {tabNbJouetParTrancheDage[0]}");
                Console.WriteLine($"Nombre de jouets pour les enfants de 3 à 5 : {tabNbJouetParTrancheDage[1]}");
                Console.WriteLine($"Nombre de jouets pour les enfants de 6 à 10 ans : {tabNbJouetParTrancheDage[2]}");
                Console.WriteLine($"Nombre de jouets pour les enfants de 11 à 15 ans : {tabNbJouetParTrancheDage[3]}");
                Console.WriteLine($"Nombre de jouets pour les enfants de 16 à 18 ans : {tabNbJouetParTrancheDage[4]}");
            }
        }
        


        
        //---------------------------------------------Classe Simulation---------------------------------------------//
        // Auteur : Tancrède et Rémi
        // Utilité : La classe Simulation sert à lancer le logiciel et à faire marcher toutes les classes et fonctions ensemble.
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

            //Pour le bilan à la fin
            public double CoutTotal; 

            //Compteur des jours qui s'écoulent dans la simulation.
            public int NBJour {get; set;}

            //Condition de relance de la simulation
            public bool VeutRelancer {get; set;} 

            //Condition pour quitter la simulation
            public bool VeutArrêter {get; set;}

            //Compteur d'heures qui s'écoulent dans la simulation.
            public int NBHeure {get; set;}

            //Temps de repos des nains et des lutins
            public int tempsReposLutins;
            public int tempsReposNains;


            //Constructeur
            public Simulation()
            {
                //Nettoie l'affichage
                Console.Clear();

                //Booléen pour sortir la boucle
                bool continuer = true;

                //Message de bienvenue
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
                        this.VeutArrêter = true;
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
                
                //Les elfes ont été initialisés dans le constructeur
                //Et finalement de même pour les Elfes
                FileElfes.Enfile(new Elfe(Continents.Europe, EntrepotEurope, ParamSimulation.NBJouetsParTraineau));
                FileElfes.Enfile(new Elfe(Continents.Asie, EntrepotAsie, ParamSimulation.NBJouetsParTraineau));
                FileElfes.Enfile(new Elfe(Continents.Amerique, EntrepotAmerique, ParamSimulation.NBJouetsParTraineau));
                FileElfes.Enfile(new Elfe(Continents.Oceanie, EntrepotOceanie, ParamSimulation.NBJouetsParTraineau));
                FileElfes.Enfile(new Elfe(Continents.Afrique, EntrepotAfrique, ParamSimulation.NBJouetsParTraineau));
            }

            //----Méthode CreationLettres----//
            //Auteur : Tancrède
            //Description : Fonction qui crée les lettres utilisées par la simulation.
            public void CreationLettres()
            {
                int nbLettresAleatoire = Randomizator.Next(1,ParamSimulation.NBLettresParHeures + 1);
                
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
            public double CompteCoutHeure()
            {
                //Variable qui stocke le coût
                double Cout = 0.0;
                
                //On défile la file des Lutins NBLutin fois et on ajoute leur coût en fonction de leur activité.
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
                //On défile la file des Nains NBNain fois et on ajoute leur coût en fonction de leur activité.
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
                //On défile la file des Elfes 5 fois et on ajoute leur coût en fonction de leur activité.
                for(int i = 0; i < 5 ; i++)
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
                    FileElfes.Enfile(e);
                }
                return Cout;
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
                bool finPrepaCadeau = FinPreparationCadeau();
                for(int i = 0; i < 5; i++)
                {
                    Pile<Lettre> CadeauxAutreContinent = new Pile<Lettre>{}; // Créer une pile de cadeaux pour les cadeaux qui n'ont pas encore été récupérés par l'elfe de leur continent.
                    Elfe e = FileElfes.Defile(); //Prends l'elfe qui est à la fin de la file
                    while(!CadeauxEmballesCeTour.EstVide()) // Tant que la première pile de cadeaux n'est pas vide, continuer de vérifier si l'elfe actuel ne peut pas les prendre
                    {
                        Lettre cadeau = CadeauxEmballesCeTour.Depile(); // Sélectionne le cadeau le plus en haut de la pile
                        if(e.Continent == cadeau.Continent) // on regarde si l'elfe a le même continent que le cadeau
                        {
                            e.PileAttente.Empile(cadeau); // Si oui, on ajoute le cadeau à la pile de l'elfe
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
                    if (DernierVoyage(e) == true)
                    {
                        e.Depart();
                    }
                    e.Travaille();
                }
            }

            //----Méthode FinPreparationCadeau----//
            //Auteur : Tancrède
            //Description : Vérifie que toutes les lettres ont étés traitées et que tous les cadeaux ont bien étés préparés et
            //Qu'ils sont donc au moins dans la file d'attente de leur elfe respectif
            public bool FinPreparationCadeau()
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
                return  ParamSimulation.NBEnfants <= 0 &&//Reste-t-il des lettres à traiter
                        FileAttenteLutin.EstVide() && //Y a-t-il des lettres en attente pour les lutins
                        LettresBureauPereNoel.EstVide() && //Y a-t-il des lettres en attente sur le bureau du père noël
                        FileAttenteNain.EstVide();//Y a-t-il des lettres en attente pour les nains
            }
            //----DernierVoyage----//
            //Auteur : Tancrède
            //Description : regarde si c'est bien le dernier voyage que l'elfe va effectuer
            //Renvoie true si oui et false sinon
            public bool DernierVoyage(Elfe e)
            {
                //Vérifie si c'est bien le dernier voyage de l'elfe
                if (e.PileAttente.Taille > 0 || e.Statut == EtatTravail.EnVoyage)//Reste-t-il des cadeaux à livrer (en dehors de ceux dans le traineau)
                    {return false;}
                    
                return e.TraineauCont.PileCadeaux.Taille > 0;//L'elfe a-t-il encore des cadeaux dans son traineau
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
                //Vérifie si tous les elfes ont finis de livrer les cadeaux
                for(int i = 0 ; i < 5 ; i++)
                {
                    Elfe e = FileElfes.Defile();
                    FileElfes.Enfile(e);
                    if (e.TraineauCont.PileCadeaux.Taille > 0 //Le traineau est-il vide
                    ||  e.Statut == EtatTravail.EnVoyage //L'elfe est-il toujours en voyage ?
                    ||  e.PileAttente.Taille > 0)//Reste-t-il des cadeaux à livrer
                        {return false;}
                    
                }

                //Vérifie si tous les cadeaux ont bien été traités
                return FinPreparationCadeau();
            }
            
            //----Méthode LancerSimulation----//
            //Auteur : Rémi 
            // Description : Cette fonction lance la gestion des jours et des heures, en affichant pour chaque heure un résumé de ce qu'il s'est passé durant cette dernière.
            //               La fonction fait aussi l'ajout des coûts des travailleurs.
            public void Lancer()
            {
                this.NBJour = 1; //Initialise le nombre de jour à 1
                this.CoutTotal = 0; //Initialise le nombre de pièces dues à 1
                while (this.EstTermine() == false && this.VeutRelancer == false) //Tant que ce n'est pas fini ou que l'utilisateur ne veut pas quitter
                {
                    for (int heure = 1; heure <= 12; heure++)//Passe les heures de la journée
                    {
                        // On calcule ce qui se passe durant cette heure
                        this.PasserHeure();
                        this.CoutHeure = this.CompteCoutHeure();
                        this.CoutTotal += this.CompteCoutHeure();
                        this.NBHeure ++;

                        // On affiche le menu et on demande le choix
                        string? temp = AfficherBilanHeure(heure, NBJour);


                        // Gestion de cas où le dernier cadeau a été livré
                        if (this.EstTermine() == true)
                        {
                            Console.WriteLine("\n--------------------------------------------------");
                            Console.WriteLine("Dernière lettre traitée au Jour " + this.NBJour + " à l'heure " + heure + " !");
                            Console.WriteLine("--------------------------------------------------");
                            Console.WriteLine("Appuyez sur une touche pour quitter...");
                            Console.ReadLine();
                            AfficherBilanFinal();
                            return;
                        }
                        // Gestion du saut de jour
                        else if(temp == "jour")
                        {
                            Console.WriteLine(">>> Avance rapide jusqu'à la fin de la journée... Veuillez patienter.");
                            
                            // On boucle sur les heures restantes
                            for (int hRestante = heure ; hRestante <= 12; hRestante++)
                            {
                                // On fait travailler tout le monde sans afficher le menu
                                this.PasserHeure();
                                this.CoutHeure = this.CompteCoutHeure();
                                this.CoutTotal += this.CoutHeure;
                                this.NBHeure ++;

                                // Il faut quand même vérifier si la simulation se termine pendant l'avance rapide
                                if (this.EstTermine() == true)
                                {
                                    Console.WriteLine($"\nLa simulation s'est terminée pendant l'avance rapide au jour {NBJour}, heure {hRestante}.");
                                    Console.ReadLine();
                                    AfficherBilanFinal();
                                    return;
                                }
                            }
                            heure = 12;// On règle la valeur à 12 car, ainsi, la boucle "for" réinitialisera l'heure à 0 et incrémentera le jour (jour + 1).
                        }
                        // Gestion du cas où l'utilisateur veut quitter la simulation
                        else if(temp == "quitter")
                        {
                            AfficherBilanFinal();
                            this.VeutArrêter = true;
                            return;
                        }
                        // Gestion du cas où l'utilisateur veut relancer une simulation
                        else if(temp == "relancer")
                        {
                            AfficherBilanFinal();
                            this.VeutRelancer = true;
                            return;
                        }
                    }
                    // Fin de la journée
                    Console.WriteLine("========== FIN DU JOUR " + this.NBJour + " ==========");
                    this.NBJour++;
                    Console.WriteLine("Appuyez sur une touche pour commencer le jour suivant...");
                    Console.ReadLine();
                }
            }


            //----Méthode AfficherBilanHeure----//
            //Auteur : Rémi 
            //Description : Bilan de l'heure qui vient de s'écouler, et affichage des infos pratique au père noël
            public string AfficherBilanHeure(int heure, int jour)
            {                
                string? temp = "";
                while(temp != "suivant" && temp != "jour" && temp !="quitter" && temp !="relancer") // Vérifie si l'utilisateur veut avancer le temps ou quitter le bilan
                {
                    Console.Clear();
                    Console.WriteLine("\n=========================================================");
                    Console.WriteLine($"∣∣-------------Bilan de L'heure " + heure + " du Jour "+ jour +"------------∣∣");
                    Console.WriteLine("=========================================================");
                    Console.WriteLine("∣∣-> 1         pour le menu de gestion des nains       ∣∣");
                    Console.WriteLine("∣∣-> 2         pour le menu de gestion des lutins      ∣∣");
                    Console.WriteLine("∣∣-> 3         pour voir les stocks dans les entrepots ∣∣");
                    Console.WriteLine("∣∣-> 4         pour voir le nombre de pièces dépensées ∣∣");
                    Console.WriteLine("∣∣-> suivant   pour passer à l'heure suivante          ∣∣");
                    Console.WriteLine("∣∣-> jour      pour passer au prochain jour            ∣∣");
                    Console.WriteLine("∣∣-> relancer  pour relancer une nouvelle simulation   ∣∣");
                    Console.WriteLine("∣∣-> quitter   pour arrêter la simulation              ∣∣");
                    Console.WriteLine("=========================================================");
                    temp = Console.ReadLine();
                    if(temp == "1") // Si l'utilisateur veut gérer les nains
                    {
                        GestionNains();
                    }
                    else if(temp == "2") // Si l'utilisateur veut gérer les lutins
                    {
                        GestionLutins();
                    }
                    else if(temp == "3")// Si l'utilisateur veut regarder les entrepôts
                    {
                        AfficherEntrepot();
                    }
                    else if(temp == "4")// Si l'utilisateur veut regarder ses dépenses
                    {
                        MenuDesDepences();
                    }
                    else if(temp == "quitter" || temp == "jour" || temp == "suivant" || temp == "relancer") // Si l'utilisateur veut quitter ou avancer la simulation
                    {
                        return temp;
                    }
                }
                return temp;
            }

            //----Méthode MenuDesDepences----//
            //Auteur : Rémi 
            //Description : Affiche les pièces que le Père Noël doit payer à ses travailleurs.
            public void MenuDesDepences()
            {
                Console.Clear();
                Console.WriteLine("\n===============================================================================");
                Console.WriteLine($"∣∣--------------------------Menu de Gestion des Dépenses--------------------∣∣");
                Console.WriteLine("===============================================================================");
                Console.WriteLine("∣∣                                                                          ∣∣");
                Console.WriteLine("∣∣   Nombre de pièces dépensée durant le tour "+this.CoutHeure+"                         ∣∣");
                Console.WriteLine("∣∣   Nombre de pièces dépensée depuis le début "+this.CoutTotal+"                        ∣∣");
                Console.WriteLine("∣∣                                                                          ∣∣");
                Console.WriteLine("===============================================================================");
                Console.WriteLine();
                Console.WriteLine("Appuyez sur une touche pour revenir au menu de l'heure...");
                string? temp = Console.ReadLine();
            }


            //----Méthode AfficherEntrepot----//
            //Auteur : Rémi 
            //Description : Organise la présentation et affiche le contenu des entrepôts.
            public void AfficherEntrepot()
            {
                string? temp = "";
                while(temp != "retour") //Jusqu'au retour au menu principal après consultation du bilan.
                {
                    Console.Clear();
                    Console.WriteLine("\n===============================================================================");
                    Console.WriteLine($"∣∣--------------------------Menu de Gestion des Entrepôts--------------------∣∣");
                    Console.WriteLine("===============================================================================");
                    Console.WriteLine("∣∣-> 1       pour verifier le stock de l'entrepôt en Asie                    ∣∣");
                    Console.WriteLine("∣∣-> 2       pour verifier le stock de l'entrepôt en Europe                  ∣∣");
                    Console.WriteLine("∣∣-> 3       pour verifier le stock de l'entrepôt en Afrique                 ∣∣");
                    Console.WriteLine("∣∣-> 4       pour verifier le stock de l'entrepôt en Amérique                ∣∣");
                    Console.WriteLine("∣∣-> 5       pour verifier le stock de l'entrepôt en Océanie                 ∣∣");
                    Console.WriteLine("∣∣-> retour  pour revenir sur le bilan de l'heure                            ∣∣");
                    Console.WriteLine("===============================================================================");
                    temp = Console.ReadLine();// Attend le choix de l'utilisateur
                    if(temp == "1")
                    {
                        Console.Clear();
                        EntrepotAsie.EvaluationEntrepot();// Affiche le contenu de l'entrepôt d'Asie
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Appuyer sur une touche pour revenir sur le menu de gestion des entrepôts...");
                        Console.ReadLine();
                    }
                    else if(temp == "2")
                    {
                        Console.Clear();
                        EntrepotEurope.EvaluationEntrepot();// Affiche le contenu de l'entrepôt d'Europe
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Appuyer sur une touche pour revenir sur le menu de gestion des entrepôts...");
                        Console.ReadLine();
                    }
                    else if(temp == "3")
                    {
                        Console.Clear();
                        EntrepotAfrique.EvaluationEntrepot();// Affiche le contenu de l'entrepôt d'Afrique
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Appuyer sur une touche pour revenir sur le menu de gestion des entrepôts...");
                        Console.ReadLine();
                    }
                    else if(temp == "4")
                    {
                        Console.Clear();
                        EntrepotAmerique.EvaluationEntrepot();// Affiche le contenu de l'entrepôt d'Amérique
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Appuyer sur une touche pour revenir sur le menu de gestion des entrepôts...");
                        Console.ReadLine();
                    }
                    else if(temp == "5")
                    {
                        Console.Clear();
                        EntrepotOceanie.EvaluationEntrepot();// Affiche le contenu de l'entrepôt d'Océanie
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Appuyer sur une touche pour revenir sur le menu de gestion des entrepôts...");
                        Console.ReadLine();
                    }
                }
            }

            //----Méthode AfficherBilanFinal----//
            //Auteur : Rémi 
            //Description : Affiche le bilan de la simulation lorsqu'on la quitte.
            public void AfficherBilanFinal()
            {
                Console.Clear();
                Console.WriteLine("\n=============================================================================");
                Console.WriteLine($"∣∣-------------------------Bilan Final de la Simulation--------------------∣∣");
                Console.WriteLine("=============================================================================");
                Console.WriteLine("================================== Entrepôts ================================");
                Console.WriteLine($"∣∣ Il y a actuellement {EntrepotAfrique.StockJouet.Taille} Jouet(s) dans l'entrepôt d'Afrique.               ∣∣");//Affiche le nombre de jouets en Afrique
                Console.WriteLine($"∣∣ Il y a actuellement {EntrepotAmerique.StockJouet.Taille} Jouet(s) dans l'entrepôt d'Amérique.              ∣∣");//Affiche le nombre de jouets en Amérique
                Console.WriteLine($"∣∣ Il y a actuellement {EntrepotAsie.StockJouet.Taille} Jouet(s) dans l'entrepôt d'Asie.                  ∣∣");//Affiche le nombre de jouets en Asie
                Console.WriteLine($"∣∣ Il y a actuellement {EntrepotEurope.StockJouet.Taille} Jouet(s) dans l'entrepôt d'Europe.                ∣∣");//Affiche le nombre de jouets en Europe
                Console.WriteLine($"∣∣ Il y a actuellement {EntrepotOceanie.StockJouet.Taille} Jouet(s) dans l'entrepôt d'Océanie.               ∣∣");//Affiche le nombre de jouets en Océanie
                Console.WriteLine("=================================== Dépenses ================================");
                Console.WriteLine($"∣∣ Somme totale des pièces dues aux travailleurs : {this.CoutTotal} euro(s)           ∣∣");//Affiche le nombre de pièces dépensées durant toute la simulation
                Console.WriteLine($"∣∣ Somme moyenne des pièces dues aux travailleurs par heure : {this.CoutTotal / this.NBHeure} euro(s)  ∣∣");//Affiche le nombre de pièces dépensées en moyenne durant une heure de simulation
                Console.WriteLine("============================= Nombre(s) de Jour(s) ==========================");
                Console.WriteLine($"∣∣ Durée de la simulation : {this.NBJour} jours                                        ∣∣");//Affiche le nombre de jours passés dans la simulation
                Console.WriteLine("==================================== Fin ====================================");
                Console.WriteLine();
                Console.WriteLine("Appuyez sur une touche pour passer le bilan final et finir la simulation...");
                string? temp = Console.ReadLine();
                Console.Clear();
            }


            //----Méthode GestionLutins----//
            //Auteur : Tancrède
            //Description : Fonction qui affiche combien de lutins sont au repos, en attente ou en train de travailler
            public void GestionLutins()
            {
                int nbTravail = 0;
                int nbRepos = 0;
                int nbAttente = 0;

                //Nombres de Lutins en train de travailler, au repos et en attente
                for (int i = 0; i < ParamSimulation.NBLutins ; i++)
                {
                    Lutin l = FileLutins.Defile();
                    if (l.Statut == EtatTravail.Travail){nbTravail++;}
                    else if (l.Statut == EtatTravail.Attente){nbAttente++;}
                    else if(l.Statut == EtatTravail.Repos){nbRepos++;}
                    FileLutins.Enfile(l);
                }
                Console.Clear();
                Console.WriteLine("===================================================");
                Console.WriteLine("∣∣                 GESTION DES LUTINS            ∣∣");
                Console.WriteLine("===================================================");
                Console.WriteLine($"∣∣  Il y a {nbTravail} lutins en train de travailler       ∣∣");
                Console.WriteLine($"∣∣  Il y a {nbAttente} lutins en attente                   ∣∣");
                Console.WriteLine($"∣∣  Il y a {nbRepos} lutins au repos                     ∣∣");
                Console.WriteLine("===================================================");
                //Options et traitement des choix
                Console.WriteLine("∣∣            Que voulez vous faire ?            ∣∣");
                Console.WriteLine("===================================================");
                Console.WriteLine("∣∣ -> 1 : Envoyer des lutins au repos            ∣∣");
                Console.WriteLine("∣∣ -> 2 : Envoyer des lutins au travail          ∣∣");
                Console.WriteLine("∣∣ -> retour : Retourner au menu précédent       ∣∣");
                Console.WriteLine("===================================================");
                string? repString = Console.ReadLine();
                int rep ;
                //Tant que l'on ne veut pas retourner au menu d'avant
                while (repString != "retour")
                {
                    //Si on a fait un retour en arrière on réaffiche tout
                    if (repString == "back")
                    {
                        Console.Clear();
                        Console.WriteLine("===================================================");
                        Console.WriteLine("∣∣                 GESTION DES LUTINS            ∣∣");
                        Console.WriteLine("===================================================");
                        Console.WriteLine($"∣∣  Il y a {nbTravail} lutins en train de travailler       ∣∣");
                        Console.WriteLine($"∣∣  Il y a {nbAttente} lutins en attente                   ∣∣");
                        Console.WriteLine($"∣∣  Il y a {nbRepos} lutins au repos                     ∣∣");
                        Console.WriteLine("===================================================");
                        //Options et traitement des choix
                        Console.WriteLine("∣∣            Que voulez vous faire ?            ∣∣");
                        Console.WriteLine("===================================================");
                        Console.WriteLine("∣∣ -> 1 : Envoyer des lutins au repos            ∣∣");
                        Console.WriteLine("∣∣ -> 2 : Envoyer des lutins au travail          ∣∣");
                        Console.WriteLine("∣∣ -> retour : Retourner au menu précédent       ∣∣");
                        Console.WriteLine("===================================================");
                        repString = Console.ReadLine();

                    }
                    //Si la réponse n'est pas valide, on fait resaisir la valeur
                    if ((int.TryParse(repString, out rep) == false || rep < 1 || rep > 3) && repString != "retour")
                    {
                        Console.WriteLine("** /!\\ Veuillez rentrer un entier valide (entre 1 et 3)");
                        repString = Console.ReadLine();
                    }
                    //-------------------------------------------
                    //Si on choisit de mettre des lutins au repos
                    else if(rep == 1)
                    {
                        Console.Clear();
                        //Si on peut mettre des Lutins au repos (çàd s'il y en a en attente)
                        if(nbAttente > 0)
                        {
                            Console.WriteLine($"Combien de lutins voulez vous envoyer au repos (vous pouvez en envoyer jusqu'à {nbAttente})");
                            Console.WriteLine($"    ('retour' pour annuler)");
                            repString = Console.ReadLine();

                            while (repString != "retour")//Tant que l'utilisateur ne veut pas quitter ce menu
                            {
                                //Si le nombre de lutins est invalide (pas dans l'interval 0 < x <= nbAttente)
                                if(int.TryParse(repString, out rep) == false || rep < 1 || rep > nbAttente)
                                {
                                    Console.WriteLine($"** /!\\ Veuillez rentrer un entier valide (entre 1 et {nbAttente})");
                                    repString = Console.ReadLine();
                                }
                                else//Sinon on fait le traitement demandé
                                {
                                    for(int i = 0; i < ParamSimulation.NBLutins ; i++)
                                    {
                                        Lutin l = FileLutins.Defile();
                                        int count = rep;
                                        //Si on doit encore mettre des lutins au repos et que le lutin est en attente
                                        if(l.Statut == EtatTravail.Attente && count > 0)
                                        {
                                            l.Statut = EtatTravail.Repos;
                                            count--;//On a un Lutin de moins à mettre au repos
                                        }
                                        FileLutins.Enfile(l);
                                    }
                                    //On met à jour les différents comptes
                                    nbAttente = nbAttente - rep;
                                    nbRepos = nbRepos + rep;
                                    tempsReposLutins = 12;
                                    repString = "retour";
                                }
                            }
                        }
                        //Sinon, on prévient l'utilisateur
                        else
                        {
                            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("** /!\\ Il n'y a pas de lutins en attente à mettre au repos, veuillez faire un autre choix");
                            Console.WriteLine();
                            Console.WriteLine("Veuillez appuyer sur une touche pour continuer...");
                            string? retour = Console.ReadLine();
                        }
                        Console.Clear();
                        repString = "back";
                    }
                    //---------------------------------------------
                    //Si on choisit de mettre des lutins au travail
                    else if (rep == 2)
                    {
                        Console.Clear();
                        //Si on peut mettre des Lutins au travail (çàd s'il y en a en attente)
                        if(nbRepos > 0 && tempsReposLutins <= 0)
                        {
                            Console.WriteLine($"Combien de lutins voulez vous envoyer au travail (vous pouvez en envoyer jusqu'à {nbRepos})");
                            Console.WriteLine($"    ('retour' pour annuler)");
                            repString = Console.ReadLine();

                            while (repString != "retour")//Tant que l'utilisateur ne veut pas quitter ce menu
                            {
                                //Si le nombre de lutins est invalide (pas dans l'interval 0 < x <= nbRepos)
                                if(int.TryParse(repString, out rep) == false || rep < 1 || rep > nbRepos)
                                {
                                    Console.WriteLine($"** /!\\ Veuillez rentrer un entier valide (entre 1 et {nbRepos})");
                                    repString = Console.ReadLine();
                                }
                                else//Sinon on fait le traitement demandé
                                {
                                    for(int i = 0; i < ParamSimulation.NBLutins ; i++)
                                    {
                                        Lutin l = FileLutins.Defile();
                                        int count = rep;
                                        //Si on doit encore mettre des lutins au travail et que le lutin est au repos
                                        if(l.Statut == EtatTravail.Repos && count > 0)
                                        {
                                            l.Statut = EtatTravail.Attente;//on le met en attente car il n'est pas encore en train de travailler
                                            count--;//On a un Lutin de moins à mettre au travail
                                        }
                                        FileLutins.Enfile(l);
                                    }
                                    //On met à jour les différents comptes
                                    nbAttente = nbAttente + rep;
                                    nbRepos = nbRepos - rep;
                                    repString = "retour";
                                }
                            }
                        }
                        //Sinon, on prévient l'utilisateur
                        else if (tempsReposLutins > 0)
                        {
                            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"** /!\\ Les lutins sont encore au repos pour {tempsReposLutins} heure(s), veuillez faire un autre choix");
                        }
                        else
                        {
                            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("** /!\\ Il n'y a pas de lutins au repos, veuillez faire un autre choix");
                            Console.WriteLine();
                            Console.WriteLine("Veuillez appuyer sur une touche pour continuer...");
                            string? retour = Console.ReadLine();
                        }
                        Console.Clear();
                        repString = "back";
                    }
                }
                    
            }

            //----Méthode GestionNains----//
            //Auteur : Tancrède
            //Description : Fonction qui affiche combien de nains sont au repos, en attente ou en train de travailler
            public void GestionNains()
            {                
                //Nombres de nains en train de travailler, au repos et en attente
                int nbTravail = 0;
                int nbRepos = 0;
                int nbAttente = 0;

                for (int i = 0; i < ParamSimulation.NBNains ; i++)
                {
                    Nain n = FileNains.Defile();
                    if (n.Statut == EtatTravail.Travail){nbTravail++;}
                    else if (n.Statut == EtatTravail.Attente){nbAttente++;}
                    else if(n.Statut == EtatTravail.Repos){nbRepos++;}
                    FileNains.Enfile(n);
                }
                Console.Clear();
                Console.WriteLine("===================================================");
                Console.WriteLine("∣∣                 GESTION DES Nains             ∣∣");
                Console.WriteLine("===================================================");
                Console.WriteLine($"∣∣  Il y a {nbTravail} nains en train de travailler        ∣∣");
                Console.WriteLine($"∣∣  Il y a {nbAttente} nains en attente                    ∣∣");
                Console.WriteLine($"∣∣  Il y a {nbRepos} nains au repos                      ∣∣");
                Console.WriteLine("===================================================");
                //Options et traitement des choix
                Console.WriteLine("∣∣            Que voulez vous faire ?            ∣∣");
                Console.WriteLine("===================================================");
                Console.WriteLine("∣∣ -> 1 : Envoyer des nains au repos             ∣∣");
                Console.WriteLine("∣∣ -> 2 : Envoyer des nains au travail           ∣∣");
                Console.WriteLine("∣∣ -> 'retour' : Retourner au menu précédent     ∣∣");
                Console.WriteLine("===================================================");

                string? repString = Console.ReadLine();
                int rep ;
                //Tant que l'on ne veut pas retourner au menu d'avant
                while (repString != "retour")
                {
                    //Si on a fait un retour en arrière on réaffiche tout
                    if (repString == "back")
                    {
                        //Affichage des infos
                        Console.Clear();
                        Console.WriteLine("===================================================");
                        Console.WriteLine("∣∣                 GESTION DES Nains             ∣∣");
                        Console.WriteLine("===================================================");
                        Console.WriteLine($"∣∣  Il y a {nbTravail} nains en train de travailler        ∣∣");
                        Console.WriteLine($"∣∣  Il y a {nbAttente} nains en attente                    ∣∣");
                        Console.WriteLine($"∣∣  Il y a {nbRepos} nains au repos                      ∣∣");
                        Console.WriteLine("===================================================");
                        //Options et traitement des choix
                        Console.WriteLine("∣∣            Que voulez vous faire ?            ∣∣");
                        Console.WriteLine("===================================================");
                        Console.WriteLine("∣∣ -> 1 : Envoyer des nains au repos             ∣∣");
                        Console.WriteLine("∣∣ -> 2 : Envoyer des nains au travail           ∣∣");
                        Console.WriteLine("∣∣ -> 'retour' : Retourner au menu précédent     ∣∣");
                        Console.WriteLine("===================================================");
                        repString = Console.ReadLine();

                    }
                    //Si la réponse n'est pas valide, on fait resaisir la valeur
                    if ((int.TryParse(repString, out rep) == false || rep < 1 || rep > 3) && repString != "retour")
                    {
                        Console.WriteLine("** /!\\ Veuillez rentrer un entier valide (entre 1 et 3)");
                        repString = Console.ReadLine();
                    }
                    //-------------------------------------------
                    //Si on choisit de mettre des nains au repos
                    else if(rep == 1)
                    {
                        //Si on peut mettre des nains au repos (çàd s'il y en a en attente)
                        if(nbAttente > 0)
                        {
                            Console.Clear();
                            Console.WriteLine($"Combien de nains voulez vous envoyer au repos (vous pouvez en envoyer jusqu'à {nbAttente})");
                            Console.WriteLine($"    ('retour' pour annuler)");
                            repString = Console.ReadLine();

                            while (repString != "retour")//Tant que l'utilisateur ne veut pas quitter ce menu
                            {
                                //Si le nombre de nains est invalide (pas dans l'interval 0 < x <= nbAttente)
                                if(int.TryParse(repString, out rep) == false || rep < 1 || rep > nbAttente)
                                {
                                    Console.WriteLine($"** /!\\ Veuillez rentrer un entier valide (entre 1 et {nbAttente})");
                                    repString = Console.ReadLine();
                                }
                                else//Sinon on fait le traitement demandé
                                {
                                    for(int i = 0; i < ParamSimulation.NBNains ; i++)
                                    {
                                        Nain n = FileNains.Defile();
                                        int count = rep;
                                        //Si on doit encore mettre des nains au repos et que le nains est en attente
                                        if(n.Statut == EtatTravail.Attente && count > 0)
                                        {
                                            n.Statut = EtatTravail.Repos;
                                            count--;//On a un nains de moins à mettre au repos
                                        }
                                        FileNains.Enfile(n);
                                    }
                                    //On met à jour les différents comptes
                                    nbAttente = nbAttente - rep;
                                    nbRepos = nbRepos + rep;
                                    tempsReposNains = 24;
                                    repString = "retour";
                                }
                            }
                        }
                        //Sinon, on prévient l'utilisateur
                        else
                        {
                            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("** /!\\ Il n'y a pas de nains en attente à mettre au repos, veuillez faire un autre choix");
                            Console.WriteLine();
                            Console.WriteLine("Veuillez appuyer sur une touche pour continuer...");
                            string? retour = Console.ReadLine();
                        }
                        Console.Clear();
                        repString = "back";
                    }
                    //---------------------------------------------
                    //Si on choisit de mettre des nains au travail
                    else if (rep == 2)
                    {
                        Console.Clear();
                        //Si on peut mettre des nains au travail (çàd s'il y en a en attente)
                        if(nbRepos > 0 && tempsReposNains <= 0)
                        {
                            Console.WriteLine($"Combien de nains voulez vous envoyer au travail (vous pouvez en envoyer jusqu'à {nbRepos})");
                            Console.WriteLine($"    ('retour' pour annuler)");
                            repString = Console.ReadLine();

                            while (repString != "retour")//Tant que l'utilisateur ne veut pas quitter ce menu
                            {
                                //Si le nombre de lutins est invalide (pas dans l'interval 0 < x <= nbRepos)
                                if(int.TryParse(repString, out rep) == false || rep < 1 || rep > nbRepos)
                                {
                                    Console.WriteLine($"** /!\\ Veuillez rentrer un entier valide (entre 1 et {nbRepos})");
                                    repString = Console.ReadLine();
                                }
                                else//Sinon on fait le traitement demandé
                                {
                                    for(int i = 0; i < ParamSimulation.NBNains ; i++)
                                    {
                                        Nain n = FileNains.Defile();
                                        int count = rep;
                                        //Si on doit encore mettre des nains au travail et que le nains est au repos
                                        if(n.Statut == EtatTravail.Repos && count > 0)
                                        {
                                            n.Statut = EtatTravail.Attente;//on le met en attente car il n'est pas encore en train de travailler
                                            count--;//On a un nains de moins à mettre au travail
                                        }
                                        FileNains.Enfile(n);
                                    }
                                    //On met à jour les différents comptes
                                    nbAttente = nbAttente + rep;
                                    nbRepos = nbRepos - rep;
                                    repString = "retour";
                                }
                            }
                        }
                        //Sinon, on prévient l'utilisateur
                        else if (tempsReposLutins > 0)
                        {
                            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"** /!\\ Les nains sont encore au repos pour {tempsReposNains} heure(s), veuillez faire un autre choix");
                        }
                        else
                        {
                            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("** /!\\ Il n'y a pas de nains au repos, veuillez faire un autre choix");
                            Console.WriteLine();
                            Console.WriteLine("Veuillez appuyer sur une touche pour continuer...");
                            string? retour = Console.ReadLine();
                        }
                        Console.Clear();
                        repString = "back";
                    }
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
        //Utilité : La fonction sert à verifier si ce que l'utilisateur à rentrer est bien un int, et s'il est positif.
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
            // Test si l'âge entré par l'utilisateur est dans compris entre 0 et 18 ans les deux compris, sinon renvoie un erreur.
            if(0 <= age && age <= 2){return Jouet.Nounours;}
            else if(3 <= age && age <= 5){return Jouet.Tricycle;}
            else if(6 <= age && age <= 10){return Jouet.Jumelles;}
            else if(11 <= age && age <= 15){return Jouet.Abonnement;}
            else if(16 <= age && age <= 18) {return Jouet.Ordinateur;}
            else {throw new Exception("L'âge doit être compris entre 0 et 18 ans.");}
        }

        //---------------------------------------------Fonction RandomContinent---------------------------------------------// 
        //Auteur : Rémi
        //Fonction/Class : RandomContinent
        //Paramètres : nbr (int)
        //Renvoie : Continents
        //Utilité : La fonction renvoie un continent en fonction du numero au hasard qui est en paramètre.
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
            Continents continent = RandomContinent(random.Next(5));// Prend un nombre aléatoire et en fait un Continent
            int age = random.Next(19); // Prend un âge aléatoire entre 0 et 18 ans et le transforme en jouet
            string adresse = ListeAdresse[random.Next(ListeAdresse.Length)];// Prend une adresse aléatoire dans la liste des adresses
            Lettre lettreAléatoire = new Lettre(age,nom, prenom, continent, adresse); // Créer la lettre avec les valeurs aléatoires plus hauts
            //On retourne la lettre
            return lettreAléatoire;
        }


        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------ Main ----------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        public static void Main()
        {
            bool ApplicationOuverte = true;
            while(ApplicationOuverte)
            {
                Simulation simulation = new Simulation(); 
                if(simulation.VeutArrêter == true)
                {
                    break; // On arrête tout le code car l'utilisateur a entré QUITTER plutôt que LANCEMENT
                }
                simulation.CreationTravailleurs();
                simulation.CreationLettres();
                while (simulation.EstTermine() == false && simulation.VeutRelancer == false && simulation.VeutArrêter == false)//Tant que toutes les lettres ne sont pas envoyées, que l'utilisateur ne veut pas relancer la simulation ou qu'il ne veut pas l'arrêter
                {   
                    simulation.Lancer();
                }
                if(simulation.VeutRelancer == true) //L'utilisateur veut relancer une nouvelle simulation, donc on retire l'ancien affichage.
                {
                    Console.Clear();
                }
                else
                {
                    ApplicationOuverte = false; // Si l'utilisateur veut arrêter la simulation pendant son exécution
                }
            }
        }
    }

}
