using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class UserActivityViewModel
    {
        private IDatabaseSvc _db = null;
        public UserActivityViewModel()
        {
        }
        public UserActivityViewModel(IDatabaseSvc db)
        {
            _db = db;
        }
        public int ReadingLogID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int FamilyID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Format { get; set; }
        public string ISBN { get; set; }
        public int MinutesRead { get; set; }
        public int HoursRead { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public bool BookActive { get; set; }
        public List<User> UserList { get; set; }
        public HashSet<Book> ActiveBooks { get; set; }
        public HashSet<Book> InactiveBooks { get; set; }
        public List<PrizeProgress> PrizeList { get; set; }
        public Stack<ReadingLog> ReadingLogs { get; set; }

        public int MinutesReadByUser(int userId)
        {
            int minutes = _db.GetTotalMinutesReadByUser(userId);
            return minutes;
        }
        public Book MostCurrentBook(int userId)
        {
            Book book = _db.GetMostCurrentBook(userId);
            return book;
        }
    }
}