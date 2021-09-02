using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Interfaces
{
    public interface IAccountService
    {
        IQueryable<AppUser> Users {  get; }

        Task<IdentityResult> CreateAsync(AppUser user, string password);

        Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword,
            string password);

        Task<IdentityResult> AddToRoleAsync(AppUser user, string role); 

        Task<bool> CheckPasswordSignInAsync(AppUser user, string password);

        Task<bool> CheckPasswordAsync(AppUser user, string password);

        Task<bool> UpdateUserAsync(AppUser user);

        Task<IdentityResult> AddAddressAsync(Address address);

        Task<IdentityResult> DeleteAddressAsync(Address address);

        Task<bool> AddressAlreadyExists(Address address);
    }
}