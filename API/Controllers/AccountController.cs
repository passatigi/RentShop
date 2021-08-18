

using System;
using System.Net.Mail;
using System.Threading.Tasks;
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
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, ITokenService tokenService, IMapper mapper)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _mapper = mapper;

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