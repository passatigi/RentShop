using System.Threading.Tasks;
using API.Interfaces.Repositories;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IDeliveryManRepository DeliveryManRepository { get; }
        IOrderRepository OrderRepository { get; }
        IMessageRepository MessageRepository { get; }
        IProductRepository ProductRepository { get; }
        IAdminRepository AdminRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}