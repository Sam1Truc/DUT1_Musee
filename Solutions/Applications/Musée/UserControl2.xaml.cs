using System.Windows.Controls;
using Core;
using System.Windows;
using System.Windows.Data;

namespace Musée
{
    /// <summary>
    /// Logique d'interaction pour UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {

        public UserControl2()
        {
            InitializeComponent();
        }

        public string DescriptionOrg
        {
            get { return (string)GetValue(DescriptionOrgProperty); }
            set { SetValue(DescriptionOrgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DescriptionOrg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionOrgProperty =
            DependencyProperty.Register("DescriptionOrg", typeof(string), typeof(UserControl2), new PropertyMetadata("Bla"));



    }
}
