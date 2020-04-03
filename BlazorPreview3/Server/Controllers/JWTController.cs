using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BlazorPreview3.Shared;

namespace BlazorPreview3.Server.Controllers
{
    [Route("auth")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public JWTController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(User userInfo)
        {
            UserToken userToken = new UserToken() { IsSuccess = false };
            //Aqui pueder ir un proceso de validacion.
            if (userInfo.Username == "email@gmail.com" && userInfo.Password == "123456789")
            {
                //Metodo que contruye el token
                userToken = BuildToken(userInfo);
            }
            return userToken;
        }

        private UserToken BuildToken(User userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Username),
                new Claim(ClaimTypes.Name, userInfo.Name ),
                new Claim(ClaimTypes.Email, userInfo.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
           };

            //Obtener Key del Arhivo appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"]));
            //Encriptar el key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            var expiration = DateTime.Today.AddHours(1);

            //Creacion del token
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            //Devolver el token 
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                IsSuccess = true
            };
        }
    }
}