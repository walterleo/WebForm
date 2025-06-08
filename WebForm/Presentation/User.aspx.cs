using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebForm.Objects;

namespace WebForm.Presentation
{
	public partial class User : System.Web.UI.Page
	{

		private List<users> AllUsers
		{
			get => Session["AllUsers"] as List<users>;
			set => Session["AllUsers"] = value;
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack) {
				var bl = new BLL_Negocio.BLL_users();
				var list = bl.GetListUsers();
				AllUsers = list;                  // la guardo en Session
				gvUser.DataSource = list;         // relleno la grilla
				gvUser.DataBind();

				UpdateDataGrid();
				//VerificationCookie();
				//VerificationSession();
			}

		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			// 1) obtengo texto del TextBox (tienes que crear txtBuscarCedula en el .aspx)
			string filtro = txtBuscarCedula.Text.Trim();

			// 2) filtro la lista en memoria
			var filtrados = (AllUsers ?? new List<users>())
					.Where(u => u.IDC_USE.StartsWith(filtro))
					.ToList();

			// 3) rebindeo la grilla
			gvUser.DataSource = filtrados;
			gvUser.DataBind();
		}

		private void UpdateDataGrid()
		{
			BLL_Negocio.BLL_users bl = new BLL_Negocio.BLL_users();
			List<users> lusers = bl.GetListUsers();
			gvUser.DataSource = lusers;
			gvUser.DataBind();
		}

		private void VerificationSession()
		{
			if (Session["ID_USE_SESSION"] == null || Session["NAM_USE_SESSION"] == null)
			{
				// Si no existe sesión, redirige al login
				Response.Redirect("Login.aspx");
			}
			else
			{
				// Opcional: puedes mostrar el nombre del usuario
				string nombre = Session["NAM_USE_SESSION"].ToString();
				// Ejemplo: lblWelcome.Text = "Hola, " + nombre;
			}
		}

		

		private void VerificationCookie()
		{
			HttpCookie cookie = Request.Cookies["UserAuth"]; 
			if (cookie == null)
			{
				Response.Redirect("Login.aspx");
			}
		}

		protected void btnSave_Click(object sender, EventArgs e) {

			saveNewUser();
	
		}

		


		private void saveNewUser()
		{

			Objects.users us = new Objects.users();

			if (NAM_USE != null && !string.IsNullOrWhiteSpace(NAM_USE.Text))
			{
				us.NAM_USE = NAM_USE.Text.Trim();
			}
			else
			{
				popup("El campo 'Nombre' es obligatorio.");
				us.error = true;
				
			}

			if (LAS_USE != null && !string.IsNullOrWhiteSpace(LAS_USE.Text))
			{
				us.LAS_USE = LAS_USE.Text.Trim();
			}
			else
			{
				popup("El campo 'Apellido' es obligatorio.");
				us.error = true;
				
			}

			if (IDC_USE != null && !string.IsNullOrWhiteSpace(IDC_USE.Text))
			{
				us.IDC_USE = IDC_USE.Text.Trim();
			}
			else
			{
				popup("El campo 'Cédula' es obligatorio.");
				us.error = true;
				
			}

			if (ADD_USE != null && !string.IsNullOrWhiteSpace(ADD_USE.Text))
			{
				us.ADD_USE = ADD_USE.Text.Trim();
			}
			else
			{
				popup("El campo 'Dirección' es obligatorio.");
				us.error = true;
				
			}

			if (PAS_USE != null && !string.IsNullOrWhiteSpace(PAS_USE.Text))
			{
				us.PAS_USE = PAS_USE.Text.Trim();
			}
			else
			{
				popup("El campo 'Contraseña' es obligatorio.");
				us.error = true;
				
			}

			BLL_Negocio.BLL_users blus = new BLL_Negocio.BLL_users();


			us = blus.SaveNewUSers(us);

			popup(Convert.ToString(us.error));
			
			if (!us.error)
			{
				popup("Usuario guardado correctamente.");
			}
			else
			{
				popup("Error al guardar el usuario: " + us.message);
			}

		}

		private void popup(string mensaje)
		{
			string script = $"alert('{mensaje}')";
			ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
		}


		protected void btnLogout_Click(object sender, EventArgs e)
		{
			deleteCookie();

		}

		private void deleteCookie()
		{
			if (Request.Cookies["UserAuth"] != null)
			{
				HttpCookie cookie = new HttpCookie("UserAuth");
				cookie.Expires = DateTime.Now.AddDays(-1); // Eliminar
				Response.Cookies.Add(cookie);
			}

			Session.Clear();       
			Session.Abandon();     

			Response.Redirect("Login.aspx");
		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectRow();
		}

		private void SelectRow()
		{
			// Obtener la fila seleccionada
			GridViewRow row = gvUser.SelectedRow;

			// Asegúrate de que haya una fila seleccionada
			
				// Ejemplo: Obtener valores de las celdas
				string id = row.Cells[1].Text; // Primera celda (ID)
				string nombre = row.Cells[2].Text;
				string apellido = row.Cells[3].Text;
				string cedula = row.Cells[4].Text;// Segunda celda (Nombre)

				// Ejemplo: Mostrar en un Label (opcional)
				ID_USE.Text = id;
				NAM_USE.Text =nombre;
				LAS_USE.Text = apellido;
				IDC_USE.Text = cedula;

				
			
		}

		protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			DeletingRow(e);
		}

		private void DeletingRow(GridViewDeleteEventArgs e)
		{
			int id = Convert.ToInt32(gvUser.DataKeys[e.RowIndex].Value);

			BLL_Negocio.BLL_users blus = new BLL_Negocio.BLL_users();
			users userToDelete = new users { ID_USE = id };
			var result = blus.DeleteUser(userToDelete);

			if (result != null && !result.error)
			{
				popup("✅ Usuario eliminado correctamente. " + result.message);
				UpdateDataGrid();
			}
			else
			{
				popup("❌ Error al eliminar el usuario. " + result?.message);
			}

		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			UpdateUser();
		}

		private void UpdateUser()
		{
			Objects.users us = new Objects.users();
			us.ID_USE = Convert.ToInt32(ID_USE.Text);
			us.NAM_USE = NAM_USE.Text;
			us.LAS_USE = LAS_USE.Text;
			us.IDC_USE = IDC_USE.Text;

			BLL_Negocio.BLL_users blus = new BLL_Negocio.BLL_users();
			us = blus.UpdateUser(us);

			if (us.error)
			{
				popup("Error Update");
			}
			else
			{
				popup("Update ok! " + us.message);
				UpdateDataGrid();
			}
		}



	}
}