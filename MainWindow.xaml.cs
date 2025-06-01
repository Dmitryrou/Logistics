using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User user { get; set; } = new User();

        private UserWindow userWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Auth(object sender, RoutedEventArgs e)
        {
            string login = Login_txb.Text.Trim(); 
            string password = Password_Pas.Password.Trim();
            ObjectResult<Nullable<int>> id = uk_koksEntities2.GetContext().CheckPassword(login, password);
            // Извлекаем значение
            Nullable<int> nullableInt = id.FirstOrDefault();

            // Преобразуем в int, если значение не null
            int result = nullableInt ?? 0;

            if (result > 0)
            {
                user = uk_koksEntities2.GetContext().User.Where(item => item.id_user == result).FirstOrDefault();
                MessageBox.Show("Здравствуйте, " + user.Name + ", вы успешно вошли в систему ",
                    "Успешная авторизация",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                userWindow = new UserWindow(user);
                userWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы ввели неверный логин или пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
