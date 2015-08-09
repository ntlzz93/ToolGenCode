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
    public  class DetailsTableBO
    {

        DatabaseDA aDatabaseDA = new DatabaseDA();
        

		public List<DetailsTable> Sel_ByIDLang( int IDLang)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(z => z.IDLang == IDLang).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByIDLang:{0}", ex.Message));
            }
        }
		public List<DetailsTable> Sel_ByIDLang( int IDLang, bool Disable)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(z => z.IDLang == IDLang).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByIDLang:{0}", ex.Message));
            }
        }
        public List<DetailsTable> Sel_ByType_ByIDLang(int Type, int IDLang)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(p => p.Type == Type).Where(z => z.IDLang == IDLang).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByType:{0}", ex.Message));
            }
        }
        public List<DetailsTable> Sel_ByType_ByIDLang(int Type, int IDLang, bool Disable)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(p => p.Type == Type).Where(z => z.IDLang == IDLang).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByType:{0}", ex.Message));
            }
        }

        
     
        public List<DetailsTable> Sel_ByStatus_ByIDLang(int Status, int IDLang)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(p => p.Status == Status).Where(z => z.IDLang == IDLang).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByStatus:{0}", ex.Message));
            }
        }
        public List<DetailsTable> Sel_ByStatus_ByIDLang(int Status, int IDLang, bool Disable)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(p => p.Status == Status).Where(z => z.IDLang == IDLang).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByStatus:{0}", ex.Message));
            }
        }

        public List<DetailsTable> Sel_ByType_ByStatus_ByIDLang(int Type, int Status, int IDLang)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(c => c.Type == Type).Where(p => p.Status == Status).Where(z => z.IDLang == IDLang).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByType_ByStatus_ByIDLang:{0}", ex.Message));
            }
        }
        public List<DetailsTable> Sel_ByType_ByStatus_ByIDLang_ByDisable(int Type, int Status, int IDLang, bool Disable)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(c => c.Type == Type).Where(p => p.Status == Status).Where(z => z.IDLang == IDLang).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByType_ByStatus_ByIDLang:{0}", ex.Message));
            }
        }

        public List<DetailsTable> Sel_ByCode(string Code)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(p => p.Code == Code).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByCode: {0}", ex.Message));
            }
        }
        public List<DetailsTable> Sel_ByCode(string Code, bool Disable)
        {
            try
            {
                return aDatabaseDA.DetailsTable.Where(p => p.Code == Code).Where(p => p.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel_ByCode: {0}", ex.Message));
            }
        }

        
        public int Del()
        {
            try
            {
                List<DetailsTable> aTemp = aDatabaseDA.DetailsTable.ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.DetailsTable.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Del_ByCode: {0}", ex.Message));
            }
        }
		
       
        public int Upd_Status_ByPersonID(int PersonID, int NewStatus) 
        {
            try
            {
                List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.PersonID == PersonID).ToList();
                if (aListDetailsTable.Count > 0) 
                {
                   
                        aListDetailsTable[0].Status = NewStatus;
                        aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Status_ByPersonID:{0}", ex.Message));
            }
        }
        
        public int Upd_Type_ByPersonID(int PersonID, int NewType) 
        {
            try
            {
                List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.PersonID == PersonID).ToList();
                if (aListDetailsTable.Count > 0) 
                {
                   
                        aListDetailsTable[0].Type = NewType;
                        aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
               
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Type_ByPersonID:{0}", ex.Message));
            }
        }

        
        public int Upd_Disable_ByPersonID(int PersonID,bool NewDisable) 
        {
            try
            {
                List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.PersonID == PersonID).ToList();
                if (aListDetailsTable.Count > 0) 
                {
                   
                        aListDetailsTable[0].Disable = NewDisable;
                        aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Disable_ByPersonID:{0}", ex.Message));
            }
        }
		
		public int Del_ByPersonID(Int32 PersonID)
        {
            try
            {
                List<DetailsTable> aTemp = aDatabaseDA.DetailsTable.Where(p=>p.PersonID == PersonID).ToList();
           
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.DetailsTable.Remove(aTemp[0]);
                    return aDatabaseDA.SaveChanges();
                }
                else
                {
                    throw new Exception(String.Format("DetailsTableBO.Del: {0}", "Không tìm thấy DetailsTable có PersonID = " + PersonID));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Del: {0}", ex.Message));
            }
        }
		
        public int Del_By_ListPersonID(List<int> aListPersonID)
        {
            try
            {
                List<DetailsTable> aTemp = aDatabaseDA.DetailsTable.Where(p => aListPersonID.Contains(int.Parse(p.PersonID.ToString()))).ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.DetailsTable.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Del_ByCode: {0}", ex.Message));
            }
        }

        public  DetailsTable Sel_ByPersonID(Int32 PersonID)
        {
            try
            {
                DetailsTable aDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.PersonID == PersonID).ToList()[0];
              
                    return aDetailsTable;
               
                
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Sel: {0}", ex.Message));
            }
        }
        

        public int Upd_Status_ByCode(string Code, int NewStatus) 
        {
            try
            {
                List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.Code==Code).ToList();
                if (aListDetailsTable.Count > 0)
                {
                   
                        aListDetailsTable[0].Status = NewStatus;
                        aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                        return aDatabaseDA.SaveChanges();
                   
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Upd_Status_ByCode:{0}", ex.Message));
            }
        }
        
        public int Upd_Type_ByCode(string Code, int NewType) 
        {
            try
            {
                List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.Code==Code).ToList();
                if (aListDetailsTable.Count > 0)
                {
                   
                        aListDetailsTable[0].Type = NewType;
                        aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                        return aDatabaseDA.SaveChanges();
                   
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Upd_Type_ByCode:{0}", ex.Message));
            }
        }
        
        public int Upd_Disable_ByCode(string Code,bool NewDisable) 
        {
            try
            {
                 List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.Code==Code).ToList();
                 if (aListDetailsTable.Count > 0)
                  {
                        aListDetailsTable[0].Disable = NewDisable;
                        aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                        return aDatabaseDA.SaveChanges();
                   
                  }
                 return 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Type_ByCode:{0}", ex.Message));
            }
        }
        
        public int Upd_Status_ByCode_ByLang(string Code, int NewStatus, int IDLang) 
        {
            try
            {
                List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.Code == Code).Where(z=>z.IDLang==IDLang).ToList();
                if (aListDetailsTable.Count > 0)
                {
                    aListDetailsTable[0].Status = NewStatus;
                    aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                    return aDatabaseDA.SaveChanges();

                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Upd_Status_ByCode_ByLang:{0}", ex.Message));
            }
        }
        

        public int Upd_Type_ByCode_ByLang(string Code,int NewType,int IDLang) 
        {
            try
            {
                List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.Code == Code).Where(z => z.IDLang == IDLang).ToList();
                if (aListDetailsTable.Count > 0)
                {
                    aListDetailsTable[0].Type = NewType;
                    aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                    return aDatabaseDA.SaveChanges();

                }
                return 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Type_ByCode_ByLang:{0}", ex.Message));
            }
        }
        
        public int Upd_Disable_ByCode_ByLang(string Code, bool NewDisable, int IDLang) 
        {
            try
            {
                List<DetailsTable> aListDetailsTable = aDatabaseDA.DetailsTable.Where(b => b.Code == Code).Where(z => z.IDLang == IDLang).ToList();
                if (aListDetailsTable.Count > 0)
                {
                    aListDetailsTable[0].Disable = NewDisable;
                    aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable[0]);
                    return aDatabaseDA.SaveChanges();

                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Upd_Disable_ByCode_ByLang:{0}", ex.Message));
            }
        }


        //IDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDIDID#
        //NgocBM

        public int Ins(DetailsTable aDetailsTable)
        {
            try
            {
                aDatabaseDA.DetailsTable.AddOrUpdate(aDetailsTable);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Ins: {0}", ex.Message));
            }
        }
        public int Ins(ref List<DetailsTable> aListDetailsTable)
        {
            try
            {
                aListDetailsTable = aDatabaseDA.DetailsTable.AddRange(aListDetailsTable).ToList();
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Ins: {0}", ex.Message));
            }
        }
        public int Upd(List<DetailsTable> aListDetailsTable)
        {
            try
            {
                aDatabaseDA.DetailsTable.AddOrUpdate(aListDetailsTable.ToArray());
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Ins: {0}", ex.Message));
            }
        }
        public int Upd(DetailsTable aDetailsTable)
        {
            try
            {
                aDatabaseDA.DetailsTable.AddOrUpdate(aDetailsTable);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Upd: {0}", ex.Message));
            }
        }

        public int Del(List<DetailsTable> aListDetailsTable)
        {
            try
            {
                aDatabaseDA.DetailsTable.RemoveRange(aListDetailsTable);
                return aDatabaseDA.SaveChanges();
                

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Del: {0}", ex.Message));
            }
        }

        public int Del_ByCode(string Code)
        {
            try
            {
                List<DetailsTable> aTemp = aDatabaseDA.DetailsTable.Where(p => p.Code == Code).ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.DetailsTable.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Del_ByCode: {0}", ex.Message));
            }
        }
        public int Del_ByCode_ByIDLang(string Code, int IDLang)
        {
            try
            {
                List<DetailsTable> aTemp = aDatabaseDA.DetailsTable.Where(p => p.Code == Code).Where(p => p.IDLang == IDLang).ToList();
                if (aTemp.Count >0)
                {
                    aDatabaseDA.DetailsTable.Remove(aTemp[0]);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Del_ByCode_ByIDLang: {0}", ex.Message));
            }
        }
        public int Del_By_ListCode(List<string> aListCode)
        {
            try
            {
                List<DetailsTable> aTemp = aDatabaseDA.DetailsTable.Where(p => aListCode.Contains(p.Code)).ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.DetailsTable.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.Del_ByCode: {0}", ex.Message));
            }
        }



        //NgocBM
        public DetailsTable Sel_ByCode_ByIDLang(string Code, int IDLang)
        {
            try
            {
            
                List<DetailsTable> aListDetailsTable = new List<DetailsTable>();


                aListDetailsTable = aDatabaseDA.DetailsTable.Where(a => a.Code == Code).Where(a => a.IDLang == IDLang).ToList();
                if (aListDetailsTable.Count > 0)
                {
                    return aListDetailsTable[0];
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.SelectListDetailsTable_ByCode_ByCategoryL1: {0}", ex.Message));
            }
        }
        public DetailsTable Sel_ByCode_ByIDLang(string Code, int IDLang, bool Disable)
        {
            try
            {
             
                List<DetailsTable> aListDetailsTable = new List<DetailsTable>();


                aListDetailsTable = aDatabaseDA.DetailsTable.Where(a => a.Code == Code).Where(a => a.IDLang == IDLang).Where(a => a.Disable == Disable).ToList();
                if (aListDetailsTable.Count > 0)
                {
                    return aListDetailsTable[0];
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("DetailsTableBO.SelectListDetailsTable_ByCode_ByCategoryL1: {0}", ex.Message));
            }
        }
        //==========================================================================================================================


    }
}

