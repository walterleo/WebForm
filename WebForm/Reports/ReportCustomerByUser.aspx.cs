using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using WebForm.BLL_Negocio;

namespace WebForm.Reports
{
    public partial class ReportCustomerByUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userId;
                if (int.TryParse(Request.QueryString["userId"], out userId))
                {
                    RunReport(userId);
                }
				else
				{
					// handle the case without a userId
					// e.g., show message or load a default (empty) data set
					lblMessage.Text = "Please specify a valid userId.";
					lblMessage.Visible = true;
					ReportViewer1.Visible = false;  // or RunReport(defaultId);
				}
			}
        }

		private void RunReport(int userId)
		{
			BLL_customers bll = new BLL_customers();
			DataSet table = bll.GetCustomersByUser(userId);

			ReportViewer1.ProcessingMode = ProcessingMode.Local;
			ReportViewer1.Reset();

			// Ruta del reporte RDLC
			ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportCustomerByUser.rdlc");

			ReportViewer1.LocalReport.DataSources.Clear();

			ReportViewer1.LocalReport.DataSources.Add(
					new Microsoft.Reporting.WebForms.ReportDataSource("DataSet5", table.Tables["DataSet5"])
			);

			ReportViewer1.LocalReport.Refresh();
		}

	}
}
