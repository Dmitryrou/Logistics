using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Text.RegularExpressions;//Добавили пространнство имен регулярные выражения 
using Application = Microsoft.Office.Interop.Word.Application;
using Paragraph = Microsoft.Office.Interop.Word.Paragraph;
using Table = Microsoft.Office.Interop.Word.Table;
using Page = System.Windows.Controls.Page;

namespace Logistics
{
    /// <summary>
    /// Логика взаимодействия для Sklad.xaml
    /// </summary>
    public partial class Sklad : Page
    {
        private User User = new User();
        public Sklad(User user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void Button_Click_Insert(object sender, RoutedEventArgs e)
        {
            backgroundTransparent();
            string shablonName = @"^[1-9]\d{0,6}$";
            Regex myRegex = new Regex(shablonName);
            if (myRegex.IsMatch(txb_quantity.Text) && cmb_Storage.SelectedItem != null)
            {
                
                    Nomenclature nomenclature = uk_koksEntities2.GetContext().Nomenclature.Where(item => item.Name == cmb_Storage.SelectedItem.ToString()).FirstOrDefault();
                    Storage_Accounting storage = uk_koksEntities2.GetContext().Storage_Accounting.Where(item => item.id_Nomenclature == nomenclature.id_Nomenclature).FirstOrDefault();
                    storage.quantity = Convert.ToInt32(txb_quantity.Text);
                    uk_koksEntities2.GetContext().SaveChanges();
                    Button_Click_Refresh(sender, e);
            }
            else
            {
                if(cmb_Storage.SelectedItem == null)
                {
                    MessageBox.Show("Выберете номенклатуру");
                    cmb_Storage.Background = Brushes.Red;
                }
                if(!myRegex.IsMatch(txb_quantity.Text))
                {
                    txb_quantity.Background = Brushes.Red;
                    txb_quantity.ToolTip = "Введите только положительное цисло не более 7 знаков";
                }                
            }             
        }

        private void Button_Click_Refresh(object sender, RoutedEventArgs e)
        { 
            cmb_Storage.Items.Clear();
            List<Nomenclature> nomenclatures = uk_koksEntities2.GetContext().Nomenclature.ToList();
            List<Storage_Accounting> storages = uk_koksEntities2.GetContext().Storage_Accounting.ToList();
            for (int i = 0; i < nomenclatures.Count; i++)
            {
                cmb_Storage.Items.Add(nomenclatures[i].Name);
            }
            tbx_1.Text = nomenclatures[0].Name.ToString();
            tbx_2.Text = nomenclatures[1].Name.ToString();
            tbx_3.Text = nomenclatures[2].Name.ToString();
            tbx_4.Text = nomenclatures[3].Name.ToString();
            tbx_5.Text = nomenclatures[4].Name.ToString();
            tbx_6.Text = nomenclatures[5].Name.ToString();
            tbx_11.Text = storages[0].quantity.ToString();
            tbx_22.Text = storages[1].quantity.ToString();
            tbx_33.Text = storages[2].quantity.ToString();
            tbx_44.Text = storages[3].quantity.ToString();
            tbx_55.Text = storages[4].quantity.ToString();
            tbx_66.Text = storages[5].quantity.ToString();
        }
        void backgroundTransparent()
        {
            txb_quantity.Background = Brushes.Transparent;
            txb_quantity.ToolTip = null;
            cmb_Storage.ToolTip = null;
            cmb_Storage.Background = Brushes.Transparent;
        }

        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            var wordApp = new Application();
            var document = wordApp.Documents.Add();

            // Добавляем заголовок
            Paragraph paragraph = document.Content.Paragraphs.Add();
            paragraph.Range.Text = "Отчет по остаткам на складах " + DateTime.Now.ToString();
            paragraph.Range.InsertParagraphAfter();

            // Создаем таблицу в Word
            int rowCount = 1;
            int columnCount = 6;

            Table table = document.Tables.Add(paragraph.Range, rowCount + 1, columnCount);
            table.Borders.Enable = 1; // Включаем границы таблицы

            // Заполняем заголовки столбцов

            table.Cell(1, 0 + 1).Range.Text = tbx_1.Text;
            table.Cell(1, 0 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 1 + 1).Range.Text = tbx_2.Text;
            table.Cell(1, 1 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 2 + 1).Range.Text = tbx_3.Text;
            table.Cell(1, 2 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 3 + 1).Range.Text = tbx_4.Text;
            table.Cell(1, 3 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 4 + 1).Range.Text = tbx_5.Text;
            table.Cell(1, 4 + 1).Range.Bold = 1; // Делаем текст жирным
            table.Cell(1, 5 + 1).Range.Text = tbx_6.Text;
            table.Cell(1, 5 + 1).Range.Bold = 1; // Делаем текст жирным

            // Заполняем данные
            for (int i = 0; i < rowCount; i++)
            {
                table.Cell(i + 2, 1).Range.Text = tbx_11.Text;
                table.Cell(i + 2, 2).Range.Text = tbx_22.Text;
                table.Cell(i + 2, 3).Range.Text = tbx_33.Text;
                table.Cell(i + 2, 4).Range.Text = tbx_44.Text;
                table.Cell(i + 2, 5).Range.Text = tbx_55.Text;
                table.Cell(i + 2, 6).Range.Text = tbx_66.Text;

            }

            // Показываем документ
            wordApp.Visible = true;

            // Освобождаем ресурсы
            System.Runtime.InteropServices.Marshal.ReleaseComObject(document);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
        }
    }
}
