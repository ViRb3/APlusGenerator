using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace APlusGenerator
{
	public static class WebFunctions
	{
	    private const string Server = "http://microcast.mcserver.ws/aplus/service.php";
	    private static readonly BetterWebClient WebClient = new BetterWebClient();

		public static string Request(NameValueCollection data)
		{
		    byte[] response;

		    try
		    {
                response = WebClient.UploadValues(Server, "POST", data);
		    }
		    catch (WebException e)
		    {
		        return e.Message;
		    }

			return WebClient.Encoding.GetString(response);
		}
			
		public static void ClearCookies()
		{
			WebClient.CookieJar = new CookieContainer();
		}			
	}

	class BetterWebClient : WebClient
	{
        public BetterWebClient()
	    {
	        this.Encoding = Encoding.UTF8;
	    }

		public CookieContainer CookieJar = new CookieContainer();

		protected override WebRequest GetWebRequest(Uri address)
		{
			HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;

			if (request != null)
			{
				request.Method = "POST";
				request.CookieContainer = CookieJar;
			}

			return request;
		}
	}
}

