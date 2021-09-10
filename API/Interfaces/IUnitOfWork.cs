using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IDeliveryManRepository DeliveryManRepository { get; }
        IOrderRepository OrderRepository { get; }
        IMessageRepository MessageRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}