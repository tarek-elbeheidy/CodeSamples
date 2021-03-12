using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
  
    public class RequestStatus
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string EnglishStatus { get; set; }
        public string ArabicStatus { get; set; }
        public string SelectedID { get; set; }
        public string SelectedTitle { get; set; }


        public string ApplicantDescriptionAr { get; set; }
        public string ApplicantDescriptionEn { get; set; }
        public string ReviewerDescriptionAr { get; set; }
        public string ReviewerDescriptionEn { get; set; }
        public bool CanApplicantEditRequest { get; set; }
        public bool CanReviewerEditRequest { get; set; }
        public bool IsCommentRequired { get; set; }
        public string TargetPageUrl { get; set; }
        public string HistoryDescriptionEn { get; set; }
        public string HistoryDescriptionAr { get; set; }
        
                   
        public string ReviewerTargetPageURL { get; set; }
        public string TargetEmailGroup { get; set; }
        public string FinalDecisionAr { get; set; }
        public string FinalDecisionEn { get; set; }
        public bool CanDeleteRequest { get; set; }
        public string ApplicantRequestPhaseAr { get; set; }
        public string ApplicantRequestPhaseEn { get; set; }
        public string ReviewerRequestPhaseAr { get; set; }
        public string ReviewerRequestPhaseEn { get; set; }
        public string ActionByEn { get; set; }
        public string ActionByAr { get; set; }
    }
}
