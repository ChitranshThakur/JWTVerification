using JWTVerification.Models;
using JWTVerification.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTVerification.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]   
    public class UsersController : Controller
    {
        private readonly IJWtManagerRepository _jWTManager;

        public UsersController(IJWtManagerRepository jWtManager)
        {
            _jWTManager = jWtManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get
        [HttpGet("get")]
        public List<string> Get()
        {
            var users = new List<string>
        {
            "Chitransh",
            "Vishu",
            "Anita"
        };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users users)
        {
            var token = _jWTManager.Authenticate(users);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }
}
