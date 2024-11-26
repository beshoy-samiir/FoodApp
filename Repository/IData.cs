using FoodApp_project.Models;
using System.Security.Claims;

namespace FoodApp_project.Repository
{
    public interface IData
    {
        Task<ApplicationUser> GetUser(ClaimsPrincipal claims);
    }
}
