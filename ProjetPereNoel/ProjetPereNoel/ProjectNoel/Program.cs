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

        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------Classes-----------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//

        //---------------------------------------------Classe Param---------------------------------------------//
        //Auteur : Rémi
        //Paramètres : Aucun
        //Renvoie : Rien
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

            //Constructeur
            internal File()
            {
                Liste = new ListeChainee<T> {} ;
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
            }
        }



        //---------------------------------------------Classe Pile---------------------------------------------//        
        // //Auteur : Tancrède 
        //Description : Classe représentant une pile avec des listes chaînées.
        //Elle contient les méthodes : Empile, Depile et EstVide

        public class Pile<T>
        {
            public ListeChainee<T> Liste {get; set;}//Liste représentant la pile

            //Constructeur
            public Pile()
            {
                Liste = new ListeChainee<T> {} ;
            }

            //----Méthode Empile----//
            //Auteur : Tancrède
            //Paramètres :
            //  pValeur, valeur à empiler de type quelconque
            //Description : Empile la valeur en paramètre.
            public void Empile (T pValeur)
            {
                this.Liste.AddFirst(pValeur);//La première position est la position d'arrivée dans la pile
            }

            //----Méthode Depile----//
            //Auteur : Tancrède
            //Description : Depile la pile et renvoie la valeur dépilée.
            public T Depile()
            {
                if (this.EstVide() == false){
                    T returnValue = this.Liste.Head.Valeur;
                    this.Liste.RemoveFirst();//On enlève la première valeur de la liste quand on dépile
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
            }
        }
        //---------------------------------------------Classe Lettre---------------------------------------------//        
        //Auteur : Tancrède
        //Description : classe qui représente une lettre avec trois attributs, le nom et le prénom de l'enfant et le jouet demandé par l'enfant
        public class Lettre
        {
            public string Nom {get; set;}
            public string Prenom {get; set;}
            public Jouet Jouet {get; set;}

            //Constructeur
            public Lettre( Jouet jouet, string nom, string prenom)
            {
                Nom = nom;
                Prenom = prenom;

                //On utilisera la fonction AgeToJouet pour ce paramètre
                Jouet = jouet;
            }

            public void Affiche()
            {
                Console.WriteLine($"{Prenom} {Nom} a demandé un {Jouet} au Père Noël");
            }

        }
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------Méthodes--------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------------//


        //Auteur : Rémi
        //Fonction/Class : ClearConsole
        //Paramètres : Aucun
        //Renvoie : Void
        //Utilité : La fonction sert à nettoyer la console, 
        // pour rendre l'interaction de l'utilisateur avec cette dernière plus facile et agréable.
        public static void ClearConsole ()
        {
            Console.Clear();
        }

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
            ClearConsole();
            return Valeur;
        }

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
            string prenom = ListePrenoms[random.Next(ListePrenoms.Length)]; // Prend un prénoms aléatoire dans la liste des prénoms
            string nom = ListeNoms[random.Next(ListeNoms.Length)];// Prend un nom aléatoire dans la liste des nom
            Jouet jouet = AgeToJouet(random.Next(18)); // Prend un âge aléatoire entre 0 et 18 ans et le transforme en jouet 
            Lettre lettreAléatoire = new Lettre(jouet,nom, prenom); // Créer la lettre avec les valeurs aléatoires plus hauts
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
            Pile<Lettre> pileLettres = new Pile<Lettre>{};

            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------Test unitaires----------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            ClearConsole();
            //----------------------------Jalon 1----------------------------//

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
            Lettre lettreTest = new Lettre(Jouet.Tricycle, "Robert", "LEROI"); 

            //Affichage
            lettreTest.Affiche();

            //Création et affichage d'une lettre dans la pile des lettres du père Noël
            CreerLettre(pileLettres);
            pileLettres.Affiche();


            
        }
    }

}
