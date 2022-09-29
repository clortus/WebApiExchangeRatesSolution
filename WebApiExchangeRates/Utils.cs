namespace WebApiExchangeRates
{
	/// <summary>
    /// Утилиты
    /// </summary>
    public static class Utils
    {
	    /// <summary>
        /// Получить случайный Decimal
        /// </summary>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns></returns>
        public static decimal GetRandomDecimal(int min, int max)
        {
            var maxPreparingForDecimal = max * 100;
            var minPreparingForDecimal = min * 100;

            var random = new Random();
            int randomNum = random.Next(minPreparingForDecimal, maxPreparingForDecimal);

            return Convert.ToDecimal(randomNum) / 100;
        }
    }
}
