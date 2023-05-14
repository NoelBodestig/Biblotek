using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotek_Uppgift
{
    public class User
    {
        public string FirstName;
        public string Surname;
        public string SocialSecurityNumber;
        public string Password;
        public static string Title = "Member";
        public static string BorrowedBooks = "";

        public User(string FirstName, string Surname, string SocialSecurityNumber, string Password)
        {
            this.FirstName = FirstName;
            this.Surname = Surname;
            this.SocialSecurityNumber = SocialSecurityNumber;
            this.Password = Password;
        }
    }
}
