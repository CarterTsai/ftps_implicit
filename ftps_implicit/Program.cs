using System;
using AlexPilotti.FTPS.Client;
using AlexPilotti.FTPS.Common;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ftps_implicit
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try {
				using (FTPSClient client = new FTPSClient())
				{
					string host = "test.rebex.net";
					int port = 990;

					NetworkCredential credential = null;
					credential = new NetworkCredential("demo", "password");

					// 參考其他作法https://social.msdn.microsoft.com/Forums/en-US/56a10641-4504-4f8b-8434-86156f8104be/how-to-accept-ssl-certificate-of-ftps-server?forum=netfxnetcom
					// The path to the certificate.
					string Certificate = "certificate.cer";

					// Load the certificate into an X509Certificate object.
					X509Certificate cert = new X509Certificate(Certificate);

					client.Connect(host, 
						port,
						credential, 
						ESSLSupportMode.Implicit,
						new RemoteCertificateValidationCallback(ValidateTestServerCertificate),
						cert,
						0,
						0,
						0,
						1000
					);

					// Download a file
					var list = client.GetDirectoryList();
					Console.WriteLine (list[1].Name);
					client.GetFile(list[1].Name, "/tmp/readme.txt");
					client.Close();
				}
			} catch (Exception e) {
				Console.WriteLine (e.Message);
			}
		}

		private static bool ValidateTestServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
//			// If the certificate is a valid, signed certificate, return true.
//			if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
//			{
//				return true;
//			}
//
//			// If there are errors in the certificate chain, look at each error to determine the cause.
//			if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
//			{
//				if (chain != null && chain.ChainStatus != null)
//				{
//					foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
//					{
//						if ((certificate.Subject == certificate.Issuer) &&
//							(status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
//						{
//							// Self-signed certificates with an untrusted root are valid. 
//							continue;
//						}
//						else
//						{
//							if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
//							{
//								// If there are any other errors in the certificate chain, the certificate is invalid,
//								// so the method returns false.
//								return false;
//							}
//						}
//					}
//				}
//
//				// When processing reaches this line, the only errors in the certificate chain are 
//				// untrusted root errors for self-signed certificates. These certificates are valid
//				// for default Exchange server installations, so return true.
//				return true;
//			}
//			else
//			{
//				// In all other cases, return false.
//				return false;
//			}
		}

//		private static bool ValidateTestServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
//		{
//			bool certOk = false;
//
//			if (sslPolicyErrors == SslPolicyErrors.None) {
//				certOk = true;
//			}
//			else
//			{
//				Console.Error.WriteLine();
//
//				if((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) > 0)
//					Console.Error.WriteLine("WARNING: SSL/TLS remote certificate chain errors");
//
//				if((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) > 0)
//					Console.Error.WriteLine("WARNING: SSL/TLS remote certificate name mismatch");
//
//				if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) > 0)
//					Console.Error.WriteLine("WARNING: SSL/TLS remote certificate not available");                
//
//				certOk = true;
//			}
//				
//			return certOk;
//		}
	}
}
