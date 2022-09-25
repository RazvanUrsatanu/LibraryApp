using System;

namespace LibraryNS
{
    public class Program
    {
        static void Main(string[] args)
        {
            Library libraryInstance = new Library();
            libraryInstance.Initialize();

            while (true)
            {
                libraryInstance.PrintMenu();
                switch (Console.ReadLine())
                {
                    case "1":
                        libraryInstance.InitializeNewBook(libraryInstance);
                        break;
                    case "2":
                        libraryInstance.DisplayLibrary();
                        break;
                    case "3":
                        libraryInstance.ChooseBookByName(libraryInstance);
                        break;
                    case "4":
                        libraryInstance.ChooseBook(libraryInstance);
                        break;
                    case "5":
                        libraryInstance.InfoReturnBook(libraryInstance);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Alegere invalida!");
                        break;
                }
            }
        }
    }
}
