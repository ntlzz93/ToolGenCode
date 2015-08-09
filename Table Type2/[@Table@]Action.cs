using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using Library;
using Newtonsoft.Json;
using EntitiesExt;
using Newtonsoft.Json.Converters;
using BussinessLogic;
using System.IO;
using System.Globalization;
using CORE;
using DataAccess;
using Bussiness;

namespace ActionHandler
{
    public class |@Table@|Action : IAction
    {
        public SystemUsers aCurrentSystemUsers = new SystemUsers();
        public void Do(HttpContext context)
        {
            string action = context.Request["action"].ToString();
            if (!String.IsNullOrEmpty(action))
            {
                this.aCurrentSystemUsers = (SystemUsers)context.Session["LoginAccount"];

                switch (action)
                {
                    case "Sel_ByIDLang":
                        this.Sel_ByIDLang(context);
                        break;
                    case "Upd_ByCode":
                        this.Upd_ByCode(context, CORE_Language.sys_NUM_LANG);
                        break;
                    case "Ins":
                        this.Ins(context, CORE_Language.sys_NUM_LANG);
                        break;
                    case "Del_ByCode":
                        this.Del_ByCode(context);
                        break;
                    case "Sel_ByCode":
                        this.Sel_ByCode(context);
                        break;
					//-----------------------------------------------------------
					[@Loop looptype='Column' by='KeyColumn' from='C' to = 'D' SeparateBy='E' @]
                    case "Sel_By|@Column@|":
                        this.Sel_By|@Column@|(context);
                        break;
					[@/Loop@]
					//-----------------------------------------------------------

                    case "Sel_ByCode_ByIDLang":
                        this.Sel_ByCode_ByIDLang(context);
                        break;


					//-----------------------------------------------------------
					[@Loop looptype='Column' by='ForeignColumn' from='C' to = 'D' SeparateBy='E' @]
                    case "Sel_Ext_ByCode|@RefTable@|":
                        this.Sel_Ext_ByCode|@RefTable@|(context);
                        break;
					[@/Loop@]
					//-----------------------------------------------------------

					//-----------------------------------------------------------
					[@Loop looptype='Column' by='ForeignColumn' from='C' to = 'D' SeparateBy='E' @]
                    case "Sel_Ext_ByCode|@RefTable@|_ByIDLang":
                        this.Sel_Ext_ByCode|@RefTable@|_ByIDLang(context);
					
                        break;
					[@/Loop@]
					//-----------------------------------------------------------
                    /*####################################*/
                    case "Sel_Ext_ByCode":
                        this.Sel_Ext_ByCode(context);
                        break;
					
					//-----------------------------------------------------------
					[@Loop looptype='Column' by='KeyColumn' from='C' to = 'D' SeparateBy='E' @]
                    case "Sel_Ext_ByKeyCode|@Table@|_ByIDLang":
                        this.Sel_Ext_ByKeyCode|@Table@|_ByIDLang(context);
                        break;
					[@/Loop@]
					//-----------------------------------------------------------
					[@Loop looptype='Column' by='ForeignColumn' from='C' to = 'D' SeparateBy='E' @]
                    case "Sel_Ext_ByKeyCode|@RefTable@|_ByIDLang":
                        this.Sel_Ext_ByKeyCode|@RefTable@|_ByIDLang(context);
                        break;
                    [@/Loop@]
					//-----------------------------------------------------------------------

                    default:
                        context.Response.Write("Can't find action");
                        break;
                }
            }
        }


        private IsoDateTimeConverter _converter = new IsoDateTimeConverter();
        private IFormatProvider culture = new CultureInfo("es-ES", true);

		//-----------------------------------------------------------
	
