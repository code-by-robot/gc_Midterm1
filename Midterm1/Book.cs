using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm1
{
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public DateTime? CheckOutDate {get; set;}

        //Constructor
        public Book(string _title, string _author, string _status)
        {
            Title = _title;
            Author = _author;
            Status = _status;
            CheckOutDate = null;
        }
        public Book(string _title, string _author, string _status, DateTime _checkoutdate)
        {
            Title = _title;
            Author = _author;
            Status = _status;
            CheckOutDate = _checkoutdate;
        }
        //methods

    }
}
