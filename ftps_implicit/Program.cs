using System;
using System.Net;
using System.IO;

namespace ftps_implicit
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try 
			{
				// List Directory
				string host = "ftp://test.rebex.net";
				FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host);
				request.Credentials = new NetworkCredential("demo", "password");
				request.Method =  WebRequestMethods.Ftp.ListDirectoryDetails;
			
				request.Credentials = new NetworkCredential("demo", "password");

				using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
					Stream responseStream = response.GetResponseStream();

					using(StreamReader reader = new StreamReader(responseStream)) {
						Console.WriteLine(reader.ReadToEnd());
						Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);
					}
				}

				// Download File
				request = (FtpWebRequest)WebRequest.Create(host+"/readme.txt");
				request.Credentials = new NetworkCredential("demo", "password");
				request.Method =  WebRequestMethods.Ftp.DownloadFile;

				using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
					Stream responseStream = response.GetResponseStream();

					using(StreamReader reader = new StreamReader(responseStream)) {
						Console.WriteLine(reader.ReadToEnd());
					}
				}

			} 
			catch (Exception e) 
			{
				Console.WriteLine (e.Message);
			}
		}
	}
}
