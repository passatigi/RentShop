using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<AppUser> Users { get; }
        Task<AppUser> GetLoggedInUserAsync(ClaimsPrincipal User);
        Task<AppUser> FindUserAsync(string email);
        void UpdateUser(AppUser user);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword,
            string newPassword);
        Task<List<Address>> GetAddressesAsync(int id);
        Task<bool> AddressAlreadyExists(Address address);
        Task<IdentityResult> AddAddressAsync(Address address);
        Task<Address> FindAddressAsync(int id);
        Task<IdentityResult> DeleteAddressAsync(Address address);
        Task<bool> CheckPasswordSignInAsync(AppUser user, string password);
        Task<bool> UserExists(string email);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
    }
}