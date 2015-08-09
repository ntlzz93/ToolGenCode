using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml;

namespace LotusGenCode
{
    public class TemplateReader
    {
        public Table aTable;
        public string TemplateBeforeProcess { set; get; }

        //Const : Tab Navigation Gen code 
        private const string EndREPEAT_Tab = "</LOOP>";
        private const string BeginREPEAT_Tab = "<LOOP";
        //Const : Paramater of Tab Navigation Gen code
        private const string SeparateBy = "SeparateBy";
        private const string LoopCount = "LoopCount";

        public TemplateReader(string TemplateBeforeProcess, Table aTable)
        {
            this.TemplateBeforeProcess = TemplateBeforeProcess;
            this.ProcessStep0_EncodeGENTAB();
            this.ProcessStep1_EncodeHTML();
            this.ProcessStep2_EncodeGENTAB();

            this.aTable = aTable;

        }

        private void ProcessStep0_EncodeGENTAB()
        {
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("[@/Loop@]", "[---]");
            List<int> OpenTab = GetListIndexGENTAB(this.TemplateBeforeProcess, "[@L");
            List<int> CloseTab = GetListIndexGENTAB(this.TemplateBeforeProcess, "@]");
            string b = "";
            StringBuilder aStringBuilder = new StringBuilder();
            for (int i = 0; i < OpenTab.Count; i++)
            {
                b = TemplateBeforeProcess.Substring(OpenTab[i], CloseTab[i] - OpenTab[i]);
                b = b.Replace("'", "`").Replace("\"", "`");

                this.TemplateBeforeProcess = this.TemplateBeforeProcess.Remove(OpenTab[i], CloseTab[i] - OpenTab[i]);
                this.TemplateBeforeProcess = this.TemplateBeforeProcess.Insert(OpenTab[i], b);

                aStringBuilder = new StringBuilder(this.TemplateBeforeProcess);
                aStringBuilder.Remove(OpenTab[i], CloseTab[i] - OpenTab[i]);
                aStringBuilder.Insert(OpenTab[i], b);
                this.TemplateBeforeProcess = aStringBuilder.ToString();

            }


            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("[---]", "[@/Loop@]");

        }
        private List<int> GetListIndexGENTAB(string Content , string AbstractObject)
        {

            List<int> ret = new List<int>();
            int index = -2;
           
            int PreIndexOfAbstractObject = 0;
            while (index != -1)
            {
                index = Content.IndexOf(AbstractObject, 0, StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0)
                {

                    Content = Content.Substring(index + AbstractObject.Length);
                    ret.Add(index + PreIndexOfAbstractObject);
                    PreIndexOfAbstractObject = index + PreIndexOfAbstractObject + AbstractObject.Length;

                }

            }

            return ret;
        }

        private void ProcessStep1_EncodeHTML()
        {
            
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("&", "&amp;");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("<", "&lt;");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace(">", "&gt;");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("'", "&apos;");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("\"", "&quot;");
            
            
        }

        private void ProcessStep2_EncodeGENTAB()
        {
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("`", "'").Replace("`", "\"");

            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("[@Loop", "[@LOOP");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("[@/Loop", "[@/LOOP");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("[@loop", "[@LOOP");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("[@/loop", "[@/LOOP");

            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("[@", "<");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("@]", ">");
            this.TemplateBeforeProcess = this.TemplateBeforeProcess.Replace("/@]", "/>");
            

            
        }

        private string ProcessStep3_DecodeHTML(string TemplateAfterProcess)
        {
            TemplateAfterProcess = TemplateAfterProcess.Replace("&quot;", "\"");
            TemplateAfterProcess = TemplateAfterProcess.Replace("&apos;", "'");
            TemplateAfterProcess = TemplateAfterProcess.Replace("&gt;", ">");
            TemplateAfterProcess = TemplateAfterProcess.Replace("&lt;", "<");
            TemplateAfterProcess = TemplateAfterProcess.Replace("&amp;", "&");
            
            return TemplateAfterProcess;
        }

        // Hàm tìm trong File TemplateFilePath trả về danh sách vị trí xuất hiện các từ khóa (đối tượng trừu tượng- AbstractObject)


