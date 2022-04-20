using BankAdministration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BankAdministration.Services.StatisticsService.UserStatistics
{
    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly BankContext _context;

        public UserStatisticsService(BankContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
