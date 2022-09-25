using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryNS;
using System.Collections.Generic;
using System;

namespace LibraryTest
{
    [TestClass]
    public class LibraryTests
    {
        Library library = new Library();
        [TestMethod]
        public void TestAddBookMethod()
        {
            Dictionary<string, List<Book>> expected = new Dictionary<string, List<Book>>();
            string newBookName = "Carte";
            string newBookIsbn = "978-0-306-40615-7";
            double newBookPrice = 100;
            Book book = new Book(newBookName, newBookIsbn, newBookPrice);
  
            library.AddBook(newBookName, newBookIsbn, newBookPrice);

            Assert.AreEqual(book, library.GetAllBooks()[newBookName][0]);
        }
        [TestMethod]
        public void TestAddBookMethodIvalidIsbn()
        {
            Dictionary<string, List<Book>> expected = new Dictionary<string, List<Book>>();
            string newBookName = "Carte";
            string newBookIsbn = "978-0-306-40632-7";
            double newBookPrice = 100;

            library.AddBook(newBookName, newBookIsbn, newBookPrice);

            Assert.AreEqual(0, library.GetAllBooks().Count);
        }
        [TestMethod]
        public void TestRentBookMethodNoBookInLibrary()
        {
            string bookName = "Carte";
            string personName = "Razvan";

            library.RentBook(bookName, personName, DateTime.Now.Date);

            Assert.AreEqual(0, library.GetRentedBookPerPerson().Count);
        }
        [TestMethod]
        public void TestRentBookMethodBooksInLibrary()
        {
            string newBookName1 = "Carte1";
            string newBookIsbn1 = "978-0-306-40615-7";
            double newBookPrice1 = 100;
            string newBookName2 = "Carte2";
            string newBookIsbn2 = "978-3-16-148410-0";
            double newBookPrice2 = 100;
            string personName = "Razvan";

            library.AddBook(newBookName1, newBookIsbn1, newBookPrice1);
            library.AddBook(newBookName2, newBookIsbn2, newBookPrice2);

            library.RentBook("carte3", personName, DateTime.Now.Date);
            Assert.AreEqual(0, library.GetRentedBookPerPerson().Count);
        }

        [TestMethod]
        public void TestRentBookMethodBookExistInLibrary()
        {
            string newBookName = "Carte";
            string newBookIsbn = "978-0-306-40615-7";
            double newBookPrice = 100;
            string personName = "Razvan";
            Book book = new Book(newBookName, newBookIsbn, newBookPrice);

            library.AddBook(newBookName, newBookIsbn, newBookPrice);

            library.RentBook(newBookName, personName, DateTime.Now.Date);
            Assert.AreEqual(book, library.GetRentedBookPerPerson()[personName][0]);
        }
        [TestMethod]
        public void TestReturnBookMethodNoBookRented()
        {
            string bookName = "Carte";
            string personName = "Razvan";

            library.ReturnBook(personName, bookName);

            Assert.AreEqual(0, library.GetRentedBookPerPerson().Count);
        }
        [TestMethod]
        public void TestReturnBookMethodReturnAnotherBook()
        {
            string newBookName = "Carte";
            string newBookIsbn = "978-0-306-40615-7";
            double newBookPrice = 100;
            string personName = "Razvan";
            Book book = new Book(newBookName, newBookIsbn, newBookPrice);

            library.AddBook(newBookName, newBookIsbn, newBookPrice);
            library.RentBook(newBookName, personName, DateTime.Now.Date);
            library.ReturnBook(personName, "Carte2");

            Assert.AreEqual(book, library.GetRentedBookPerPerson()[personName][0]);
        }
        [TestMethod]
        public void TestReturnBookMethodBookRented()
        {
            string newBookName = "Carte";
            string newBookIsbn = "978-0-306-40615-7";
            double newBookPrice = 100;
            string personName = "Razvan";

            library.AddBook(newBookName, newBookIsbn, newBookPrice);
            library.RentBook(newBookName, personName, DateTime.Now.Date);
            library.ReturnBook(personName, newBookName);

            Assert.AreEqual(0, library.GetRentedBookPerPerson()[personName].Count);
        }
        [TestMethod]
        public void TestPaymentAmountMethod()
        {
            string newBookName = "Carte";
            string newBookIsbn = "978-0-306-40615-7";
            double newBookPrice = 100;
            Book book = new Book(newBookName, newBookIsbn, newBookPrice);
            book.SetDate(new DateTime(2022, 9, 10));

            Assert.AreEqual(10, book.PaymentAmount(new DateTime(2022, 9, 25)));
        }
    }
}
