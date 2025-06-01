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
    /// Логика взаимодействия для CreateTrain.xaml
    /// </summary>
    public partial class CreateTrain : Page
    {
        public CreateTrain(User user)
        {
            InitializeComponent();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Railway_Car railway_Car = new Railway_Car()
            {
                Name_Car = tbx_name.Text.Trim(),
                id_Status_Car = 5,
                Status_Car = null,
                Railway_Railway_Car = null,
                Sending_Railway = null,
            };
            uk_koksEntities2.GetContext().Railway_Car.Add(railway_Car);
            uk_koksEntities2.GetContext().SaveChanges();
            Page_Loaded(sender, e);
        }

        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            if (dg_Train.SelectedItem != null)
            {
                var selectedItem = (SelectRailWayCar_Result)dg_Train.SelectedItem;
                Railway_Car railwayCar = uk_koksEntities2.GetContext().Railway_Car.Where(item => item.id_Railway_Car == selectedItem.ИдентификационныйНомер).FirstOrDefault();
                if (railwayCar.id_Status_Car == 6)
                {
                    MessageBox.Show("В данный поезд входят вагоны");
                }
                else if (railwayCar.id_Status_Car == 7)
                {
                    MessageBox.Show("Данный поезд уже отправлен");
                }
                else
                {
                    uk_koksEntities2.GetContext().Railway_Car.Remove(railwayCar);
                    uk_koksEntities2.GetContext().SaveChanges();
                    Page_Loaded(sender, e);
                }               
            }
            else
            {
                MessageBox.Show("Выберете запись");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_Train.ItemsSource = uk_koksEntities2.GetContext().SelectRailWayCar();
        }
    }
}
