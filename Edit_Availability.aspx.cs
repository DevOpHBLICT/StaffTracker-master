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


public partial class _Default : Page
{







    protected void Save_Data()
    {
        var ok = 1;

        Error_Label.Visible = false;
        var Error_Text = "Please Enter:";


        if (Name.Text == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Name,"; }
        if (Entry_Date.Text == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Date,"; }
        if (Client.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Client,"; }
        if (QM.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "QM,"; }
        if (Activity.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Activity,"; }
        if (Project.SelectedValue == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Project,"; }
        if (Duration.Text == "") { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Duration,"; }
        // if ((OtherDescription.Text == "") && (Activity.SelectedValue.Contains("Other"))) { ok = 0; Error_Label.Visible = true; Error_Text = Error_Text + "Other Description,"; }
        var project_code = "";

        string strCon = System.Web
   .Configuration
   .WebConfigurationManager
   .ConnectionStrings["LocalityConn"].ConnectionString;
        string sql = "select Project_ID from ProjectHierarchy where Project_name='" + Project.SelectedValue + "'";
        SqlConnection conn = new SqlConnection(strCon);
        SqlCommand comm = new SqlCommand(sql, conn);
        conn.Open();
        project_code = (String)comm.ExecuteScalar();

        conn.Close();



        if (Project.SelectedValue == "--Please Select--") { project_code = "--Please Select--"; Project.SelectedValue = "N/A"; }
        if (Project.SelectedValue == "N/A") { project_code = "N/A"; }
        if (project_code == "") { project_code = Project.SelectedValue; }

        Error_Label.Text = Error_Text.TrimEnd(',');


        if (ok == 1)
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalityConn"].ConnectionString;
            cnn.Open();


            var pc = project_code;
            var pc_desc = Project.SelectedValue;
            if (OtherDescription.Text.Length > 0)
            {
                pc = OtherDescription.Text;
                pc_desc = OtherDescription.Text;
            }



            sql = "";
            sql = sql + "update  TimeSheets " +
                "set act_date ='" + Entry_Date.Text + "'," +
            //"log_date = '" + DateTime.Now.ToString() + "'," +
            "client = '" + Client.SelectedValue + "'," +
           "project_name ='" + pc_desc + "'," +
           "project_code ='" + pc + "'," +
            "quantifiable_measure='" + QM.SelectedValue + "'," +
            "activity='" + Activity.SelectedValue + "'," +
            "time_spent='" + Duration.Text + "'," +
            "other_description=" + "'" + ActivityOther.Text + "'," +
            "comments='" + Comments.Text.Replace("\"", "").Replace("'", "") + "' where record_id = '" + Request.QueryString["id"].ToString() + "'";




            //     sql = sql + "'" + Trainer2.SelectedValue + "',";
            //       sql = sql + "'" + Trainer_Dropdown.Text.Replace("'","") + "',";
            //     sql = sql + "'" + txtDate.Text.Replace("'","") + "',";




            SqlCommand cmd2 = new SqlCommand(sql, cnn);
            cmd2.ExecuteNonQuery();

            if (Request.QueryString["source"].ToString() == "history")
            {
                Response.Redirect("History.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

        }

    }

    protected void Delete_Record()
    {
        var ok = 1;




        SqlConnection cnn = new SqlConnection();
        cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalityConn"].ConnectionString;
        cnn.Open();

        var sql = "";
        sql = sql + "delete from  TimeSheets where record_id = '" + Request.QueryString["id"].ToString() + "'";




        //     sql = sql + "'" + Trainer2.SelectedValue + "',";
        //       sql = sql + "'" + Trainer_Dropdown.Text.Replace("'","") + "',";
        //     sql = sql + "'" + txtDate.Text.Replace("'","") + "',";




        SqlCommand cmd2 = new SqlCommand(sql, cnn);
        cmd2.ExecuteNonQuery();

        if (Request.QueryString["source"].ToString() == "history")
        {
            Response.Redirect("History.aspx");
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }



    protected void Delete_Click(object sender, EventArgs e)
    {

        Delete_Record();



    }


    protected void Delete_Check(object sender, EventArgs e)
    {

        if (deletebox.Text == "delete")
        {
            Delete.Enabled = true;
        }



    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        Save_Data();



    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["source"].ToString() == "history")
        {
            Response.Redirect("History.aspx");
        }
        else
        {
            Response.Redirect("Default.aspx");
        }


    }




    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {





    }


    protected void Activity_Selected(object sender, EventArgs e)

    {
        if (Activity.SelectedValue.Contains("Other"))
        {
            Activity_Other_Label.Visible = true;
            ActivityOther.Visible = true;


        }
        else
        {
            Activity_Other_Label.Visible = false;
            ActivityOther.Visible = false;
            ActivityOther.Text = "";
        }
    }



    protected void Project_Selected(object sender, EventArgs e)

    {
        if (Project.SelectedValue.Contains("Other"))
        {
            OtherLabel.Visible = true;
            OtherDescription.Visible = true;
            OtherDescription.Text = "";

        }
        else
        {
            OtherLabel.Visible = false;
            OtherDescription.Visible = false;
            OtherDescription.Text = "";
        }
    }




    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {




            string j = Request.QueryString["source"].ToString();

            //Get 
            String i = Request.QueryString["id"].ToString();
            string query = "select * from Timesheets where record_id ='" + i + "'";
            SqlCommand cmd = new SqlCommand(query);

            string strCon = System.Web
                      .Configuration
                      .WebConfigurationManager
                      .ConnectionStrings["LocalityConn"].ConnectionString;
            Project.Items.Add(new ListItem("N/A", "N/A"));

            ClientList_DropDown();
            using (SqlConnection con = new SqlConnection(strCon))

            {

                using (SqlDataAdapter sda = new SqlDataAdapter())

                {

                    cmd.Connection = con;

                    sda.SelectCommand = cmd;

                    using (DataSet ds = new DataSet())

                    {

                        sda.Fill(ds);

                        //    string name= ds.Tables[0].Rows[0].ItemArray[4].ToString();
                        Client.SelectedValue = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                        string department = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                        QM_DropDown();
                        if (QM.Items.FindByValue(ds.Tables[0].Rows[0].ItemArray[8].ToString()) == null)
                        {
                            QM.Items.Insert(0, new ListItem(ds.Tables[0].Rows[0].ItemArray[8].ToString(), ds.Tables[0].Rows[0].ItemArray[8].ToString()));
                        }
                        QM.SelectedValue = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                        Project_DropDown();
                        if (Project.Items.FindByValue(ds.Tables[0].Rows[0].ItemArray[7].ToString()) == null)
                        {
                            Project.Items.Insert(0, new ListItem(ds.Tables[0].Rows[0].ItemArray[7].ToString(), ds.Tables[0].Rows[0].ItemArray[7].ToString()));
                        }
                        Project.Items.FindByValue(ds.Tables[0].Rows[0].ItemArray[7].ToString()).Selected = true;


                        if (Activity.Items.FindByValue(ds.Tables[0].Rows[0].ItemArray[9].ToString()) == null)
                        {
                            Activity.Items.Insert(0, new ListItem(ds.Tables[0].Rows[0].ItemArray[9].ToString(), ds.Tables[0].Rows[0].ItemArray[9].ToString()));
                        }


                        Activity.SelectedValue = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                        Duration.Text = ds.Tables[0].Rows[0].ItemArray[10].ToString();
                        ActivityOther.Text = ds.Tables[0].Rows[0].ItemArray[11].ToString();
                        Comments.Text = ds.Tables[0].Rows[0].ItemArray[12].ToString();
                        Entry_Date.Text = ds.Tables[0].Rows[0].ItemArray[17].ToString();
                        var other = ds.Tables[0].Rows[0].ItemArray[9].ToString();

                        if (ActivityOther.Text.Length > 0)
                        {
                            ActivityOther.Visible = true;
                            Activity_Other_Label.Visible = true;
                        }
                        else
                        {
                            ActivityOther.Visible = false;
                            Activity_Other_Label.Visible = false;

                        }

                        OtherLabel.Visible = false;
                        OtherDescription.Visible = false;
                        //   }



                        //  Duration.Text= ds.Tables[0].Rows[0].ItemArray[16].ToString();
                    }




                }



            }





            if (Session["name"] != null)
            {
                //Get users name
                Name.Text = Session["name"].ToString();
            }
            else
            {
                Response.Redirect("default.aspx");
            }




        }



    }



    protected void ClientList_DropDown()
    {

        //Call your stored procedure here

        string strCon = System.Web
                   .Configuration
                   .WebConfigurationManager
                   .ConnectionStrings["LocalityConn"].ConnectionString;


        //Use a using statement to handle your SQL calls (it's much safer)
        using (SqlConnection sqlConnection = new SqlConnection(strCon))
        {
            //Your SQL Command
            SqlCommand cmd = new SqlCommand("select * from Client_List order by Client_name", sqlConnection);

            //Open your connection
            sqlConnection.Open();

            //Set up your databinding
            Client.DataSource = cmd.ExecuteReader();
            Client.DataTextField = "Client_name";
            Client.DataValueField = "Client_name";

            //Bind your data
            Client.DataBind();
        }

    }

    protected void QM_DropDown()
    {



        //Your ID to use
        string id = Client.SelectedValue;

        //Call your stored procedure here

        string strCon = System.Web
                   .Configuration
                   .WebConfigurationManager
                   .ConnectionStrings["LocalityConn"].ConnectionString;


        //Use a using statement to handle your SQL calls (it's much safer)
        using (SqlConnection sqlConnection = new SqlConnection(strCon))
        {
            //Your SQL Command
            if (Session["Department"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            SqlCommand cmd = new SqlCommand("select * from QM_Hierarchy where Department ='" + Session["Department"] + "' order by QM;", sqlConnection);

            //Open your connection
            sqlConnection.Open();

            //Set up your databinding
            QM.DataSource = cmd.ExecuteReader();
            QM.DataTextField = "QM";
            QM.DataValueField = "QM";
            //     QM.Items.Insert(0, "--Please Select--");
            //Bind your data
            QM.DataBind();
        }


    }

    protected void Project_DropDown()
    {



        //Your ID to use
        string id = Client.SelectedValue;

        //Call your stored procedure here

        string strCon = System.Web
                   .Configuration
                   .WebConfigurationManager
                   .ConnectionStrings["LocalityConn"].ConnectionString;


        //Use a using statement to handle your SQL calls (it's much safer)
        using (SqlConnection sqlConnection = new SqlConnection(strCon))
        {
            //Your SQL Command
            SqlCommand cmd = new SqlCommand("select * from ProjectHierarchy where Client_Name ='" + Client.Text + "' order by Project_name;", sqlConnection);

            //Open your connection
            sqlConnection.Open();

            //Set up your databinding
            Project.DataSource = cmd.ExecuteReader();
            Project.DataTextField = "Project_name";
            Project.DataValueField = "Project_name";
            if (Project.Items.FindByText("--Please Select--") == null)
            {
                Project.Items.Insert(0, "--Please Select--");
            }
            //Bind your data
            Project.DataBind();
        }


    }
    protected void ClientSelected(object sender, EventArgs e)
    {
        Project.Items.Clear();
        Project_DropDown();
    }


    protected void QM_Selecting(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (Session["department"] != null)
        {
            e.Command.Parameters["@DynamicVariable"].Value = Session["department"];
        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }

    protected void Activity_Selecting(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters["@DynamicVariable"].Value = Session["department"];
        if (Session["department"] != null)
        {
            e.Command.Parameters["@DynamicVariable"].Value = Session["department"];
        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }



    protected void ProjectSelecting(object sender, SqlDataSourceCommandEventArgs e)
    {

        e.Command.Parameters["@DynamicVariable"].Value = Client.SelectedValue;

    }


    protected void Prefer_To_Have_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}





