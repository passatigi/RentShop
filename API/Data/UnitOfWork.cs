using System.Threading.Tasks;
using API.Entities;
using API.Data.Repositories;
using API.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using API.Interfaces;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public IUserRepository UserRepository => new UserRepository(_context, _userManager,
            _signInManager);

        public ICategoryRepository CategoryRepository => 
            new CategoryRepository(_context,_mapper);
        public IOrderRepository OrderRepository => 
            new OrderRepository(_context, _mapper);
        public IDeliveryManRepository DeliveryManRepository =>
            new DeliveryManRepository(_context, _mapper);
        public IMessageRepository MessageRepository => 
            new MessageRepository(_context, _mapper);

        public IProductRepository ProductRepository => 
            new ProductRepository(_context, _mapper);

        public IAdminRepository AdminRepository => 
            new AdminRepository(_context, _mapper);

        public IPhotoRepository PhotoRepository => 
            new PhotoRepository(_context, _mapper);
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
