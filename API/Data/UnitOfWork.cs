using System.Threading.Tasks;
using API.Data.Repositories;
using API.Interfaces.Repositories;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public IMessageRepository MessageRepository => new MessageRepository(_dataContext, _mapper);

        public async Task<bool> Complete()
        {
            return  await _dataContext.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _dataContext.ChangeTracker.HasChanges();
        }

    }
}