using FireBase.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Windows;
using FireSharp.Response;
using FireBase.Model;

namespace FireBase.ViewModel
{
    class MainVM : FireBaseServises, INotifyPropertyChanged
    {
        /// <summary>
        /// Имейл пользователя
        /// </summary>
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
        /// Пароль пользоваетля
        /// </summary>
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
        /// Команда открытия окна регистрации нового пользователя
        /// </summary>
        public ICommand OpenRgisterWindow { get; private set; }
       
        /// <summary>
        /// Команда открытия личного кабинета пользователя
        /// </summary>
        public ICommand OpenPersonCabinet { get; private set; }

        /// <summary>
        /// Команда вспомнить пароль, если забыл
        /// </summary>
        public ICommand ForgotPassword { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainVM() : base()
        {
            OpenRgisterWindow = new DelegateCommand(FunctionOpenRgisterWindow);
            OpenPersonCabinet = new DelegateCommand(FunctionOpenPersonCabinet);
            ForgotPassword = new DelegateCommand(FunctionForgotPassword);
        }

        /// <summary>
        /// Метод, который помогает вспомнить пароль
        /// </summary>
        private void FunctionForgotPassword(object obj)
        {
            MessageBox.Show("Функция в процессе реализации");
        }

        /// <summary>
        /// Метод открытия личного кабинета пользователя
        /// </summary>
        private void FunctionOpenRgisterWindow(object obj)
        {            
            Registration RegistrationWindow = new Registration();
            RegistrationWindow.ShowDialog();
        }

        /// <summary>
        /// Метод открытия личного кабинета пользователя
        /// </summary>
        private void FunctionOpenPersonCabinet(object obj)
        {
            try
            {
                FirebaseResponse res = client.Get(@"Users/");
                Dictionary<int, User> rezult = res.ResultAs<Dictionary<int, User>>();// получить всех User
                var (Id, FindEmail) = HelpTools.FindIdToInputEmail(rezult, Email);
                if (FindEmail)// если имейл существует
                {
                    FirebaseResponse resOneUser = client.Get(@"Users/" + Id);
                    User concreteUser = resOneUser.ResultAs<User>();
                    if (concreteUser.Password == Password)
                    {
                        PersonCabinet PersonCabinetWindow = new PersonCabinet(concreteUser, Id);
                        PersonCabinetWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль!");
                    }

                }
                else
                {
                    MessageBox.Show("Некорректно введены данные");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} Возможно отсутсвует подключение к сети интернет.");
            }   
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
