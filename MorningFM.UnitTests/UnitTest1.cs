using Microsoft.VisualStudio.TestTools.UnitTesting;
using MorningFM.Logic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MorningFM.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public static string accessToken; 
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {           
        }

        [TestMethod]
        public async Task GetTopTracks()
        {
            //var spotifyHandler = new SpotifyHandler();
            //var results = await spotifyHandler.AddTrackToPlaylist(accessToken, "08Xxtgw50J4GyidsGn24ey", "spotify:episode:7aLTc5KoQuCvbe7uW7i9Z8"); 
            
            //Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task CreatePlaylist()
        {
            //var spotifyHandler = new SpotifyHandler();
            //await spotifyHandler.CreateRecommendedPlaylist(accessToken);
         
        }

        [TestMethod]
        public async Task GetShowEpisodes()
        {
            //var spotifyHandler = new SpotifyHandler();
            //var showBlobs = await spotifyHandler.GetCurrentUserSavedShows(accessToken);
            //var showIds = showBlobs?.Take(5).Select(s => s.Show.Id).ToArray();

            //var episodeIds = await spotifyHandler.GetLatestEpisodes(accessToken, showIds);

            //Assert.IsNotNull(episodeIds);

            var DT = DateTime.Today;

            var output = DT.ToString("MM-dd") + "-2020";


        }

    }
}
