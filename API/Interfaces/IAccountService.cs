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

        Task<IdentityResult> AddToRoleAsync(AppUser user, string role); 

        Task<bool> CheckPasswordSignInAsync(AppUser user, string password);

        
    }
}