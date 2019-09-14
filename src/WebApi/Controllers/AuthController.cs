using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataTransferObjects;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        public AuthController(INotifier notifier,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings) : base(notifier)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register-user")]
        public async Task<ActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUserDTO.Email,
                Email = registerUserDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(GenerateJWT());
            }
            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse(registerUserDTO);
        }

        [HttpPost("login")]
        public async Task<ActionResult> RegisterUser(LoginUserDTO loginUserDTO)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Email, loginUserDTO.Password,
                isPersistent: false,
                lockoutOnFailure: true);

            if (result.Succeeded)
                return CustomResponse(GenerateJWT());

            if (result.IsLockedOut)
            {
                NotifyError("Usuário bloqueado por tentativas inválidas.");
                return CustomResponse(loginUserDTO);
            }

            NotifyError("Usuário ou senha inválidos");
            return CustomResponse(loginUserDTO);
        }

        private string GenerateJWT()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256)
            });

            return tokenHandler.WriteToken(token);

        }
    }
}