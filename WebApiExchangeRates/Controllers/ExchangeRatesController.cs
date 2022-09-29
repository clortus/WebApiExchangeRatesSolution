using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WebApiExchangeRates.Models;

namespace WebApiExchangeRates.Controllers
{
    /// <summary>
    /// Контроллер для получения курсов валют
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private static readonly Currency[] Currencies = new Currency[]{Currency.USD, Currency.EUR, Currency.GBP};
        private static readonly DateTime MaxDate = new(2022, 09, 28);
        private static readonly DateTime MinDate = new(2022, 09, 01);


        /// <summary>
        /// Данные о курсе валюты
        /// </summary>
        /// <param name="date">Дата, на которую нужно получить курс</param>
        /// <param name="currencyStr">Валюта, на которую необходимо получить курс</param>
        /// <returns>
        /// Вернет сериализованный ExchangeRateInfo в JsonResult.value
        /// </returns>
        [HttpGet]
        public JsonResult GetByDateCurrency(DateTime date, string currencyStr)
        {
            if(!DateIsValid(date))
                return new(BadRequest(date));
            var currencyEnum = Currencies.FirstOrDefault(с => с.ToString() == currencyStr.ToUpper());
            if(currencyEnum == Currency.None)
                return new(BadRequest(currencyStr));
            ExchangeRateInfo exchangeRateInfo = new(currencyEnum, date);
            return new(Ok(exchangeRateInfo));
        }

        /// <summary>
        /// Данные всех доступные валюты
        /// </summary>
        /// <param name="date">Дата, на которую нужно получить курс</param>
        /// <returns>
        /// Вернет массив сериализованных ExchangeRateInfo в JsonResult.value
        /// </returns>
        [HttpGet]
        public JsonResult GetAllByDate(DateTime date)
        {
            if (!DateIsValid(date))
                return new(BadRequest(date));
            List<ExchangeRateInfo> resultList = new();
            foreach (var currency in Currencies)
                resultList.Add(new(currency, date));
            return new(Ok(resultList));
        }

        private static bool DateIsValid(DateTime dateToCheck)
        {
            return dateToCheck >= MinDate && dateToCheck < MaxDate;
        }
    }
    /// <summary>
    /// Виды валют
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Currency
    {
        /// <summary>
        /// Отсутсвует (значение по умолчанию)
        /// </summary>
        None = 0,
        /// <summary>
        /// Доллар
        /// </summary>
        USD = 1,
        /// <summary>
        /// Евро
        /// </summary>
        EUR = 2,
        /// <summary>
        /// Киргизский сом
        /// </summary>
        GBP = 3
    }
}
