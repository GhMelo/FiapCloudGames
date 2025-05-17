using Core.Entity;
using Core.Input.AuthInput;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;
        public AuthController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginInput usuario)
        {
            var usuarioLogin = _usuarioRepository.obterPorNome(usuario.Nome);

            if (usuario.Nome == usuarioLogin.Nome && usuario.Senha == usuarioLogin.Senha)
            {
                if(usuarioLogin.Tipo == TipoUsuario.Administrador)
                {
                    var token = GenerateToken(usuario.Nome, "Administrador");
                    return Ok(new { token });
                }
                else
                {
                    var token = GenerateToken(usuario.Nome, "UsuarioPadrao");
                    return Ok(new { token });
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GenerateToken(string username, string role)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

