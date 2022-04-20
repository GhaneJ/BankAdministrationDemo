using BankAdministration.Services.StatisticsService.UserStatistics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages.Statistics.User
{
    public class UserStatisticsModel : PageModel
    {
        private readonly IUserStatisticsService _userStatisticsService;
        public List<UserStatisticsViewModel> UserStatistics { get; set; }

        public class UserStatisticsViewModel
        {
            public string Id { get; set; }
            public string LoginName { get; set; }
        }

        public UserStatisticsModel(IUserStatisticsService userStatisticsService)
        {
            _userStatisticsService = userStatisticsService;
        }

        public void OnGet()
        {
            UserStatistics = _userStatisticsService.GetUsers().Select(r => new UserStatisticsViewModel
            {
                LoginName = r.LoginName.ToString()
            }).ToList();
        }
    }
}
