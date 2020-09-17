using Microsoft.VisualStudio.TestTools.UnitTesting;
using MorningFM.Logic;
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
            accessToken = "BQAM8pHUUo62QQMqB63R41SeTgYBY0Pd5SNADAIRYIzeJ4IA2CKBPoU2XlAoLY9JB5apu4Gr9SCNd1m6GnUglFJwoQwe7hh4876_9FN2ouograt_TvXOK6P7W54znbgDKTY4ned_4b00EwUSRRZVo4Ux_c7zIaZ1XTUplBl-kWMxjhYmTsElBEyg-9IHf49fiL2LUKi0--idUTx9ix7Kl55-G2eGgfSzRKda9XnVB-hbPTroy4EjN-pCNSMu2Q"; 
        }

        [TestMethod]
        public async Task GetTopTracks()
        {
            var spotifyHandler = new SpotifyHandler();
            var results = await spotifyHandler.AddTrackToPlaylist(accessToken, "08Xxtgw50J4GyidsGn24ey", "spotify:episode:7aLTc5KoQuCvbe7uW7i9Z8"); 
            
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task CreatePlaylist()
        {
            var spotifyHandler = new SpotifyHandler();
            await spotifyHandler.CreateRecommendedPlaylist(accessToken);
         
        }
    }
}
