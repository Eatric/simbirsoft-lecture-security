using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System;
using System.Security.Cryptography;
using System.Text;

namespace TestSimbirsoftVulnerabilities
{
	class Program3
	{
		public static string Salt()
		{
			byte[] salt = new byte[256 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			return Convert.ToBase64String(salt);
		}

		public static string PasswordHash(string pwd, string salt, int iterations)
		{
			var strResult = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: pwd,
				salt: Convert.FromBase64String(salt),
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: iterations,
				numBytesRequested: 512 / 8));

			return strResult;
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Enter password: ");
			var password = Console.ReadLine();
			var salt = Salt();
			var hashResult = PasswordHash(password, salt, 10000);
			Console.WriteLine($"Salt: {salt}");
			Console.WriteLine($"PasswordHash: {hashResult}");
		}
	}
}
