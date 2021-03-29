using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FireBase.Model
{
    class HelpTools
    {
        /// <summary>
        /// Метод, который проверяет есть ли в БД такой Email
        /// </summary>
        /// <param name="rezult">вся БД</param>
        /// <param name="Email">уникальный идентификатор пользователя (Email)</param>
        /// <returns>Возвращает  Id и соответствующий Email</returns>
        public static bool CheckExistingEmail(Dictionary<int, User> rezult, string Email)
        {
            bool emailNotExist = true;
            if (rezult != null)
            {
                foreach (User currentUser in rezult.Values)
                {
                    if (currentUser.Email == Email)
                    {
                        emailNotExist = false;
                        MessageBox.Show($"Пользователь c таким имейл уже существует!");
                    }
                }
            }
            return emailNotExist;
        }


        /// <summary>
        /// Метод, который генерирует уникальный ID
        /// </summary>
        /// <param name="rezult">вся БД</param>
        /// <param name="Email">уникальный идентификатор пользователя (Email)</param>
        /// <returns>Возвращает уникальный Id</returns>
        public static int GenerateID(Dictionary<int, User> rezult, string Email)
        {
            int Id = Email.GetHashCode();
            if (rezult != null)
            {
                for (int i = 0; i < rezult.Keys.Count; i++)
                {
                    if (Id == rezult.Keys.ElementAt(i))
                    {
                        Random rnd = new Random();
                        Id = rnd.Next(-2147483648, 2147483647); // любое целое число(но так чтоб в Int поместилось)                      
                        i = 0;
                    }
                }
            }
            return Id;
        }

        /// <summary>
        /// Метод, который находит Id по Email уникальный ID
        /// </summary>
        /// <param name="rezult">вся БД</param>
        /// <param name="Email">уникальный идентификатор пользователя (Email)</param>
        /// <returns>Возвращает  Id и соответствующий Email</returns>
        public static (int,bool) FindIdToInputEmail(Dictionary<int, User> rezult, string Email)
        {
            var (Id, FindEmail) = ( 0,  false);

            if (rezult != null)
            {
                int index = 0;
                foreach (User currentUser in rezult.Values)
                {                    
                    if (currentUser.Email == Email)
                    {
                        Id = rezult.Keys.ElementAt(index);
                        FindEmail = true;
                        break;
                    }
                    index++;
                }
            }
            return (Id, FindEmail);
        }
    }
}
