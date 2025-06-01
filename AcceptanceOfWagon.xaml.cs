using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            dg_Wagons.ItemsSource = uk_koksEntities2.GetContext().SelectRailWay(1);
        }

        private void Button_Click_Check(object sender, RoutedEventArgs e)
        {
            tbx_id_railway.Background = Brushes.Transparent;
            tbx_id_railway.ToolTip = "";
            string shablonName = @"^[1-9]\d{0,6}$";
            Regex myRegex = new Regex(shablonName);
            if (myRegex.IsMatch(tbx_id_railway.Text))
            {
                bool result = checkRailway();
                if (result == true)
                {
                    Page_Loaded_AcceptanceOfWagon(sender, e);
                }
                else
                {

                    MessageBox.Show("Данного вагона не существует в базе, заполните остальные поля и нажмите на кнопку 'Записать' ",
                        "Проверка завершена",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            else
            {
                tbx_id_railway.Background = Brushes.Red;
                tbx_id_railway.ToolTip = "Введите в поле номер вагона только цифры";
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            tbx_id_railway.Background = Brushes.Transparent;
            tbx_id_railway.ToolTip = "";
            int id = 0;
            int tareWeight = 0;
            int capacity = 0;
            string shablonName = @"^[1-9]\d{0,6}$";
            Regex myRegex = new Regex(shablonName);
            if (myRegex.IsMatch(tbx_id_railway.Text))
            {
                bool resultCheck = checkRailway();
                if (resultCheck == false)
                {
                    id = Convert.ToInt32(tbx_id_railway.Text.Trim());
                    if (myRegex.IsMatch(tbx_tare.Text))
                    {
                        tbx_tare.Background = Brushes.Transparent;
                        tbx_tare.ToolTip = " ";
                        tareWeight = Convert.ToInt32(tbx_tare.Text.Trim());
                    }
                    else
                    {
                        tbx_tare.Background = Brushes.Red;
                        tbx_tare.ToolTip = "Введите в поле тара только цифры";
                    }
                    if (myRegex.IsMatch(tbx_capacity.Text))
                    {
                        tbx_capacity.Background = Brushes.Transparent;
                        tbx_capacity.ToolTip = " ";
                        capacity = Convert.ToInt32(tbx_capacity.Text.Trim());
                    }
                    else
                    {
                        tbx_capacity.Background = Brushes.Red;
                        tbx_capacity.ToolTip = "Введите в поле грузоподьемность только цифры";
                    }
                    if (tareWeight != 0 & capacity != 0)
                    {

                        Railway railway = new Railway()
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
                        uk_koksEntities2.GetContext().Railway.Add(railway);
                        uk_koksEntities2.GetContext().SaveChanges();
                        Page_Loaded_AcceptanceOfWagon(sender, e);
                        MessageBox.Show("Данного вагона записан в базу как пустой",
                            "Запись сохранена",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                tbx_id_railway.Background = Brushes.Red;
                tbx_id_railway.ToolTip = "Введите в поле номер только цифры";
            }
        }

        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            if (dg_Wagons.SelectedItem != null)
            {
                var selectedItem = (SelectRailWay_Result)dg_Wagons.SelectedItem;
                Railway railway = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Railway == selectedItem.Номер).FirstOrDefault();
                uk_koksEntities2.GetContext().Railway.Remove(railway);
                uk_koksEntities2.GetContext().SaveChanges() ;
                Page_Loaded_AcceptanceOfWagon(sender,e);
            }
            else
            {
                MessageBox.Show("Выберете запись");
            }
        }
        public bool checkRailway()
        {
            bool result = false;
            int id = Convert.ToInt32(tbx_id_railway.Text.Trim());
            Railway railway = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Railway == id).FirstOrDefault();
            if (railway != null && railway.id_Status_Car == 1)
            {
                MessageBox.Show("Данный вагон уже есть в списке пустых вагонов",
                    "Проверка завершена",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                result = true;
            }
            else if (railway != null && railway.id_Status_Car != 1)
            {
                railway.id_Status_Car = 1;
                uk_koksEntities2.GetContext().SaveChanges();
                MessageBox.Show("Вагон существует в базе. Он добавлен в списке пустых вагонов",
                    "Проверка завершена",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                result = true;
            }
            
            return result;
        }
    }
}
