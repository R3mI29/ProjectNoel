using System.Formats.Asn1;
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
            public int NBLutins { get; set; } // Le nombre de Lutins Maximum que le Père Noël autorise
            public int NBNains { get; set; } //Le nombre de Nains Maximum que le Père Noël autorise
            public int NBJouetsParTraineau { get; set; } //Le nombre de jouets par traineau Maximum que le Père Noël autorise
            public int NBEnfants { get; set; } //Le nombre maximum d'enfants qui envoient une lettres au Père Noël
            public int NBLettresParHeures { get; set; } //Le nombres de lettres que le Père Noël reçois par heures

            //Le Constructeur//
            public Param()
            {
                Console.WriteLine("Veuillez donner Le nombres de lutins max :");
                NBLutins = DemandeInt();// Appelle de la fonction DemandeInt() qui demande à l'utilisateur de rentrer les valeurs de chaque variables.
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
                this.Liste.AddFirst(pValeur);//La première position est la position d'arrivée dans la file
            }

            //----Méthode Defile----//
            //Auteur : Tancrède
            //Description : Defile la file et renvoie la valeur défilée.
            public T Defile()
            {
                if (this.EstVide() == false)
                {
                    T returnValue = this.Liste.Head.Valeur;
                    this.Liste.RemoveLast();//La dernière position est la première position dans la file
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
                if (this.EstVide() == true)//Si la file est vide on ne l'affiche pas 
                {
                    Console.WriteLine("La file est vide");
                }
                else
                {
                    Node<T> tete = this.Liste.Head;//On prend la première valeur de la file
                    while(tete != null)
                    {
                        Console.Write($" | {tete.Valeur}");
                        tete = tete.Next;//On passe à la prochaine valeur de la file
                    }
                    Console.WriteLine(" --> ");
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
                if (this.EstVide() == true)
                {
                    Console.WriteLine("La pile est vide");
                }
                else
                {
                    Node<T> tete = this.Liste.Head;//On prend la première valeur de la pile
                    Console.Write("<-- ");//sens de sortie
                    while(tete != null)
                    {
                        Console.Write($"{tete.Valeur} | ");
                        tete = tete.Next;//On passe à la prochaine valeur de la pile
                    }
                    Console.WriteLine();
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
            Console.Clear();// Clear la console pour la rendre plus lisible et agréable pour l'utilisateur
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
                if (!condition || int.Parse(saisie) < 1) // Si la condition est toujours égale à False, ou que la valeur de l'utilisateur et négative. La fonction renvoie une erreur et demande à l'utilisateur une nouvelle valeur.
                {
                    Console.WriteLine("Erreur : veuillez entrer un entier valide !");
                    condition = false;// Remet la condition à False, car la valeur est un chiffre mais il est négatif.
                }
                else
                {
                    Valeur = int.Parse(saisie);// Coonversion de la valeur en Int, si la valeur est un chiffre positif.
                }
            } while (!condition);// Tant que la condition vaut False, la boucle continue de tourner.
            ClearConsole();
            return Valeur;// Retourne la valeur int que l'utilisateur a entrer.
        }

        //Auteur : Rémi
        //Fonction/Class : JouetsParEnfants
        //Paramètres : age (int)
        //Renvoie : Jouet
        //Utilité : La fonction renvoie le type de jouet adapté en fonction de l'âge de l'enfant.
        public static Jouet JouetParEnfants(int age)
        {
            if (age < 0 || age > 18){throw new Exception("L'âge doit être compris entre 0 et 18 ans.");}
            else if(age < 3){Console.Write("Nounours");return Jouet.Nounours;}
            else if(age < 6){Console.Write("Tricycle");return Jouet.Tricycle;}
            else if(age < 11){Console.Write("Jumelles");return Jouet.Jumelles;}
            else if(age < 16){Console.Write("Abonnement");return Jouet.Abonnement;}
            else{Console.Write("Ordi");return Jouet.Ordinateur;}
        }

        public static void Main()
        {
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------Variables globales------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------//
            
            //Param Noel = new Param();

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








            
            ClearConsole();
            JouetParEnfants(18);
        }
    }

}
