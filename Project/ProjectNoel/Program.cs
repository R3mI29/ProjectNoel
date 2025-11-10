public class Program
{
    public class Param
    {
        public int NBLutins { get; set; }
        public int NBNains { get; set; }
        public int NBJouetsParTraineau { get; set; }
        public int NBEnfants { get; set; }
        public int NBLettresParHeures { get; set; }
        public Param(int nblutins, int nbnains, int nbJouetsParTraineau, int nbEnfants, int nbLettresParHeures)
        {
            this.NBLutins = nblutins;
            this.NBNains = nbnains;
            this.NBJouetsParTraineau = nbJouetsParTraineau;
            this.NBEnfants = nbEnfants;
            this.NBLettresParHeures = nbLettresParHeures;

        }
    }
    public static void InitParam()
    {
        Console.WriteLine("Veuillez donner Le nombres de lutins max :");
        int nblutins = int.Parse(Console.ReadLine());
        Console.WriteLine("Veuillez donner Le nombres de nains max :");
        int nbnains = int.Parse(Console.ReadLine());
        Console.WriteLine("Veuillez donner Le nombres de jouets max par traineau :");
        int nbJouetsParTraineau = int.Parse(Console.ReadLine());
        Console.WriteLine("Veuillez donner Le nombres d'enfants max :");
        int nbEnfants = int.Parse(Console.ReadLine());
        Console.WriteLine("Veuillez donner Le nombres de lettres par heures :");
        int nbLettresParHeures = int.Parse(Console.ReadLine());
        Param Noel = new Param(nblutins, nbnains, nbJouetsParTraineau, nbEnfants, nbLettresParHeures);
    }



    public static void Main()
    {
        InitParam();
    }
}
