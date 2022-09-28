using NUnit.Framework;
using RestSharp;
using System.Net;

namespace WebApiExchangeRates.Tests
{
    [TestFixture]
    public class UnitTest
    {
        private const string testingRestAddress = "https://localhost:7214/";

        [Test]
        //ToDo нейминг тестов
        public void TestMethod1()
        {
            // arrange
            RestClient client = new RestClient(testingRestAddress);
            //https://localhost:7214/GetAll?date=2022-09-22
            RestRequest request = new RestRequest("GetAll?date=2022-09-22", Method.Get);

            // act
            RestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    }
}