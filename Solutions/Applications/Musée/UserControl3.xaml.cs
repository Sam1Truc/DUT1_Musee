using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Core;
using System.Windows.Data;
using System;

namespace Musée
{
    /// <summary>
    /// Logique d'interaction pour UserControl3.xaml
    /// </summary>
    public partial class UserControl3 : UserControl
    {

        /// <summary>
        /// Membre qui permet de récupérer les données du Manager.
        /// </summary>
        private Manager DataManager
        {
            get { return ((Application.Current as App).Resources["DataManager"] as ObjectDataProvider).Data as Manager; }
        }

        /// <summary>
        /// Liste des StackPanels dans l'onglet Collection.
        /// </summary>
        private List<StackPanel> listePanelsCol = new List<StackPanel>();

        /// <summary>
        /// Liste des StackPanels dans l'onglet Elément.
        /// </summary>
        private List<StackPanel> listePanelsElem = new List<StackPanel>();

        /// <summary>
        /// Liste des boutons pour l'onglet Collection.
        /// </summary>
        private List<Button> listeBouttonsColl = new List<Button>();

        /// <summary>
        /// Liste des boutons pour l'onglet Elément.
        /// </summary>
        private List<Button> listeBouttonsElem = new List<Button>();

        /// <summary>
        /// Liste des boutons pour l'onglet Général.
        /// </summary>
        private List<Button> listeBouttonsGen = new List<Button>();

        /// <summary>
        /// Liste temporaire pour stocker des médias qui seront à ajouter à un nouvel élément.
        /// </summary>
        private List<Media> listeTempMédias = new List<Media>();


