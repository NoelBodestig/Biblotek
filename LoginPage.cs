using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bibliotek_Uppgift
{
    public class LoginPage
    {
        private static string UserFirstName = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userFirstName.txt";
        private static string UserSurname = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userSurname.txt";
        private static string UserSocialSecurityNumber = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userSocialSecurityNumber.txt";
        private static string UserPassword = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userPassword.txt";
        private static string UserTitle = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\userTitle.txt";
        private static string BorrowedBooks = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\BorrowedBooks.txt";
        public static void StartPage()
        {
            LoginOrSignUp();
        }

        public static void LoginOrSignUp()
        {
            Console.WriteLine("Skriv 'logga in' om du vill logga in  eller 'registrera' om du vill registrera dig");
            string Input = Console.ReadLine();
            while (Input != "logga in" && Input != "registrera")
            {
                Console.WriteLine("Du skrev fel.");
                Input = Console.ReadLine();
            }
            if (Input == "registrera")
            {
                SignUp();
            }
            else
            {
                Login();
            }

        }
        public static void Login()
        {
            Console.Write("Skriv in ditt personnummer: ");
            string SSNInput = Console.ReadLine(); //(Social Security Number)
            SSNInput = CheckSSN(SSNInput);
            Console.Write("Skriv in ditt lösenord: ");
            string PasswordInput = Console.ReadLine();

            if (AuthenticateLogin(SSNInput, PasswordInput) == true && CheckUserTitle(SSNInput) == "Member")
            {
                List<string> SSNList = new List<string>(File.ReadAllLines(UserSocialSecurityNumber));
                int Index = SSNList.FindIndex(a => a.Contains(SSNInput));
                Console.Clear();
                MainPage.UserMainScreen(Index);
            }
            else if(AuthenticateLogin(SSNInput, PasswordInput) == true && CheckUserTitle(SSNInput) == "Librarian")
            {
                Console.Clear();
                MainPage.LibrarianMainScreen();
            }
            else
            {
                Console.WriteLine("Inloggningsuppgifter stämde inte!");
                Console.ReadLine();
                Console.Clear();
                LoginOrSignUp();
            }
        }
        public static bool AuthenticateLogin(string SocialSecurityNumber, string Password)
        {
            string[] SSNArray = File.ReadAllLines(UserSocialSecurityNumber);
            string[] PasswordArray = File.ReadAllLines(UserPassword);
            int SSNIndex = Array.IndexOf(SSNArray, SocialSecurityNumber); //Tar indexen för förekommandet av "SocialSecurityNumber"
            if(SSNIndex > -1) //Om SocialSecurityNumber inte finns i arrayen så blir indexet -1
            {
                if (Password == PasswordArray[SSNIndex])
                {
                    return true;
                }
            }
            return false;
        }
        public static string CheckUserTitle(string SocialSecurityNumber)
        {
            string[] SSNArray = File.ReadAllLines(UserSocialSecurityNumber);
            string[] TitleArray = File.ReadAllLines(UserTitle);
            int SSNIndex = Array.IndexOf(SSNArray, SocialSecurityNumber); //Tar indexen för förekommandet av "SocialSecurityNumber"
            if (SSNIndex > -1) //Om SocialSecurityNumber inte finns i arrayen så blir indexet -1
            {
                if (TitleArray[SSNIndex] == "Member") 
                {
                    return "Member";
                }
            }
            return "Librarian";
        }
        public static void SignUp()
        {
            Console.Write("Förnamn: ");
            string FirstName = Console.ReadLine();
            Console.Write("Efternamn: ");
            string Surname = Console.ReadLine();
            Console.Write("Personnummer(ÅÅMMDDNNNC): ");
            string SocialSecurityNumber = Console.ReadLine();
            SocialSecurityNumber = CheckSSN(SocialSecurityNumber);
            if(SSNExists(SocialSecurityNumber))
            {
                Console.WriteLine("Personnummret är redan registrerat!");
                Console.ReadLine();
                Console.Clear();
                LoginOrSignUp();
                return;
            }
            Console.Write("Lösenord: ");
            string Password = Console.ReadLine();
            Console.WriteLine("Du har registrerat dig!");
            Console.ReadLine();
            Console.Clear();

            User user = new User(FirstName, Surname, SocialSecurityNumber, Password);
            File.AppendAllText(UserFirstName, user.FirstName + Environment.NewLine);
            File.AppendAllText(UserSurname, user.Surname + Environment.NewLine);
            File.AppendAllText(UserSocialSecurityNumber, user.SocialSecurityNumber + Environment.NewLine);
            File.AppendAllText(UserPassword, user.Password + Environment.NewLine);
            File.AppendAllText(UserTitle, User.Title + Environment.NewLine);
            File.AppendAllText(BorrowedBooks, User.BorrowedBooks + Environment.NewLine);

            LoginOrSignUp();
        }
        public static string CheckSSN(string SocialSecurityNumber)
        {
            while (SocialSecurityNumber.Length != 10)
            {
                Console.WriteLine("Personnummret ska vara 10 siffror långt");
                SocialSecurityNumber = Console.ReadLine();
            }
            return SocialSecurityNumber;
        }
        public static bool SSNExists(string SocialSecurityNumber)
        {
            string[] SSNArray = File.ReadAllLines(UserSocialSecurityNumber);
            
            for (int i = 0; i < SSNArray.Length; i++)
            {
                if(SocialSecurityNumber == SSNArray[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
