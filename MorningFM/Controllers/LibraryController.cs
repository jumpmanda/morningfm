using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MorningFM.Logic;
using MorningFM.Logic.Repositories;
using MorningFM.Logic.DTOs;
using MorningFM.Logic.Repository;

namespace MorningFM.Controllers
{
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        private SpotifyAuthorization _spotifyAuthorization;
        private SpotifyHandler _spotifyHandler;
        private IRepository<Session> _sessionRepo;
        public LibraryController(IConfiguration configuration, MorningFMRepository<Session> sessionRepo)
        {
            _spotifyAuthorization = new SpotifyAuthorization(configuration.GetValue<string>("Spotify:ClientId"), configuration.GetValue<string>("Spotify:ClientSecret"));
            _spotifyHandler = new SpotifyHandler();
            _sessionRepo = sessionRepo ?? throw new ArgumentException("Session repository is not initialized.");
        }

        [HttpGet("{sessionToken}/shows")]
        public async Task<IActionResult> GetUserShows(string sessionToken)
        {
            try
            {
                if (string.IsNullOrEmpty(sessionToken))
                {
                    return BadRequest("Must provide session token.");
                }
                var sessionResults = await _sessionRepo.GetAsync<Session>(s => s.Token == Guid.Parse(sessionToken));                 
                if(sessionResults.Count == 0)
                {
                    return BadRequest("Could not retrieve shows. Bad session."); 
                }
                var session = sessionResults[0]; 
                var results = await _spotifyHandler.GetCurrentUserSavedShows(session.spotifyAccess.AccessToken);
                return Ok(results); 
            }
            catch (Exception e)
            {
                //TODO: if could not make request due to expired access token, try refreshing token
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }        

        [HttpGet("{sessionToken}/top-tracks")]
        public async Task<IActionResult> GetTopTracks(string sessionToken)
        {
            if (string.IsNullOrEmpty(sessionToken))
            {
                return BadRequest("Must provide session token.");
            }
            var sessionResults = await _sessionRepo.GetAsync<Session>(s => s.Token == Guid.Parse(sessionToken));
            if (sessionResults.Count == 0)
            {
                return BadRequest("Could not retrieve tracks. Bad session.");
            }
            var session = sessionResults[0];
            var results = await _spotifyHandler.GetTopTracks(session.spotifyAccess.AccessToken);
            return Ok(results);
        }

        [HttpGet("{sessionToken}/recommended-tracks")]
        public async Task<IActionResult> GetRecommendedTracks(string sessionToken)
        {
            if (string.IsNullOrEmpty(sessionToken))
            {
                return BadRequest("Must provide session token.");
            }
            var sessionResults = await _sessionRepo.GetAsync<Session>(s => s.Token == Guid.Parse(sessionToken));
            if (sessionResults.Count == 0)
            {
                return BadRequest("Could not retrieve tracks. Bad session.");
            }
            var session = sessionResults[0];
            var results = await _spotifyHandler.GetRecommendedTracks(session.spotifyAccess.AccessToken);
            return Ok(results);
        }

        [HttpPost("{sessionToken}/recommended-playlist")]
        public async Task<IActionResult> CreatePlaylist(string sessionToken)
        {
            if (string.IsNullOrEmpty(sessionToken))
            {
                return BadRequest("Must provide session token.");
            }
            var sessionResults = await _sessionRepo.GetAsync<Session>(s => s.Token == Guid.Parse(sessionToken));
            if (sessionResults.Count == 0)
            {
                return BadRequest("Could not retrieve tracks. Bad session.");
            }
            var session = sessionResults[0];
            await _spotifyHandler.CreateRecommendedPlaylist(session.spotifyAccess.AccessToken);
            return Ok();
        }

    }
}