using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.DataTransferObjects;

namespace WebApi.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(INotifier notifier,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager) : base(notifier)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                return CustomResponse(registerUserDTO);
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
                return CustomResponse(loginUserDTO);

            if (result.IsLockedOut)
            {
                NotifyError("Usuário bloqueado por tentativas inválidas.");
                return CustomResponse(loginUserDTO);
            }

            NotifyError("Usuário ou senha inválidos");
            return CustomResponse(loginUserDTO);
        }
    }
}