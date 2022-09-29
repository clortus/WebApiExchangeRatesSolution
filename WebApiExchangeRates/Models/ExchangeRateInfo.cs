using WebApiExchangeRates.Controllers;

namespace WebApiExchangeRates.Models
{
	/// <summary>
    /// Информация о курсе обмена
    /// </summary>
    public class ExchangeRateInfo
    {
        /// <summary>
        /// Конструктор нформации о курсе обмена
        /// </summary>
        /// <param name="currencyType">Валюта</param>
        /// <param name="dateTime">Дата</param>
        public ExchangeRateInfo(Currency currencyType, DateTime dateTime)
        {
            CurrencyType = currencyType;
            DateTime = dateTime;
            Price = GeneratePrice(CurrencyType);            
        }
        /// <summary>
        /// Тип валюты
        /// </summary>
        public Currency CurrencyType { get; set; }

        /// <summary>
        /// Наименование типа валюты
        /// </summary>
        public string CurrencyName 
        {
            get 
            {
                return CurrencyType.ToString();
            }
        }

        /// <summary>
        /// Цена на валюту
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата актуальности цены на валюту
        /// </summary>
        public DateTime DateTime { get; set; }

        //ToDo вероятно стоит хранить значения для генератора отдельно
        private static decimal GeneratePrice(Currency currency)
        {
	        if (currency == Currency.None)
                throw new ArgumentNullException(nameof(currency));
	        return currency switch
	        {
		        Currency.USD => Utils.GetRandomDecimal(100, 200),
		        Currency.EUR => Utils.GetRandomDecimal(200, 300),
		        Currency.GBP => Utils.GetRandomDecimal(300, 400),
		        _ => throw new NotImplementedException("Unsupported currencyType")
	        };
        }
    }
}
