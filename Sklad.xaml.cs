using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logistics
{
    /// <summary>
    /// Логика взаимодействия для Sklad.xaml
    /// </summary>
    public partial class Sklad : Page
    {
        private User User = new User();
        public Sklad(User user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void Button_Click_Insert(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Refresh(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public static void Refresh()
        {
            DataGridStorage.ItemsSource = uk_koks_Entities.GetContext().Sto.ToList();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
