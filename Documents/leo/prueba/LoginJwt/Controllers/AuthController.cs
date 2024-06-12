using JwtAuthLibrary;
using LoginJwt.Models;
using LoginJwt.Data;
using LoginJwt.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginJwt.Controllers.Authentication
{
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BaseContext _context;
        private readonly Utilidades _utilidades;
        private readonly JwtAuthManager _jwtAuthManager;

        public AuthController(BaseContext context, Utilidades utilidades, JwtAuthManager jwtAuthManager)
        {
            _context = context;
            _utilidades = utilidades;
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpPost]
        [Route("api/registration")]
        public async Task<IActionResult> Registrarse(Usuario objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modeloUsuario = new Usuario
            {
                Nombre = objeto.Nombre,
                Email = objeto.Email,
                Password = _utilidades.EncriptarSHA256(objeto.Password),
                Documento = objeto.Documento,
                IdTipoDocumento = objeto.IdTipoDocumento,
                IdRol = objeto.IdRol
            };

            await _context.Usuarios.AddAsync(modeloUsuario);
            await _context.SaveChangesAsync();

            if (modeloUsuario.Id != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(UsuarioLogin objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioEncontrado = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == objeto.Email);

            if (usuarioEncontrado == null || !_utilidades.VerificarSHA256(objeto.Password, usuarioEncontrado.Password))
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            }
            else
            {
                var token = _jwtAuthManager.GenerateToken(usuarioEncontrado.Id.ToString(), usuarioEncontrado.Email);
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = token });
            }
        }

    }
}
