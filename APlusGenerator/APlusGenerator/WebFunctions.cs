using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace APlusGenerator
{
	public static class WebFunctions
	{
	    private const string Server = "http://microcast.mcserver.ws/aplus/service.php";
	    private static readonly BetterWebClient WebClient = new BetterWebClient();
		private static Cookie _signedInCookie;

		public static string Request(NameValueCollection data)
		{
			byte[] response = WebClient.UploadValues(Server, "POST", data);
			string result = WebClient.Encoding.GetString(response);

			if (result != "Login success!")
				return result;

			if (_signedInCookie == null)
				_signedInCookie = WebClient.CookieJar.GetCookies(new Uri(Server)).Cast<Cookie>().FirstOrDefault(c => c.Name == "signedUser");

			if (_signedInCookie == null)
                throw new Exception("Unable to load keep signed in cookie");

			return result;
		}
			
		public static void ClearCookies()
		{
			_signedInCookie = null;
			WebClient.CookieJar = new CookieContainer ();
		}			
	}

	class BetterWebClient : WebClient
	{
		public CookieContainer CookieJar = new CookieContainer();

		protected override WebRequest GetWebRequest(Uri address)
		{
			HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;

			if (request != null)
			{
				request.Method = "Post";
				request.CookieContainer = CookieJar;
			}

			return request;
		}
	}
}

