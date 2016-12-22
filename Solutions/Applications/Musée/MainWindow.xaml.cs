using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Core;
using System.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace Musée
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Membre qui permet de récupérer les données du Manager.
        /// </summary>
        public Manager DataManager
        {
            get { return ((Application.Current as App).Resources["DataManager"] as ObjectDataProvider).Data as Manager; }
        }

        public MainWindow()
        {
            InitializeComponent();
            UserControlBottomManager.ListeUserControl = new List<UserControl>() { mUC1, mUC2, mUC3 };
            DataContext = DataManager;
        }

        /// <summary>
        /// Evènement lorsque l'utilisateur click sur le bouton d'Administration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (monBouton.Content.Equals("Administration"))
            {
                UserControlBottomManager.ShowUC(0);
                monBouton.Content = "Déconnexion";
            }
            else
            {
                DataManager.Deconnexion();
                UserControlBottomManager.ShowUC(1);
                monBouton.Content = "Administration";
            }

        }

        /// <summary>
        /// Méthode permettant la recherche dans la barre de rechercher rapide.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mabarreRecherche_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            listBoxElements.ItemsSource = (comboBoxCollections.SelectedItem as IPieceDeMusee).AllElemDePDM.Where(elt => elt.Nom.Contains(mabarreRecherche.Text));
        }

        /// <summary>
        /// Méthode permettant de demander une confirmation lors de la fermeture de l'application.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            var response = MessageBox.Show("Voulez-vous vraiment fermer l'application ?", "Fermeture...",
                                   MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                DataManager.SauvegarderMusee(DataManager.Racine.ListePDM);
                DataManager.SauvegarderOrganisme(DataManager.Organisme);
                DataManager.SauvegardeUtilisateurs(DataManager.ListeUtilisateurs);
                Application.Current.Shutdown();
            }
        }

        private void comboBoxCollections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectCollection = comboBoxCollections.SelectedItem;
            if (selectCollection != null)
            {
                listBoxElements.ItemsSource = (selectCollection as IPieceDeMusee).AllElemDePDM;
            }
            else
            {
                listBoxElements.ItemsSource = null;
            }
            mabarreRecherche.Text = "";
        }
    }
}
