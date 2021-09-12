using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PhotoRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductImgDto>> GetImagesAsync(int productId)
        {
            return await _context.ProductImgs
                            .Where(i => i.ProductId == productId)
                            .ProjectTo<ProductImgDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        public void AddPhoto(ProductImg photo){
            _context.ProductImgs.Add(photo);
        }

        public ProductImg GetPhoto(int photoId){
            return _context.ProductImgs.FirstOrDefault(x => x.Id == photoId);
        }

        public void RemovePhoto(ProductImg photo){
            _context.ProductImgs.Remove(photo);
        }
    }
}