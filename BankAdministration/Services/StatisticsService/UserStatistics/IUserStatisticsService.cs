using BankAdministration.Models;
using Microsoft.AspNetCore.Identity;

namespace BankAdministration.Services.StatisticsService.UserStatistics
{
    public interface IUserStatisticsService
    {
        public List<User> GetUsers();
    }
}
