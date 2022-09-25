using System;
using System.Collections.Generic;

namespace LibraryNS
{
    public class Library
    {
        private Dictionary<string, List<Book>> AllBooks;
        private Dictionary<string, List<Book>> RentedBookPerPerson;
        public Library()
        {
            AllBooks = new Dictionary<string, List<Book>>();
            RentedBookPerPerson = new Dictionary<string, List<Book>>();
        }
        public Dictionary<string, List<Book>> GetAllBooks()
        {
            return AllBooks;
        }
        public Dictionary<string, List<Book>> GetRentedBookPerPerson()
        {
            return RentedBookPerPerson;
        }
        public void Initialize()
        {
            Console.WriteLine("Buna ziua!");
            Console.WriteLine("Cu ce va putem ajua?");
        }
        public void PrintMenu()
        {
            Console.WriteLine("Apasati tasta 1, daca doriti sa adaugati o noua carte in biblioteca!");
            Console.WriteLine("Apasati tasta 2, daca doriti sa viziualizati cartile disponibile in biblioteca!");
            Console.WriteLine("Apasati tasta 3, daca doriti sa aflati numarul de exemplare disponibile pentru o anumita carte!");
            Console.WriteLine("Apasati tasta 4, daca doriti sa imprumutati o carte!");
            Console.WriteLine("Apasati tasta 5, daca doriti sa restituiti o carte!");
            Console.WriteLine("Apasati tasta 0, daca doriti sa parasiti biblioteca!");
        }
        public void InitializeNewBook(Library library)
        {
            Console.Write("Introduceti numele cartii: ");
            string newBookName = Console.ReadLine();
            Console.Write("Introucdeti ISBN-ul cartii: ");
            string newBookIsbn = Console.ReadLine();
            Console.Write("Introduceti pretul cartii: ");
            string newBookPrice = Console.ReadLine();
            try
            {
                double bookPrice = double.Parse(newBookPrice);
                library.AddBook(newBookName, newBookIsbn, bookPrice);
            }
            catch
            {
                Console.WriteLine("Pret invalid!");
            }
        }
        static bool isValidIsbn(string isbn)
        {
            int sum = 0, numberOfDigits = 0;
            for(int i = 0; i < isbn.Length; i++)
            {
                if (isbn[i] == '-')
                {
                    continue;
                }
                int digit = isbn[i] - '0';
                if(numberOfDigits % 2 == 0)
                {
                    sum += digit;
                }
                else
                {
                    sum += digit * 3;
                }
                numberOfDigits++;
            }
            if(sum % 10 == 0 && numberOfDigits == 13)
            {
                return true;
            }
            return false;
        }
        public void AddBook(string newBookName, string newBookIsbn, double newBookPrice)
        {
            Book book = new Book(newBookName, newBookIsbn, newBookPrice);
            if(!isValidIsbn(newBookIsbn))
            {
                Console.WriteLine("Isbn invalid!");
                return;
            }
            if (AllBooks.ContainsKey(newBookName))
            {
                AllBooks[newBookName].Add(book);
            }
            else
            {
                AllBooks.Add(newBookName, new List<Book>());
                AllBooks[newBookName].Add(book);
            }
            Console.WriteLine("Cartea a fost adaugata cu succes!");
        }
        public void DisplayLibrary()
        {
            Console.WriteLine("Lista cartilor disponibile:");
            foreach (string book in AllBooks.Keys)
            {
                Console.WriteLine(book);
            }
        }
        public void ChooseBookByName(Library library)
        {
            Console.WriteLine("Care este numele cartii pentru care doriti numarul de exemplare existente?");
            string bookName = Console.ReadLine();
            DisplayNumberByBookName(bookName);
        }
        public void DisplayNumberByBookName(string bookName)
        {
            int availableBooks = 0;
            foreach (Book book in AllBooks[bookName])
            {
                if(!book.IsRented())
                {
                    availableBooks++;
                }
            }
            Console.WriteLine("{0} exemplare, din care {1} in stoc!", AllBooks[bookName].Count, availableBooks);
        }
        public void ChooseBook(Library library)
        {
            Console.WriteLine("Care este numele cartii pe care doriti sa o imprumutati?");
            string rentBookName = Console.ReadLine();
            Console.WriteLine("Care este numele dumneavoastra?");
            string personName = Console.ReadLine();
            library.RentBook(rentBookName, personName, DateTime.Now.Date);
        }
        public void RentBook(string rentBookName, string personName, DateTime date)
        {
            if(!AllBooks.ContainsKey(rentBookName) || AllBooks[rentBookName].Count == 0)
            {
                Console.WriteLine("Cartea nu este in stoc!");
                return;
            }
            foreach(Book book in AllBooks[rentBookName])
            {
                if(!book.IsRented())
                {
                    book.SetDate(date);
                    book.SetIsRented(true);
                    if (!RentedBookPerPerson.ContainsKey(personName))
                    {
                        RentedBookPerPerson.Add(personName, new List<Book>());
                        RentedBookPerPerson[personName].Add(book);
                    }
                    else
                    {
                        RentedBookPerPerson[personName].Add(book);
                    }           
                    Console.WriteLine("Cartea a fost imprumutata cu succes!");
                    return;
                }
            }
            Console.WriteLine("Cartea nu este in stoc!");
        }
        public void InfoReturnBook(Library library)
        {
            Console.WriteLine("Care este numele pe care a fost cartea imprumutata?");
            string personName = Console.ReadLine();
            Console.WriteLine("Care este cartea pe care doriti sa o restituiti?");
            string bookName = Console.ReadLine();
            ReturnBook(personName, bookName);
        }
        public void ReturnBook(string personName, string bookName)
        {
            if (!RentedBookPerPerson.ContainsKey(personName))
            {
                Console.WriteLine("Nu figurati in baza de date cu acest nume!");
                return;
            }
            foreach (Book book in RentedBookPerPerson[personName])
            {
                if (book.GetBookName().Equals(bookName))
                {
                    foreach(Book bookLibrary in AllBooks[bookName])
                    {
                        if(bookLibrary.Equals(book))
                        {
                            book.SetIsRented(false);
                            RentedBookPerPerson[personName].Remove(book);
                            if ((DateTime.Now.Date - book.GetDate()).TotalDays > 14)
                            {
                                Console.WriteLine("Va trebui sa mai platiti {0} lei!", book.PaymentAmount(DateTime.Now.Date));
                                return;
                            }
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("Nu figurati in baza de date cu aceasta carte!");
        }
    }
}
