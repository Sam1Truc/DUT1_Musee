using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core
{
    public interface IPieceDeMusee
    {
        /// <summary>
        /// Propriété pour une zone spécifique de l'organisme.
        /// </summary>
        string Zone
        {
            get;
        }

        /// <summary>
        /// Propriété pour le nom de la PieceDeMusee.
        /// </summary>
        string Nom
        {
            get;
        }

        /// <summary>
        /// Méthode pour avoir une chaîne de caractère représentant les différentes PieceDeMusee.
        /// </summary>
        /// <returns>
        /// Chaine de caractère descriptive.</returns>
        string ToString();

        /// <summary>
        /// Propriété en lecture seule qui renvoit la liste de tous les éléments et collections contenus dans une collection parente.
        /// </summary>
        ObservableCollection<IPieceDeMusee> ListePDM
        {
            get;
        }

        /// <summary>
        /// Propriété en lecture seule qui renvoit la liste de tous les éléments contenus dans la collection principale et ses sous-collections.
        /// </summary>
        IEnumerable<IPieceDeMusee> AllElemDePDM
        {
            get;
        }

        /// <summary>
        /// Propriété en lecture seule qui renvoit la liste de toutes les collections contenues dans l'ensemble d'une collection racine.
        /// </summary>
        IEnumerable<IPieceDeMusee> AllColl
        {
            get;
        }

        /// <summary>
        /// Propriété en lecture seule qui renvoit la liste de tous les éléments contenus à la base de la collection racine.
        /// </summary>
        IEnumerable<IPieceDeMusee> AllElemSeuls
        {
            get;
        }

    }
}
