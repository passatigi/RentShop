using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(
            int categoryId)
        {
            return await _context.Products.Include(p => p.ProductImgs)
                .Where(p => p.CategoryId == categoryId)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<DetailedProductDto> GetProductById(
            int id){
            return await _context.Products.Include(p => p.ProductImgs)
                .Include(p => p.ProductFeatures).ThenInclude(f => f.Feature)
                .Include(p => p.RealProducts)
                .Where(p => p.Id == id)
                .ProjectTo<DetailedProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<RealProductSchedule> GetRealProductShedule(int id){
            return await _context.RealProducts
                .Where(p => p.Id == id)
                .Include(rp => rp.OrderProducts).ThenInclude(op => op.Order)
                .ProjectTo<RealProductSchedule>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public void AddProduct(Product product){
            _context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}