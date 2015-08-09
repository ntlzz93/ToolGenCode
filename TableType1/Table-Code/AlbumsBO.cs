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
    public  class AlbumsBO
    {

        DatabaseDA aDatabaseDA = new DatabaseDA();
        

		public List<Albums> Sel_ByIDLang( int IDLang)
        {
            try
            {
                return aDatabaseDA.Albums.Where(z => z.IDLang == IDLang).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByIDLang:{0}", ex.Message));
            }
        }
		public List<Albums> Sel_ByIDLang( int IDLang, bool Disable)
        {
            try
            {
                return aDatabaseDA.Albums.Where(z => z.IDLang == IDLang).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByIDLang:{0}", ex.Message));
            }
        }
        public List<Albums> Sel_ByType_ByIDLang(int Type, int IDLang)
        {
            try
            {
                return aDatabaseDA.Albums.Where(p => p.Type == Type).Where(z => z.IDLang == IDLang).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByType:{0}", ex.Message));
            }
        }
        public List<Albums> Sel_ByType_ByIDLang(int Type, int IDLang, bool Disable)
        {
            try
            {
                return aDatabaseDA.Albums.Where(p => p.Type == Type).Where(z => z.IDLang == IDLang).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByType:{0}", ex.Message));
            }
        }

        
     
        public List<Albums> Sel_ByStatus_ByIDLang(int Status, int IDLang)
        {
            try
            {
                return aDatabaseDA.Albums.Where(p => p.Status == Status).Where(z => z.IDLang == IDLang).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByStatus:{0}", ex.Message));
            }
        }
        public List<Albums> Sel_ByStatus_ByIDLang(int Status, int IDLang, bool Disable)
        {
            try
            {
                return aDatabaseDA.Albums.Where(p => p.Status == Status).Where(z => z.IDLang == IDLang).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByStatus:{0}", ex.Message));
            }
        }

        public List<Albums> Sel_ByType_ByStatus_ByIDLang(int Type, int Status, int IDLang)
        {
            try
            {
                return aDatabaseDA.Albums.Where(c => c.Type == Type).Where(p => p.Status == Status).Where(z => z.IDLang == IDLang).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByType_ByStatus_ByIDLang:{0}", ex.Message));
            }
        }
        public List<Albums> Sel_ByType_ByStatus_ByIDLang_ByDisable(int Type, int Status, int IDLang, bool Disable)
        {
            try
            {
                return aDatabaseDA.Albums.Where(c => c.Type == Type).Where(p => p.Status == Status).Where(z => z.IDLang == IDLang).Where(z => z.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByType_ByStatus_ByIDLang:{0}", ex.Message));
            }
        }

        public List<Albums> Sel_ByCode(string Code)
        {
            try
            {
                return aDatabaseDA.Albums.Where(p => p.Code == Code).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByCode: {0}", ex.Message));
            }
        }
        public List<Albums> Sel_ByCode(string Code, bool Disable)
        {
            try
            {
                return aDatabaseDA.Albums.Where(p => p.Code == Code).Where(p => p.Disable == Disable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel_ByCode: {0}", ex.Message));
            }
        }

        
        public int Del()
        {
            try
            {
                List<Albums> aTemp = aDatabaseDA.Albums.ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.Albums.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Del_ByCode: {0}", ex.Message));
            }
        }
		
       
        public int Upd_Status_ByID(int ID, int NewStatus) 
        {
            try
            {
                List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.ID == ID).ToList();
                if (aListAlbums.Count > 0) 
                {
                   
                        aListAlbums[0].Status = NewStatus;
                        aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Status_ByID:{0}", ex.Message));
            }
        }
        
        public int Upd_Type_ByID(int ID, int NewType) 
        {
            try
            {
                List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.ID == ID).ToList();
                if (aListAlbums.Count > 0) 
                {
                   
                        aListAlbums[0].Type = NewType;
                        aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
               
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Type_ByID:{0}", ex.Message));
            }
        }

        
        public int Upd_Disable_ByID(int ID,bool NewDisable) 
        {
            try
            {
                List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.ID == ID).ToList();
                if (aListAlbums.Count > 0) 
                {
                   
                        aListAlbums[0].Disable = NewDisable;
                        aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
                        return aDatabaseDA.SaveChanges();
                    
                }
                return 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(String.Format("Upd_Disable_ByID:{0}", ex.Message));
            }
        }
		
		public int Del_ByID(Int32 ID)
        {
            try
            {
                List<Albums> aTemp = aDatabaseDA.Albums.Where(p=>p.ID == ID).ToList();
           
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.Albums.Remove(aTemp[0]);
                    return aDatabaseDA.SaveChanges();
                }
                else
                {
                    throw new Exception(String.Format("AlbumsBO.Del: {0}", "Không tìm thấy Albums có ID = " + ID));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Del: {0}", ex.Message));
            }
        }
		
        public int Del_By_ListID(List<int> aListID)
        {
            try
            {
                List<Albums> aTemp = aDatabaseDA.Albums.Where(p => aListID.Contains(int.Parse(p.ID.ToString()))).ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.Albums.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Del_ByCode: {0}", ex.Message));
            }
        }

        public  Albums Sel_ByID(Int32 ID)
        {
            try
            {
                Albums aAlbums = aDatabaseDA.Albums.Where(b => b.ID == ID).ToList()[0];
              
                    return aAlbums;
               
                
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Sel: {0}", ex.Message));
            }
        }
        

        public int Upd_Status_ByCode(string Code, int NewStatus) 
        {
            try
            {
                List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.Code==Code).ToList();
                if (aListAlbums.Count > 0)
                {
                   
                        aListAlbums[0].Status = NewStatus;
                        aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
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
                List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.Code==Code).ToList();
                if (aListAlbums.Count > 0)
                {
                   
                        aListAlbums[0].Type = NewType;
                        aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
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
                 List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.Code==Code).ToList();
                 if (aListAlbums.Count > 0)
                  {
                        aListAlbums[0].Disable = NewDisable;
                        aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
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
                List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.Code == Code).Where(z=>z.IDLang==IDLang).ToList();
                if (aListAlbums.Count > 0)
                {
                    aListAlbums[0].Status = NewStatus;
                    aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
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
                List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.Code == Code).Where(z => z.IDLang == IDLang).ToList();
                if (aListAlbums.Count > 0)
                {
                    aListAlbums[0].Type = NewType;
                    aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
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
                List<Albums> aListAlbums = aDatabaseDA.Albums.Where(b => b.Code == Code).Where(z => z.IDLang == IDLang).ToList();
                if (aListAlbums.Count > 0)
                {
                    aListAlbums[0].Disable = NewDisable;
                    aDatabaseDA.Albums.AddOrUpdate(aListAlbums[0]);
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

        public int Ins(Albums aAlbums)
        {
            try
            {
                aDatabaseDA.Albums.AddOrUpdate(aAlbums);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Ins: {0}", ex.Message));
            }
        }
        public int Ins(ref List<Albums> aListAlbums)
        {
            try
            {
                aListAlbums = aDatabaseDA.Albums.AddRange(aListAlbums).ToList();
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Ins: {0}", ex.Message));
            }
        }
        public int Upd(List<Albums> aListAlbums)
        {
            try
            {
                aDatabaseDA.Albums.AddOrUpdate(aListAlbums.ToArray());
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Ins: {0}", ex.Message));
            }
        }
        public int Upd(Albums aAlbums)
        {
            try
            {
                aDatabaseDA.Albums.AddOrUpdate(aAlbums);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Upd: {0}", ex.Message));
            }
        }

        public int Del(List<Albums> aListAlbums)
        {
            try
            {
                aDatabaseDA.Albums.RemoveRange(aListAlbums);
                return aDatabaseDA.SaveChanges();
                

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Del: {0}", ex.Message));
            }
        }

        public int Del_ByCode(string Code)
        {
            try
            {
                List<Albums> aTemp = aDatabaseDA.Albums.Where(p => p.Code == Code).ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.Albums.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Del_ByCode: {0}", ex.Message));
            }
        }
        public int Del_ByCode_ByIDLang(string Code, int IDLang)
        {
            try
            {
                List<Albums> aTemp = aDatabaseDA.Albums.Where(p => p.Code == Code).Where(p => p.IDLang == IDLang).ToList();
                if (aTemp.Count >0)
                {
                    aDatabaseDA.Albums.Remove(aTemp[0]);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Del_ByCode_ByIDLang: {0}", ex.Message));
            }
        }
        public int Del_By_ListCode(List<string> aListCode)
        {
            try
            {
                List<Albums> aTemp = aDatabaseDA.Albums.Where(p => aListCode.Contains(p.Code)).ToList();
                if (aTemp.Count > 0)
                {
                    aDatabaseDA.Albums.RemoveRange(aTemp);
                    return aDatabaseDA.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.Del_ByCode: {0}", ex.Message));
            }
        }



        //NgocBM
        public Albums Sel_ByCode_ByIDLang(string Code, int IDLang)
        {
            try
            {
            
                List<Albums> aListAlbums = new List<Albums>();


                aListAlbums = aDatabaseDA.Albums.Where(a => a.Code == Code).Where(a => a.IDLang == IDLang).ToList();
                if (aListAlbums.Count > 0)
                {
                    return aListAlbums[0];
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.SelectListAlbums_ByCode_ByCategoryL1: {0}", ex.Message));
            }
        }
        public Albums Sel_ByCode_ByIDLang(string Code, int IDLang, bool Disable)
        {
            try
            {
             
                List<Albums> aListAlbums = new List<Albums>();


                aListAlbums = aDatabaseDA.Albums.Where(a => a.Code == Code).Where(a => a.IDLang == IDLang).Where(a => a.Disable == Disable).ToList();
                if (aListAlbums.Count > 0)
                {
                    return aListAlbums[0];
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("AlbumsBO.SelectListAlbums_ByCode_ByCategoryL1: {0}", ex.Message));
            }
        }
        //==========================================================================================================================


    }
}

