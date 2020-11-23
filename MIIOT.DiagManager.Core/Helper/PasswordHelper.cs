using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Core
{
    public class PasswordHelper
    {
		/// <summary>
		/// 明文密码=》密文
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Encrypt(string value)
        {
			Crypt c = new Crypt();
			return c.Encrypt(value);
		}


		/// <summary>
		/// 密文=》明文密码
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Decrypt(string value)
		{
			Crypt c = new Crypt();
			return c.Decrypt(value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string TryDecrypt(string value)
		{
            try
            {
				Crypt c = new Crypt();
				return c.Decrypt(value);
			}
            catch
            {
				return string.Empty;
            }
		}
	}

	public class Crypt
	{
		private TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();

		private byte[] TruncateHash(string key, int length)
		{
			SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] bytes = Encoding.Unicode.GetBytes(key);
			byte[] array = sHA1CryptoServiceProvider.ComputeHash(bytes);
			byte[] array2 = new byte[length];
			if (array.Length >= array2.Length)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = array[i];
				}
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i];
				}
			}
			return array2;
		}

		public Crypt()
		{
			this.TripleDes.Key = this.TruncateHash("QYZN", this.TripleDes.KeySize / 8);
			this.TripleDes.IV = this.TruncateHash("", this.TripleDes.BlockSize / 8);
		}

		public string Encrypt(string plaintext)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.Unicode.GetBytes(plaintext);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, this.TripleDes.CreateEncryptor(), CryptoStreamMode.Write);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				result = Convert.ToBase64String(memoryStream.ToArray());
				memoryStream.Dispose();
				cryptoStream.Dispose();
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		public string Decrypt(string encryptedtext)
		{
			string result;
			try
			{
				byte[] array = Convert.FromBase64String(encryptedtext);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, this.TripleDes.CreateDecryptor(), CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				result = Encoding.Unicode.GetString(memoryStream.ToArray());
				memoryStream.Dispose();
				cryptoStream.Dispose();
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}
	}
}
