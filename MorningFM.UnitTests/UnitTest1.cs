using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        }


        [TestMethod]
        [Ignore]
        public async Task GetShowEpisode()
        {            
            var logger = new Mock<ILogger<SpotifyHandler>>(); 
            var httpHandlerLogger = new Mock<ILogger<HttpHandler>>();

            var spotifyHandler = new SpotifyHandler(logger.Object, new HttpHandler(httpHandlerLogger.Object));

            var showId = "1aCcf9CN3cunTBdkIzYTvo";
            var result = await spotifyHandler.GetLatestEpisode(accessToken, showId); 

            Assert.IsNotNull(result);
        }

    }
}
