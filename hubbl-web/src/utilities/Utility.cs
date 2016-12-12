using System;
using System.Security.Cryptography;
using System.Text;

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
		public static string sha256(string input) {
			SHA256 sha256 = System.Security.Cryptography.SHA256Managed.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = sha256.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("X2"));

			return sb.ToString();
		}

		private static Random random = new Random((int) DateTime.Now.Ticks);

		public static string randomSalt() {
			StringBuilder sb = new StringBuilder();
        	char ch;
        	for (int i = 0; i < 16; i++) {
            	ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            	sb.Append(ch);
        	}

        	return builder.ToString();
		}

	}
}
