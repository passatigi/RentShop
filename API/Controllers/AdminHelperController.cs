using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminHelperController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public AdminHelperController(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;

        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            return await _dataContext.Categories.Where(c => c.ParentCategoryId != null)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("features/{id}")]
        public async Task<ActionResult<IEnumerable<FeatureDto>>> GetFeatures(int id)
        {
            return await _dataContext.Features.Where(f => f.CategoryId == id)
                .ProjectTo<FeatureDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpPost("features")]
        public async Task<ActionResult<FeatureDto>> AddNewFeature(AddFeatureDto featureDto)
        {
            var feature = _mapper.Map<Feature>(featureDto);
            _dataContext.Add(feature);
            await _dataContext.SaveChangesAsync();
            return Ok(_mapper.Map<FeatureDto>(feature));
        }

        [HttpPost("products")]
        public async Task<ActionResult<int>> AddProduct(AddProductDto productDto)
        {
            var product  = new Product();
            _mapper.Map(productDto, product);

            _dataContext.Products.Add(product);

            await _dataContext.SaveChangesAsync();

            return Ok(product.Id);
        }

        // [HttpPost("photos")]
        // [HttpPost("add-photo")]
        // public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        // {
        //     var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUserName());

        //     var result = await _photoService.AddPhotoAsync(file);

        //     if (result.Error != null) return BadRequest(result.Error.Message);

        //     var photo = new Photo
        //     {
        //         Url = result.SecureUrl.AbsoluteUri,
        //         PublicId = result.PublicId
        //     };

        //     // if (user.Photos.Count == 0)
        //     // {
        //     //     photo.IsMain = true;
        //     // }

        //     user.Photos.Add(photo);

        //     if (await _unitOfWork.Complete())
        //     {

        //         return CreatedAtRoute("GetUser", new { userName = user.UserName }, _mapper.Map<PhotoDto>(photo));

        //     }


        //     return BadRequest("Problem additing photo");
        // }
    }
}