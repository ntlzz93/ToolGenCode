//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

//namespace ToolGenPHPLayer
//{
//    public class MySQLReader
//    {
        
//        private MySqlConnection aConn = new MySqlConnection();

//        public  MySQLReader(MySqlConnection Connection )
//        {
//            this.aConn = Connection; 
//        }
//        public List<string> GetListNameOfAllColumns(string TableName)
//        {
//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    ret.Add(aTable.Rows[i]["Field"].ToString());
                    
//                }

//                return ret;
//            }
//            return null;
//        }
//        public List<string> GetListNameOfAutoIncrementColumns(string TableName)
//        {
//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if (aTable.Rows[i]["Extra"].ToString() == "auto_increment")
//                    {
//                        ret.Add(aTable.Rows[i]["Field"].ToString());
//                    }
//                }

//                return ret;
//            }
//            return null;
//        }
//        public List<string> GetListNameOfKeyColumns(string TableName)
//        {

//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if (aTable.Rows[i]["Key"].ToString() == "PRI")
//                    {
//                        ret.Add(aTable.Rows[i]["Field"].ToString());
//                    }
//                }

//                return ret;
//            }
//            return null;
//        }

//        // Hàm này trả về danh sách các trường bình thường trong bảng (KỂ CẢ TRƯỜNG KHÓA NGOẠI)
//        public List<string> GetListNameOfNormalColumns(string TableName)
//        {

//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if (string.IsNullOrEmpty(aTable.Rows[i]["Key"].ToString()) == true) 
//                    {
//                        ret.Add(aTable.Rows[i]["Field"].ToString());
//                    }
//                }

//                return ret;
//            }
//            return null;
//        }

//        // Hàm này trả về danh sách các trường khóa ngoại của bảng
//        public List<string> GetListNameOfForeignColumns(string TableName)
//        {

//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if ((aTable.Rows[i]["Key"].ToString() == "MUL") && (aTable.Rows[i]["Key"].ToString() == "UNI"))
//                    {
//                        ret.Add(aTable.Rows[i]["Field"].ToString());
//                    }
//                }

//                return ret;
//            }
//            return null;
//        }

//        public List<string> GetList_DataTypeOf_AllColumns(string TableName)
//        {
//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    string DataType = aTable.Rows[i]["Type"].ToString();
//                    int Cut = DataType.IndexOf("(");
//                    if (Cut != -1)
//                    {
//                        int Lenght = DataType.Length - (DataType.Length - Cut);
//                        ret.Add(DataType.Substring(0, Lenght));
//                    }
//                    else
//                    {
//                        ret.Add(DataType);
//                    }

//                }

//                return ret;
//            }
//            return null;
//        }
//        public List<string> GetList_DataTypeOf_ForeignColumns(string TableName)
//        {

//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if ((aTable.Rows[i]["Key"].ToString() == "MUL") && (aTable.Rows[i]["Key"].ToString() == "UNI"))
//                    {
//                        string DataType = aTable.Rows[i]["Type"].ToString();
//                        int Cut = DataType.IndexOf("(");
//                        if (Cut != -1)
//                        {
//                            int Lenght = DataType.Length - (DataType.Length - Cut);
//                            ret.Add(DataType.Substring(0, Lenght));
//                        }
//                        else
//                        {
//                            ret.Add(DataType);
//                        }
//                    }
//                }

//                return ret;
//            }
//            return null;
//        }
//        public List<string> GetList_DataTypeOf_NormalColumns(string TableName)
//        {

//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if (string.IsNullOrEmpty(aTable.Rows[i]["Key"].ToString()) == true) 
//                    {
//                        string DataType = aTable.Rows[i]["Type"].ToString();
//                        int Cut = DataType.IndexOf("(");
//                        if (Cut != -1)
//                        {
//                            int Lenght = DataType.Length - (DataType.Length - Cut);
//                            ret.Add(DataType.Substring(0, Lenght));
//                        }
//                        else 
//                        {
//                            ret.Add(DataType);
//                        }


//                    }
//                }

//                return ret;
//            }
//            return null;
//        }
//        public List<string> GetList_DataTypeOf_AutoIncrementColumns(string TableName)
//        {
//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if (aTable.Rows[i]["Extra"].ToString() == "auto_increment")
//                    {
//                        string DataType = aTable.Rows[i]["Type"].ToString();
//                        int Cut = DataType.IndexOf("(");
//                        if (Cut != -1)
//                        {
//                            int Lenght = DataType.Length - (DataType.Length - Cut);
//                            ret.Add(DataType.Substring(0, Lenght));
//                        }
//                        else
//                        {
//                            ret.Add(DataType);
//                        }
//                    }
//                }

//                return ret;
//            }
//            return null;
//        }
//        public List<string> GetList_DataTypeOf_KeyColumns(string TableName)
//        {
//            if (this.aConn.State == ConnectionState.Open)
//            {
//                List<string> ret = new List<string>();

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if (aTable.Rows[i]["Key"].ToString() == "PRI")
//                    {
//                        string DataType = aTable.Rows[i]["Type"].ToString();
//                        int Cut = DataType.IndexOf("(");
//                        if (Cut != -1)
//                        {
//                            int Lenght = DataType.Length - (DataType.Length - Cut);
//                            ret.Add(DataType.Substring(0, Lenght));
//                        }
//                        else
//                        {
//                            ret.Add(DataType);
//                        }
//                    }
//                }

//                return ret;
//            }
//            return null;
//        }

//        public string Get_DataTypeOf_Columns(string TableName, string ColumnName)
//        {
//            if (this.aConn.State == ConnectionState.Open)
//            {
//                string ret;

//                MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + TableName, aConn);
//                MySqlDataAdapter Adapter = new MySqlDataAdapter(Cmd);
//                DataTable aTable = new DataTable();
//                Adapter.Fill(aTable);
//                for (int i = 0; i < aTable.Rows.Count; i++)
//                {
//                    if (aTable.Rows[i]["Field"].ToString() == ColumnName)
//                    {
//                        return ret = aTable.Rows[i]["Type"].ToString();
//                    }

//                }
//            }
//            return null;
//        }

//        public int GetNumberAllColumn(string TableName)
//        {
//            return this.GetListNameOfAllColumns(TableName).Count;
//        }
//        public int GetNumberNormalColumn(string TableName)
//        {
//            return this.GetListNameOfNormalColumns(TableName).Count;

//        }
//        public int GetNumberAutoIncrementColumn(string TableName)
//        {
//            return this.GetListNameOfAutoIncrementColumns(TableName).Count;
//        }
//        public int GetNumberKeyColumns(string TableName)
//        {
//            return this.GetListNameOfKeyColumns(TableName).Count;
//        }
//        public int GetNumberForeignColumns(string TableName)
//        {
//            return this.GetListNameOfForeignColumns(TableName).Count;
//        }


//    }
//}
