using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IPhotoRepository
    {
        Task<List<ProductImgDto>> GetImagesAsync(int productId);
        void AddPhoto(ProductImg photo);
        ProductImg GetPhoto(int photoId);
        void RemovePhoto(ProductImg photo);
    }
}