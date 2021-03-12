using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class SupportingDocsRepository
    {

        public static SupportingDocsModel GetBy(SupportingDocsModel supportingDocsModel)
        {
            SupportingDocsModel supportingDoc = new SupportingDocsModel();

            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == supportingDocsModel.Term && D.SchoolCode == supportingDocsModel.SchoolCode && D.Grade == supportingDocsModel.Grade && D.CurriculumID == supportingDocsModel.CurriculumID && D.DocumentTypeID == supportingDocsModel.DocumentTypeID)).Select(D => new SupportingDocsModel
                {
                    ID = D.ID,
                    DocumentTypeID = D.DocumentTypeID,
                    CurriculumID = D.CurriculumID,
                    Grade = D.Grade,
                    SchoolCode = D.SchoolCode,
                    Term = D.Term,
                    IsRequiredForSchool = D.IsRequiredForSchool,
                    CreateDate = D.CreateDate,
                    CreatedBy = D.CreatedBy,
                    IsRequiredForPSO = D.IsRequiredForPSO,
                    ModifiedBy = D.ModifiedBy,
                    ModifiedDate = D.ModifiedDate,
                }).FirstOrDefault();
                    





            }

            return supportingDoc;

        }



        public static List<SupportingDocsModel> GetBy(string Term,string SchoolCode,string Grade,string CurriculumID)
        {
            List<SupportingDocsModel> supportingDoc = new List<SupportingDocsModel>();

            
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                if (Grade!="")
                {

                    if ((SchoolCode == "All"|| SchoolCode == null) && (CurriculumID == "All"|| CurriculumID == null))
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.Grade == Grade)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,

                        }).ToList();

                    }
                    else if ((SchoolCode == "All"||SchoolCode == null) && (CurriculumID != "All"|| CurriculumID != null))
                    {
                      supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term  && D.Grade == Grade &&D.CurriculumID==CurriculumID)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,

                        }).ToList();
                    }
                    else if ((SchoolCode != "All" || SchoolCode != null) &&( CurriculumID == null || CurriculumID == "All"))

                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == SchoolCode && D.Grade == Grade )).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,

                        }).ToList();
                    }

                    else
                    {

                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == SchoolCode && D.Grade == Grade &&D.CurriculumID==CurriculumID)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,

                        }).ToList();
                    }

                }
                else
                {
                    if ((SchoolCode == "All" || SchoolCode == null) && (CurriculumID == "All" || CurriculumID == null))
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,

                        }).ToList();

                    }
                    else if ((SchoolCode == "All" || SchoolCode == null) && (CurriculumID != "All" || CurriculumID != null))
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term  && D.CurriculumID == CurriculumID)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,

                        }).ToList();
                    }
                    else if ((SchoolCode != "All" || SchoolCode != null) && (CurriculumID == null || CurriculumID == "All"))
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == SchoolCode )).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,

                        }).ToList();
                    }

                    else
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == SchoolCode)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,

                        }).ToList();
                    }

                }





            }

            return supportingDoc;

        }


        public static List<SupportingDocsModel> GetBy(string Term, string SchoolCode, string Grade,int DocumentTypeID,string CurriculumID)
        {
            List<SupportingDocsModel> supportingDoc = new List<SupportingDocsModel>();


            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                if (SchoolCode == "All" && CurriculumID == "All")
                {
                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.Grade == Grade && D.DocumentTypeID == DocumentTypeID) ).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,


                    }).ToList();

                }
                else if (SchoolCode == "All" && CurriculumID != "All")
                {
                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term  && D.Grade == Grade && D.DocumentTypeID == DocumentTypeID && D.CurriculumID == CurriculumID)).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,


                    }).ToList();
                }
                else if (SchoolCode != "All" && CurriculumID == "All")
                {
                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == SchoolCode && D.Grade == Grade && D.DocumentTypeID == DocumentTypeID )).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,


                    }).ToList();

                }

                else
                {
                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == SchoolCode && D.Grade == Grade && D.DocumentTypeID == DocumentTypeID &&D.CurriculumID==CurriculumID)).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,


                    }).ToList();


                }
            }

            return supportingDoc;

        }

        public static List<SupportingDocsModel> GetBy(string Term, string SchoolCode, int DocumentTypeID,string CurriculumID)
        {
            List<SupportingDocsModel> supportingDoc = new List<SupportingDocsModel>();


            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                if (SchoolCode == "All" && CurriculumID == "All")
                {
                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.DocumentTypeID == DocumentTypeID )).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                    }).ToList();

                }
                else if (SchoolCode == "All" && CurriculumID != "All")
                {
                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term  && D.DocumentTypeID == DocumentTypeID && D.CurriculumID == CurriculumID)).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                    }).ToList();
                }
                else if (SchoolCode != "All" && CurriculumID == "All")
                {
                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == SchoolCode && D.DocumentTypeID == DocumentTypeID )).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                    }).ToList();
                }

                else
                {
                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == SchoolCode && D.DocumentTypeID == DocumentTypeID &&D.CurriculumID==CurriculumID)).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                    }).ToList();
                }


            }

            return supportingDoc;

        }

        /// <summary>
        /// Note : if you want all grades send grade with All if you want all document types send document type id 0
        /// </summary>
        /// <param name="Term"></param>
        /// <param name="schoolCode"></param>
        /// <param name="Grade"></param>
        /// <param name="Curriculum"></param>
        /// <param name="DocumentTypeID"></param>
        /// <returns></returns>
        public static List<SupportingDocsModel> Get(string Term, string schoolCode, string Grade, string CurriculumID,int? DocumentTypeID)
        {
            
            List<SupportingDocsModel> supportingDoc = new List<SupportingDocsModel>();

            if (Grade=="All" && DocumentTypeID==0)
            {
                //Then load all schools Document type for all grades

               
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();

                    if ((schoolCode == "All"|| schoolCode == null) && (CurriculumID == "All"|| CurriculumID == null))
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.DocumentTypeID == DocumentTypeID && D.Grade == Grade)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,


                        }).ToList();

                    }
                    else if ((schoolCode == "All"|| schoolCode == null) && (CurriculumID != "All"|| CurriculumID != null))
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.CurriculumID==CurriculumID&& D.Term == Term && D.DocumentTypeID == DocumentTypeID && D.Grade == Grade)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,


                        }).ToList();
                    }
                    else if ((schoolCode != "All"|| schoolCode != null) && (CurriculumID == "All"|| CurriculumID == null))
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where( D => (D.SchoolCode==schoolCode && D.Term == Term && D.DocumentTypeID == DocumentTypeID && D.Grade == Grade)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,


                        }).ToList();
                    }
                   
                    else
                    {
                        supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Term == Term && D.SchoolCode == schoolCode && D.DocumentTypeID == DocumentTypeID && D.Grade == Grade && D.CurriculumID == CurriculumID)).Select(D => new SupportingDocsModel
                        {
                            ID = D.ID,
                            DocumentTypeID = D.DocumentTypeID,
                            CurriculumID = D.CurriculumID,
                            Grade = D.Grade,
                            SchoolCode = D.SchoolCode,
                            Term = D.Term,
                            IsRequiredForSchool = D.IsRequiredForSchool,
                            CreateDate = D.CreateDate,
                            CreatedBy = D.CreatedBy,
                            IsRequiredForPSO = D.IsRequiredForPSO,
                            ModifiedBy = D.ModifiedBy,
                            ModifiedDate = D.ModifiedDate,


                        }).ToList();
                    }
                }
            }
            else if (Grade != "All" && DocumentTypeID != 0)
            {
                //Then load by specific schools Document type for specific grades
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();

                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Grade == Grade && D.DocumentTypeID == DocumentTypeID && D.Term == Term && D.SchoolCode == schoolCode && D.CurriculumID == CurriculumID)).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                    }).ToList();

                }

            }
            else if (Grade == "All")
            {
                //Then load specific schools Document type for all grades
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();

                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.DocumentTypeID == DocumentTypeID && D.Term == Term && D.SchoolCode == schoolCode && D.CurriculumID == CurriculumID)).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                    }).ToList();

                }
            }
            else
            {
                //Then load All schools Document type for specific grade
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();

                    supportingDoc = DB.SupportingDocumentSetups.Where(D => (D.Grade == Grade && D.Term == Term && D.SchoolCode == schoolCode && D.CurriculumID == CurriculumID)).Select(D => new SupportingDocsModel
                    {
                        ID = D.ID,
                        DocumentTypeID = D.DocumentTypeID,
                        CurriculumID = D.CurriculumID,
                        Grade = D.Grade,
                        SchoolCode = D.SchoolCode,
                        Term = D.Term,
                        IsRequiredForSchool = D.IsRequiredForSchool,
                        CreateDate = D.CreateDate,
                        CreatedBy = D.CreatedBy,
                        IsRequiredForPSO = D.IsRequiredForPSO,
                        ModifiedBy = D.ModifiedBy,
                        ModifiedDate = D.ModifiedDate,
                    }).ToList();

                }
            }

         

            return supportingDoc;

        }

        public static DBOperationResult Insert(SupportingDocsModel supportingDocsModel)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
               // int CurrentSupportingDocID = 0;
               List< SupportingDocsModel> CheckIfExit = Get(supportingDocsModel.Term, supportingDocsModel.SchoolCode, supportingDocsModel.Grade, supportingDocsModel.CurriculumID,supportingDocsModel.DocumentTypeID);
                if (CheckIfExit.Count> 0)
                {
                    if (supportingDocsModel.IsUpdateAllowed)
                    {



                        //then update
                        using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                        {
                            DB.Database.Connection.Open();


                            foreach (var item in CheckIfExit)
                            {


                                SupportingDocumentSetup supportingDocumentForUpdate = DB.SupportingDocumentSetups.Where(D => D.ID == item.ID).FirstOrDefault();

                                supportingDocumentForUpdate.IsRequiredForPSO = supportingDocsModel.IsRequiredForPSO;
                                supportingDocumentForUpdate.IsRequiredForSchool = supportingDocsModel.IsRequiredForSchool;
                                supportingDocumentForUpdate.ModifiedBy = supportingDocsModel.ModifiedBy;
                                supportingDocumentForUpdate.ModifiedDate = supportingDocsModel.ModifiedDate;
                                if ((bool)supportingDocumentForUpdate.IsRequiredForPSO)
                                {
                                    //if the document required by PSO it will be froce required also for school
                                    supportingDocumentForUpdate.IsRequiredForPSO = supportingDocsModel.IsRequiredForPSO;
                                    supportingDocumentForUpdate.IsRequiredForSchool = supportingDocsModel.IsRequiredForPSO;
                                }




                                DB.SaveChanges();
                            }
                        }
                    }

                }
                else
                {
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();
                        SupportingDocumentSetup supportingDocument = new SupportingDocumentSetup
                        {
                            CreateDate = DateTime.Now,
                            CreatedBy = supportingDocsModel.CreatedBy,
                            CurriculumID = supportingDocsModel.CurriculumID,
                            DocumentTypeID = supportingDocsModel.DocumentTypeID,
                            Grade = supportingDocsModel.Grade,
                            IsRequiredForPSO = supportingDocsModel.IsRequiredForPSO,
                            IsRequiredForSchool = supportingDocsModel.IsRequiredForSchool,
                            ModifiedBy = supportingDocsModel.ModifiedBy,
                            ModifiedDate = supportingDocsModel.ModifiedDate,
                            SchoolCode = supportingDocsModel.SchoolCode,
                           
                            Term = supportingDocsModel.Term,
                        };

                        DB.SupportingDocumentSetups.Add(supportingDocument);
                        DB.SaveChanges();
                        ReturnValue.InsertedID = supportingDocument.ID;

                    }

                }



            }

            catch (Exception ex)
            {




            }

            return ReturnValue;



        }

    }
}