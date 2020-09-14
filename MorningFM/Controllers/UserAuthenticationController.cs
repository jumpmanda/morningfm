using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using MorningFM.Logic;
using MorningFM.Logic.DTOs;
using MorningFM.Logic.Repositories;
using MorningFM.Logic.Repository;
using Newtonsoft.Json;

namespace MorningFM.Controllers
{
    [Route("api/authentication")]
    public class UserAuthenticationController : Controller
    {
        #region Properties
        private static SpotifyAuthorization _spotifyAuthorization;
        private SpotifyHandler _spotifyHandler;
        private IRepository<User> _repo;
        private IRepository<Session> _sessionRepo;
        private ILogger _logger;
        #endregion

        #region Construction 
        public UserAuthenticationController(IConfiguration configuration, MorningFMRepository<User> repo, MorningFMRepository<Session> sessionRepo, ILogger<UserAuthenticationController> logger)
        {
            _spotifyAuthorization = new SpotifyAuthorization(configuration.GetValue<string>("Spotify:ClientId"), configuration.GetValue<string>("Spotify:ClientSecret"));
            _spotifyHandler = new SpotifyHandler();
            _repo = repo ?? throw new ArgumentException("User repository is not initialized.");
            _sessionRepo = sessionRepo ?? throw new ArgumentException("Session repository is not initialized.");
            _logger = logger ?? throw new ArgumentException("Logger is not initialized.");
        }
        #endregion 

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authorize([FromBody] UserLoginRequest loginRequest)
        {
            try
            {
                if(loginRequest == null || loginRequest.Email == null || loginRequest.Password == null)
                {
                    return BadRequest("Must provide login credentials.");
                }
                if(loginRequest.LoginAction == LoginAction.SIGNUP)
                {
                    try
                    {
                        var plainTextBytes = Encoding.UTF8.GetBytes(loginRequest.Password);
                        var basicAuth = System.Convert.ToBase64String(plainTextBytes);
                        //TODO: Do better job at saving credentials; use some salting perhaps...
                        await _repo.AddAsync(new User() { Email = loginRequest.Email, Password = basicAuth });
                        _logger.LogInformation($"New user added {loginRequest.Email}"); 
                    }
                    catch
                    {
                        return BadRequest("New user could not be added.");
                    }                    
                }
                else
                {
                    try
                    {
                        var results = await _repo.GetAsync<User>(u => u.Email == loginRequest.Email);
                        if (results == null)
                        {
                            return BadRequest("User could not be found.");
                        }
                        var bytes = Convert.FromBase64String(results[0].Password);
                        var plainTextBytes = Encoding.UTF8.GetBytes(loginRequest.Password);
                        if (!Encoding.UTF8.GetString(plainTextBytes).Equals(loginRequest.Password))
                        {
                            return BadRequest("Bad password.");
                        }
                    }
                    catch
                    {
                        return BadRequest("Could not login user.");
                    }
                   
                }
                
                var url = _spotifyAuthorization.GetLoginPage().ToString(); 
                return Ok(new { redirectUrl = url}); 
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }        


        [HttpGet("callback")]
        public async Task<IActionResult> GetAccessToken()
        {
            StringValues code;
            Request.Query.TryGetValue("code", out code);

            try
            {
                var accessBlob = await _spotifyAuthorization.GetAccessBlob(code);
                var session = new Session() { Token = Guid.NewGuid(), spotifyAccess = accessBlob };
                await _sessionRepo.AddAsync(session); //todo: add logout for user
                return Redirect($"/home?token={session.Token}");
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}