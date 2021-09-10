using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IQueryable<AppUser> Users => _userManager.Users;

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<bool> CheckPasswordSignInAsync(AppUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded) return false;
            return true;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password){
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword,
            string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }



        public async Task<bool> UpdateUserAsync(AppUser user){
           _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<IdentityResult> AddAddressAsync(Address address){
            var result = await _context.Addresses.AddAsync(address);
            if ((await _context.SaveChangesAsync())>0)
                return IdentityResult.Success;
            
            return IdentityResult.Failed();
            
        }

        public async Task<IdentityResult> DeleteAddressAsync(Address address){
            var result = _context.Addresses.Remove(address);
            if ((await _context.SaveChangesAsync())>0)
                return IdentityResult.Success;
            
            return IdentityResult.Failed();
        }

        public async Task<bool> AddressAlreadyExists(Address address){
            if ((await _context.Addresses
                .Where(a => a.AppUserId == address.AppUserId)
                .Where(a => a.Country == address.Country)
                .Where(a => a.City == address.City)
                .Where(a => a.HouseAddress == address.HouseAddress)
                .Where(a => a.PostalCode == address.PostalCode)
                .FirstOrDefaultAsync())
                != null){
                return true;
            }
            return false;
        }
    }
}