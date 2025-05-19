using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AcceptanceOfWagon.xaml
    /// </summary>
    public partial class AcceptanceOfWagon : Page
    {
        private User User = new User();
        public AcceptanceOfWagon(User user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void Page_Loaded_AcceptanceOfWagon(object sender, RoutedEventArgs e)
        {
            dg_Wagons.ItemsSource = uk_koks_Entities.GetContext().SelectRailWay(1);
        }

        private void Button_Click_Check(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(tbx_id_railway.Text.Trim());
            Railway railway = uk_koks_Entities.GetContext().Railway.Where(item => item.id_Railway == id).FirstOrDefault();
            if (railway != null && railway.id_Status_Car == 1)
            {
                MessageBox.Show("Данный вагон уже есть в списке пустых вагонов",
                    "Проверка завершена",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else if (railway != null && railway.id_Status_Car != 1)
            {
                railway.id_Status_Car = 1;
                uk_koks_Entities.GetContext().SaveChanges();
                Page_Loaded_AcceptanceOfWagon(sender, e);
                MessageBox.Show("Вагон существует в базе. Он добавлен в списке пустых вагонов",
                    "Проверка завершена",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else if(railway == null)
            {
                MessageBox.Show("Данного вагона не существует в базе, заполните остальные поля и нажмите на кнопку 'Записать' ",
                    "Проверка завершена",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(tbx_id_railway.Text.Trim());
            int tareWeight = Convert.ToInt32(tbx_tare.Text.Trim());
            int capacity = Convert.ToInt32(tbx_capacity.Text.Trim());
            Railway railway = uk_koks_Entities.GetContext().Railway.Where(item => item.id_Railway == id).FirstOrDefault();
            if (railway == null)
            {
                railway = new Railway()
                {
                    id_Railway = id,
                    id_Status_Car = 1,
                    Tare_Weight = tareWeight,
                    Capacity_Car = capacity,
                    Loading_Railway = null,
                    Railway_Railway_Car = null,
                    Railway_Railway_Car1 = null,
                    Status_Car = null
                };
                uk_koks_Entities.GetContext().Railway.Add(railway);
                uk_koks_Entities.GetContext().SaveChanges();
                Page_Loaded_AcceptanceOfWagon(sender, e);
                MessageBox.Show("Данного вагона записан в базу как пустой",
                    "Запись сохранена",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            
            
        }
    }
}
