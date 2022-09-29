using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using WebApiExchangeRates.Models;

namespace WebApiExchangeRates.Tests
{
    [TestFixture]
    public class ExchangeRatesControllerUnitTests
    {
        private const string TestingRestAddress = "https://localhost:7214/api/ExchangeRates/";

        //В каждом тесте на строчку меньше
        private RestClient? _client;
        private RestClient Client
        {
	        get { return _client ??= new(TestingRestAddress); }
        }

        private static readonly string ValidDate = "2022-09-22";
        private static readonly string InvalidDate = "2022-10-22";
        private static readonly string ValidCurrency = "GBP";
        private static readonly string InvalidCurrency = "RUB";

        [Test]
        public void GetAllByDate_InvalidDate_BadRequest()
        {
	        // arrange
	        RestRequest request = new($"GetAllByDate?date={InvalidDate}", Method.Get);

	        // act
	        RestResponse response = Client.Execute(request);

            // assert
            dynamic array = JsonConvert.DeserializeObject(response.Content);
            Assert.That(array["statusCode"].ToString(), Is.EqualTo(HttpStatusCode.BadRequest.GetHashCode().ToString()));
        }

        [Test]
        public void GetByDateCurrency_InvalidDate_BadRequest()
        {
	        // arrange
	        RestRequest request = new($"GetByDateCurrency?date={InvalidDate}&currencyStr={ValidCurrency}", Method.Get);

	        // act
	        RestResponse response = Client.Execute(request);

	        // assert
	        dynamic array = JsonConvert.DeserializeObject(response.Content);
	        Assert.That(array["statusCode"].ToString(), Is.EqualTo(HttpStatusCode.BadRequest.GetHashCode().ToString()));
        }

        [Test]
        public void GetByDateCurrency_InvalidCurrency_BadRequest()
        {
	        // arrange
	        RestRequest request = new($"GetByDateCurrency?date={ValidDate}&currencyStr={InvalidCurrency}", Method.Get);

	        // act
	        RestResponse response = Client.Execute(request);

	        // assert
	        dynamic array = JsonConvert.DeserializeObject(response.Content);
	        Assert.That(array["statusCode"].ToString(), Is.EqualTo(HttpStatusCode.BadRequest.GetHashCode().ToString()));
        }

        [Test]
        public void GetByDateCurrency_ValidDateValidCurrency_Deserialisable()
        {
            // arrange
            RestRequest request = new($"GetByDateCurrency?date={ValidDate}&currencyStr={ValidCurrency}", Method.Get);

            // act
            RestResponse response = Client.Execute(request);

            // assert
            dynamic array = JsonConvert.DeserializeObject(response.Content);
            var value = array["value"].ToString();
            ExchangeRateInfo? exchangeRateInfo = JsonConvert.DeserializeObject<ExchangeRateInfo>(value);
            Assert.That(exchangeRateInfo != null, Is.EqualTo(true));
        }

    }
}