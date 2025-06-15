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
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using Paragraph = Microsoft.Office.Interop.Word.Paragraph;
using Table = Microsoft.Office.Interop.Word.Table;
using Page = System.Windows.Controls.Page;


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
            dg_Wagons2.ItemsSource = uk_koksEntities2.GetContext().SelectRailWayLoading(2);//Заполнение датагрид
            //Очистка комбобоксов
            cbx_nomenclature.Items.Clear();
            cbx_stationLoading.Items.Clear();
            cbx_id_railway.Items.Clear();
            //Создание листов для заполнения комбобоксов
            List<Railway> railway = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Status_Car == 1).ToList();
            List<Nomenclature> nomenclatures = uk_koksEntities2.GetContext().Nomenclature.ToList();
            List<Station_Loading> station_Loadings = uk_koksEntities2.GetContext().Station_Loading.ToList();
            //Заполнение комбобоксов
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
                    )//Если все комбобоксы заполненны
                {
                    Nomenclature nomenclature = uk_koksEntities2.GetContext().Nomenclature.Where(item => item.Name == cbx_nomenclature.SelectedItem.ToString()).FirstOrDefault();
                    Storage_Accounting storage = uk_koksEntities2.GetContext().Storage_Accounting.Where(item => item.id_Nomenclature == nomenclature.id_Nomenclature).FirstOrDefault();
                    Station_Loading station = uk_koksEntities2.GetContext().Station_Loading.Where(item => item.Name == cbx_stationLoading.SelectedItem.ToString()).FirstOrDefault();
                    int idRailway = Convert.ToInt32(cbx_id_railway.SelectedItem);
                    Railway railway1 = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Railway == idRailway).FirstOrDefault();
                    int capacity = Convert.ToInt32(txb_quantity.Text.Trim());
                    if (railway1.Capacity_Car >= capacity) // Если Вместимость вагона меньше или равно количеству
                    {
                        Loading_Railway railway = new Loading_Railway()//Создание переменной которую будем записывать в БД
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
                        uk_koksEntities2.GetContext().Loading_Railway.Add(railway);//Добавление в БД
                        uk_koksEntities2.GetContext().SaveChanges();//Сохранение данных
                        Page_Loaded(sender, e);
                    }
                    else                    
                        MessageBox.Show("Количесто не должно превышать вместимость выбранного вагона");                   
                }
                else
                    MessageBox.Show("Заполните все данные");
            }
            else
                MessageBox.Show("Поле количество не должно содержать буквы или знаки");
        }

        private void Button_Click_remove(object sender, RoutedEventArgs e)
        {
            if (dg_Wagons2.SelectedItem != null)// если элемент выбран
            {
                var selectedItem = (SelectRailWayLoading_Result)dg_Wagons2.SelectedItem;
                Railway railway = uk_koksEntities2.GetContext().Railway.Where(item => item.id_Railway == selectedItem.Номер).FirstOrDefault();
                Loading_Railway loading_Railway = uk_koksEntities2.GetContext().Loading_Railway.Where(item => item.id_Railway == selectedItem.Номер).FirstOrDefault();
                Storage_Accounting storage_Accounting = uk_koksEntities2.GetContext().Storage_Accounting.Where(item => item.id_Storage == loading_Railway.id_Storage).FirstOrDefault();
                storage_Accounting.quantity = storage_Accounting.quantity + loading_Railway.Capacity;
                uk_koksEntities2.GetContext().Loading_Railway.Remove(loading_Railway);//Удаление найденной переменной
                uk_koksEntities2.GetContext().SaveChanges();
                dg_Wagons2.ItemsSource =uk_koksEntities2.GetContext().SelectRailWayLoading(2);
            }
            else
            {
                MessageBox.Show("Выберете запись");
            }
        }

        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            var wordApp = new Application();
            var document = wordApp.Documents.Add();
            // Добавляем заголовок
            Paragraph paragraph = document.Content.Paragraphs.Add();
            paragraph.Range.Text = "Отчет по загруженным вагонам на " + DateTime.Now.ToString();
            paragraph.Range.InsertParagraphAfter();
            // Создаем таблицу в Word
            int rowCount = dg_Wagons2.Items.Count;
            int columnCount = 7;
            Table table = document.Tables.Add(paragraph.Range, rowCount + 1, columnCount);
            table.Borders.Enable = 1; // Включаем границы таблицы
            // Заполняем заголовки столбцов
            table.Cell(1, 0 + 1).Range.Text = "Номер";
            table.Cell(1, 0 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 1 + 1).Range.Text = "Нетто";
            table.Cell(1, 1 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 2 + 1).Range.Text = "Вместимость";
            table.Cell(1, 2 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 3 + 1).Range.Text = "Дата";
            table.Cell(1, 3 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 4 + 1).Range.Text = "ВесВагона";
            table.Cell(1, 4 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 5 + 1).Range.Text = "Фамилия";
            table.Cell(1, 5 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 6 + 1).Range.Text = "Имя";
            table.Cell(1, 6 + 1).Range.Bold = 1; // Делаем текст жирным
            // Заполняем данные
            for (int i = 0; i < rowCount; i++)
            {
                table.Cell(i + 2, 1).Range.Text = ((SelectRailWayLoading_Result)dg_Wagons2.Items[i]).Номер.ToString();
                table.Cell(i + 2, 2).Range.Text = ((SelectRailWayLoading_Result)dg_Wagons2.Items[i]).Нетто.ToString();
                table.Cell(i + 2, 3).Range.Text = ((SelectRailWayLoading_Result)dg_Wagons2.Items[i]).Вместимость.ToString();
                table.Cell(i + 2, 4).Range.Text = ((SelectRailWayLoading_Result)dg_Wagons2.Items[i]).Дата.ToString();
                table.Cell(i + 2, 5).Range.Text = ((SelectRailWayLoading_Result)dg_Wagons2.Items[i]).ВесВагона.ToString();
                table.Cell(i + 2, 6).Range.Text = ((SelectRailWayLoading_Result)dg_Wagons2.Items[i]).Фамилия.ToString();
                table.Cell(i + 2, 7).Range.Text = ((SelectRailWayLoading_Result)dg_Wagons2.Items[i]).Имя.ToString();
            }
            // Показываем документ
            wordApp.Visible = true;
        }
    }
}
