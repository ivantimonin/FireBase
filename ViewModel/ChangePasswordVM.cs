using FireBase.Model;
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
    class ChangePasswordVM : FireBaseServises,  INotifyPropertyChanged
    {
        /// <summary>
        /// Новый пароль
        /// </summary>
        private string newPassword;
        public string NewPassword
        {
            get
            {
                return newPassword;
            }
            set
            {

                newPassword = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Личный кабинет пользователя
        /// </summary>
        private PersonCabinetVM personCabinetVM;

        /// <summary>
        /// Команда изменения пароля
        /// </summary>
        public ICommand SaveChangePassword { get; private set; }

        // <summary>
        /// Конструктор
        /// </summary>
        public ChangePasswordVM(PersonCabinetVM personCabinetVM)
        {
            this.personCabinetVM = personCabinetVM;
            SaveChangePassword = new DelegateCommand(FunctionChangePassword);
        }

        /// <summary>
        /// Метод изменения пароля
        /// </summary>
        private void FunctionChangePassword(object obj)
        {            
            if (NewPassword?.Length>0)
            {                
                personCabinetVM.Password = NewPassword;
                User UserWgithNewPassword = new User(personCabinetVM.Name, personCabinetVM.Surname,
                                                     personCabinetVM.Email, NewPassword);
                FirebaseResponse response = personCabinetVM.client.Update(@"Users/" + personCabinetVM.Id, UserWgithNewPassword);
            }
            else
            {
                MessageBox.Show("Пароль не может быть пуст");
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
