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
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace MorningFM.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LibraryController : Controller
    {
        private ILogger<LibraryController> _logger; 
        private SpotifyAuthorization _spotifyAuthorization;
        private SpotifyHandler _spotifyHandler;
        private IRepository<Session> _sessionRepo;

        public LibraryController(MorningFMRepository<Session> sessionRepo, 
            ILogger<LibraryController> logger, 
            SpotifyAuthorization spotifyAuthorization, 
            SpotifyHandler spotifyHandler)
        {
            _logger = logger ?? throw new ArgumentException("Logger is not initialized.");
            _spotifyAuthorization = spotifyAuthorization ?? throw new ArgumentException("Spotify authorization is not initialized.");
            _spotifyHandler = spotifyHandler ?? throw new ArgumentException("Spotify handler is not initialized.");
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
                _logger.LogDebug(new EventId((int)MorningFMEventId.SpotifyAPI), $"Session {sessionToken} - Fetching user shows.");
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
            try
            {
                var sessionResults = await _sessionRepo.GetAsync<Session>(s => s.Token == Guid.Parse(sessionToken));
                if (sessionResults.Count == 0)
                {
                    return BadRequest("Could not retrieve tracks. Bad session.");
                }
                var session = sessionResults[0];
                var results = await _spotifyHandler.GetTopTracks(session.spotifyAccess.AccessToken);
                _logger.LogDebug(new EventId((int)MorningFMEventId.SpotifyAPI), $"Session {sessionToken} - Fetching user top tracks.");
                return Ok(results);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
           
        }

        [HttpGet("{sessionToken}/recommended-tracks")]
        public async Task<IActionResult> GetRecommendedTracks(string sessionToken)
        {
            if (string.IsNullOrEmpty(sessionToken))
            {
                return BadRequest("Must provide session token.");
            }
            try
            {
                var sessionResults = await _sessionRepo.GetAsync<Session>(s => s.Token == Guid.Parse(sessionToken));
                if (sessionResults.Count == 0)
                {
                    return BadRequest("Could not retrieve tracks. Bad session.");
                }
                var session = sessionResults[0];
                var results = await _spotifyHandler.GetRecommendedTracks(session.spotifyAccess.AccessToken);
                _logger.LogDebug(new EventId((int)MorningFMEventId.SpotifyAPI), $"Session {sessionToken} - Fetching user recommended tracks.");
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }          
        }

        [HttpPost("{sessionToken}/recommended-playlist")]
        public async Task<IActionResult> CreatePlaylist(string sessionToken, [FromBody] MorningShowRequest showRequest)
        {
            if (string.IsNullOrEmpty(sessionToken))
            {
                return BadRequest("Must provide session token.");
            }
            try
            {
                var sessionResults = await _sessionRepo.GetAsync<Session>(s => s.Token == Guid.Parse(sessionToken));
                if (sessionResults.Count == 0)
                {
                    return BadRequest("Could not retrieve tracks. Bad session.");
                }
                
                var session = sessionResults.FirstOrDefault();

                var playlistId = await _spotifyHandler.CreateRecommendedPlaylist(session.spotifyAccess.AccessToken);
                _logger.LogDebug(new EventId((int)MorningFMEventId.SpotifyAPI), $"Session {sessionToken} - Fetching user recommended playlist.");
                //todo: take in show ids to fetch episodes and add to playlist

                if (showRequest == null)
                {
                    return BadRequest("Invalid request.");
                }
                if (showRequest.ShowIds == null || showRequest.ShowIds.Length == 0)
                {
                    return BadRequest("Must provide at least 1 podcast episode to add.");
                }

                var episodesResults = await _spotifyHandler.GetLatestEpisodes(session.spotifyAccess.AccessToken, showRequest.ShowIds);
                var episodeIds = episodesResults.Select(e => $"spotify:episode:{e}").ToArray();
                var completed = await _spotifyHandler.AddTrackToPlaylistWithPosition(session.spotifyAccess.AccessToken, playlistId, episodeIds);

                return Ok(new { playlistId = playlistId });
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
           
        }

    }
}