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

namespace LotusGenCode
{
    public partial class FrmListTable : Form
    {
        //public MySqlConnection Connect_MySQL = new MySqlConnection();
        public SqlConnection Connect_MSSQL = new SqlConnection();
        public string ConnectType = "MySQL";

        public List<string> ListTable = new List<string>();
       
        //public MySQLReader aMySQLReader;
        public MSSQLReader aMSSQLReader;

        public TemplateReader aTemplateReader;

        //public FrmListTable(List<string> aListTable, MySqlConnection aConn)
        //{

        //    this.ListTable = null;
        //    this.ListTable = aListTable;
        //    this.Connect_MySQL = aConn;
        //    this.ConnectType = "MySQL";
        //    InitializeComponent();
        //}

        public FrmListTable(List<string> aListTable, SqlConnection aConn)
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
                ChListboxTable.Items.Add(this.ListTable[i].ToString());
            }

            foreach (string Item in AbstractObj.ConvertToList())
            {
                lbAbsObj.Text =lbAbsObj.Text + Item + "\t\n\n";
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //==================================
            List<String> ListTableChecked = new List<string>();
            for (int i = 0; i < ChListboxTable.CheckedItems.Count; i++)
            {
                ListTableChecked.Add(ChListboxTable.CheckedItems[i].ToString());
            }
            //==================================
            //// Gan cac gia tri cho 2 class 
            //if (this.ConnectType.ToUpper() == "MYSQL")
            //{
            //    if (this.SetValueFor2Reader(this.Connect_MySQL, txtTemplatePath.Text) == true)
            //    {
            //        //==================================
            //        // Check format của template
            //        if (this.aTemplateReader.CheckAvaiableTemplate() == false)
            //        {
            //            MessageBox.Show("File template không đúng cấu trúc");
            //        }
            //        else
            //        {
            //            // lặp vòng gen từng file dựa trên từng bảng
            //            foreach (string TableName in ChListboxTable.CheckedItems)
            //            {
            //                this.GenCodeForEachTable(TableName);
            //            }

            //        }
            //    }
            //    //==================================
            //}
            if (this.ConnectType.ToUpper() == "MSSQL")
            {
                if (this.SetValueFor2Reader(this.Connect_MSSQL, txtTemplatePath.Text) == true)
                {
                    //==================================
                    // Check format của template
                    if (this.aTemplateReader.CheckAvaiableTemplate() == false)
                    {
                        MessageBox.Show("File template không đúng cấu trúc");
                    }
                    else
                    {
                        // lặp vòng gen từng file dựa trên từng bảng
                        foreach (string TableName in ChListboxTable.CheckedItems)
                        {
                            this.GenCodeForEachTable(TableName);
                        }

                    }
                }
                //==================================
            }

            MessageBox.Show("Đã convert xong");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AbstractObj.AllColumns);

        }

        private void btnOpenTemplate_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtTemplatePath.Text = openFileDialog1.FileName;
        }
        //===============================
        //Khởi tạo giá trị cho 2 đối tương Reader (MySQL, Template)
        //private bool SetValueFor2Reader( MySqlConnection Connect, string TemplateFilePath)
        //{
        //    this.aMySQLReader = new MySQLReader(Connect);
        //    if (string.IsNullOrEmpty(TemplateFilePath) == true)
        //    {
        //        MessageBox.Show("Chon file Template");
        //        return false;
        //    }
        //    else
        //    {
        //        this.aTemplateReader = new TemplateReader(TemplateFilePath);
        //        return true;
        //    }
        //}
        //===============================
        //Khởi tạo giá trị cho 2 đối tương Reader (MySQL, Template)
        private bool SetValueFor2Reader(SqlConnection Connect, string TemplateFilePath)
        {
            this.aMSSQLReader = new MSSQLReader(Connect);
            if (string.IsNullOrEmpty(TemplateFilePath) == true)
            {
                MessageBox.Show("Chon file Template");
                return false;
            }
            else
            {
                this.aTemplateReader = new TemplateReader(TemplateFilePath);
                return true;
            }
        }
        //===============================
        private bool GenCodeForEachTable(string TableName)
        {
            //if (this.ConnectType.ToUpper() == "MYSQL")
            //{
            //    #region Gen code với MySQL
            //    if (this.aTemplateReader.GetNumberTab() == 0)
            //    {
            //        string OutputData = this.aTemplateReader.ReadFile();
            //        //===============================
            //        // Xử lý một số đối tượng không nằm trong repeat tab

            //        OutputData = OutputData.Replace(AbstractObj.TableName, TableName);

            //        if (this.aMySQLReader.GetListNameOfAutoIncrementColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumns, this.aMySQLReader.GetListNameOfAutoIncrementColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetListNameOfForeignColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.ForeignColumns, this.aMySQLReader.GetListNameOfForeignColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetListNameOfKeyColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.KeyColumns, this.aMySQLReader.GetListNameOfKeyColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumnsDataType, this.aMySQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetList_DataTypeOf_ForeignColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.ForeignColumnsDataType, this.aMySQLReader.GetList_DataTypeOf_ForeignColumns(TableName)[0].ToString());


            //        OutputData = ProcessSpecialCharacters(OutputData);
            //        //===============================
            //        // Ghi dữ liệu xuống file
            //        string FileName = txtFileName.Text.Replace("[@Table@]", TableName);
            //        File.WriteAllText(Path.GetDirectoryName(txtTemplatePath.Text) + "\\" + FileName, OutputData);
            //        return true;
            //        //===============================
            //    }
            //    else
            //    {
            //        int LoopAllColumn = this.aMySQLReader.GetNumberAllColumn(TableName);
            //        int LoopNormalColumn = this.aMySQLReader.GetNumberNormalColumn(TableName);

            //        List<int[,]> OutsideListtab;
            //        //================================
            //        //Luu cac khoang ngoai Tab
            //        List<string> ListContentOutsideTab = this.aTemplateReader.GetContentOutSideTab(out OutsideListtab);
            //        //===============================
            //        // Xu ly tung tab theo Index tab
            //        List<string> ContentTabAfterProcess = new List<string>();
            //        for (int iii = 1; iii <= this.aTemplateReader.GetNumberTab(); iii++)
            //        {
            //            ContentTabAfterProcess.Add(this.GenCodeForEachTab(iii, TableName));
            //        }
            //        //===============================
            //        // Nối dữ liệu cuối cùng sau khi repeat
            //        string OutputData = this.MergeData(ListContentOutsideTab, ContentTabAfterProcess);
            //        //===============================
            //        // Xử lý một số đối tượng không nằm trong repeat tab

            //        OutputData = OutputData.Replace(AbstractObj.TableName, TableName);

            //        if (this.aMySQLReader.GetListNameOfAutoIncrementColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumns, this.aMySQLReader.GetListNameOfAutoIncrementColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetListNameOfForeignColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumns, this.aMySQLReader.GetListNameOfForeignColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetListNameOfKeyColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.KeyColumns, this.aMySQLReader.GetListNameOfKeyColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumnsDataType, this.aMySQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetList_DataTypeOf_KeyColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.KeyColumnsDataType, this.aMySQLReader.GetList_DataTypeOf_KeyColumns(TableName)[0].ToString());

            //        if (this.aMySQLReader.GetList_DataTypeOf_ForeignColumns(TableName).Count > 0)
            //            OutputData = OutputData.Replace(AbstractObj.ForeignColumnsDataType, this.aMySQLReader.GetList_DataTypeOf_ForeignColumns(TableName)[0].ToString());



            //        OutputData = ProcessSpecialCharacters(OutputData);
            //        //===============================
            //        // Ghi dữ liệu xuống file
            //        string FileName = Path.GetFileName(txtFileName.Text.Replace("[@Table@]", TableName));

            //        File.WriteAllText(Path.GetDirectoryName(txtTemplatePath.Text) + "\\" + FileName, OutputData);

            //        return true;
                    
            //    }
            //    #endregion Gen code với MySQL
            //}
             if (this.ConnectType.ToUpper() == "MSSQL")
            {
                #region Gen code với MSSQL
                if (this.aTemplateReader.GetNumberTab() == 0)
                {
                    string OutputData = this.aTemplateReader.ReadFile();
                    //===============================
                    // Xử lý một số đối tượng không nằm trong repeat tab

                    OutputData = OutputData.Replace(AbstractObj.TableName, TableName);

                    if (this.aMSSQLReader.GetListNameOfAutoIncrementColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumns, this.aMSSQLReader.GetListNameOfAutoIncrementColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetListNameOfForeignColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.ForeignColumns, this.aMSSQLReader.GetListNameOfForeignColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetListNameOfKeyColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.KeyColumns, this.aMSSQLReader.GetListNameOfKeyColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumnsDataType, this.aMSSQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetList_DataTypeOf_ForeignColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.ForeignColumnsDataType, this.aMSSQLReader.GetList_DataTypeOf_ForeignColumns(TableName)[0].ToString());


                    OutputData = ProcessSpecialCharacters(OutputData);
                    //===============================
                    // Ghi dữ liệu xuống file
                    string FileName = txtFileName.Text.Replace("[@Table@]", TableName);
                    File.WriteAllText(Path.GetDirectoryName(txtTemplatePath.Text) + "\\" + FileName, OutputData);
                    return true;
                    //===============================
                }
                else
                {
                    int LoopAllColumn = this.aMSSQLReader.GetNumberAllColumn(TableName);
                    int LoopNormalColumn = this.aMSSQLReader.GetNumberNormalColumn(TableName);
                    int LoopAllTable = this.ChListboxTable.SelectedItems.Count;

                    List<int[,]> OutsideListtab;
                    //================================
                    //Luu cac khoang ngoai Tab
                    List<string> ListContentOutsideTab = this.aTemplateReader.GetContentOutSideTab(out OutsideListtab);
                    //===============================
                    // Xu ly tung tab theo Index tab
                    List<string> ContentTabAfterProcess = new List<string>();
                    for (int iii = 1; iii <= this.aTemplateReader.GetNumberTab(); iii++)
                    {
                        ContentTabAfterProcess.Add(this.GenCodeForEachTab(iii, TableName));
                    }
                    //===============================
                    // Nối dữ liệu cuối cùng sau khi repeat
                    string OutputData = this.MergeData(ListContentOutsideTab, ContentTabAfterProcess);
                    //===============================
                    // Xử lý một số đối tượng đơn không nằm trong repeat tab

                    OutputData = OutputData.Replace(AbstractObj.TableName, TableName);

                    if (this.aMSSQLReader.GetListNameOfAutoIncrementColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumns, this.aMSSQLReader.GetListNameOfAutoIncrementColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetListNameOfForeignColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumns, this.aMSSQLReader.GetListNameOfForeignColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetListNameOfKeyColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.KeyColumns, this.aMSSQLReader.GetListNameOfKeyColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.AutoIncrementColumnsDataType, this.aMSSQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetList_DataTypeOf_KeyColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.KeyColumnsDataType, this.aMSSQLReader.GetList_DataTypeOf_KeyColumns(TableName)[0].ToString());

                    if (this.aMSSQLReader.GetList_DataTypeOf_ForeignColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.ForeignColumnsDataType, this.aMSSQLReader.GetList_DataTypeOf_ForeignColumns(TableName)[0].ToString());
                    
                    /*
                    if (this.aMSSQLReader.GetList_DataTypeOf_NormalColumns(TableName).Count > 0)
                        OutputData = OutputData.Replace(AbstractObj.NormalColumnsDataType, this.aMSSQLReader.GetList_DataTypeOf_NormalColumns(TableName)[0].ToString());
                    */


                    OutputData = ProcessSpecialCharacters(OutputData);
                    //===============================
                    // Ghi dữ liệu xuống file
                    string FileName = Path.GetFileName(txtFileName.Text.Replace("[@Table@]", TableName));

                    File.WriteAllText(Path.GetDirectoryName(txtTemplatePath.Text) + "\\" + FileName, OutputData);

                    return true;

                }
                #endregion Gen code với MSSQL
            }
            else
            { return false; }
                  
        }
                
        private string GenCodeForEachTab(int TabNumber, string TableName)
        {
            // Lấy đoạn tab dạng thô 
            string InfoTabNavigation = this.aTemplateReader.SplitInfoTabNavigation(TabNumber);
            //Lấy nội dung đoạn tab sau khi đã bỏ cặp tab 2 đầu
            string TabText = this.aTemplateReader.GetContentOfTabNavigation(InfoTabNavigation);
            // Tách lấy paramte của tab
            Hashtable Paramates = this.aTemplateReader.GetParamateOfTabNavigation(InfoTabNavigation);
            //----
            //string AbsObject = this.aTemplateReader.Get_NameOfAbstractObject_In_TabNavigation(TabNumber);
            List<string> ListAbsObject_Col = this.aTemplateReader.Get_NameOfAbstractObject_In_TabNavigation(TabNumber);


            List<string> ColumnsName = this.GetListInforColumns(ListAbsObject_Col, TableName);
         

            int LoopRepeat = ColumnsName.Count;

            string temp = TabText;
            string StringProcess = "";
            //=============================================================================
            List<string> LibAbsObj = new List<string>();
            //LibAbsObj = AbstractObj.ConvertToList();
            for (int i = 0; i < LoopRepeat; i++)
            {
                // Khi tab không có đối số đi kèm
                if (Paramates.Count == 0)
                {
                    StringProcess = StringProcess + temp.Replace(AbsObject, ColumnsName[i]);
                    temp = TabText;
                }
                else
                {
                    //=============================
                    // Xử lý paramate "SeparateBy"
                    if (Paramates.ContainsKey("SeparateBy") == true)
                    {
                        if (i < LoopRepeat - 1)
                        {
                            StringProcess = StringProcess + temp.Replace(AbsObject, ColumnsName[i]) + Paramates["SeparateBy"].ToString();
                            temp = TabText;
                        }
                        if (i == LoopRepeat - 1)
                        {
                            StringProcess = StringProcess + temp.Replace(AbsObject, ColumnsName[i]) ;
                            temp = TabText;
                        }
                    }
                    //=============================
                    // Xử lý các paramate khác ở đây
                    //=============================
                }
            }
            return StringProcess;
        }
        public List<string> GetListInforColumns(List<string> ListAbsObject, string TableName)
        {
            
            string AbsObject = "";
            List<string> ColumnsName = new List<string>();
            for (int i = 0; i < ListAbsObject.Count; i++ )
            {
                AbsObject = ListAbsObject[i];
             
                if (AbsObject == AbstractObj.AllColumns)
                {
                    ColumnsName = this.aMSSQLReader.GetListNameOfAllColumns(TableName);
                }

                if (AbsObject == AbstractObj.NormalColumns)
                {
                    ColumnsName = this.aMSSQLReader.GetListNameOfNormalColumns(TableName);
                }

                if (AbsObject == AbstractObj.AutoIncrementColumns)
                {
                    ColumnsName = this.aMSSQLReader.GetListNameOfAutoIncrementColumns(TableName);
                }

                if (AbsObject == AbstractObj.KeyColumns)
                {
                    ColumnsName = this.aMSSQLReader.GetListNameOfKeyColumns(TableName);
                }
                if (AbsObject == AbstractObj.ForeignColumns)
                {
                    ColumnsName = this.aMSSQLReader.GetListNameOfForeignColumns(TableName);
                }

                if (AbsObject == AbstractObj.KeyColumnsDataType)
                {
                    ColumnsName = this.aMSSQLReader.GetList_DataTypeOf_KeyColumns(TableName);
                }

                if (AbsObject == AbstractObj.AutoIncrementColumnsDataType)
                {
                    ColumnsName = this.aMSSQLReader.GetList_DataTypeOf_AutoIncrementColumns(TableName);
                }

                if (AbsObject == AbstractObj.ForeignColumnsDataType)
                {
                    ColumnsName = this.aMSSQLReader.GetList_DataTypeOf_ForeignColumns(TableName);
                }

                if (AbsObject == AbstractObj.NormalColumnsDataType)
                {
                    ColumnsName = this.aMSSQLReader.GetList_DataTypeOf_NormalColumns(TableName);
                }
                if (AbsObject == AbstractObj.AllColumnsDataType)
                {
                    ColumnsName = this.aMSSQLReader.GetList_DataTypeOf_AllColumns(TableName);
                }
                /*===========update=============*/
                if (AbsObject == AbstractObj.ListTableName)
                {
                    foreach (string aTableName in ChListboxTable.CheckedItems)
                    {
                        ColumnsName.Add(aTableName);
                    }
                }
            }
            /*========================*/
            return ColumnsName;

        }
        
        public string MergeData(List<string> ListContentOutsideTab, List<string> ContentTabAfterProcess)
        {
            string ret = "";
            for (int i = 0; i < ContentTabAfterProcess.Count; i++)
            {
                ret = ret + ListContentOutsideTab[i] + ContentTabAfterProcess[i];
            }
            // Cộng với phần tử cuối cùng của mảng/file template . 
            //Mảng ListContentOutsideTab luôn nhiều hơn mảng ContentTabAfterProcess 1 phần tử do chặn đầu cuối -->  | a | b |
            ret = ret + ListContentOutsideTab[ListContentOutsideTab.Count - 1]; 
            return ret;
        }
        public string ProcessSpecialCharacters(string Content)
        {
            Content = Content.Replace("&lt;", "<");
            Content = Content.Replace("&gt;", ">");
            Content = Content.Replace("&quot;",@"\");
            Content = Content.Replace("&amp;","&");
            return Content;
        }

        private void checkBoxAllTable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAllTable.Checked == true)
            {
                for (int i = 0 ; i<ChListboxTable.Items.Count ; i++)
                {
                    ChListboxTable.SetItemChecked(i,true);
                }
            }
            else if (checkBoxAllTable.Checked == false)
            {
                for (int i = 0 ; i<ChListboxTable.Items.Count ; i++)
                {
                    ChListboxTable.SetItemChecked(i, false);
                }
            }               
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string content =  File.ReadAllText("bbb.txt");
            Template aTemp = new Template(content);
            aTemp.ProcessStep1_EncodeHTML();
            aTemp.ProcessStep2_DecodeGENTAB();
            string FileName = "aaa.txt";
            File.WriteAllText(FileName, aTemp.TemplateBeforeProcess);
        }

    }
}
