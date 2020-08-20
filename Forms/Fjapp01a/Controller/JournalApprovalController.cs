using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjapp01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Exceptions;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using System;

namespace Alio.Forms.Fjapp01a.Controllers
{


    public class JournalApprovalController : BaseBlockController<Fjapp01aTask, Fjapp01aModel>
    {

        public JournalApprovalController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER JOURNAL_APPROVAL.POST-QUERY
        /*

begin

begin
select approval_description INTO :journal_approval.app_code_desc
from shr.approval_code_master acm
where acm.approval_code = :journal_approval.approval_code;
exception
  when others then
    :journal_approval.app_code_desc := NULL;
end;

if  :journal_approval.status_flag   = 'W' then
   :journal_approval.status_description    := 'WAITING';
elsif :journal_approval.status_flag = 'A' then
   :journal_approval.status_description  := 'APPROVED'; 
end if;

end;	

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:67
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JOURNAL_APPROVAL.POST-QUERY</TriggerInfo>

        [AfterQuery]
        public void journalApproval_AfterQuery(RowAdapterEventArgs args)
        {
            JournalApprovalAdapter journalApprovalElement = args.Row as JournalApprovalAdapter;

            int rowCount = 0;
            try
            {
                #region Execute data command
                String sql1 = "SELECT approval_description " +
" FROM shr.approval_code_master acm " +
" WHERE acm.approval_code = @JOURNAL_APPROVAL_APPROVAL_CODE ";
                DataCommand command1 = new DataCommand(sql1);
                //Setting query parameters
                command1.AddParameter("@JOURNAL_APPROVAL_APPROVAL_CODE", journalApprovalElement.ApprovalCode);
                ResultSet results1 = command1.ExecuteQuery();
                rowCount = command1.RowCount;
                if (results1.HasData)
                {
                    journalApprovalElement.AppCodeDesc = results1.GetString(0);
                }
                results1.Close();
                #endregion
            }
            catch (TaskControlException) { throw; }
            catch
            {
                journalApprovalElement.AppCodeDesc = null;
            }
            if (journalApprovalElement.StatusFlag == "W")
            {
                journalApprovalElement.StatusDescription = "WAITING";
            }
            else if (journalApprovalElement.StatusFlag == "A")
            {
                journalApprovalElement.StatusDescription = "APPROVED";
            }
        }



        #endregion

    }
}
