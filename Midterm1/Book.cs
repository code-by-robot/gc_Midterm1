﻿using System;
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
        public DateTime CheckOutDate = DateTime.Now;

        //Constructor
        public Book(string _title, string _author, string _status, DateTime _checkoutdate)
        {
            Title = _title;
            Author = _author;
            Status = _status;
            CheckOutDate = _checkoutdate;
        }
        //methods
        //ReturnDate takes in a DateTime x and adds 14 days.  This then returns the due date in a string
        public static DateTime ReturnDate(DateTime x)
        {
            double test = 14;
            x.AddDays(test);
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
