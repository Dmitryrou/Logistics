using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Xml.Linq;

namespace Logistics
{
    /// <summary>
    /// Логика взаимодействия для CreateOfSostav.xaml
    /// </summary>
    public partial class CreateOfSostav : Page
    {
        private User User = new User();
        public CreateOfSostav(User user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void Button_Click_add(object sender, RoutedEventArgs e)
        {
            string name = cmb_train_add.Text.Trim();
            Railway_Car railway_Car = uk_koksEntities2.GetContext().Railway_Car.Where(item => item.Name_Car == name).FirstOrDefault();
            Railway_Railway_Car railway_Railway_Car = new Railway_Railway_Car()
            {
                id_Railway = Convert.ToInt32(cmb_railway.SelectedItem.ToString()),
                id_Railway_Car = railway_Car.id_Railway_Car,
                Railway = null,
                Railway1 = null,
                Railway_Car = null
            };
            uk_koksEntities2.GetContext().Railway_Railway_Car.Add(railway_Railway_Car);
            uk_koksEntities2.GetContext().SaveChanges();
            Button_Click_Refresh(sender, e);
            Page_Loaded(sender, e);
        } 

        private void Button_Click_Refresh(object sender, RoutedEventArgs e)
        {
            
            if(cmb_train_select.SelectedItem != null)
            {
                string name = cmb_train_select.Text.Trim();
                Railway_Car railway_Car = uk_koksEntities2.GetContext().Railway_Car.Where(item => item.Name_Car == name).FirstOrDefault();
                int id = railway_Car.id_Railway_Car;
                dg_Wagons3.ItemsSource = uk_koksEntities2.GetContext().SelectRailWayDicpathch(3, id);
            }
            else
            {
                MessageBox.Show("Выберите состав для отображения списка");
            }
            
        }
          
        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            if (dg_Wagons3.SelectedItem != null)
            {
                var selectedItem = (SelectRailWayDicpathch_Result)dg_Wagons3.SelectedItem;
                Railway_Railway_Car railwayRailwayCar = uk_koksEntities2.GetContext().Railway_Railway_Car.Where(item => item.id == selectedItem.НомерЗаписи).FirstOrDefault();
                uk_koksEntities2.GetContext().Railway_Railway_Car.Remove(railwayRailwayCar);
                uk_koksEntities2.GetContext().SaveChanges();
                Page_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Выберете запись");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            cmb_railway.Items.Clear();
            cmb_train_add.Items.Clear();
            cmb_train_select.Items.Clear();
            List<Railway> railway = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Status_Car == 2).ToList();
            List<Railway_Car> railway_Cars_Add = uk_koksEntities2.GetContext().Railway_Car.Where(item => item.id_Status_Car < 7).ToList();
            List<Railway_Car> railway_Cars_Search = uk_koksEntities2.GetContext().Railway_Car.Where(item => item.id_Status_Car < 7).ToList();
            for (int i = 0; i < railway.Count; i++)
            {
                cmb_railway.Items.Add(railway[i].id_Railway);
            }
            for (int i = 0; i < railway_Cars_Add.Count; i++)
            {
                cmb_train_add.Items.Add(railway_Cars_Add[i].Name_Car);
            }
            for (int i = 0; i < railway_Cars_Search.Count; i++)
            {
                cmb_train_select.Items.Add(railway_Cars_Search[i].Name_Car);
            }
        }
    }
}
