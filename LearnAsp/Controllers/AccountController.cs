using LearnAsp.Domain;
using LearnAsp.Exceptions;
using LearnAsp.Models;
using LearnAsp.Repository;
using LearnAsp.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearnAsp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVm);
            }
            try
            {
                var user = await _repository.GetByLogin(loginVm.Login);

                if (user.Password != loginVm.Password)
                {
                    throw new NotFoundException();
                }
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
                };
                var key = AuthSettings.GetSymmetricSecurityKey();
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken (
                issuer: AuthSettings.ISSUER,
                audience: AuthSettings.AUDIENCE,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);


                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                Response.Cookies.Append("jwt", tokenString, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.Now.AddMinutes(60)
                });

                return RedirectToAction("Index", "Home");
            }
            catch (NotFoundException)
            {
                return View(loginVm);
            }
        }
    }
}
