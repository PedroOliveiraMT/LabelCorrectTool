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

namespace Alio.Forms.Fjent03a.Controllers
{


    public class RecurJeDataController : BaseBlockController<Fjent03aTask, Fjent03aModel>
    {

        public RecurJeDataController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER RECUR_JE_DATA.PRE-INSERT
        /*
         declare 

cursor next_line_no_cur is 
select (max(line_no) + 1)line_no 
 from fas.recur_je_data 
where recur_je_data.recur_journal_id = :recur_je_header.recur_journal_id; 

next_line_no_rec  next_line_no_cur%rowtype; 

cursor profile_cur is 
select profile_data 
from adm.profiles 
where profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS'; 

profile_rec profile_cur%rowtype; 
rec_notfound      boolean; 

BEGIN 

if :recur_je_data.dr_total <> :recur_je_data.cr_total then 
  Message('Debit and Credit totals are Not equal.'); 
  Message(' ',no_acknowledge); 
  raise form_trigger_failure; 
end if; 

if nvl(:recur_je_data.account_id,0) <= 0 then 
  Message('This Account ' 
         ||:recur_je_data.account_no 
         ||' is Invalid. You must use a valid Account.'); 
  Message(' ',no_acknowledge); 
  raise form_trigger_failure; 
end if; 

open profile_cur; 
fetch profile_cur into profile_rec; 
close profile_cur; 

if (nvl(profile_rec.profile_data, 'N') = 'Y') then 
if(:recur_je_data.dr_amount < 0 or :recur_je_data.cr_amount < 0) then 
 MESSAGE('Journal Amounts may not be negative. Please correct the amounts.'); 
 MESSAGE(' ', no_acknowledge); 
 raise form_trigger_failure; 
end if; 
end if; 

--  journal_budget_check;  -- alio-3074 09/03/14 klb added --cec alio-3074 12/3/14 moved to create journal button 

<multilinecomment> --cec alio-3074 12/1/14 moved to wnri trigger 
if :recur_je_data.line_no is null then 
  open next_line_no_cur; 
  fetch next_line_no_cur into next_line_no_rec; 
  close next_line_no_cur; 
  :recur_je_data.line_no := nvl(next_line_no_rec.line_no,1); 
end if;      
</multilinecomment> 

END;   


        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:72
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>RECUR_JE_DATA.PRE-INSERT</TriggerInfo>

        [RowInserting]
        public virtual void recurJeData_RowInserting(RowAdapterEventArgs args)
        {
            RecurJeHeaderAdapter recurJeHeaderElement = this.Model.RecurJeHeader.CurrentRowAdapter;
            RecurJeDataAdapter recurJeDataElement = args.Row as RecurJeDataAdapter;


            int rowCount = 0;
            {
                String sqlnextLineNoCur = "SELECT (max(line_no) + 1) line_no " +
" FROM fas.recur_je_data " +
" WHERE recur_je_data.recur_journal_id = @RECUR_JE_HEADER_RECUR_JOURNAL_ID ";
                DataCursor nextLineNoCur = new DataCursor(sqlnextLineNoCur);
                TableRow nextLineNoRec = null;
                String sqlprofileCur = "SELECT profile_data " +
" FROM adm.profiles " +
" WHERE profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS' ";
                DataCursor profileCur = new DataCursor(sqlprofileCur);
                TableRow profileRec = null;
                NBool recNotfound = NBool.Null;
                #region
                try
                {
                    if (recurJeDataElement.DrTotal != recurJeDataElement.CrTotal)
                    {
                        TaskServices.Message("Debit and Credit totals are Not equal.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        throw new ApplicationException();
                    }
                    if (Lib.IsNull(recurJeDataElement.AccountId, 0) <= 0)
                    {
                        TaskServices.Message("This Account " + recurJeDataElement.AccountNo + " is Invalid. You must use a valid Account.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        throw new ApplicationException();
                    }
                    profileCur.Open();
                    profileRec = profileCur.FetchRow();
                    if ((Lib.IsNull(profileRec.GetStr("profile_data"), "N") == "Y"))
                    {
                        if ((recurJeDataElement.DrAmount < 0 || recurJeDataElement.CrAmount < 0))
                        {
                            TaskServices.Message("Journal Amounts may not be negative. Please correct the amounts.");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                            throw new ApplicationException();
                        }
                    }
                }
                finally
                {
                    profileCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_DATA.PRE-UPDATE
        /*
         declare 
cursor profile_cur is 
select profile_data 
from adm.profiles 
where profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS'; 

profile_rec profile_cur%rowtype; 

debit_credit_in_ number; 

BEGIN 

if :recur_je_data.dr_total <> :recur_je_data.cr_total then 
  Message('Debit and Credit totals are Not equal.'); 
  Message(' ',no_acknowledge); 
  raise form_trigger_failure; 
end if; 

if nvl(:recur_je_data.account_id,0) <= 0 then 
  Message('This Account ' 
         ||:recur_je_data.account_no 
         ||' is Invalid. You must use a valid Account.'); 
  Message(' ',no_acknowledge); 
  raise form_trigger_failure; 
end if; 

--  journal_budget_check;  -- alio-3074 09/03/14 klb added --cec alio-3074 12/3/14 moved to create journal button 

if :recur_je_data.cr_amount <> 0 then 
  :recur_je_data.journal_amount := :recur_je_data.cr_amount; 
  :recur_je_data.debit_credit_flag := -1; 
  debit_credit_in_ := -1; 
else 

  :recur_je_data.journal_amount := :recur_je_data.dr_amount; 
  :recur_je_data.debit_credit_flag := 1; 
  debit_credit_in_ := 1; 

end if; 

open profile_cur; 
fetch profile_cur into profile_rec; 
close profile_cur; 

if (nvl(profile_rec.profile_data, 'N') = 'Y') then 
if(:recur_je_data.dr_amount < 0 or :recur_je_data.cr_amount < 0) then 
 MESSAGE('Journal Amounts may not be negative. Please correct the amounts.'); 
 MESSAGE(' ', no_acknowledge); 
 raise form_trigger_failure; 
end if; 
end if; 

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:73
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>RECUR_JE_DATA.PRE-UPDATE</TriggerInfo>

        [RowUpdating]
        public virtual void recurJeData_RowUpdating(RowAdapterEventArgs args)
        {
            RecurJeDataAdapter recurJeDataElement = args.Row as RecurJeDataAdapter;


            int rowCount = 0;
            {
                String sqlprofileCur = "SELECT profile_data " +
" FROM adm.profiles " +
" WHERE profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS' ";
                DataCursor profileCur = new DataCursor(sqlprofileCur);
                TableRow profileRec = null;
                NNumber debitCreditIn_ = NNumber.Null;
                #region
                try
                {
                    if (recurJeDataElement.DrTotal != recurJeDataElement.CrTotal)
                    {
                        TaskServices.Message("Debit and Credit totals are Not equal.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        throw new ApplicationException();
                    }
                    if (Lib.IsNull(recurJeDataElement.AccountId, 0) <= 0)
                    {
                        TaskServices.Message("This Account " + recurJeDataElement.AccountNo + " is Invalid. You must use a valid Account.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        throw new ApplicationException();
                    }
                    //   journal_budget_check;  -- alio-3074 09/03/14 klb added --cec alio-3074 12/3/14 moved to create journal button 
                    if (recurJeDataElement.CrAmount != 0)
                    {
                        recurJeDataElement.JournalAmount = recurJeDataElement.CrAmount;
                        recurJeDataElement.DebitCreditFlag = -(1);
                        debitCreditIn_ = -(1);
                    }
                    else
                    {
                        recurJeDataElement.JournalAmount = recurJeDataElement.DrAmount;
                        recurJeDataElement.DebitCreditFlag = 1;
                        debitCreditIn_ = 1;
                    }
                    profileCur.Open();
                    profileRec = profileCur.FetchRow();
                    if ((Lib.IsNull(profileRec.GetStr("profile_data"), "N") == "Y"))
                    {
                        if ((recurJeDataElement.DrAmount < 0 || recurJeDataElement.CrAmount < 0))
                        {
                            TaskServices.Message("Journal Amounts may not be negative. Please correct the amounts.");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                            throw new ApplicationException();
                        }
                    }
                }
                finally
                {
                    profileCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_DATA.POST-QUERY
        /*
         BEGIN 

:recur_je_data.account_no := get_account_no(:recur_je_data.account_id); 



if :recur_je_data.debit_credit_flag = -1 then 
  :recur_je_data.cr_amount := :recur_je_data.journal_amount; 
else 
  :recur_je_data.dr_amount := :recur_je_data.journal_amount; 
end if; 

:recur_je_data.hold_account_id := :recur_je_data.account_id; --cec alio-3074 
:recur_je_data.hold_journal_amount := 0; --cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
:recur_je_data.hold_cr_amount := 0; --cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
:recur_je_data.hold_dr_amount := 0; --cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 

--cec alio-3074 
if(:recur_je_data.line_no >= nvl(:recur_je_header.next_line_no, 0)) then 
:recur_je_header.next_line_no := :recur_je_data.line_no + 1; 
end if; 

:recur_je_data.account_balance := get_account_balance; 


-- 07/17/06 KLY Ref No 33948 - uncommented this set_record_property statement because it is 
-- prompting to save after querying every record. Cound not find an action saying why this was 
-- commented out. 
--<multilinecomment> 
set_record_property(:system.trigger_record 
                   ,'recur_je_data' 
                   ,status 
                   ,query_status); 
--</multilinecomment> 

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:74
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>RECUR_JE_DATA.POST-QUERY</TriggerInfo>

        [AfterQuery]
        public virtual void recurJeData_AfterQuery(RowAdapterEventArgs args)
        {
            RecurJeDataAdapter recurJeDataElement = args.Row as RecurJeDataAdapter;
            RecurJeHeaderAdapter recurJeHeaderElement = this.Model.RecurJeHeader.CurrentRowAdapter;


            if (!recurJeDataElement.HoldCrAmount.IsNull)
                this.holdCrAmount_PostChange(recurJeDataElement);
            if (!recurJeDataElement.HoldDrAmount.IsNull)
                this.holdDrAmount_PostChange(recurJeDataElement);
            if (!recurJeDataElement.CrAmount.IsNull)
                this.crAmount_PostChange(recurJeDataElement);
            if (!recurJeDataElement.DrAmount.IsNull)
                this.drAmount_PostChange(recurJeDataElement);
            if (!recurJeDataElement.AccountBalance.IsNull)
                this.accountBalance_PostChange(recurJeDataElement);
            recurJeDataElement.AccountNo = this.Task.Services.FGetAccountNo(recurJeDataElement.AccountId);
            if (recurJeDataElement.DebitCreditFlag == -(1))
            {
                recurJeDataElement.CrAmount = recurJeDataElement.JournalAmount;
            }
            else
            {
                recurJeDataElement.DrAmount = recurJeDataElement.JournalAmount;
            }
            recurJeDataElement.HoldAccountId = recurJeDataElement.AccountId;
            // cec alio-3074 
            recurJeDataElement.HoldJournalAmount = 0;
            // cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
            recurJeDataElement.HoldCrAmount = 0;
            // cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
            recurJeDataElement.HoldDrAmount = 0;
            // cec alio-3074 - set to 0 so the journal_budget_check will check the balance for entire amt 
            // cec alio-3074 
            if ((recurJeDataElement.LineNo >= Lib.IsNull(recurJeHeaderElement.NextLineNo, 0)))
            {
                recurJeHeaderElement.NextLineNo = recurJeDataElement.LineNo + 1;
            }
            recurJeDataElement.AccountBalance = this.Task.Services.FGetAccountBalance(recurJeDataElement);
            //  07/17/06 KLY Ref No 33948 - uncommented this set_record_property statement because it is 
            //  prompting to save after querying every record. Cound not find an action saying why this was 
            //  commented out. 
            // /* 
            BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "recur_je_data", RecordStatus.QUERY);
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_DATA.WHEN-VALIDATE-RECORD
        /*
         declare 
   debit_credit_in_ number; 
begin 
-- msg_debug('rjd.wvr'); 
if :recur_je_data.cr_amount <> 0 then 
  :recur_je_data.journal_amount := :recur_je_data.cr_amount; 
  :recur_je_data.debit_credit_flag := -1; 
  debit_credit_in_ := -1; 
ELSE 
  :recur_je_data.journal_amount := :recur_je_data.dr_amount; 
  :recur_je_data.debit_credit_flag := 1; 
  debit_credit_in_ := 1; 
end if; 
end ;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:75
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger RECUR_JE_DATA.WHEN-VALIDATE-RECORD . See documentation for details.
        ///</remarks>
        /// <TriggerInfo>RECUR_JE_DATA.WHEN-VALIDATE-RECORD</TriggerInfo>

        [RecordValidationTrigger]
        public virtual void recurJeData_WhenValidateRecord()
        {

            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;

            {
                NNumber debitCreditIn_ = NNumber.Null;
                //  msg_debug('rjd.wvr'); 
                if (recurJeDataElement.CrAmount != 0)
                {
                    recurJeDataElement.JournalAmount = recurJeDataElement.CrAmount;
                    recurJeDataElement.DebitCreditFlag = -(1);
                    debitCreditIn_ = -(1);
                }
                else
                {
                    recurJeDataElement.JournalAmount = recurJeDataElement.DrAmount;
                    recurJeDataElement.DebitCreditFlag = 1;
                    debitCreditIn_ = 1;
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_DATA.WHEN-NEW-RECORD-INSTANCE
        /*
         declare 

cursor batch_cur(cur_in_batch_year varchar2, 
              cur_in_batch_no varchar2) is 
select batch_description 
 from fas.batches 
where batch_year = cur_in_batch_year 
  and batch_no = cur_in_batch_no; 
v_status varchar2(30); 
begin 
--cec alio-3074 
v_status := :system.record_status; 
--msg_debug('recur_je_data wnri status: ' || v_status); 
if(:recur_je_data.line_no is null) then 
:recur_je_data.line_no := nvl(:recur_je_header.next_line_no, 1); 
 :recur_je_header.next_line_no := :recur_je_data.line_no + 1; 

if :recur_je_data.account_no is null then 

   if :desc_buttons.description_type = 'batch' then 
    open batch_cur(:batch_no_block.batch_year, 
                   :batch_no_block.batch_no); 
    fetch batch_cur into :recur_je_data.journal_description; 
    close batch_cur; 
   end if; 

   if :desc_buttons.description_type = 'repeat' then 
      :recur_je_data.journal_description := :hold.hold_desc; 
   end if; 

end if; 

 if(v_status = 'NEW') then 
  SET_RECORD_PROPERTY (:SYSTEM.trigger_record, 
                       'recur_je_data', 
                       status, 
                       new_status); 
 end if; 
end if; 



end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:76
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>RECUR_JE_DATA.WHEN-NEW-RECORD-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-RECORD-INSTANCE", Function = KeyFunction.RECORD_CHANGE)]
        public virtual void recurJeData_recordChange()
        {

            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;
            RecurJeHeaderAdapter recurJeHeaderElement = this.Model.RecurJeHeader.CurrentRowAdapter;


            int rowCount = 0;
            {
                String sqlbatchCur = "SELECT batch_description " +
" FROM fas.batches " +
" WHERE batch_year = @P_CUR_IN_BATCH_YEAR AND batch_no = @P_CUR_IN_BATCH_NO ";
                DataCursor batchCur = new DataCursor(sqlbatchCur);
                String batchCurCurInBatchYear = string.Empty;
                String batchCurCurInBatchNo = string.Empty;
                string vStatus = string.Empty;
                #region
                try
                {
                    // cec alio-3074 
                    vStatus = TaskServices.RecordStatus;
                    // msg_debug('recur_je_data wnri status: ' || v_status); 
                    if ((recurJeDataElement.LineNo.IsNull))
                    {
                        recurJeDataElement.LineNo = Lib.IsNull(recurJeHeaderElement.NextLineNo, 1);
                        recurJeHeaderElement.NextLineNo = recurJeDataElement.LineNo + 1;
                        if (string.IsNullOrEmpty(recurJeDataElement.AccountNo))
                        {
                            if (Model.DescButtons.DescriptionType == "batch")
                            {
                                batchCurCurInBatchYear = Model.BatchNoBlock.BatchYear;
                                batchCurCurInBatchNo = Model.BatchNoBlock.BatchNo;
                                //Setting query parameters
                                batchCur.AddParameter("@P_CUR_IN_BATCH_YEAR", batchCurCurInBatchYear);
                                batchCur.AddParameter("@P_CUR_IN_BATCH_NO", batchCurCurInBatchNo);
                                batchCur.Open();
                                ResultSet batchCurResults = batchCur.FetchInto();
                                if (batchCurResults != null)
                                {
                                    recurJeDataElement.JournalDescription = batchCurResults.GetString(0);
                                }
                            }
                            if (Model.DescButtons.DescriptionType == "repeat")
                            {
                                recurJeDataElement.JournalDescription = Model.Hold.HoldDesc;
                            }
                        }
                        if ((vStatus == "NEW"))
                        {
                            BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "recur_je_data", RecordStatus.NEW);
                        }
                    }
                }
                finally
                {
                    batchCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_DATA.POST-RECORD
        /*
         begin 

:hold.hold_desc := :recur_je_data.journal_description; 

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:77
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger RECUR_JE_DATA.POST-RECORD . See documentation for details.
        ///</remarks>
        /// <TriggerInfo>RECUR_JE_DATA.POST-RECORD</TriggerInfo>

        [ActionTrigger(Action = "POST-RECORD")]
        public virtual void recurJeData_PostRecord()
        {

            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;


            Model.Hold.HoldDesc = recurJeDataElement.JournalDescription;
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_DATA.WHEN-REMOVE-RECORD
        /*
         <multilinecomment> Formatted on 12/01/2014 12:48:23 PM (QP5 v5.163.1008.3004) </multilinecomment> 
DECLARE 
v_dc_amount              NUMBER DEFAULT 0; 
v_dc_flag                VARCHAR2 (1) DEFAULT NULL; 
v_hold_dc_amount         NUMBER DEFAULT 0;          --cec alio-3074 12/1/14 
v_hold_dc_flag           VARCHAR2 (1) DEFAULT NULL; --cec alio-3074 12/1/14 
v_validate_amount        NUMBER DEFAULT 0; 
v_hold_validate_amount   NUMBER DEFAULT 0;          --cec alio-3074 12/1/14 
v_program                VARCHAR2 (30) DEFAULT 'FJENT03AFMX'; 
v_trace                  VARCHAR2 (10) DEFAULT NULL; 
v_amount                 fas.je_data.journal_amount%TYPE; 
BEGIN 
<multilinecomment>msg_debug ( 
     'begin wrr... line no: ' 
  || :recur_je_data.line_no 
  || ', acct_id: ' 
  || :recur_je_data.account_id 
  || ', je amt: ' 
  || :recur_je_data.journal_amount 
  || ', dr_amt: ' 
  || :recur_je_data.dr_amount 
  || ', cr_amt: ' 
  || :recur_je_data.cr_amount 
  || ', d/c flag: ' 
  || :recur_je_data.debit_credit_flag 
  || ', hold acct: ' 
  || :recur_je_data.hold_account_id 
  || ', hold je amt: ' 
  || :recur_je_data.hold_journal_amount 
  || ', hold_dr_amt: ' 
  || :recur_je_data.hold_dr_amount 
  || ', hold_cr_amt: ' 
  || :recur_je_data.hold_cr_amount 
  || ', status: ' 
  || :system.record_status);</multilinecomment> 
if(:system.record_status <> 'QUERY') then 
IF (:recur_je_data.account_id IS NOT NULL AND :recur_je_data.line_no IS NOT NULL) 
THEN 
  IF :recur_je_data.dr_amount <> 0 
  THEN 
     v_dc_amount := :recur_je_data.dr_amount; 
     v_dc_flag := 'D'; 
  ELSIF :recur_je_data.cr_amount <> 0 
  THEN 
     v_dc_amount := :recur_je_data.cr_amount; 
     v_dc_flag := 'C'; 
  END IF; 

  v_validate_amount := 
     fjent01a_amt_validate_budget_ (v_dc_amount, 
                                    v_dc_flag, 
                                    NULL, 
                                    :recur_je_data.account_id); 
  --accounting_array.remove_line_no(:recur_je_data.account_id, :recur_je_data.line_no); 
  accounting_array.update_accounting_tab (:recur_je_data.account_id, 
                                          v_validate_amount * -1, 
                                          :recur_je_data.account_no, 
                                          :recur_je_data.line_no); 
END IF; 
IF (NVL (:recur_je_data.hold_account_id, -1) <> NVL (:recur_je_data.account_id, -1) 
   AND :recur_je_data.line_no IS NOT NULL) 
THEN 
  IF :recur_je_data.hold_dr_amount <> 0 
  THEN 
     v_dc_amount := :recur_je_data.hold_dr_amount; 
     v_dc_flag := 'D'; 
  ELSIF :recur_je_data.hold_cr_amount <> 0 
  THEN 
     v_dc_amount := :recur_je_data.hold_cr_amount; 
     v_dc_flag := 'C'; 
  END IF; 

  v_validate_amount := 
     fjent01a_amt_validate_budget_ (v_dc_amount, 
                                    v_dc_flag, 
                                    NULL, 
                                    :recur_je_data.hold_account_id); 
  accounting_array.update_accounting_tab (:recur_je_data.hold_account_id, 
                                          v_validate_amount * -1, 
                                          NULL, 
                                          :recur_je_data.line_no); 
END IF; 
end if; 
END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:78
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>RECUR_JE_DATA.WHEN-REMOVE-RECORD</TriggerInfo>

        [RowRemoved]
        public virtual void recurJeData_RowRemoved(RowAdapterEventArgs args)
        {
            //  Formatted on 12/01/2014 12:48:23 PM (QP5 v5.163.1008.3004) 
            RecurJeDataAdapter recurJeDataElement = args.Row as RecurJeDataAdapter;


            {
                NNumber vDcAmount = 0;
                string vDcFlag = null;
                NNumber vHoldDcAmount = 0;
                // cec alio-3074 12/1/14 
                string vHoldDcFlag = null;
                // cec alio-3074 12/1/14 
                NNumber vValidateAmount = 0;
                NNumber vHoldValidateAmount = 0;
                // cec alio-3074 12/1/14 
                string vProgram = "FJENT03AFMX";
                string vTrace = null;
                NNumber vAmount = NNumber.Null;
                // msg_debug (
                // 'begin wrr... line no: '
                // || :recur_je_data.line_no
                // || ', acct_id: '
                // || :recur_je_data.account_id
                // || ', je amt: '
                // || :recur_je_data.journal_amount
                // || ', dr_amt: '
                // || :recur_je_data.dr_amount
                // || ', cr_amt: '
                // || :recur_je_data.cr_amount
                // || ', d/c flag: '
                // || :recur_je_data.debit_credit_flag
                // || ', hold acct: '
                // || :recur_je_data.hold_account_id
                // || ', hold je amt: '
                // || :recur_je_data.hold_journal_amount
                // || ', hold_dr_amt: '
                // || :recur_je_data.hold_dr_amount
                // || ', hold_cr_amt: '
                // || :recur_je_data.hold_cr_amount
                // || ', status: '
                // || :system.record_status);
                if ((TaskServices.RecordStatus != "QUERY"))
                {
                    if ((!recurJeDataElement.AccountId.IsNull && !recurJeDataElement.LineNo.IsNull))
                    {
                        if (recurJeDataElement.DrAmount != 0)
                        {
                            vDcAmount = recurJeDataElement.DrAmount;
                            vDcFlag = "D";
                        }
                        else if (recurJeDataElement.CrAmount != 0)
                        {
                            vDcAmount = recurJeDataElement.CrAmount;
                            vDcFlag = "C";
                        }

                        vValidateAmount = StoredProcedures.Fjent01aAmtValidateBudget_(vDcAmount, vDcFlag, null, recurJeDataElement.AccountId);

                        // accounting_array.remove_line_no(:recur_je_data.account_id, :recur_je_data.line_no); 
                        Task.Packages.AccountingArray.UpdateAccountingTab(recurJeDataElement.AccountId, vValidateAmount * -(1), recurJeDataElement.AccountNo, recurJeDataElement.LineNo);
                    }
                    if ((Lib.IsNull(recurJeDataElement.HoldAccountId, -(1)) != Lib.IsNull(recurJeDataElement.AccountId, -(1)) && !recurJeDataElement.LineNo.IsNull))
                    {
                        if (recurJeDataElement.HoldDrAmount != 0)
                        {
                            vDcAmount = recurJeDataElement.HoldDrAmount;
                            vDcFlag = "D";
                        }
                        else if (recurJeDataElement.HoldCrAmount != 0)
                        {
                            vDcAmount = recurJeDataElement.HoldCrAmount;
                            vDcFlag = "C";
                        }

                        vValidateAmount = StoredProcedures.Fjent01aAmtValidateBudget_(vDcAmount, vDcFlag, null, recurJeDataElement.HoldAccountId);

                        Task.Packages.AccountingArray.UpdateAccountingTab(recurJeDataElement.HoldAccountId, vValidateAmount * -(1), null, recurJeDataElement.LineNo);
                    }
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER RECUR_JE_DATA.PRE-QUERY
        /*
         <multilinecomment> 
NJA Alio 12797 added the pre query trigger to look up the profile_data and determine if the negative amounts should be shown 
</multilinecomment> 

declare 

cursor profile_cur is 
select profile_data 
from adm.profiles 
where profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS'; 

v_profile varchar2(1) := 'N'; 
v_default_where varchar2(1000) := null; 

begin 

open profile_cur; 
fetch profile_cur into v_profile; 
close profile_cur; 

if(v_profile = 'Y') then 
--v_default_where := 'where recur_je_data.cr_amount > 0 and recur_je_data.dr_amount > 0'; 
v_default_where := 'where recur_je_data.journal_amount > 0'; 
SET_BLOCK_PROPERTY('recur_je_data', DEFAULT_WHERE ,v_default_where); 
end if; 

end; 

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:79
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>RECUR_JE_DATA.PRE-QUERY</TriggerInfo>

        [BeforeQuery]
        public virtual void recurJeData_BeforeQuery(QueryEventArgs args)
        {
            // NJA Alio 12797 added the pre query trigger to look up the profile_data and determine if the negative amounts should be shown
            int rowCount = 0;
            {
                String sqlprofileCur = "SELECT profile_data " +
" FROM adm.profiles " +
" WHERE profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS' ";
                DataCursor profileCur = new DataCursor(sqlprofileCur);
                string vProfile = "N";
                string vDefaultWhere = null;
                #region
                try
                {
                    profileCur.Open();
                    ResultSet profileCurResults = profileCur.FetchInto();
                    if (profileCurResults != null)
                    {
                        vProfile = profileCurResults.GetString(0);
                    }
                    if ((vProfile == "Y"))
                    {
                        // v_default_where := 'where recur_je_data.cr_amount > 0 and recur_je_data.dr_amount > 0'; 
                        vDefaultWhere = "where recur_je_data.journal_amount > 0";
                        BlockServices.SetWhereClause("recur_je_data", vDefaultWhere);
                    }
                }
                finally
                {
                    profileCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.WHEN-VALIDATE-ITEM
        /*
         DECLARE 

batch_year     FAS.BATCHES.BATCH_YEAR%TYPE  := :BATCH_NO_BLOCK.BATCH_YEAR;     -- Batch/Account Year NJA ALIO 307 ADDED THE %TYPE 
bank_no        SHR.BANK_MASTER.BANK_NO%TYPE  := NULL;                           -- Bank Number OR NULL NJA ALIO 307 ADDED THE %TYPE 
trans_desc     varchar2(8)  := 'AJE';                          -- TRANSACTION_DESCRIPTION 
debit_credit   number(2)    := '-1';                           -- ( +1, 0, -1) 
send_status    varchar2(1)  := '1';                            -- (1)Primary Acct, (2)Secondary Acct, (3)Both 
call_from      varchar2(1)  := 'V';                            -- (L)OV, (T)rigger, (V)alidate 

hold_null      varchar2(1);                                    -- Used for Return of only one account 

BEGIN 

if :system.mode = 'NORMAL'  and 
  :recur_je_data.account_no is not null then 
  validate_account.load_values(batch_year, 
                               bank_no,              
                               trans_desc, 
                               debit_credit, 
                               send_status, 
                               call_from, 
                               :recur_je_data.account_no,      --:BLOCK_NAME.<Primary Account NO> 
                               :recur_je_data.account_id,      --:BLOCK_NAME.<Primary Account ID> 
                               hold_null,                --:BLOCK_NAME.<Secondary Account NO> 
                               hold_null                 --:BLOCK_NAME.<Secondary Account ID> 
                               ); 
:recur_je_data.account_balance := get_account_balance; 
end if; 

END;         
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:46
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_NO.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "ACCOUNT_NO")]
        [Before]
        public virtual void accountNo_validate()
        {

            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;
            {
                NNumber batchYear = Model.BatchNoBlock.BatchYear;
                //  Batch/Account Year NJA ALIO 307 ADDED THE %TYPE 
                string bankNo = string.Empty;
                //  Bank Number OR NULL NJA ALIO 307 ADDED THE %TYPE 
                string transDesc = "AJE";
                //  TRANSACTION_DESCRIPTION 
                NNumber debitCredit = "-1";
                //  ( +1, 0, -1) 
                string sendStatus = "1";
                //  (1)Primary Acct, (2)Secondary Acct, (3)Both 
                string callFrom = "V";
                //  (L)OV, (T)rigger, (V)alidate 
                string holdNull1 = string.Empty;
                NNumber holdNull2 = NNumber.Default;
                if (TaskServices.Mode == "NORMAL" && !string.IsNullOrEmpty(recurJeDataElement.AccountNo))
                {
                    String acctNo1_ref = recurJeDataElement.AccountNo;
                    NNumber acctId1_ref = recurJeDataElement.AccountId;

                    Task.Packages.ValidateAccount.LoadValues(batchYear, ref bankNo, transDesc, debitCredit, sendStatus, callFrom, ref acctNo1_ref, ref acctId1_ref, ref holdNull1, ref holdNull2);

                    recurJeDataElement.AccountNo = acctNo1_ref;
                    recurJeDataElement.AccountId = acctId1_ref;
                    recurJeDataElement.AccountBalance = this.Task.Services.FGetAccountBalance(recurJeDataElement);
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.KEY-LISTVAL
        /*
         DECLARE 

batch_year     FAS.BATCHES.BATCH_YEAR%TYPE  := :BATCH_NO_BLOCK.BATCH_YEAR;     -- Batch/Account YearNJA ALIO 307 ADDED THE %TYPE 
bank_no        SHR.BANK_MASTER.BANK_NO%TYPE  := NULL;                           -- Bank Number OR NULL NJA ALIO 307 ADDED THE %TYEP 
trans_desc     varchar2(8)  := 'AJE';                          -- TRANSACTION_DESCRIPTION 
debit_credit   number(2)    := '-1';                           -- ( +1, 0, -1) 
send_status    varchar2(1)  := '1';                            -- (1)Primary Acct, (2)Secondary Acct, (3)Both 
call_from      varchar2(1)  := 'L';                            -- (L)OV, (T)rigger, (V)alidate 

hold_null      varchar2(1);                                    -- Used for Return of only one account 

BEGIN 

  validate_account.load_values(batch_year, 
                               bank_no,              
                               trans_desc, 
                               debit_credit, 
                               send_status, 
                               call_from, 
                               :recur_je_data.account_no,      --:BLOCK_NAME.<Primary Account NO> 
                               :recur_je_data.account_id,      --:BLOCK_NAME.<Primary Account ID> 
                               hold_null,                      --:BLOCK_NAME.<Secondary Account NO> 
                               hold_null                       --:BLOCK_NAME.<Secondary Account ID> 
                               ); 
:recur_je_data.account_balance := get_account_balance; 

END;         
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:47
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_NO.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "ACCOUNT_NO", Function = KeyFunction.LIST_VALUES)]
        public virtual void accountNo_ListValues()
        {

            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;


            {
                NNumber batchYear = Model.BatchNoBlock.BatchYear;
                //  Batch/Account YearNJA ALIO 307 ADDED THE %TYPE 
                string bankNo = string.Empty;
                //  Bank Number OR NULL NJA ALIO 307 ADDED THE %TYEP 
                string transDesc = "AJE";
                //  TRANSACTION_DESCRIPTION 
                NNumber debitCredit = "-1";
                //  ( +1, 0, -1) 
                string sendStatus = "1";
                //  (1)Primary Acct, (2)Secondary Acct, (3)Both 
                string callFrom = "L";
                //  (L)OV, (T)rigger, (V)alidate 
                string holdNull1 = string.Empty;
                NNumber holdNull2 = NNumber.Default;
                String acctNo1_ref = recurJeDataElement.AccountNo;
                NNumber acctId1_ref = recurJeDataElement.AccountId;

                Task.Packages.ValidateAccount.LoadValues(batchYear, ref bankNo, transDesc, debitCredit, sendStatus, callFrom, ref acctNo1_ref, ref acctId1_ref, ref holdNull1, ref holdNull2);

                recurJeDataElement.AccountNo = acctNo1_ref;
                recurJeDataElement.AccountId = acctId1_ref;
                recurJeDataElement.AccountBalance = this.Task.Services.FGetAccountBalance(recurJeDataElement);
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.POST-TEXT-ITEM
        /*
            :recur_je_data.account_balance := get_account_balance;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:48
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_NO.POST-TEXT-ITEM</TriggerInfo>

        [ActionTrigger(Action = "POST-TEXT-ITEM", Item = "ACCOUNT_NO", Function = KeyFunction.ITEM_OUT)]
        public virtual void accountNo_itemOut()
        {

            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;


            recurJeDataElement.AccountBalance = this.Task.Services.FGetAccountBalance(recurJeDataElement);
        }


        #region Original PL/SQL code code for TRIGGER SHOW_LOV.WHEN-BUTTON-PRESSED
        /*
         BEGIN 
go_item('recur_je_data.account_no'); 
do_key('list_values'); 
END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:50
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>SHOW_LOV.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "ACCOUNT_NO")]
        public virtual void showLov_buttonClick()
        {
            ItemServices.GoItem("recur_je_data.account_no");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_BALANCE.POST-CHANGE
        /*
         BEGIN 

if :recur_je_data.journal_amount <> nvl(:recur_je_data.dr_amount, 
                                       :recur_je_data.journal_amount) then 
 :recur_je_data.journal_amount := :recur_je_data.dr_amount; 
 :recur_je_data.debit_credit_flag := 1; 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:52
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger ACCOUNT_BALANCE.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="recurJeDataElement"></param>
        /// <TriggerInfo>ACCOUNT_BALANCE.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "ACCOUNT_BALANCE")]
        public virtual void accountBalance_PostChange(RecurJeDataAdapter recurJeDataElement)
        {
            if (recurJeDataElement.JournalAmount != Lib.IsNull(recurJeDataElement.DrAmount, recurJeDataElement.JournalAmount))
            {
                recurJeDataElement.JournalAmount = recurJeDataElement.DrAmount;
                recurJeDataElement.DebitCreditFlag = 1;
            }
        }


        #region Original PL/SQL code code for TRIGGER DR_AMOUNT.POST-CHANGE
        /*
         BEGIN 

if :recur_je_data.journal_amount <> nvl(:recur_je_data.dr_amount, 
                                       :recur_je_data.journal_amount) then 
 :recur_je_data.journal_amount := :recur_je_data.dr_amount; 
 :recur_je_data.debit_credit_flag := 1; 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:54
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger DR_AMOUNT.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="recurJeDataElement"></param>
        /// <TriggerInfo>DR_AMOUNT.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "DR_AMOUNT")]
        public virtual void drAmount_PostChange(RecurJeDataAdapter recurJeDataElement)
        {
            if (recurJeDataElement.JournalAmount != Lib.IsNull(recurJeDataElement.DrAmount, recurJeDataElement.JournalAmount))
            {
                recurJeDataElement.JournalAmount = recurJeDataElement.DrAmount;
                recurJeDataElement.DebitCreditFlag = 1;
            }
        }


        #region Original PL/SQL code code for TRIGGER CR_AMOUNT.POST-CHANGE
        /*
         BEGIN 

if :recur_je_data.journal_amount <> nvl(:recur_je_data.cr_amount, 
                                       :recur_je_data.journal_amount) then 
 :recur_je_data.journal_amount := :recur_je_data.cr_amount; 
 :recur_je_data.debit_credit_flag := -1; 
end if; 

END; 

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:56
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger CR_AMOUNT.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="recurJeDataElement"></param>
        /// <TriggerInfo>CR_AMOUNT.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "CR_AMOUNT")]
        public virtual void crAmount_PostChange(RecurJeDataAdapter recurJeDataElement)
        {
            if (recurJeDataElement.JournalAmount != Lib.IsNull(recurJeDataElement.CrAmount, recurJeDataElement.JournalAmount))
            {
                recurJeDataElement.JournalAmount = recurJeDataElement.CrAmount;
                recurJeDataElement.DebitCreditFlag = -(1);
            }
        }


        #region Original PL/SQL code code for TRIGGER RESEQUENCE.WHEN-BUTTON-PRESSED
        /*
         BEGIN 

resequence; 

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:65
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>RESEQUENCE.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "RESEQUENCE")]
        public virtual void resequence_buttonClick()
        {

            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;


            this.Task.Services.Resequence(recurJeDataElement);
        }


        #region Original PL/SQL code code for TRIGGER HOLD_DR_AMOUNT.POST-CHANGE
        /*
         BEGIN 

if :recur_je_data.hold_dr_amount <> 0 then 
  :recur_je_data.hold_journal_amount := :recur_je_data.hold_dr_amount; 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:69
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger HOLD_DR_AMOUNT.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="recurJeDataElement"></param>
        /// <TriggerInfo>HOLD_DR_AMOUNT.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "HOLD_DR_AMOUNT")]
        public virtual void holdDrAmount_PostChange(RecurJeDataAdapter recurJeDataElement)
        {
            if (recurJeDataElement.HoldDrAmount != 0)
            {
                recurJeDataElement.HoldJournalAmount = recurJeDataElement.HoldDrAmount;
            }
        }


        #region Original PL/SQL code code for TRIGGER HOLD_CR_AMOUNT.POST-CHANGE
        /*
         BEGIN 

if :recur_je_data.hold_cr_amount <> 0 then 
  :recur_je_data.hold_journal_amount := :recur_je_data.hold_cr_amount; 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:71
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger HOLD_CR_AMOUNT.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="recurJeDataElement"></param>
        /// <TriggerInfo>HOLD_CR_AMOUNT.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "HOLD_CR_AMOUNT")]
        public virtual void holdCrAmount_PostChange(RecurJeDataAdapter recurJeDataElement)
        {
            if (recurJeDataElement.HoldCrAmount != 0)
            {
                recurJeDataElement.HoldJournalAmount = recurJeDataElement.HoldCrAmount;
            }
        }


        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_BALANCE.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "ACCOUNT_BALANCE")]
        public virtual void accountBalance_validate()
        {
            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;
            this.accountBalance_PostChange(recurJeDataElement);

        }


        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>DR_AMOUNT.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "DR_AMOUNT")]
        public virtual void drAmount_validate()
        {
            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;
            this.drAmount_PostChange(recurJeDataElement);

        }


        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>CR_AMOUNT.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "CR_AMOUNT")]
        public virtual void crAmount_validate()
        {
            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;
            this.crAmount_PostChange(recurJeDataElement);

        }


        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>HOLD_DR_AMOUNT.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "HOLD_DR_AMOUNT")]
        public virtual void holdDrAmount_validate()
        {
            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;
            this.holdDrAmount_PostChange(recurJeDataElement);

        }


        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>HOLD_CR_AMOUNT.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "HOLD_CR_AMOUNT")]
        public virtual void holdCrAmount_validate()
        {
            RecurJeDataAdapter recurJeDataElement = this.Model.RecurJeData.CurrentRowAdapter;
            this.holdCrAmount_PostChange(recurJeDataElement);

        }



        #endregion

    }
}
