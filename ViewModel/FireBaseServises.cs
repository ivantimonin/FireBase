using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FireBase.ViewModel
{
    public class FireBaseServises
    {
        /// <summary>
        /// Параметры подключения к Firebase
        /// </summary>
        public IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "TV2IK6HyHgmUrWdiRQQWvKSZUW7BZ6asxQGZj9yz",
            BasePath = "https://testvicecode-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        /// <summary>
        /// Клиент подключения к Firebase
        /// </summary>
        public IFirebaseClient client;

        /// <summary>
        /// Конструктор создания клиента подключения к Firebase
        /// </summary>
        public FireBaseServises()
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }            
    }
}
