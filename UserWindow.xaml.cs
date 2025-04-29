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
using System.Windows.Shapes;

namespace Logistics
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User User = new User();
        public UserWindow(User user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_storage_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new Sklad(User));
        }

        private void btn_waybill_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new WayBill(User));
        }

        private void btn_acceptance_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new AcceptanceOfWagon(User));
        }

        private void btn_loading_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new LoadingOfWagons(User));
        }

        private void btn_CreateSost_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new CreateOfSostav(User));
        }

        private void btn_dispatch_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new DispatchOfWagons(User));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string fio = User.FirstName + " " + User.Name + " " + User.LastName;
            fio_tbx.Text = fio;
        }
    }
}
