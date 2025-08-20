using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Domain.Mappers;
using MinimalApi.Infra.Interfaces;

namespace MinimalApi.Domain.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;
        private readonly IJWTService _jwtService;

        public AuthController(IAdministratorService administratorService, IJWTService jwtService)
        {
            _administratorService = administratorService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var login = await _administratorService.Login(loginDTO);
            if (login != null)
            {
                return Ok(_jwtService.GenerateToken(login));
            }
            else
            {
                return Unauthorized("Falha ao logar");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var registered = await _administratorService.Register(registerDTO);

            if (registered != null)
            {
                return Ok(registered.ToAdministratorDTO());
            }
            return BadRequest();
        }

        [HttpGet("all/{page?}/{pageSize?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll([FromRoute] int? page, [FromRoute] int? pageSize)
        {
            var administrators = await _administratorService.All(page, pageSize);
            return Ok(administrators.Select(adm => adm.ToAdministratorDTO()));
        }

        [HttpGet("id/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var administrator = await _administratorService.FindById(id);

            if (administrator == null)
            {
                return NotFound();
            }

            return Ok(administrator.ToAdministratorDTO());
        }
    }
}
