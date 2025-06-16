using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using WebForm.BLL_Negocio;
using System.Linq;

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

        protected void btnRun_Click(object sender, EventArgs e)
        {
            int userId;
            if (int.TryParse(txtUserId.Text, out userId))
            {
                RunReport(userId);
            }
            else
            {
                ShowAvailableIds("Please specify a valid userId.");
            }
        }

        private void RunReport(int userId)
        {
            BLL_customers bll = new BLL_customers();
            DataSet table = bll.GetCustomersByUser(userId);

            if (table.Tables["DataSet5"] == null || table.Tables["DataSet5"].Rows.Count == 0)
            {
                ShowAvailableIds("No data found for the specified userId.");
                return;
            }

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportCustomerByUser.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", table.Tables["DataSet5"]));
            ReportViewer1.LocalReport.Refresh();

            lblMessage.Visible = false;
            ReportViewer1.Visible = true;
        }

        private void ShowAvailableIds(string message)
        {
            BLL_users bllu = new BLL_users();
            var ids = bllu.GetListUsers().Select(u => u.ID_USE.ToString());
            lblMessage.Text = message + " Available IDs: " + string.Join(", ", ids);
            lblMessage.Visible = true;
            ReportViewer1.Visible = false;
        }

	}
}
