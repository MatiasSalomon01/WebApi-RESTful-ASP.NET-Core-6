﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using WebApiAutores.DTOs;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CrendencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser
            { 
                UserName = credencialesUsuario.Email, 
                Email = credencialesUsuario.Email
            };

            var resultado = await userManager.CreateAsync(usuario, credencialesUsuario.Password);

            if(resultado.Succeeded) 
            {
                return ConstuirToken(credencialesUsuario);
            } 
            else
            {
                return BadRequest(resultado.Errors);   
            }
        }  

        private RespuestaAutenticacion ConstuirToken(CrendencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuario.Email),
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llaveJwt"])); //llave secreta 
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(
                issuer: null, 
                audience: null, 
                claims: claims, 
                expires: expiration, 
                signingCredentials: 
                creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }
    }
}