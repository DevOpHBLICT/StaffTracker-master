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

    public void TextBox1_TextChanged(object sender, EventArgs e)
    {


        SqlDataSource SqlDataSource5 = new SqlDataSource();

        //    SqlDataSource1.ID = "SqlDataSource5";

        this.Page.Controls.Add(SqlDataSource5);

        string sql = "";
        string id = SubCostCode.SelectedValue;
        if (id.Length > 0)
        {


            sql = "select * from Staff a where subCostCode='" + SubCostCode.SelectedValue + "' and CurrentStaff !='N' and not exists(select 1 from Availabilities b where a.Name= b.Name and b.Date= '" + Entry_Date.Text + "') order by Name;";
        }
        else
        {
            sql = "select * from Staff a where CostCentre='" + CostCentre.SelectedValue + "' and CurrentStaff !='N' and not exists(select 1 from Availabilities b where a.Name= b.Name and b.Date= '" + Entry_Date.Text + "') order by Name;";

        }


        SqlDataSource5.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    SELECT* FROM[TimeSheets] WHERE([names] = @names) and convert(date, act_Date,103) >= DATEADD(day, -7, GETDATE()) order by convert(date, act_Date, 103) desc
        SqlDataSource5.SelectCommand = sql;

        GridView1.DataSource = SqlDataSource5;

        GridView1.DataBind();



    }

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
            dropdown.Items.Insert(0, new ListItem("Please select"));
        }
    }
    protected void SubCostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
       string sql = "";
        string id = SubCostCode.SelectedValue;
        if (id=="Please select"||id.Length<1)
        {
            sql = "select * from Staff a where CostCentre='" + CostCentre.SelectedValue + "' and CurrentStaff !='N' and not exists(select 1 from Availabilities b where a.Name= b.Name and b.Date= '" + Entry_Date.Text + "') order by Name;";

        }else
        {


            sql = "select * from Staff a where subCostCode='" + SubCostCode.SelectedValue + "' and CurrentStaff !='N' and not exists(select 1 from Availabilities b where a.Name= b.Name and b.Date= '" + Entry_Date.Text + "') order by Name;";
        }
       
      
        string strCon = System.Web
                       .Configuration
                       .WebConfigurationManager
                       .ConnectionStrings["LocalityConn"].ConnectionString;


            //Use a using statement to handle your SQL calls (it's much safer)
            using (SqlConnection sqlConnection = new SqlConnection(strCon))
            {
                //Your SQL Command
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                //Open your connection

                sqlConnection.Open();

                //Set up your databinding
                GridView1.DataSource = cmd.ExecuteReader();
                //Bind your data
                GridView1.DataBind();

                sqlConnection.Close();
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
            string sql = "";
            using (SqlConnection sqlConnection = new SqlConnection(strCon))
            {

                sql = "select * from subCostCode where CostCode ='" + id + "'";
                //Your SQL Command
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                //Open your connection
                SubCostCode.DataSource = null;
                sqlConnection.Open();

                //Set up your databinding
                SubCostCode.DataSource = cmd.ExecuteReader();
                //Bind your data
                SubCostCode.DataTextField = "Subname";
                SubCostCode.DataValueField = "subCostCode";
          

            SubCostCode.DataBind();
            SubCostCode.Items.Insert(0, new ListItem("Please select"));

            sqlConnection.Close();
            }

            sql = "";
            if (CostCentre.SelectedValue == "999999")
            {

                sql = "select * from Staff a where SMT ='Y'  and CurrentStaff !='N' and not exists(select 1 from Availabilities b where a.Name= b.Name and b.Date= '" + Entry_Date.Text + "') order by Name;";
            }
            else
            {
                sql = "select * from Staff a where CostCentre ='" + id + "' and SMT ='N' and CurrentStaff !='N' and not exists(select 1 from Availabilities b where a.Name= b.Name and b.Date= '" + Entry_Date.Text + "') order by Name;";
            }



            //Use a using statement to handle your SQL calls (it's much safer)
            using (SqlConnection sqlConnection = new SqlConnection(strCon))
            {
                //Your SQL Command
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                //Open your connection

                sqlConnection.Open();

                //Set up your databinding
                GridView1.DataSource = cmd.ExecuteReader();
                //Bind your data
                GridView1.DataBind();

                sqlConnection.Close();
            }


            strCon = System.Web
                      .Configuration
                      .WebConfigurationManager
                      .ConnectionStrings["LocalityConn"].ConnectionString;



        

    }
    protected void Save_Data()
    {
        var ok = 1;
        var project_code = "";

        Error_Label.Visible = false;
        var Error_Text = "Please Enter:";


        //   if (Staff.SelectedValue== "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Name,"; } 
        if (Entry_Date.Text == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Date,"; }
        //    if (Status.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Status,"; }
        if (Directorate.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Directorate,"; }
        if (CostCentre.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Cost Centre,"; }

        string staff;
        string smt;
        string status;
        string sql;
        string cc;
        Object j;

      //  foreach (GridViewRow row in GridView1.Rows)
      //  {
      //      var ddl = row.Cells[3].FindControl("dropdownlist1") as DropDownList;
      //      status = ddl.SelectedValue;
      //      if (status == "Please select") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Status,"; }
      //  }

        Error_Label.Text = Error_Text.TrimEnd(',');


        if (ok == 1)
        {

            foreach (GridViewRow row in GridView1.Rows)
        {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    cc = row.Cells[0].Text;
                    staff = row.Cells[1].Text;
                    smt = row.Cells[2].Text;
                    var ddl = row.Cells[3].FindControl("dropdownlist1") as DropDownList;

                    status = ddl.SelectedValue;

                    if (status!="Please select")
                    {


                        if (smt == "Y")
                        {
                            smt = "999999";

                        }
                        else
                        {


                            smt = cc;
                        }

                        //     cc = CostCentre.SelectedValue;
                        //  }



                        SqlConnection cnn = new SqlConnection();
                        cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalityConn"].ConnectionString;
                        cnn.Open();

                        DateTime activity_date = Convert.ToDateTime(Entry_Date.Text);
                        DateTime logged_date = Convert.ToDateTime(DateTime.Now);

                        var pc = project_code;

                        sql = "";
                        sql = sql + "insert into  Availabilities(Name,date,smtcode,costcentrecode,directorate,status";

                        sql = sql + ")";
                        sql = sql + "values(";
                        sql = sql + "'" + staff + "',";

                        sql = sql + "'" + Entry_Date.Text + "',";
                        sql = sql + "'" + smt + "',";
                        sql = sql + "'" + cc + "',";

                        sql = sql + "'" + Directorate.SelectedValue + "',";

                        sql = sql + "'" + status + "'";

                        sql = sql + ")";




                        SqlCommand cmd2 = new SqlCommand(sql, cnn);


                        try
                        {
                            cmd2.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {


                        }



                        cnn.Close();
                    }
                }
            }
        }
        if (ok == 1)
        {
            Response.Redirect("default.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        Save_Data();



    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");



    }

    
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {





    }










    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            
            Entry_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");

            SqlDataSource SqlDataSource6 = new SqlDataSource();

            //    SqlDataSource1.ID = "SqlDataSource5";

            this.Page.Controls.Add(SqlDataSource6);
            SqlDataSource6.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //    SELECT* FROM[TimeSheets] WHERE([names] = @names) and convert(date, act_Date,103) >= DATEADD(day, -7, GETDATE()) order by convert(date, act_Date, 103) desc
            SqlDataSource6.SelectCommand = "SELECT * from Directorate;";
            //Open your connection


            //Set up your databinding
            Directorate.DataSource = SqlDataSource6;


            Directorate.DataTextField = "Directorate";
            Directorate.DataValueField = "ID";
            Directorate.DataBind();

            if (!String.IsNullOrEmpty(Session["Directorate"] as string))
            {

                string s = Session["Directorate"].ToString();
                Directorate.SelectedIndex =
               Directorate.Items.IndexOf(Directorate.Items.FindByValue(s));
                //         Directorate.DataBind();
            }

             Directorate.SelectedValue=null;
   
        }

   }

    


    protected void QM_Selecting(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters["@DynamicVariable"].Value = Session["department"];
    }

    protected void Activity_Selecting(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters["@DynamicVariable"].Value = Session["department"];
    }

    protected void Prefer_To_Have_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void CostCentre_DropDown()
    {



        //Your ID to use
     string id = Directorate.SelectedValue;

        //Call your stored procedure here

        string strCon = System.Web
                   .Configuration
                   .WebConfigurationManager
                   .ConnectionStrings["LocalityConn"].ConnectionString;


        //Use a using statement to handle your SQL calls (it's much safer)
        using (SqlConnection sqlConnection = new SqlConnection(strCon))
        {
            //Your SQL Command
            SqlCommand cmd = new SqlCommand("select * from CostCentre where DirectorateCode ='" +id + "' ;", sqlConnection);

            //Open your connection
            sqlConnection.Open();

            //Set up your databinding
        CostCentre.DataSource = cmd.ExecuteReader();
           CostCentre.DataTextField = "CostCentreName";
       CostCentre.DataValueField = "CostCentreCode";
         CostCentre.Items.Insert(0, "--Please Select--");
          CostCentre.Items.Insert(1, "N/A");
            //Bind your data
         CostCentre.DataBind();

            sqlConnection.Close();

        }

    
       // GridView1.DataBind();
    }
    
    protected void DirectorateSelected(object sender, EventArgs e)
    {

        Session["Directorate"] =Directorate.SelectedItem.Value;
        CostCentre.Items.Clear();
        CostCentre_DropDown();
    }



    protected void CostCentreSelected(object sender, EventArgs e)

    {
        Session["CostCentre"] = CostCentre.SelectedItem.Value;
    //    Staff.Items.Clear();
    }




    protected void Project_Selected(object sender, EventArgs e)

    {
 
    }



        protected void ProjectSelecting(object sender, EventArgs e)




    {




        SqlDataSource SqlDataSource6= new SqlDataSource();

     //   Project.DataSource = null;
     //Project.DataBind();
        SqlDataSource6.ID = "SqlDataSource6";

        this.Page.Controls.Add(SqlDataSource6);

        SqlDataSource6.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
  
      
    }
}



    

