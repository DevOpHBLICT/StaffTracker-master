using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Net.Mime;
using System.Drawing;

public partial class _Default : Page
{
    string currentId = "";
    decimal subTotal = 0;

    decimal total = 0;

    int subTotalRowIndex = 0;
   
    protected void itemSelected(object sender, EventArgs e)
    {

   
    }


   
  
    protected void Button1_Click(object sender, EventArgs e)
    {




    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");



    }

    protected void btntoExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        String dt = DateTime.Now.ToString();
        String d = "attachment;filename=" + "Staff Availability " + dt + ".xls";

        Response.AddHeader("content-disposition", d);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }


    protected void grv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {


        }
    }
    public void TextBox1_TextChanged(object sender, EventArgs e)
    {
      

        SqlDataSource SqlDataSource6 = new SqlDataSource();

        //    SqlDataSource1.ID = "SqlDataSource5";

        this.Page.Controls.Add(SqlDataSource6);
        SqlDataSource6.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    SELECT* FROM[TimeSheets] WHERE([names] = @names) and convert(date, act_Date,103) >= DATEADD(day, -7, GETDATE()) order by convert(date, act_Date, 103) desc
   //     String sql = " SELECT  * FROM (SELECT Status, c.CostCentreName FROM Availabilities a, CostCentre c where a.SMTCode = c.CostCentreCode and  convert(date,date,103) =convert(date,'" + Entry_Date.Text + "',103)  ) t PIVOT(COUNT(Status) FOR  Status IN( [A], [B], [Ci], [Cii],[Di],[Dii],[E], [F], [G],[H])   ) AS pivot_table ;";
       

string sql="select c.costcentrename as FunctionalArea, a.Name, s.Status from Availabilities a inner join costcentre c on a.costcentrecode = c.costcentrecode inner join status s on a.status = s.code where convert(date, date,103) = convert(date, '" + Entry_Date.Text + "', 103)   order by 1,2";

        SqlDataSource6.SelectCommand = sql;
        GridView1.DataSource = SqlDataSource6;



        GridView1.DataBind();

    }


    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

    
     
     

    }









    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        String dt = DateTime.Now.ToString();
        String d= "attachment;filename=" + "Staff Availability " + dt + ".xls";
        Response.AddHeader("content-disposition", d);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView1.AllowPaging = false;
            GridView1.DataBind();

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

   


    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
           



          



        
     


            }








        }


        







    }









    

