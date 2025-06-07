using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebForm.BLL_Negocio
{
	public class BLL_General
	{
		public string GetHashSHA256(string texto)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(texto);
				byte[] hashBytes = sha256.ComputeHash(bytes);
				return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
			}
		}

	}
}