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
using Application = Microsoft.Office.Interop.Word.Application;
using Paragraph = Microsoft.Office.Interop.Word.Paragraph;
using Table = Microsoft.Office.Interop.Word.Table;
using Page = System.Windows.Controls.Page;

namespace Logistics
{
    /// <summary>
    /// Логика взаимодействия для DispatchOfWagons.xaml
    /// </summary>
    public partial class DispatchOfWagons : Page
    {
        private User User = new User();
        public DispatchOfWagons(User user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Railway_Car railway_Car = uk_koksEntities2.GetContext().Railway_Car.Where(item => item.Name_Car == cmb_RailwayCar.SelectedItem.ToString()).FirstOrDefault();
            Station_Distination station_Distination = uk_koksEntities2.GetContext().Station_Distination.Where(item => item.Name == cmb_StationDistination.SelectedItem.ToString()).FirstOrDefault();
            Sending_Railway sending_Railway = new Sending_Railway()
            {
                DateTime = DateTime.Now,
                id_Railway_Car = railway_Car.id_Railway_Car,
                id_Station_Distination = station_Distination.id_Station_Distination,
                id_user = User.id_user,
                Railway_Car = null,
                Station_Distination = null,
                User = User,
            };
            uk_koksEntities2.GetContext().Sending_Railway.Add(sending_Railway);
            uk_koksEntities2.GetContext().SaveChanges();
            Page_Loaded(sender, e);
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            var selectedItem = (SelectRailWayCarSending_Result)dg_RailwaySending.SelectedItem;
            Sending_Railway sendingCar = uk_koksEntities2.GetContext().Sending_Railway.Where(item => item.id_Sending_Railway == selectedItem.НомерЗаписи).FirstOrDefault();
            uk_koksEntities2.GetContext().Sending_Railway.Remove(sendingCar);
            uk_koksEntities2.GetContext().SaveChanges();
            Page_Loaded(sender, e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_StationDistination.Items.Clear();
            cmb_RailwayCar.Items.Clear();
            List<Railway_Car> railway_Cars_Add = uk_koksEntities2.GetContext().Railway_Car.Where(item => item.id_Status_Car == 6).ToList();
            List<Station_Distination> station_Distinations = uk_koksEntities2.GetContext().Station_Distination.ToList();
            for (int i = 0; i < railway_Cars_Add.Count; i++)
            {
                cmb_RailwayCar.Items.Add(railway_Cars_Add[i].Name_Car);
            }
            for (int i = 0;i < station_Distinations.Count;i++)
            {
                cmb_StationDistination.Items.Add(station_Distinations[i].Name);
            }
            dg_RailwaySending.ItemsSource = uk_koksEntities2.GetContext().SelectRailWayCarSending();
        }

        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            var wordApp = new Application();
            var document = wordApp.Documents.Add();

            // Добавляем заголовок
            Paragraph paragraph = document.Content.Paragraphs.Add();
            paragraph.Range.Text = "Отчет по отправленным составам " + DateTime.Now.ToString();
            paragraph.Range.InsertParagraphAfter();

            // Создаем таблицу в Word
            int rowCount = dg_RailwaySending.Items.Count;
            int columnCount = 5;

            Table table = document.Tables.Add(paragraph.Range, rowCount + 1, columnCount);
            table.Borders.Enable = 1; // Включаем границы таблицы

            // Заполняем заголовки столбцов

            table.Cell(1, 0 + 1).Range.Text = "НомерЗаписи";
            table.Cell(1, 0 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 1 + 1).Range.Text = "Поезд";
            table.Cell(1, 1 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 2 + 1).Range.Text = "СтанцияПолучателя";
            table.Cell(1, 2 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 3 + 1).Range.Text = "Фамилия";
            table.Cell(1, 3 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 4 + 1).Range.Text = "ДатаСоздания";
            table.Cell(1, 4 + 1).Range.Bold = 1; // Делаем текст жирным

            // Заполняем данные
            for (int i = 0; i < rowCount; i++)
            {
                table.Cell(i + 2, 1).Range.Text = ((SelectRailWayCarSending_Result)dg_RailwaySending.Items[i]).НомерЗаписи.ToString();
                table.Cell(i + 2, 2).Range.Text = ((SelectRailWayCarSending_Result)dg_RailwaySending.Items[i]).Поезд.ToString();
                table.Cell(i + 2, 3).Range.Text = ((SelectRailWayCarSending_Result)dg_RailwaySending.Items[i]).СтанцияПолучателя.ToString();
                table.Cell(i + 2, 4).Range.Text = ((SelectRailWayCarSending_Result)dg_RailwaySending.Items[i]).Фамилия.ToString();
                table.Cell(i + 2, 5).Range.Text = ((SelectRailWayCarSending_Result)dg_RailwaySending.Items[i]).ДатаСоздания.ToString();
            }

            // Показываем документ
            wordApp.Visible = true;

            // Освобождаем ресурсы
            System.Runtime.InteropServices.Marshal.ReleaseComObject(document);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
        }
    }
}