        public void Sel_ByIDLang(HttpContext context)
        {

            String jSonString = "";
            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            int IDLang = string.IsNullOrEmpty(context.Request.QueryString["IDLang"]) ? CORE_Language.sys_CUR_LANG : int.Parse(context.Request.QueryString["IDLang"]);
			

            List<vw_|@Table@|ViewAll> obj = new List<vw_|@Table@|ViewAll>();
            if (string.IsNullOrEmpty(context.Request.QueryString["Disable"]))
            {
                 obj = a|@Table@|BO.Sel_Ext_ByIDLang(IDLang);
            }
            else
            {
                obj = a|@Table@|BO.Sel_Ext_ByIDLang(IDLang, bool.Parse(context.Request.QueryString["Disable"]) );
            }

           
            int count = obj.Count;

            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].|@Table@|_Info = HttpUtility.HtmlDecode(obj[i].|@Table@|_Info);
              
            }
            if (obj != null)
            {
                _converter.DateTimeFormat = "dd/MM/yyyy";
                jSonString = JsonConvert.SerializeObject(obj, _converter);
            }

            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }

     
		//-----------------------------------------------------------

		public void Ins(HttpContext context, int NUM_LANG)
        {
            ConfigsBO aConfigsBO = new ConfigsBO();

            //Code|@RefTable@| = Convert.ToString(context.Request.Form["txtCode|@RefTable@|"]);

            
            String jSonString = "";
            try
            {
                List<|@Table@|> aList|@Table@| = new List<|@Table@|>();

                |@Table@|BO a|@Table@|BO = new |@Table@|BO();
                |@Table@| a|@Table@| = new |@Table@|();
                TimeSpan Codespan = new TimeSpan(DateTime.Now.Ticks);
                string Code = Math.Floor(Codespan.TotalSeconds).ToString();

                for (int i = 1; i <= NUM_LANG; i++)
                {
                    a|@Table@| = new |@Table@|();
                    a|@Table@|.Code = Code;
                    a|@Table@|.Data = null;


					//-----------------------------------------------------------
					[@Loop looptype='DataTypeNET' by='int' from='C' to = 'D' SeparateBy='E' @]
                    a|@Table@|.|@Column@| = !String.IsNullOrEmpty(context.Request.Form["cbb|@Column@|"]) ? Convert.ToInt32(context.Request.Form["cbb|@Column@|"]) : 0;
					[@/Loop@]
					//-----------------------------------------------------------
					//-----------------------------------------------------------
					[@Loop looptype='DataTypeNET' by='int' from='C' to = 'D' SeparateBy='E' @]
                    a|@Table@|.|@Column@| = !String.IsNullOrEmpty(context.Request.Form["dtp|@Column@|"]) ? DateTime.ParseExact(context.Request.Form["dtp|@Column@|"], "dd/MM/yyyy", culture) : DateTime.Now;
					[@/Loop@]
					//-----------------------------------------------------------

                    a|@Table@|.CreatedBy = aCurrentSystemUsers.Username;

                    a|@Table@|.Disable = !String.IsNullOrEmpty(context.Request.Form["cbbDisable"]) ? Convert.ToBoolean(context.Request.Form["cbbDisable"]) : false;
                    a|@Table@|.Tag = !String.IsNullOrEmpty(context.Request.Form["txtTag"]) ? Convert.ToString(context.Request.Form["txtTag"]) : "";
                    a|@Table@|.DateCreated = !String.IsNullOrEmpty(context.Request.Form["txtDateCreated"]) ? DateTime.ParseExact(context.Request.Form["txtDateCreated"], "dd/MM/yyyy", culture) : DateTime.Now;

                    a|@Table@|.DateEdited = !String.IsNullOrEmpty(context.Request.Form["txtDateEdited"]) ? DateTime.ParseExact(context.Request.Form["txtDateEdited"], "dd/MM/yyyy", culture) : DateTime.Now;
                    a|@Table@|.UpdateBy = !String.IsNullOrEmpty(context.Request.Form["txtUpdateBy"]) ? Convert.ToString(context.Request.Form["txtUpdateBy"]) : "";
                    a|@Table@|.PublishDate = !String.IsNullOrEmpty(context.Request.Form["txtPublishDate"]) ? DateTime.ParseExact(context.Request.Form["txtPublishDate"], "dd/MM/yyyy", culture) : DateTime.Now;

                    
                    a|@Table@|.IDAlbum = !String.IsNullOrEmpty(context.Request.Form["txtIDAlbum"]) ? Convert.ToInt32(context.Request.Form["txtIDAlbum"]) : 0;
                    a|@Table@|.ViewCount = !String.IsNullOrEmpty(context.Request.Form["txtViewCount"]) ? Convert.ToInt64(context.Request.Form["txtViewCount"]) : 0;

                    a|@Table@|.Image1 = !String.IsNullOrEmpty(context.Request.Form["txtImage_2"]) ? Convert.ToString(context.Request.Form["txtImage_2"]) : "";
                    a|@Table@|.Image2 = !String.IsNullOrEmpty(context.Request.Form["txtImage_3"]) ? Convert.ToString(context.Request.Form["txtImage_3"]) : "";
                    a|@Table@|.Image3 = !String.IsNullOrEmpty(context.Request.Form["txtImage_4"]) ? Convert.ToString(context.Request.Form["txtImage_4"]) : "";

                    a|@Table@|.Title = !String.IsNullOrEmpty(context.Request.Form["txtTitle_Lang" + i]) ? Convert.ToString(context.Request.Form["txtTitle_Lang" + i]) : "";
                    a|@Table@|.Intro = !String.IsNullOrEmpty(context.Request.Form["txtIntro_Lang" + i]) ? Convert.ToString(HttpUtility.HtmlDecode(context.Request.Form["txtIntro_Lang" + i])) : "";
                    a|@Table@|.Info = !String.IsNullOrEmpty(context.Request.Form["txtInfo_Lang" + i]) ? Convert.ToString(HttpUtility.HtmlDecode(context.Request.Form["txtInfo_Lang" + i])) : "";

                    a|@Table@|.ExtendProperties1 = !String.IsNullOrEmpty(context.Request.Form["txtExtendProperties1_Lang" + i]) ? Convert.ToString(context.Request.Form["txtExtendProperties1_Lang" + i]) : "";
                    a|@Table@|.ExtendProperties2 = !String.IsNullOrEmpty(context.Request.Form["txtExtendProperties2_Lang" + i]) ? Convert.ToString(context.Request.Form["txtExtendProperties2_Lang" + i]) : "";
                    a|@Table@|.ExtendProperties3 = !String.IsNullOrEmpty(context.Request.Form["txtExtendProperties3_Lang" + i]) ? Convert.ToString(context.Request.Form["txtExtendProperties3_Lang" + i]) : "";

                    a|@Table@|.Image = !String.IsNullOrEmpty(context.Request.Form["txtImage_1"]) ? Convert.ToString(context.Request.Form["txtImage_1"]) : "";
                    a|@Table@|.IDLang = !String.IsNullOrEmpty(context.Request.Form["IDLang_" + i]) ? Convert.ToInt32(context.Request.Form["IDLang_" + i]) : 0;

                    aList|@Table@|.Add(a|@Table@|);
                }

                int Ret1 = -1;
                Ret1 = a|@Table@|BO.Ins(ref aList|@Table@|);
                if (Ret1 < aList|@Table@|.Count)
                {
                    jSonString = "{\"status\":\"error\" ,\"message\":\"" + Ret1.ToString() + "\"}";
                    a|@Table@|BO.Del(aList|@Table@|);
                    return;
                }
                else
                {
                    string ListTempt = !String.IsNullOrEmpty(context.Request.Form["ckbCode|@RefTable@|[]"]) ? Convert.ToString(context.Request.Form["ckbCode|@RefTable@|[]"]) : "";
                    if (string.IsNullOrEmpty(ListTempt) == true)
                    {
                        |@RefTable@|BO a|@RefTable@|BO = new |@RefTable@|BO();
                        List<|@RefTable@|> aListItem = a|@RefTable@|BO.Sel_ByCode("000");
                        if (aListItem.Count > 0)
                        {
                            ListTempt = aListItem[0].Code;
                        }
                        else
                        {
                            this.Create|@RefTable@|Default(context, "[Default]", NUM_LANG);
                            ListTempt = "000";
                        }
                    }

                    //else if (string.IsNullOrEmpty(ListTempt) == false)
                    //{
                        List<string> ListCode|@RefTable@| = ListTempt.Split(',').ToList();
                        List<|@Table@|_|@RefTable@|> aList|@Table@|_|@RefTable@| = new List<|@Table@|_|@RefTable@|>();
                        |@Table@|_|@RefTable@| a|@Table@|_|@RefTable@| = new |@Table@|_|@RefTable@|();


                        for (int ii = 0; ii < aList|@Table@|.Count; ii++)
                        {
                            for (int iii = 0; iii < ListCode|@RefTable@|.Count; iii++)
                            {
                                a|@Table@|_|@RefTable@| = new |@Table@|_|@RefTable@|();

                                a|@Table@|_|@RefTable@|.Code|@RefTable@| = ListCode|@RefTable@|[iii].ToString();
                                a|@Table@|_|@RefTable@|.Code|@Table@| = aList|@Table@|[ii].Code.ToString();
                                a|@Table@|_|@RefTable@|.Disable = aList|@Table@|[ii].Disable;
                                a|@Table@|_|@RefTable@|.IDLang = aList|@Table@|[ii].IDLang;
                                a|@Table@|_|@RefTable@|.Status = aList|@Table@|[ii].Status;
                                a|@Table@|_|@RefTable@|.Type = aList|@Table@|[ii].Type;

                                aList|@Table@|_|@RefTable@|.Add(a|@Table@|_|@RefTable@|);
                            }
                        }
                        |@Table@|_|@RefTable@|BO a|@Table@|_|@RefTable@|BO = new |@Table@|_|@RefTable@|BO();
                        int Ret2 = -1;
                        Ret2 = a|@Table@|_|@RefTable@|BO.Ins(ref aList|@Table@|_|@RefTable@|);
                        if (Ret2 < aList|@Table@|_|@RefTable@|.Count)
                        {
                            a|@Table@|_|@RefTable@|BO.Del(aList|@Table@|_|@RefTable@|);
                            a|@Table@|BO.Del(aList|@Table@|);
                            jSonString = "{\"status\":\"error\" ,\"message\":\"" + Ret2.ToString() + "\"}";
                            return;
                        }
                    //}
                }

                jSonString = "{\"status\": \"success\"}";
            }
            catch (Exception ex)
            {
                jSonString = "{\"status\":\"error\" ,\"message\":\"" + ex.Message.ToString() + "\"}";
            }
            finally
            {
                context.Response.Write(jSonString);
            }
        }
        
		[@/Loop@]
		//-----------------------------------------------------------

        public void Upd_ViewCount(HttpContext context, int NUM_LANG)
        {
            int ret = -1;
            String jSonString = "";
            String Code = context.Request.QueryString["Code|@Table@|"];

            //if ( context.Request.Cookies["ViewCount"] == null)
            if (context.Session[Code] == null) 
            {
                try
                {
                    if (NUM_LANG < 1)
                    {
                        NUM_LANG = 1;
                    }
                    List<|@Table@|> aList|@Table@| = new List<|@Table@|>();
                    |@Table@|BO a|@Table@|BO = new |@Table@|BO();
                    

                    //aList|@Table@| = a|@Table@|BO.Sel_all_ByCode(Code);

                    int LoopUpdate = 0;

                    if (aList|@Table@|.Count <= NUM_LANG)
                    {
                        LoopUpdate = aList|@Table@|.Count;
                        for (int i = 0; i < LoopUpdate; i++)
                        {

                            aList|@Table@|[i].ViewCount = !String.IsNullOrEmpty(aList|@Table@|[i].ViewCount.ToString()) ? Convert.ToInt64(aList|@Table@|[i].ViewCount + 1) : 1;


                            ret = a|@Table@|BO.Upd(aList|@Table@|[i]);
                            if (ret == 0)
                            {
                                jSonString = "{\"status\":\"error|" + ret.ToString() + "\"}";
                                break;
                            }

                        }
                    }
                    if (ret != 0)
                    { jSonString = "{\"status\": \"success\"}"; }
                    //HttpCookie aHttpCookie = new HttpCookie("ViewCount");
                    //aHttpCookie.Expires = DateTime.Now.AddDays(10);
                    //context.Request.Cookies.Add(aHttpCookie);
                    context.Session[Code] = 1;

                }
                catch (Exception ex)
                {
                    jSonString = "{\"status\":\"error\" ,\"message\":\"" + ex.Message.ToString() + "\"}";
                }
                finally
                {
                    context.Response.Write(jSonString);
                }


            }

        }

        public void Upd_ByCode(HttpContext context, int NUM_LANG)
        {


            try
            {
                if (NUM_LANG < 1)
                {
                    NUM_LANG = 1;
                }
                List<|@Table@|> aList|@Table@| = new List<|@Table@|>();
                |@Table@|BO a|@Table@|BO = new |@Table@|BO();
                String Code = context.Request.Form["txtCode"];

                aList|@Table@| = a|@Table@|BO.Sel_ByCode(Code);

                if (aList|@Table@|.Count <= NUM_LANG)
                {
                    for (int i = 0; i < aList|@Table@|.Count; i++)
                    {
                        aList|@Table@|[i].UpdateBy = aCurrentSystemUsers.Username;

                       [@Loop looptype='DataTypeNET' by='int'  @]				
                        aList|@Table@|[i].|@Column@| = !String.IsNullOrEmpty(context.Request.Form["ddl|@Column@|"]) ? Convert.To|@DataTypeNET@|32(context.Request.Form["ddl|@Column@|"]) : aList|@Table@|[i].|@Column@|;
                       [@/loop@]

                       [@Loop looptype='DataTypeNET' by='string'  @]				
                        aList|@Table@|[i].|@Column@| = !String.IsNullOrEmpty(context.Request.Form["txt|@Column@|"]) ? Convert.To|@DataTypeNET@|(context.Request.Form["txt|@Column@|"]) : aList|@Table@|[i].|@Column@|;
                       [@/loop@]

                       [@Loop looptype='DataTypeNET' by='bool'  @]				
                        aList|@Table@|[i].|@Column@| = !String.IsNullOrEmpty(context.Request.Form["ckb|@Column@|"]) ? Convert.To|@DataTypeNET@|(context.Request.Form["ckb|@Column@|"]) : aList|@Table@|[i].|@Column@|;
                       [@/loop@]
                        
					   [@Loop looptype='DataTypeNET' by='datetime'  @]				
                         aList|@Table@|[i].|@Column@| = !String.IsNullOrEmpty(context.Request.Form["dtp|@Column@|"]) ? DateTime.ParseExact(context.Request.Form["dtp|@Column@|"], "dd/MM/yyyy", culture) : aList|@Table@|[i].|@Column@|;
					   [@/loop@]

					   aList|@Table@|[i].Title = !String.IsNullOrEmpty(context.Request.Form["txtTitle_Lang" + (i + 1)]) ? Convert.ToString(context.Request.Form["txtTitle_Lang" + (i + 1)]) : aList|@Table@|[i].Title;

                        aList|@Table@|[i].Info = !String.IsNullOrEmpty(context.Request.Form["txtInfo_Lang" + (i + 1)]) ? Convert.ToString(HttpUtility.HtmlDecode(context.Request.Form["txtInfo_Lang" + (i + 1)])) : aList|@Table@|[i].Info;

                        aList|@Table@|[i].Intro = !String.IsNullOrEmpty(context.Request.Form["txtIntro_Lang" + (i + 1)]) ? Convert.ToString(HttpUtility.HtmlDecode(context.Request.Form["txtIntro_Lang" + (i + 1)])) : aList|@Table@|[i].Intro;

                        aList|@Table@|[i].IDLang = !String.IsNullOrEmpty(context.Request.Form["IDLanguage_Lang" + (i + 1)]) ? Convert.ToInt32(context.Request.Form["IDLanguage_Lang" + (i + 1)]) : aList|@Table@|[i].IDLang;

   
                        aList|@Table@|[i].ExtendProperties1 = !String.IsNullOrEmpty(context.Request.Form["txtExtendProperties1_Lang" + (i + 1)]) ? Convert.ToString(context.Request.Form["txtExtendProperties1_Lang" + (i + 1)]) : aList|@Table@|[i].ExtendProperties1;

                        aList|@Table@|[i].ExtendProperties2 = !String.IsNullOrEmpty(context.Request.Form["txtExtendProperties2_Lang" + (i + 1)]) ? Convert.ToString(context.Request.Form["txtExtendProperties2_Lang" + (i + 1)]) : aList|@Table@|[i].ExtendProperties2;

                        aList|@Table@|[i].ExtendProperties3 = !String.IsNullOrEmpty(context.Request.Form["txtExtendProperties3_Lang" + (i + 1)]) ? Convert.ToString(context.Request.Form["txtExtendProperties3_Lang" + (i + 1)]) : aList|@Table@|[i].ExtendProperties3;
                    }

                    int Ret1 = -1;
                    Ret1 = a|@Table@|BO.Upd(aList|@Table@|);
                    String jSonString = "";

                    

                        string ListTempt = !String.IsNullOrEmpty(context.Request.Form["ckbCode|@RefTable@|[]"]) ? Convert.ToString(context.Request.Form["ckbCode|@RefTable@|[]"]) : "";

                        if (string.IsNullOrEmpty(ListTempt) == false)
                        {
                            List<string> ListCode|@RefTable@| = ListTempt.Split(',').ToList();
                            List<|@Table@|_|@RefTable@|> aList|@Table@|_|@RefTable@| = new List<|@Table@|_|@RefTable@|>();
                            List<|@Table@|_|@RefTable@|> aListTempt = new List<|@Table@|_|@RefTable@|>();

                            |@Table@|_|@RefTable@| a|@Table@|_|@RefTable@| = new |@Table@|_|@RefTable@|();
                            |@Table@|_|@RefTable@|BO a|@Table@|_|@RefTable@|BO = new |@Table@|_|@RefTable@|BO();
                            for (int ii = 0; ii < aList|@Table@|.Count; ii++)
                            {
                                for (int iii = 0; iii < ListCode|@RefTable@|.Count; iii++)
                                {
                                    aListTempt = a|@Table@|_|@RefTable@|BO.Sel_ByCode|@Table@|_ByIDLang(aList|@Table@|[ii].Code, aList|@Table@|[ii].IDLang.GetValueOrDefault(0)).Where(p => p.Code|@RefTable@| == ListCode|@RefTable@|[iii]).ToList();
                                    if (aListTempt.Count > 0)
                                    {
                                        aListTempt[0].Disable = aList|@Table@|[ii].Disable;
                                        aListTempt[0].Status = aList|@Table@|[ii].Status;
                                        aListTempt[0].Type = aList|@Table@|[ii].Type;

                                        aList|@Table@|_|@RefTable@|.Add(aListTempt[0]);
                                        
                                    }
                                    else
                                    {
                                        a|@Table@|_|@RefTable@| = new |@Table@|_|@RefTable@|();

                                        a|@Table@|_|@RefTable@|.Disable = aList|@Table@|[ii].Disable;
                                        a|@Table@|_|@RefTable@|.Status = aList|@Table@|[ii].Status;
                                        a|@Table@|_|@RefTable@|.Type = aList|@Table@|[ii].Type;
                                        
                                        a|@Table@|_|@RefTable@|.Code|@Table@| = aList|@Table@|[ii].Code;
                                        a|@Table@|_|@RefTable@|.Code|@RefTable@| = ListCode|@RefTable@|[iii];
                                        a|@Table@|_|@RefTable@|.IDLang = aList|@Table@|[ii].IDLang;

                                        aList|@Table@|_|@RefTable@|.Add(a|@Table@|_|@RefTable@|);
                                        
                                    }

                                }
                            }

                            int Ret2 = -1;
                            Ret2 = a|@Table@|_|@RefTable@|BO.Upd(aList|@Table@|_|@RefTable@|);
                            jSonString = "";

                            if (Ret2 < aList|@Table@|.Count)
                            {
                                jSonString = "{\"status\":\"error\" ,\"message\":\"" + Ret2.ToString() + "\"}";

                                return;
                            }
                        }
                   
                }
            }
            catch (Exception e1)
            { 
            }     
        }

        public void Upd_Disable(HttpContext context)
        {
            int ret = -1;
            String jSonString = "";
            try
            {
                |@Table@|BO a|@Table@|BO = new |@Table@|BO();
                List<|@Table@|> list|@Table@| = new List<|@Table@|>();
                string Code = a|@Table@|BO.Sel_By|@Column@|(int.Parse(context.Request.QueryString["|@Column@||@Table@|"])).Code;
                //list|@Table@| = a|@Table@|BO.Sel_all_ByCode(Code);
                for (int i = 0; i < list|@Table@|.Count; i++)
                {
                    list|@Table@|[i].Disable = true;
                    ret = a|@Table@|BO.Upd(list|@Table@|[i]);
                }

                if (ret != 0)
                {
                    jSonString = "{\"status\":\"error|" + ret.ToString() + "\"}";

                }

                if (ret == 0)
                { jSonString = "{\"status\": \"success\"}"; }
            }
            catch (Exception ex)
            {
                jSonString = "{\"status\":\"error\" ,\"message\":\"" + ex.Message.ToString() + "\"}";
            }
            finally
            {
                context.Response.Write(jSonString);
            }
        }
        //=================================================================================================

        public void Sel_ByCode_ByIDLang(HttpContext context)
        {

            String jSonString = "";
            string Code = context.Request.QueryString["Code"];
          int IDLang=  int.Parse(context.Request.QueryString["IDLang"]);

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            |@Table@| obj = a|@Table@|BO.Sel_ByCode_ByIDLang(Code, IDLang);


            //obj.Info = HttpUtility.HtmlDecode(obj.Info);
            //obj.Intro = HttpUtility.HtmlDecode(obj.Intro);

            if (obj != null)
            {
                _converter.DateTimeFormat = "dd/MM/yyyy";

                jSonString = JsonConvert.SerializeObject(obj, _converter);
            }
            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }
        public void Sel_ByCode(HttpContext context)
        {

            String jSonString = "";
            string Code = context.Request.QueryString["Code"];

            int Limit = context.Request.QueryString["Limit"] != null && context.Request.QueryString["Limit"] != "undefined" ? int.Parse(context.Request.QueryString["Limit"]) : 50;
            int IntroLenght = context.Request.QueryString["IntroLenght"] != null && context.Request.QueryString["IntroLenght"] != "undefined" ? int.Parse(context.Request.QueryString["IntroLenght"]) : 100;
            int TitleLenght = context.Request.QueryString["TitleLenght"] != null && context.Request.QueryString["TitleLenght"] != "undefined" ? int.Parse(context.Request.QueryString["TitleLenght"]) : 100;
            string Order = string.IsNullOrEmpty(context.Request.QueryString["Order"]) == false ? context.Request.QueryString["Order"] : "|@Table@|_ID";
            bool IsDesc = context.Request.QueryString["IsDesc"] != null && context.Request.QueryString["IsDesc"] != "undefined" ? bool.Parse(context.Request.QueryString["IsDesc"]) : true;

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            List<|@Table@|> obj = a|@Table@|BO.Sel_ByCode(Code);
            obj = this.ConvertList(obj, TitleLenght, IntroLenght, Limit, Order, IsDesc);


            //obj.Info = HttpUtility.HtmlDecode(obj.Info);
            //obj.Intro = HttpUtility.HtmlDecode(obj.Intro);

            if (obj != null)
            {
                _converter.DateTimeFormat = "dd/MM/yyyy";

                jSonString = JsonConvert.SerializeObject(obj, _converter);
            }
            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }

		[@Loop looptype='Column' by='KeyColumn'  @]
        public void Sel_By|@Column@|(HttpContext context)
        {

            String jSonString = "";
            int |@Column@||@Table@| = int.Parse(context.Request.QueryString["|@Column@||@Table@|"]);

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            |@Table@| obj = a|@Table@|BO.Sel_By|@Column@|(|@Column@||@Table@|);
            //obj.Info = HttpUtility.HtmlDecode(obj.Info);
            //obj.Intro = HttpUtility.HtmlDecode(obj.Intro);

            if (obj != null)
            {
                _converter.DateTimeFormat = "dd/MM/yyyy";

                jSonString = JsonConvert.SerializeObject(obj, _converter);
            }
            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }

        
		
		public void Sel_Ext_ByCode|@RefTable@|(HttpContext context)
        {

            String jSonString = "";
            string CodeCategory1 = context.Request.QueryString["Code|@RefTable@|"];
            

            //obj.Info = HttpUtility.HtmlDecode(obj.Info);
            //obj.Intro = HttpUtility.HtmlDecode(obj.Intro);

            int Limit = context.Request.QueryString["Limit"] != null && context.Request.QueryString["Limit"] != "undefined" ? int.Parse(context.Request.QueryString["Limit"]) : 50;
            int IntroLenght = context.Request.QueryString["IntroLenght"] != null && context.Request.QueryString["IntroLenght"] != "undefined" ? int.Parse(context.Request.QueryString["IntroLenght"]) : 100;
            int TitleLenght = context.Request.QueryString["TitleLenght"] != null && context.Request.QueryString["TitleLenght"] != "undefined" ? int.Parse(context.Request.QueryString["TitleLenght"]) : 100;
            string Order = string.IsNullOrEmpty(context.Request.QueryString["Order"]) == false ? context.Request.QueryString["Order"] : "|@Table@|_ID";
            bool IsDesc = context.Request.QueryString["IsDesc"] != null && context.Request.QueryString["IsDesc"] != "undefined" ? bool.Parse(context.Request.QueryString["IsDesc"]) : true;

            int IDLang = !string.IsNullOrEmpty(context.Request.QueryString["IDLang"].ToString()) ? int.Parse(context.Request.QueryString["IDLang"]) : 1;

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            List<vw_|@Table@|ViewAll> obj = a|@Table@|BO.Sel_Ext_ByCode|@RefTable@|(CodeCategory1);
            obj = this.ConvertList(obj, TitleLenght, IntroLenght, Limit, Order, IsDesc);


            for (int i = 0; i < obj.Count; i++)
            {
                if ((obj[i].|@Table@|_Intro.Length > Limit) & (Limit >= 0))
                {
                    obj[i].|@Table@|_Intro = obj[i].|@Table@|_Intro.Substring(0, Limit);
                }
                else
                {
                    obj[i].|@Table@|_Intro = obj[i].|@Table@|_Intro;
                }
            }
            List<vw_|@Table@|ViewAll> ObjOrderLimit = new List<vw_|@Table@|ViewAll>();
            if ((Limit > -1) & (obj.Count > Limit))
            {
                ObjOrderLimit = obj.GetRange(0, Limit).ToList();

                _converter.DateTimeFormat = "dd/MM/yyyy";
                jSonString = JsonConvert.SerializeObject(ObjOrderLimit, _converter);
            }
            else
            {
                if (obj != null)
                {
                    _converter.DateTimeFormat = "dd/MM/yyyy";

                    jSonString = JsonConvert.SerializeObject(obj, _converter);
                }
            }
            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }
        public void Sel_Ext_ByCode|@RefTable@|_ByIDLang(HttpContext context)
        {

            String jSonString = "";
            string CodeCategory1 = context.Request.QueryString["Code|@RefTable@|"];
            
            int Limit = context.Request.QueryString["Limit"] != null && context.Request.QueryString["Limit"] != "undefined" ? int.Parse(context.Request.QueryString["Limit"]) : 50;
            int IntroLenght = context.Request.QueryString["IntroLenght"] != null && context.Request.QueryString["IntroLenght"] != "undefined" ? int.Parse(context.Request.QueryString["IntroLenght"]) : 100;
            int TitleLenght = context.Request.QueryString["TitleLenght"] != null && context.Request.QueryString["TitleLenght"] != "undefined" ? int.Parse(context.Request.QueryString["TitleLenght"]) : 100;
            string Order = string.IsNullOrEmpty(context.Request.QueryString["Order"]) == false ? context.Request.QueryString["Order"] : "|@Table@|_ID";
            bool IsDesc = context.Request.QueryString["IsDesc"] != null && context.Request.QueryString["IsDesc"] != "undefined" ? bool.Parse(context.Request.QueryString["IsDesc"]) : true;
            
            int IDLang = !string.IsNullOrEmpty( context.Request.QueryString["IDLang"].ToString()) ? int.Parse(context.Request.QueryString["IDLang"]) : 1;

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            List<vw_|@Table@|ViewAll> obj = a|@Table@|BO.Sel_Ext_ByCode|@RefTable@|_ByIDLang(CodeCategory1, false, IDLang);
            obj = this.ConvertList(obj, TitleLenght, IntroLenght, Limit, Order, IsDesc);
            //obj.Info = HttpUtility.HtmlDecode(obj.Info);
            //obj.Intro = HttpUtility.HtmlDecode(obj.Intro);

            for (int i = 0; i < obj.Count; i++)
            {
                if ((obj[i].|@Table@|_Intro.Length > IntroLenght) & (IntroLenght >= 0))
                {
                    obj[i].|@Table@|_Intro = obj[i].|@Table@|_Intro.Substring(0, IntroLenght);
                }
                else
                {
                    obj[i].|@Table@|_Intro = obj[i].|@Table@|_Intro;
                }
                //--------------------------
                if ((obj[i].|@Table@|_Title.Length > TitleLenght) & (TitleLenght >= 0))
                {
                    obj[i].|@Table@|_Title = obj[i].|@Table@|_Title.Substring(0, TitleLenght);
                }
                else
                {
                    obj[i].|@Table@|_Title = obj[i].|@Table@|_Title;
                }
                //--------------------------
            }

            List<vw_|@Table@|ViewAll> ObjOrderLimit = new List<vw_|@Table@|ViewAll>();
            if ((Limit > -1) & (obj.Count > Limit))
            {
                ObjOrderLimit = obj.GetRange(0, Limit).ToList();

                _converter.DateTimeFormat = "dd/MM/yyyy";
                jSonString = JsonConvert.SerializeObject(ObjOrderLimit, _converter);
            }
            else
            {
                if (obj != null)
                {
                    _converter.DateTimeFormat = "dd/MM/yyyy";

                    jSonString = JsonConvert.SerializeObject(obj, _converter);
                }
            }
            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }
		
		
		public void Sel_Ext_ByKeyCode|@RefTable@|_ByIDLang(HttpContext context)   // Load ra danh sách các contact bằng IDLang
        {
            String jSonString = "";

            string KeyCode|@RefTable@| = context.Request.QueryString["KeyCode|@RefTable@|"] != null && context.Request.QueryString["KeyCode|@RefTable@|"] != "undefined" ? context.Request.QueryString["KeyCode|@RefTable@|"] : "";

            int Limit = context.Request.QueryString["Limit"] != null && context.Request.QueryString["Limit"] != "undefined" ? int.Parse(context.Request.QueryString["Limit"]) : 50;
            int IntroLenght = context.Request.QueryString["IntroLenght"] != null && context.Request.QueryString["IntroLenght"] != "undefined" ? int.Parse(context.Request.QueryString["IntroLenght"]) : 100;
            int TitleLenght = context.Request.QueryString["TitleLenght"] != null && context.Request.QueryString["TitleLenght"] != "undefined" ? int.Parse(context.Request.QueryString["TitleLenght"]) : 100;
            string Order = string.IsNullOrEmpty(context.Request.QueryString["Order"]) == false ? context.Request.QueryString["Order"] : "|@Table@|_ID";
            bool IsDesc = context.Request.QueryString["IsDesc"] != null && context.Request.QueryString["IsDesc"] != "undefined" ? bool.Parse(context.Request.QueryString["IsDesc"]) : true;
            int IDLang = !string.IsNullOrEmpty(context.Request.QueryString["IDLang"].ToString()) ? int.Parse(context.Request.QueryString["IDLang"]) : 1;

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            List<vw_|@Table@|ViewAll> obj = a|@Table@|BO.Sel_Ext_ByKeyCode|@RefTable@|_ByIDLang(KeyCode|@RefTable@|, IDLang);
            obj = this.ConvertList(obj, TitleLenght, IntroLenght, Limit, Order, IsDesc);

            if (obj != null)
            {
                _converter.DateTimeFormat = "dd/MM/yyyy";
                jSonString = JsonConvert.SerializeObject(obj, _converter);
            }
            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }

		public void Del(HttpContext context)
        {

            String jSonString = "";
            int |@Column@||@Table@| = Convert.ToInt32(context.Request.QueryString["|@Column@||@Table@|"]);

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            int ret = a|@Table@|BO.Del_ByID(|@Column@||@Table@|);


            if (ret > 0)
            { jSonString = "{\"status\": \"success\"}"; }

            if (ret <= 0)
            { jSonString = "{\"status\":\"error\" ,\"message\":\"" + ret.ToString() + "\"}"; }


            context.Response.Write(jSonString);
        }
        //=================================================================================================
        private int Create|@RefTable@|Default(HttpContext context, string CategoryNameLevel1, int NUM_LANG)
        {
            List<|@RefTable@|> aList = new List<|@RefTable@|>();
            |@RefTable@| a|@RefTable@| = new |@RefTable@|();


            for (int i = 1; i <= NUM_LANG; i++)
            {
                a|@RefTable@| = new |@RefTable@|();

                a|@RefTable@|.Code = "000";
                a|@RefTable@|.IDLang = !String.IsNullOrEmpty(context.Request.Form["IDLang_" + i]) ? Convert.ToInt32(context.Request.Form["IDLang_" + i]) : 0;

                a|@RefTable@|.CategoryNameLevel1 = CategoryNameLevel1;
                a|@RefTable@|.Intro = "[Defaul |@RefTable@|]";
                a|@RefTable@|.Info = "[Defaul |@RefTable@|]";

                a|@RefTable@|.Status = 0;
                a|@RefTable@|.Disable = false;
                a|@RefTable@|.Type = 0;
                a|@RefTable@|.IDAlbum = 0;
                a|@RefTable@|.Image = "";
                a|@RefTable@|.Image1 = "";
                a|@RefTable@|.Image2 = "";
                a|@RefTable@|.Image3 = "";
                a|@RefTable@|.Tag = "";
                a|@RefTable@|.Note = "";
                a|@RefTable@|.Tag = "";
                a|@RefTable@|.ViewCount = 0;

                aList.Add(a|@RefTable@|);
            }
            |@RefTable@|BO a|@RefTable@|BO = new |@RefTable@|BO();


            return a|@RefTable@|BO.Ins(ref aList);
        }
		[@/Loop@]


        public void Sel_Ext_ByKeyCode|@Table@|_ByIDLang(HttpContext context)   // Load ra danh sách các contact bằng IDLang
        {

            String jSonString = "";

            string KeyCode|@Table@| = context.Request.QueryString["KeyCode|@Table@|"] != null && context.Request.QueryString["KeyCode|@Table@|"] != "undefined" ? context.Request.QueryString["KeyCode|@Table@|"] : "";

            int Limit = context.Request.QueryString["Limit"] != null && context.Request.QueryString["Limit"] != "undefined" ? int.Parse(context.Request.QueryString["Limit"]) : 50;
            int IntroLenght = context.Request.QueryString["IntroLenght"] != null && context.Request.QueryString["IntroLenght"] != "undefined" ? int.Parse(context.Request.QueryString["IntroLenght"]) : 100;
            int TitleLenght = context.Request.QueryString["TitleLenght"] != null && context.Request.QueryString["TitleLenght"] != "undefined" ? int.Parse(context.Request.QueryString["TitleLenght"]) : 100;
            string Order = string.IsNullOrEmpty(context.Request.QueryString["Order"]) == false ? context.Request.QueryString["Order"] : "|@Table@|_ID";
            bool IsDesc = context.Request.QueryString["IsDesc"] != null && context.Request.QueryString["IsDesc"] != "undefined" ? bool.Parse(context.Request.QueryString["IsDesc"]) : true;
            int IDLang = !string.IsNullOrEmpty(context.Request.QueryString["IDLang"].ToString()) ? int.Parse(context.Request.QueryString["IDLang"]) : 1;

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            List<vw_|@Table@|ViewAll> obj = a|@Table@|BO.Sel_Ext_ByKeyCode|@Table@|_ByIDLang(KeyCode|@Table@|, IDLang);
            obj = this.ConvertList(obj, TitleLenght, IntroLenght, Limit, Order, IsDesc);

            if (obj != null)
            {
                _converter.DateTimeFormat = "dd/MM/yyyy";
                jSonString = JsonConvert.SerializeObject(obj, _converter);
            }
            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }

        public void Sel_Ext_ByCode(HttpContext context)   // Load ra danh sách các contact bằng IDLang
        {

            String jSonString = "";

            string Code = context.Request.QueryString["Code"] != null && context.Request.QueryString["Code"] != "undefined" ? context.Request.QueryString["Code"] : "";

            int Limit = context.Request.QueryString["Limit"] != null && context.Request.QueryString["Limit"] != "undefined" ? int.Parse(context.Request.QueryString["Limit"]) : 50;
            int IntroLenght = context.Request.QueryString["IntroLenght"] != null && context.Request.QueryString["IntroLenght"] != "undefined" ? int.Parse(context.Request.QueryString["IntroLenght"]) : 100;
            int TitleLenght = context.Request.QueryString["TitleLenght"] != null && context.Request.QueryString["TitleLenght"] != "undefined" ? int.Parse(context.Request.QueryString["TitleLenght"]) : 100;
            string Order = string.IsNullOrEmpty(context.Request.QueryString["Order"]) == false ? context.Request.QueryString["Order"] : "|@Table@|_ID";
            bool IsDesc = context.Request.QueryString["IsDesc"] != null && context.Request.QueryString["IsDesc"] != "undefined" ? bool.Parse(context.Request.QueryString["IsDesc"]) : true;
            int IDLang = !string.IsNullOrEmpty(context.Request.QueryString["IDLang"].ToString()) ? int.Parse(context.Request.QueryString["IDLang"]) : 1;

            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            List<vw_|@Table@|ViewAll> obj = a|@Table@|BO.Sel_Ext_ByCode(Code);
            obj = this.ConvertList(obj, TitleLenght, IntroLenght, Limit, Order, IsDesc);


            if (obj != null)
            {
                _converter.DateTimeFormat = "dd/MM/yyyy";
                jSonString = JsonConvert.SerializeObject(obj, _converter);
            }
            jSonString = "{\"data\":" + jSonString + "}";
            context.Response.Write(jSonString);
        }


        public void Del_ByCode(HttpContext context)
        {
            |@Table@|BO a|@Table@|BO = new |@Table@|BO();
            String jSonString = "";
            string Code = context.Request.QueryString["Code"].ToString();
            int ret = a|@Table@|BO.Del_ByCode(Code);


            if (ret > 0)
            { jSonString = "{\"status\": \"success\"}"; }

            if (ret <= 0)
            { jSonString = "{\"status\":\"error\" ,\"message\":\"" + ret.ToString() + "\"}"; }


            context.Response.Write(jSonString);
        }



        private List<vw_|@Table@|ViewAll> ConvertList(List<vw_|@Table@|ViewAll> List|@Table@|, int TitleLenght, int IntroLenght, int Limit, string Order, bool IsDesc)
        {
            if (( Limit == null ) || (Limit <= 0))
            {
                Limit = List|@Table@|.Count;
            }
            if (Limit > List|@Table@|.Count)
            {
                Limit = List|@Table@|.Count;
            }

            if ((TitleLenght == null) || (TitleLenght <= 0))
            {
                TitleLenght = 1000;
            }
            if ((IntroLenght == null) || (IntroLenght <= 0))
            {
                IntroLenght = 1000;
            }

            //=========================
            
            switch (Order)
            {
                case "|@Table@|_Code":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_Code).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_Code).ToList();
                    }
                    break;
                case "|@Table@|_DateCreated":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_DateCreated).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_DateCreated).ToList();
                    }
                    break;
                case "|@Table@|_DateEdited":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_DateEdited).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_DateEdited).ToList();
                    }
                    break;
                case "|@Table@|_CommentCount":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_CommentCount).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_CommentCount).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_ExpireDate":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_ExpireDate).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_ExpireDate).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_ID":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_ID).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_ID).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_LikeCount":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_LikeCount).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_LikeCount).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_PublishDate":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_PublishDate).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_PublishDate).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_ViewCount":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_ViewCount).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_ViewCount).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_Vote":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_Vote).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_Vote).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_Type":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_Type).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_Type).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_Title":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_Title).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_Title).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_Status":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_Status).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_Status).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_Disable":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_Disable).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_Disable).ToList();
                    }
                    break;
                //###############################################################################
                case "|@RefTable@|_CategoryNameLevel1":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@RefTable@|_CategoryNameLevel1).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@RefTable@|_CategoryNameLevel1).ToList();
                    }
                    break;
                //==============================================================================
                case "|@RefTable@|_Code":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@RefTable@|_Code).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@RefTable@|_Code).ToList();
                    }
                    break;
                //==============================================================================
                case "|@RefTable@|_Disable":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@RefTable@|_Disable).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@RefTable@|_Disable).ToList();
                    }
                    break;
                //==============================================================================
                case "|@RefTable@|_Status":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@RefTable@|_Status).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@RefTable@|_Status).ToList();
                    }
                    break;
                //==============================================================================
                case "|@RefTable@|_Type":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@RefTable@|_Type).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@RefTable@|_Type).ToList();
                    }
                    break;
                //==============================================================================
                case "|@RefTable@|_ViewCount":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@RefTable@|_ViewCount).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@RefTable@|_ViewCount).ToList();
                    }
                    break;
                //###############################################################################
                case "|@Table@|_|@RefTable@|_Disable":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_|@RefTable@|_Disable).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_|@RefTable@|_Disable).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_|@RefTable@|_Status":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_|@RefTable@|_Status).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_|@RefTable@|_Status).ToList();
                    }
                    break;
                //==============================================================================
                case "|@Table@|_|@RefTable@|_Type":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.|@Table@|_|@RefTable@|_Type).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.|@Table@|_|@RefTable@|_Type).ToList();
                    }
                    break;
                //==============================================================================
                default:
                   
                    break;
            }


            //###############################################################################
            int TitleLenght_Tempt = TitleLenght;
            int IntroLenght_Tempt = IntroLenght;

            for (int i = 0; i < Limit; i++)
            {
                if (List|@Table@|[i].|@Table@|_Title.Length < TitleLenght_Tempt)
                {
                    TitleLenght_Tempt = List|@Table@|[i].|@Table@|_Title.Length;
                }
                if (List|@Table@|[i].|@Table@|_Intro.Length < IntroLenght_Tempt)
                {
                    IntroLenght_Tempt = List|@Table@|[i].|@Table@|_Intro.Length;
                }

                //if (TitleLenght == 0) { TitleLenght = 1; }
                //if (IntroLenght == 0) { IntroLenght = 1; }

                List|@Table@|[i].|@Table@|_Title = List|@Table@|[i].|@Table@|_Title.Substring(0, TitleLenght_Tempt);
                List|@Table@|[i].|@Table@|_Intro = List|@Table@|[i].|@Table@|_Intro.Substring(0, IntroLenght_Tempt);
                
                TitleLenght_Tempt = TitleLenght;
                IntroLenght_Tempt = IntroLenght;
            }

            List|@Table@| = List|@Table@|.GetRange(0, Limit).ToList();
            return List|@Table@|;
            //=========================
        }
        private List<|@Table@|> ConvertList(List<|@Table@|> List|@Table@|, int TitleLenght, int IntroLenght, int Limit, string Order, bool IsDesc)
        {
            if ((Limit == null) || (Limit <= 0))
            {
                Limit = List|@Table@|.Count;
            }
            if (Limit > List|@Table@|.Count)
            {
                Limit = List|@Table@|.Count;
            }

            if ((TitleLenght == null) || (TitleLenght <= 0))
            {
                TitleLenght = 1000;
            }
            if ((IntroLenght == null) || (IntroLenght <= 0))
            {
                IntroLenght = 1000;
            }

            //=========================

            switch (Order)
            {
                case "Code":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.Code).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.Code).ToList();
                    }
                    break;
                case "DateCreated":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.DateCreated).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.DateCreated).ToList();
                    }
                    break;
                case "DateEdited":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.DateEdited).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.DateEdited).ToList();
                    }
                    break;
                case "CommentCount":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.CommentCount).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.CommentCount).ToList();
                    }
                    break;
                //==============================================================================
                case "ExpireDate":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.ExpireDate).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.ExpireDate).ToList();
                    }
                    break;
                //==============================================================================
                case "ID":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.ID).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.ID).ToList();
                    }
                    break;
                //==============================================================================
                case "LikeCount":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.LikeCount).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.LikeCount).ToList();
                    }
                    break;
                //==============================================================================
                case "PublishDate":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.PublishDate).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.PublishDate).ToList();
                    }
                    break;
                //==============================================================================
                case "ViewCount":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.ViewCount).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.ViewCount).ToList();
                    }
                    break;
                //==============================================================================
                case "Vote":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.Vote).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.Vote).ToList();
                    }
                    break;
                //==============================================================================
                case "Type":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.Type).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.Type).ToList();
                    }
                    break;
                //==============================================================================
                case "Title":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.Title).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.Title).ToList();
                    }
                    break;
                //==============================================================================
                case "Status":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.Status).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.Status).ToList();
                    }
                    break;
                //==============================================================================
                case "Disable":
                    if (IsDesc == false)
                    {
                        List|@Table@| = List|@Table@|.OrderBy(a => a.Disable).ToList();
                    }
                    else
                    {
                        List|@Table@| = List|@Table@|.OrderByDescending(a => a.Disable).ToList();
                    }
                    break;
                //==============================================================================
                default:

                    break;
            }
            //###############################################################################
            int TitleLenght_Tempt = TitleLenght;
            int IntroLenght_Tempt = IntroLenght;

            for (int i = 0; i < Limit; i++)
            {
                if (List|@Table@|[i].Title.Length < TitleLenght_Tempt)
                {
                    TitleLenght_Tempt = List|@Table@|[i].Title.Length;
                }
                if (List|@Table@|[i].Intro.Length < IntroLenght_Tempt)
                {
                    IntroLenght_Tempt = List|@Table@|[i].Intro.Length;
                }

                //if (TitleLenght == 0) { TitleLenght = 1; }
                //if (IntroLenght == 0) { IntroLenght = 1; }

                List|@Table@|[i].Title = List|@Table@|[i].Title.Substring(0, TitleLenght_Tempt);
                List|@Table@|[i].Intro = List|@Table@|[i].Intro.Substring(0, IntroLenght_Tempt);

                TitleLenght_Tempt = TitleLenght;
                IntroLenght_Tempt = IntroLenght;
            }



            List|@Table@| = List|@Table@|.GetRange(0, Limit).ToList();
            return List|@Table@|;
            //=========================
        }
    }
}
