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
        User GetUser(string username);
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
        ReadingLog GetReadingLog(ReadingLog log);
        ReadingLog CreateReadingLog(ReadingLog log);

        //Prizes
        List<Prize> GetPrizes(int familyID);
        Prize AddPrize(Prize prize);
        List<PrizeProgress> GetPrizesByUser(User user);


    }
}
