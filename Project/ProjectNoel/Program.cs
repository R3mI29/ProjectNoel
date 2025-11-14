using System.Reflection.Metadata;

public class Program
{
    //Auteur : Rémi
    //Fonction/Class : ClearConsole
    //Paramètres : Aucun
    //Renvoie : Void
    //Utilité : La fonction sert à nettoyer la console, pour rendre l'interaction de l'utilisateur avec cette dernioère plus facile et agréable.
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
    //Fonction/Class : Param
    //Paramètres : Aucun
    //Renvoie : Rien
    //Utilité : La class Param sert à demander à l'utilisateur de definir les valeurs qui vont servir le programme durant la suite de son fonctionement.
    public class Param
    {
        public int NBLutins { get; set; }
        public int NBNains { get; set; }
        public int NBJouetsParTraineau { get; set; }
        public int NBEnfants { get; set; }
        public int NBLettresParHeures { get; set; }
        public Param()
        {
            Console.WriteLine("Veuillez donner Le nombres de lutins max :");
            NBLutins = DemandeInt();
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



    public static void Main()
    {
        ClearConsole();
        Param Noel = new Param();
    }
}
