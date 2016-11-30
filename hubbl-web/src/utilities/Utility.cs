using System;

namespace hubbl.web {
	
	public class Utility {
		
		public static string md5(string str) {
			
			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
			byte[] hash = md5.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("X2"));

			return sb.ToString();
		}

	}
}

