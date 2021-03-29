using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FireBase.Model;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace FireBase.ViewModel
{
    class CreateNewAccountVM: FireBaseServises, INotifyPropertyChanged
    {
        /// <summary>
        /// Имя нового пользоваетля
        /// </summary>       
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
        /// Фамилия нового пользоваетля
        /// </summary>  
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
        /// Имейл нового пользоваетля
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
        /// Пароль нового пользоваетля
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
        /// Имейл нового пользоваетля
        /// </summary> 
        public ICommand CreateAccount { get; private set; }

        /// <summary>
        /// Конструктор
        /// /// </summary>
        public CreateNewAccountVM():base()
        {            
            CreateAccount = new DelegateCommand(CreateNewAccount);
        }

        //Регистрация нового пользователя
        private void CreateNewAccount(object obj)
        {
            if (Name=="" || Surname == "" || Email == "" || Password == "" )
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
                return;
            }

            try
            {
                FirebaseResponse res = client.Get(@"Users/");
                Dictionary<int, User> rezult = res.ResultAs<Dictionary<int, User>>();// получить всех User

                bool emailNotExist = HelpTools.CheckExistingEmail(rezult, Email);
                int ID = HelpTools.GenerateID(rezult, Email);

                if (emailNotExist)
                {
                    User someUser = new User(Name, Surname, Email, Password);
                    SetResponse set = client.Set(@"Users/" + ID, someUser);                   
                    MessageBox.Show("Регистарация нового пользователя произведена успешно!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} Возможно, отсутсвует подключение к сети интернет.");
            }
            
        }

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
