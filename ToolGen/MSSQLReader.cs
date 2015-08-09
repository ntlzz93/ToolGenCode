using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;

namespace LotusGenCode
{
    public class MSSQLReader
    {
        private SqlConnection aConn = new SqlConnection();

        public MSSQLReader(SqlConnection Connection)
        {
            this.aConn = Connection; 
        }
        //ok
        public List<string> GetListNameOfAllColumns(string TableName)
        {
            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("select * from " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                for (int i = 0; i < aTable.Columns.Count; i++)
                {
                    ret.Add(aTable.Columns[i].ColumnName.ToString());
                    
                }

                return ret;
            }
            return null;
        }

        //Ok
        public List<string> GetListNameOfAutoIncrementColumns(string TableName)
        {
            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("exec sp_columns " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                for (int i = 0; i < aTable.Rows.Count; i++)
                {
                    if (aTable.Rows[i]["TYPE_NAME"].ToString().IndexOf("identity") > 0)
                    {
                        ret.Add(aTable.Rows[i]["COLUMN_NAME"].ToString());
                    }
                }

                return ret;

            }
            return null;
        }

        //check 0k
        public List<string> GetListNameOfKeyColumns(string TableName)
        {

            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("exec sp_primary_keys_rowset " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                for (int i = 0; i < aTable.Rows.Count; i++)
                {
                    ret.Add(aTable.Rows[i]["COLUMN_NAME"].ToString());
                }

                return ret;
            }
            return null;
        }

        // Hàm này trả về danh sách các trường bình thường trong bảng (KỂ CẢ TRƯỜNG KHÓA NGOẠI)
        //check ok
        public List<string> GetListNameOfNormalColumns(string TableName)
        {

            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ret = new List<string>();
                List<string> ListKey = new List<string>();
                ListKey = this.GetListNameOfKeyColumns(TableName);

                SqlCommand Cmd = new SqlCommand("SELECT * FROM " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                string temp = "";
                for (int i = 0; i < aTable.Columns.Count; i++)
                {
                    try
                    {
                        temp = ListKey.First(p => p == aTable.Columns[i].ColumnName.ToString());
                        
                    }
                    catch //hàm tìm kiếm chuỗi trong mảng trên trả về mã lỗi nếu trong mảng không có chuỗi tìm kiếm
                    { 
                        ret.Add(aTable.Columns[i].ColumnName.ToString()); 
                    }
                }

                return ret;
            }
            return null;
        }

        // Hàm này trả về danh sách các trường khóa ngoại của bảng
        //Check ok
        public List<string> GetListNameOfForeignColumns(string TableName)
        {
           
            
            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("exec sp_foreign_keys_rowset2 " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                
                Adapter.Fill(aTable);
                for (int i = 0; i < aTable.Rows.Count; i++)
                {
                    
                        ret.Add(aTable.Rows[i]["FK_COLUMN_NAME"].ToString());
                                          
                }

                return ret;
            }
            return null;
        }

        public int GetNumberAllColumn(string TableName)
        {
            return this.GetListNameOfAllColumns(TableName).Count;
        }
        public int GetNumberNormalColumn(string TableName)
        {
            return this.GetListNameOfNormalColumns(TableName).Count;

        }
        public int GetNumberAutoIncrementColumn(string TableName)
        {
            return this.GetListNameOfAutoIncrementColumns(TableName).Count;
        }
        public int GetNumberKeyColumns(string TableName)
        {
            return this.GetListNameOfKeyColumns(TableName).Count;
        }
        public int GetNumberForeignColumns(string TableName)
        {
            return this.GetListNameOfForeignColumns(TableName).Count;
        }

