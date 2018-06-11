using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace LibrarySystem
{
    class DataManager
    {
        public static List<Book> Books = new List<Book>();
        public static List<User> Users = new List<User>();
        public static List<BorrowedHistory> borrowedHistories = new List<BorrowedHistory>();
        public static List<ReturnedHistory> returnedHistories = new List<ReturnedHistory>();

        static DataManager()
        {
            //Testing();
            Load();
        }

        public static void Testing()
        {
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "hello.txt", "Hello World");
        }
 

        public static void Load()
        {
            try
            {
                string booksOutput = File.ReadAllText(@"./Books.xml");
                XElement booksXElement = XElement.Parse(booksOutput);
                Books = (from item in booksXElement.Descendants("book")
                         select new Book()
                         {
                             Isbn = item.Element("isbn").Value,
                             Name = item.Element("name").Value,
                             Publisher = item.Element("publisher").Value,
                             Page = int.Parse(item.Element("page").Value),
                             BorrowedAt = DateTime.Parse(item.Element("borrowedAt").Value),
                             isBorrowed = item.Element("isBorrowed").Value != "0" ? true : false,
                             UserId = int.Parse(item.Element("userId").Value),
                             UserName = item.Element("userName").Value

                         }).ToList<Book>();
                string usersOutput = File.ReadAllText(@"./Users.xml");
                XElement usersXElement = XElement.Parse(usersOutput);
                Users = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             Id = int.Parse(item.Element("id").Value),
                             Name = item.Element("name").Value,
                             Phone= item.Element("phone").Value,
                             borrowedNumber = int.Parse(item.Element("borrowedNumber").Value)
                         }).ToList<User>();

                string borrowedHistoryOutput = File.ReadAllText(@"./BorrowedHistories.xml");
                XElement borrowedHistoryElement = XElement.Parse(borrowedHistoryOutput);
                borrowedHistories = (from item in borrowedHistoryElement.Descendants("borrowedHistory")
                                     select new BorrowedHistory()
                                     {
                                         UserName = item.Element("userName").Value,
                                         BorrowedAt = DateTime.Parse(item.Element("borrowedAt").Value),
                                         BookName = item.Element("bookName").Value,
                                         BookIsbn = item.Element("bookIsbn").Value,
                                         UserId = int.Parse(item.Element("userId").Value)
                                     }).ToList<BorrowedHistory>();

                string returnedHistoryOutput = File.ReadAllText(@"./ReturnedHistories.xml");
                XElement returnedHistoryElement = XElement.Parse(returnedHistoryOutput);
                returnedHistories = (from item in returnedHistoryElement.Descendants("returnedHistory")
                                     select new ReturnedHistory()
                                     {
                                         UserName = item.Element("userName").Value,
                                         ReturnedAt = DateTime.Parse(item.Element("returnedAt").Value),
                                         BookName = item.Element("bookName").Value,
                                         BookIsbn = item.Element("bookIsbn").Value,
                                         UserId = int.Parse(item.Element("userId").Value)
                                     }).ToList<ReturnedHistory>();

            }
            catch (FileNotFoundException exception)
            {

                Save();
            }
        }

        public static void Save()
        {
            string booksOutput = "";
            booksOutput += "<books>\n";
            foreach (var item in Books)
            {
                booksOutput += "<book>\n";
                booksOutput += "    <isbn>" + item.Isbn + "</isbn>\n";
                booksOutput += "    <name>" + item.Name + "</name>\n";
                booksOutput += "    <publisher>" + item.Publisher + "</publisher>\n";
                booksOutput += "    <page>" + item.Page + "</page>\n";
                booksOutput += "    <borrowedAt>" + item.BorrowedAt.ToLongDateString() + "</borrowedAt>\n";
                booksOutput += "    <isBorrowed>" + (item.isBorrowed ? 1 : 0) + "</isBorrowed>\n";
                booksOutput += "    <userId>" + item.UserId + "</userId>\n";
                booksOutput += "    <userName>" + item.UserName + "</userName>\n";
                booksOutput += "</book>\n";

            }
            booksOutput += "</books>";

            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                usersOutput += "<user>\n";
                usersOutput += "    <id>" + item.Id + "</id>\n";
                usersOutput += "    <name>" + item.Name + "</name>\n";
                usersOutput += "    <phone>" + item.Phone + "</phone>\n";
                usersOutput += "    <borrowedNumber>" + item.borrowedNumber + "</borrowedNumber>\n";
                usersOutput += "</user>\n";
            }
            usersOutput += "</users>";

            string borrowedHistoryOutput = "";
            borrowedHistoryOutput += "<borrowedHistories>\n";
            foreach (var item in borrowedHistories)
            {
                borrowedHistoryOutput += "<borrowedHistory>\n";
                borrowedHistoryOutput += "  <userId>" + item.UserId + "</userId>\n";
                borrowedHistoryOutput += "  <userName>" + item.UserName + "</userName>\n";
                borrowedHistoryOutput += "  <borrowedAt>" + item.BorrowedAt + "</borrowedAt>\n";
                borrowedHistoryOutput += "  <bookName>" + item.BookName + "</bookName>\n";
                borrowedHistoryOutput += "  <bookIsbn>" + item.BookIsbn + "</bookIsbn>\n";
                borrowedHistoryOutput += "</borrowedHistory>\n";
            }
            borrowedHistoryOutput += "</borrowedHistories>";

            string returnedHistoryOutput = "";
            returnedHistoryOutput += "<returnedHistories>\n";
            foreach (var item in returnedHistories)
            {
                returnedHistoryOutput += "<returnedHistory>\n";
                returnedHistoryOutput += "  <userId>" + item.UserId + "</userId>\n";
                returnedHistoryOutput += "  <userName>" + item.UserName + "</userName>\n";
                returnedHistoryOutput += "  <returnedAt>" + item.ReturnedAt + "</returnedAt>\n";
                returnedHistoryOutput += "  <bookName>" + item.BookName + "</bookName>\n";
                returnedHistoryOutput += "  <bookIsbn>" + item.BookIsbn + "</bookIsbn>\n";
                returnedHistoryOutput += "</returnedHistory>\n";
            }
            returnedHistoryOutput += "</returnedHistories>";

            File.WriteAllText(@"./Books.xml", booksOutput);
            File.WriteAllText(@"./Users.xml", usersOutput);
            File.WriteAllText(@"./BorrowedHistories.xml", borrowedHistoryOutput);
            File.WriteAllText(@"./ReturnedHistories.xml", returnedHistoryOutput);
        }
    }
}
