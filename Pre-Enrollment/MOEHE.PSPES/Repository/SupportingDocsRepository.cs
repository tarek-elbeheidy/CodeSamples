using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
   public class SupportingDocsRepository
    {

        public static async Task<DBOperationResult> Insert(SupportingDocsModel supportingDocsModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/SupportingDocs/", supportingDocsModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch
                {
                    //this mean service down (Server may be changed)
                    //MessageID will be Error

                }
                return ReturnedResult;
            }
        }
        public static DocumentTypeList GetDocumentTypeByID(string ID,string CurrentSiteURL)
        {
           
            DocumentTypeList DocumentType = new DocumentTypeList();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite CurrentSite = new SPSite(CurrentSiteURL))
                    {
                        using (SPWeb web = CurrentSite.OpenWeb())
                        {

                            SPList list = web.Lists["documentTypeList"];
                            foreach (SPListItem item in list.Items)
                            {
                                if (Convert.ToBoolean(item["DocumentTypeStatus"]) && item["ID"].ToString() == ID)
                                {
                                    DocumentType.DocumentTypeName = item["EnglishNameDocumentType"].ToString();
                                    DocumentType.ArabicDocumentTypeName = item["ArabicNameDocumentType"].ToString();
                                    DocumentType.DocumentTypeID = int.Parse(ID);


                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            { }
            return DocumentType;
        }
        
       

        public static CurriculumModel GetCurrilculmName(List<ListOfValues_Model> list, string ID)
        {
            CurriculumModel CurrilculmModel = new CurriculumModel();

            CurrilculmModel = list.Where(S => S.ID == ID).Select(S => new CurriculumModel {  ArabicCurriculumName = S.DescriptionArabic,  EnglishCurriculumName = S.DescriptionEnglish,  CurriculumID = S.ID }).FirstOrDefault();
            return CurrilculmModel;
        }

        public static List<DocumentTypeList> GetDocumentTypeList(string Lang)
        {
            List<DocumentTypeList> AllDocumentTypes = new List<DocumentTypeList>();
            using (SPSite CurrentSite = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb web = CurrentSite.OpenWeb())
                {

                    SPList list = web.Lists["documentTypeList"];
                    foreach (SPListItem item in list.Items)
                    {
                        if (Convert.ToBoolean(item["DocumentTypeStatus"]))
                        {
                            if (Lang == PSPESConstants.ArabicLanguage)
                            {
                                AllDocumentTypes.Add(new DocumentTypeList { DocumentTypeName = item["ArabicNameDocumentType"].ToString(), DocumentTypeID = int.Parse(item["ID"].ToString()) });
                            }
                            else
                            {
                                AllDocumentTypes.Add(new DocumentTypeList { DocumentTypeName = item["EnglishNameDocumentType"].ToString(), DocumentTypeID = int.Parse(item["ID"].ToString()) });



                            }
                        }
                    }
                }
            }
            return AllDocumentTypes;
        }

        public static async Task<List<SupportingDocsModel>> GetSupportingDocs(MOE_SCHOOL_Model SchoolInfo,SupportingDocsModel supportingDocsModel,bool ShowEnglish,bool IsMinistryUser,bool IsSchoolUser, string CurrentSiteURL)
        {
            List<ListOfValues_Model> AllCurricullms = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
            List<SupportingDocsModel> SchoolsWithDocumentName = new List<SupportingDocsModel>();

            List<SupportingDocsModel> Schools = new List<SupportingDocsModel>();
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/GetSupportingDocs/", supportingDocsModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Schools = await res.Content.ReadAsAsync<List<SupportingDocsModel>>();
                    }
                
                    //foreach (var D in Schools)
                    //{
                    //    SupportingDocsModel supportingDocsModel1 = new SupportingDocsModel();

                    //    supportingDocsModel1.ID = D.ID;
                    //    supportingDocsModel1.DocumentTypeID = D.DocumentTypeID;
                    //    supportingDocsModel1.ArabicDocumentType = GetDocumentTypeByID(D.DocumentTypeID.ToString(),CurrentSiteURL).ArabicDocumentTypeName;
                    //    supportingDocsModel1.CreateDate = D.CreateDate;
                    //    supportingDocsModel1.CreatedBy = D.CreatedBy;
                    //    supportingDocsModel1.Curriculum = D.Curriculum;
                    //    supportingDocsModel1.EnglishDocumentType = GetDocumentTypeByID(D.DocumentTypeID.ToString(),CurrentSiteURL).DocumentTypeName;
                    //    supportingDocsModel1.Grade = D.Grade;
                    //    supportingDocsModel1.IsRequiredForPSO = D.IsRequiredForPSO;
                    //    supportingDocsModel1.EnableSchoolRequired = !(bool)D.IsRequiredForPSO;
                    //    supportingDocsModel1.IsRequiredForSchool = ((bool)D.IsRequiredForSchool || (bool)D.IsRequiredForPSO);
                    //    supportingDocsModel1.ModifiedBy = D.ModifiedBy;
                    //    supportingDocsModel1.ModifiedDate = D.ModifiedDate;
                    //    supportingDocsModel1.SchoolCode = D.SchoolCode;
                    //    supportingDocsModel1.SchoolName = D.SchoolName;
                    //    supportingDocsModel1.ShowArabic = !ShowEnglish;
                    //    supportingDocsModel1.ShowEnglish = ShowEnglish;
                    //    supportingDocsModel1.Term = D.Term;
                    //    SchoolsWithDocumentName.Add(supportingDocsModel);


                    //}

                    SchoolsWithDocumentName = Schools.Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        ArabicDocumentType = GetDocumentTypeByID(D.DocumentTypeID.ToString(), CurrentSiteURL).ArabicDocumentTypeName,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        Curriculum = GetCurrilculmName(AllCurricullms, D.CurriculumID).EnglishCurriculumName,
                        EnglishDocumentType = GetDocumentTypeByID(D.DocumentTypeID.ToString(), CurrentSiteURL).DocumentTypeName,
                        Grade = D.Grade,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        EnableSchoolRequired = IsSchoolUser&&!((bool)D.IsRequiredForPSO), EnableMinistryRequired=IsMinistryUser,
                        IsRequiredForSchool = ((bool)D.IsRequiredForSchool || (bool)D.IsRequiredForPSO),
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                        SchoolCode = D.SchoolCode,
                        SchoolName = SchoolInfo.MOE_SCHOOL_NAME_ENG,
                        ShowArabic = !ShowEnglish,
                        ShowEnglish = ShowEnglish,
                        Term = D.Term,
                         ArabicSchoolName= SchoolInfo.MOE_SCHOOL_NAME_ARA,
                          ArabicCurriculum=GetCurrilculmName(AllCurricullms, D.CurriculumID).ArabicCurriculumName,
                           CurriculumID=D.CurriculumID
                         
                        
                    }).ToList();
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return SchoolsWithDocumentName;
            }
        }

        public static async Task<List<SupportingDocsModel>> GetSupportingDocs( SupportingDocsModel supportingDocsModel, bool ShowEnglish, bool IsMinistryUser, string CurrentSiteURL)
        {
            List<ListOfValues_Model> AllCurricullms = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
            List<SupportingDocsModel> SchoolsWithDocumentName = new List<SupportingDocsModel>();

            List<SupportingDocsModel> Schools = new List<SupportingDocsModel>();
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/GetSupportingDocs/", supportingDocsModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Schools = await res.Content.ReadAsAsync<List<SupportingDocsModel>>();
                    }

                    //foreach (var D in Schools)
                    //{
                    //    SupportingDocsModel supportingDocsModel1 = new SupportingDocsModel();

                    //    supportingDocsModel1.ID = D.ID;
                    //    supportingDocsModel1.DocumentTypeID = D.DocumentTypeID;
                    //    supportingDocsModel1.ArabicDocumentType = GetDocumentTypeByID(D.DocumentTypeID.ToString(),CurrentSiteURL).ArabicDocumentTypeName;
                    //    supportingDocsModel1.CreateDate = D.CreateDate;
                    //    supportingDocsModel1.CreatedBy = D.CreatedBy;
                    //    supportingDocsModel1.Curriculum = D.Curriculum;
                    //    supportingDocsModel1.EnglishDocumentType = GetDocumentTypeByID(D.DocumentTypeID.ToString(),CurrentSiteURL).DocumentTypeName;
                    //    supportingDocsModel1.Grade = D.Grade;
                    //    supportingDocsModel1.IsRequiredForPSO = D.IsRequiredForPSO;
                    //    supportingDocsModel1.EnableSchoolRequired = !(bool)D.IsRequiredForPSO;
                    //    supportingDocsModel1.IsRequiredForSchool = ((bool)D.IsRequiredForSchool || (bool)D.IsRequiredForPSO);
                    //    supportingDocsModel1.ModifiedBy = D.ModifiedBy;
                    //    supportingDocsModel1.ModifiedDate = D.ModifiedDate;
                    //    supportingDocsModel1.SchoolCode = D.SchoolCode;
                    //    supportingDocsModel1.SchoolName = D.SchoolName;
                    //    supportingDocsModel1.ShowArabic = !ShowEnglish;
                    //    supportingDocsModel1.ShowEnglish = ShowEnglish;
                    //    supportingDocsModel1.Term = D.Term;
                    //    SchoolsWithDocumentName.Add(supportingDocsModel);


                    //}

                    SchoolsWithDocumentName = Schools.Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        ArabicDocumentType = GetDocumentTypeByID(D.DocumentTypeID.ToString(), CurrentSiteURL).ArabicDocumentTypeName,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        //Curriculum = GetCurrilculmName(AllCurricullms, D.CurriculumID).EnglishCurriculumName,
                        EnglishDocumentType = GetDocumentTypeByID(D.DocumentTypeID.ToString(), CurrentSiteURL).DocumentTypeName,
                        Grade = D.Grade,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        EnableSchoolRequired = !(bool)D.IsRequiredForPSO || IsMinistryUser,
                        IsRequiredForSchool = ((bool)D.IsRequiredForSchool || (bool)D.IsRequiredForPSO),
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                        SchoolCode = D.SchoolCode,
                     
                        ShowArabic = !ShowEnglish,
                        ShowEnglish = ShowEnglish,
                        Term = D.Term,
              
                        //ArabicCurriculum = GetCurrilculmName(AllCurricullms, D.CurriculumID).ArabicCurriculumName,
                        CurriculumID = D.CurriculumID


                    }).ToList();
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return SchoolsWithDocumentName;
            }
        }

    }
}
