using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace APlusGenerator
{
	public static class Functions
	{

		public static bool IsLoggedIn()
		{
			var data = new NameValueCollection();
			data.Add("login", string.Empty);

			if (WebFunctions.Request (data) == "Already logged in!")
				return true;

			return false;	
		}

		public static string GetSha256(string input)
		{
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte @byte in hash)
            {
                stringBuilder.Append(@byte.ToString("x2"));
            }

            return stringBuilder.ToString();
		}

		public static string[] TrimArray(this string[] array)
		{
			List<string> listArray = new List<string> (array);

			while(string.IsNullOrWhiteSpace(listArray[listArray.Count - 1]))
				listArray.RemoveAt(listArray.Count - 1);

			return listArray.ToArray();
		}
	} 
}