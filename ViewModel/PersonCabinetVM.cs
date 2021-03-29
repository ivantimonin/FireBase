using FireBase.Model;
using FireBase.View;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FireBase.ViewModel
{
    public class PersonCabinetVM: FireBaseServises,INotifyPropertyChanged
    {
        /// <summary>
        /// Уникальный Id конкретного пользователя
        /// </summary>
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Имя конкретного пользователя
        /// </summary
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Фамилия конкретного пользователя
        /// </summary
        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {               
                surname = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Пароль конкретного пользователя
        /// </summary
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {             
                password = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Имейл конкретного пользователя
        /// </summary
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команда удаления пользователя
        /// </summary>
        public ICommand DelateAccaunt { get; private set; }
        
        /// <summary>
        /// Команда изменения пароля пользователя
        /// </summary>
        public ICommand ChangePassword { get; private set; }

        /// <summary>
        /// Конструктор личного кабинета
        /// </summary>
        public PersonCabinetVM(User SomeUser, int Id)
        {           
            this.Id = Id;            
            Surname = SomeUser.Surname;
            Name= SomeUser.Name;
            Email = SomeUser.Email;
            Password = SomeUser.Password;
            DelateAccaunt = new DelegateCommand(FunctionDelateAccaunt);
            ChangePassword = new DelegateCommand(FunctionChangePassword);
        }

        /// <summary>
        /// Метод изменения пароля пользователя
        /// </summary>
        private void FunctionChangePassword(object obj)
        {
           ChangePassword ChangePassword = new ChangePassword(this);
           ChangePassword.ShowDialog();
        }

        /// <summary>
        /// Метод удаления аккаунта
        /// </summary>
        private void FunctionDelateAccaunt(object obj)
        {            
            FirebaseResponse res = client.Delete(@"Users/"+Id);           
            MessageBox.Show("Аккаунт удален");
        }

        /// <summary>
        /// Релизаця INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }        
    }
}
