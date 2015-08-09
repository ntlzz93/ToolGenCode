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
        //Const : Tab Navigation Gen code 
        private const string EndREPEAT_Tab = "</LOOP>";
        private const string BeginREPEAT_Tab = "<LOOP";
        //Const : Paramater of Tab Navigation Gen code
        private const string SeparateBy = "SeparateBy";
        private const string LoopCount = "LoopCount";

        private List<string> Paramate;

        private string TemplateFilePath;

        public TemplateReader(string FileName)
        {
            this.TemplateFilePath = FileName;
            //this.Paramate.Add(SeparateBy);
        }
        public string ReadFile()
        {
            try
            {
                return File.ReadAllText(this.TemplateFilePath);
            }
            catch (Exception e)
            {
                throw new Exception("CodeTemplateReader : " + e.Message.ToString());
            }
        }

        // Hàm tìm trong File TemplateFilePath trả về danh sách vị trí xuất hiện các từ khóa (đối tượng trừu tượng- AbstractObject)
        public List<int> GetListIndexAbstractObject(string AbstractObject)
        {
            return this.GetListIndexAbstractObject(AbstractObject, this.ReadFile());
        }

        // Hàm tìm trong đoạn template truyền vào, trả về danh sách vị trí xuất hiện các từ khóa (đối tượng trừu tượng- AbstractObject)
        private List<int> GetListIndexAbstractObject(string AbstractObject, string ContentTemplate)
        {
            List<int> ret = new List<int>();
            int index = -2;
            string Content = ContentTemplate;
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

        // Hàm trả về true nếu cấu trúc file template hợp lệ (kiểm tra các tab điều khiển định hướng gen - <Repeat></Repeat>)
        public bool CheckAvaiableTemplate()
        {
            return this.CheckAvaiableTemplate(this.ReadFile());
        }
        private bool CheckAvaiableTemplate(string ContentTemplate)
        {
            List<int> ListBeginTab = new List<int>();
            List<int> ListEndTab = new List<int>();

            string Content = this.ReadFile();

            ListBeginTab = this.GetListIndexAbstractObject(BeginREPEAT_Tab, ContentTemplate);
            ListEndTab = this.GetListIndexAbstractObject(EndREPEAT_Tab, ContentTemplate);

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

        public List<int> GetListIndexBeginTab()
        {
            return this.GetListIndexAbstractObject("<REPEAT");
        }
        public List<int> GetListIndexEndTab()
        {
            return this.GetListIndexAbstractObject("</REPEAT>");
        }

        public List<int> GetListIndexBeginTab(string ContentTemplate)
        {
            return this.GetListIndexAbstractObject("<REPEAT", ContentTemplate);
        }
        public List<int> GetListIndexEndTab(string ContentTemplate)
        {
            return this.GetListIndexAbstractObject("</REPEAT>", ContentTemplate);
        }

        // Hàm trả về số cặp tab điều khiển định hướng gen <Repeat></Repeat> trong file hiện hành nếu template hợp lệ. 
        // Trả về -1 nếu template không hợp lệ
        public int GetNumberTab()
        {
            return this.GetNumberTab(this.ReadFile());
        }
        public int GetNumberTab(string ContentTemplate)
        {
            if (this.CheckAvaiableTemplate(ContentTemplate) == true)
            {
                return this.GetListIndexBeginTab(ContentTemplate).Count;
            }
            else
            {
                return -1;
            }
        }

        //Trả về List chứa vị trí tab đầu và tab cuối 
        public List<int[,]> GetListTabNavigation(string ContentTemplate)
        {
            if (this.CheckAvaiableTemplate(ContentTemplate) == true)
            {
                    List<int> ListBeginTab = new List<int>();
                    List<int> ListEndTab = new List<int>();

                    ListBeginTab = this.GetListIndexAbstractObject(BeginREPEAT_Tab, ContentTemplate);
                    ListEndTab = this.GetListIndexAbstractObject(EndREPEAT_Tab, ContentTemplate);

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
        public List<int[,]> GetListTabNavigation()
        {
            return this.GetListTabNavigation( this.ReadFile());
        }

        // Trả về đoạn text thô cắt theo số thứ tự tab , đoạn text bao gồm cả 2tab đầu và cuối 
        //  ( () )  --> Số thứ tự đoạn tab được đánh từ trên xuống, từ trong ra
        public string SplitInfoTabNavigation(int TabNumber, string ContentTemplate)
        {
            List<int[,]> ListTab = new List<int[,]>();
            ListTab = this.GetListTabNavigation(ContentTemplate);
            if (TabNumber >= 1 && TabNumber <= this.GetNumberTab())
            {
                int[,] TabPosition = ListTab[TabNumber-1];
                int BeginSplit = TabPosition[0, 0];
                int EndSplit = TabPosition[0, 1];
                return ContentTemplate.Substring(BeginSplit, EndSplit + EndREPEAT_Tab.Length - BeginSplit);
                
            }
            else
            {
                return null;
            }
        }
        public string SplitInfoTabNavigation(int TabNumber)
        {
            return this.SplitInfoTabNavigation(TabNumber, this.ReadFile());
        }

        // Trả về bảng tham số đi kèm của tab
        // Input : Text thô của tab cắt từ hàm SplitInfoTabNavigation
        public Hashtable GetParamateOfTabNavigation(string InfoTabNavigation)
        {
            Hashtable ret = new Hashtable();
            string ParamateName ;
            string ParamateValue;
            
            XmlDocument XMLDoc = new XmlDocument();
            XmlNode aNode = null;

            XMLDoc.LoadXml(InfoTabNavigation);
            aNode = XMLDoc.SelectSingleNode("/REPEAT");
           
            for (int i = 0 ; i < aNode.Attributes.Count ; i++)
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
            XmlNode aNode = null;

            XMLDoc.LoadXml(InfoTabNavigation);
            aNode = XMLDoc.SelectSingleNode("/REPEAT");
            return aNode.InnerXml;
        }

       // Hàm này trả về danh sách chứa các khoảng text bên ngoài các tab <repeat>
       // Đoạn text nằm giữa các tab LỒNG NHAU không nằm trong danh sách trả về này
       // Hàm này có tham biến, tham biến này cho biết danh sách các TabNavigation mà các List text bao bên ngoài nó (các tab)
       public List<string> GetContentOutSideTab(string ContentTemplate , out List<int[,]> OutSideListTab)
        {
            List<string> ret = new List<string>();
            List<int[,]> ListPositionTab = new List<int[,]>();
            ListPositionTab = this.GetListTabNavigation(ContentTemplate);

            OutSideListTab = new List<int[,]>();

            string tempt = "";
            int NextSplitPosition = 0;
            int[,] ToaDo = new int[1, 2] { { 0, 0 } };

            for (int i = 0; i <= ListPositionTab.Count; i++)
            {
                
                if (i == 0)
                {
                    ToaDo = ListPositionTab[i];
                    tempt = ContentTemplate.Substring(0, ToaDo[0, 0]);

                    ret.Add(tempt);
                    tempt = "";
                    OutSideListTab.Add(ToaDo);
                    NextSplitPosition = ToaDo[0, 1] + EndREPEAT_Tab.Length;
                }
                else if (i == ListPositionTab.Count )
                {
                    tempt = ContentTemplate.Substring(NextSplitPosition, ContentTemplate.Length - NextSplitPosition );

                    ret.Add(tempt);
                    tempt = "";
                    OutSideListTab.Add(ToaDo);

                }
                else
                {
                    ToaDo = ListPositionTab[i];
                    //if (ListPositionTab[i][0, 0] < ListPositionTab[i + 1][0, 0])
                    //{
                        if (ToaDo[0, 0] > NextSplitPosition)
                        {
                            tempt = ContentTemplate.Substring(NextSplitPosition, ToaDo[0, 0] - NextSplitPosition - 1);

                            ret.Add(tempt);
                            tempt = "";
                            OutSideListTab.Add(ToaDo);
                            NextSplitPosition = ToaDo[0, 1] + EndREPEAT_Tab.Length;
                        }
                        if (ToaDo[0, 0] == NextSplitPosition)
                        { // Truong hop nay can thiet vi phuc vu cho muc dich noi template cho de
                            tempt = "";
                            ret.Add(tempt);
                            OutSideListTab.Add(ToaDo);
                            NextSplitPosition = ToaDo[0, 1] + EndREPEAT_Tab.Length;
                        }
                    //}
                    //else : Tình huống tab lồng trong nhau
                    // Không xét tab bên trong, nhảy sang tab tiếp theo
                }
            }
            return ret;

        }
       public List<string> GetContentOutSideTab(out List<int[,]> OutSideListTab)
       {
           return this.GetContentOutSideTab(this.ReadFile(), out OutSideListTab);
       }
       //Trong mỗi TabNavigation chỉ có 1 AbstractObject
       // Hàm này truyền vào 1 danh sách các AbstractObject , 
       // và số thứ tự tab cần xác định AbstractObject bên trong nó  
       // Hàm trả ra tên của AbstractObject trong danh sách TabNavigation
       // Hàm này phục vụ cho việc tính toán số lần lặp của 1 TabNavigation ở tầng trên
       public List<string> Get_NameOfAbstractObject_In_TabNavigation(int TabNumber)
        {
            string ContentTemplate;
            
            ContentTemplate = this.SplitInfoTabNavigation(TabNumber);
            List<string> LibraryAbstractObject = AbstractObj.ConvertToList();
            List<string> ListAbstractInTab = new List<string>();
           foreach (string Item in LibraryAbstractObject)
            {
                if (this.GetListIndexAbstractObject(Item, ContentTemplate).Count >= 1)
                {
                    ListAbstractInTab.Add(Item);
                }
            }
           return ListAbstractInTab;
        }
    }
    ;

    // Tab định hướng điều khiển việc gen code <Repeat ..></Repeat>
    class TabNavigation
    {
        string BeginTab;
        string EndTab;
        string ContentTab;
    }
}
   

