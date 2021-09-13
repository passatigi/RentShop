using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<ActionResult<IEnumerable<FeatureDto>>> GetFeatures(int id);
        void AddFeature(Feature feature);
        void AddProductFeature(ProductFeature feature);
        void AddRealProduct(RealProduct realProduct);
        void UpdateRealProduct(RealProduct realProduct);
        Task<List<ProductFeature>> GetPreviousFeatures(int productId);
        void RemoveFeature(ProductFeature feature);
    }
}