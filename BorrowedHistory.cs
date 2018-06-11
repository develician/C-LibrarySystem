using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    class BorrowedHistory
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime BorrowedAt { get; set; }
        public string BookName { get; set; }
        public string BookIsbn { get; set; }

    }
}
