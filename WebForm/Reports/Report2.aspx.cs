using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Reports
{
	public partial class Report2 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack) {
				RunReport();
			}
			
		}

		protected DataSet LlenarGrid()
		{
			DataSet ds = new DataSet();
			string cadenaConexion = "Server=127.0.0.1;Port=3306;Database=market1234;Uid=root;password=;";
			string sql = "SELECT NAM_USE, LAS_USE, IDC_USE FROM users";

			using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
			{
				MySqlDataAdapter adaptador = new MySqlDataAdapter(sql, conexion);


				adaptador.Fill(ds, "DataSet1");


			}

			return ds;

		}

		protected void RunReport() {
			ReportViewer1.LocalReport.DataSources.Clear();
			ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", LlenarGrid().Tables["DataSet1"]));
	
			ReportViewer1.LocalReport.Refresh();

		}

	}
}