namespace KFitServer.BusinessLogic.Helpers
{
	public class BCryptCreator : IHashCreator
	{
		public string GenerateSalt()
		{
			return BCrypt.Net.BCrypt.GenerateSalt();
		}

		/// <summary>
		/// Метод для хеширования данных
		/// </summary>
		/// <param name="data">Данные для хеширования</param>
		/// <param name="salt">Соль для увеличения безопасноти хеширования</param>
		/// <returns>Захешированная строка</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public string Hash(string data, string salt)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(data, nameof(data));
			ArgumentException.ThrowIfNullOrWhiteSpace(salt, nameof(salt));
			return BCrypt.Net.BCrypt.HashPassword(data, salt);
		}

		/// <summary>
		/// Метод для хеширования данных
		/// </summary>
		/// <param name="data">Данные для хеширования</param>
		/// <returns>Захешированная строка</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public string Hash(string data)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(data, nameof(data));
			return BCrypt.Net.BCrypt.HashPassword(data);
		}
	}
}
