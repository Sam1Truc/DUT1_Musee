using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// Interface qui permettra d'implémenter les différentes méthodes de chargement et de sauvegarde des données du programme.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Méthode abstraite qui devra définir comment charger les données concernant les utilisateurs.
        /// </summary>
        IEnumerable<IUtilisateur> ChargerUtilisateurs();

        /// <summary>
        /// Méthode abstraite qui devra définir comment sauvegarder les données concernant les utilisateurs.
        /// </summary>
        /// <param param name="listeU">Liste des utilisateurs à sauvegarder.</param>
        void SauvegardeUtilisateurs(IEnumerable<IUtilisateur> listeU);

        /// <summary>
        /// Méthode abstraite qui devra définir comment charger les collections et les éléments.
        /// </summary>
        IEnumerable<IPieceDeMusee> ChargerComposite();

        /// <summary>
        /// Méthode abstraite qui devra définir comment sauvegarder les collections et éléments.
        /// </summary>
        /// <param name="lPDM">Collection contenant toutes les collections et éléments de l'organisme.</param>
        void SauvegarderComposite(IEnumerable<IPieceDeMusee> lPDM);

        /// <summary>
        /// Méthode abstraite qui devra définir comment charger les données relatives à l'organisme.
        /// </summary>
        Organisation ChargerOrganisme();

        /// <summary>
        /// Méthode abstraite qui devra définir comment sauvegarder les données relatives à l'organisme.
        /// </summary>
        /// <param name="org">Objet de type Organisation.</param>
        void SauvegarderOrganisme(Organisation org);

    }
}
