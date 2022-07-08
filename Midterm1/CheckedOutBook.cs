using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm1
{
    internal class CheckedOutBook : Book
    {
        //properties
        public DateTime CheckOutDate { get; set; }
        //constructor
        public CheckedOutBook(string _title, string _author, string _status, DateTime _checkoutdate) : base(_title, _author, _status, _checkoutdate)
        {
            CheckOutDate = _checkoutdate;
        }
        //methods
        //ReturnDate takes in a DateTime x and adds 14 days.  This then returns the due date in a string
        public static DateTime ReturnDate(DateTime x)
        {
            x.AddDays(14);
            return x;
        }
        //string to datetime converter
        public static DateTime DateConverter(string x)
        {
            DateTime checkOutDate = DateTime.Parse(x);
            return checkOutDate;
        }

    }
}
