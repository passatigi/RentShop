using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IQueryable<AppUser> Users => _userManager.Users;

        public UserRepository(DataContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<AppUser> GetLoggedInUserAsync(ClaimsPrincipal User)
        {
            var email = User.GetEmail();

            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email == email);

            return user;
        }


        public async Task<AppUser> FindUserAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }


        public void UpdateUser(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }


        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }


        public async Task<IdentityResult> ChangePasswordAsync(AppUser user, 
            string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<List<Address>> GetAddressesAsync(int id){
            var addresses = await _context.Addresses
                            .Where(i => i.AppUserId == id)
                            .ToListAsync();
            return addresses;
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

        public async Task<IdentityResult> AddAddressAsync(Address address){
            var result = await _context.Addresses.AddAsync(address);
            if ((await _context.SaveChangesAsync())>0)
                return IdentityResult.Success;
            
            return IdentityResult.Failed();
        }

        public async Task<Address> FindAddressAsync(int addressId){
            return await _context.Addresses.FindAsync(addressId);
        }

        public async Task<IdentityResult> DeleteAddressAsync(Address address){
            var result = _context.Addresses.Remove(address);
            if ((await _context.SaveChangesAsync())>0)
                return IdentityResult.Success;
            
            return IdentityResult.Failed();
        }

        public async Task<bool> CheckPasswordSignInAsync(AppUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded) return false;
            return true;
        }
        public async Task<bool> UserExists(string email)
        {
            return await Users.AnyAsync(u => u.Email == email.ToLower());
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}