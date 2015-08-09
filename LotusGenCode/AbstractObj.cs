using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGenCode
{
    public static class AbstractObj
    {
        public static string AllColumns = "[@AllField@]";
        public static string AllColumnsDataType = "[@AllField_DataType@]";

        public static string NormalColumns = "[@NormalField@]";
        public static string NormalColumnsDataType = "[@NormalField_DataType@]";

        public static string AutoIncrementColumns = "[@AutoIncrementField@]";
        public static string AutoIncrementColumnsDataType = "[@AutoIncrementField_DataType@]";
        
        public static string KeyColumns = "[@KeyField@]";
        public static string KeyColumnsDataType = "[@KeyField_DataType@]";

        public static string ForeignColumns = "[@ForeignField@]";
        public static string ForeignColumnsDataType = "[@ForeignField_DataType@]";

        public static string TableName = "[@Table@]";
        public static string ListTableName = "[@ListTable@]";
        public static string ShotTableName = "[@ShotTableName@]";

        public static List<string> ConvertToList()
        {
            List<string> ret = new List<string>();
            ret.Add(AllColumns);
            ret.Add(NormalColumns);
            ret.Add(AutoIncrementColumns);
            ret.Add(KeyColumns);
            ret.Add(ForeignColumns);

            ret.Add(AllColumnsDataType);
            ret.Add(NormalColumnsDataType);
            ret.Add(AutoIncrementColumnsDataType);
            ret.Add(KeyColumnsDataType);
            ret.Add(ForeignColumnsDataType);

            ret.Add(TableName);
            ret.Add(ListTableName);
            ret.Add(ShotTableName);

            return ret;
        }
    }
}
