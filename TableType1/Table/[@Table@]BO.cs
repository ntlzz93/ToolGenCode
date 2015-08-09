using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Core.Objects;
using System.Configuration;
using System.Linq;
using DataAccess;
using EntitiesExt;

namespace BussinessLogic
{
    public  class |@Table@|BO
    {

        DatabaseDA aDatabaseDA = new DatabaseDA();
        

		public List<|@Table@|> Sel()
        {
            try
            {
                return aDatabaseDA.|@Table@|.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel:{0}", ex.Message));
            }
        }
		public List<|@Table@|> Sel(bool Disable)
        {
            try
            {
                return aDatabaseDA.|@Table@|.Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel:{0}", ex.Message));
            }
        }
        public List<|@Table@|> Sel_ByType(int Type)
        {
            try
            {
                return aDatabaseDA.|@Table@|.Where(p => p.Type == Type).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel_ByType:{0}", ex.Message));
            }
        }
        public List<|@Table@|> Sel_ByType(int Type, bool Disable)
        {
            try
            {
                return aDatabaseDA.|@Table@|.Where(p => p.Type == Type).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel_ByType:{0}", ex.Message));
            }
        }

        
     
        public List<|@Table@|> Sel_ByStatus(int Status)
        {
            try
            {
                return aDatabaseDA.|@Table@|.Where(p => p.Status == Status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel_ByStatus:{0}", ex.Message));
            }
        }
        public List<|@Table@|> Sel_ByStatus(int Status, bool Disable)
        {
            try
            {
                return aDatabaseDA.|@Table@|.Where(p => p.Status == Status).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel_ByStatus:{0}", ex.Message));
            }
        }

        public List<|@Table@|> Sel_ByType_ByStatus(int Type, int Status)
        {
            try
            {
                return aDatabaseDA.|@Table@|.Where(c => c.Type == Type).Where(p => p.Status == Status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel_ByType_ByStatus:{0}", ex.Message));
            }
        }
        public List<|@Table@|> Sel_ByType_ByStatus_ByDisable(int Type, int Status, bool Disable)
        {
            try
            {
                return aDatabaseDA.|@Table@|.Where(c => c.Type == Type).Where(p => p.Status == Status).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel_ByType_ByStatus:{0}", ex.Message));
            }
        }

      
        
        public int Del()
        {
            try
            {
                List<|@Table@|> aTemp = aDatabaseDA.|@Table@|.ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.|@Table@|.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Del: {0}", ex.Message));
            }
        }
		
       [@Loop looptype='Column' by='KeyColumn'  @]
        public int Upd_Status_By|@Column@|(int |@Column@|, int NewStatus) 
        {
            try
            {
                List<|@Table@|> aList|@Table@| = aDatabaseDA.|@Table@|.Where(b => b.|@Column@| == |@Column@|).ToList();
                if (aList|@Table@|.Count > 0) 
                {
                   
                        aList|@Table@|[0].Status = NewStatus;
                        aDatabaseDA.|@Table@|.AddOrUpdate(aList|@Table@|[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Status_By|@Column@|:{0}", ex.Message));
            }
        }
        
        public int Upd_Type_By|@Column@|(int |@Column@|, int NewType) 
        {
            try
            {
                List<|@Table@|> aList|@Table@| = aDatabaseDA.|@Table@|.Where(b => b.|@Column@| == |@Column@|).ToList();
                if (aList|@Table@|.Count > 0) 
                {
                   
                        aList|@Table@|[0].Type = NewType;
                        aDatabaseDA.|@Table@|.AddOrUpdate(aList|@Table@|[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
               
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Type_By|@Column@|:{0}", ex.Message));
            }
        }

        
        public int Upd_Disable_By|@Column@|(int |@Column@|,bool NewDisable) 
        {
            try
            {
                List<|@Table@|> aList|@Table@| = aDatabaseDA.|@Table@|.Where(b => b.|@Column@| == |@Column@|).ToList();
                if (aList|@Table@|.Count > 0) 
                {
                   
                        aList|@Table@|[0].Disable = NewDisable;
                        aDatabaseDA.|@Table@|.AddOrUpdate(aList|@Table@|[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Disable_By|@Column@|:{0}", ex.Message));
            }
        }
		
		public int Del_By|@Column@|(Int32 |@Column@|)
        {
            try
            {
                List<|@Table@|> aTemp = aDatabaseDA.|@Table@|.Where(p=>p.|@Column@| == |@Column@|).ToList();
           
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.|@Table@|.Remove(aTemp[0]);
                    return aDatabaseDA.SaveChanges();
                }
                else
                {
                    throw new Exception(String.Format("|@Table@|BO.Del_By|@Column@|: {0}", "Không tìm thấy |@Table@| có |@Column@| = " + |@Column@|));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Del: {0}", ex.Message));
            }
        }
		
        public int Del_By_List|@Column@|(List<int> aList|@Column@|)
        {
            try
            {
                List<|@Table@|> aTemp = aDatabaseDA.|@Table@|.Where(p => aList|@Column@|.Contains(int.Parse(p.|@Column@|.ToString()))).ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.|@Table@|.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Del_By_List|@Column@|: {0}", ex.Message));
            }
        }

        public  |@Table@| Sel_By|@Column@|(Int32 |@Column@|)
        {
            try
            {
                |@Table@| a|@Table@| = aDatabaseDA.|@Table@|.Where(b => b.|@Column@| == |@Column@|).ToList()[0];
              
                    return a|@Table@|;
               
                
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Sel: {0}", ex.Message));
            }
        }
        [@/Loop@]


        //#######################################################
        //NgocBM

        public int Ins(|@Table@| a|@Table@|)
        {
            try
            {
                aDatabaseDA.|@Table@|.AddOrUpdate(a|@Table@|);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Ins: {0}", ex.Message));
            }
        }
        public int Ins(ref List<|@Table@|> aList|@Table@|)
        {
            try
            {
                aList|@Table@| = aDatabaseDA.|@Table@|.AddRange(aList|@Table@|).ToList();
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Ins: {0}", ex.Message));
            }
        }
        public int Upd(List<|@Table@|> aList|@Table@|)
        {
            try
            {
                aDatabaseDA.|@Table@|.AddOrUpdate(aList|@Table@|.ToArray());
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Ins: {0}", ex.Message));
            }
        }
        public int Upd(|@Table@| a|@Table@|)
        {
            try
            {
                aDatabaseDA.|@Table@|.AddOrUpdate(a|@Table@|);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Upd: {0}", ex.Message));
            }
        }

        public int Del(List<|@Table@|> aList|@Table@|)
        {
            try
            {
                aDatabaseDA.|@Table@|.RemoveRange(aList|@Table@|);
                return aDatabaseDA.SaveChanges();
                

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("|@Table@|BO.Del: {0}", ex.Message));
            }
        }

     
        //NgocBM

    }
}

