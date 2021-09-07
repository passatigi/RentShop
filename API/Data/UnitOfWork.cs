using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public IUserRepository UserRepository => new UserRepository(_context, _userManager,
            _signInManager);

        public ICategoryRepository CategoryRepository => new CategoryRepository(_context);

        public UnitOfWork(DataContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _context = context;
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync()>0;
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}