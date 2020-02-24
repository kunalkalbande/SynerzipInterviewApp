using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SynerzipInterviewApp.Models;
using SynerzipInterviewApp.Models.Repository;
using Newtonsoft.Json;

namespace SynerzipInterviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IAuthenticationRepository authService;

        public LoginController (IAuthenticationRepository authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = authService.Login(login);
                if (!user.isAuthenticated)
                {
                    return Unauthorized();
                }

                if (null != user)
                {
                    var jwtUser = JWTSettings.GetJWTUser(user);
                    return Ok(jwtUser);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,ex);
            }
        
        }

        [HttpPost("/RefreshToken")]
        public IActionResult RefreshToken([FromBody]RefreshTokenModel token)
        {
            try
            {
                JWTUserModel user = JWTSettings.GetNewAccessToken(token);
                if (user == null)
                    return Unauthorized();
                return Ok(user);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status401Unauthorized);
            }
       
        }


        [HttpPost("/ValidateToken")]
        public IActionResult ValidateToken([FromBody] RefreshTokenModel token)
        {
            try
            {
                var user = JWTSettings.GetUserFromToken(token.AccessToken);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}