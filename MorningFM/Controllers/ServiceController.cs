using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MorningFM.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService; 
        public ServiceController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(); 
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("user/{userid}")]
        public string GetAuthorizedToken(Guid userId)
        {
            return _authenticationService.Authorize(userId);
        }
    }
}
