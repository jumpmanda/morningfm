using System;
using System.Collections.Generic;
using System.IO;
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
        private IRepository<User> _repo;
        private IRepository<Session> _sessionRepo;
        private ILogger _logger;
        #endregion

        #region Construction 
        public UserAuthenticationController(IConfiguration configuration, 
            MorningFMRepository<User> repo, 
            MorningFMRepository<Session> sessionRepo, 
            ILogger<UserAuthenticationController> logger, SpotifyAuthorization spotifyAuthorization)
        {
            _logger = logger ?? throw new ArgumentException("Logger is not initialized.");
            _spotifyAuthorization = spotifyAuthorization ?? throw new ArgumentException("Spotify authorization is not initialized.");
            _repo = repo ?? throw new ArgumentException("User repository is not initialized.");
            _sessionRepo = sessionRepo ?? throw new ArgumentException("Session repository is not initialized.");       
        }
        #endregion 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authorize()
        {
            try
            {
                StringValues code;
                Request.Query.TryGetValue("code", out code);
                StringValues state;
                Request.Query.TryGetValue("state", out state);
                _logger.LogInformation($"Spotify Initial Login was successful code: {code} state: {state}");            

                var url = _spotifyAuthorization.GetLoginPage().ToString();
                _logger.LogDebug(new EventId((int)MorningFMEventId.UserAuthentication), "User provided with spotify authorization login url.");
                return Redirect(url); 
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
            _logger.LogDebug(new EventId((int)MorningFMEventId.UserAuthentication), "Spotify provided authorization code in callback.");

            try
            {
                var accessBlob = await _spotifyAuthorization.GetAccessBlob(code);
                var session = new Session() { Token = Guid.NewGuid(), spotifyAccess = accessBlob };
                await _sessionRepo.AddAsync(session);
                return Redirect($"/playlist?token={session.Token}");
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        } 
        
        //TODO: Add logout

    }
}