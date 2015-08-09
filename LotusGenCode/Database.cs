using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGenCode
{
    public class Column
    {
        public Column()
        {
            this.AllowNull = true;
            this.ColumnName = "";
            this.ColumnType = -1;
            this.DataTypeNET = "";
            this.DataTypeSQL = "";
        }
        public string ColumnName = "";
        public string DataTypeSQL = "";
        public string DataTypeNET = "";

        public string DefaultValue = "";
        public string RefColumn = "";
        public string RefTable= "";



        public bool AllowNull = true;
        public bool AutoColumn = false;
        public int ColumnType = -1;

        //1: IsPrimaryKey { set; get; }
        //2: ForeignKey { set; get; }
        //3: NormalColumn { set; get; }

    }
    public class Table
    {
        public string TableName = "";
        public string Schema = "";
        
        public Table()
        {
            this.aListColumn = new List<Column>();
        }

        public List<Column> aListColumn = new List<Column>();
        public List<Column> GetListNormalColumn()
        {
            return this.aListColumn.Where(p => p.ColumnType == 3).ToList();
        }
        public List<Column> GetListPrimaryColumn()
        {
            return this.aListColumn.Where(p => p.ColumnType == 1).ToList();
        }
        public List<Column> GetListForeignColumn()
        {
            return this.aListColumn.Where(p => p.ColumnType == 2).ToList();
        }
        public List<Column> GetListAutoColumn()
        {
            return this.aListColumn.Where(p => p.AutoColumn == true).ToList();
        }

        public List<Column> GetListColumnByDataTypeNET(string NETDataType)
        {
            return this.aListColumn.Where(p => p.DataTypeNET.ToUpper() == NETDataType.ToUpper()).ToList();
        }
        public List<Column> GetListColumnByDataTypeSQL(string SQLDataType)
        {
            return this.aListColumn.Where(p => p.DataTypeSQL.ToUpper() == SQLDataType.ToUpper()).ToList();
        }

        public List<Column> GetListColumnByName(string Name)
        {
           return this.aListColumn.Where(p => p.ColumnName.Contains(Name)).ToList();
        }
        public List<Column> GetListAllColumn()
        {
            return this.aListColumn.ToList();
        }

    }
    public class Database
    {
        public string DatabaseName;
        public List<Table> aListTable = new List<Table>();
        public void InsertTable(Table aTable)
        {
            this.aListTable.Insert(0, aTable);
        }
        public int InsertColumn (string TableName, Column aCollumn)
        {
            if (this.aListTable.Where(p => p.TableName == TableName).ToList().Count() > 0)
            {
                this.aListTable.Where(p => p.TableName == TableName).ToList()[0].aListColumn.Insert(0, aCollumn);
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
