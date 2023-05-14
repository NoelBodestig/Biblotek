using Bibliotek_Uppgift;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ManageBooks


{
    public class ManageBooks
    {
        private static string BookTitle = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\BookTitle.txt";
        private static string BookISBN = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\BookISBN.txt";
        private static string BookGenre = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\BookGenre.txt";
        private static string BookAuthor = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\BookAuthor.txt";
        private static string BookAccesible = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\BookAccesible.txt";
        private static string BorrowedBooks = @"C:\Users\noel.bodestig\Desktop\Bibliotek Uppgift\Bibliotek Uppgift\BorrowedBooks.txt";

        public class Book
        {
            public string BookTitle;
            public string BookISBN;
            public string BookGenre;
            public string BookAuthor;
            public static string BookAccesible = "true";
            public Book(string BookTitle, string BookISBN, string BookGenre, string BookAuthor)
            {
                this.BookTitle = BookTitle;
                this.BookISBN = BookISBN;
                this.BookGenre = BookGenre;
                this.BookAuthor = BookAuthor;
            }
        }
        public static void AddBook()
        {
            Console.WriteLine("Ange uppgifter på boken!");
            Console.Write("Titel: ");
            string Title = Console.ReadLine();
            Console.Write("ISBN: ");
            string ISBN = Console.ReadLine();
            Console.Write("Genre: ");
            string Genre = Console.ReadLine();
            Console.Write("Författare: ");
            string Author = Console.ReadLine();
            Console.WriteLine("Du har lagt till boken!");
            Console.WriteLine();
            Console.Clear();

            Book book = new Book(Title, ISBN, Genre, Author);
            File.AppendAllText(BookTitle, book.BookTitle + Environment.NewLine);
            File.AppendAllText(BookISBN, book.BookISBN + Environment.NewLine);
            File.AppendAllText(BookGenre, book.BookGenre + Environment.NewLine);
            File.AppendAllText(BookAuthor, book.BookAuthor + Environment.NewLine);
            File.AppendAllText(BookAccesible, Book.BookAccesible + Environment.NewLine);
            MainPage.LibrarianMainScreen();
        }
        public static void DeleteBook()
        {
            List<string> TitleList = new List<string>(File.ReadAllLines(BookTitle));
            List<string> ISBNList = new List<string>(File.ReadAllLines(BookISBN));
            List<string> GenreList = new List<string>(File.ReadAllLines(BookGenre));
            List<string> AuthorList = new List<string>(File.ReadAllLines(BookAuthor));
            List<string> AccesibleList = new List<string>(File.ReadAllLines(BookAccesible));

            Console.WriteLine("Skriv ISBN på bok du vill ta bort!");
            string ISBN = Console.ReadLine();
            if(ISBNExists(ISBN) == false)
            {
                Console.WriteLine("Boken finns inte registrerad!");
                Console.ReadLine();
                Console.Clear();
                MainPage.LibrarianMainScreen();
                return;
            }
            int index = ISBNList.FindIndex(a => a.Contains(ISBN));

            TitleList.RemoveAt(index);
            ISBNList.RemoveAt(index);
            GenreList.RemoveAt(index);
            AuthorList.RemoveAt(index);
            AccesibleList.RemoveAt(index);
            Console.WriteLine("Bok raderad!");
            Console.ReadLine();
            Console.Clear();
            
            File.WriteAllLines(BookTitle, TitleList);
            File.WriteAllLines(BookISBN, ISBNList);
            File.WriteAllLines(BookGenre, GenreList);
            File.WriteAllLines(BookAuthor, AuthorList);
            File.WriteAllLines(BookAccesible, AccesibleList);
            MainPage.LibrarianMainScreen();
        }
        public static bool ISBNExists(string ISBN)
        {
            string[] ISBNArray = File.ReadAllLines(BookISBN);

            for (int i = 0; i < ISBNArray.Length; i++)
            {
                if (ISBN == ISBNArray[i])
                {
                    return true;
                }
            }
            return false;
        }
        public static void EditBook()
        {
            List<string> ISBNList = new List<string>(File.ReadAllLines(BookISBN));

            Console.Write("Skriv in ISBN på bok du vill redigera: ");
            string ISBN = Console.ReadLine();
            if (ISBNExists(ISBN) == false)
            {
                Console.WriteLine("Boken finns inte registrerad!");
                Console.ReadLine();
                Console.Clear();
                MainPage.LibrarianMainScreen();
                return;
            }
            int Index = ISBNList.FindIndex(a => a.Contains(ISBN));

            Console.WriteLine("Vill du redigera titel(1), ISBN(2), genre(3) eller författare(4)");
            int Input = Int32.Parse(Console.ReadLine());
            while (Input < 1 || Input > 4)
            {
                Console.WriteLine("Ej giltig input");
                Input = Int32.Parse(Console.ReadLine());
            }
            if(Input == 1)
            {
                EditTitle(Index);
            }
            else if(Input == 2) 
            { 
                EditISBN(Index);
            }
            else if( Input == 3)
            {
                EditGenre(Index);
            }
            else
            {
                EditAuthor(Index);
            }
            Console.WriteLine("Bok uppdaterad!");
            Console.ReadLine();
            Console.Clear();
            MainPage.LibrarianMainScreen();
        }
        public static void EditTitle(int Index)
        {
            List<string> TitleList = new List<string>(File.ReadAllLines(BookTitle));
            Console.Write("Skriv in ny titel: ");
            string NewTitle = Console.ReadLine();
            TitleList[Index] = NewTitle;
            File.WriteAllLines(BookTitle, TitleList);
        }
        public static void EditISBN(int Index)
        {
            List<string> ISBNList = new List<string>(File.ReadAllLines(BookISBN));
            Console.Write("Skriv in nytt ISBN: ");
            string NewISBN = Console.ReadLine();
            ISBNList[Index] = NewISBN;
            File.WriteAllLines(BookISBN, ISBNList);
        }
        public static void EditGenre(int Index) 
        {
            List<string> GenreList = new List<string>(File.ReadAllLines(BookGenre));
            Console.Write("Skriv in ny genre: ");
            string NewGenre = Console.ReadLine();
            GenreList[Index] = NewGenre;
            File.WriteAllLines(BookGenre, GenreList);
        }
        public static void EditAuthor(int Index)
        {
            List<string> AuthorList = new List<string>(File.ReadAllLines(BookAuthor));
            Console.Write("Skriv in ny författare: ");
            string NewAuthor = Console.ReadLine();
            AuthorList[Index] = NewAuthor;
            File.WriteAllLines(BookAuthor, AuthorList);
        }
        public static void SearchBooks()
        {
            string[] Title = File.ReadAllLines(BookTitle);
            string[] ISBN = File.ReadAllLines(BookISBN);
            string[] Genre = File.ReadAllLines(BookGenre);
            string[] Author = File.ReadAllLines(BookAuthor);
            string[] Accesible = File.ReadAllLines(BookAccesible);
            Console.WriteLine("Skriv in bokens titel, ISBN, genre eller författare");
            Console.WriteLine("");
            string Input = Console.ReadLine();

            for (int i = 0; i < Title.Length; i++)
            {
                if (Title[i] == Input || ISBN[i] == Input || Genre[i] == Input || Author[i] == Input)
                {
                    Console.Write("Titel: ");
                    Console.WriteLine(Title[i]);
                    Console.Write("ISBN: ");
                    Console.WriteLine(ISBN[i]);
                    Console.Write("Genre: ");
                    Console.WriteLine(Genre[i]);
                    Console.Write("Författare: ");
                    Console.WriteLine(Author[i]);
                    Console.Write("Ledig: ");
                    Console.WriteLine(Accesible[i]);
                    Console.WriteLine("");
                }
            }
        }
        public static void BorrowBook(int UserIndex)
        {
            List<string> ISBNList = new List<string>(File.ReadAllLines(BookISBN));
            List<string> AccesibleList = new List<string>(File.ReadAllLines(BookAccesible));
            List<string> BorrowedBooksList = new List<string>(File.ReadAllLines(BorrowedBooks));
         
            Console.WriteLine("Skriv in ISBN på bok du vill låna");
            string ISBNInput = Console.ReadLine();
            int ISBNIndex = ISBNList.FindIndex(a => a.Contains(ISBNInput));
            for (int i = 0; i < ISBNList.Count; i++)
            {
                if(ISBNInput == ISBNList[i] && AccesibleList[i] == "true")
                {
                    BorrowedBooksList[UserIndex] = BorrowedBooksList[UserIndex] + " " + ISBNInput;
                    AccesibleList[ISBNIndex] = "false";
                    Console.WriteLine("Du har lånat boken!");
                    break;
                }
                else if(ISBNInput == ISBNList[i] && AccesibleList[i] == "false")
                {
                    Console.WriteLine("Boken är redan lånad!");
                }
            }
            File.WriteAllLines(BorrowedBooks, BorrowedBooksList);
            File.WriteAllLines(BookAccesible, AccesibleList);
            Console.ReadLine();
            Console.Clear();
            MainPage.UserMainScreen(UserIndex);
        }
        public static void ReturnBook(int UserIndex)
        {
            List<string> ISBNList = new List<string>(File.ReadAllLines(BookISBN));
            List<string> AccesibleList = new List<string>(File.ReadAllLines(BookAccesible));
            List<string> BorrowedBooksList = new List<string>(File.ReadAllLines(BorrowedBooks));
            Console.WriteLine("Skriv in ISBN på bok du vill returnera");
            string ISBNInput = Console.ReadLine();
            int ISBNIndex = ISBNList.FindIndex(a => a.Contains(ISBNInput));
            if (BorrowedBooksList[UserIndex].Contains(ISBNInput))
            {
                BorrowedBooksList.RemoveAt(UserIndex);
                AccesibleList[ISBNIndex] = "true";
            }
        }
        
    }
}
