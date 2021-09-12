using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PhotoController(IPhotoService photoService, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _photoService = photoService;

        }
        [HttpGet("photos/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductImgDto>>> GetPhotos(int productId)
        {
            var imgs = await _unitOfWork.PhotoRepository.GetImagesAsync(productId);

            if(imgs?.Count == 0) return NotFound();

            return Ok(imgs);
        }

        [HttpPost("add-photo/{id}")]
        public async Task<ActionResult<ProductImgDto>> AddPhoto(int id, IFormFile file)
        {

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new ProductImg
            {
                Link = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                ProductId = id
            };

            _unitOfWork.PhotoRepository.AddPhoto(photo);

            if (await _unitOfWork.Complete())
            {
                return Ok(_mapper.Map<ProductImgDto>(photo));
            }

            return BadRequest("Problem adding photo");
        }


        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var photo = _unitOfWork.PhotoRepository.GetPhoto(photoId);

            if (photo == null) return NotFound();

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }
            _unitOfWork.PhotoRepository.RemovePhoto(photo);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete the photo");
        }
    }
}