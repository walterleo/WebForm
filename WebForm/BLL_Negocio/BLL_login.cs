using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebForm.Objects;

namespace WebForm.BLL_Negocio
{
	public class BLL_login
	{

		public users VerificacionData(users us) {
			BLL_Negocio.BLL_General blg = new BLL_General();
			us.PAS_USE = blg.GetHashSHA256(us.PAS_USE);
			DAL_Datos.DAL_login lg = new DAL_Datos.DAL_login();
			return lg.selectUserlogin(us);

		}

	}
	
}