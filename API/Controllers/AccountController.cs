using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public AccountController(DataContext context,
            ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        
        [HttpPut("updateUserInfo")]
        public async Task<ActionResult<UserDto>> UpdateUserInfo(UserUpdateDto userUpdateDto){
            var user = await _unitOfWork.UserRepository.GetLoggedInUserAsync(User);
            
            if(user==null) return NotFound("User not found");

            if(!IsValidEmail(userUpdateDto.Email)) return BadRequest("Wrong email");

            if(await _unitOfWork.UserRepository.FindUserAsync(userUpdateDto.Email)
                != null && await _unitOfWork.UserRepository.FindUserAsync(userUpdateDto.Email)
                != user) return BadRequest("User with this email already exists");

            _mapper.Map(userUpdateDto, user);
            _unitOfWork.UserRepository.UpdateUser(user);
            if (await _unitOfWork.Complete()) return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Token = await _tokenService.CreateTokenAsync(user),
            };

            return BadRequest("Failed to update user info");
        }


        [HttpPut("changePassword")]
        public async Task<ActionResult> ChangePassword(string currentPassword, string newPassword){
            var user = await _unitOfWork.UserRepository.GetLoggedInUserAsync(User);
            
            if(user==null) return NotFound("User not found");
            
            if (!(await _unitOfWork.UserRepository.CheckPasswordAsync(user, currentPassword))) 
                return BadRequest("Wrong password!");

            if((await _unitOfWork.UserRepository.ChangePasswordAsync(user, currentPassword, newPassword))
                .Succeeded) return Ok();

            return BadRequest("Failed to change password");
        }

        [HttpGet("getAddresses/{email}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetUserAddresses(string email)
        {
           var user = await _unitOfWork.UserRepository.FindUserAsync(email);

            if(user==null) return NotFound("User not found");

           var addresses = await _unitOfWork.UserRepository.GetAddressesAsync(user.Id);

           return Ok(addresses);
        }

        [HttpPost("addAddress")]
        public async Task<ActionResult<AddressDto>> AddAddress(AddressDto addressDto){
            var user = await _unitOfWork.UserRepository.GetLoggedInUserAsync(User);
            
            if(user==null) return NotFound("User not found");

            var address = _mapper.Map<Address>(addressDto);
            address.AppUserId = user.Id;

            if (await _unitOfWork.UserRepository.AddressAlreadyExists(address)){
                return BadRequest("Address already exists");
            }

            if((await _unitOfWork.UserRepository.AddAddressAsync(address)).Succeeded){
                return Ok(address);
            }

            return BadRequest("Failed to add address");
        }

        [HttpDelete("deleteAddress/{addressId}")]
        public async Task<ActionResult<bool>> DeleteAddress(int addressId){
            var user = await _unitOfWork.UserRepository.GetLoggedInUserAsync(User);

            if(user == null) return NotFound("User not found");

            var address = await _unitOfWork.UserRepository.FindAddressAsync(addressId);

            if (address == null) return NotFound("Wrong address id");

            if((await _unitOfWork.UserRepository.DeleteAddressAsync(address)).Succeeded){
                return Ok();
            }

            return BadRequest("Failed to delete address");
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.UserRepository.Users
            .SingleOrDefaultAsync(u => u.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid email");
           

            if (!await _unitOfWork.UserRepository.CheckPasswordSignInAsync(user, loginDto.Password)) return Unauthorized("Wrong password");

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Token = await _tokenService.CreateTokenAsync(user),
            };
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _unitOfWork.UserRepository.UserExists(registerDto.Email)) 
                return BadRequest("Email is taken");

            if(!IsValidEmail(registerDto.Email)) return BadRequest("Wrong email");

            var user = _mapper.Map<AppUser>(registerDto);
            user.Email = registerDto.Email.ToLower();

            var result = await _unitOfWork.UserRepository.CreateUserAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);
            var roleResult = await _unitOfWork.UserRepository.AddToRoleAsync(user, "Customer");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Token = await _tokenService.CreateTokenAsync(user)
            };
        }
        private bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}