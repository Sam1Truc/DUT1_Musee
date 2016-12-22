using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Musée
{
    /// <summary>
    /// Classe qui permet la gestion des UserControls.
    /// </summary>
    static class UserControlBottomManager
    {
        /// <summary>
        /// Propriété statique qui contient une collection de ContentControls.
        /// </summary>
        public static List<UserControl> ListeUserControl
        {
            get { return listeUC; }
            set { listeUC = value; }
        }
        private static List<UserControl> listeUC = new List<UserControl>();

        /// <summary>
        /// Méthode permettant de gérer la visibilité entre les différents UserControls.
        /// </summary>
        /// <param name="index">Indice du UserControl que l'on souhaite afficher.</param>
        public static void ShowUC(int index)
        {
            if (index < 0 || index >= ListeUserControl.Count)
            {
                return;
            }

            foreach (var uc in ListeUserControl)
            {
                uc.Visibility = System.Windows.Visibility.Collapsed;
            }

            ListeUserControl[index].Visibility = System.Windows.Visibility.Visible;
        }

    }
}
