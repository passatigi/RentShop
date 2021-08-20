using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotoController : BaseApiController
    {
        private readonly IPhotoService _photoService;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public PhotoController(IPhotoService photoService, DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _photoService = photoService;

        }

        [HttpPost("add-photo/{id}")]
        public async Task<ActionResult<ProductImg>> AddPhoto(int id, IFormFile file)
        {


            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new ProductImg
            {
                Link = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                ProductId = id
            };


            _dataContext.ProductImgs.Add(photo);

            if (await _dataContext.SaveChangesAsync() > 0)
            {
                return Ok(_mapper.Map<ProductImgDto>(photo));
            }

            return BadRequest("Problem additing photo");
        }


        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {


            var photo = _dataContext.ProductImgs.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }
            _dataContext.ProductImgs.Remove(photo);

            if (await _dataContext.SaveChangesAsync() > 0) return Ok();

            return BadRequest("Failed to delete the photo");
        }
    }
}