

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly DataContext _context;

        public AccountController(IAccountService accountService, DataContext context,
            ITokenService tokenService, IMapper mapper)
        {
            _accountService = accountService;
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;

        }

        
        [HttpPut("updateUserInfo")]
        public async Task<ActionResult<UserDto>> UpdateUserInfo(UserUpdateDto userUpdateDto){
            var email = User.GetEmail();
        
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email == email);
            
            if(user==null) return BadRequest("User not found");
            _mapper.Map(userUpdateDto, user);

            if(await _context.Users.FirstOrDefaultAsync(x => x.Email == userUpdateDto.Email)
                != null) return BadRequest("User with this email already exists");

            if(await _accountService.UpdateUserAsync(user)) return new UserDto
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
            var email = User.GetEmail();
        
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email == email);
            
            if(user==null) return BadRequest("User not found");
            
            if (!(await _accountService.CheckPasswordAsync(user, currentPassword))) 
                return BadRequest("Wrong password!");

            if((await _accountService.ChangePasswordAsync(user, currentPassword, newPassword))
                .Succeeded) return Ok(newPassword);

            return BadRequest("Failed to change password");
        }

        [HttpGet("getAddresses/{email}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetUserAddresses(string email)
        {
           var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email == email);

            if(user==null) return BadRequest("User not found");

           var addresses = await _context.Addresses
                            .Where(i => i.AppUserId == user.Id)
                            .ToListAsync();

           if(addresses?.Count == 0) return Ok("User hasn't added any addresses");

           return Ok(addresses);
        }

        [HttpPost("addAddress")]
        public async Task<ActionResult<AddressDto>> AddAddress(AddressDto addressDto){
            var email = User.GetEmail();
        
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email == email);
            
            if(user==null) return BadRequest("User not found");

            var address = _mapper.Map<Address>(addressDto);
            address.AppUserId = user.Id;

            if (await _accountService.AddressAlreadyExists(address)){
                return BadRequest("Address already exists");
            }

            if((await _accountService.AddAddressAsync(address)).Succeeded){
                return Ok(address);
            }

            return BadRequest("Failed to add address");
        }

        [HttpDelete("deleteAddress/{addressId}")]
        public async Task<ActionResult<bool>> DeleteAddress(int addressId){
            var email = User.GetEmail();
        
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email == email);

            if(user == null) return BadRequest("User not found");

            var address = await _context.Addresses.FindAsync(addressId);

            if (address == null) return BadRequest("Wrong id");

            if((await _accountService.DeleteAddressAsync(address)).Succeeded){
                return Ok();
            }

            return BadRequest("Failed to delete address");
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _accountService.Users
            .SingleOrDefaultAsync(u => u.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid email");
           

            if (! await _accountService.CheckPasswordSignInAsync(user, loginDto.Password)) return Unauthorized("Wrong password");

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
            if (await UserExists(registerDto.Email)) return BadRequest("Email is taken");

            var user = _mapper.Map<AppUser>(registerDto);

            if(!IsValidEmail(registerDto.Email)) return BadRequest("Wrong email");

            user.Email = registerDto.Email.ToLower();
            var emailParts = registerDto.Email.ToLower().Split('@');
            user.UserName = emailParts[0] + emailParts[1];



            var result = await _accountService.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);
            var roleResult = await _accountService.AddToRoleAsync(user, "Customer");

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

        private async Task<bool> UserExists(string email)
        {
            return await _accountService.Users.AnyAsync(u => u.Email == email.ToLower());
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