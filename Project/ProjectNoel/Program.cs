using System.Reflection.Metadata;

public class Program
{
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
        return Valeur;
    }
    public static void ClearConsole ()
    {
        Console.Clear();
    }
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
        Param Noel = new Param();
    }
}
