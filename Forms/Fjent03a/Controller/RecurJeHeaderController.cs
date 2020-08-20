using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjent03a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.AppSupportLib.UI;
using Foundations.Core.Types;
using System;
using System.Data;

namespace Alio.Forms.Fjent03a.Controllers
{


    public class RecurJeHeaderController : BaseBlockController<Fjent03aTask, Fjent03aModel>
    {

        public RecurJeHeaderController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER RECUR_JE_HEADER.PRE-DELETE
        /*
         -- 
-- Begin default relation program section 
-- 
BEGIN 
-- 
-- Begin RECUR_JE_DATA detail program section 
-- 
DELETE FROM FAS.RECUR_JE_DATA F 
WHERE F.RECUR_JOURNAL_ID = :RECUR_JE_HEADER.RECUR_JOURNAL_ID; 
-- 
-- End RECUR_JE_DATA detail program section 
-- 
-- 
-- Begin RECUR_JE_DATA detail program section 
-- 
DELETE FROM FAS.RECUR_JE_DATA F 
WHERE F.RECUR_JOURNAL_ID = :RECUR_JE_HEADER.RECUR_JOURNAL_ID; 
-- 
-- End RECUR_JE_DATA detail program section 
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
        ///	ID:39
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>RECUR_JE_HEADER.PRE-DELETE</TriggerInfo>

        [RowDeleting]
        public virtual void recurJeHeader_RowDeleting(RowAdapterEventArgs args)
        {
            //  
            //  Begin default relation program section 
            //  
            RecurJeHeaderAdapter recurJeHeaderElement = args.Row as RecurJeHeaderAdapter;


            int rowCount = 0;
            //  
            //  Begin RECUR_JE_DATA detail program section 
            //  
            #region Execute data command
            String sql1 = "DELETE FROM FAS.RECUR_JE_DATA F" +
" WHERE F.RECUR_JOURNAL_ID = @RECUR_JE_HEADER_RECUR_JOURNAL_ID";
            DataCommand command1 = new DataCommand(sql1);
            //Setting query parameters
            command1.AddParameter("@RECUR_JE_HEADER_RECUR_JOURNAL_ID", recurJeHeaderElement.RecurJournalId);
            rowCount = command1.Execute();
            #endregion
            //  
            //  End RECUR_JE_DATA detail program section 
            //  
            //  
            //  Begin RECUR_JE_DATA detail program section 
            //  
            #region Execute data command
            String sql2 = "DELETE FROM FAS.RECUR_JE_DATA F" +
" WHERE F.RECUR_JOURNAL_ID = @RECUR_JE_HEADER_RECUR_JOURNAL_ID";
            DataCommand command2 = new DataCommand(sql2);
            //Setting query parameters
            command2.AddParameter("@RECUR_JE_HEADER_RECUR_JOURNAL_ID", recurJeHeaderElement.RecurJournalId);
            rowCount = command2.Execute();
            #endregion
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_HEADER.ON-POPULATE-DETAILS
        /*
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
-- Begin RECUR_JE_DATA detail program section 
-- 
IF ( (:RECUR_JE_HEADER.RECUR_JOURNAL_ID is not null) ) THEN   
rel_id := Find_Relation('RECUR_JE_HEADER.HEADER_DATA');   
Query_Master_Details(rel_id, 'RECUR_JE_DATA');   
END IF; 
-- 
-- End RECUR_JE_DATA detail program section 
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
        ///	ID:40
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>RECUR_JE_HEADER.ON-POPULATE-DETAILS</TriggerInfo>

        [PopulateDetails]
        public virtual void recurJeHeader_PopulateDetails(RowAdapterEventArgs args)
        {
            //  
            //  Begin default relation declare section 
            //  

            RecurJeHeaderAdapter recurJeHeader = args.Row as RecurJeHeaderAdapter;

            if (recurJeHeader.RowState == DataRowState.Added || recurJeHeader.RowState == DataRowState.Detached)
                return;

            Model.RecurJeData.LoadData();

        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_HEADER.PRE-QUERY
        /*

accounting_array.accounting_tab_init;--cec alio-3074
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:41
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>RECUR_JE_HEADER.PRE-QUERY</TriggerInfo>

        [BeforeQuery]
        public virtual void recurJeHeader_BeforeQuery(QueryEventArgs args)
        {
            Task.Packages.AccountingArray.AccountingTabInit();
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_HEADER.POST-BLOCK
        /*
         begin 
--cec alio-3074 
if(nvl(:recur_je_header.recur_journal_id, '-123xyzABC') <> nvl(:recur_je_data.recur_journal_id, '-321xyzABC')) then 
accounting_array.accounting_tab_init; 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:42
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>RECUR_JE_HEADER.POST-BLOCK</TriggerInfo>

        [ActionTrigger(Action = "POST-BLOCK", Function = KeyFunction.BLOCK_OUT)]
        public virtual void recurJeHeader_blockOut()
        {
            RecurJeHeaderAdapter recurJeHeaderElement = this.Model.RecurJeHeader.CurrentRowAdapter;
            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;


            // cec alio-3074 
            if ((Lib.IsNull(recurJeHeaderElement.RecurJournalId, "-123xyzABC") != Lib.IsNull(recurJeDataElement.RecurJournalId, "-321xyzABC")))
            {
                Task.Packages.AccountingArray.AccountingTabInit();
            }
        }


        #region Original PL/SQL code code for TRIGGER ASSIGN_RECUR_JOURNAL_ID.WHEN-NEW-ITEM-INSTANCE
        /*
         begin 
if :system.mode = 'ENTER-QUERY' then 
  go_item('recur_je_header.recur_journal_id'); 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:15
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ASSIGN_RECUR_JOURNAL_ID.WHEN-NEW-ITEM-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-ITEM-INSTANCE", Item = "ASSIGN_RECUR_JOURNAL_ID", Function = KeyFunction.ITEM_CHANGE)]
        public virtual void assignRecurJournalId_itemChange()
        {
            if (TaskServices.Mode == "ENTER-QUERY")
            {
                ItemServices.GoItem("recur_je_header.recur_journal_id");
            }
        }


        #region Original PL/SQL code code for TRIGGER ASSIGN_RECUR_JOURNAL_ID.WHEN-BUTTON-PRESSED
        /*
         begin 
if :recur_je_header.recur_journal_id is null then 
  :recur_je_header.recur_journal_id 
     := amgen01a_.get_next_counter('RECUR_JOURNAL_ID'); 
  go_item('recur_je_header.recur_journal_desc'); 
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
        /// <TriggerInfo>ASSIGN_RECUR_JOURNAL_ID.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "ASSIGN_RECUR_JOURNAL_ID")]
        public virtual void assignRecurJournalId_buttonClick()
        {

            RecurJeHeaderAdapter recurJeHeaderElement = this.Model.RecurJeHeader.CurrentRowAdapter;

            if (string.IsNullOrEmpty(recurJeHeaderElement.RecurJournalId))
            {
                recurJeHeaderElement.RecurJournalId = Amgen01a_.GetNextCounter("RECUR_JOURNAL_ID").ToString();
                foreach (var item in this.Model.RecurJeData.Rows)
                {
                    item.RecurJournalId = recurJeHeaderElement.RecurJournalId;
                }
                ItemServices.GoItem("recur_je_header.recur_journal_desc");
            }
        }


        #region Original PL/SQL code code for TRIGGER SHOW_LOV.WHEN-BUTTON-PRESSED
        /*
         declare 

lov_selected  boolean; 

begin 

if :system.mode = 'NORMAL' then 
  do_key('enter_query'); 
  return; 
end if; 

lov_selected := show_lov('recur_je_header'); 


if lov_selected then 
  do_key('execute_query'); 
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
        /// <TriggerInfo>SHOW_LOV.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Function = KeyFunction.LIST_VALUES, Item = "RECUR_JOURNAL_ID")]
        public virtual void ListValuesRecurJournalId()
        {
            NBool lovSelected = NBool.Null;
            if (TaskServices.Mode == "NORMAL")
            {
                TaskServices.ExecuteAction("SEARCH");
                return;
            }
            lovSelected = TaskServices.ShowLov("recur_je_header");
            if (lovSelected)
            {
                BlockServices.ExecuteQuery();
            }
        }


        #region Original PL/SQL code code for TRIGGER ASSIGN_REFERENCE.WHEN-NEW-ITEM-INSTANCE
        /*
         begin 
if :system.mode = 'ENTER-QUERY' then 
  go_item('recur_je_header.reference_no'); 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:22
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ASSIGN_REFERENCE.WHEN-NEW-ITEM-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-ITEM-INSTANCE", Item = "ASSIGN_REFERENCE", Function = KeyFunction.ITEM_CHANGE)]
        public virtual void assignReference_itemChange()
        {
            if (TaskServices.Mode == "ENTER-QUERY")
            {
                ItemServices.GoItem("recur_je_header.reference_no");
            }
        }


        #region Original PL/SQL code code for TRIGGER ASSIGN_REFERENCE.WHEN-BUTTON-PRESSED
        /*
         begin 
if :recur_je_header.reference_no is null then 
  :recur_je_header.reference_no 
     := amgen01a_.get_next_counter('JE_REFERENCE'); 
  go_block('recur_je_data'); 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:23
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ASSIGN_REFERENCE.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "ASSIGN_REFERENCE")]
        public virtual void assignReference_buttonClick()
        {

            RecurJeHeaderAdapter recurJeHeaderElement = this.Model.RecurJeHeader.CurrentRowAdapter;

            if (string.IsNullOrEmpty(recurJeHeaderElement.ReferenceNo))
            {
                recurJeHeaderElement.ReferenceNo = Amgen01a_.GetNextCounter("JE_REFERENCE").ToString();

                BlockServices.GoBlock("recur_je_data");
            }
        }


        #region Original PL/SQL code code for TRIGGER JOURNAL_DATE.KEY-LISTVAL
        /*
         BEGIN 

date_lov.get_date(sysdate,                      -- initial date 
              'recur_je_header.journal_date', -- return block.item 
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
        ///	ID:27
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>JOURNAL_DATE.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "JOURNAL_DATE", Function = KeyFunction.LIST_VALUES)]
        public virtual void journalDate_ListValues()
        {
            //Task.Libraries.Calendar.DateLov.GetDate(NDate.Now, "recur_je_header.journal_date", 240, 60, "Journal Date", "OK", "Cancel", true, false, false);
        }


        #region Original PL/SQL code code for TRIGGER JOURNAL_DATE.WHEN-MOUSE-DOUBLECLICK
        /*
         begin 
go_block('recur_je_header'); 
go_record(to_number(:system.cursor_record)); 
go_item('recur_je_header.journal_date'); 
do_key('list_values'); 
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
        /// <TriggerInfo>JOURNAL_DATE.WHEN-MOUSE-DOUBLECLICK</TriggerInfo>

        [ActionTrigger(Action = "WHEN-MOUSE-DOUBLECLICK", Item = "JOURNAL_DATE")]
        public virtual void journalDate_doubleClick()
        {
            BlockServices.GoBlock("recur_je_header");
            BlockServices.SetCurrentRecord(Lib.ToNumber(TaskServices.CursorRecord));
            ItemServices.GoItem("recur_je_header.journal_date");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER ZERO_AMOUNTS.WHEN-BUTTON-PRESSED
        /*
         begin 
-- Are you sure you want to zero amounts? 
if show_alert('zero_amounts') = alert_button1 then 
  go_block('recur_je_data'); 
  first_record; 
  loop 
     :recur_je_data.dr_amount := null; 
     :recur_je_data.cr_amount := null; 
  :recur_je_data.hold_journal_amount := 0; --cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
  :recur_je_data.hold_cr_amount := 0; --cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
  :recur_je_data.hold_dr_amount := 0; --cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
     if :system.last_record = 'TRUE' then 
        exit; 
     end if; 
     next_record; 
  end loop; 
  first_record; 
end if; 
--   :totals.dr_total := 0; 
--   :totals.cr_total := 0; 

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:30
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ZERO_AMOUNTS.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "ZERO_AMOUNTS")]
        public virtual void zeroAmounts_buttonClick()
        {

            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;


            //  Are you sure you want to zero amounts? 
            if (TaskServices.ShowAlert("zero_amounts") == MessageServices.BUTTON1)
            {
                BlockServices.GoBlock("recur_je_data");
                BlockServices.FirstRecord();
                while (true)
                {
                    recurJeDataElement.DrAmount = NNumber.Null;
                    recurJeDataElement.CrAmount = NNumber.Null;
                    recurJeDataElement.HoldJournalAmount = 0;
                    // cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
                    recurJeDataElement.HoldCrAmount = 0;
                    // cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
                    recurJeDataElement.HoldDrAmount = 0;
                    // cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
                    if (BlockServices.InLastRecord)
                    {
                        break;
                    }
                    BlockServices.NextRecord();
                }
                BlockServices.FirstRecord();
            }
        }


        #region Original PL/SQL code code for TRIGGER CREATE_JOURNAL.WHEN-BUTTON-PRESSED
        /*
         declare 

cursor validate_ref_no_cur is 
select reference_no 
 from fas.je_header 
where reference_no = :recur_je_header.reference_no; 

validate_ref_no_rec validate_ref_no_cur%rowtype; 

cursor next_line_no_cur is 
select max(line_no) + 1 
 from fas.recur_je_data 
where recur_je_data.recur_journal_id 
      = :recur_je_header.recur_journal_id; 

-- 07/17/06 KLY Ref No 33948 - added check of account_year and give error if it does not match batch_year. 
cursor get_account_year_cur(in_account_id number) is 
select account_year 
 from shr.accounts 
where account_id = in_account_id; 

get_account_year_rec get_account_year_cur%rowtype; 

cursor profile_cur is 
select profile_data 
from adm.profiles 
where profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS'; 

profile_rec profile_cur%rowtype; 


invalid_journal_id_exc     exception; 
null_reference_no_exc      exception; 
invalid_reference_no_exc   exception; 
invalid_journal_date_exc   exception; 
invalid_amounts_exc        exception; 
hold_line_no               number(4); 
v_program                VARCHAR2 (30) DEFAULT 'FJENT03AFMX'; 

begin 

open profile_cur; 
fetch profile_cur into profile_rec; 
close profile_cur; 

go_block('recur_je_data'); ------  MNN 03-21-2001 ref#4056 
first_record; 
loop 
  if :recur_je_data.account_id <= 0 then 
     Message('This Account ' 
            ||:recur_je_data.account_no 
            ||' is Invalid. You must use a valid Account.'); 
     Message(' ',no_acknowledge); 
     raise form_trigger_failure; 
  end if; 

if (nvl(profile_rec.profile_data, 'N') = 'Y') then 
if(:recur_je_data.dr_amount < 0 or :recur_je_data.cr_amount < 0) then 
 MESSAGE('Journal Amounts may not be negative. Please correct the amounts.'); 
 MESSAGE(' ', no_acknowledge); 
 raise form_trigger_failure; 
end if; 
end if; 

  -- 07/17/06 KLY Ref No 33948 - added check of account_year and give error if it does not match batch_year. 
  open get_account_year_cur(:recur_je_data.account_id); 
  fetch get_account_year_cur into get_account_year_rec; 
  if get_account_year_rec.account_year <> :batch_no_block.batch_year then 
     Message('The Account Year '||get_account_year_rec.account_year|| 
             ' for Account '||:recur_je_data.account_no|| 
             ' does not match the Batch Year '||:batch_no_block.batch_year||' for this Batch.'); 
     Message(' ',no_acknowledge); 
     raise form_trigger_failure;       
  end if; 
  close get_account_year_cur; 
  msg_debug('rjh seq: ' || :recur_je_data.line_no || ', acct no: ' || 
   :recur_je_data.account_no || ', amt: ' || :recur_je_data.journal_amount); 
journal_budget_check; --cec alio-3074 

  exit when :system.last_record = 'TRUE'; 
  next_record; 
end loop; 
accounting_array.validate_budget (v_program); --cec alio-3074 2/27/15 - moved from journal_budget_check so all acct records are updated before checking the balance 
go_block('recur_je_header'); ------ MNN END    

if :recur_je_header.recur_journal_id is null or 
  :recur_je_data.recur_journal_id   is null then 
  raise invalid_journal_id_exc; 
end if; 

if :recur_je_header.reference_no is null then 
  raise null_reference_no_exc; 
end if;   

open  validate_ref_no_cur; 
fetch validate_ref_no_cur into validate_ref_no_rec; 
if validate_ref_no_cur%found then 
  close validate_ref_no_cur;   
  raise invalid_reference_no_exc; 
end if; 
close validate_ref_no_cur;   

if :recur_je_header.journal_date is null then 
  raise invalid_journal_date_exc; 
end if; 

if :recur_je_data.cr_total <> :recur_je_data.dr_total then 
  raise invalid_amounts_exc; 
end if; 

insert into fas.je_header (reference_no, 
                          batch_year, 
                          batch_no, 
                          journal_description, 
                          journal_date,
                          system_owner,
                          approval_chain,                  --mfl alio-14830 19.2 
                          release_flag)                    --mfl alio-16668 19.4
                  values (:recur_je_header.reference_no, 
                          :batch_no_block.batch_year, 
                          :batch_no_block.batch_no,        
                          :recur_je_header.journal_description, 
                          :recur_je_header.journal_date,
                          'FAS', --dph alio-3109
                          :recur_je_header.approval_chain, -- mfl alio-14830 19.2
                          :recur_je_header.ready_for_approval); --mfl alio-16668 19.4

 -- header always goes with data no need to call by je_data
 if :parameter.journal_approval = 'Y' then
     build_approval_array(:recur_je_header.reference_no);
 end if;

go_block('recur_je_data'); 
first_record; 
loop 
 IF nvl(:recur_je_data.journal_amount,0) <> 0 then 

 if :recur_je_data.line_no is not null then 
    hold_line_no := :recur_je_data.line_no; 
 else 
    if hold_line_no is null then 
       open next_line_no_cur; 
       fetch next_line_no_cur into hold_line_no; 
       if hold_line_no is null then 
          hold_line_no := 1; 
       end if; 
       close next_line_no_cur; 
    else 
       hold_line_no := hold_line_no + 1; 
    end if; 
 end if; 
   insert into fas.je_data (reference_no, 
                            line_no, 
                            journal_amount, 
                            journal_description, 
                            account_id, 
                            debit_credit_flag) 
                   values (:recur_je_header.reference_no, 
                           hold_line_no, 
                            :recur_je_data.journal_amount,        
                            :recur_je_data.journal_description, 
                            :recur_je_data.account_id, 
                            :recur_je_data.debit_credit_flag); 

 end if; 
 exit when :system.last_record = 'TRUE'; 
 next_record; 
end loop; 
update fas.batches 
  set posted_flag = 'EN' 
where batch_no = :batch_no_block.batch_no and 
      batch_year = :batch_no_block.batch_year; 
commit_form; 

if :parameter.journal_approval = 'Y' then
build_approval_chain;
message('Journal ' 
        ||:recur_je_header.reference_no 
        ||' has been added to Journals. Ready for Approval, Edit and Post.');  
 message(' ', no_acknowledge); 


else  
 message('Journal ' 
        ||:recur_je_header.reference_no 
        ||' has been added to Journals. Ready for Edit and Post.');  
 message(' ', no_acknowledge); 

end if;


EXCEPTION 

when invalid_journal_id_exc then 
 message('You Must have a Valid Recurring Journal Number.'); 
 message(' ',no_acknowledge); 
 go_item('recur_je_header.recur_journal_id'); 
 raise form_trigger_failure; 

when null_reference_no_exc then 
 message('You must Enter a Reference Number.'); 
 message(' ',no_acknowledge); 
 go_item('recur_je_header.reference_no'); 
 raise form_trigger_failure; 

when invalid_reference_no_exc then   
 message('Reference Number ' 
        ||:recur_je_header.reference_no 
        ||' is Already Assigned to a Journal.'); 
 message(' ',no_acknowledge); 
 go_item('recur_je_header.reference_no'); 
 :recur_je_header.reference_no := null; 
 raise form_trigger_failure; 

when invalid_journal_date_exc then  
 message('There is not a Journal Date for this Reference Number.'); 
 message(' ',no_acknowledge); 
 go_item('recur_je_header.journal_date'); 
 raise form_trigger_failure; 

when invalid_amounts_exc then 
 message('Debit and Credit Amounts are not Equal. Please Correct This.'); 
 message(' ',no_acknowledge); 
 go_item('recur_je_data.account_no'); 
 raise form_trigger_failure; 

-- 11/30/07 KLB 043957 - Added to trap duplicate line no on form if Create Journal is pressed 
--   before form is closed and primary key on RECUR_JE_DATA is enabled. 
when dup_val_on_index then 
 message('Duplicate line number encountered on recurring journal entry.  '|| 
         'Please use RESEQUENCE or correct line number manually.'); 
 message(' ',no_acknowledge);     
 go_item('recur_je_data.line_no'); 
 raise form_trigger_failure;     

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:32
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>CREATE_JOURNAL.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "CREATE_JOURNAL")]
        public virtual void createJournal_buttonClick()
        {

            RecurJeHeaderAdapter recurJeHeaderElement = this.Model.RecurJeHeader.CurrentRowAdapter;
            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;


            int rowCount = 0;
            {
                String sqlvalidateRefNoCur = "SELECT reference_no " +
" FROM fas.je_header " +
" WHERE reference_no = @RECUR_JE_HEADER_REFERENCE_NO ";
                DataCursor validateRefNoCur = new DataCursor(sqlvalidateRefNoCur);
                TableRow validateRefNoRec = null;
                String sqlnextLineNoCur = "SELECT max(line_no) + 1 " +
" FROM fas.recur_je_data " +
" WHERE recur_je_data.recur_journal_id = @RECUR_JE_HEADER_RECUR_JOURNAL_ID ";
                DataCursor nextLineNoCur = new DataCursor(sqlnextLineNoCur);
                String sqlgetAccountYearCur = "SELECT account_year " +
" FROM shr.accounts " +
" WHERE account_id = @P_IN_ACCOUNT_ID ";
                DataCursor getAccountYearCur = new DataCursor(sqlgetAccountYearCur);
                NNumber getAccountYearCurInAccountId = NNumber.Null;
                TableRow getAccountYearRec = null;
                String sqlprofileCur = "SELECT profile_data " +
" FROM adm.profiles " +
" WHERE profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS' ";
                DataCursor profileCur = new DataCursor(sqlprofileCur);
                TableRow profileRec = null;
                NNumber holdLineNo = NNumber.Null;
                string vProgram = "FJENT03AFMX";
                #region
                try
                {
                    try
                    {
                        profileCur.Open();
                        profileRec = profileCur.FetchRow();
                        BlockServices.GoBlock("recur_je_data");
                        // ----  MNN 03-21-2001 ref#4056 
                        BlockServices.FirstRecord();
                        while (true)
                        {
                            if (recurJeDataElement.AccountId <= 0)
                            {
                                TaskServices.Message("This Account " + recurJeDataElement.AccountNo + " is Invalid. You must use a valid Account.");
                                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                                throw new ApplicationException();
                            }
                            if ((Lib.IsNull(profileRec.GetStr("profile_data"), "N") == "Y"))
                            {
                                if ((recurJeDataElement.DrAmount < 0 || recurJeDataElement.CrAmount < 0))
                                {
                                    TaskServices.Message("Journal Amounts may not be negative. Please correct the amounts.");
                                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                                    throw new ApplicationException();
                                }
                            }
                            //  07/17/06 KLY Ref No 33948 - added check of account_year and give error if it does not match batch_year. 
                            getAccountYearCurInAccountId = recurJeDataElement.AccountId;
                            //Setting query parameters
                            getAccountYearCur.AddParameter("@P_IN_ACCOUNT_ID", getAccountYearCurInAccountId);
                            getAccountYearCur.Open();
                            getAccountYearRec = getAccountYearCur.FetchRow();
                            if (getAccountYearRec.GetNumber("account_year") != Model.BatchNoBlock.BatchYear)
                            {
                                TaskServices.Message("The Account Year " + getAccountYearRec.GetNumber("account_year").ToString() + " for Account " + recurJeDataElement.AccountNo + " does not match the Batch Year " + Model.BatchNoBlock.BatchYear + " for this Batch.");
                                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                                throw new ApplicationException();
                            }
                            this.Task.Services.MsgDebug("rjh seq: " + recurJeDataElement.LineNo.ToString().ToString() + ", acct no: " + recurJeDataElement.AccountNo + ", amt: " + recurJeDataElement.JournalAmount.ToString());
                            this.Task.Services.JournalBudgetCheck(recurJeDataElement);
                            // cec alio-3074 
                            if (BlockServices.InLastRecord)
                                break;
                            BlockServices.NextRecord();
                        }
                        Task.Packages.AccountingArray.ValidateBudget(vProgram);
                        // cec alio-3074 2/27/15 - moved from journal_budget_check so all acct records are updated before checking the balance 
                        BlockServices.GoBlock("recur_je_header");
                        // ---- MNN END    
                        if (string.IsNullOrEmpty(recurJeHeaderElement.RecurJournalId) || string.IsNullOrEmpty(recurJeDataElement.RecurJournalId))
                        {
                            throw new InvalidJournalIdExc();
                        }
                        if (string.IsNullOrEmpty(recurJeHeaderElement.ReferenceNo))
                        {
                            throw new NullReferenceNoExc();
                        }
                        //Setting query parameters
                        validateRefNoCur.AddParameter("@RECUR_JE_HEADER_REFERENCE_NO", recurJeHeaderElement.ReferenceNo);
                        validateRefNoCur.Open();
                        validateRefNoRec = validateRefNoCur.FetchRow();
                        if (validateRefNoCur.Found)
                        {
                            throw new InvalidReferenceNoExc();
                        }
                        if (recurJeHeaderElement.JournalDate.IsNull)
                        {
                            throw new InvalidJournalDateExc();
                        }
                        if (recurJeDataElement.CrTotal != recurJeDataElement.DrTotal)
                        {
                            throw new InvalidAmountsExc();
                        }
                        #region Execute data command
                        String sql1 = "INSERT INTO fas.je_header " +
"(reference_no, batch_year, batch_no, journal_description, journal_date, system_owner, approval_chain, release_flag)" +
"VALUES (@RECUR_JE_HEADER_REFERENCE_NO, @BATCH_NO_BLOCK_BATCH_YEAR, @BATCH_NO_BLOCK_BATCH_NO, @RECUR_JE_HEADER_JOURNAL_DESCRIPTION, @RECUR_JE_HEADER_JOURNAL_DATE, 'FAS', @RECUR_JE_HEADER_APPROVAL_CHAIN, @RECUR_JE_HEADER_READY_FOR_APPROVAL)";
                        DataCommand command1 = new DataCommand(sql1);
                        //Setting query parameters
                        command1.AddParameter("@RECUR_JE_HEADER_REFERENCE_NO", recurJeHeaderElement.ReferenceNo);
                        command1.AddParameter("@BATCH_NO_BLOCK_BATCH_YEAR", Model.BatchNoBlock.BatchYear);
                        command1.AddParameter("@BATCH_NO_BLOCK_BATCH_NO", Model.BatchNoBlock.BatchNo);
                        command1.AddParameter("@RECUR_JE_HEADER_JOURNAL_DESCRIPTION", recurJeHeaderElement.JournalDescription);
                        command1.AddParameter("@RECUR_JE_HEADER_JOURNAL_DATE", recurJeHeaderElement.JournalDate);
                        command1.AddParameter("@RECUR_JE_HEADER_APPROVAL_CHAIN", recurJeHeaderElement.ApprovalChain);
                        command1.AddParameter("@RECUR_JE_HEADER_READY_FOR_APPROVAL", recurJeHeaderElement.ReadyForApproval);
                        rowCount = command1.Execute();
                        #endregion
                        // mfl alio-16668 19.4
                        //  header always goes with data no need to call by je_data
                        if (this.Model.Params.JournalApproval == "Y")
                        {
                            this.Task.Services.BuildApprovalArray(recurJeHeaderElement.ReferenceNo);
                        }
                        BlockServices.GoBlock("recur_je_data");
                        BlockServices.FirstRecord();
                        while (true)
                        {
                            if (Lib.IsNull(recurJeDataElement.JournalAmount, 0) != 0)
                            {
                                if (!recurJeDataElement.LineNo.IsNull)
                                {
                                    holdLineNo = recurJeDataElement.LineNo;
                                }
                                else
                                {
                                    if (holdLineNo.IsNull)
                                    {
                                        //Setting query parameters
                                        nextLineNoCur.AddParameter("@RECUR_JE_HEADER_RECUR_JOURNAL_ID", recurJeHeaderElement.RecurJournalId);
                                        nextLineNoCur.Open();
                                        ResultSet nextLineNoCurResults = nextLineNoCur.FetchInto();
                                        if (nextLineNoCurResults != null)
                                        {
                                            holdLineNo = nextLineNoCurResults.GetNumber(0);
                                        }
                                        if (holdLineNo.IsNull)
                                        {
                                            holdLineNo = 1;
                                        }
                                    }
                                    else
                                    {
                                        holdLineNo = holdLineNo + 1;
                                    }
                                }
                                #region Execute data command
                                String sql2 = "INSERT INTO fas.je_data " +
"(reference_no, line_no, journal_amount, journal_description, account_id, debit_credit_flag)" +
"VALUES (@RECUR_JE_HEADER_REFERENCE_NO, @P_HOLD_LINE_NO, @RECUR_JE_DATA_JOURNAL_AMOUNT, @RECUR_JE_DATA_JOURNAL_DESCRIPTION, @RECUR_JE_DATA_ACCOUNT_ID, @RECUR_JE_DATA_DEBIT_CREDIT_FLAG)";
                                DataCommand command2 = new DataCommand(sql2);
                                //Setting query parameters
                                command2.AddParameter("@RECUR_JE_HEADER_REFERENCE_NO", recurJeHeaderElement.ReferenceNo);
                                command2.AddParameter("@P_HOLD_LINE_NO", holdLineNo);
                                command2.AddParameter("@RECUR_JE_DATA_JOURNAL_AMOUNT", recurJeDataElement.JournalAmount);
                                command2.AddParameter("@RECUR_JE_DATA_JOURNAL_DESCRIPTION", recurJeDataElement.JournalDescription);
                                command2.AddParameter("@RECUR_JE_DATA_ACCOUNT_ID", recurJeDataElement.AccountId);
                                command2.AddParameter("@RECUR_JE_DATA_DEBIT_CREDIT_FLAG", recurJeDataElement.DebitCreditFlag);
                                rowCount = command2.Execute();
                                #endregion
                            }
                            if (BlockServices.InLastRecord)
                                break;
                            BlockServices.NextRecord();
                        }
                        #region Execute data command
                        String sql3 = "UPDATE fas.batches " +
"SET posted_flag = 'EN' " +
" WHERE batch_no = @BATCH_NO_BLOCK_BATCH_NO AND batch_year = @BATCH_NO_BLOCK_BATCH_YEAR";
                        DataCommand command3 = new DataCommand(sql3);
                        //Setting query parameters
                        command3.AddParameter("@BATCH_NO_BLOCK_BATCH_NO", Model.BatchNoBlock.BatchNo);
                        command3.AddParameter("@BATCH_NO_BLOCK_BATCH_YEAR", Model.BatchNoBlock.BatchYear);
                        rowCount = command3.Execute();
                        #endregion
                        TaskServices.Commit();
                        if (this.Model.Params.JournalApproval == "Y")
                        {
                            this.Task.Services.BuildApprovalChain();
                            TaskServices.Message("Journal " + recurJeHeaderElement.ReferenceNo + " has been added to Journals. Ready for Approval, Edit and Post.");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        }
                        else
                        {
                            TaskServices.Message("Journal " + recurJeHeaderElement.ReferenceNo + " has been added to Journals. Ready for Edit and Post.");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        }
                    }
                    catch (InvalidJournalIdExc)
                    {
                        TaskServices.Message("You Must have a Valid Recurring Journal Number.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        ItemServices.GoItem("recur_je_header.recur_journal_id");
                        throw new ApplicationException();
                    }
                    catch (NullReferenceNoExc)
                    {
                        TaskServices.Message("You must Enter a Reference Number.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        ItemServices.GoItem("recur_je_header.reference_no");
                        throw new ApplicationException();
                    }
                    catch (InvalidReferenceNoExc)
                    {
                        TaskServices.Message("Reference Number " + recurJeHeaderElement.ReferenceNo + " is Already Assigned to a Journal.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        ItemServices.GoItem("recur_je_header.reference_no");
                        recurJeHeaderElement.ReferenceNo = null;
                        throw new ApplicationException();
                    }
                    catch (InvalidJournalDateExc)
                    {
                        TaskServices.Message("There is not a Journal Date for this Reference Number.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        ItemServices.GoItem("recur_je_header.journal_date");
                        throw new ApplicationException();
                    }
                    catch (InvalidAmountsExc)
                    {
                        TaskServices.Message("Debit and Credit Amounts are not Equal. Please Correct This.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        ItemServices.GoItem("recur_je_data.account_no");
                        throw new ApplicationException();
                    }
                    catch (DupValOnIndex)
                    {
                        TaskServices.Message("Duplicate line number encountered on recurring journal entry.  " + "Please use RESEQUENCE or correct line number manually.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        ItemServices.GoItem("recur_je_data.line_no");
                        throw new ApplicationException();
                    }
                }
                finally
                {
                    profileCur.Close();
                    getAccountYearCur.Close();
                    validateRefNoCur.Close();
                    nextLineNoCur.Close();
                }
                #endregion
            }
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
        ///	ID:36
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



        #endregion

    }
}
