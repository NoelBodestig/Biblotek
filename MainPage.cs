using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotek_Uppgift
{
    public class MainPage
    {
        public static void UserMainScreen(int UserIndex)
        {
            Console.WriteLine("Startsida");
            Console.WriteLine("Vill du söka på en bok(1), låna en bok(2), returnera en bok(3), ändra lösenord(4), eller logga ut(5)");
            int Input = Int32.Parse(Console.ReadLine());
            while(Input < 1 || Input > 5)
            {
                Console.WriteLine("Ej giltig input");
                Input = Int32.Parse(Console.ReadLine());
            }
            if (Input == 1)
            {
                ManageBooks.SearchBooks();
            }
            else if (Input == 2)
            {
                ManageBooks.BorrowBook(UserIndex);
            }
            else if(Input == 3)
            {
                ManageBooks.ReturnBook(UserIndex);
            }
            else if(Input == 4)
            {
                ManageUser.ChangePassword(UserIndex);
            }
            else
            {
                LogOut();
            }
        }
        public static void LibrarianMainScreen()
        {
            Console.WriteLine("Lägg till användare(1), redigera en användare(2), ta bort en användare(3), lista alla medlemmar(4), lägg till en bok(5), radera en bok(6), redigera en bok(7), logga ut(8)");
            int Input = Int32.Parse(Console.ReadLine());
            while (Input < 1 || Input > 8)
            {
                Console.WriteLine("Ej giltig input");
                Input = Int32.Parse(Console.ReadLine());
            }
            if(Input == 1)
            {
                ManageUser.AddUser();
            }
            else if(Input == 2)
            {
                ManageUser.EditUser();
            }
            else if(Input == 3)
            {
                ManageUser.RemoveUser();
            }
            else if(Input == 4)
            {
                ManageUser.ListUsers();
            }
            else if(Input == 5)
            {
                ManageBooks.AddBook();
            }
            else if(Input == 6)
            {
                ManageBooks.DeleteBook();
            }
            else if(Input == 7)
            {
                ManageBooks.EditBook();
            }
            else
            {
                LogOut();
            }
        }
        public static void LogOut()
        {
            Console.Clear();
            Console.WriteLine("Du har loggat ut!");
            Console.ReadLine();
            Console.Clear();
            LoginPage.StartPage();
        }
        
    }
}
