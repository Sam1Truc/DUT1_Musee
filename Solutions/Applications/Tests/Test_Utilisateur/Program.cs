using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Stub;

namespace Test_Utilisateur
{
    class Program
    {

        /// <summary>
        /// Fonction pour avoir la description des utilisateurs dans la liste du manager.
        /// </summary>
        /// <param name="lUtils"></param>
        static void DescriptionListeUtilisateurs(IEnumerable<IUtilisateur> lUtils)
        {
            Console.WriteLine("Description des utilisateurs :\n");
            foreach (Utilisateur utilis in lUtils)
            {
                Console.WriteLine(utilis + "\n");
            }
        }

        /// <summary>
        /// Fonction qui teste si la connexion à réussie ou non. Si elle a réussie, elle finit par déconnecter cet utilisateur.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur souhaitant se connecter.</param>
        /// <param name="mdp">Mot de passe de l'utilisateur souhaitant se connecter.</param>
        /// <param name="man">Manager dans lequel figure la liste des Utilisateurs enregistrés.</param>
        static void DemandeDeConnexion(string id, string mdp, Manager man)
        {
            if (man.Connexion(id, mdp))
            {
                StringBuilder res = new StringBuilder();
                res.AppendFormat("L'utilisateur {0} s'est bien connecté.\n",man.UtilisateurCourant.Identifiant);
                if(man.UtilisateurCourant is Administrateur) { res.Replace("utilisateur", "administrateur"); }
                Console.WriteLine("\n" + res);
                DescriptionListeUtilisateurs(man.ListeUtilisateurs);
                Console.Write("\n{0} se déconnecte.\n", man.UtilisateurCourant.Identifiant);
                man.Deconnexion();
            }
            else
            {
                Console.WriteLine("\nL'identifiant {0} ou sont mot de passe est incorrect.\n", id);
            }
            return;
        }

        static void Main(string[] args)
        {
            Manager man = new Manager(new StubManager());

            DescriptionListeUtilisateurs(man.ListeUtilisateurs);
            Console.WriteLine("Sasir l'identifiant :");
            string id = Console.ReadLine();
            Console.WriteLine("Saisir le mot de passe :");
            string mdp = Console.ReadLine();

            DemandeDeConnexion(id, mdp, man);

            Console.WriteLine("Sasir l'identifiant :");
            string id1 = Console.ReadLine();
            Console.WriteLine("Saisir le mot de passe :");
            string mdp1 = Console.ReadLine();

            DemandeDeConnexion(id1, mdp1, man);

        }
    }
}
