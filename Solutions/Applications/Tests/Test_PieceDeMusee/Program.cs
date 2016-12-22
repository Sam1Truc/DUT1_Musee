using Core;
using Stub;
using System;

namespace Test_PieceDeMusee
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager man = new Manager(new StubManager());

            //man.AjouterCollection("ZoneTest","TestBrute");

            man.Connexion("Admin", "admin");
            man.AjouterCollection("Musée", "BlocCentral");
            man.AjouterElement("Galerie", "Mona Lisa", "Type : Peinture", "Peinture célèbre", "Léonard De Vinci", new Media("chèvre"));
            man.AjouterAUneSousCollection("Animaux", new Element("Musée", "Poule", "Pond des oeufs", "Utile pour manger"));
            man.AjouterAUneSousCollection("BlocCentral", new Collection("Entrée Principale", "Armures"));

            foreach (IPieceDeMusee p in man.Racine.ListePDM)
            {
                Console.WriteLine((PieceDeMusee)p);
            }
        }
    }
}
