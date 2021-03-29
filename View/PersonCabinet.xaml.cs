using FireBase.Model;
using FireBase.ViewModel;
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
using System.Windows.Shapes;

namespace FireBase.View
{
    /// <summary>
    /// Логика взаимодействия для PersonCabinet.xaml
    /// </summary>
    public partial class PersonCabinet : Window
    {
        
        public PersonCabinet(User concreteUser, int Id)
        {
            InitializeComponent();           
            this.DataContext = new PersonCabinetVM(concreteUser, Id);  // подписка на контекст данных

        }
    }
}
