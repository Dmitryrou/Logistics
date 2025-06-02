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

        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            var wordApp = new Application();
            var document = wordApp.Documents.Add();

            // Добавляем заголовок
            Paragraph paragraph = document.Content.Paragraphs.Add();
            paragraph.Range.Text = "Отчет по поездам на " + DateTime.Now.ToString();
            paragraph.Range.InsertParagraphAfter();

            // Создаем таблицу в Word
            int rowCount = dg_Train.Items.Count;
            int columnCount = 3;

            Table table = document.Tables.Add(paragraph.Range, rowCount + 1, columnCount);
            table.Borders.Enable = 1; // Включаем границы таблицы

            // Заполняем заголовки столбцов

            table.Cell(1, 0 + 1).Range.Text = "Идентификационный Номер";
            table.Cell(1, 0 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 1 + 1).Range.Text = "Наименование";
            table.Cell(1, 1 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 2 + 1).Range.Text = "Статус";
            table.Cell(1, 2 + 1).Range.Bold = 1; // Делаем текст жирным




            // Заполняем данные
            for (int i = 0; i < rowCount; i++)
            {
                table.Cell(i + 2, 1).Range.Text = ((SelectRailWayCar_Result)dg_Train.Items[i]).ИдентификационныйНомер.ToString();
                table.Cell(i + 2, 2).Range.Text = ((SelectRailWayCar_Result)dg_Train.Items[i]).Наименование.ToString();
                table.Cell(i + 2, 3).Range.Text = ((SelectRailWayCar_Result)dg_Train.Items[i]).Статус.ToString();
            }

            // Показываем документ
            wordApp.Visible = true;

            // Освобождаем ресурсы
            System.Runtime.InteropServices.Marshal.ReleaseComObject(document);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
        }
    }
}
