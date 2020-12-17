using System;
using System.Security.Cryptography;
using System.Text;

namespace TestSimbirsoftVulnerabilities
{
	class Program
	{
		public static string Salt()
		{
			var stringBuilder = new StringBuilder();
			var rand = new Random();

			for (var i = 0; i < 8; i++)
			{
				var isBigSymbol = rand.Next(0, 2);
				var symbol = rand.Next(0, 26);

				stringBuilder.Append((char)(
					isBigSymbol == 1 ? (65 + symbol) : (97 + symbol)));
			}

			return stringBuilder.ToString();
		}

		public static string PasswordHash(string data, string salt)
		{
			var strResult = string.Empty;

			using (var md5 = new MD5CryptoServiceProvider())
			{
				var hashData = Encoding.UTF8.GetBytes(data + salt);
				var hashDataEnc = md5.ComputeHash(hashData);

				strResult = Convert.ToBase64String(hashDataEnc);
			}

			return strResult;
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Enter password: ");
			var password = Console.ReadLine();
			var salt = Salt();
			var hashResult = PasswordHash(password, salt);
			Console.WriteLine($"Salt: {salt}");
			Console.WriteLine($"PasswordHash: {hashResult}");
		}
	}
}