        public List<string> GetList_DataTypeOf_AutoIncrementColumns(string TableName)
        {
            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("exec sp_columns " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                for (int i = 0; i < aTable.Rows.Count; i++)
                {
                    if (aTable.Rows[i]["TYPE_NAME"].ToString().IndexOf("identity") > 0 )
                    {
                        ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString().Replace("identity","").Replace(" ",""));
                    }
                }

                return ret;
            }
            return null;
        }
        //check ok
        public List<string> GetList_DataTypeOf_ForeignColumns(string TableName)
        {
            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ListForeignColumns = new List<string>();
                ListForeignColumns = this.GetListNameOfForeignColumns(TableName);


                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("exec sp_columns " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                string temp = "";
                for (int i = 0; i < aTable.Rows.Count; i++)
                {

                    try
                    {
                        temp = ListForeignColumns.First(p => p == aTable.Rows[i]["COLUMN_NAME"].ToString());
                        if (string.IsNullOrEmpty(temp) == false)
                        {
                            if (aTable.Rows[i]["TYPE_NAME"].ToString().IndexOf("identity") > 0)
                            {
                                ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString().Replace("identity", "").Replace(" ", ""));
                            }
                            else
                            {
                                ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString());
                            }
                        }
                    }
                    catch //hàm tìm kiếm chuỗi trong mảng trên trả về mã lỗi nếu trong mảng không có chuỗi tìm kiếm
                    {

                    }
                }

                return ret;
            }
            return null;
        }
        //check ok
        public List<string> GetList_DataTypeOf_KeyColumns(string TableName)
        {
            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ListKeyColumns = new List<string>();
                ListKeyColumns = this.GetListNameOfKeyColumns(TableName);


                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("exec sp_columns " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                string temp = "";
                for (int i = 0; i < aTable.Rows.Count; i++)
                {

                    try
                    {
                        temp = ListKeyColumns.First(p => p == aTable.Rows[i]["COLUMN_NAME"].ToString());
                        if (string.IsNullOrEmpty(temp) == false)
                        {
                            if (aTable.Rows[i]["TYPE_NAME"].ToString().IndexOf("identity") > 0)
                            {
                                ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString().Replace("identity", "").Replace(" ", ""));
                            }
                            else
                            {
                                ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString());
                            }
                        }
                    }
                    catch //hàm tìm kiếm chuỗi trong mảng trên trả về mã lỗi nếu trong mảng không có chuỗi tìm kiếm
                    {
                        
                    }

                   
                }

                return ret;
            }
            return null;
        }
        // lấy cả cột tự sinh
        //check
        public List<string> GetList_DataTypeOf_NormalColumns(string TableName)
        {
            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ListNormalColumns = new List<string>();
                ListNormalColumns = this.GetListNameOfNormalColumns(TableName);


                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("exec sp_columns " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                string temp = "";
                for (int i = 0; i < aTable.Rows.Count; i++)
                {

                    try
                    {
                        temp = ListNormalColumns.First(p => p == aTable.Rows[i]["COLUMN_NAME"].ToString());
                        if (string.IsNullOrEmpty(temp) == false)
                        {
                            if (aTable.Rows[i]["TYPE_NAME"].ToString().IndexOf("identity") > 0)
                            {
                                ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString().Replace("identity", "").Replace(" ", ""));
                            }
                            else
                            {
                                ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString());
                            }
                        }
                    }
                    catch //hàm tìm kiếm chuỗi trong mảng trên trả về mã lỗi nếu trong mảng không có chuỗi tìm kiếm
                    {
                     
                    }
                }

                return ret;
            }
            return null;
        }

        public List<string> GetList_DataTypeOf_AllColumns(string TableName)
        {
            if (this.aConn.State == ConnectionState.Open)
            {
                List<string> ret = new List<string>();

                SqlCommand Cmd = new SqlCommand("exec sp_columns " + TableName, aConn);
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                DataTable aTable = new DataTable();
                Adapter.Fill(aTable);
                for (int i = 0; i < aTable.Rows.Count; i++)
                {
                    if (aTable.Rows[i]["TYPE_NAME"].ToString().IndexOf("identity") > 0)
                    {
                        ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString().Replace("identity", "").Replace(" ", ""));
                    }
                    else
                    {
                        ret.Add(aTable.Rows[i]["TYPE_NAME"].ToString());
                    }
                }

                return ret;
            }
            return null;
        }

        
    }
}
