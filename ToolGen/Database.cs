using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGenCode
{
    public class Column
    {
        public string ColumnName {set ; get;}
        public string DataType_SQL { set; get; }
        public string DataType_Net { set; get; }
        
        public bool DefaultValue { set; get; }
        public bool Value { set; get; }


        public bool IsNull { set; get; }
        public int ColumnType { set; get; }
        
       //1: IsPrimaryKey { set; get; }
       //2: ForeignKey { set; get; }
       //3: AutoIncrementColumn { set; get; }
       //4: NormalColumn { set; get; }

    }
    public class Table
    {
        public string TableName {set; get;}
        
        public List<Column> aListColumn = new List<Column>();
        public List<Column> GetListNormalColumn();
        public List<Column> GetListPrimaryColumn();
        public List<Column> GetListForeignColumn();
        public List<Column> GetListAutoIncrementColumn();

        public List<Column> GetListColumnByDataType(string NETDataType);


    }
    public class Database
    {
        public string DatabaseName { set; get; }
        public List<Table> aListTable = new List<Table>();
        public int InsertTable(Table aTable);
        public int InsertColumn(string TableName , Column aCollumn);

    }
}
