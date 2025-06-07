using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.Objects;

namespace WebForm.Presentation
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			LoginUser();
		}

		private void LoginUser() { 
			Objects.users us = new Objects.users();
			us.NAM_USE = txtUser.Text;
			us.PAS_USE = txtPassword.Text;
			BLL_Negocio.BLL_login bl = new BLL_Negocio.BLL_login();
			us = bl.VerificacionData(us);
			if (!us.error)
			{
				// ✅ Login exitoso
				popup("Bienvenido, " + us.NAM_USE);
				// Opcional: redirigir a otra página
				Response.Cookies.Add(setCookie(us));
				CreateSession(us);

				// Luego redirige
				Response.Redirect("User.aspx");
			}
			else
			{
				// ❌ Login fallido
				popup(us.message);  // Muestra el mensaje: "No existen Datos" o error
			}
		}

		private void CreateSession(users us)
		{
			Session["ID_USE_SESSION"] = us.ID_USE;
			Session["NAM_USE_SESSION"] = us.NAM_USE;
		}

		private HttpCookie setCookie(users us)
		{
			HttpCookie cookie = new HttpCookie("UserAuth");
			cookie.Values["UserID"] = Convert.ToString(us.ID_USE);
			cookie.Values["UserName"] = us.NAM_USE;
			cookie.Expires = DateTime.Now.AddDays(1);
     return cookie;

		}

		private void popup(string mensaje)
		{
			string script = $"alert('{mensaje}')";
			ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
		}



	}
}