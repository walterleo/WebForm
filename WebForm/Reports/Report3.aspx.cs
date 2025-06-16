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
	public partial class Report3 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				RunReport();
			}

		}

		protected DataSet LlenarGrid()
		{
			DataSet ds = new DataSet();
			string cadenaConexion = "Server=127.0.0.1;Port=3306;Database=market1234;Uid=root;password=;";
			string sql = "SELECT USER_AFFECTED, FIELD_CHANGED, OLD_VALUE, NEW_VALUE, ACTION_TYPE, CHANGED_BY, CHANGE_DATE FROM auditor_users";

			using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
			{
				MySqlDataAdapter adaptador = new MySqlDataAdapter(sql, conexion);


				adaptador.Fill(ds, "DataSet4");


			}

			return ds;

		}

		protected void RunReport()
		{
			ReportViewer1.LocalReport.DataSources.Clear();
			ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet4", LlenarGrid().Tables["DataSet4"]));

			ReportViewer1.LocalReport.Refresh();

		}

	}
}