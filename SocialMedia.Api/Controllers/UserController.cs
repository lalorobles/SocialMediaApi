using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Produces("Application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = nameof(RoleType.Administrator))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UserController(IUserService userService, IMapper mapper, IPasswordService passwordService)
        {
            _userService = userService;
            _mapper = mapper; 
            _passwordService = passwordService;
        }

        /// <summary>
        /// Create a new User.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(PostUser))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Password = _passwordService.Hash(user.Password);

            await _userService.RegisterUser(user);

            userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);
            return Ok(response);
        }
    }
}
