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
            accessToken = "BQCSkl8xrZJlxk3F6VxEWjYhKXHo_EPpwmA6CTs-4w_Me76GXZyGU-OEHyTRqe_HeKGSo7DD7NNC2zdn7TEc0pWiGTZeDOuqi2Ve124AxtySEAwv2fd-fgPpZlK50yCkrwl8W2QBh8XUh9o3rc3jrCg-Ty8Sd6l0VTK__xj8FcpmQSHyCumdUuCTSbS1E9PYJ9830k7-lmg"; 
        }

        [TestMethod]
        public async Task GetTopTracks()
        {
            var spotifyHandler = new SpotifyHandler();
            var results = await spotifyHandler.GetTopTracks(accessToken);
            
            Assert.IsNotNull(results);
        }
    }
}
