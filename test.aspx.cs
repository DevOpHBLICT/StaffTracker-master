using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    //protected public  CostCentre_SelectedIndexChanged(object sender, EventArgs e)


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strCon = System.Web
                      .Configuration
                      .WebConfigurationManager
                      .ConnectionStrings["LocalityConn"].ConnectionString;

            SqlDataSource SqlDataSource6 = new SqlDataSource();
            SqlDataSource6.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //    SELECT* FROM[TimeSheets] WHERE([names] = @names) and convert(date, act_Date,103) >= DATEADD(day, -7, GETDATE()) order by convert(date, act_Date, 103) desc
            SqlDataSource6.SelectCommand = "SELECT * from Status";
            //Open your connection


           

           
            var dropdown = (DropDownList)e.Row.FindControl("DropDownList1");
            //Set up your databinding

            dropdown.DataSource = SqlDataSource6;
            dropdown.DataTextField = "Status";
            dropdown.DataValueField = "Code";
            dropdown.DataBind();
            dropdown.Items.Insert(0,new ListItem("Please select"));
        }
    }

    protected void CostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Your ID to use
        string id = CostCentre.SelectedValue;

        //Call your stored procedure here

        string strCon = System.Web
                   .Configuration
                   .WebConfigurationManager
                   .ConnectionStrings["LocalityConn"].ConnectionString;


        //Use a using statement to handle your SQL calls (it's much safer)
        using (SqlConnection sqlConnection = new SqlConnection(strCon))
        {
            //Your SQL Command
            SqlCommand cmd = new SqlCommand("select * from staff where CostCentre='" + CostCentre.SelectedValue + "';", sqlConnection);

            //Open your connection
     
            sqlConnection.Open();

            //Set up your databinding
            GridView1.DataSource = cmd.ExecuteReader();

            //Bind your data
            GridView1.DataBind();

            sqlConnection.Close();

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string i;
        Object j;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                 i = row.Cells[1].Text;
                var ddl = row.Cells[3].FindControl("dropdownlist1") as DropDownList;
                i = ddl.SelectedValue;
            }
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {

    }
}