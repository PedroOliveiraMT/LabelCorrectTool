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

namespace Alio.Forms.Fjent01a.Controllers
{


    public class JeDataController : BaseBlockController<Fjent01aTask, Fjent01aModel>
    {

        public JeDataController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER JE_DATA.PRE-INSERT
        /*
         declare 

cursor next_line_no_cur is 
select max(line_no) + 1 
 from fas.je_data 
where je_data.reference_no = :je_header.reference_no; 

cursor profile_cur is 
select profile_data 
from adm.profiles 
where profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS'; 

profile_rec profile_cur%rowtype; 

debit_credit_in_  number; 
alert_status number; 

BEGIN 

<multilinecomment> 
if nvl(:je_data.dr_total,0) <> nvl(:je_data.cr_total,0) then 
  Message('Debit and Credit totals are Not equal.'); 
  Message(' ',no_acknowledge); 
  --raise form_trigger_failure; 
end if; 
</multilinecomment> 
if nvl(:je_data.dr_total,0) <> nvl(:je_data.cr_total,0) and 
  nvl(:batch_no_block.save_flag,'N') <> 'Y' then 

 alert_status := show_alert('debit_credit'); 
 if alert_status = alert_button1 then 
   :batch_no_block.save_flag := 'Y'; 
 else 
   :batch_no_block.save_flag := 'N'; 
   raise form_trigger_failure; 
 end if; 
end if; 

if nvl(:je_data.account_id,0) <= 0 then 
  Message('This Account ' 
         ||:je_data.account_no 
         ||' is Invalid. You must use a valid Account.'); 
  Message(' ',no_acknowledge); 
  raise form_trigger_failure; 
end if; 


open profile_cur; 
fetch profile_cur into profile_rec; 
close profile_cur; 

if (nvl(profile_rec.profile_data, 'N') = 'Y') then 
if(:je_data.dr_amount < 0 or :je_data.cr_amount < 0) then 
 MESSAGE('Journal Amounts may not be negative. Please correct the amounts.'); 
 MESSAGE(' ', no_acknowledge); 
 raise form_trigger_failure; 
end if; 
end if; 



--   if :je_data.cr_amount <> 0 and 
--      :je_data.dr_amount <> 0 then 
--      message ('Invalid Amounts - Enter Only One Amount'); 
--      message (' ',no_acknowledge); 
--      raise form_trigger_failure; 
--   end if; 

--  journal_budget_check;  -- alio-3074 09/03/14 klb added --cec alio-3074 12/1/14 moved to wvri trigger 

if :je_data.cr_amount <> 0 then 
  :je_data.journal_amount := :je_data.cr_amount; 
  :je_data.debit_credit_flag := -1; 
  debit_credit_in_ := -1; 
elsif :je_data.dr_amount <> 0 then 
  :je_data.journal_amount := :je_data.dr_amount; 
  :je_data.debit_credit_flag := 1; 
  debit_credit_in_ := 1; 
else 
 :je_data.journal_amount := 0; 
 :je_data.debit_credit_flag := 1; 
end if; 
<multilinecomment> --cec alio-3074 12/1/14 moved to wnri trigger 
open next_line_no_cur; 
fetch next_line_no_cur into :je_data.line_no; 
    if :je_data.line_no is null then 
       :je_data.line_no := 1; 
    end if; 
close next_line_no_cur;</multilinecomment> 

END;   


        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:81
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_DATA.PRE-INSERT</TriggerInfo>

        [RowInserting]
        public virtual void jeData_RowInserting(RowAdapterEventArgs args)
        {
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;
            JeDataAdapter jeDataElement = args.Row as JeDataAdapter;


            int rowCount = 0;
            {
                String sqlnextLineNoCur = "SELECT max(line_no) + 1 " +
" FROM fas.je_data " +
" WHERE je_data.reference_no = @JE_HEADER_REFERENCE_NO ";
                DataCursor nextLineNoCur = new DataCursor(sqlnextLineNoCur);
                String sqlprofileCur = "SELECT profile_data " +
" FROM adm.profiles " +
" WHERE profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS' ";
                DataCursor profileCur = new DataCursor(sqlprofileCur);
                TableRow profileRec = null;
                NNumber debitCreditIn_ = NNumber.Null;
                NNumber alertStatus = NNumber.Null;
                #region
                try
                {
                    // if nvl(:je_data.dr_total,0) <> nvl(:je_data.cr_total,0) then
                    // Message('Debit and Credit totals are Not equal.');
                    // Message(' ',no_acknowledge);
                    // --raise form_trigger_failure;
                    // end if;
                    if (Lib.IsNull(jeDataElement.DrTotal, 0) != Lib.IsNull(jeDataElement.CrTotal, 0) && Lib.IsNull(Model.BatchNoBlock.SaveFlag, "N") != "Y")
                    {
                        alertStatus = TaskServices.ShowAlert("debit_credit");
                        if (alertStatus == MessageServices.BUTTON1)
                        {
                            Model.BatchNoBlock.SaveFlag = "Y";
                        }
                        else
                        {
                            Model.BatchNoBlock.SaveFlag = "N";
                            throw new ApplicationException();
                        }
                    }
                    if (Lib.IsNull(jeDataElement.AccountId, 0) <= 0)
                    {
                        TaskServices.Message("This Account " + jeDataElement.AccountNo + " is Invalid. You must use a valid Account.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        throw new ApplicationException();
                    }
                    profileCur.Open();
                    profileRec = profileCur.FetchRow();
                    profileCur.Close();
                    if ((Lib.IsNull(profileRec.GetStr("profile_data"), "N") == "Y"))
                    {
                        if ((jeDataElement.DrAmount < 0 || jeDataElement.CrAmount < 0))
                        {
                            TaskServices.Message("Journal Amounts may not be negative. Please correct the amounts.");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                            throw new ApplicationException();
                        }
                    }
                    //    if :je_data.cr_amount <> 0 and 
                    //       :je_data.dr_amount <> 0 then 
                    //       message ('Invalid Amounts - Enter Only One Amount'); 
                    //       message (' ',no_acknowledge); 
                    //       raise form_trigger_failure; 
                    //    end if; 
                    //   journal_budget_check;  -- alio-3074 09/03/14 klb added --cec alio-3074 12/1/14 moved to wvri trigger 
                    if (jeDataElement.CrAmount != 0)
                    {
                        jeDataElement.JournalAmount = jeDataElement.CrAmount;
                        jeDataElement.DebitCreditFlag = -(1);
                        debitCreditIn_ = -(1);
                    }
                    else if (jeDataElement.DrAmount != 0)
                    {
                        jeDataElement.JournalAmount = jeDataElement.DrAmount;
                        jeDataElement.DebitCreditFlag = 1;
                        debitCreditIn_ = 1;
                    }
                    else
                    {
                        jeDataElement.JournalAmount = 0;
                        jeDataElement.DebitCreditFlag = 1;
                    }
                }
                finally
                {
                    profileCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.PRE-UPDATE
        /*
         declare 
cursor profile_cur is 
select profile_data 
from adm.profiles 
where profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS'; 

profile_rec profile_cur%rowtype; 

debit_credit_in_  number; 
alert_status number; 

BEGIN 


<multilinecomment> 
if nvl(:je_data.dr_total,0) <> nvl(:je_data.cr_total,0) then 
  Message('Debit and Credit totals are Not equal.'); 
  Message(' ',no_acknowledge); 
  --raise form_trigger_failure; 
end if; 
</multilinecomment> 
if nvl(:je_data.dr_total,0) <> nvl(:je_data.cr_total,0) and 
  nvl(:batch_no_block.save_flag,'N') <> 'Y' then 

 alert_status := show_alert('debit_credit'); 
 if alert_status = alert_button1 then 
   :batch_no_block.save_flag := 'Y'; 
 else 
   :batch_no_block.save_flag := 'N'; 
   raise form_trigger_failure; 
 end if; 
end if; 


if nvl(:je_data.account_id,0) <= 0 then 
  Message('This Account ' 
         ||:je_data.account_no 
         ||' is Invalid. You must use a valid Account.'); 
  Message(' ',no_acknowledge); 
  raise form_trigger_failure; 
end if; 


open profile_cur; 
fetch profile_cur into profile_rec; 
close profile_cur; 

if (nvl(profile_rec.profile_data, 'N') = 'Y') then 
if(:je_data.dr_amount < 0 or :je_data.cr_amount < 0) then 
 MESSAGE('Journal Amounts may not be negative. Please correct the amounts.'); 
 MESSAGE(' ', no_acknowledge); 
 raise form_trigger_failure; 
end if; 
end if; 

--  if :je_data.cr_amount <> 0 and 
--     :je_data.dr_amount <> 0 then 
--     message ('Invalid Amounts - Enter Only One Amount'); 
--     message (' ',no_acknowledge); 
--     raise form_trigger_failure; 
--  end if; 

--  journal_budget_check;  -- alio-3074 09/03/14 klb added --cec 12/1/14 moved to wvr trigger 

if :je_data.cr_amount <> 0 then 
  :je_data.journal_amount := :je_data.cr_amount; 
  :je_data.debit_credit_flag := -1; 
  debit_credit_in_ := -1; 
elsif :je_data.dr_amount <> 0 then 
  :je_data.journal_amount := :je_data.dr_amount; 
  :je_data.debit_credit_flag := 1; 
  debit_credit_in_ := 1; 
else 
 :je_data.journal_amount := 0; 
 :je_data.debit_credit_flag := 1; 
end if; 


END;   


        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:82
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_DATA.PRE-UPDATE</TriggerInfo>

        [RowUpdating]
        public virtual void jeData_RowUpdating(RowAdapterEventArgs args)
        {
            JeDataAdapter jeDataElement = args.Row as JeDataAdapter;


            int rowCount = 0;
            {
                String sqlprofileCur = "SELECT profile_data " +
" FROM adm.profiles " +
" WHERE profile_key = 'FJENT_DISALLOW_NEGATIVE_AMOUNTS' ";
                DataCursor profileCur = new DataCursor(sqlprofileCur);
                TableRow profileRec = null;
                NNumber debitCreditIn_ = NNumber.Null;
                NNumber alertStatus = NNumber.Null;
                #region
                try
                {
                    // if nvl(:je_data.dr_total,0) <> nvl(:je_data.cr_total,0) then
                    // Message('Debit and Credit totals are Not equal.');
                    // Message(' ',no_acknowledge);
                    // --raise form_trigger_failure;
                    // end if;
                    if (Lib.IsNull(jeDataElement.DrTotal, 0) != Lib.IsNull(jeDataElement.CrTotal, 0) && Lib.IsNull(Model.BatchNoBlock.SaveFlag, "N") != "Y")
                    {
                        alertStatus = TaskServices.ShowAlert("debit_credit");
                        if (alertStatus == MessageServices.BUTTON1)
                        {
                            Model.BatchNoBlock.SaveFlag = "Y";
                        }
                        else
                        {
                            Model.BatchNoBlock.SaveFlag = "N";
                            throw new ApplicationException();
                        }
                    }
                    if (Lib.IsNull(jeDataElement.AccountId, 0) <= 0)
                    {
                        TaskServices.Message("This Account " + jeDataElement.AccountNo + " is Invalid. You must use a valid Account.");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        throw new ApplicationException();
                    }
                    profileCur.Open();
                    profileRec = profileCur.FetchRow();
                    if ((Lib.IsNull(profileRec.GetStr("profile_data"), "N") == "Y"))
                    {
                        if ((jeDataElement.DrAmount < 0 || jeDataElement.CrAmount < 0))
                        {
                            TaskServices.Message("Journal Amounts may not be negative. Please correct the amounts.");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                            throw new ApplicationException();
                        }
                    }
                    //   if :je_data.cr_amount <> 0 and 
                    //      :je_data.dr_amount <> 0 then 
                    //      message ('Invalid Amounts - Enter Only One Amount'); 
                    //      message (' ',no_acknowledge); 
                    //      raise form_trigger_failure; 
                    //   end if; 
                    //   journal_budget_check;  -- alio-3074 09/03/14 klb added --cec 12/1/14 moved to wvr trigger 
                    if (jeDataElement.CrAmount != 0)
                    {
                        jeDataElement.JournalAmount = jeDataElement.CrAmount;
                        jeDataElement.DebitCreditFlag = -(1);
                        debitCreditIn_ = -(1);
                    }
                    else if (jeDataElement.DrAmount != 0)
                    {
                        jeDataElement.JournalAmount = jeDataElement.DrAmount;
                        jeDataElement.DebitCreditFlag = 1;
                        debitCreditIn_ = 1;
                    }
                    else
                    {
                        jeDataElement.JournalAmount = 0;
                        jeDataElement.DebitCreditFlag = 1;
                    }
                }
                finally
                {
                    profileCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.POST-QUERY
        /*
         BEGIN 

:je_data.account_no := get_account_no(:je_data.account_id); 

:je_data.account_desc := get_account_desc(:je_data.account_id); 

if :je_data.debit_credit_flag = -1 then 
  :je_data.cr_amount := :je_data.journal_amount; 
else 
  :je_data.dr_amount := :je_data.journal_amount; 
end if; 

:je_data.hold_account_id := :je_data.account_id; --cec alio-3074 
:je_data.hold_journal_amount := :je_data.journal_amount; --cec alio-3074 
:je_data.hold_cr_amount := :je_data.cr_amount; --cec alio-3074 
:je_data.hold_dr_amount := :je_data.dr_amount; --cec alio-3074 


if(:je_data.line_no >= nvl(:je_header.next_line_no, 0)) then 
:je_header.next_line_no := :je_data.line_no + 1; 
end if; 

:je_data.account_balance := get_account_balance; 

set_record_property(:system.trigger_record 
                ,'je_data' 
                ,status 
                ,query_status); 



END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:83
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_DATA.POST-QUERY</TriggerInfo>

        [AfterQuery]
        public virtual void jeData_AfterQuery(RowAdapterEventArgs args)
        {
            JeDataAdapter jeDataElement = args.Row as JeDataAdapter;
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;


            if (!jeDataElement.AccountBalance.IsNull)
                this.accountBalance_PostChange(jeDataElement);
            if (!jeDataElement.HoldCrAmount.IsNull)
                this.holdCrAmount_PostChange(jeDataElement);
            if (!jeDataElement.HoldDrAmount.IsNull)
                this.holdDrAmount_PostChange(jeDataElement);
            if (!jeDataElement.CrAmount.IsNull)
                this.crAmount_PostChange(jeDataElement);
            if (!jeDataElement.DrAmount.IsNull)
                this.drAmount_PostChange(jeDataElement);
            jeDataElement.AccountNo = this.Task.Services.FGetAccountNo(jeDataElement.AccountId);
            jeDataElement.AccountDesc = this.Task.Services.FGetAccountDesc(jeDataElement.AccountId);
            if (jeDataElement.DebitCreditFlag == -(1))
            {
                jeDataElement.CrAmount = jeDataElement.JournalAmount;
            }
            else
            {
                jeDataElement.DrAmount = jeDataElement.JournalAmount;
            }
            jeDataElement.HoldAccountId = jeDataElement.AccountId;
            // cec alio-3074 
            jeDataElement.HoldJournalAmount = jeDataElement.JournalAmount;
            // cec alio-3074 
            jeDataElement.HoldCrAmount = jeDataElement.CrAmount;
            // cec alio-3074 
            jeDataElement.HoldDrAmount = jeDataElement.DrAmount;
            // cec alio-3074 
            if ((jeDataElement.LineNo >= Lib.IsNull(jeHeaderElement.NextLineNo, 0)))
            {
                jeHeaderElement.NextLineNo = jeDataElement.LineNo + 1;
            }
            jeDataElement.AccountBalance = this.Task.Services.FGetAccountBalance(jeDataElement);
            BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "je_data", RecordStatus.QUERY);
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.KEY-CREREC
        /*
         BEGIN 

last_record; 
create_record; 

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:84
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>JE_DATA.KEY-CREREC</TriggerInfo>

        [ActionTrigger(Action = "KEY-CREREC", Function = KeyFunction.CREATE_RECORD)]
        public virtual void jeData_CreateRecord()
        {
            BlockServices.LastRecord();
            BlockServices.CreateRecord();
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.WHEN-VALIDATE-RECORD
        /*
         <multilinecomment> Formatted on 12/01/2014 7:25:36 AM (QP5 v5.163.1008.3004) </multilinecomment>
DECLARE
BEGIN
<multilinecomment> 08/11/17 psr alio-15391 Commented. This method was not acceptable.
msg_debug('je_data.wvr ' || :Parameter.SHOW_MESSAGE);
-- 06/22/17 psr alio-15391 Added If to Raise form_trigger_failure if Account No is invalid.
If :je_data.account_id = -1 then
-- 07/31/17 psr alio-15391 Added :Parameter.SHOW_MESSAGE and Moved Message() inside If
-- Message('Account No is invalid.  Select a valid Account No to continue.');
-- Message(' ', no_acknowledge);
If :Parameter.SHOW_MESSAGE = 'N' Then -- Do not display message this time.
    :Parameter.SHOW_MESSAGE := 'Y';     -- Set for next time to display message.
    Raise form_trigger_failure;
Else                                  -- Display message this time.
  Message('Account No is invalid.  Select a valid Account No to continue.');
  Message(' ', no_acknowledge);
    :Parameter.SHOW_MESSAGE := 'N';     -- Reset for next time.
    Raise form_trigger_failure;
  End if;
End if;
</multilinecomment>

journal_budget_check;
END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:85
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger JE_DATA.WHEN-VALIDATE-RECORD . See documentation for details.
        ///</remarks>
        /// <TriggerInfo>JE_DATA.WHEN-VALIDATE-RECORD</TriggerInfo>

        [RecordValidationTrigger]
        public virtual void jeData_WhenValidateRecord()
        {
            //  Formatted on 12/01/2014 7:25:36 AM (QP5 v5.163.1008.3004) 

            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            {
                //  08/11/17 psr alio-15391 Commented. This method was not acceptable.
                // msg_debug('je_data.wvr ' || :Parameter.SHOW_MESSAGE);
                // -- 06/22/17 psr alio-15391 Added If to Raise form_trigger_failure if Account No is invalid.
                // If :je_data.account_id = -1 then
                // -- 07/31/17 psr alio-15391 Added :Parameter.SHOW_MESSAGE and Moved Message() inside If
                // -- Message('Account No is invalid.  Select a valid Account No to continue.');
                // -- Message(' ', no_acknowledge);
                // If :Parameter.SHOW_MESSAGE = 'N' Then -- Do not display message this time.
                // :Parameter.SHOW_MESSAGE := 'Y';     -- Set for next time to display message.
                // Raise form_trigger_failure;
                // Else                                  -- Display message this time.
                // Message('Account No is invalid.  Select a valid Account No to continue.');
                // Message(' ', no_acknowledge);
                // :Parameter.SHOW_MESSAGE := 'N';     -- Reset for next time.
                // Raise form_trigger_failure;
                // End if;
                // End if;
                this.Task.Services.JournalBudgetCheck(jeDataElement);
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.WHEN-NEW-RECORD-INSTANCE
        /*
         --cec alio-3074 12/1/14 created trigger 
begin 
--msg_debug('je_data.wnri line:' || :je_data.line_no); 
if(:je_data.line_no is null) then 
:je_data.line_no := nvl(:je_header.next_line_no, 1); 
 :je_header.next_line_no := :je_data.line_no + 1; 

  SET_RECORD_PROPERTY (:SYSTEM.trigger_record, 
                       'je_data', 
                       status, 
                       new_status); 
end if; 
end; 


        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:86
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>JE_DATA.WHEN-NEW-RECORD-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-RECORD-INSTANCE", Function = KeyFunction.RECORD_CHANGE)]
        public virtual void jeData_recordChange()
        {
            // cec alio-3074 12/1/14 created trigger 

            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;

            // msg_debug('je_data.wnri line:' || :je_data.line_no); 
            if ((jeDataElement.LineNo.IsNull))
            {
                jeDataElement.LineNo = Lib.IsNull(jeHeaderElement.NextLineNo, 1);
                jeHeaderElement.NextLineNo = jeDataElement.LineNo + 1;
                BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "je_data", RecordStatus.NEW);
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.WHEN-REMOVE-RECORD
        /*
         <multilinecomment> Formatted on 12/01/2014 12:48:23 PM (QP5 v5.163.1008.3004) </multilinecomment>
DECLARE
v_dc_amount              NUMBER DEFAULT 0;
v_dc_flag                VARCHAR2 (1) DEFAULT NULL;
v_hold_dc_amount         NUMBER DEFAULT 0;          --cec alio-3074 12/1/14
v_hold_dc_flag           VARCHAR2 (1) DEFAULT NULL; --cec alio-3074 12/1/14
v_validate_amount        NUMBER DEFAULT 0;
v_hold_validate_amount   NUMBER DEFAULT 0;          --cec alio-3074 12/1/14
v_program                VARCHAR2 (30) DEFAULT 'FJENT01AFMX';
v_trace                  VARCHAR2 (10) DEFAULT NULL;
v_amount                 fas.je_data.journal_amount%TYPE;
BEGIN
<multilinecomment>msg_debug (
     'begin wrr... line no: '
  || :je_data.line_no
  || ', acct_id: '
  || :je_data.account_id
  || ', je amt: '
  || :je_data.journal_amount
  || ', dr_amt: '
  || :je_data.dr_amount
  || ', cr_amt: '
  || :je_data.cr_amount
  || ', d/c flag: '
  || :je_data.debit_credit_flag
  || ', hold acct: '
  || :je_data.hold_account_id
  || ', hold je amt: '
  || :je_data.hold_journal_amount
  || ', hold_dr_amt: '
  || :je_data.hold_dr_amount
  || ', hold_cr_amt: '
  || :je_data.hold_cr_amount);
</multilinecomment>
-- dph alio-15017 added nvl to all the amounts ( there were none)
IF (:je_data.account_id IS NOT NULL AND :je_data.line_no IS NOT NULL
 and :je_data.account_id <> -1)  -- 07/31/17 psr alio-15391 Added to avoid calling fjent01a_amt_validate_budget_ with -1 account_id
THEN
  IF nvl(:je_data.dr_amount,0) <> 0
  THEN
     v_dc_amount := :je_data.dr_amount;
     v_dc_flag := 'D';
  ELSIF nvl(:je_data.cr_amount,0) <> 0
  THEN
     v_dc_amount := :je_data.cr_amount;
     v_dc_flag := 'C';
  END IF;

  v_validate_amount :=
     fjent01a_amt_validate_budget_ (nvl(v_dc_amount,0),
                                    v_dc_flag,
                                    NULL,
                                    :je_data.account_id);
  --accounting_array.remove_line_no(:je_data.account_id, :je_data.line_no);
  accounting_array.update_accounting_tab (:je_data.account_id,
                                          nvl(v_validate_amount,0) * -1,
                                          :je_data.account_no,
                                          :je_data.line_no);
END IF;


IF (NVL (:je_data.hold_account_id, -1) <> NVL (:je_data.account_id, -1)
   AND :je_data.line_no IS NOT NULL)
THEN
  IF nvl(:je_data.hold_dr_amount,0) <> 0
  THEN
     v_dc_amount := :je_data.hold_dr_amount;
     v_dc_flag := 'D';
  ELSIF nvl(:je_data.hold_cr_amount,0) <> 0
  THEN
     v_dc_amount := :je_data.hold_cr_amount;
     v_dc_flag := 'C';
  END IF;

  v_validate_amount :=
     fjent01a_amt_validate_budget_ (nvl(v_dc_amount,0),
                                    v_dc_flag,
                                    NULL,
                                    :je_data.hold_account_id);

  accounting_array.update_accounting_tab (:je_data.hold_account_id,
                                          nvl(v_validate_amount,0) * -1,
                                          NULL,
                                          :je_data.line_no);
END IF;

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:87
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_DATA.WHEN-REMOVE-RECORD</TriggerInfo>

        [RowRemoved]
        public virtual void jeData_RowRemoved(RowAdapterEventArgs args)
        {
            //  Formatted on 12/01/2014 12:48:23 PM (QP5 v5.163.1008.3004) 
            JeDataAdapter jeDataElement = args.Row as JeDataAdapter;


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
                string vProgram = "FJENT01AFMX";
                string vTrace = null;
                NNumber vAmount = NNumber.Null;
                // msg_debug (
                // 'begin wrr... line no: '
                // || :je_data.line_no
                // || ', acct_id: '
                // || :je_data.account_id
                // || ', je amt: '
                // || :je_data.journal_amount
                // || ', dr_amt: '
                // || :je_data.dr_amount
                // || ', cr_amt: '
                // || :je_data.cr_amount
                // || ', d/c flag: '
                // || :je_data.debit_credit_flag
                // || ', hold acct: '
                // || :je_data.hold_account_id
                // || ', hold je amt: '
                // || :je_data.hold_journal_amount
                // || ', hold_dr_amt: '
                // || :je_data.hold_dr_amount
                // || ', hold_cr_amt: '
                // || :je_data.hold_cr_amount);
                //  dph alio-15017 added nvl to all the amounts ( there were none)
                if ((!jeDataElement.AccountId.IsNull && !jeDataElement.LineNo.IsNull && jeDataElement.AccountId != -(1)))
                {
                    if (Lib.IsNull(jeDataElement.DrAmount, 0) != 0)
                    {
                        vDcAmount = jeDataElement.DrAmount;
                        vDcFlag = "D";
                    }
                    else if (Lib.IsNull(jeDataElement.CrAmount, 0) != 0)
                    {
                        vDcAmount = jeDataElement.CrAmount;
                        vDcFlag = "C";
                    }

                    vValidateAmount = StoredProcedures.Fjent01aAmtValidateBudget_(Lib.IsNull(vDcAmount, 0), vDcFlag, null, jeDataElement.AccountId);

                    Task.Packages.AccountingArray.UpdateAccountingTab(jeDataElement.AccountId, Lib.IsNull(vValidateAmount, 0) * -(1), jeDataElement.AccountNo, jeDataElement.LineNo);
                }
                if ((Lib.IsNull(jeDataElement.HoldAccountId, -(1)) != Lib.IsNull(jeDataElement.AccountId, -(1)) && !jeDataElement.LineNo.IsNull))
                {
                    if (Lib.IsNull(jeDataElement.HoldDrAmount, 0) != 0)
                    {
                        vDcAmount = jeDataElement.HoldDrAmount;
                        vDcFlag = "D";
                    }
                    else if (Lib.IsNull(jeDataElement.HoldCrAmount, 0) != 0)
                    {
                        vDcAmount = jeDataElement.HoldCrAmount;
                        vDcFlag = "C";
                    }

                    vValidateAmount = StoredProcedures.Fjent01aAmtValidateBudget_(Lib.IsNull(vDcAmount, 0), vDcFlag, null, jeDataElement.HoldAccountId);

                    Task.Packages.AccountingArray.UpdateAccountingTab(jeDataElement.HoldAccountId, Lib.IsNull(vValidateAmount, 0) * -(1), null, jeDataElement.LineNo);


                }
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.PRE-QUERY
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
--v_default_where := 'where je_data.cr_amount > 0 and je_data.dr_amount > 0'; 
v_default_where := 'where je_data.journal_amount > 0'; 
SET_BLOCK_PROPERTY('je_data', DEFAULT_WHERE ,v_default_where); 
end if; 

end; 

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:88
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_DATA.PRE-QUERY</TriggerInfo>

        [BeforeQuery]
        public virtual void jeData_BeforeQuery(QueryEventArgs args)
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
                        // v_default_where := 'where je_data.cr_amount > 0 and je_data.dr_amount > 0'; 
                        vDefaultWhere = "where je_data.journal_amount > 0";
                        BlockServices.SetWhereClause("je_data", vDefaultWhere);
                    }
                }
                finally
                {
                    profileCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.POST-INSERT
        /*
         begin
build_approval_array(:je_data.reference_no);
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:89
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_DATA.POST-INSERT</TriggerInfo>

        [AfterRowInsert]
        public virtual void jeData_AfterRowInsert(RowAdapterEventArgs args)
        {
            JeDataAdapter jeDataElement = args.Row as JeDataAdapter;
            this.Task.Services.BuildApprovalArray(jeDataElement.ReferenceNo);
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.POST-UPDATE
        /*
         begin
build_approval_array(:je_data.reference_no);
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:90
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_DATA.POST-UPDATE</TriggerInfo>

        [AfterRowUpdate]
        public virtual void jeData_AfterRowUpdate(RowAdapterEventArgs args)
        {
            JeDataAdapter jeDataElement = args.Row as JeDataAdapter;
            this.Task.Services.BuildApprovalArray(jeDataElement.ReferenceNo);
        }


        #region Original PL/SQL code code for TRIGGER JE_DATA.POST-DELETE
        /*
         begin
build_approval_array(:je_data.reference_no);
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:91
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JE_DATA.POST-DELETE</TriggerInfo>

        [AfterRowDelete]
        public virtual void jeData_AfterRowDelete(RowAdapterEventArgs args)
        {
            JeDataAdapter jeDataElement = args.Row as JeDataAdapter;
            this.Task.Services.BuildApprovalArray(jeDataElement.ReferenceNo);
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.KEY-LISTVAL
        /*
         DECLARE 

batch_year     fas.batches.batch_year%type := :BATCH_NO_BLOCK.BATCH_YEAR;  -- nja alio 307 ;     -- Batch/Account Year 
bank_no        SHR.BANK_MASTER.BANK_NO%TYPE   := NULL;                           -- Bank Number OR NULL-- NJA ALIO 7410 ADDED THE %TYPE 
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
                               :je_data.account_no,      --:BLOCK_NAME.<Primary Account NO> 
                               :je_data.account_id,      --:BLOCK_NAME.<Primary Account ID> 
                               hold_null,                --:BLOCK_NAME.<Secondary Account NO> 
                               hold_null                 --:BLOCK_NAME.<Secondary Account ID> 
                               ); 

-- dph alio-15017 null out accounts that did't find an account_id when they use the lov!
if nvl(:je_data.account_id,-1) = -1 then
     Null;
     --:je_data.account_no := null;
else                               
  :je_data.account_desc := get_account_desc(:je_data.account_id); 
  :je_data.account_balance := get_account_balance; 
end if;                               
END;         
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:55
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_NO.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "ACCOUNT_NO", Function = KeyFunction.LIST_VALUES)]
        public virtual void accountNo_ListValues()
        {
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            {
                NNumber batchYear = Model.BatchNoBlock.BatchYear;
                //  nja alio 307 ;     -- Batch/Account Year 
                string bankNo = string.Empty;
                //  Bank Number OR NULL-- NJA ALIO 7410 ADDED THE %TYPE 
                string transDesc = "AJE";
                //  TRANSACTION_DESCRIPTION 
                NNumber debitCredit = -1;
                //  ( +1, 0, -1) 
                string sendStatus = "1";
                //  (1)Primary Acct, (2)Secondary Acct, (3)Both 
                string callFrom = "L";
                //  (L)OV, (T)rigger, (V)alidate 
                string holdNull1 = string.Empty;
                NNumber holdNull2 = NNumber.Null;
                String acctNo1_ref = jeDataElement.AccountNo;
                NNumber acctId1_ref = jeDataElement.AccountId;

                Task.Packages.ValidateAccount.LoadValues(batchYear, ref bankNo, transDesc, debitCredit, sendStatus, callFrom, ref acctNo1_ref, ref acctId1_ref, ref holdNull1, ref holdNull2);

                jeDataElement.AccountNo = acctNo1_ref;
                jeDataElement.AccountId = acctId1_ref;
                //  dph alio-15017 null out accounts that did't find an account_id when they use the lov!
                if (Lib.IsNull(jeDataElement.AccountId, -(1)) == -(1))
                {
                }
                else
                {
                    jeDataElement.AccountDesc = this.Task.Services.FGetAccountDesc(jeDataElement.AccountId);
                    jeDataElement.AccountBalance = this.Task.Services.FGetAccountBalance(jeDataElement);
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.WHEN-VALIDATE-ITEM
        /*
         DECLARE 

batch_year     fas.batches.batch_year%type := :BATCH_NO_BLOCK.BATCH_YEAR;  -- nja alio 307 ;     -- Batch/Account Year 
bank_no        SHR.BANK_MASTER.BANK_NO%TYPE  := NULL;                           -- Bank Number OR NULL-- NJA ALIO 7410 ADDED THE %TYPE 
trans_desc     varchar2(8)  := 'AJE';                          -- TRANSACTION_DESCRIPTION 
debit_credit   number(2)    := '-1';                           -- ( +1, 0, -1) 
send_status    varchar2(1)  := '1';                            -- (1)Primary Acct, (2)Secondary Acct, (3)Both 
call_from      varchar2(1)  := 'V';                            -- (L)OV, (T)rigger, (V)alidate 

hold_null      varchar2(1);                                    -- Used for Return of only one account 

BEGIN 

if :system.mode = 'NORMAL' and 
  :je_data.account_no is not null then 
  validate_account.load_values(batch_year, 
                               bank_no,              
                               trans_desc, 
                               debit_credit, 
                               send_status, 
                               call_from, 
                               :je_data.account_no,      --:BLOCK_NAME.<Primary Account NO> 
                               :je_data.account_id,      --:BLOCK_NAME.<Primary Account ID> 
                               hold_null,                --:BLOCK_NAME.<Secondary Account NO> 
                               hold_null                 --:BLOCK_NAME.<Secondary Account ID> 
                               ); 
end if; 

    -- dph alio-15017 null out accounts that did't find an account_id when they use the lov!
  if nvl(:je_data.account_id,-1) = -1 then
     Null;
     --:je_data.account_no := null;
else

:je_data.account_desc := get_account_desc(:je_data.account_id); 
:je_data.account_balance := get_account_balance; 

end if;

END;         
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:56
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_NO.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "ACCOUNT_NO", FireInSearch = false)]
        public virtual void accountNo_validate()
        {
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            {
                NNumber batchYear = Model.BatchNoBlock.BatchYear;
                //  nja alio 307 ;     -- Batch/Account Year 
                string bankNo = string.Empty;
                //  Bank Number OR NULL-- NJA ALIO 7410 ADDED THE %TYPE 
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
                if (TaskServices.Mode == "NORMAL" && !string.IsNullOrEmpty(jeDataElement.AccountNo))
                {
                    String acctNo1_ref = jeDataElement.AccountNo;
                    NNumber acctId1_ref = jeDataElement.AccountId;

                    Task.Packages.ValidateAccount.LoadValues(batchYear, ref bankNo, transDesc, debitCredit, sendStatus, callFrom, ref acctNo1_ref, ref acctId1_ref, ref holdNull1, ref holdNull2);

                    jeDataElement.AccountNo = acctNo1_ref;
                    jeDataElement.AccountId = acctId1_ref;
                    if (TaskServices.RecordStatus == RecordStatus.NEW.ToString() && TaskServices.BlockStatus == RecordStatus.QUERY.ToString())
                        BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "je_data", RecordStatus.INSERT);
                }
                //  dph alio-15017 null out accounts that did't find an account_id when they use the lov!
                if (Lib.IsNull(jeDataElement.AccountId, -(1)) == -(1))
                {
                }
                else
                {
                    jeDataElement.AccountDesc = Task.Services.FGetAccountDesc(jeDataElement.AccountId);
                    jeDataElement.AccountBalance = Task.Services.FGetAccountBalance(jeDataElement);

                    if (TaskServices.RecordStatus == RecordStatus.NEW.ToString() && TaskServices.BlockStatus == RecordStatus.QUERY.ToString())
                        BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "je_data", RecordStatus.INSERT);
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.POST-TEXT-ITEM
        /*
         --cec alio-3074 - only update the account desc if it has changed, otherwise, the record will be revalidated each time the user touches account no 
if(nvl(:parameter.hold_acct_id, -1) <> nvl(:je_data.account_id, -1)) then 
:je_data.account_desc := get_account_desc(:je_data.account_id); 
:je_data.account_balance := get_account_balance; 
end if; 
:parameter.hold_acct_id := null;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:57
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_NO.POST-TEXT-ITEM</TriggerInfo>

        [ActionTrigger(Action = "POST-TEXT-ITEM", Item = "ACCOUNT_NO", Function = KeyFunction.ITEM_OUT)]
        public virtual void accountNo_itemOut()
        {
            // cec alio-3074 - only update the account desc if it has changed, otherwise, the record will be revalidated each time the user touches account no 

            JeDataAdapter jeDataElement = Model.JeData.CurrentRowAdapter;


            // cec alio-3074 - only update the account desc if it has changed, otherwise, the record will be revalidated each time the user touches account no 
            if ((Lib.IsNull(Model.Params.HoldAcctId, (-1).ToString()) != Lib.IsNull(jeDataElement.AccountId.ToString(), (-1).ToString())))
            {
                jeDataElement.AccountDesc = Task.Services.FGetAccountDesc(jeDataElement.AccountId);
                jeDataElement.AccountBalance = Task.Services.FGetAccountBalance(jeDataElement);
            }
            Model.Params.HoldAcctId = null;
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.PRE-TEXT-ITEM
        /*
         --cec alio-3074 
:parameter.hold_acct_id := :je_data.account_id;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:58
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_NO.PRE-TEXT-ITEM</TriggerInfo>

        [ActionTrigger(Action = "PRE-TEXT-ITEM", Item = "ACCOUNT_NO", Function = KeyFunction.ITEM_IN)]
        public virtual void accountNo_itemIn()
        {
            // cec alio-3074 
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            // cec alio-3074 
            this.Model.Params.HoldAcctId = jeDataElement.AccountId.ToString();
        }


        #region Original PL/SQL code code for TRIGGER SHOW_LOV.WHEN-BUTTON-PRESSED
        /*
         BEGIN 
go_item('je_data.account_no'); 
do_key('list_values'); 
END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:60
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>SHOW_LOV.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVALUE", Item = "ACCOUNT_NO")]
        public virtual void showLov_buttonClick()
        {
            ItemServices.GoItem("je_data.account_no");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER DR_AMOUNT.POST-CHANGE
        /*
         BEGIN 
if :je_data.dr_amount <> 0 then 
  :je_data.journal_amount := :je_data.dr_amount; 
  :je_data.debit_credit_flag := 1; 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:62
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger DR_AMOUNT.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="jeDataElement"></param>
        /// <TriggerInfo>DR_AMOUNT.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "DR_AMOUNT")]
        public virtual void drAmount_PostChange(JeDataAdapter jeDataElement)
        {
            if (jeDataElement.DrAmount != 0)
            {
                jeDataElement.JournalAmount = jeDataElement.DrAmount;
                jeDataElement.DebitCreditFlag = 1;
            }
        }


        #region Original PL/SQL code code for TRIGGER CR_AMOUNT.POST-CHANGE
        /*
         BEGIN 

if :je_data.cr_amount <> 0 then 
  :je_data.journal_amount := :je_data.cr_amount; 
  :je_data.debit_credit_flag := -1;--cec alio-3074 changed from 1 to -1 to be consistent with pre-insert, pre-update triggers 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:64
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger CR_AMOUNT.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="jeDataElement"></param>
        /// <TriggerInfo>CR_AMOUNT.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "CR_AMOUNT")]
        public virtual void crAmount_PostChange(JeDataAdapter jeDataElement)
        {
            if (jeDataElement.CrAmount != 0)
            {
                jeDataElement.JournalAmount = jeDataElement.CrAmount;
                jeDataElement.DebitCreditFlag = -(1);
            }
        }


        #region Original PL/SQL code code for TRIGGER HOLD_DR_AMOUNT.POST-CHANGE
        /*
         BEGIN 

if :je_data.hold_dr_amount <> 0 then 
  :je_data.hold_journal_amount := :je_data.hold_dr_amount; 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:76
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger HOLD_DR_AMOUNT.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="jeDataElement"></param>
        /// <TriggerInfo>HOLD_DR_AMOUNT.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "HOLD_DR_AMOUNT")]
        public virtual void holdDrAmount_PostChange(JeDataAdapter jeDataElement)
        {
            if (jeDataElement.HoldDrAmount != 0)
            {
                jeDataElement.HoldJournalAmount = jeDataElement.HoldDrAmount;
            }
        }


        #region Original PL/SQL code code for TRIGGER HOLD_CR_AMOUNT.POST-CHANGE
        /*
         BEGIN 

if :je_data.hold_cr_amount <> 0 then 
  :je_data.hold_journal_amount := :je_data.hold_cr_amount; 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:78
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger HOLD_CR_AMOUNT.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="jeDataElement"></param>
        /// <TriggerInfo>HOLD_CR_AMOUNT.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "HOLD_CR_AMOUNT")]
        public virtual void holdCrAmount_PostChange(JeDataAdapter jeDataElement)
        {
            if (jeDataElement.HoldCrAmount != 0)
            {
                jeDataElement.HoldJournalAmount = jeDataElement.HoldCrAmount;
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_BALANCE.POST-CHANGE
        /*
         BEGIN 
if :je_data.dr_amount <> 0 then 
  :je_data.journal_amount := :je_data.dr_amount; 
  :je_data.debit_credit_flag := 1; 
end if;     

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:80
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger ACCOUNT_BALANCE.POST-CHANGE . See documentation for details.
        ///</remarks>
        /// <param name="jeDataElement"></param>
        /// <TriggerInfo>ACCOUNT_BALANCE.POST-CHANGE</TriggerInfo>

        [ActionTrigger(Action = "POST-CHANGE", Item = "ACCOUNT_BALANCE")]
        public virtual void accountBalance_PostChange(JeDataAdapter jeDataElement)
        {
            if (jeDataElement.DrAmount != 0)
            {
                jeDataElement.JournalAmount = jeDataElement.DrAmount;
                jeDataElement.DebitCreditFlag = 1;
            }
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
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            this.drAmount_PostChange(jeDataElement);
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
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            this.crAmount_PostChange(jeDataElement);
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
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            this.holdDrAmount_PostChange(jeDataElement);
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
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            this.holdCrAmount_PostChange(jeDataElement);
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
            JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
            this.accountBalance_PostChange(jeDataElement);
        }



        #endregion

    }
}
