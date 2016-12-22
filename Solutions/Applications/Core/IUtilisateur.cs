using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Interface d'une classe Utilisateur qui devra contenir ces propriétés pour implémenter cette interface.
    /// </summary>
    public interface IUtilisateur
    {
        /// <summary>
        /// Propriété pour l'identifiant de l'utilisateur.
        /// </summary>
        string Identifiant
        {
            get;
        }

        /// <summary>
        /// Propriété pour le mot de passe.
        /// </summary>
        string MotDePasse
        {
            get;
        }

        /// <summary>
        /// Propriété pour le nom
        /// </summary>
        string Nom
        {
            get;
        }

        /// <summary>
        /// Propriété pour le prénom
        /// </summary>
        string Prenom
        {
            get;
        }

        /// <summary>
        /// Propriété pour le mail
        /// </summary>
        string Mail
        {
            get;
        }

    }
}
