using HighInfoVoter_Api.Models.Request;
using HighInfoVoter_Api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HighInfoVoter_Api.UnitTests
{
    [TestClass]
    public class EchoTest
    {
        [TestMethod]
        public void Insert_Test()
        {
            EchoAddRequest model = new EchoAddRequest
            {
                Echo = "ECHO Echo echo"
            };

            EchoService svc = new EchoService();
            int result = svc.Insert(model);

            Assert.IsTrue(result > 0, "The insert failed!");
        }
    }
}
