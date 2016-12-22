using System;
using Core;
using Persistance_XML;

namespace Test_Persistance_XML
{
    class Program
    {
        static void Main(string[] args)
        {

            Manager manager = new Manager(new PersistanceXML());
            Organisation org = new Organisation("DémoMusée", "AdresseDémo", "TelDémo", "MailDémo", "DescDémo");
            manager.SauvegarderOrganisme(org);
            //manager.ChargerOrganisme();
            //Console.WriteLine(manager.Musee.ToString());
            Utilisateur u = new Administrateur("admin", "admin", "nom_Admin", "prenom_Admin", "mail_Admin");
            Utilisateur u2 = new Utilisateur("Toto", "toto", "totomail");
            manager.InscrireUtilisateur(u);
            manager.InscrireUtilisateur(u2);
            manager.SauvegardeUtilisateurs(manager.ListeUtilisateurs);
            //manager.ChargerUtilisateurs();
            //foreach(var ut in manager.ListeUtilisateurs)
            //Console.WriteLine(ut.ToString()); 
            manager.Connexion("admin", "admin");
            manager.AjouterCollection("zonetest", "nomtest");
            manager.AjouterElement("zonelemtest", "nomelemtest", "caracelemtest", "descelemtest");
            manager.SauvegarderMusee(manager.Racine.ListePDM);

            foreach (PieceDeMusee p in manager.Racine.ListePDM)
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}
