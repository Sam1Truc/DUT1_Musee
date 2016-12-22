using System.Windows;
using System.Windows.Controls;
using Core;
using System.Windows.Data;

namespace Musée
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        /// <summary>
        /// Membre qui permet de récupérer les données du Manager.
        /// </summary>
        private Manager DataManager
        {
            get { return ((Application.Current as App).Resources["DataManager"] as ObjectDataProvider).Data as Manager; }
        }

        public UserControl1()
        { 
            InitializeComponent();
            DataContext = DataManager;
        }

        /// <summary>
        /// Propriété pour le mot de passe.
        /// </summary>
        private string MotDePasse
        {
            get;
            set;
        }

        /// <summary>
        /// Evènement lorsque l'on clique sur le bouton "Valider"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            if (DataManager.Connexion(textBoxId.Text,passwordBox.Password))
            {
                if (DataManager.ConnexionAdmin())
                {
                    UserControlBottomManager.ShowUC(2);
                }
                else
                {
                    textBlockAttenteUtilisateur.Visibility = Visibility.Visible;
                }
            }
            else
            {
                passwordBox.Clear();
                textBlockMauvaisPass.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Evènement lorsque l'on clique sur le bouton "Annuler".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            DataManager.Deconnexion();
            UserControlBottomManager.ShowUC(1);
            (App.Current.MainWindow as MainWindow).monBouton.Content = "Administration";
        }

    }
}
