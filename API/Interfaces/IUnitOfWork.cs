using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IDeliveryManRepository DeliveryManRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}