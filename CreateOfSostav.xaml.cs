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
    }
}
