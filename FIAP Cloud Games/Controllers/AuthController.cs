﻿using Application.Input.AuthInput;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginInput usuario)
        {
            var usuarioLoginToken = _authService.FazerLogin(usuario);

            if (usuarioLoginToken != string.Empty)
            {
                return Ok(usuarioLoginToken);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

