using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Xml.XPath;

namespace LotusGenCode
{
    public partial class FrmListTable : Form
    {
        //public MySqlConnection Connect_MySQL = new MySqlConnection();


        public SqlConnection Connect_MSSQL = new SqlConnection();
        public string ConnectType = "MySQL";
        public Database aDB = new Database();
        public List<TableInfo> ListTable = new List<TableInfo>();

        public MSSQLReader aMSSQLReader;


        public FrmListTable(List<TableInfo> aListTable, SqlConnection aConn)
        {
            this.ListTable = null;
            this.ListTable = aListTable;
            this.Connect_MSSQL = aConn;
            this.ConnectType = "MSSQL";

            InitializeComponent();
        }

        private void FrmListTable_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ListTable.Count; i++)
            {
                ChListboxTable.Items.Add(this.ListTable[i].TableName.ToString());
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ////==================================
            try
            {
                List<Table> ListTableChecked = new List<Table>();
                Table aTable = new Table();

                TableInfo aTableInfo = new TableInfo();

                for (int i = 0; i < ChListboxTable.CheckedItems.Count; i++)
                {
                    aTable = new Table();
                    if (this.ConnectType.ToUpper() == "MSSQL")
                    {
                        aTableInfo = ListTable.Where(p => p.TableName == ChListboxTable.CheckedItems[i].ToString()).ToList()[0];
                        this.aMSSQLReader = new MSSQLReader(this.Connect_MSSQL, aTableInfo);
                    }
                    aTable = aMSSQLReader.aTable;

                    aDB.InsertTable(aTable);
                    aDB.DatabaseName = Connect_MSSQL.Database;
                    GenCode(txtTemplatePath.Text, aTable);
                    // ListTableChecked.Add(ChListboxTable.CheckedItems[i].ToString());
                }
                MessageBox.Show("Done");
            }
            catch (Exception e1)
            {
                MessageBox.Show("Có lỗi:" + e1.Message.ToString());
            }

        }


        private void btnOpenTemplate_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtTemplatePath.Text = openFileDialog1.FileName;
        }

        public void GenCode(string TemplateFile, Table aTable)
        {
            string content = File.ReadAllText(TemplateFile);

            TemplateReader aTemplateReader = new TemplateReader(content, aTable);
            string ContentFile = aTemplateReader.CutAndSaveLoopTabToFile();

            if (TemplateFile.IndexOf("[@Table@]") >= 0)
            {
                TemplateFile = TemplateFile.Replace("[@Table@]", aTable.TableName);
            }
            else
            {
                string FileName = Path.GetFileName(TemplateFile);
                TemplateFile = Path.GetDirectoryName(TemplateFile) + "\\" + FileName;
                TemplateFile = TemplateFile.Replace("[@Table@]", aTable.TableName);
            }
            File.WriteAllText(TemplateFile,  ContentFile);

            
        }

    }
}
