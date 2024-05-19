using HospitalManagement.Data;
using HospitalManagement.Models.EntityModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalManagement.Controllers
{
    public class AuthController : Controller
    {
        public readonly _DbContext _context;
        public AuthController(_DbContext context) 
        { 
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string user, string password)
        {
            User u = _context.Users.Where(u => u.Name == user && u.Password == password).FirstOrDefault();
            if (u != null)
            {
                //1, Collection of claims
                List<Claim> userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,u.Name),
                    new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),
                    new Claim(ClaimTypes.Role,u.RoleId.ToString()),
                    new Claim(ClaimTypes.Email,u.Email),
                    new Claim("ContactNo",u.ContactNo),
                    new Claim("Photo",u.Photo),
                    new Claim("FullName",u.FullName)

               };

                //2. Make Claim Identity
                var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                //3. Make Claim Principal
                var userPrinciple = new ClaimsPrincipal(new[] { userIdentity });

                HttpContext.SignInAsync(userPrinciple);


                return RedirectToAction("Index", "Home");

            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
            
        }
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }
    }
}
