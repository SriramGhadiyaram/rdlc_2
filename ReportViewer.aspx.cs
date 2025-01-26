using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace rdlc_2
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/EmployeeReport.rdlc");

                    // Set up the data source
                    string connectionString = "Data Source=LAPTOP-7ROD4UTH\\SQLEXPRESS;Initial Catalog=rdlc;Integrated Security=True;";
                    string query = "SELECT * FROM Employees";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            throw new Exception("No data found in the Employees table.");
                        }

                        // Use the correct data source name from the RDLC file
                        ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dataTable);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(reportDataSource);

                        // Refresh the report
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    // Log or display error for debugging
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }
    }
}

