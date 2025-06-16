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
            }
        }

        private void RunReport(int userId)
        {
            BLL_customers bll = new BLL_customers();
            DataTable table = bll.GetCustomersByUser(userId);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", table));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}
