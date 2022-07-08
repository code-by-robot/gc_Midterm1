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
        public DateTime? ReturnDate {get; set;}

        //Constructor
        public Book(string _title, string _author, string _status)
        {
            Title = _title;
            Author = _author;
            Status = _status;
            ReturnDate = null;
        }
        public Book(string _title, string _author, string _status, DateTime _returndate)
        {
            Title = _title;
            Author = _author;
            Status = _status;
            ReturnDate = _returndate;
        }
        //methods

    }
}
