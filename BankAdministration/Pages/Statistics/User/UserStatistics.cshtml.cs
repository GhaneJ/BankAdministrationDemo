namespace BankAdministration.Pages.Statistics.User;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class UserStatisticsModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserStatisticsModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public class Item
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public List<IdentityUser> AllUsers { get; set; }
    public List<IdentityRole> Roles { get; set; }
    public List<object> UsersAndRoles { get; set; }

    public List<IdentityRole> GetRoles()
    {
        Roles = new List<IdentityRole>();
        return Roles;
    }
    public List<IdentityUser> GetUsers()
    {
        AllUsers = new List<IdentityUser>();
        return AllUsers;
    }
    public void OnGet()
    {
        AllUsers = _userManager.Users.Select(x => new IdentityUser
        {
            UserName = x.UserName,
            Email = x.Email
        }).ToList();

        Roles = _roleManager.Roles.Select(x => new IdentityRole
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}