        /// <summary>
        /// Propriété pour le nom de l'organisme.
        /// </summary>
        public string NomOrg
        {
            get { return (string)GetValue(NomOrgProperty); }
            set { SetValue(NomOrgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NomOrg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomOrgProperty =
            DependencyProperty.Register("NomOrg", typeof(string), typeof(UserControl3), new PropertyMetadata(""));


        /// <summary>
        /// Propriété pour l'adresse de l'organisme.
        /// </summary>
        public string AdresseOrg
        {
            get { return (string)GetValue(AdresseOrgProperty); }
            set { SetValue(AdresseOrgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdresseOrg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdresseOrgProperty =
            DependencyProperty.Register("AdresseOrg", typeof(string), typeof(UserControl3), new PropertyMetadata(""));


        /// <summary>
        /// Propriété pour le numéro de téléphone de l'organisme.
        /// </summary>
        public string TelOrg
        {
            get { return (string)GetValue(TelOrgProperty); }
            set { SetValue(TelOrgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TelOrg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TelOrgProperty =
            DependencyProperty.Register("TelOrg", typeof(string), typeof(UserControl3), new PropertyMetadata(""));


        /// <summary>
        /// Propriété pour l'email de l'organisme.
        /// </summary>
        public string MailOrg
        {
            get { return (string)GetValue(MailOrgProperty); }
            set { SetValue(MailOrgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MailOrg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MailOrgProperty =
            DependencyProperty.Register("MailOrg", typeof(string), typeof(UserControl3), new PropertyMetadata(""));



        public string DescOrg
        {
            get { return (string)GetValue(DescOrgProperty); }
            set { SetValue(DescOrgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DescOrg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescOrgProperty =
            DependencyProperty.Register("DescOrg", typeof(string), typeof(UserControl3), new PropertyMetadata(""));




        /// <summary>
        /// Constructeur du UserControl3 avec l'ajout des StackPanels dans les listes correspondantes.
        /// </summary>
        public UserControl3()
        {
            InitializeComponent();
            listePanelsCol.AddRange(new List<StackPanel>() { stackPanelListAjouteElems, stackPanelZoneCol, stackPanelNomCol });
            listePanelsElem.AddRange(new List<StackPanel>() { stackPanelNomElement, stackPanelDescElement, stackPanelCaracElement, stackPanelNomZone, stackPanelAuteur });
            listeBouttonsColl.AddRange(new List<Button>() { buttonAnnulerColl, buttonAjoutCollection, buttonModifCollection, buttonSuppCollection });
            listeBouttonsElem.AddRange(new List<Button>() { buttonAnnulerElem, buttonAjoutElem, buttonModifElem, buttonSuppElem });
            DataContext = DataManager;
        }
        /// <summary>
        /// Méthode permettant de jouer sur la visibilité des StackPanels contenus dans une liste.
        /// </summary>
        /// <param name="liste">Liste contenant des StackPanels.</param>
        /// <param name="v">Permet d'identifier le niveau de visibilité qu'il va falloir appliqué.</param>
        private void VisibilityPanels(List<StackPanel> liste, int v)
        {
            if (v < 0 || v > 1)
            {
                return;
            }
            if (v == 0)
            {
                foreach (StackPanel panel in liste)
                {
                    panel.Visibility = Visibility.Collapsed;
                }
                ChangeTextBlockMilieu(0);
            }
            if (v == 1)
            {
                foreach (StackPanel panel in liste)
                {
                    panel.Visibility = Visibility.Visible;
                }
                ChangeTextBlockMilieu(0);
            }
        }

        private void SelectionBoutton(List<Button> liste, int c)
        {
            if (c < 0 || c > 3) return;
            foreach (Button b in liste)
            {
                b.Visibility = Visibility.Collapsed;
            }
            if (c > 0)
            {
                liste[0].Visibility = Visibility.Visible;
                liste[c].Visibility = Visibility.Visible;
                liste[c].Content = "Valider";
            }
            else
            {
                liste[0].Visibility = Visibility.Collapsed;
                liste[1].Visibility = Visibility.Visible;
                liste[1].Content = "Ajouter";
                liste[2].Visibility = Visibility.Visible;
                liste[2].Content = "Modifier";
                liste[3].Visibility = Visibility.Visible;
                liste[3].Content = "Supprimer";
            }
        }



        /// <summary>
        /// Permet de contrôler la visibilité de deux TextBlocks afin d'afficher soit l'un, soit l'autre.
        /// </summary>
        /// <param name="n">Entier qui décide de quel TextBlock doit être affiché.</param>
        private void ChangeTextBlockMilieu(int n)
        {
            if (n < 0 || n > 1)
            {
                return;
            }
            if (n == 0)
            {
                textBlockListeElem.Visibility = Visibility.Visible;
                textBlockSupprListeElem.Visibility = Visibility.Collapsed;
            }
            if (n == 1)
            {
                textBlockListeElem.Visibility = Visibility.Collapsed;
                textBlockSupprListeElem.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Gestion du clic sur le boutton Ajouter de l'onglet Collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAjoutCollection_Click(object sender, RoutedEventArgs e)
        {
            if (buttonAjoutCollection.Content.Equals("Ajouter"))
            {
                VisibilityPanels(listePanelsCol, 0);
                comboBoxSelectCollection.Visibility = Visibility.Collapsed;
                stackPanelListeModifElems.Visibility = Visibility.Collapsed;
                stackPanelListAjouteElems.Visibility = Visibility.Visible;
                stackPanelZoneCol.Visibility = Visibility.Visible;
                stackPanelNomCol.Visibility = Visibility.Visible;
                SelectionBoutton(listeBouttonsColl, 1);
                textBoxNomCol.Text = "";
                textBoxNomZoneCol.Text = "";
            }
            else
            {
                if (buttonAjoutCollection.Content.Equals("Valider"))
                {
                    MessageBoxResult result = MessageBox.Show("Confirmez-vous l'ajout de la collection ?", "Ajout d'une nouvelle Collection", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        List<IPieceDeMusee> lItems = new List<IPieceDeMusee>();
                        foreach (object o in listBoxAjouteElem.SelectedItems)
                        {
                            lItems.Add(o as IPieceDeMusee);
                        }
                        DataManager.AjouterCollection(textBoxNomZoneCol.Text, textBoxNomCol.Text, lItems.ToArray());
                    }
                    buttonAnnulerColl_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// Gestion du clic sur le bouton Modifier de l'onglet Collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonModifCollection_Click(object sender, RoutedEventArgs e)
        {
            if (buttonModifCollection.Content.Equals("Modifier"))
            {
                VisibilityPanels(listePanelsCol, 0);
                stackPanelListAjouteElems.Visibility = Visibility.Visible;
                stackPanelZoneCol.Visibility = Visibility.Visible;
                stackPanelNomCol.Visibility = Visibility.Visible;
                SelectionBoutton(listeBouttonsColl, 2);
                ChangeTextBlockMilieu(1);
                comboBoxSelectCollection.Visibility = Visibility.Collapsed;
                var collselect = comboBoxSelectCollection.SelectedItem;
                if (collselect == null)
                {
                    buttonAnnulerColl_Click(sender, e);
                    MessageBox.Show("Vous devez sélectionner un élément pour le modifier.");
                    return;
                }
                textBoxNomCol.Text = (collselect as IPieceDeMusee).Nom;
                textBoxNomZoneCol.Text = (collselect as IPieceDeMusee).Zone;
            }
            else
            {
                if (buttonModifCollection.Content.Equals("Valider"))
                {
                    MessageBoxResult result = MessageBox.Show("Confirmez-vous la modification de la collection ?", "Modification d'une nouvelle Collection", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        List<IPieceDeMusee> lItemsSuppr = new List<IPieceDeMusee>();
                        List<IPieceDeMusee> lItemsAjout = new List<IPieceDeMusee>();

                        foreach (IPieceDeMusee ipdm in listeElemsColl.SelectedItems)
                        {
                            lItemsSuppr.Add(ipdm);
                        }
                        foreach (IPieceDeMusee ipdm in listBoxAjouteElem.SelectedItems)
                        {
                            lItemsAjout.Add(ipdm);
                        }

                        DataManager.ModifierCollection(comboBoxSelectCollection.SelectedItem as IPieceDeMusee, textBoxNomZoneCol.Text, textBoxNomCol.Text,lItemsSuppr, lItemsAjout);
                    }
                    buttonAnnulerColl_Click(sender, e);
                }
            }
        }


        /// <summary>
        /// Gestion du clic sur le bouton Supprimer de l'onglet Collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSuppCollection_Click(object sender, RoutedEventArgs e)
        {
            VisibilityPanels(listePanelsCol, 0);
            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer cette collection ?", "Suppresion d'une collection", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DataManager.SupprimerCollection(comboBoxSelectCollection.SelectedItem as IPieceDeMusee);
            }
            buttonAnnulerColl_Click(sender, e);
        }

        /// <summary>
        /// Gestion du clic sur le bouton Ajouter de l'onglet Elément.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAjoutElem_Click(object sender, RoutedEventArgs e)
        {
            if (buttonAjoutElem.Content.Equals("Ajouter"))
            {
                VisibilityPanels(listePanelsElem, 1);
                comboBoxElem.Visibility = Visibility.Collapsed;
                stackPanelListMedia.Visibility = Visibility.Visible;
                gridMedia.Visibility = Visibility.Visible;
                SelectionBoutton(listeBouttonsElem, 1);
                comboBoxElem.ClearValue(ComboBox.SelectedItemProperty);
                textBoxNomElement.Text = "";
                textBoxNomZone.Text = "";
                textBoxDescElement.Text = "";
                textBoxCaracElement.Text = "";
                //listBoxMedia.ItemsSource = "";
                textBoxAuteur.Text = "";
            }
            else
            {
                if (buttonAjoutElem.Content.Equals("Valider"))
                {
                    MessageBoxResult result = MessageBox.Show("Confirmez-vous l'ajout de l'élément ?", "Ajout d'une nouvel élément", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        if(!string.IsNullOrWhiteSpace(textBoxAuteur.Text))
                        {
                            DataManager.AjouterElement(textBoxNomZone.Text, textBoxNomElement.Text, textBoxCaracElement.Text, textBoxDescElement.Text, textBoxAuteur.Text);
                        }
                        else
                        {
                            DataManager.AjouterElement(textBoxNomZone.Text, textBoxNomElement.Text, textBoxCaracElement.Text, textBoxDescElement.Text);
                        }
                    }
                    buttonAnnulerElem_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// Gestion du clic sur le bouton Modifier de l'onglet Elément.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonModifElem_Click(object sender, RoutedEventArgs e)
        {
            if (buttonModifElem.Content.Equals("Modifier"))
            {
                VisibilityPanels(listePanelsElem, 1);
                comboBoxElem.Visibility = Visibility.Collapsed;
                gridMedia.Visibility = Visibility.Visible;
                stackPanelListMedia.Visibility = Visibility.Visible;
                SelectionBoutton(listeBouttonsElem, 2);
                var elemSelect = comboBoxElem.SelectedItem;
                if(elemSelect == null)
                {
                    buttonAnnulerElem_Click(sender, e);
                    MessageBox.Show("Vous devez sélectionner un élément pour le modifier.");
                    return;
                }
                textBoxNomElement.Text = (elemSelect as IPieceDeMusee).Nom;
                textBoxNomZone.Text = (elemSelect as IPieceDeMusee).Zone;
            }
            else
            {
                if (buttonModifElem.Content.Equals("Valider"))
                {
                    MessageBoxResult result = MessageBox.Show("Confirmez-vous la modification de l'élément ?", "Modification d'un élément", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (textBoxAuteur.Text != null && textBoxAuteur.Text != "")
                        {
                            DataManager.ModifierElement(comboBoxElem.SelectedItem as IPieceDeMusee, textBoxNomZone.Text, textBoxNomElement.Text, textBoxCaracElement.Text, textBoxDescElement.Text, textBoxAuteur.Text);
                        }
                        else
                        {
                            DataManager.ModifierElement(comboBoxElem.SelectedItem as IPieceDeMusee, textBoxNomZone.Text, textBoxNomElement.Text, textBoxCaracElement.Text, textBoxDescElement.Text);
                        }
                    }
                    buttonAnnulerElem_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// Gestion du clic sur le bouton Supprimer de l'onglet Elément.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSuppElem_Click(object sender, RoutedEventArgs e)
        {
            VisibilityPanels(listePanelsElem, 0);
            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer cet élément ?", "Suppresion d'un élément", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                DataManager.SupprimerElement(comboBoxElem.SelectedItem as IPieceDeMusee);
            }
            buttonAjouterMedia.Visibility = Visibility.Hidden;
            buttonSupprimerMedia.Visibility = Visibility.Hidden;
            stackPanelListMedia.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Gestion du clic sur le bouton Valider de l'onglet Général
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonValiderGen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirmez-vous la modification des informations de l'organisme ?", "Modification des informations de l'organisme", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DataManager.CreerModifOrganisation(textBoxNomGeneral.Text, textBoxAdresseGeneral.Text, textBoxTelGeneral.Text, textBoxMailGeneral.Text, textBoxDescGeneral.Text);
            }
            else
            {
                buttonAnnulerGen_Click(sender, e);
            }
        }

        /// <summary>
        /// Gestion du clic sur le bouton Annuler de l'onglet Général
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnnulerGen_Click(object sender, RoutedEventArgs e)
        {
            textBoxNomGeneral.Text = DataManager.Organisme.NomOrg;
            textBoxAdresseGeneral.Text = DataManager.Organisme.AdresseOrg;
            textBoxTelGeneral.Text = DataManager.Organisme.TelOrg;
            textBoxMailGeneral.Text = DataManager.Organisme.MailOrg;
            textBoxDescGeneral.Text = DataManager.Organisme.DescOrg;
        }

        /// <summary>
        /// Gestion du clic sur le bouton Annuler de l'onglet Element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnnulerElem_Click(object sender, RoutedEventArgs e)
        {
            VisibilityPanels(listePanelsElem, 0);
            stackPanelNomElement.Visibility = Visibility.Collapsed;
            stackPanelNomZone.Visibility = Visibility.Collapsed;
            stackPanelDescElement.Visibility = Visibility.Collapsed;
            stackPanelCaracElement.Visibility = Visibility.Collapsed;
            gridMedia.Visibility = Visibility.Collapsed;
            stackPanelListMedia.Visibility = Visibility.Collapsed;
            comboBoxElem.Visibility = Visibility.Visible;
            SelectionBoutton(listeBouttonsElem, 0);
        }

        /// <summary>
        /// Gestion du clic sur le bouton Annuler de l'onglet Collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnnulerColl_Click(object sender, RoutedEventArgs e)
        {
            VisibilityPanels(listePanelsCol, 0);
            stackPanelListAjouteElems.Visibility = Visibility.Collapsed;
            stackPanelNomCol.Visibility = Visibility.Collapsed;
            stackPanelZoneCol.Visibility = Visibility.Collapsed;
            comboBoxSelectCollection.Visibility = Visibility.Visible;
            stackPanelListeModifElems.Visibility = Visibility.Visible;
            SelectionBoutton(listeBouttonsColl, 0);
        }

        /// <summary>
        /// Gestion du clic sur le bouton Media de l'onglet Collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAjouterMedia_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog mediafiles = new OpenFileDialog();
            mediafiles.Filter = "Type d'image | *.jpg";
            mediafiles.Title = "Séléctionner l'image à ajouter.";
            //mediafiles.ShowDialog();
            
            if(mediafiles.ShowDialog().HasValue)
            {
                if (buttonModifElem.Content.Equals("Valider"))
                {
                    DataManager.AjouterMédia(comboBoxElem.SelectedItem as IPieceDeMusee, mediafiles.FileName);
                }
                else
                {
                    listeTempMédias.Add(new Media(mediafiles.FileName));
                    listBoxMedia.SetValue(ListBox.ItemsSourceProperty, listeTempMédias.ToArray());
                }
            }

        }

        /// <summary>
        /// Gestion du clic sur le bouton SupprimerMédia de l'onglet Collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSupprimerMedia_Click(object sender, RoutedEventArgs e)
        {
            List<Media> l = new List<Media>();

            foreach (Object o in listBoxMedia.SelectedItems)
            {
                l.Add(o as Media);
            }
            DataManager.SupprimerMedias(comboBoxElem.SelectedItem as IPieceDeMusee, l);
        }

    }
}
