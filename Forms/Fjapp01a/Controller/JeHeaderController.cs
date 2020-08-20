using Alio.Common.Runtime.Controller;
using Alio.Forms.Common.DbServices;
using Alio.Forms.Fjapp01a.Model;
using Alio.Libraries.Sutl01a;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.AppSupportLib.UI;
using Foundations.Core.Types;
using System;
using System.Data;

namespace Alio.Forms.Fjapp01a.Controllers
{


    public class JeHeaderController : BaseBlockController<Fjapp01aTask, Fjapp01aModel>
    {

        public JeHeaderController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER JE_HEADER.POST-QUERY
        /*
         declare 
cursor get_disapprovals (reference_no_cur varchar2) is 
select disapproval_message  
from fas.journal_disapproval 
where  reference_no = reference_no_cur; 

cursor get_name is 
select last_name||', '||first_name user_name 
  from hrs.employee_master em, 
       adm.users           u 
 where u.user_id     = :je_header.user_id 
   and u.employee_no = em.employee_no; 


begin 

:je_header.user_id := :global.qtsi_user_id;

:je_header.disapproval_message := null; 
for disapproval_rec in get_disapprovals( :je_header.reference_no) 
loop 
:je_header.disapproval_message:= disapproval_rec.disapproval_message; 
end loop; 


for name_rec in get_name 
loop 
:je_header.user_name := name_rec.user_name; 
end loop; 
:je_header.user_name:= nvl(:je_header.user_name, :je_header.user_id); 

get_additional_info;  

populate_batch_totals; 


end; 
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:31
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.POST-QUERY</TriggerInfo>

        [AfterQuery]
        public virtual void jeHeader_AfterQuery(RowAdapterEventArgs args)
        {
            JeHeaderAdapter jeHeaderElement = args.Row as JeHeaderAdapter;

            int rowCount = 0;
            {
                String sqlgetDisapprovals = "SELECT disapproval_message " +
" FROM fas.journal_disapproval " +
" WHERE reference_no = @P_REFERENCE_NO_CUR ";
                DataCursor getDisapprovals = new DataCursor(sqlgetDisapprovals);
                String getDisapprovalsReferenceNoCur = string.Empty;
                String sqlgetName = "SELECT last_name || ', ' || first_name user_name " +
" FROM hrs.employee_master em, adm.users u " +
" WHERE u.user_id = @JE_HEADER_USER_ID AND u.employee_no = em.employee_no ";
                DataCursor getName = new DataCursor(sqlgetName);
                jeHeaderElement.UserId = Alio.Common.Globals.QtsiUserId;
                jeHeaderElement.DisapprovalMessage = null;
                getDisapprovalsReferenceNoCur = jeHeaderElement.ReferenceNo;
                //Setting query parameters
                getDisapprovals.AddParameter("@P_REFERENCE_NO_CUR", getDisapprovalsReferenceNoCur);
                #region loop for cursor getDisapprovals
                try
                {
                    getDisapprovals.Open();
                    while (true)
                    {
                        TableRow disapprovalRec = getDisapprovals.FetchRow();
                        if (getDisapprovals.NotFound) break;
                        jeHeaderElement.DisapprovalMessage = disapprovalRec.GetString("disapproval_message");
                    }
                }
                finally
                {
                    getDisapprovals.Close();
                }
                #endregion
                //Setting query parameters
                getName.AddParameter("@JE_HEADER_USER_ID", jeHeaderElement.UserId);
                #region loop for cursor getName
                try
                {
                    getName.Open();
                    while (true)
                    {
                        TableRow nameRec = getName.FetchRow();
                        if (getName.NotFound) break;
                        jeHeaderElement.UserName = nameRec.GetString("user_name").ToString();
                    }
                }
                finally
                {
                    getName.Close();
                }
                #endregion
                jeHeaderElement.UserName = Lib.IsNull(jeHeaderElement.UserName, jeHeaderElement.UserId).ToString();
                this.Task.Services.GetAdditionalInfo(jeHeaderElement);
                this.Task.Services.PopulateBatchTotals();
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.PRE-DELETE
        /*
         -- 
-- Begin default relation program section 
-- 
BEGIN 
-- 
-- Begin JE_DATA detail program section 
-- 
DELETE FROM FAS.JE_DATA 
WHERE REFERENCE_NO = :JE_HEADER.REFERENCE_NO; 
-- 
-- End JE_DATA detail program section 
-- 
END; 
-- 
-- End default relation program section 
-- 

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:32
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.PRE-DELETE</TriggerInfo>

        [RowDeleting]
        public virtual void jeHeader_RowDeleting(RowAdapterEventArgs args)
        {
            //  
            //  Begin default relation program section 
            //  
            JeHeaderAdapter jeHeaderElement = args.Row as JeHeaderAdapter;


            int rowCount = 0;
            //  
            //  Begin JE_DATA detail program section 
            //  
            #region Execute data command
            String sql1 = "DELETE FROM FAS.JE_DATA " +
" WHERE REFERENCE_NO = @JE_HEADER_REFERENCE_NO";
            DataCommand command1 = new DataCommand(sql1);
            //Setting query parameters
            command1.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
            rowCount = command1.Execute();
            #endregion
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.ON-POPULATE-DETAILS
        /*
         -- 
-- Begin default relation declare section 
-- 
DECLARE 
recstat     VARCHAR2(20) := :System.record_status;   
startitm    VARCHAR2(61) := :System.cursor_item;   
rel_id      Relation; 

cursor cr_total_cur is 
select sum(je_data.journal_amount) 
from fas.je_data 
where debit_credit_flag = '-1' and 
     :je_header.reference_no = je_data.reference_no; 

cursor dr_total_cur is 
select sum(je_data.journal_amount) 
from fas.je_data 
where debit_credit_flag = ' 1' and 
     :je_header.reference_no = je_data.reference_no; 

-- 
-- End default relation declare section 
-- 
-- 
-- Begin default relation program section 
-- 
BEGIN 
IF ( recstat = 'NEW' or recstat = 'INSERT' ) THEN   
RETURN; 
END IF; 
-- 
-- Begin JE_DATA detail program section 
-- 
IF ( (:JE_HEADER.REFERENCE_NO is not null) ) THEN   
rel_id := Find_Relation('JE_HEADER.HEADER_DATA');   
Query_Master_Details(rel_id, 'JE_DATA');   
END IF; 
-- 
-- End JE_DATA detail program section 
-- 

IF ( :System.cursor_item <> startitm ) THEN     
 Go_Item(startitm);     
 Check_Package_Failure;     
END IF; 

--   open  cr_total_cur; 
--   fetch cr_total_cur into :totals.cr_total;                             
--   close cr_total_cur; 

--   open  dr_total_cur; 
--   fetch dr_total_cur into :totals.dr_total;                             
--   close dr_total_cur; 

END; 
-- 
-- End default relation program section 
-- 
--
-- Begin default relation declare section
--
DECLARE
recstat     VARCHAR2(20) := :System.record_status;   
startitm    VARCHAR2(61) := :System.cursor_item;   
rel_id      Relation;
--
-- End default relation declare section
--
--
-- Begin default relation program section
--
BEGIN
IF ( recstat = 'NEW' or recstat = 'INSERT' ) THEN   
RETURN;
END IF;
--
-- Begin JOURNAL_APPROVAL detail program section
--
IF ( (:JE_HEADER.REFERENCE_NO is not null) ) THEN   
rel_id := Find_Relation('JE_HEADER.JE_HEADER_JOURNAL_APPROVAL');   
Query_Master_Details(rel_id, 'JOURNAL_APPROVAL');   
END IF;
--
-- End JOURNAL_APPROVAL detail program section
--

IF ( :System.cursor_item <> startitm ) THEN     
 Go_Item(startitm);     
 Check_Package_Failure;     
END IF;
END;
--
-- End default relation program section
--

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:33
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.ON-POPULATE-DETAILS</TriggerInfo>

        [PopulateDetails]
        public virtual void jeHeader_PopulateDetails(RowAdapterEventArgs args)
        {
            JeHeaderAdapter jeHeader = args.Row as JeHeaderAdapter;

            if (jeHeader.RowState == DataRowState.Added || jeHeader.RowState == DataRowState.Detached)
                return;

            Model.JeData.LoadData();

            Model.JournalApproval.LoadData();
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.WHEN-NEW-RECORD-INSTANCE
        /*
         begin 

populate_batch_totals; 

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:34
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>JE_HEADER.WHEN-NEW-RECORD-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-RECORD-INSTANCE", Function = KeyFunction.RECORD_CHANGE)]
        public virtual void jeHeader_recordChange()
        {
            this.Task.Services.PopulateBatchTotals();
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.ON-CHECK-DELETE-MASTER
        /*
         --
-- Begin default relation declare section
--
DECLARE
Dummy_Define CHAR(1);
--
-- Begin JOURNAL_APPROVAL detail declare section
--
CURSOR JOURNAL_APPROVAL_cur IS      
SELECT 1 FROM FAS.JOURNAL_APPROVAL F     
WHERE F.REFERENCE_NO = :JE_HEADER.REFERENCE_NO;
--
-- End JOURNAL_APPROVAL detail declare section
--
--
-- End default relation declare section
--
--
-- Begin default relation program section
--
BEGIN
--
-- Begin JOURNAL_APPROVAL detail program section
--
OPEN JOURNAL_APPROVAL_cur;     
FETCH JOURNAL_APPROVAL_cur INTO Dummy_Define;     
IF ( JOURNAL_APPROVAL_cur%found ) THEN     
Message('Cannot delete master record when matching detail records exist.');     
CLOSE JOURNAL_APPROVAL_cur;     
RAISE Form_Trigger_Failure;     
END IF;
CLOSE JOURNAL_APPROVAL_cur;
--
-- End JOURNAL_APPROVAL detail program section
--
END;
--
-- End default relation program section
--

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:35
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.ON-CHECK-DELETE-MASTER</TriggerInfo>

        [DeleteDetails]
        public virtual void jeHeader_DeleteDetails(RowAdapterEventArgs args)
        {
            // 
            //  Begin default relation declare section
            // 
            JeHeaderAdapter jeHeaderElement = args.Row as JeHeaderAdapter;


            int rowCount = 0;
            {
                string dummyDefine = string.Empty;
                String sqljournalApprovalCur = "SELECT 1 " +
" FROM FAS.JOURNAL_APPROVAL F " +
" WHERE F.REFERENCE_NO = @JE_HEADER_REFERENCE_NO ";
                DataCursor journalApprovalCur = new DataCursor(sqljournalApprovalCur);
                #region
                try
                {
                    // 
                    //  Begin JOURNAL_APPROVAL detail program section
                    // 
                    //Setting query parameters
                    journalApprovalCur.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
                    journalApprovalCur.Open();
                    ResultSet journalApprovalCurResults = journalApprovalCur.FetchInto();
                    if (journalApprovalCurResults != null)
                    {
                        dummyDefine = journalApprovalCurResults.GetString(0);
                    }
                    if ((journalApprovalCur.Found))
                    {
                        TaskServices.Message("Cannot delete master record when matching detail records exist.");
                        journalApprovalCur.Close();
                        throw new ApplicationException();
                    }
                }
                finally
                {
                    journalApprovalCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER SHOW_LOV.WHEN-BUTTON-PRESSED
        /*
         BEGIN 
go_item('je_header.reference_no'); 
do_key('list_values'); 
END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:13
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>SHOW_LOV.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "SHOW_LOV")]
        public virtual void showLov_buttonClick()
        {
            ItemServices.GoItem("je_header.reference_no");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER APPROVE.WHEN-BUTTON-PRESSED
        /*
         declare 
alert_number number; 

begin 
delete fas.journal_disapproval 
where reference_no  = :je_header.reference_no; 


commit; 

fjapp01a_.approve_next_level( :je_header.reference_no,
                            user,
                            'Y'); -- Y approve next level is on

if :system.last_record <> 'TRUE' then 
 next_record; 
else 

set_alert_property('ON_MESSAGE',title,'All Records Processed'); 
  set_alert_property('ON_MESSAGE', 
                     alert_message_text, 
                     'This is the Last Record.'); 
  alert_number := show_alert('ON_MESSAGE'); 
 -- raise form_trigger_failure; 

    sutl01a.close_window; -- dph 9/18/15 
end if; 
end;                                               
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:20
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>APPROVE.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "APPROVE")]
        public virtual void approve_buttonClick()
        {
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;

            int rowCount = 0;
            {
                NNumber alertNumber = NNumber.Null;
                #region Execute data command
                String sql1 = "DELETE FROM fas.journal_disapproval " +
" WHERE reference_no = @JE_HEADER_REFERENCE_NO";
                DataCommand command1 = new DataCommand(sql1);
                //Setting query parameters
                command1.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
                rowCount = command1.Execute();
                #endregion
                TaskServices.Commit();

                Fjapp01a_.ApproveNextLevel(jeHeaderElement.ReferenceNo, DbManager.User, "Y");
                //  Y approve next level is on
                if (!BlockServices.InLastRecord)
                {
                    BlockServices.NextRecord(true);
                }
                else
                {
                    MessageServices.SetAlertTitle("ON_MESSAGE", "All Records Processed");
                    MessageServices.SetAlertMessageText("ON_MESSAGE", "This is the Last Record.");
                    alertNumber = TaskServices.ShowAlert("ON_MESSAGE");
                    //  raise form_trigger_failure; 
                    PkgSutl01a.CloseWindow();
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER DISAPPROVE.WHEN-BUTTON-PRESSED
        /*
         DECLARE 
-- logic copied from fbapp02a.fmx
alert_number number; 
v_name       varchar2(100); 
v_sequence   number; 

cursor get_name is 
select last_name||', '||first_name user_name 
  from hrs.employee_master em, 
       adm.users           u 
 where u.user_id = :global.qtsi_user_id 
  and  u.employee_no = em.employee_no; 

BEGIN 

delete fas.journal_disapproval 
WHERE reference_no = :JE_HEADER.reference_no; 

open get_name; 
fetch get_name into v_name; 
close get_name; 

v_name := nvl(v_name,:global.qtsi_user_id); 

:je_header.disapproval_message := 'Disapproved by: '||v_name|| 
                                    '.   '||:je_header.disapproval_message; 

insert into fas.journal_disapproval(reference_no, 
                                  disapproval_message, 
                                  disapproval_date) 
                           values(:je_header.reference_no, 
                                  :je_header.disapproval_message, 
                                   sysdate);     

update fas.je_header 
  set approval_status = 'DR'    -- disapprove 
where reference_no  = :je_header.reference_no;
commit; 

select max(approver_sequence) 
into v_sequence 
from fas.journal_approval 
where reference_no  = :je_header.reference_no
 and status_flag   = 'W'; 

if v_sequence > 1 then 

 v_sequence := v_sequence - 1; 

 update fas.journal_approval 
 set status_flag = decode(approver_sequence,v_sequence, 'W', 
                                                        null) 
 where reference_no  = :je_header.reference_no
   and approver_sequence >= v_sequence; 

end if; 

if :system.last_record <> 'TRUE' then 
  next_record; 
else 
set_alert_property('ON_MESSAGE',title,'All Records Processed'); 
  set_alert_property('ON_MESSAGE', 
                     alert_message_text, 
                     'This is the Last Record.'); 
  alert_number := show_alert('ON_MESSAGE'); 
 -- raise form_trigger_failure; -- dph 9/18/15 
     sutl01a.close_window;      -- dph 9/18/15 
end if; 

END; 



        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:22
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>DISAPPROVE.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "DISAPPROVE")]
        public virtual void disapprove_buttonClick()
        {
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;

            int rowCount = 0;
            {
                //  logic copied from fbapp02a.fmx
                NNumber alertNumber = NNumber.Null;
                string vName = string.Empty;
                NNumber vSequence = NNumber.Null;
                String sqlgetName = "SELECT last_name || ', ' || first_name user_name " +
" FROM hrs.employee_master em, adm.users u " +
" WHERE u.user_id = @GLOBAL_QTSI_USER_ID AND u.employee_no = em.employee_no ";
                DataCursor getName = new DataCursor(sqlgetName);
                #region
                try
                {
                    #region Execute data command
                    String sql1 = "DELETE FROM fas.journal_disapproval " +
" WHERE reference_no = @JE_HEADER_REFERENCE_NO";
                    DataCommand command1 = new DataCommand(sql1);
                    //Setting query parameters
                    command1.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
                    rowCount = command1.Execute();
                    #endregion
                    //Setting query parameters
                    getName.AddParameter("@GLOBAL_QTSI_USER_ID", Alio.Common.Globals.QtsiUserId);
                    getName.Open();
                    ResultSet getNameResults = getName.FetchInto();
                    if (getNameResults != null)
                    {
                        vName = getNameResults.GetString(0);
                    }
                    vName = Lib.IsNull(vName, Alio.Common.Globals.QtsiUserId).ToString();
                    jeHeaderElement.DisapprovalMessage = "Disapproved by: " + vName + ".   " + jeHeaderElement.DisapprovalMessage;
                    #region Execute data command
                    String sql2 = "INSERT INTO fas.journal_disapproval " +
"(reference_no, disapproval_message, disapproval_date)" +
"VALUES (@JE_HEADER_REFERENCE_NO, @JE_HEADER_DISAPPROVAL_MESSAGE, sysdate)";
                    DataCommand command2 = new DataCommand(sql2);
                    //Setting query parameters
                    command2.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
                    command2.AddParameter("@JE_HEADER_DISAPPROVAL_MESSAGE", jeHeaderElement.DisapprovalMessage);
                    rowCount = command2.Execute();
                    #endregion
                    #region Execute data command
                    String sql3 = "UPDATE fas.je_header " +
"SET approval_status = 'DR' " +
" WHERE reference_no = @JE_HEADER_REFERENCE_NO";
                    DataCommand command3 = new DataCommand(sql3);
                    //Setting query parameters
                    command3.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
                    rowCount = command3.Execute();
                    #endregion
                    TaskServices.Commit();
                    #region Execute data command
                    String sql4 = "SELECT max(approver_sequence) " +
" FROM fas.journal_approval " +
" WHERE reference_no = @JE_HEADER_REFERENCE_NO AND status_flag = 'W' ";
                    DataCommand command4 = new DataCommand(sql4);
                    //Setting query parameters
                    command4.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
                    ResultSet results4 = command4.ExecuteQuery();
                    rowCount = command4.RowCount;
                    if (results4.HasData)
                    {
                        vSequence = results4.GetNumber(0);
                    }
                    results4.Close();
                    #endregion
                    if (vSequence > 1)
                    {
                        vSequence = vSequence - 1;
                        #region Execute data command
                        String sql5 = "UPDATE fas.journal_approval " +
"SET status_flag = decode(approver_sequence, @P_V_SEQUENCE, 'W', NULL) " +
" WHERE reference_no = @JE_HEADER_REFERENCE_NO AND approver_sequence >= @P_V_SEQUENCE";
                        DataCommand command5 = new DataCommand(sql5);
                        //Setting query parameters
                        command5.AddParameter("@P_V_SEQUENCE", vSequence);
                        command5.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
                        rowCount = command5.Execute();
                        #endregion
                    }
                    if (!BlockServices.InLastRecord)
                    {
                        BlockServices.NextRecord(true);
                    }
                    else
                    {
                        MessageServices.SetAlertTitle("ON_MESSAGE", "All Records Processed");
                        MessageServices.SetAlertMessageText("ON_MESSAGE", "This is the Last Record.");
                        alertNumber = TaskServices.ShowAlert("ON_MESSAGE");
                        //  raise form_trigger_failure; -- dph 9/18/15 
                        PkgSutl01a.CloseWindow();
                    }
                }
                finally
                {
                    getName.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER DISAPPROVAL_EDIT.WHEN-BUTTON-PRESSED
        /*
         GO_ITEM ('je_header.disapproval_message'); 
EDIT_TEXTITEM; 

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:25
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>DISAPPROVAL_EDIT.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "DISAPPROVAL_EDIT")]
        public virtual void disapprovalEdit_buttonClick()
        {
            ItemServices.GoItem("je_header.disapproval_message");
            TaskServices.EditTextitem();
        }



        #endregion

    }
}
