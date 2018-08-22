using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web
{
   public interface IDatabaseSvc
    {
        //Role

        //User
        List<User> GetAllUsers();
        User GetUserByUsername(string username);
        User GetUserByID(int id);
        User CreateUser(User newUser);

        //Family
        //User GetUserFromFamilyID(int familyID);
        string GetFamilyFromFamilyID(int familyID);
        int CreateFamily(Family newFamily);
        User CreateFamilyMember(User newUser);
        List<User> GetAllUsersFromFamilyID(int familyID);

        //Book
        Book GetMostCurrentBook(int userID);
        Book CreateBook(Book book);
        List<Book> GetActiveBooks(int userID);
        List<Book> GetInactiveBooks(int userID);
        List<Book> GetAllBooksByFamilyID(int familyID);

        //Reading Log
        Stack<ReadingLog> GetReadingLog(int userID);
        ReadingLog CreateReadingLog(ReadingLog log);

        //Prizes
        List<Prize> GetPrizes(int familyID);
        Prize AddPrize(Prize prize);
        List<PrizeProgress> GetPrizesByUser(User user);
        bool EditPrize(Prize prize);
        Prize GetPrizeById(int id);
    }
}
