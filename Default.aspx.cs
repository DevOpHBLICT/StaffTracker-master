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


   
   protected void Save_Data()
    {
        var ok = 1;
        var project_code = "";
       
        Error_Label.Visible=false;
        var Error_Text="Please Enter:";
      

       if (Staff.SelectedValue== "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Name,"; } 
       if (Entry_Date.Text == "") { ok = 0; Error_Label.Visible = true;Error_Text=Error_Text+"Date,"; }
     if (Status.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Status,"; }
       if (Directorate.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Directorate,"; }
        if (CostCentre.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Cost Centre,"; }


        string strCon = System.Web
                   .Configuration
                   .WebConfigurationManager
                   .ConnectionStrings["LocalityConn"].ConnectionString;
             string sql = "select SMT from Staff where  CurrentStaff ='Y' and Name='" +  Staff.SelectedValue +   "' and CostCentre='" + CostCentre.SelectedValue  + "' order by Name";
     //   string sql = "select * from Staff where  Name='" +  Staff.SelectedValue +   "' and CostCentre='" + CostCentre.SelectedValue  + "' order by Name";

        SqlConnection conn = new SqlConnection(strCon);
        SqlCommand comm = new SqlCommand(sql, conn);
        conn.Open();
        string smt = (string)comm.ExecuteScalar();


       strCon = System.Web
                 .Configuration
                 .WebConfigurationManager
                 .ConnectionStrings["LocalityConn"].ConnectionString;
        //If SMT selected, get the cost centre for the SMT staff.
        



        sql = "select CostCentre from Staff where CurrentStaff ='Y' and Name='" + Staff.SelectedValue + "'  and CostCentre='" + CostCentre.SelectedValue  + "'  order by Name";
     //   sql = "select CostCentre from Staff where  Name='" + Staff.SelectedValue + "'  and CostCentre='" + CostCentre.SelectedValue + "'  order by Name";

        conn = new SqlConnection(strCon);
       comm = new SqlCommand(sql, conn);
        conn.Open();
     string  cc = (string)comm.ExecuteScalar();



        if (smt == "Y")
        {
            smt = "999999";

        }
        else
        {
            smt= CostCentre.SelectedValue;
        }

        //     cc = CostCentre.SelectedValue;
        //  }
        if (CostCentre.SelectedValue == "999999")
        {
            sql = "select CostCentre from Staff where CurrentStaff ='Y' and Name='" + Staff.SelectedValue + "'";
            SqlConnection conn2 = new SqlConnection(strCon);
            SqlCommand comm2 = new SqlCommand(sql, conn2);
            conn2.Open();
      cc = (string)comm2.ExecuteScalar();
            conn2.Close();
        }
        Error_Label.Text = Error_Text.TrimEnd(',');
     
       if (ok ==1)
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalityConn"].ConnectionString;
            cnn.Open();

            DateTime activity_date = Convert.ToDateTime(Entry_Date.Text);
            DateTime logged_date = Convert.ToDateTime(DateTime.Now);

            var pc = project_code;
            //    var pc_desc = Project.SelectedValue;
           
            sql = "";
            sql = sql + "insert into  Availabilities(name,date,smtcode,costcentrecode,directorate,status";
            
             sql = sql + ")";
            sql = sql + "values(";
            sql = sql + "'" + Staff.SelectedValue + "',";
         
        sql = sql + "'" + Entry_Date.Text+ "',";
            //     Session["Date"] =Entry_Date.Text;
            sql = sql + "'" + smt + "',";
            sql = sql + "'" + cc + "',";
       // Session["CostCentre"] = CostCentre.SelectedValue;

            sql = sql + "'" + Directorate.SelectedValue + "',";
         // Session["Directorate"] = Directorate.SelectedValue;

            sql = sql + "'" + Status.SelectedValue + "'";

            sql = sql + ")";
         
        


            SqlCommand cmd2 = new SqlCommand(sql, cnn);


            try
            {
                cmd2.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                

            }

                    Response.Redirect("default.aspx");

            cnn.Close();
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

    public void TextBox1_TextChanged(object sender, EventArgs e)
    {
      

            SqlDataSource SqlDataSource5 = new SqlDataSource();

        //    SqlDataSource1.ID = "SqlDataSource5";

            this.Page.Controls.Add(SqlDataSource5);

            SqlDataSource5.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //    SELECT* FROM[TimeSheets] WHERE([names] = @names) and convert(date, act_Date,103) >= DATEADD(day, -7, GETDATE()) order by convert(date, act_Date, 103) desc
            SqlDataSource5.SelectCommand = "select a.id,Date, a.Name,CostCentreCode as 'Cost Centre',d.Directorate,Status from Availabilities a,Directorate d where a.Directorate = d.ID and [SMTCode] = '" +  CostCentre.SelectedValue+ "' and convert(date,date,103) =convert(date,'" + Entry_Date.Text + "',103)" +   " order by convert(date,date,103) desc";

            GridView1.DataSource = SqlDataSource5;

            GridView1.DataBind();

       

    }


    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

    
     
     

    }





    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Data.DataRowView drv = e.Row.DataItem as System.Data.DataRowView;
        //    e.Row.Attributes.Add("ondblclick", String.Format("window.location='Edit_Availability.aspx?id={0}'", drv["id"]));
        }
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
                SqlCommand cmd = new SqlCommand("select * from CostCentre where DirectorateCode ='" + id + "' ;", sqlConnection);

                //Open your connection
                sqlConnection.Open();

                //Set up your databinding
                CostCentre.DataSource = cmd.ExecuteReader();
                CostCentre.DataTextField = "CostCentreName";
                CostCentre.DataValueField = "CostCentreCode";
            //    CostCentre.Items.Insert(0, "--Please Select--");
              //  CostCentre.Items.Insert(1, "N/A");
                //Bind your data
                CostCentre.DataBind();

                sqlConnection.Close();

            }






            if (!String.IsNullOrEmpty(Session["CostCentre"] as string))
            {

                String k = Session["CostCentre"].ToString();
CostCentre.SelectedIndex =
               CostCentre.Items.IndexOf(CostCentre.Items.FindByValue(k));
       //       CostCentre.DataBind();
            }



            //Your ID to use
            id = CostCentre.SelectedValue;

            //Call your stored procedure here

           strCon = System.Web
                       .Configuration
                       .WebConfigurationManager
                       .ConnectionStrings["LocalityConn"].ConnectionString;


            //Use a using statement to handle your SQL calls (it's much safer)
            using (SqlConnection sqlConnection = new SqlConnection(strCon))
            {
                //Your SQL Command
                string sql = "";
                if (CostCentre.SelectedValue == "999999")
                {
          //  sql = "select * from Staff where CurrentStaff='Y' and SMT ='Y' order by Name;";
          sql = "select * from Staff a where CurrentStaff='Y' and SMT ='Y'  and not exists(select 1 from Availabilities b where a.Name= b.Name and b.Date= '" + Entry_Date.Text + "') order by Name;";

                }

                else
                {
          // sql = "select * from Staff where CurrentStaff='Y' and CostCentre ='" + id + "' and SMT ='N' order by Name;";
         sql = "select * from Staff a where CurrentStaff='Y' and CostCentre ='" + id + "' and SMT ='N'  and not exists(select 1 from Availabilities b where a.Name= b.Name and b.Date= '" + Entry_Date.Text + "') order by Name;";

                }




                SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                //Open your connection
                sqlConnection.Open();

                //Set up your databinding
                Staff.DataSource = cmd.ExecuteReader();
                Staff.DataTextField = "Name";
                Staff.DataValueField = "Name";
          //      Staff.Items.Insert(0, "--Please Select--");
                //Bind your data
                Staff.DataBind();

                sqlConnection.Close();



                SqlDataSource SqlDataSource5 = new SqlDataSource();

                //    SqlDataSource1.ID = "SqlDataSource5";

                this.Page.Controls.Add(SqlDataSource5);

                SqlDataSource5.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //    SELECT* FROM[TimeSheets] WHERE([names] = @names) and convert(date, act_Date,103) >= DATEADD(day, -7, GETDATE()) order by convert(date, act_Date, 103) desc
                SqlDataSource5.SelectCommand = "select Date, a.Name,CostCentreCode as 'Cost Centre',d.Directorate,Status from Availabilities a,Directorate d where a.Directorate = d.ID and [SMTCode] = '" + CostCentre.SelectedValue + "' and convert(date,date,103) =convert(date,'" + Entry_Date.Text + "',103)" + " order by convert(date,date,103) desc";

                GridView1.DataSource = SqlDataSource5;

                GridView1.DataBind();




            }








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

    
        GridView1.DataBind();
    }
    protected void Staff_DropDown()
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

            string sql = "";
            if(CostCentre.SelectedValue=="999999")
            {
                sql = "select * from Staff where SMT ='Y' order by Name;";
            }else
            {
              sql=  "select * from Staff where CostCentre ='" + id + "' and SMT ='N' order by Name;";
            }

            //Your SQL Command
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);

            //Open your connection
            sqlConnection.Open();

            //Set up your databinding
            Staff.DataSource = cmd.ExecuteReader();
           Staff.DataTextField = "Name";
            Staff.DataValueField = "Name";
            Staff.Items.Insert(0, "--Please Select--");
             //Bind your data
            Staff.DataBind();

            sqlConnection.Close();

        }


        SqlDataSource SqlDataSource5 = new SqlDataSource();

        //    SqlDataSource1.ID = "SqlDataSource5";

        this.Page.Controls.Add(SqlDataSource5);

        SqlDataSource5.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    SELECT* FROM[TimeSheets] WHERE([names] = @names) and convert(date, act_Date,103) >= DATEADD(day, -7, GETDATE()) order by convert(date, act_Date, 103) desc
        SqlDataSource5.SelectCommand = "select Date, a.Name,CostCentreCode as 'Cost Centre',d.Directorate,Status from Availabilities a,Directorate d where a.Directorate = d.ID and [SMTCode] = '" + CostCentre.SelectedValue + "' and convert(date,date,103) =convert(date,'" + Entry_Date.Text + "',103)" + " order by convert(date,date,103) desc";
        GridView1.DataSource = SqlDataSource5;

        GridView1.DataBind();
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
        Staff.Items.Clear();
        Staff_DropDown();
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



    

