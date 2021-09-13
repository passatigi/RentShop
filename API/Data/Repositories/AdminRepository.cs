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
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AdminRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<FeatureDto>>> GetFeatures(int id){
            return await _context.Features.Where(f => f.CategoryId == id)
                .ProjectTo<FeatureDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public void AddFeature(Feature feature){
            _context.Features.Add(feature);
        }
        public void AddProductFeature(ProductFeature feature){
            _context.ProductFeatures.Add(feature);
        }

        public void AddRealProduct(RealProduct realProduct){
            _context.RealProducts.Add(realProduct);
        }

        public void UpdateRealProduct(RealProduct realProduct)
        {
            _context.Entry(realProduct).State = EntityState.Modified;
        }

        public async Task<List<ProductFeature>> GetPreviousFeatures(int productId){
            return await _context.ProductFeatures
                                            .Where(pf => pf.ProductId == productId)
                                            .ToListAsync();
        }

        public void RemoveFeature(ProductFeature feature){
            _context.Remove(feature);
        }
    }
}