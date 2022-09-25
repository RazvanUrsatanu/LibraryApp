using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryNS
{
    public class Book
    {
        private string BookName;
        private string Isbn;
        private double Price;
        private bool Rented;
        private DateTime Date;
        public Book(string bookName, string isbn, double price)
        {
            BookName = bookName;
            Isbn = isbn;
            Price = price;
            Rented = false;
        }

        public string GetBookName() { return BookName; }
        public double GetPrice() { return Price; }
        public bool IsRented() { return Rented; }
        public DateTime GetDate() { return Date; }
        public void SetDate(DateTime date) { Date = date; }
        public void SetIsRented(bool rented) { Rented = rented; }
        public double PaymentAmount(DateTime dateTime)
        {
            return ((dateTime - GetDate()).TotalDays - 14) * 0.1 * GetPrice();
        }

        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   BookName == book.BookName &&
                   Isbn == book.Isbn &&
                   Price == book.Price;
        }
    }
}
