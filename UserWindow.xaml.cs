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
        public UserWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_storage_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new Storage());
        }

        private void btn_waybill_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new WayBill());
        }

        private void btn_acceptance_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new AcceptanceOfWagon());
        }

        private void btn_loading_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new LoadingOfWagons());
        }

        private void btn_CreateSost_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new CreateOfSostav());
        }

        private void btn_dispatch_Click(object sender, RoutedEventArgs e)
        {
            frm_HomePage.NavigationService.Navigate(new DispatchOfWagons());
        }
    }
}
