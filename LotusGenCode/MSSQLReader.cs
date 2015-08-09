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
        //public DataTable aTable1 = new DataTable();
        public Table aTable = new Table();
        public MSSQLReader(SqlConnection Connection , TableInfo TableName)
        {
            this.aConn = Connection;
            this.aTable = this.GetTable(TableName);
        }
        //ok
        private string GetSchemaOnly(string FullTableName)
        {
            char[] delimiterChars = {'.'};
            string[] tempt = FullTableName.Split(delimiterChars, StringSplitOptions.None);
            if (tempt.Count() == 2)
            {
                return tempt[0];
            }
            return null;
        }
        private string GetTableOnly(string FullTableName)
        {
            char[] delimiterChars = { '.' };
            string[] tempt = FullTableName.Split(delimiterChars, StringSplitOptions.None);
            if (tempt.Count() >0)
            {
                return tempt[tempt.Count()-1];
            }
            return null;
        }
        private Table GetTable(TableInfo aTableInfo)
        {
            
            if (this.aConn.State == ConnectionState.Open)
            {
                if (aTableInfo.IsView == false)
                {
                    string sql = "SELECT sys.schemas.name as [Schema] , sys.Tables.name as [Table] , sys.Tables.object_id, sys.all_columns.name as [Column], sys.types.name as [DataType] , sys.indexes.is_primary_key as is_primary_key , sys.all_columns.is_identity , sys.all_columns.column_id ,";
                    sql = sql + "   ISNULL(sys.foreign_key_columns.parent_column_id ,0) as is_foreignkey ,  sys.foreign_keys.name AS Constraints , OBJECT_NAME (sys.foreign_keys.referenced_object_id) AS ReferenceTableName,";
                    sql = sql + "	COL_NAME(sys.foreign_key_columns.referenced_object_id, sys.foreign_key_columns.referenced_column_id) AS ReferenceColumnName,";
                    sql = sql + "	sys.all_columns.is_filestream , sys.all_columns.is_nullable   FROM  ";
                    sql = sql + "     sys.Tables inner join sys.all_columns on sys.Tables.object_id = sys.all_columns.object_id  ";
                    sql = sql + "	             inner join sys.schemas on sys.Tables.schema_id = sys.schemas.schema_id";
                    sql = sql + "				left join  sys.types on sys.all_columns.system_type_id =  sys.types.user_type_id";
                    sql = sql + "				left join  sys.foreign_key_columns on (sys.all_columns.object_id = sys.foreign_key_columns.parent_object_id ) and (sys.all_columns.column_id = sys.foreign_key_columns.parent_column_id)";
                    sql = sql + "				left join sys.foreign_keys  on  sys.foreign_keys.parent_object_id = sys.tables.object_id";
                    sql = sql + "				left join  sys.indexes on sys.Tables.object_id = sys.indexes.OBJECT_ID and sys.indexes.index_id = sys.all_columns.column_id";
                    sql = sql + " 			where sys.tables.name =" + "'" + GetTableOnly(aTableInfo.TableName) + "'";


                    SqlCommand Cmd = new SqlCommand(sql, aConn);
                    SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                    DataTable aTable = new DataTable();

                    Table aTemptTable = new Table();
                    Column aTemptColumn = new Column();

                    Adapter.Fill(aTable);

                    aTemptTable.TableName = aTable.Rows[0][1].ToString(); ;
                    aTemptTable.Schema = aTable.Rows[0][0].ToString();
                    for (int i = 0; i < aTable.Rows.Count; i++)
                    {
                        aTemptColumn = new Column();
                        aTemptColumn.ColumnName = aTable.Rows[i]["Column"].ToString();

                        aTemptColumn.DataTypeSQL = aTable.Rows[i]["DataType"].ToString();

                        if (aTable.Rows[i]["DataType"].ToString() == "bigint") { aTemptColumn.DataTypeNET = "Int64"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "binary") { aTemptColumn.DataTypeNET = "Byte[]"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "bit") { aTemptColumn.DataTypeNET = "Boolean"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "char") { aTemptColumn.DataTypeNET = "String"; }

                        else if (aTable.Rows[i]["DataType"].ToString() == "date") { aTemptColumn.DataTypeNET = "DateTime"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "datetime") { aTemptColumn.DataTypeNET = "DateTime"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "datetime2") { aTemptColumn.DataTypeNET = "DateTime"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "datetimeoffset") { aTemptColumn.DataTypeNET = "DateTimeOffset"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "decimal") { aTemptColumn.DataTypeNET = "Decimal"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "FILESTREAM attribute (varbinary(max))") { aTemptColumn.DataTypeNET = "Byte[]"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "float") { aTemptColumn.DataTypeNET = "Double"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "image") { aTemptColumn.DataTypeNET = "Byte[]"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "int") { aTemptColumn.DataTypeNET = "int"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "money") { aTemptColumn.DataTypeNET = "Decimal"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "nchar") { aTemptColumn.DataTypeNET = "String"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "ntext") { aTemptColumn.DataTypeNET = "String"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "numeric") { aTemptColumn.DataTypeNET = "Decimal"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "nvarchar") { aTemptColumn.DataTypeNET = "String"; }

                        else if (aTable.Rows[i]["DataType"].ToString() == "real") { aTemptColumn.DataTypeNET = "Single"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "rowversion") { aTemptColumn.DataTypeNET = "Byte[]"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "smalldatetime") { aTemptColumn.DataTypeNET = "DateTime"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "smallint") { aTemptColumn.DataTypeNET = "Int16"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "smallmoney") { aTemptColumn.DataTypeNET = "Decimal"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "sql_variant") { aTemptColumn.DataTypeNET = "Object"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "text") { aTemptColumn.DataTypeNET = "String"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "time") { aTemptColumn.DataTypeNET = "TimeSpan"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "timestamp") { aTemptColumn.DataTypeNET = "Byte[]"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "tinyint") { aTemptColumn.DataTypeNET = "Byte"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "uniqueidentifier") { aTemptColumn.DataTypeNET = "Guid"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "varbinary") { aTemptColumn.DataTypeNET = "Byte[]"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "varchar") { aTemptColumn.DataTypeNET = "String"; }
                        else if (aTable.Rows[i]["DataType"].ToString() == "xml") { aTemptColumn.DataTypeNET = "Xml"; }

                        else
                        {
                            aTemptColumn.DataTypeNET = aTable.Rows[i]["DataType"].ToString();
                        }

                        //aTemptColumn.DefaultValue = aTable.Rows[i][4].ToString();
                        aTemptColumn.AllowNull = Convert.ToBoolean(aTable.Rows[i][12].ToString());

                        if (bool.Parse(aTable.Rows[i]["is_identity"].ToString()) == true)
                        {
                            aTemptColumn.AutoColumn = true;

                        }
                        if (aTable.Rows[i]["is_primary_key"].ToString().ToUpper() == "true".ToUpper())
                        {
                            aTemptColumn.ColumnType = 1;

                        }
                        //else if (int.Parse(aTable.Rows[i]["is_foreignkey"].ToString()) > 0 )
                        else if (int.Parse(aTable.Rows[i]["is_foreignkey"].ToString()) > 0)
                        {
                            aTemptColumn.ColumnType = 2;
                            aTemptColumn.RefTable = aTable.Rows[i]["ReferenceTableName"].ToString();
                            aTemptColumn.RefColumn = aTable.Rows[i]["ReferenceColumnName"].ToString();


                        }
                        else
                        {
                            aTemptColumn.ColumnType = 3;
                        }

                        aTemptTable.aListColumn.Insert(aTemptTable.aListColumn.Count, aTemptColumn);

                    }
                    return aTemptTable;
                }
                
            }
            return null;
        }




    }
}
