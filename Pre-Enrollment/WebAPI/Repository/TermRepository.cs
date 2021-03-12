using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class TermRepository
    {
        public static List<TermModel> Get()
        {
            List<TermModel> AllTerms = new List<TermModel>();
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                AllTerms = DB.MOE_TERM.Select(D => new TermModel
                {
                    TermCode = D.MOE_TERM_CODE.ToString(),
                    TermName = D.MOE_TERM_DESCRIPTION_ENG.ToString(),
                    IsActive = (bool)D.MOE_ISOPEN_ENROLLMENT,
                    ACADEMIC_YEAR_DESC = D.MOE_ACADEMIC_YEAR_DESC
                }).ToList();
            }
            return AllTerms;
        }


    }
}