using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Internal;


namespace WebApiExchangeRates.Tests
{
    [TestFixture]
    public class ExchangeRatesControllerUnitTests
    {
        private const string TestingRestAddress = "https://localhost:7214/api/ExchangeRates/";

        //� ������ ����� �� ������� ������
        private RestClient? _client;
        private RestClient Client
        {
	        get { return _client ??= new(TestingRestAddress); }
        }

        private static readonly string ValidDate = "2022-09-22";
        private static readonly string InvalidDate = "2022-10-22";
        private static readonly string ValidCurrency = "GBR";
        private static readonly string InvalidCurrency = "RUB";

        [Test]
        public void GetAll_InvalidDate_BadRequest()
        {
	        // arrange
	        RestRequest request = new($"GetAll?date={InvalidDate}", Method.Get);

	        // act
	        RestResponse response = Client.Execute(request);

            // assert
            dynamic array = JsonConvert.DeserializeObject(response.Content);
            Assert.That(array["statusCode"].ToString(), Is.EqualTo(HttpStatusCode.BadRequest.GetHashCode().ToString()));
        }

        [Test]
        public void Get_InvalidDate_BadRequest()
        {
	        // arrange
	        RestRequest request = new($"Get?date={InvalidDate}&currencyStr={ValidCurrency}", Method.Get);

	        // act
	        RestResponse response = Client.Execute(request);

	        // assert
	        dynamic array = JsonConvert.DeserializeObject(response.Content);
	        Assert.That(array["statusCode"].ToString(), Is.EqualTo(HttpStatusCode.BadRequest.GetHashCode().ToString()));
        }

        [Test]
        public void Get_InvalidCurrency_BadRequest()
        {
	        // arrange
	        RestRequest request = new($"Get?date={ValidDate}&currencyStr={InvalidCurrency}", Method.Get);

	        // act
	        RestResponse response = Client.Execute(request);

	        // assert
	        dynamic array = JsonConvert.DeserializeObject(response.Content);
	        Assert.That(array["statusCode"].ToString(), Is.EqualTo(HttpStatusCode.BadRequest.GetHashCode().ToString()));
        }

        //�� ������, ����� �� ��������� ��������� �����, ��� ��� ��������� � ����������� �� ������ ���������� 

    }
}