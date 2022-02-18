#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forfarming.Entities;
using Forfarming.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Forfarming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginUsersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public LoginUsersController(ApplicationContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        
        //Kullanıcı login işlemi
        [HttpPost]
        public async Task<IActionResult> Create(Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.User.FirstOrDefaultAsync(x=>x.UserName==login.UserName && x.Password==login.Password);
               
                if (user!=null)
                {
                    string token = CreateToken(user);
                    return Ok(token);
                }
                else
                {
                    return NotFound();
                }
                
            }
            return NotFound();
        }

        //Token Oluşturma İşlemi
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:creds);

            var jwt=new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

       
    }
}
