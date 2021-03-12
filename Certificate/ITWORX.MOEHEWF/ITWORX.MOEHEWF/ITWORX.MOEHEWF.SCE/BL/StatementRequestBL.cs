using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.SCE.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
    class StatementRequestBL
    {

        public static List<StatementRequest> GetStatementRequestsList(int requestID,int lan)
        {
            Logging.GetInstance().Debug("Entering method StatementRequestBL.GetStatementRequestsList");
            List<StatementRequest> List = new List<StatementRequest>();

            try
            {
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    var agancyList = ctx.StatementAgencyList.ToList();
                    List = ctx.SCEStatementsRequestsList.ScopeToFolder("", true)
                              .Where(x => x.RequestID.Id == requestID)
                              .Select(x => new StatementRequest()
                              {
                                  ID = (int)x.Id,
                              
                                  RequestID = requestID,
                              
                                  StatementDate = x.StatementDate != null ? ExtensionMethods.QatarFormatedDate((DateTime)x.StatementDate) : null,
                              
                                  Sender = x.Sender,
                              
                                  StatementSubject = x.StatementSubject,
                              
                                  RequiredStatement = x.RequiredStatement,
                              
                                  StatementAgency = lan == (int)Language.English ? x.StatementAgency.Title : agancyList.Where(xx => xx.Id == x.StatementAgency.Id).FirstOrDefault().TitleAr,
                              
                                  ReplayDate = x.ReplyDate != null ? ExtensionMethods.QatarFormatedDate((DateTime)x.ReplyDate) : null,
                              
                                  StatementReplay = x.StatementReply,
                              
                                  ReplaySender = x.ReplySender
                              }).OrderByDescending(x=>x.ID)
                              .ToList();


                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method StatementRequestBL.GetStatementRequestsList");
            }
            return List;
        }

        public static StatementRequest GetStatementByID(int id, int lan)
        {
            Logging.GetInstance().Debug("Entering method StatementRequestBL.GetStatementRequestsList");
            StatementRequest List = new StatementRequest();

            try
            {
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    var agancyList = ctx.StatementAgencyList.ToList();
                    List = ctx.SCEStatementsRequestsList.ScopeToFolder("", true)
                              .Where(x => x.Id == id)
                              .Select(x => new StatementRequest()
                              {
                                  ID = (int)x.Id,

                                  RequestID = (int)x.RequestID.Id,

                                  StatementDate = x.StatementDate != null ? ExtensionMethods.QatarFormatedDate((DateTime)x.StatementDate) : null,

                                  Sender = x.Sender,

                                  StatementSubject = x.StatementSubject,

                                  RequiredStatement = x.RequiredStatement,

                                  StatementAgency = lan == (int)Language.English ? x.StatementAgency.Title : agancyList.Where(xx => xx.Id == x.StatementAgency.Id).FirstOrDefault().TitleAr,

                                  ReplayDate = x.ReplyDate != null ? ExtensionMethods.QatarFormatedDate((DateTime)x.ReplyDate) : null,

                                  StatementReplay = x.StatementReply,

                                  ReplaySender = x.ReplySender
                              })
                              .FirstOrDefault();


                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method StatementRequestBL.GetStatementRequestsList");
            }
            return List;
        }
    }
}
