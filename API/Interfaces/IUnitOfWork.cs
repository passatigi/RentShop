using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}