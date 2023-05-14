using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bibliotek_Uppgift
{
    class ManageUser
    {
        private static string UserFirstName = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userFirstName.txt";
        private static string UserSurname = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userSurname.txt";
        private static string UserSocialSecurityNumber = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userSocialSecurityNumber.txt";
        private static string UserPassword = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userPassword.txt";
        private static string UserTitle = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userTitle.txt";
        private static string BorrowedBooks = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\BorrowedBooks.txt";
        public static void ChangePassword(int UserIndex) //Funktion som tillåter användaren att byta sitt eget lösenord
        {
            List<string> PasswordList = new List<string>(File.ReadAllLines(UserPassword));
            Console.WriteLine("Skriv in ditt nya lösenord");
            string NewPassword = Console.ReadLine();
            PasswordList[UserIndex] = NewPassword;
            File.WriteAllLines(UserPassword, PasswordList);
            Console.WriteLine("Du har bytt lösenord!");
            Console.ReadLine();
            Console.Clear();
            MainPage.UserMainScreen(UserIndex);
        }
        public static void AddUser()
        {
            Console.Write("Förnamn: ");
            string FirstName = Console.ReadLine();
            Console.Write("Efternamn: ");
            string Surname = Console.ReadLine();
            Console.Write("Personnummer(ÅÅMMDDNNNC): ");
            string SocialSecurityNumber = Console.ReadLine();
            SocialSecurityNumber = LoginPage.CheckSSN(SocialSecurityNumber);
            if (LoginPage.SSNExists(SocialSecurityNumber))
            {
                Console.WriteLine("Personnummret är redan registrerat!");
                Console.ReadLine();
                Console.Clear();
                MainPage.LibrarianMainScreen();
                return;
            }
            Console.Write("Lösenord: ");
            string Password = Console.ReadLine();
            Console.WriteLine("Du har registrerat en användare!");
            Console.ReadLine();
            Console.Clear();

            User user = new User(FirstName, Surname, SocialSecurityNumber, Password);
            File.AppendAllText(UserFirstName, user.FirstName + Environment.NewLine);
            File.AppendAllText(UserSurname, user.Surname + Environment.NewLine);
            File.AppendAllText(UserSocialSecurityNumber, user.SocialSecurityNumber + Environment.NewLine);
            File.AppendAllText(UserPassword, user.Password + Environment.NewLine);
            File.AppendAllText(UserTitle, User.Title + Environment.NewLine);
            File.AppendAllText(BorrowedBooks, User.BorrowedBooks + Environment.NewLine);

            MainPage.LibrarianMainScreen();
        }
        public static void RemoveUser()
        {
            List<string> SSNList = new List<string>(File.ReadAllLines(UserSocialSecurityNumber));
            List<string> PasswordList = new List<string>(File.ReadAllLines(UserPassword));
            List<string> FirstNameList = new List<string>(File.ReadAllLines(UserFirstName));
            List<string> SurnameList = new List<string>(File.ReadAllLines(UserSurname));
            List<string> BorrowedList = new List<string>(File.ReadAllLines(BorrowedBooks));

            Console.Write("Skriv in personnummer på användare du vill ta bort: ");
            string SocialSecurityNumber = Console.ReadLine();
            if(LoginPage.SSNExists(SocialSecurityNumber) == false)
            {
                Console.WriteLine("Personnummer ej registrerat");
                Console.ReadLine();
                Console.Clear();
                MainPage.LibrarianMainScreen();
                return;
            }
            int index = SSNList.FindIndex(a => a.Contains(SocialSecurityNumber));

            SSNList.RemoveAt(index);
            PasswordList.RemoveAt(index);
            FirstNameList.RemoveAt(index);
            SurnameList.RemoveAt(index);
            BorrowedList.RemoveAt(index);

            File.WriteAllLines(UserSocialSecurityNumber, SSNList);
            File.WriteAllLines(UserPassword, PasswordList);
            File.WriteAllLines(UserFirstName, FirstNameList);
            File.WriteAllLines(UserSurname, SurnameList);
            File.WriteAllLines(BorrowedBooks, BorrowedList);

            Console.WriteLine("Användare raderad!");
            Console.ReadLine();
            Console.Clear();
            MainPage.LibrarianMainScreen();
        }
        public static void EditUser()
        {
            List<string> SSNList = new List<string>(File.ReadAllLines(UserSocialSecurityNumber));

            Console.Write("Skriv in personnummer på användare du vill redigera: ");
            string SocialSecurityNumber = Console.ReadLine(); 
            if(LoginPage.SSNExists(SocialSecurityNumber) == false)
            {
                Console.WriteLine("Personnummer ej registrerat!");
                Console.ReadLine();
                Console.Clear();
                MainPage.LibrarianMainScreen();
                return;
            }
            int Index = SSNList.FindIndex(a => a.Contains(SocialSecurityNumber));

            Console.WriteLine("Vill du redigera förnamn(1), efternamn(2) eller lösenord(3)");
            int Input = Int32.Parse(Console.ReadLine());

            while (Input < 1 || Input > 3)
            {
                Console.WriteLine("Ej giltig input");
                Input = Int32.Parse(Console.ReadLine());
            }
            if(Input == 1)
            {
                UpdateFirstName(Index);
            }
            else if(Input == 2)
            {
                UpdateSurname(Index);
            }
            else 
            {
                UpdatePassword(Index);
            }
            
            Console.WriteLine("Användare uppdaterad");
            Console.ReadLine();
            Console.Clear();
            MainPage.LibrarianMainScreen();
        }
        public static void UpdateFirstName(int Index)
        {
            List<string> FirstNameList = new List<string>(File.ReadAllLines(UserFirstName));
            Console.Write("Skriv in nytt förnamn: ");
            string NewFirstName = Console.ReadLine();
            FirstNameList[Index] = NewFirstName;
            File.WriteAllLines(UserFirstName, FirstNameList);
        }
        public static void UpdateSurname(int Index)
        {
            List<string> SurnameList = new List<string>(File.ReadAllLines(UserSurname));
            Console.Write("Skriv in nytt efternamn: ");
            string NewSurname= Console.ReadLine();
            SurnameList[Index] = NewSurname;
            File.WriteAllLines(UserSurname, SurnameList);
        }
        public static void UpdatePassword(int Index) //Funktion som tillåter bibliotekarien att byta användarens lösenord
        {
            List<string> PasswordList = new List<string>(File.ReadAllLines(UserPassword));
            Console.Write("Skriv in nytt lösenord: ");
            string NewPassword = Console.ReadLine();
            PasswordList[Index] = NewPassword;
            File.WriteAllLines(UserPassword, PasswordList);
        }
        public static void ListUsers()
        {
            string[] FirstNames = File.ReadAllLines(UserFirstName);
            string[] Surnames = File.ReadAllLines(UserSurname);
            string[] Titles = File.ReadAllLines(UserTitle);
            Console.WriteLine("Användare:");
            for (int i = 0; i < FirstNames.Length; i++)
            {
                if (Titles[i] == "Member")
                {
                    Console.WriteLine($"{FirstNames[i]} {Surnames[i]}");
                }
            }
            Console.ReadLine();
            Console.Clear();
            MainPage.LibrarianMainScreen();
        }
    }
}
