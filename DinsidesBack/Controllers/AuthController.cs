using Application.Auth.Dto;
using Application.Auth.Services.Interfaces;
using Application.Usuarios.Services.Interface;
using DinsidesBack.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IJwtServices _jwtServices;
        public AuthController(IConfiguration configuration, IUserService userService, IJwtServices jwtServices)
        {
            _configuration = configuration;
            _userService = userService;
            _jwtServices = jwtServices;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSecurityDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorValidationModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<BadRequest, Ok<UserSecurityDto>>> Post([FromBody] LoginRequest userAuthDto)
        {
            UserSecurityDto userSecurity = await _userService.LoginAsync(userAuthDto);
            return TypedResults.Ok(userSecurity);
        }
    }

}
