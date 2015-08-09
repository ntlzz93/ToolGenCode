using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using MySql.Data.MySqlClient;
//using MySql.Data;
//using MySql.Data.MySqlClient.Properties;
using System.Data.SqlClient;

namespace LotusGenCode
{
    public partial class FrmConnect : Form
    {
      //  public MySqlConnection aConnection_MySQL;
        public SqlConnection   aConnection_MSSQL;


        public DataSet aDS_SQL = new DataSet();
        public FrmConnect()
        {
           //aDS_SQL.Tables.Add("Table");
            InitializeComponent();
        }

        private void FrmConnect_Load(object sender, EventArgs e)
        {
            this.combServerType.Items.Add("MYSQL");
            this.combServerType.Items.Add("MSSQL");

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.combServerType.SelectedItem.ToString().ToUpper() == "MYSQL")
                {
                    //this.ConnectToMySQL();
                }
                else if (this.combServerType.SelectedItem.ToString().ToUpper() == "MSSQL")
                {
                    this.ConnectToMSSQL();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            
        }
        private bool GetDataBase()
        {
            try
            {
                //if (this.combServerType.SelectedItem.ToString().ToUpper() == "MYSQL")
                //{
                //    if ((string.IsNullOrEmpty(txtServer.Text) == false) & (string.IsNullOrEmpty(txtUsername.Text) == false) & (string.IsNullOrEmpty(txtPort.Text) == false))
                //    {
                //        if (string.IsNullOrEmpty(txtPassword.Text) == true)
                //        {
                //            string ConnectionString = "server=" + txtServer.Text + ";User Id=" + txtUsername.Text;
                //            aConnection_MySQL = new MySqlConnection(ConnectionString);
                //            aConnection_MySQL.Open();
                //        }
                //        else
                //        {
                //            string ConnectionString = "server=" + txtServer.Text + ";User Id=" + txtUsername.Text + ";Password=" + txtPassword.Text;
                //            aConnection_MySQL = new MySqlConnection(ConnectionString);
                //            aConnection_MySQL.Open();

                //        }
                //        return true;

                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                 if (this.combServerType.SelectedItem.ToString().ToUpper() == "MSSQL")
                {
                    //INDEPENDENCE-PC;Initial Catalog=Web20;Integrated Security=True
                    if (checkbox_XacThucWindow.Checked == true)
                    {
                        string ConnectionString = "Data Source=" + txtServer.Text + ";Integrated Security=True";
                            aConnection_MSSQL = new SqlConnection(ConnectionString);
                            aConnection_MSSQL.Open();
                        return true;
                    }
                    else
                    {
                        
                         if (string.IsNullOrEmpty(txtPassword.Text) == true)
                        {

                            string ConnectionString = "Data Source=" + txtServer.Text + ";User ID=" + txtUsername.Text + ";Integrated Security=SSPI";
                            aConnection_MSSQL = new SqlConnection(ConnectionString);
                            aConnection_MSSQL.Open();
                        }
                        else
                        {
                            string ConnectionString = "Data Source=" + txtServer.Text + ";User ID=" + txtUsername.Text + ";Password=" + txtPassword.Text + ";Integrated Security=SSPI";
                            aConnection_MSSQL = new SqlConnection(ConnectionString);
                            aConnection_MSSQL.Open();
                        }
                            return true;
                    }
                  
               
                }
                else
                {
                    MessageBox.Show("Chưa chọn kiểu DB");
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
        private void LoadListDBtoComboBox()
        {
            if (this.GetDataBase() == true)
            {
                if (this.combServerType.SelectedItem.ToString().ToUpper() == "MYSQL")
                {
                    //MySqlCommand aCmd = new MySqlCommand("show databases", this.aConnection_MySQL);

                    //MySqlDataAdapter Adapter = new MySqlDataAdapter(aCmd);
                    //Adapter.SelectCommand = aCmd;
                    //DataTable aTable = new DataTable("DataBase");
                    //aTable.Columns.Add("DataBase");
                    //Adapter.Fill(aTable);
                    //cboxDataBase.DataSource = aTable;
                    //cboxDataBase.DisplayMember = "DataBase";
                }
                else
                {


                    SqlCommand aCmd = new SqlCommand("SELECT [name] FROM master.dbo.sysdatabases WHERE dbid > 6", this.aConnection_MSSQL);

                    SqlDataAdapter Adapter = new SqlDataAdapter(aCmd);
                    Adapter.SelectCommand = aCmd;
                    DataTable aTable = new DataTable("DataBase");
                    aTable.Columns.Add("DataBase");
                    Adapter.Fill(aTable);
                    cboxDataBase.DataSource = aTable;
                    cboxDataBase.DisplayMember = "name";

                }
            }
        }
        
        //private void ConnectToMySQL()
        //{
        //    if (this.aConnection_MySQL.State == ConnectionState.Open)
        //    {
        //        this.aConnection_MySQL.ChangeDatabase(cboxDataBase.Text);
        //        MySqlCommand aCmd = new MySqlCommand("show tables", this.aConnection_MySQL);
        //        aCmd.CommandType = CommandType.Text;

        //        MySqlDataAdapter Adapter = new MySqlDataAdapter(aCmd);
        //        this.aDS_SQL = new DataSet();
        //        Adapter.Fill(this.aDS_SQL);

        //        List<string> ListTable = new List<string>();


        //        for (int i = 0; i < this.aDS_SQL.Tables[0].Rows.Count; i++)
        //        {
        //            ListTable.Add(this.aDS_SQL.Tables[0].Rows[i][0].ToString());
        //        }

        //        FrmListTable aFrm = new FrmListTable(ListTable, this.aConnection_MySQL);
        //        aFrm.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không kết nối được đến DB, kiểm tra lại các thông số");
        //    }
            
        //}

        private void ConnectToMSSQL()
        {
            if (this.aConnection_MSSQL.State == ConnectionState.Open)
            {
                
                this.aConnection_MSSQL.ChangeDatabase(cboxDataBase.Text);
                SqlCommand aCmd = new SqlCommand("SELECT * FROM sys.Tables", this.aConnection_MSSQL);
                aCmd.CommandType = CommandType.Text;

                SqlDataAdapter Adapter = new SqlDataAdapter(aCmd);
                this.aDS_SQL = new DataSet();
                Adapter.Fill(this.aDS_SQL);

                List<string> ListTable = new List<string>();

                
                for (int i = 0; i < this.aDS_SQL.Tables[0].Rows.Count; i++)
                {
                    ListTable.Add(this.aDS_SQL.Tables[0].Rows[i][0].ToString());
                }
                FrmListTable aFrm = new FrmListTable(ListTable, this.aConnection_MSSQL);
                aFrm.Show();
            }
            else
            {
                MessageBox.Show("Không kết nối được đến DB, kiểm tra lại các thông số");
            }

        }

        private void cboxDataBase_MouseClick(object sender, MouseEventArgs e)
        {
            this.LoadListDBtoComboBox();
        }


    }
}
