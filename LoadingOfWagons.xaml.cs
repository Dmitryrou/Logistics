using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для LoadingOfWagons.xaml
    /// </summary>
    public partial class LoadingOfWagons : Page
    {
        public User UserPage = new User();
        public LoadingOfWagons(User user)
        {
            InitializeComponent();
            this.UserPage = user;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_Wagons2.ItemsSource = uk_koksEntities2.GetContext().SelectRailWayLoading(2);
            cbx_nomenclature.Items.Clear();
            cbx_stationLoading.Items.Clear();
            cbx_id_railway.Items.Clear();
            List<Railway> railway = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Status_Car == 1).ToList();
            List<Nomenclature> nomenclatures = uk_koksEntities2.GetContext().Nomenclature.ToList();
            List<Station_Loading> station_Loadings = uk_koksEntities2.GetContext().Station_Loading.ToList();
            for (int i = 0; i < nomenclatures.Count; i++)
            {
                cbx_nomenclature.Items.Add(nomenclatures[i].Name);
            }
            for (int i = 0; i < railway.Count; i++)
            {
                cbx_id_railway.Items.Add(railway[i].id_Railway);
            }
            for (int i = 0; i < station_Loadings.Count; i++)
            {
                cbx_stationLoading.Items.Add(station_Loadings[i].Name);
            }
        }

        private void Button_Click_add(object sender, RoutedEventArgs e)
        {
            string shablonName = @"^[1-9]\d{0,6}$";
            Regex myRegex = new Regex(shablonName);
            if (myRegex.IsMatch(txb_quantity.Text))
            {
                if(cbx_nomenclature.SelectedIndex != -1 &&
                    cbx_stationLoading.SelectedIndex != -1 &&
                    cbx_id_railway.SelectedIndex != -1 
                    )
                {
                    Nomenclature nomenclature = uk_koksEntities2.GetContext().Nomenclature.Where(item => item.Name == cbx_nomenclature.SelectedItem.ToString()).FirstOrDefault();
                    Storage_Accounting storage = uk_koksEntities2.GetContext().Storage_Accounting.Where(item => item.id_Nomenclature == nomenclature.id_Nomenclature).FirstOrDefault();
                    Station_Loading station = uk_koksEntities2.GetContext().Station_Loading.Where(item => item.Name == cbx_stationLoading.SelectedItem.ToString()).FirstOrDefault();
                    int idRailway = Convert.ToInt32(cbx_id_railway.SelectedItem);
                    Railway railway1 = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Railway == idRailway).FirstOrDefault();
                    int capacity = Convert.ToInt32(txb_quantity.Text.Trim());
                    if (railway1.Capacity_Car >= capacity)
                    {
                        Loading_Railway railway = new Loading_Railway()
                        {
                            id_Railway = idRailway,
                            Capacity = capacity,
                            Date = DateTime.Now,
                            id_Station_Loading = station.id_Station_Loading,
                            id_Storage = storage.id_Storage,
                            id_user = UserPage.id_user,
                            Railway = null,
                            Station_Loading = null,
                            Storage_Accounting = null,
                            User = null
                        };
                        storage.quantity = storage.quantity - capacity;
                        uk_koksEntities2.GetContext().Loading_Railway.Add(railway);
                        uk_koksEntities2.GetContext().SaveChanges();
                        Page_Loaded(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Количесто не должно превышать вместимость выбранного вагона");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все данные");
                }
            }
            else
            {
                MessageBox.Show("Поле количество не должно содержать буквы или знаки");
            }
        }

        private void Button_Click_remove(object sender, RoutedEventArgs e)
        {
            if (dg_Wagons2.SelectedItem != null)
            {
                var selectedItem = (SelectRailWayLoading_Result)dg_Wagons2.SelectedItem;
                Railway railway = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Railway == selectedItem.Номер).FirstOrDefault();
                Loading_Railway loading_Railway = uk_koksEntities2.GetContext().Loading_Railway.Where(item => item.id_Railway == selectedItem.Номер).FirstOrDefault();
                Storage_Accounting storage_Accounting = uk_koksEntities2.GetContext().Storage_Accounting.Where(item => item.id_Storage == loading_Railway.id_Storage).FirstOrDefault();
                storage_Accounting.quantity = storage_Accounting.quantity + loading_Railway.Capacity;
                uk_koksEntities2.GetContext().Loading_Railway.Remove(loading_Railway);
                uk_koksEntities2.GetContext().SaveChanges();
                dg_Wagons2.ItemsSource =uk_koksEntities2.GetContext().SelectRailWayLoading(2);
            }
            else
            {
                MessageBox.Show("Выберете запись");
            }
        }
    }
}
