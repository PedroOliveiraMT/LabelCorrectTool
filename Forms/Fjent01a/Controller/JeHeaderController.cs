using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjent01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.AppSupportLib.UI;
using Foundations.Core.Types;
using System;
using System.Data;

namespace Alio.Forms.Fjent01a.Controllers
{


    public class JeHeaderController : BaseBlockController<Fjent01aTask, Fjent01aModel>
    {

        public JeHeaderController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
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

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:43
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
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.WHEN-NEW-RECORD-INSTANCE
        /*
         begin 

:batch_no_block.save_flag := 'N'; 
populate_batch_totals; 

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:44
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>JE_HEADER.WHEN-NEW-RECORD-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-RECORD-INSTANCE", Function = KeyFunction.RECORD_CHANGE)]
        public virtual void jeHeader_recordChange()
        {
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;

            Model.BatchNoBlock.SaveFlag = "N";
            this.Task.Services.PopulateBatchTotals(jeHeaderElement);
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.POST-QUERY
        /*
         declare
 cursor order_disapproval_cur (cur_in_reference_no varchar2) is
select disapproval_message
 from fas.journal_disapproval
where reference_no = cur_in_reference_no
order by disapproval_date desc;
order_disapproval_rec order_disapproval_cur%rowtype;

cursor get_approval_cursor (cur_in_reference_no varchar2) is 
select approval_code,min(approver_sequence)
 from fas.journal_approval
where reference_no = cur_in_reference_no and
      status_flag = 'W'
group by approver_sequence,approval_code;
get_approval_rec get_approval_cursor%rowtype;

cursor auto_approve (cur_in_reference_no varchar2) is 
select count(1)
 from fas.journal_approval
where reference_no = cur_in_reference_no;

BEGIN 

populate_batch_totals; 

-- dph alio-14278
-- 06/12/17 psr alio-15315 No longer using display_reference_no
--:je_header.display_reference_no := nvl(:je_header.whs_order_no, :je_header.reference_no);

if :parameter.journal_approval = 'Y' then

if :je_header.approval_status is null then
  :je_header.approval_status_desc := 'No Approvals Found for Reference';
else 

 if :je_header.approval_status in ('EN', 'RQ') then -- reset below
      if :je_header.ready_for_approval = 'Y' then                                 --alio-14824 mfl
         if :je_header.debit_total  is not null then
          :je_header.approval_status_desc := 'Needs Approval by';
         else
            :je_header.approval_status_desc := 'No Accounting';
         end if;
    else                                                                  --alio-14824 mfl
         if :je_header.debit_total  is not null then                        --alio-14824 mfl
          :je_header.approval_status_desc := 'Pending Release';           --alio-14824 mfl
         else                                                               --alio-14824 mfl
            :je_header.approval_status_desc := 'No Accounting';             --alio-14824 mfl
         end if;                                                            --alio-14824 mfl
      end if;                                                               --alio-14824 mfl 
 elsif :je_header.approval_status = 'AP' then
    :je_header.approval_status_desc := 'Approved Ready to Post';
 elsif :je_header.approval_status = 'RP' then
    :je_header.approval_status_desc := 'Ready to Print';
 elsif :je_header.approval_status = 'PO' then
    :je_header.approval_status_desc := 'Posted to Journals';
    --Disable Item and Account tabs
 elsif :je_header.approval_status in('DB','IP','PR' )then
      :je_header.approval_status_desc := 'Printed, Ready to post';
    --Disable Item and Account tabs
 elsif :je_header.approval_status like 'C%' then
      :je_header.approval_status_desc := 'Printed, Ready to post';        
 elsif :je_header.approval_status = 'ER' then

      :je_header.approval_status_desc := 'Accounting Error';
 elsif :je_header.approval_status = 'DR' then
    open order_disapproval_cur(:je_header.reference_no);
    fetch order_disapproval_cur into order_disapproval_rec;
    close order_disapproval_cur;
    :je_header.approval_status_desc := order_disapproval_rec.disapproval_message;
 end if;
end if; 


if :je_header.approval_status IN ('EN','RQ') 
  and :je_header.debit_total is not null 
  and :je_header.ready_for_approval = 'Y' then                             --alio-14824 mfl
      open get_approval_cursor (:je_header.reference_no);
      fetch get_approval_cursor into get_approval_rec;

      if get_approval_cursor%notfound then
         for cur_rec in  auto_approve(:je_header.reference_no)
         loop
             if cur_rec.count > 0 then
               :je_header.approval_status_desc := 'Unfinished Save'; -- should never get needs approved by blank
             else
               :je_header.approval_status_desc := 'Approved, Ready for Edit/Print';
             end if;  
         end loop;

      elsif get_approval_cursor%found then
         :je_header.approval_status_desc := 
         :je_header.approval_status_desc || ' ' || get_approval_rec.approval_code;
         if get_approval_rec.approval_code = 'PO' then
            :je_header.approval_status_desc := 'Approved, Ready for Edit/Print';
         end if;
      end if;
      close get_approval_cursor;  
 end if; -- approval_status is null

end if; -- parameter = 'Y'	  


END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:45
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
                String sqlorderDisapprovalCur = "SELECT disapproval_message " +
" FROM fas.journal_disapproval " +
" WHERE reference_no = @P_CUR_IN_REFERENCE_NO " +
" ORDER BY disapproval_date DESC";
                DataCursor orderDisapprovalCur = new DataCursor(sqlorderDisapprovalCur);
                String orderDisapprovalCurCurInReferenceNo = string.Empty;
                TableRow orderDisapprovalRec = null;
                String sqlgetApprovalCursor = "SELECT approval_code, min(approver_sequence) " +
" FROM fas.journal_approval " +
" WHERE reference_no = @P_CUR_IN_REFERENCE_NO AND status_flag = 'W' " +
" GROUP BY approver_sequence, approval_code ";
                DataCursor getApprovalCursor = new DataCursor(sqlgetApprovalCursor);
                String getApprovalCursorCurInReferenceNo = string.Empty;
                TableRow getApprovalRec = null;
                String sqlautoApprove = "SELECT count(1)  AS COUNT " +
" FROM fas.journal_approval " +
" WHERE reference_no = @P_CUR_IN_REFERENCE_NO ";
                DataCursor autoApprove = new DataCursor(sqlautoApprove);
                String autoApproveCurInReferenceNo = string.Empty;
                #region
                try
                {
                    this.Task.Services.PopulateBatchTotals(jeHeaderElement);
                    //  dph alio-14278
                    //  06/12/17 psr alio-15315 No longer using display_reference_no
                    // :je_header.display_reference_no := nvl(:je_header.whs_order_no, :je_header.reference_no);
                    if (this.Model.Params.JournalApproval == "Y")
                    {
                        if (string.IsNullOrEmpty(jeHeaderElement.ApprovalStatus))
                        {
                            jeHeaderElement.ApprovalStatusDesc = "No Approvals Found for Reference";
                        }
                        else
                        {
                            if ((jeHeaderElement.ApprovalStatus == "EN" || jeHeaderElement.ApprovalStatus == "RQ"))
                            {
                                //  reset below
                                if (jeHeaderElement.ReadyForApproval == "Y")
                                {
                                    // alio-14824 mfl
                                    if (!jeHeaderElement.DebitTotal.IsNull)
                                    {
                                        jeHeaderElement.ApprovalStatusDesc = "Needs Approval by";
                                    }
                                    else
                                    {
                                        jeHeaderElement.ApprovalStatusDesc = "No Accounting";
                                    }
                                }
                                else
                                {
                                    // alio-14824 mfl
                                    if (!jeHeaderElement.DebitTotal.IsNull)
                                    {
                                        // alio-14824 mfl
                                        jeHeaderElement.ApprovalStatusDesc = "Pending Release";
                                    }
                                    else
                                    {
                                        // alio-14824 mfl
                                        jeHeaderElement.ApprovalStatusDesc = "No Accounting";
                                    }
                                }
                            }
                            else if (jeHeaderElement.ApprovalStatus == "AP")
                            {
                                jeHeaderElement.ApprovalStatusDesc = "Approved Ready to Post";
                            }
                            else if (jeHeaderElement.ApprovalStatus == "RP")
                            {
                                jeHeaderElement.ApprovalStatusDesc = "Ready to Print";
                            }
                            else if (jeHeaderElement.ApprovalStatus == "PO")
                            {
                                jeHeaderElement.ApprovalStatusDesc = "Posted to Journals";
                            }
                            else if ((jeHeaderElement.ApprovalStatus == "DB" || jeHeaderElement.ApprovalStatus == "IP" || jeHeaderElement.ApprovalStatus == "PR"))
                            {
                                jeHeaderElement.ApprovalStatusDesc = "Printed, Ready to post";
                            }
                            else if (Lib.Like(jeHeaderElement.ApprovalStatus, "C%"))
                            {
                                jeHeaderElement.ApprovalStatusDesc = "Printed, Ready to post";
                            }
                            else if (jeHeaderElement.ApprovalStatus == "ER")
                            {
                                jeHeaderElement.ApprovalStatusDesc = "Accounting Error";
                            }
                            else if (jeHeaderElement.ApprovalStatus == "DR")
                            {
                                orderDisapprovalCurCurInReferenceNo = jeHeaderElement.ReferenceNo;
                                //Setting query parameters
                                orderDisapprovalCur.AddParameter("@P_CUR_IN_REFERENCE_NO", orderDisapprovalCurCurInReferenceNo);
                                orderDisapprovalCur.Open();
                                orderDisapprovalRec = orderDisapprovalCur.FetchRow();
                                jeHeaderElement.ApprovalStatusDesc = orderDisapprovalRec.GetStr("disapproval_message");
                            }
                        }
                        if ((jeHeaderElement.ApprovalStatus == "EN" || jeHeaderElement.ApprovalStatus == "RQ") && !jeHeaderElement.DebitTotal.IsNull && jeHeaderElement.ReadyForApproval == "Y")
                        {
                            // alio-14824 mfl
                            getApprovalCursorCurInReferenceNo = jeHeaderElement.ReferenceNo;
                            //Setting query parameters
                            getApprovalCursor.AddParameter("@P_CUR_IN_REFERENCE_NO", getApprovalCursorCurInReferenceNo);
                            getApprovalCursor.Open();
                            getApprovalRec = getApprovalCursor.FetchRow();
                            if (getApprovalCursor.NotFound)
                            {
                                autoApproveCurInReferenceNo = jeHeaderElement.ReferenceNo;
                                //Setting query parameters
                                autoApprove.AddParameter("@P_CUR_IN_REFERENCE_NO", autoApproveCurInReferenceNo);
                                #region loop for cursor autoApprove
                                try
                                {
                                    autoApprove.Open();
                                    while (true)
                                    {
                                        TableRow curRec = autoApprove.FetchRow();
                                        if (autoApprove.NotFound) break;
                                        if (curRec.GetNumber("count") > 0)
                                        {
                                            jeHeaderElement.ApprovalStatusDesc = "Unfinished Save";
                                        }
                                        else
                                        {
                                            jeHeaderElement.ApprovalStatusDesc = "Approved, Ready for Edit/Print";
                                        }
                                    }
                                }
                                finally
                                {
                                    autoApprove.Close();
                                }
                                #endregion
                            }
                            else if (getApprovalCursor.Found)
                            {
                                jeHeaderElement.ApprovalStatusDesc = jeHeaderElement.ApprovalStatusDesc + " " + getApprovalRec.GetStr("approval_code");
                                if (getApprovalRec.GetStr("approval_code") == "PO")
                                {
                                    jeHeaderElement.ApprovalStatusDesc = "Approved, Ready for Edit/Print";
                                }
                            }
                        }
                    }
                }
                finally
                {
                    orderDisapprovalCur.Close();
                    getApprovalCursor.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.POST-INSERT
        /*
         begin 

populate_batch_totals; 
 if :parameter.journal_approval = 'Y' then
 build_approval_array(:je_header.reference_no);
 end if;

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:46
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.POST-INSERT</TriggerInfo>

        [AfterRowInsert]
        public virtual void jeHeader_AfterRowInsert(RowAdapterEventArgs args)
        {
            JeHeaderAdapter jeHeaderElement = args.Row as JeHeaderAdapter;


            this.Task.Services.PopulateBatchTotals(jeHeaderElement);
            if (this.Model.Params.JournalApproval == "Y")
            {
                this.Task.Services.BuildApprovalArray(jeHeaderElement.ReferenceNo);
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.POST-UPDATE
        /*
         begin 

populate_batch_totals; 
 if :parameter.journal_approval = 'Y' then
 build_approval_array(:je_header.reference_no);
 end if;
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:47
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.POST-UPDATE</TriggerInfo>

        [AfterRowUpdate]
        public virtual void jeHeader_AfterRowUpdate(RowAdapterEventArgs args)
        {
            JeHeaderAdapter jeHeaderElement = args.Row as JeHeaderAdapter;


            this.Task.Services.PopulateBatchTotals(jeHeaderElement);
            if (this.Model.Params.JournalApproval == "Y")
            {
                this.Task.Services.BuildApprovalArray(jeHeaderElement.ReferenceNo);
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.POST-BLOCK
        /*
         begin 
--cec alio-3074 
if(nvl(:je_header.reference_no, '-123xyzABC') <> nvl(:je_data.reference_no, '-321xyzABC')) then 
accounting_array.accounting_tab_init; 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:48
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>JE_HEADER.POST-BLOCK</TriggerInfo>

        [ActionTrigger(Action = "POST-BLOCK", Function = KeyFunction.BLOCK_OUT)]
        public virtual void jeHeader_blockOut()
        {

            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;

            // cec alio-3074 
            if ((Lib.IsNull(jeHeaderElement.ReferenceNo, "-123xyzABC") != Lib.IsNull(jeDataElement.ReferenceNo, "-321xyzABC")))
            {
                Task.Packages.AccountingArray.AccountingTabInit();
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.PRE-QUERY
        /*
         declare
 cursor get_ref_no (order_no_in varchar2) is
 select reference_no
   from fas.je_header 
  where whs_order_no = order_no_in;

v_reference_no varchar2(20);
begin

-- alio-14728 added if statement - the lookup is either by reference_no or whs_order_no
<multilinecomment> 06/12/17 psr alio-15315 No longer using display_reference_no
if nvl(:parameter.use_order_no,'N') = 'Y' then 

    -- v_reference_no := :je_header.display_reference_no;
    --for cur_rec in get_ref_no( :je_header.display_reference_no)
    loop
        v_reference_no := cur_rec.reference_no;
  end loop;

  :je_header.reference_no := v_reference_no;

end if;
</multilinecomment>

accounting_array.accounting_tab_init;--cec alio-3074


if :parameter.journal_approval = 'Y' then
   build_approval_chain;
end if;

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:49
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.PRE-QUERY</TriggerInfo>

        [BeforeQuery]
        public virtual void jeHeader_BeforeQuery(QueryEventArgs args)
        {
            int rowCount = 0;

            args.Source.SelectCommandParams.Add(DbManager.DataBaseFactory.CreateDataParameter("BATCH_NO_BLOCK_BATCH_YEAR", this.Model.BatchNoBlock.BatchYear));


            args.Source.SelectCommandParams.Add(DbManager.DataBaseFactory.CreateDataParameter("BATCH_NO_BLOCK_BATCH_NO", this.Model.BatchNoBlock.BatchNo));


            #endregion
            {
                String sqlgetRefNo = "SELECT reference_no " +
" FROM fas.je_header " +
" WHERE whs_order_no = @P_ORDER_NO_IN ";
                DataCursor getRefNo = new DataCursor(sqlgetRefNo);
                String getRefNoOrderNoIn = string.Empty;
                string vReferenceNo = string.Empty;
                //  alio-14728 added if statement - the lookup is either by reference_no or whs_order_no
                //  06/12/17 psr alio-15315 No longer using display_reference_no
                // if nvl(:parameter.use_order_no,'N') = 'Y' then
                // -- v_reference_no := :je_header.display_reference_no;
                // --for cur_rec in get_ref_no( :je_header.display_reference_no)
                // loop
                // v_reference_no := cur_rec.reference_no;
                // end loop;
                // :je_header.reference_no := v_reference_no;
                // end if;

                Task.Packages.AccountingArray.AccountingTabInit();

                // cec alio-3074
                if (this.Model.Params.JournalApproval == "Y")
                {
                    this.Task.Services.BuildApprovalChain();
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.PRE-INSERT
        /*
         begin
:je_header.system_owner := 'FAS'; -- dph alio-3109
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:50
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.PRE-INSERT</TriggerInfo>

        [RowInserting]
        public virtual void jeHeader_RowInserting(RowAdapterEventArgs args)
        {
            JeHeaderAdapter jeHeaderElement = args.Row as JeHeaderAdapter;


            jeHeaderElement.SystemOwner = "FAS";
        }

        [ValidationTrigger(Item = "READY_FOR_APPROVAL", FireInSearch = false)]
        public void ReadyForApprovalValidate()
        {
            JeHeaderItemChange();
        }

        [ValidationTrigger(Item = "JOURNAL_DATE", FireInSearch = false)]
        public void JournalDateValidate()
        {
            JeHeaderItemChange();
        }

        [ValidationTrigger(Item = "JOURNAL_DESCRIPTION", FireInSearch = false)]
        public void JournalDescriptionValidate()
        {
            JeHeaderItemChange();
        }

        [ValidationTrigger(Item = "REFERENCE_NO", FireInSearch = false)]
        public void ReferenceNoValidate()
        {
            JeHeaderItemChange();
        }
        public void JeHeaderItemChange()
        {
            if (TaskServices.RecordStatus == RecordStatus.NEW.ToString())
            {
                var value = Lib.NameIn(this.Model, TaskServices.TriggerItem);
                JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;
                var oldValue = jeHeaderElement.GetValue(TaskServices.TriggerItem);
                if (value == null | oldValue == null)
                {
                    if (value != null || oldValue != null)
                        BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "je_header", RecordStatus.INSERT);
                    else
                        return;
                }
                if (NString.ToStr(oldValue) != NString.ToStr(value))
                    BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "je_header", RecordStatus.INSERT);
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_HEADER.PRE-UPDATE
        /*
         begin

null;

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:51
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_HEADER.PRE-UPDATE</TriggerInfo>

        [RowUpdating]
        public virtual void jeHeader_RowUpdating(RowAdapterEventArgs args)
        {
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
-- 3109
if :parameter.journal_approval = 'Y' then 
DELETE FROM FAS.JOURNAL_APPROVAL
WHERE REFERENCE_NO = :JE_HEADER.REFERENCE_NO;
end if; 

hold.reference_table.delete;

END; 
-- 
-- End default relation program section 
-- 

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:52
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
            //  
            //  End JE_DATA detail program section 
            //  
            //  3109
            if (this.Model.Params.JournalApproval == "Y")
            {
                #region Execute data command
                String sql2 = "DELETE FROM FAS.JOURNAL_APPROVAL " +
" WHERE REFERENCE_NO = @JE_HEADER_REFERENCE_NO";
                DataCommand command2 = new DataCommand(sql2);
                //Setting query parameters
                command2.AddParameter("@JE_HEADER_REFERENCE_NO", jeHeaderElement.ReferenceNo);
                rowCount = command2.Execute();
                #endregion
            }
            Task.Packages.Hold.referenceTable.Clear();
        }


        #region Original PL/SQL code code for TRIGGER ASSIGN_REFERENCE.WHEN-NEW-ITEM-INSTANCE
        /*
         begin 
if :system.mode = 'ENTER-QUERY' then 
  go_item('je_header.reference_no'); 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:16
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ASSIGN_REFERENCE.WHEN-NEW-ITEM-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-ITEM-INSTANCE", Item = "ASSIGN_REFERENCE", Function = KeyFunction.ITEM_CHANGE)]
        public virtual void assignReference_itemChange()
        {
            if (TaskServices.Mode == "ENTER-QUERY")
            {
                ItemServices.GoItem("je_header.reference_no");
            }
        }


        #region Original PL/SQL code code for TRIGGER ASSIGN_REFERENCE.WHEN-BUTTON-PRESSED
        /*
         begin 
if :je_header.reference_no is null then 
  :je_header.reference_no 
     := amgen01a_.get_next_counter('JE_REFERENCE'); 

  -- 06/12/17 psr alio-15315 No longer using display_reference_no
  -- :je_header.display_reference_no := :je_header.reference_no;   
  go_item('je_header.journal_description'); 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:17
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ASSIGN_REFERENCE.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "ASSIGN_REFERENCE")]
        public virtual void assignReference_buttonClick()
        {

            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;


            if (string.IsNullOrEmpty(jeHeaderElement.ReferenceNo))
            {
                jeHeaderElement.ReferenceNo = Amgen01a_.GetNextCounter("JE_REFERENCE").ToString();
                foreach (var item in this.Model.JeData.Rows)
                {
                    item.ReferenceNo = jeHeaderElement.ReferenceNo;
                }
                
                if (TaskServices.RecordStatus == RecordStatus.NEW.ToString())
                    BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "je_header", RecordStatus.INSERT);

                //  06/12/17 psr alio-15315 No longer using display_reference_no
                //  :je_header.display_reference_no := :je_header.reference_no;   
                ItemServices.GoItem("je_header.journal_description");
            }
        }


        #region Original PL/SQL code code for TRIGGER JOURNAL_DATE.KEY-LISTVAL
        /*
         BEGIN 

date_lov.get_date(sysdate,                      -- initial date 
              'je_header.journal_date',       -- return block.item 
              240,                            -- window x position 
              60,                             -- window y position 
              'Journal Date',                 -- window title 
              'OK',                           -- ok button label 
              'Cancel',                       -- cancel button label 
              TRUE,                           -- highlight weekend days 
              FALSE,                          -- autoconfirm selection 
              FALSE);                         -- autoskip after selection 

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:20
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>JOURNAL_DATE.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "JOURNAL_DATE", Function = KeyFunction.LIST_VALUES)]
        public virtual void journalDate_ListValues()
        {
            //Task.Libraries.Calendar.DateLov.GetDate(NDate.Now, "je_header.journal_date", 240, 60, "Journal Date", "OK", "Cancel", true, false, false);
        }


        #region Original PL/SQL code code for TRIGGER JOURNAL_DATE.WHEN-MOUSE-DOUBLECLICK
        /*
         begin 
go_block('je_header'); 
go_record(to_number(:system.cursor_record)); 
go_item('je_header.journal_date'); 
do_key('list_values'); 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:21
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>JOURNAL_DATE.WHEN-MOUSE-DOUBLECLICK</TriggerInfo>

        [ActionTrigger(Action = "WHEN-MOUSE-DOUBLECLICK", Item = "JOURNAL_DATE")]
        public virtual void journalDate_doubleClick()
        {
            BlockServices.GoBlock("je_header");
            BlockServices.SetCurrentRecord(Lib.ToNumber(TaskServices.CursorRecord));
            ItemServices.GoItem("je_header.journal_date");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER SPREASDSHEET.WHEN-BUTTON-PRESSED
        /*
         declare
alert_status number;
v_message varchar2(500);
begin


v_message := 'Changes must be saved before opening in spreadsheet.';

SET_ALERT_PROPERTY ('spreadsheet_alert',
                    alert_message_text,
                    v_message);

if :system.form_status = 'CHANGED' then
alert_status := SHOW_ALERT ('spreadsheet_alert');
 IF alert_status = alert_button2 THEN
   return;
 ELSIF alert_status = alert_button1 THEN
     commit_form;
  IF :System.Form_Status <> 'QUERY' THEN 
    RAISE Form_Trigger_Failure; 
  END IF; 

 ELSE -- they closed the message box
     return;
END IF;
end if;

spreadsheet;   


end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:25
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>SPREASDSHEET.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "SPREASDSHEET")]
        public virtual void spreasdsheet_buttonClick()
        {

            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;

            {
                NNumber alertStatus = NNumber.Null;
                string vMessage = string.Empty;
                vMessage = "Changes must be saved before opening in spreadsheet.";
                MessageServices.SetAlertMessageText("spreadsheet_alert", vMessage);
                if (TaskServices.FormStatus == "CHANGED")
                {
                    alertStatus = TaskServices.ShowAlert("spreadsheet_alert");
                    if (alertStatus == MessageServices.BUTTON2)
                    {
                        return;
                    }
                    else if (alertStatus == MessageServices.BUTTON1)
                    {
                        TaskServices.Commit();
                        if (TaskServices.FormStatus != "QUERY")
                        {
                            throw new ApplicationException();
                        }
                    }
                    else
                    {
                        //  they closed the message box
                        return;
                    }
                }
                this.Task.Services.Spreadsheet(jeDataElement, jeHeaderElement);
            }
        }


        #region Original PL/SQL code code for TRIGGER VIEW_BUTTON.WHEN-BUTTON-PRESSED
        /*
         begin 
go_item('je_header.approval_status_desc'); 
EDIT_TEXTITEM;
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:28
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>VIEW_BUTTON.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "VIEW_BUTTON")]
        public virtual void viewButton_buttonClick()
        {
            ItemServices.GoItem("je_header.approval_status_desc");
            TaskServices.EditTextitem();
        }


        #region Original PL/SQL code code for TRIGGER DISPLAY_REFERENCE_NO.WHEN-VALIDATE-ITEM
        /*
         -- 06/12/17 psr alio-15315 No longer using display_reference_no
Null;
<multilinecomment>
-- 06/01/17 psr alio-15301  Added for PK.  This is required when reference_no is hand keyed.
--                          Required in this trigger for je_data block
If :je_header.reference_no is Null Then
:je_header.reference_no := :je_header.display_reference_no;
End if;
</multilinecomment>
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:38
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>DISPLAY_REFERENCE_NO.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "DISPLAY_REFERENCE_NO")]
        public virtual void displayReferenceNo_validate()
        {
            //  06/12/17 psr alio-15315 No longer using display_reference_no
            //  06/12/17 psr alio-15315 No longer using display_reference_no
        }


        #region Original PL/SQL code code for TRIGGER APPROVAL_CHAIN_BUTTON.WHEN-BUTTON-PRESSED
        /*
         go_item('approval_chain');
do_key('list_values');

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:41
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>APPROVAL_CHAIN_BUTTON.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "APPROVAL_CHAIN_BUTTON")]
        public virtual void approvalChainButton_buttonClick()
        {
            ItemServices.GoItem("approval_chain");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


    }
}