        // Hàm tìm trong đoạn template truyền vào, trả về danh sách vị trí xuất hiện các từ khóa (đối tượng trừu tượng- AbstractObject)
        private List<int> GetListIndexAbstractObject(string AbstractObject)
        {
            List<int> ret = new List<int>();
            int index = -2;
            string Content = this.TemplateBeforeProcess;
            int PreIndexOfAbstractObject = 0;
            while (index != -1)
            {
                index = Content.IndexOf(AbstractObject, 0, StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0)
                {

                    Content = Content.Substring(index + AbstractObject.Length);
                    ret.Add(index + PreIndexOfAbstractObject);
                    PreIndexOfAbstractObject = index + PreIndexOfAbstractObject + AbstractObject.Length;

                }

            }

            return ret;
        }

        public bool CheckAvaiableTemplate()
        {
            List<int> ListBeginTab = new List<int>();
            List<int> ListEndTab = new List<int>();

            ListBeginTab = this.GetListIndexAbstractObject(BeginREPEAT_Tab);
            ListEndTab = this.GetListIndexAbstractObject(EndREPEAT_Tab);

            if ((ListBeginTab.Count == 0) && (ListEndTab.Count == 0))
            {
                return true;
            }
            else
            {
                if ((ListBeginTab.Count != ListEndTab.Count) || (ListBeginTab[0] > ListEndTab[0]))
                {
                    return false;
                }
                else
                {
                    Stack<string> Tempt = new Stack<string>();

                    try
                    {
                        while (ListBeginTab.Count >= 1 && ListEndTab.Count >= 1)
                        {
                            if (ListBeginTab[0] < ListEndTab[0])
                            {
                                Tempt.Push("(");
                                ListBeginTab.RemoveAt(0);
                            }
                            else
                            {
                                Tempt.Pop();
                                ListEndTab.RemoveAt(0);
                            }
                        }
                        if (ListBeginTab.Count == 0 && ListEndTab.Count >= 1)
                        {
                            for (int i = 0; i < ListEndTab.Count; i++)
                                Tempt.Pop();
                            if (Tempt.Count == 0)
                                return true;
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public List<int[,]> GetListTabNavigation()
        {
            if (this.CheckAvaiableTemplate() == true)
            {
                List<int> ListBeginTab = new List<int>();
                List<int> ListEndTab = new List<int>();

                ListBeginTab = this.GetListIndexAbstractObject(BeginREPEAT_Tab);
                ListEndTab = this.GetListIndexAbstractObject(EndREPEAT_Tab);

                Stack<int> Tempt = new Stack<int>();
                List<int[,]> InfoTab = new List<int[,]>();
                int[,] ItemInfoTab = new int[1, 2];

                int IndexBeginTab;
                int IndexEndTab;

                try
                {
                    while (ListBeginTab.Count >= 1 && ListEndTab.Count >= 1)
                    {
                        if (ListBeginTab[0] < ListEndTab[0])
                        {
                            Tempt.Push(ListBeginTab[0]);
                            ListBeginTab.RemoveAt(0);
                        }
                        else
                        {
                            IndexBeginTab = Tempt.Pop();
                            IndexEndTab = ListEndTab[0];

                            ItemInfoTab = new int[1, 2] { { IndexBeginTab, IndexEndTab } };
                            InfoTab.Add(ItemInfoTab);
                            ListEndTab.RemoveAt(0);
                        }
                    }
                    if (ListBeginTab.Count == 0 && ListEndTab.Count >= 1)
                    {
                        int loop = ListEndTab.Count;
                        for (int i = 0; i < loop; i++)
                        {
                            IndexBeginTab = Tempt.Pop();
                            IndexEndTab = ListEndTab[0];

                            ItemInfoTab = new int[1, 2] { { IndexBeginTab, IndexEndTab } };
                            InfoTab.Add(ItemInfoTab);
                            ListEndTab.RemoveAt(0);
                        }
                        return InfoTab;
                    }

                }

                catch
                {
                    return null;
                }

            }
            return null;
        }

        public int GetTimeLoop(Hashtable AttributeLoobTab)
        {
            if (AttributeLoobTab["looptype"].ToString() == "int")
            {
                return int.Parse(AttributeLoobTab["to"].ToString()) +1 - int.Parse(AttributeLoobTab["from"].ToString());
            }
            else if ((AttributeLoobTab["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (AttributeLoobTab["by"].ToString().ToUpper() == "KeyColumn".ToUpper()))
            {
                return this.aTable.GetListPrimaryColumn().Count();
            }
            else if ((AttributeLoobTab["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (AttributeLoobTab["by"].ToString().ToUpper() == "ForeignColumn".ToUpper()))
            {
                return this.aTable.GetListForeignColumn().Count();
            }
            else if ((AttributeLoobTab["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (AttributeLoobTab["by"].ToString().ToUpper() == "NormalColumn".ToUpper()))
            {
                return this.aTable.GetListNormalColumn().Count();
            }
            else if ((AttributeLoobTab["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (AttributeLoobTab["by"].ToString().ToUpper() == "AutoColumn".ToUpper()))
            {
                return this.aTable.GetListAutoColumn().Count();
            }
            else if (AttributeLoobTab["looptype"].ToString().ToUpper() == "DataTypeSQL".ToUpper())
            {
                return this.aTable.GetListColumnByDataTypeSQL(AttributeLoobTab["by"].ToString().ToUpper().ToString()).Count();
            }
            else if (AttributeLoobTab["looptype"].ToString().ToUpper() == "DataTypeNET".ToUpper())
            {
                return this.aTable.GetListColumnByDataTypeNET(AttributeLoobTab["by"].ToString().ToUpper().ToString()).Count();
            }
            else if (AttributeLoobTab["looptype"].ToString().ToUpper() == "ColumnName".ToUpper())
            {
                return this.aTable.GetListColumnByName(AttributeLoobTab["by"].ToString().ToString()).Count();
            }
            else
            {
                return 0;
            }
        }

        public string CutAndSaveLoopTabToFile()
        {
            List<int[,]> aList = GetListTabNavigation();
            string fileTemplateContent = this.TemplateBeforeProcess;
            string fileTemplateContentAfterCut = fileTemplateContent;
            string FileName;
            string ContentLoop = string.Empty;
            string TemplateProcessDone = string.Empty;
            if (aList.Count > 0)
            {
                for (int i = 0; i < aList.Count; i++)
                {
                    string tempt = fileTemplateContent;
                    FileName = i + ".txt";
                    string LoopString = tempt.Substring(aList[i][0, 0], aList[i][0, 1] - aList[i][0, 0] + 7);
                    // Cắt bỏ tab <loop> trong chuỗi looptring chỉ giữ lại nội dung bên trong
                    string ContentOfTabNavigationNotProcess = this.GetContentOfTabNavigation(LoopString);
                    // Lấy tất cả thuộc tính của <loop looptype='1' by='2' from='3' to='4'> 
                    // looptype='1' by='2' from='3' to='4'  --> Hastable
                    Hashtable Attribute = this.GetParamateOfTabNavigation(LoopString);
                    int loop = this.GetTimeLoop(Attribute);
                    TemplateProcessDone = string.Empty;
                    for (int ii = 0; ii < loop; ii++)
                    {
                        ContentLoop = ContentOfTabNavigationNotProcess;
                        if (Attribute["looptype"].ToString().ToUpper() == "int".ToUpper())
                        {
                            ContentLoop = ContentLoop.Replace("|@Number@|", (int.Parse(Attribute["from"].ToString()) + ii).ToString());
                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);

                            ContentLoop = ContentLoop.Replace("|@Column@|", "|@Column Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", "|@DataTypeSQL Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", "|@DataTypeNET Tab is not avaiable in this loop type@|");

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", "|@RefColumn Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", "|@RefTable Tab is not avaiable in this loop type@|");
                        }

                        else if ((Attribute["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (Attribute["by"].ToString().ToUpper() == "KeyColumn".ToUpper()))
                        {
                            List<Column> aListKeyColumn = this.aTable.GetListPrimaryColumn();

                            ContentLoop = ContentLoop.Replace("|@Number@|", ii.ToString());
                            ContentLoop = ContentLoop.Replace("|@Column@|", aListKeyColumn[ii].ColumnName);
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", aListKeyColumn[ii].DataTypeSQL);
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", aListKeyColumn[ii].DataTypeNET);
                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", "|@RefColumn Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", "|@RefTable Tab is not avaiable in this loop type@|");
                        }

                        else if ((Attribute["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (Attribute["by"].ToString().ToUpper() == "ForeignColumn".ToUpper()))
                        {
                            List<Column> aListForeignColumn = this.aTable.GetListForeignColumn();

                            ContentLoop = ContentLoop.Replace("|@Number@|", ii.ToString());
                            ContentLoop = ContentLoop.Replace("|@Column@|", aListForeignColumn[ii].ColumnName);
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", aListForeignColumn[ii].DataTypeSQL);
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", aListForeignColumn[ii].DataTypeNET);

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", aListForeignColumn[ii].RefColumn);
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", aListForeignColumn[ii].RefTable);

                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);
                        }

                        else if ((Attribute["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (Attribute["by"].ToString().ToUpper() == "NormalColumn".ToUpper()))
                        {
                            List<Column> aListNormalColumn = this.aTable.GetListNormalColumn();


                            ContentLoop = ContentLoop.Replace("|@Number@|", ii.ToString());
                            ContentLoop = ContentLoop.Replace("|@Column@|", aListNormalColumn[ii].ColumnName);
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", aListNormalColumn[ii].DataTypeSQL);
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", aListNormalColumn[ii].DataTypeNET);

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", "|@RefColumn Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", "|@RefTable Tab is not avaiable in this loop type@|");

                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);
                        }

                        else if ((Attribute["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (Attribute["by"].ToString().ToUpper() == "AutoColumn".ToUpper()))
                        {
                            List<Column> aListAutoColumn = this.aTable.GetListAutoColumn();

                            ContentLoop = ContentLoop.Replace("|@Number@|", ii.ToString());
                            ContentLoop = ContentLoop.Replace("|@Column@|", aListAutoColumn[ii].ColumnName);
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", aListAutoColumn[ii].DataTypeSQL);
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", aListAutoColumn[ii].DataTypeNET);

                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", "|@RefColumn Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", "|@RefTable Tab is not avaiable in this loop type@|");

                        }
                        else if ((Attribute["looptype"].ToString().ToUpper() == "Column".ToUpper()) && (Attribute["by"].ToString().ToUpper() == "AllColumn".ToUpper()))
                        {
                            List<Column> aListAllColumn = this.aTable.GetListAllColumn();

                            ContentLoop = ContentLoop.Replace("|@Number@|", ii.ToString());
                            ContentLoop = ContentLoop.Replace("|@Column@|", aListAllColumn[ii].ColumnName);
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", aListAllColumn[ii].DataTypeSQL);
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", aListAllColumn[ii].DataTypeNET);

                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", "|@RefColumn Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", "|@RefTable Tab is not avaiable in this loop type@|");

                        }
                        //------------------
                        else if (Attribute["looptype"].ToString().ToUpper() == "DataTypeSQL".ToUpper())
                        {
                            string DataTypeSQL = Attribute["by"].ToString().ToUpper();
                            List<Column> aListSameDataTypeColumn = this.aTable.GetListColumnByDataTypeSQL(DataTypeSQL);

                            ContentLoop = ContentLoop.Replace("|@Number@|", ii.ToString());
                            ContentLoop = ContentLoop.Replace("|@Column@|", aListSameDataTypeColumn[ii].ColumnName);
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", aListSameDataTypeColumn[ii].DataTypeSQL);
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", aListSameDataTypeColumn[ii].DataTypeNET);

                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", "|@RefColumn Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", "|@RefTable Tab is not avaiable in this loop type@|");


                        }
                        else if (Attribute["looptype"].ToString().ToUpper() == "DataTypeNET".ToUpper())
                        {
                            string DataTypeNET = Attribute["by"].ToString().ToUpper();
                            List<Column> aListSameDataTypeColumn = this.aTable.GetListColumnByDataTypeNET(DataTypeNET);

                            ContentLoop = ContentLoop.Replace("|@Number@|", ii.ToString());

                            ContentLoop = ContentLoop.Replace("|@Column@|", aListSameDataTypeColumn[ii].ColumnName);
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", aListSameDataTypeColumn[ii].DataTypeSQL);
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", aListSameDataTypeColumn[ii].DataTypeNET);

                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", "|@RefColumn Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", "|@RefTable Tab is not avaiable in this loop type@|");
                        }
                        else if (Attribute["looptype"].ToString().ToUpper() == "ColumnName".ToUpper())
                        {
                            string ColumnName = Attribute["by"].ToString();

                            List<Column> aListColumn = this.aTable.GetListColumnByName(ColumnName);

                            ContentLoop = ContentLoop.Replace("|@Number@|", ii.ToString());

                            ContentLoop = ContentLoop.Replace("|@Column@|", aListColumn[ii].ColumnName);
                            ContentLoop = ContentLoop.Replace("|@DataTypeSQL@|", aListColumn[ii].DataTypeSQL);
                            ContentLoop = ContentLoop.Replace("|@DataTypeNET@|", aListColumn[ii].DataTypeNET);

                            ContentLoop = ContentLoop.Replace("|@Table@|", aTable.TableName);
                            ContentLoop = ContentLoop.Replace("|@Schema@|", aTable.Schema);

                            ContentLoop = ContentLoop.Replace("|@RefColumn@|", "|@RefColumn Tab is not avaiable in this loop type@|");
                            ContentLoop = ContentLoop.Replace("|@RefTable@|", "|@RefTable Tab is not avaiable in this loop type@|");
                        }
                        if (Attribute["SeparateBy"] != null)
                        {
                            if (ii < loop - 1)
                            {
                                ContentLoop = ContentLoop + Attribute["SeparateBy"].ToString();
                            }
                        }
                        if (loop > 0)
                        {
                            TemplateProcessDone = TemplateProcessDone + ContentLoop;
                        }
                    }


                    File.WriteAllText(FileName, TemplateProcessDone); // 7: Chieu dai cua chuoi <loop>
                    //Xóa đoạn đã tách khỏi file template
                    string TextRemove = tempt.Substring(aList[i][0, 0], aList[i][0, 1] - aList[i][0, 0] + 7);
                    //fileTemplateContentAfterCut = fileTemplateContentAfterCut.Replace(TextRemove, "[@RepatePoint_" + i + "@]");
                    fileTemplateContentAfterCut = fileTemplateContentAfterCut.Replace(TextRemove, TemplateProcessDone);
                }
                // Xử lý replace các đối tượng ngoài thẻ loop
                fileTemplateContentAfterCut = fileTemplateContentAfterCut.Replace("|@Table@|", aTable.TableName);
                fileTemplateContentAfterCut = fileTemplateContentAfterCut.Replace("|@Schema@|", aTable.Schema);
 
                //------------

            }
            else
            {
                throw new Exception("Kiểm tra lại file template, có thể chưa có tab định hướng LOOP");
            }
            return ProcessStep3_DecodeHTML(fileTemplateContentAfterCut);
            //FileName = "NewTemplate.txt";
            //File.WriteAllText(FileName, fileTemplateContentAfterCut);

        }

        public Hashtable GetParamateOfTabNavigation(string InfoTabNavigation)
        {
            Hashtable ret = new Hashtable();
            string ParamateName;
            string ParamateValue;

            XmlDocument XMLDoc = new XmlDocument();
            XmlNode aNode = null;

            XMLDoc.LoadXml(InfoTabNavigation);
            aNode = XMLDoc.SelectSingleNode("/LOOP");

            for (int i = 0; i < aNode.Attributes.Count; i++)
            {
                ParamateName = aNode.Attributes[i].Name;
                ParamateValue = aNode.Attributes[i].Value;
                ret.Add(ParamateName, ParamateValue);
            }

            return ret;
        }
        public string GetContentOfTabNavigation(string InfoTabNavigation)
        {
            XmlDocument XMLDoc = new XmlDocument();


            XMLDoc.LoadXml(InfoTabNavigation);
            XmlNode aNode = XMLDoc.SelectSingleNode("/LOOP");
            return aNode.InnerXml;
        }

        
    }
}


