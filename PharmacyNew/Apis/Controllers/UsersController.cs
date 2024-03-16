using Apis.Models;
using BL1.Authuntiacation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly Users _user;
        private IConfiguration _configuration;
       
        ApiResponse oApiResponse = new ApiResponse();
        public UsersController(Users user, IConfiguration configuration)
        {
           
            _user = user;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ApiResponse Login([FromBody] string Name, string password)
        {
            bool user = _user.IsUseValid(Name, password);


            if (!user)
            {

                oApiResponse.StatusCode = 400;
                oApiResponse.Errors = "Unauthorized";
                return oApiResponse;
            }
            else
            {
                try
                {

                    string token = GenerateToken(Name, password);
               
                    oApiResponse.Data = token;
                    oApiResponse.Errors = null;
                    oApiResponse.StatusCode = 200;
                    return oApiResponse;


                }
                catch (Exception ex)
                {


                    oApiResponse.Data = null;
                    oApiResponse.Errors = "An error occurred while login.";
                    oApiResponse.StatusCode = 502;

                    return oApiResponse;
                }



            }



        }

        private string GenerateToken(string Name, string password)
        {
            var securtyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credebtials = new SigningCredentials(securtyKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JWT:Issure"], _configuration["JWT:Audience"], null, expires: DateTime.Now.AddSeconds(10), signingCredentials: credebtials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}
