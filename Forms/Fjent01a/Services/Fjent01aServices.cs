using Alio.Common;
using Alio.Common.DbServices;
using Alio.Forms.Common.DbServices;
using Alio.Forms.Fjent01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Exceptions;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fjent01a.Services
{
    public class Fjent01aServices : AbstractServices<Fjent01aModel>
    {

        public Fjent01aServices(Fjent01aModel model) : base(model)
        {
        }

        public new Fjent01aTask Task
        {
            get { return (Fjent01aTask)base.Task; }
        }

        #region Original PL/SQL code for Prog Unit CHECK_PACKAGE_FAILURE
        /*
		Procedure Check_Package_Failure IS 
BEGIN 
  IF NOT ( Form_Success ) THEN 
    RAISE Form_Trigger_Failure; 
  END IF; 
END;
		*/
        #endregion
        //ID:327
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void CheckPackageFailure()
        {
            if (!((TaskServices.ServiceSuccess)))
            {
                throw new ApplicationException();
            }
        }



        #region Original PL/SQL code for Prog Unit CLEAR_TOTALS
        /*
		--THIS PROCEDURE MUST BE IN ON_CLEAR_DETAILS 
 
   PROCEDURE clear_totals IS 
   BEGIN 
      null; 
--    :totals.dr_total := null; 
--    :totals.cr_total := null; 
   END; 

		*/
        #endregion
        //ID:330
        /// <summary></summary>
        /// <remarks>
        /// THIS PROCEDURE MUST BE IN ON_CLEAR_DETAILS 
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void ClearTotals()
        {
        }


        #region Original PL/SQL code for Prog Unit DATE_CHOOSEN
        /*
		PROCEDURE date_choosen IS 
BEGIN 
   copy(to_char(date_lov.current_lov_date,'dd-mon-yyyy'), date_lov.date_lov_return_item); 
   go_item(date_lov.date_lov_return_item); 
   if date_lov.lov_auto_skip = TRUE then 
      next_item; 
   end if; 
END;
		*/
        #endregion
        //ID:331
        /// <summary></summary>
        /// <remarks>
        /// F2N_STRONG_PRESENTATION_LOGIC : The code of this procedure was identified as containing presentation logic. See documentation for details.
        /// </remarks>
        public void DateChoosen()
        {
            //TaskServices.Copy(Lib.ToChar(Task.Libraries.Calendar.DateLov.currentLovDate, "dd-mon-yyyy"),Task.Libraries.Calendar.DateLov.dateLovReturnItem);
            //ItemServices.GoItem(Task.Libraries.Calendar.DateLov.dateLovReturnItem);
            //if ( Task.Libraries.Calendar.DateLov.lovAutoSkip == true )
            //{
            //	ItemServices.NextItem();
            //}
        }


        #region Original PL/SQL code for Prog Unit MESSAGE_DISPLAY
        /*
		-- ----------------------------------------------------------------------------- 
--  Procedure: MESSAGE_DISPLAY 
--  Purpose:   Used to display message using forms MESSAGE built in. 
--             Makes sure message does not exceed maximum length allowed by builtin. 
--             Reduces program lines in main program logic. 
-- ----------------------------------------------------------------------------- 
PROCEDURE MESSAGE_DISPLAY (p_message in varchar2) IS 
BEGIN 
    message(substr(p_message,1,200)); 
    message(' ',no_acknowledge); 
END;
		*/
        #endregion
        //ID:332
        /// <summary></summary>
        /// <param name="pMessage"></param>
        /// <remarks>
        ///  ----------------------------------------------------------------------------- 
        ///   Procedure: MESSAGE_DISPLAY 
        ///   Purpose:   Used to display message using forms MESSAGE built in. 
        ///              Makes sure message does not exceed maximum length allowed by builtin. 
        ///              Reduces program lines in main program logic. 
        ///  ----------------------------------------------------------------------------- 
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void MessageDisplay(string pMessage)
        {
            TaskServices.Message(Lib.Substring(pMessage, 1, 200));
            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
        }


        #region Original PL/SQL code for Prog Unit MSG_DEBUG
        /*
		-- ----------------------------------------------------------------------------- 
--  Procedure: MSG_DEBUG 
--  Purpose:   Display message if alio system debug flag is 'ON' 
-- ----------------------------------------------------------------------------- 
PROCEDURE MSG_DEBUG (p_message varchar2) IS 
BEGIN 
   if :global.debug_status = 'ON' then 
     message(substr(p_message,1,200));  
     message(' ',no_acknowledge); 
   end if;  
END; 
 

		*/
        #endregion
        //ID:333
        /// <summary></summary>
        /// <param name="pMessage"></param>
        /// <remarks>
        ///  ----------------------------------------------------------------------------- 
        ///   Procedure: MSG_DEBUG 
        ///   Purpose:   Display message if alio system debug flag is 'ON' 
        ///  ----------------------------------------------------------------------------- 
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void MsgDebug(string pMessage)
        {
            if (Alio.Common.Globals.DebugStatus == "ON")
            {
                TaskServices.Message(Lib.Substring(pMessage, 1, 200));
                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
            }
        }


        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_NO
        /*
		FUNCTION get_account_no (account_id_in number) RETURN varchar2 IS 
 
 cursor get_account_no(cur_in_acct_id number) is 
   select account_no 
     from shr.accounts 
    where account_id = cur_in_acct_id; 
      
 hold_account_no shr.accounts.account_no%type;  -- nja alio 307 
      
begin 
 
 open get_account_no(account_id_in); 
 fetch get_account_no into hold_account_no; 
 close get_account_no; 
 
 return hold_account_no; 
 
end; 

		*/
        #endregion
        //ID:335
        /// <summary></summary>
        /// <param name="getAccountNo"></param>
        /// <remarks>
        /// </remarks>
        public virtual string FGetAccountNo(NNumber accountIdIn)
        {
            int rowCount = 0;
            String sqlgetAccountNo = "SELECT account_no " +
    " FROM shr.accounts " +
    " WHERE account_id = @P_CUR_IN_ACCT_ID ";
            DataCursor getAccountNo = new DataCursor(sqlgetAccountNo);
            NNumber getAccountNoCurInAcctId = NNumber.Null;
            NString holdAccountNo = NString.Null;
            #region
            try
            {
                getAccountNoCurInAcctId = accountIdIn;
                //Setting query parameters
                getAccountNo.AddParameter("@P_CUR_IN_ACCT_ID", getAccountNoCurInAcctId);
                getAccountNo.Open();
                ResultSet getAccountNoResults = getAccountNo.FetchInto();
                if (getAccountNoResults != null)
                {
                    holdAccountNo = getAccountNoResults.GetStr(0);
                }
                return holdAccountNo;
            }
            finally
            {
                getAccountNo.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_DESC
        /*
		FUNCTION get_account_desc (account_id_in number) RETURN varchar2 IS 
 
 cursor get_account_desc (cur_in_acct_id number) is 
   select account_desc 
     from shr.accounts 
    where account_id = cur_in_acct_id; 
      
 hold_account_desc varchar2(50); 
      
begin 
 
 open get_account_desc (account_id_in); 
 fetch get_account_desc into hold_account_desc; 
 close get_account_desc; 
 
 return hold_account_desc; 
 
end; 
 

		*/
        #endregion
        //ID:336
        /// <summary></summary>
        /// <param name="getAccountDesc"></param>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual string FGetAccountDesc(NNumber accountIdIn)
        {
            int rowCount = 0;
            String sqlgetAccountDesc = "SELECT account_desc " +
    " FROM shr.accounts " +
    " WHERE account_id = @P_CUR_IN_ACCT_ID ";
            DataCursor getAccountDesc = new DataCursor(sqlgetAccountDesc);
            NNumber getAccountDescCurInAcctId = NNumber.Null;
            string holdAccountDesc = string.Empty;
            #region
            try
            {
                getAccountDescCurInAcctId = accountIdIn;
                //Setting query parameters
                getAccountDesc.AddParameter("@P_CUR_IN_ACCT_ID", getAccountDescCurInAcctId);
                getAccountDesc.Open();
                ResultSet getAccountDescResults = getAccountDesc.FetchInto();
                if (getAccountDescResults != null)
                {
                    holdAccountDesc = getAccountDescResults.GetString(0);
                }
                return holdAccountDesc;
            }
            finally
            {
                getAccountDesc.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit POPULATE_BATCH_TOTALS
        /*
		PROCEDURE populate_batch_totals IS 
 
BEGIN 
 
  select sum(journal_amount) 
    into :je_header.debit_total 
    from fas.je_header, 
         fas.je_data 
   where je_header.batch_no   = :je_header.batch_no 
     and je_header.batch_year = :je_header.batch_year 
     and je_data.reference_no = je_header.reference_no 
     and je_data.debit_credit_flag = 1; --cec alio-3074 changed from '1' to just 1 
     
  select sum(journal_amount) 
    into :je_header.credit_total 
    from fas.je_header, 
         fas.je_data 
   where je_header.batch_no   = :je_header.batch_no 
     and je_header.batch_year = :je_header.batch_year 
     and je_data.reference_no = je_header.reference_no 
     and je_data.debit_credit_flag = -1;--cec alio-3074 changed from '-1' to just -1 
  
END;
		*/
        #endregion
        //ID:337
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void PopulateBatchTotals(JeHeaderAdapter jeHeaderElement)
        {
            int rowCount = 0;
            #region Execute data command
            String sql1 = "SELECT sum(journal_amount) " +
    " FROM fas.je_header, fas.je_data " +
    " WHERE je_header.batch_no = @JE_HEADER_BATCH_NO AND je_header.batch_year = @JE_HEADER_BATCH_YEAR AND je_data.reference_no = je_header.reference_no AND je_data.debit_credit_flag = 1 ";
            DataCommand command1 = new DataCommand(sql1);
            //Setting query parameters
            command1.AddParameter("@JE_HEADER_BATCH_NO", jeHeaderElement.BatchNo);
            command1.AddParameter("@JE_HEADER_BATCH_YEAR", jeHeaderElement.BatchYear);
            ResultSet results1 = command1.ExecuteQuery();
            rowCount = command1.RowCount;
            if (results1.HasData)
            {
                jeHeaderElement.DebitTotal = results1.GetNumber(0);
            }
            results1.Close();
            #endregion
            // cec alio-3074 changed from '1' to just 1 
            #region Execute data command
            String sql2 = "SELECT sum(journal_amount) " +
    " FROM fas.je_header, fas.je_data " +
    " WHERE je_header.batch_no = @JE_HEADER_BATCH_NO AND je_header.batch_year = @JE_HEADER_BATCH_YEAR AND je_data.reference_no = je_header.reference_no AND je_data.debit_credit_flag = - 1 ";
            DataCommand command2 = new DataCommand(sql2);
            //Setting query parameters
            command2.AddParameter("@JE_HEADER_BATCH_NO", jeHeaderElement.BatchNo);
            command2.AddParameter("@JE_HEADER_BATCH_YEAR", jeHeaderElement.BatchYear);
            ResultSet results2 = command2.ExecuteQuery();
            rowCount = command2.RowCount;
            if (results2.HasData)
            {
                jeHeaderElement.CreditTotal = results2.GetNumber(0);
            }
            results2.Close();
            #endregion
        }


        #region Original PL/SQL code for Prog Unit CHANGE_HISTORY
        /*
		PROCEDURE CHANGE_HISTORY IS 
 
<multilinecomment> 
-- 05/21/12         NJA  Alio-307 Checked and if necessary changed the variable sizes/types of batch_no, batch_year, account_id, account_no, po_no, reference_no, vendor_no, check_key, check_no 
-- 10/23/12         NJA   ALIO-7410 recommitting due to a missed commit in 6i 
 cec  12/14/12  alio-7444  -re-inherited validate_account from idi_lib to recognize trans driver validation correction 
 NJA   02/21/14    Alio-10283  - Expanded teh size of line_no from 6 to 7 
 klb   09/04/14    alio-3074   - Validate journals against budget based on JOURNAL_BUDGET_CHECK profile 
                               - WNFI retrieve above profiles.     
                               -  Subclass IDI-Lib Component - BUDGET_OVERRIDE (includes program unit - VALIDATE_BUDGET 
                                  and alerts including over_budget_no_override. VALIDATE_BUDGET determines when amount should be validated 
                                  and displays appropriate Insufficent Funds/Override message in the alert displayed. 
                               - Added: parameter.journal_budget_check and program unit journal_budget_check. 
                               - journal_budget_check calls new function fjent01a_debit_credit_amt_ (reverse amount when needed) 
                                       and library program unit validate_budget.  
                               - Modified je_data triggers: pre insert, pre-update to call.journal_budget_check 
  klb   09/05/14    ALIO-3074   - Changed database function name to FJENT01A_AMT_VALIDATE_BUDGET instead of FJENT01A_CREDIT_DEBIT_AMT.                              
 cec  12/03/14  alio-3074  -changed balance check functionality to check the balance in the wvr trigger instead of pre-insert and pre-update triggers 
                and to use the array balance checking instead of just the validate budget program unit so that a running total 
                is kept for the account balance. 
                -changed dr and cr amount fields to not allow negative values 
 cec  12/12/14        -reinherit accounting_array 
    12/29/14        -changed journal_budget_check to decrease old account amount but flag it as validated 
    12/31/14        -changed journal_budget_check to validate full amount when account id is changed 
    01/28/15        -changed journal_budget_check to validate liability accounts for debits instead of credits 
    02/03/15        -reinherit idi_lib budget_override 
    02/23/15        -reinherit idi_lib budget_override 
 wjk  03/23/15  alio-12166 15.4  added current account balance 
 NJA   04/13/15    alio-12797  15.4    added the pre_query trigger to je_data 
 wjk  05/15/15  alio-12166 15.4  added pre_payable to account balance calculation 
 NJA   05/28/15    alio-12797  15.4    corrected the default where clause 
       06/01/15                        -Added the pre_insert/pre_update validation to DR_AMOUNT and CR_AMOUNT if the profile is set to disallow negatives 
 DPH   06/03/15    alio-13043  16.1   set account balance for type 77 (WIP) 
 DPH   08/03/15    alio-3109   16.1   add code in post insert and post update triggers to build an array of references that need approval
                                      in the post form trigger - loop through the array and call the approvals
       09/02/15                       fixed view go item  
DPH    12/31/15    alio-13043         call common plsql funciton fabal01a_avail_balance to get balance amounts
DPH    01/19/17    alio-14278 17.3    Add display_reference_no and overlay reference_no conditionally show order_no if processing by order                                         
DPH    02/13/17    alio-15017 17.4    in the account_no key list val and wvi trigger reset account no if account_id is not valid 
                                      also changed this forms account_lookup program unit to stop them if they pick an invalid account 
                                      if this works it should be applied to the library
DPH    03/08/17    alio-15017 17.4    Added nvls around the amounts in the je_data when-remove-record trigger
psr    06/01/17    alio-15301 18.1    Added assignment in je_header.display_reference_no WVI trigger :je_header.reference_no := :je_header.display_reference_no to provide FAS.JE_HEADER PK
                                      Required when reference_no is hand keyed.
psr    06/12/17    alio-15315 18.1    Removed display_reference_no from layout. No longer using display_reference_no and therefore alio-15301 change.
                                      In WNFI using USE_WHS_ORDER_NO_FOR_JOURNAL_REF profile to control use of whs_order_no or Reference no assignment button and field.
                                      Commented display_reference_no code in triggers Pre-Query and Post-Query je_header block, WBP je_header.assign_reference and WVI je_header.display_reference_no
psr    06/13/17    alio-15315 18.1    Changed WNFI using USE_WHS_ORDER_NO_FOR_JOURNAL_REF profile to control use of WHS_ORDER_NO only.
                                      Reference no assignment button and field will always be visible and enabled.
psr    06/22/17    alio-15391 18.1    In JE_DATA WVR added If to Raise form_trigger_failure if Account No is invalid.
                                      Commented Nulling of Account No in validate_account.
psr    07/31/17    alio-15391 18.1    Added :Parameter.SHOW_MESSAGE to avoid message showing multiple times in JE_DATA wvr, Form Key-F2 and Controls.Button_03 wbp triggers.
                                      Added in JE_DATA wrr :je_data.account_id <> -1 condition to bypass error when calling fjent01a_amt_validate_budget_ with -1 account_id
psr    08/11/17    alio-15391 18.1    Commented :Parameter.SHOW_MESSAGE related code.  This method was not acceptable.
mfl    07/17/18    alio-14824 19.1    Added checkbox to form - Ready for Approval.  If the profile_key JOURNAL_APPROVAL is not set to Y, then this new field will not show.
                                      If the profile_key is set to Y, then if the Ready for Approval box is checked, the release_flag in je_header will be set to Y, otherwise will
                                      be set to N.
mfl    08/02/18    alio-14824 19.1    In post_query trigger, if the approval_status is EN or RQ but the release flag is not Y, then display the status
                                      of Pending Release.
mfl    10/12/18    alio-14830 19.2    Added approval chain LOV to form to allow user to specify an approval chain. 
mfl    01/17/19    alio-16811 19.4    Added a dummy navigable field in the BATCH_NO_BLOCK block in order to eliminate the 'No navigable items in destination block' error.
mfl    02/08/19    alio-16811 19.4    Corrected tabbing order of fields on form.  Tabbing order should be:  ref#, date, desc, account no, amounts.
mfl    05/10/19    alio-16811 19.4    Fixed navigation error where if you are in the reference no field and do shift/tab, the calendar LOV would come up
                                      and then you could not exist out of it.
dph    12/23/19    alio-17304 20.3		Add ability to export to spreadsheet.                                      
</multilinecomment>
BEGIN 
  null; 
END;
		*/
        #endregion
        //ID:338
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void ChangeHistory()
        {
        }


        #region Original PL/SQL code for Prog Unit JOURNAL_BUDGET_CHECK
        /*
		<multilinecomment> Formatted on 12/08/2014 7:20:50 AM (QP5 v5.163.1008.3004) </multilinecomment> 
-- alio-3074 09/04/14  klb added. 
-- Called from je_data pre-insert, pre-update 
-- If profile set, 
--   Amount validated must have correct sign based on account type and debit or credit (uses fjent01a_debit_credit_amt) 
--   Uses alio IDI_LIB.OLB Accounting_Array to alert user if there is insufficient funds 
 
PROCEDURE journal_budget_check 
IS 
   CURSOR acct_type_cur (cur_in_account_id NUMBER) 
   IS 
      SELECT ac.account_type 
        FROM shr.accounts ac 
       WHERE ac.account_id = cur_in_account_id; 
 
   v_acct_type              shr.accounts.account_type%TYPE; 
 
   validate_balance         BOOLEAN := TRUE; 
   v_dc_amount              NUMBER DEFAULT 0; 
   v_dc_flag                VARCHAR2 (1) DEFAULT NULL; 
   v_hold_dc_amount         NUMBER DEFAULT 0;          --cec alio-3074 12/1/14 
   v_hold_dc_flag           VARCHAR2 (1) DEFAULT NULL; --cec alio-3074 12/1/14 
   v_validate_amount        NUMBER DEFAULT 0; 
   v_hold_validate_amount   NUMBER DEFAULT 0;          --cec alio-3074 12/1/14 
   v_amount                 fas.je_data.journal_amount%TYPE; --cec alio-3074 12/1/14 
   v_program                VARCHAR2 (30) DEFAULT 'FJENT01AFMX'; 
   v_trace                  VARCHAR2 (10) DEFAULT NULL; 
BEGIN 
   v_trace := 'JBC-START'; 
 
   IF :je_data.dr_amount <> 0 
   THEN 
      v_dc_amount := :je_data.dr_amount; 
      v_dc_flag := 'D'; 
   ELSIF :je_data.cr_amount <> 0 
   THEN 
      v_dc_amount := :je_data.cr_amount; 
      v_dc_flag := 'C'; 
   END IF; 
 
   --cec alio-3074 12/1/14 
   IF :je_data.hold_dr_amount <> 0 
   THEN 
      v_hold_dc_amount := :je_data.hold_dr_amount; 
      v_hold_dc_flag := 'D'; 
   ELSIF :je_data.hold_cr_amount <> 0 
   THEN 
      v_hold_dc_amount := :je_data.hold_cr_amount; 
      v_hold_dc_flag := 'C'; 
   END IF; 
 
   msg_debug ( 
         'JBC trace: ' 
      || v_trace 
      || ', v_dc_amount: ' 
      || v_dc_amount 
      || ', v_dc_flag:' 
      || v_dc_flag 
      || ', acct no: ' 
      || :je_data.account_no 
      || ', acct_id: ' 
      || :je_data.account_id 
      || ', je amt: ' 
      || :je_data.journal_amount 
      || ', hold acct_id: ' 
      || :je_data.hold_account_id 
      || ', hold je amt: ' 
      || :je_data.hold_journal_amount); 
 
   v_trace := 'JBC2'; 
 
   IF v_dc_amount <> 0 
   THEN 
      v_trace := 'JBC3'; 
 
      IF :parameter.journal_budget_check IS NULL 
      THEN 
         :parameter.journal_budget_check := 
            UPPER ( 
               NVL (TRIM (adm.get_profile_data ('JOURNAL_BUDGET_CHECK')), 
                    'N')); 
      END IF; 
 
      <multilinecomment>msg_debug ( 
            'JBC after profile journal_budget_check trace: ' 
         || v_trace 
         || ' profile journal budget: ' 
         || :parameter.journal_budget_check); 
         </multilinecomment> 
      v_trace := 'JBC4'; 
 
      IF UPPER (NVL (TRIM (:parameter.journal_budget_check), 'N')) = 'YES' 
      THEN 
         -- Determine amount to validate (reverse sign depending on account type and dc flag) 
         v_trace := 'JBC5'; 
         <multilinecomment>msg_debug ( 
               'JBC get validate amount trace: ' 
            || v_trace 
            || ', account_no: ' 
            || :je_data.account_no 
            || ', account id:' 
            || :je_data.account_id 
            || ', v_dc_amount: ' 
            || v_dc_amount);</multilinecomment> 
 
         v_validate_amount := 
            fjent01a_amt_validate_budget_ (v_dc_amount, 
                                           v_dc_flag, 
                                           NULL, -- account type (looked up using account id passed in) 
                                           :je_data.account_id); 
         msg_debug ( 
               'JBC after fjent01a_amt_validate_budget trace: ' 
            || v_trace 
            || ', v_validate_amount: ' 
            || v_validate_amount); 
 
         --cec alio-3074 12/1/14 
         v_trace := 'JBC5b'; 
 
         <multilinecomment>msg_debug ( 
               'JBC5b get validate amount trace: ' 
            || v_trace 
            || ', hold acct id:' 
            || :je_data.hold_account_id 
            || ', v_hold_dc_amount: ' 
            || v_hold_dc_amount);</multilinecomment> 
 
         IF (NVL (:je_data.hold_account_id, 0) = 0) 
         THEN 
            v_hold_validate_amount := 0; 
         ELSE 
            v_hold_validate_amount := 
               fjent01a_amt_validate_budget_ (v_hold_dc_amount, 
                                              v_hold_dc_flag, 
                                              NULL, 
                                              :je_data.hold_account_id); 
         END IF; 
 
 
         msg_debug ( 
               'JBC5b after fjent01a_amt_validate_budget trace: ' 
            || v_trace 
            || ', v_hold_validate_amount: ' 
            || v_hold_validate_amount); 
 
         -- Use alio IDI_LIB Validate_budget to determine if amount should be validated and user alerted of insufficient funds 
         v_trace := 'JBC6'; 
 
         <multilinecomment>--cec alio-3074 commented out and changed to using accounting_array logic 
         if nvl(v_validate_amount,0) != 0 then 
              v_trace := 'JBC7'; 
                -- Call IDI_LIB validate_budget program unit (subclassed) 
                 validate_budget( :je_data.account_id 
                                  ,v_validate_amount 
                                  ,:je_data.account_no 
                                  ,v_program); 
         msg_debug('JBC after validate budget trace: '||v_trace||' v_validate_amount: '|| v_validate_amount); 
 
         end if; 
         </multilinecomment> 
         --------cec alio-3074 12/1/14 begin accounting_array logic:---------- 
         IF (:je_data.account_id IS NOT NULL) 
         THEN 
            --if the account has changed, then reduce the old account amount 
            IF (:je_data.account_id <> :je_data.hold_account_id 
                AND :je_data.hold_account_id IS NOT NULL) 
            THEN 
               msg_debug ( 
                  'jed.wvr - acct changed, calling update accounting tab to reduce old acct id'); 
 
               IF (accounting_array.acct_id_line_exists ( 
                      :je_data.hold_account_id, 
                      :je_data.line_no)) 
               THEN      --don't reduce the amount if it hasn't been added yet 
                  accounting_array.update_accounting_tab ( 
                     :je_data.hold_account_id, 
                     NVL (v_hold_validate_amount, 0) * -1, 
                     NULL, 
                     :je_data.line_no); 
               ELSE 
                 accounting_array.update_accounting_tab ( 
                     :je_data.hold_account_id, 
                     NVL (v_hold_validate_amount, 0) * -1, 
                     NULL, 
                     :je_data.line_no); 
               accounting_array.acct_validated (:je_data.hold_account_id);--cec alio-3074 12/29/14 
               END IF; 
            END IF; 
 
            --if the new amount equals the old amount but the account has changed then the entire amount will need to be 
            --accounted for on the current account 
            IF (--NVL (v_validate_amount, 0) = NVL (v_hold_validate_amount, 0) AND --cec alio-3074 12/31/14 commented out 
             :je_data.account_id <> NVL (:je_data.hold_account_id, -1)) 
            THEN 
               v_amount := NVL (v_validate_amount, 0); 
            --else if neither the amounts or the accounts have changed then no amount change needs 
            --to be accounted for on the current amount 
            ELSIF (NVL (v_validate_amount, 0) = 
                      NVL (v_hold_validate_amount, 0) 
                   AND :je_data.account_id = 
                          NVL (:je_data.hold_account_id, -1)) 
            THEN 
               v_amount := 0; 
            ELSE --otherwise, an amount change has occurred and needs to be accounted for on the new account 
               v_amount := 
                  NVL (v_validate_amount, 0) 
                  - NVL (v_hold_validate_amount, 0); 
            END IF; 
 
            --update the current accounting element 
            accounting_array.update_accounting_tab (:je_data.account_id, 
                                                    v_amount, 
                                                    :je_data.account_no, 
                                                    :je_data.line_no); 
 
            --cec 11/12/14 -these resets are needed prior to validating the budget so that if the validation 
            --fails, the next time through the wvr process, the correct values are used for validation 
            :je_data.hold_account_id := :je_data.account_id; 
            :je_data.hold_journal_amount := :je_data.journal_amount; 
            :je_data.hold_dr_amount := :je_data.dr_amount; 
            :je_data.hold_cr_amount := :je_data.cr_amount; 
 
            --cec alio-3074 12/4/14 - the following checks to see if the balance should actually be checked 
            --for example, if an account is already in the hole and we are simply adding more funds to it (but not enough 
            --to make it positive) then we don't want the balance checked as it would fail 
            OPEN acct_type_cur (:je_data.account_id); 
 
            FETCH acct_type_cur INTO v_acct_type; 
 
            IF acct_type_cur%NOTFOUND 
            THEN 
               v_acct_type := 0; 
            END IF; 
 
            CLOSE acct_type_cur; 
 
            IF (v_acct_type = 0 
                OR (v_dc_flag = 'D' 
                    AND (v_acct_type BETWEEN '30' AND '38' 
                         OR v_acct_type BETWEEN '20' AND '28'--cec alio-3074 1/28/15 moved from Cr to Dr 
                         OR v_acct_type BETWEEN '90' AND '99') 
                         OR v_acct_type IN ('78','79')) 
                OR (v_dc_flag = 'C' 
                    AND (   v_acct_type BETWEEN '10' AND '18' 
                         OR v_acct_type = '77' 
                         OR v_acct_type BETWEEN '80' AND '89'))) 
            THEN 
               accounting_array.validate_budget (v_program); 
            ELSE                    --non balance checking action is occurring 
               accounting_array.acct_validated(:je_data.account_id);--don't validate the account balance later 
            END IF; 
 
         ELSIF (:je_data.account_id IS NULL 
                AND :je_data.hold_account_id IS NOT NULL) 
         THEN --the user has manually removed the account so the old account needs to have its amount reduced 
            IF (accounting_array.acct_id_line_exists ( 
                   :je_data.hold_account_id, 
                   :je_data.line_no)) 
            THEN         --don't reduce the amount if it hasn't been added yet 
               accounting_array.update_accounting_tab ( 
                  :je_data.hold_account_id, 
                  NVL (v_hold_validate_amount, 0) * -1, 
                  NULL, --don't care about the account no since the amount is being reduced, therefore passing in null 
                  :je_data.line_no); 
            END IF; 
         END IF; 
 
         :je_data.hold_account_id := :je_data.account_id; 
         :je_data.hold_journal_amount := :je_data.journal_amount; 
 
         :je_data.hold_dr_amount := :je_data.dr_amount; 
         :je_data.hold_cr_amount := :je_data.cr_amount; 
         --------cec alio-3074 12/1/14 end accounting_array logic---------- 
 
 
         v_trace := 'JBC8'; 
      END IF; -- if upper(nvl(trim(:parameter.journal_budget_check,'N') = 'YES' 
   END IF; 
 
   v_trace := 'JBC-END'; 
EXCEPTION 
   -- alert button code may raise form trigger failure that does not need another error message displayed 
   WHEN form_trigger_failure 
   THEN 
      RAISE; 
   WHEN OTHERS 
   THEN 
      message_display ( 
            'Error in JOURNAL_BUDGET_CHECK at trace: ' 
         || v_trace 
         || ' for account: ' 
         || :je_data.account_no 
         || ' Error: ' 
         || SQLERRM); 
 
      RAISE; 
END;
		*/
        #endregion
        //ID:341
        /// <summary></summary>
        /// <remarks>
        ///  Formatted on 12/08/2014 7:20:50 AM (QP5 v5.163.1008.3004) 
        ///  alio-3074 09/04/14  klb added. 
        ///  Called from je_data pre-insert, pre-update 
        ///  If profile set, 
        ///    Amount validated must have correct sign based on account type and debit or credit (uses fjent01a_debit_credit_amt) 
        ///    Uses alio IDI_LIB.OLB Accounting_Array to alert user if there is insufficient funds 
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void JournalBudgetCheck(JeDataAdapter jeDataElement)
        {
            int rowCount = 0;
            String sqlacctTypeCur = "SELECT ac.account_type " +
    " FROM shr.accounts ac " +
    " WHERE ac.account_id = @P_CUR_IN_ACCOUNT_ID ";
            DataCursor acctTypeCur = new DataCursor(sqlacctTypeCur);
            NNumber acctTypeCurCurInAccountId = NNumber.Null;
            NNumber vAcctType = NNumber.Null;
            NBool validateBalance = true;
            NNumber vDcAmount = 0;
            string vDcFlag = null;
            NNumber vHoldDcAmount = 0;
            // cec alio-3074 12/1/14 
            string vHoldDcFlag = null;
            // cec alio-3074 12/1/14 
            NNumber vValidateAmount = 0;
            NNumber vHoldValidateAmount = 0;
            // cec alio-3074 12/1/14 
            NNumber vAmount = NNumber.Null;
            // cec alio-3074 12/1/14 
            string vProgram = "FJENT01AFMX";
            string vTrace = null;
            #region
            try
            {
                try
                {
                    vTrace = "JBC-START";
                    if (jeDataElement.DrAmount != 0)
                    {
                        vDcAmount = jeDataElement.DrAmount;
                        vDcFlag = "D";
                    }
                    else if (jeDataElement.CrAmount != 0)
                    {
                        vDcAmount = jeDataElement.CrAmount;
                        vDcFlag = "C";
                    }
                    // cec alio-3074 12/1/14 
                    if (jeDataElement.HoldDrAmount != 0)
                    {
                        vHoldDcAmount = jeDataElement.HoldDrAmount;
                        vHoldDcFlag = "D";
                    }
                    else if (jeDataElement.HoldCrAmount != 0)
                    {
                        vHoldDcAmount = jeDataElement.HoldCrAmount;
                        vHoldDcFlag = "C";
                    }
                    MsgDebug("JBC trace: " + vTrace + ", v_dc_amount: " + vDcAmount.ToString() + ", v_dc_flag:" + vDcFlag + ", acct no: " + jeDataElement.AccountNo + ", acct_id: " + jeDataElement.AccountId.ToString() + ", je amt: " + jeDataElement.JournalAmount.ToString() + ", hold acct_id: " + jeDataElement.HoldAccountId.ToString() + ", hold je amt: " + jeDataElement.HoldJournalAmount.ToString());
                    vTrace = "JBC2";
                    if (vDcAmount != 0)
                    {
                        vTrace = "JBC3";
                        if (string.IsNullOrEmpty(this.Model.Params.JournalBudgetCheck))
                        {
                            Model.Params.JournalBudgetCheck = Lib.IsNull(Lib.Trim(StoredProcedures.FGetProfileData("JOURNAL_BUDGET_CHECK")), "N");
                        }
                        // msg_debug (
                        // 'JBC after profile journal_budget_check trace: '
                        // || v_trace
                        // || ' profile journal budget: '
                        // || :parameter.journal_budget_check);
                        vTrace = "JBC4";
                        if (Lib.Upper(Lib.IsNull(Lib.Trim(this.Model.Params.JournalBudgetCheck), "N")) == "YES")
                        {
                            //  Determine amount to validate (reverse sign depending on account type and dc flag) 
                            vTrace = "JBC5";
                            // msg_debug (
                            // 'JBC get validate amount trace: '
                            // || v_trace
                            // || ', account_no: '
                            // || :je_data.account_no
                            // || ', account id:'
                            // || :je_data.account_id
                            // || ', v_dc_amount: '
                            // || v_dc_amount);

                            vValidateAmount = StoredProcedures.Fjent01aAmtValidateBudget_(vDcAmount, vDcFlag, null, jeDataElement.AccountId);

                            MsgDebug("JBC after fjent01a_amt_validate_budget trace: " + vTrace + ", v_validate_amount: " + vValidateAmount.ToString());
                            // cec alio-3074 12/1/14 
                            vTrace = "JBC5b";
                            // msg_debug (
                            // 'JBC5b get validate amount trace: '
                            // || v_trace
                            // || ', hold acct id:'
                            // || :je_data.hold_account_id
                            // || ', v_hold_dc_amount: '
                            // || v_hold_dc_amount);
                            if ((Lib.IsNull(jeDataElement.HoldAccountId, 0) == 0))
                            {
                                vHoldValidateAmount = 0;
                            }
                            else
                            {
                                vHoldValidateAmount = StoredProcedures.Fjent01aAmtValidateBudget_(vHoldDcAmount, vHoldDcFlag, null, jeDataElement.HoldAccountId);
                            }
                            MsgDebug("JBC5b after fjent01a_amt_validate_budget trace: " + vTrace + ", v_hold_validate_amount: " + vHoldValidateAmount.ToString());
                            //  Use alio IDI_LIB Validate_budget to determine if amount should be validated and user alerted of insufficient funds 
                            vTrace = "JBC6";
                            // --cec alio-3074 commented out and changed to using accounting_array logic
                            // if nvl(v_validate_amount,0) != 0 then
                            // v_trace := 'JBC7';
                            // -- Call IDI_LIB validate_budget program unit (subclassed)
                            // validate_budget( :je_data.account_id
                            // ,v_validate_amount
                            // ,:je_data.account_no
                            // ,v_program);
                            // msg_debug('JBC after validate budget trace: '||v_trace||' v_validate_amount: '|| v_validate_amount);
                            // end if;
                            // ------cec alio-3074 12/1/14 begin accounting_array logic:---------- 
                            if ((!jeDataElement.AccountId.IsNull))
                            {
                                // if the account has changed, then reduce the old account amount 
                                if ((jeDataElement.AccountId != jeDataElement.HoldAccountId && !jeDataElement.HoldAccountId.IsNull))
                                {
                                    MsgDebug("jed.wvr - acct changed, calling update accounting tab to reduce old acct id");

                                    if ((Task.Packages.AccountingArray.FAcctIdLineExists(jeDataElement.HoldAccountId, jeDataElement.LineNo)))
                                    {
                                        Task.Packages.AccountingArray.UpdateAccountingTab(jeDataElement.HoldAccountId, Lib.IsNull(vHoldValidateAmount, 0) * -(1), null, jeDataElement.LineNo);
                                    }
                                    else
                                    {
                                        Task.Packages.AccountingArray.UpdateAccountingTab(jeDataElement.HoldAccountId, Lib.IsNull(vHoldValidateAmount, 0) * -(1), null, jeDataElement.LineNo);
                                        Task.Packages.AccountingArray.AcctValidated(jeDataElement.HoldAccountId);
                                    }
                                }
                                // if the new amount equals the old amount but the account has changed then the entire amount will need to be 
                                // accounted for on the current account 
                                if ((jeDataElement.AccountId != Lib.IsNull(jeDataElement.HoldAccountId, -(1))))
                                {
                                    vAmount = Lib.IsNull(vValidateAmount, 0);
                                }
                                else if ((Lib.IsNull(vValidateAmount, 0) == Lib.IsNull(vHoldValidateAmount, 0) && jeDataElement.AccountId == Lib.IsNull(jeDataElement.HoldAccountId, -(1))))
                                {
                                    vAmount = 0;
                                }
                                else
                                {
                                    // otherwise, an amount change has occurred and needs to be accounted for on the new account 
                                    vAmount = Lib.IsNull(vValidateAmount, 0) - Lib.IsNull(vHoldValidateAmount, 0);
                                }
                                // update the current accounting element 

                                Task.Packages.AccountingArray.UpdateAccountingTab(jeDataElement.AccountId, vAmount, jeDataElement.AccountNo, jeDataElement.LineNo);

                                // cec 11/12/14 -these resets are needed prior to validating the budget so that if the validation 
                                // fails, the next time through the wvr process, the correct values are used for validation 
                                jeDataElement.HoldAccountId = jeDataElement.AccountId;
                                jeDataElement.HoldJournalAmount = jeDataElement.JournalAmount;
                                jeDataElement.HoldDrAmount = jeDataElement.DrAmount;
                                jeDataElement.HoldCrAmount = jeDataElement.CrAmount;
                                // cec alio-3074 12/4/14 - the following checks to see if the balance should actually be checked 
                                // for example, if an account is already in the hole and we are simply adding more funds to it (but not enough 
                                // to make it positive) then we don't want the balance checked as it would fail 
                                acctTypeCurCurInAccountId = jeDataElement.AccountId;
                                //Setting query parameters
                                acctTypeCur.AddParameter("@P_CUR_IN_ACCOUNT_ID", acctTypeCurCurInAccountId);
                                acctTypeCur.Open();
                                ResultSet acctTypeCurResults = acctTypeCur.FetchInto();
                                if (acctTypeCurResults != null)
                                {
                                    vAcctType = acctTypeCurResults.GetString(0);
                                }
                                if (acctTypeCur.NotFound)
                                {
                                    vAcctType = 0;
                                }
                                if ((vAcctType == 0 || (vDcFlag == "D" && (vAcctType >= "30" && vAcctType <= "38" || vAcctType >= "20" && vAcctType <= "28" || vAcctType >= "90" && vAcctType <= "99") || (vAcctType == "78" || vAcctType == "79")) || (vDcFlag == "C" && (vAcctType >= "10" && vAcctType <= "18" || vAcctType == "77" || vAcctType >= "80" && vAcctType <= "89"))))
                                {
                                    Task.Packages.AccountingArray.ValidateBudget(vProgram);
                                }
                                else
                                {
                                    Task.Packages.AccountingArray.AcctValidated(jeDataElement.AccountId);
                                }
                            }
                            else if ((jeDataElement.AccountId.IsNull && !jeDataElement.HoldAccountId.IsNull))
                            {
                                // the user has manually removed the account so the old account needs to have its amount reduced 

                                if ((Task.Packages.AccountingArray.FAcctIdLineExists(jeDataElement.HoldAccountId, jeDataElement.LineNo)))
                                {
                                    Task.Packages.AccountingArray.UpdateAccountingTab(jeDataElement.HoldAccountId, Lib.IsNull(vHoldValidateAmount, 0) * -(1), null, jeDataElement.LineNo);
                                }

                            }
                            jeDataElement.HoldAccountId = jeDataElement.AccountId;
                            jeDataElement.HoldJournalAmount = jeDataElement.JournalAmount;
                            jeDataElement.HoldDrAmount = jeDataElement.DrAmount;
                            jeDataElement.HoldCrAmount = jeDataElement.CrAmount;
                            // ------cec alio-3074 12/1/14 end accounting_array logic---------- 
                            vTrace = "JBC8";
                        }
                    }
                    vTrace = "JBC-END";
                }
                catch (ApplicationException)
                {
                    throw;
                }
                catch (TaskControlException) { throw; }
                catch
                {
                    MessageDisplay("Error in JOURNAL_BUDGET_CHECK at trace: " + vTrace + " for account: " + jeDataElement.AccountNo + " Error: " + DbManager.ErrorMessage);
                    throw;
                }
            }
            finally
            {
                acctTypeCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_BALANCE_OLD
        /*
		FUNCTION get_account_balance_old RETURN number IS 
 cursor current_amounts_cur (in_account_id varchar2, in_accounting_period varchar2) is 
    select ca.account_id, 
           sum(ca.adjustment_cur) adjustment_cur, 
           sum(ca.amount_cur) amount_cur, 
           sum(ca.budget_cur) budget_cur, 
           sum(ca.encumbrance_cur) encumbrance_cur, 
           max(ac_t.encumbrance_multiplier) encumbrance_multiplier, 
           max(ac_t.amount_multiplier) amount_multiplier, 
           max(ac_t.budget_multiplier) budget_multiplier, 
           max(ac.pre_payable) pre_payable 
      from fas.current_amounts ca, 
               shr.accounts ac, 
               shr.account_types ac_t 
     where ca.account_id = in_account_id 
       and ca.account_period <= in_accounting_period 
       and ac.account_id = ca.account_id 
       and ac_t.account_type = ac.account_type 
  group by ca.account_id; 
  current_amounts_rec current_amounts_cur%rowtype; 
 
BEGIN 
 
  open current_amounts_cur(:je_data.account_id,:batch_no_block.accounting_period); 
  fetch current_amounts_cur into current_amounts_rec; 
  if current_amounts_cur%found then 
  return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) - 
          (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) - 
          (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0))- 
          (nvl(current_amounts_rec.pre_payable,0) * nvl(current_amounts_rec.amount_multiplier,0));   
  else 
  return 0; 
  end if; 
  close current_amounts_cur;  
  return 0; 
END;
		*/
        #endregion
        //ID:343
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual NNumber FGetAccountBalanceOld(JeDataAdapter jeDataElement)
        {
            int rowCount = 0;
            String sqlcurrentAmountsCur = "SELECT ca.account_id, sum(ca.adjustment_cur) adjustment_cur, sum(ca.amount_cur) amount_cur, sum(ca.budget_cur) budget_cur, sum(ca.encumbrance_cur) encumbrance_cur, max(ac_t.encumbrance_multiplier) encumbrance_multiplier, max(ac_t.amount_multiplier) amount_multiplier, max(ac_t.budget_multiplier) budget_multiplier, max(ac.pre_payable) pre_payable " +
    " FROM fas.current_amounts ca, shr.accounts ac, shr.account_types ac_t " +
    " WHERE ca.account_id = @P_IN_ACCOUNT_ID AND ca.account_period <= @P_IN_ACCOUNTING_PERIOD AND ac.account_id = ca.account_id AND ac_t.account_type = ac.account_type " +
    " GROUP BY ca.account_id ";
            DataCursor currentAmountsCur = new DataCursor(sqlcurrentAmountsCur);
            String currentAmountsCurInAccountId = string.Empty;
            String currentAmountsCurInAccountingPeriod = string.Empty;
            TableRow currentAmountsRec = null;
            #region
            try
            {
                currentAmountsCurInAccountId = jeDataElement.AccountId;
                currentAmountsCurInAccountingPeriod = Model.BatchNoBlock.AccountingPeriod;
                //Setting query parameters
                currentAmountsCur.AddParameter("@P_IN_ACCOUNT_ID", currentAmountsCurInAccountId);
                currentAmountsCur.AddParameter("@P_IN_ACCOUNTING_PERIOD", currentAmountsCurInAccountingPeriod);
                currentAmountsCur.Open();
                currentAmountsRec = currentAmountsCur.FetchRow();
                if (currentAmountsCur.Found)
                {
                    return ((Lib.IsNull(currentAmountsRec.GetString("budget_cur"), 0) * Lib.IsNull(currentAmountsRec.GetString("budget_multiplier"), 0)) + (Lib.IsNull(currentAmountsRec.GetString("adjustment_cur"), 0) * Lib.IsNull(currentAmountsRec.GetString("budget_multiplier"), 0))) - (Lib.IsNull(currentAmountsRec.GetString("encumbrance_cur"), 0) * Lib.IsNull(currentAmountsRec.GetString("encumbrance_multiplier"), 0)) - (Lib.IsNull(currentAmountsRec.GetString("amount_cur"), 0) * Lib.IsNull(currentAmountsRec.GetString("amount_multiplier"), 0)) - (Lib.IsNull(currentAmountsRec.GetString("pre_payable"), 0) * Lib.IsNull(currentAmountsRec.GetString("amount_multiplier"), 0));
                }
                else
                {
                    return 0;
                }
                return 0;
            }
            finally
            {
                currentAmountsCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_BALANCE
        /*
		FUNCTION get_account_balance RETURN number IS 
 cursor current_amounts_cur (in_account_id varchar2, in_accounting_period varchar2) is 
    select ca.account_id, 
           sum(ca.adjustment_cur) adjustment_cur, 
           sum(ca.amount_cur) amount_cur, 
           sum(ca.budget_cur) budget_cur, 
           sum(ca.encumbrance_cur) encumbrance_cur, 
           max(ac_t.encumbrance_multiplier) encumbrance_multiplier, 
           max(ac_t.amount_multiplier) amount_multiplier, 
           max(ac_t.budget_multiplier) budget_multiplier, 
           max(ac.pre_payable)   pre_payable, 
           max( ac.pre_budget)   pre_budget, 
           max(ac.pre_warehouse) pre_warehouse, 
           max(ac.pre_encumbrance)pre_encumbrance, 
           max(ac.account_type)   account_type 
      from fas.current_amounts ca, 
               shr.accounts ac, 
               shr.account_types ac_t 
     where ca.account_id = in_account_id 
       and ca.account_period <= in_accounting_period 
       and ac.account_id = ca.account_id 
       and ac_t.account_type = ac.account_type 
  group by ca.account_id; 
  current_amounts_rec current_amounts_cur%rowtype; 
 
  v_pre_amount number := 0; 
 
  v_return_amount number := 0;
 
 
 
BEGIN 

 -- dph alio-13043 create common code from fainq05a.fmx and use it; 
 v_return_amount := 
 fabal01a_avail_balance_ (:je_data.account_id,
                          'Y', --include_encubrance
                          :batch_no_block.accounting_period);
                          
 return v_return_amount;
 
 <multilinecomment> comment out below 
  open current_amounts_cur(:je_data.account_id,:batch_no_block.accounting_period); 
  fetch current_amounts_cur into current_amounts_rec; 
 
  ------ begin dph 13037 code ------- 
 -- determine pre amount based on code in fainq01a.fmx 
 -- and add it to the current amount 
  if current_amounts_cur%found then 
 
   if current_amounts_rec.account_type between '77' and '99' then 
     if current_amounts_rec.account_type in( '77' , '78') then 
      
       v_pre_amount := nvl(current_amounts_rec.pre_budget,0) 
                     - nvl(current_amounts_rec.pre_warehouse,0) 
                     - nvl(current_amounts_rec.pre_encumbrance,0) 
                     - nvl(current_amounts_rec.pre_payable,0); 
     else 
      v_pre_amount := nvl(current_amounts_rec.pre_budget,0)      * nvl(current_amounts_rec.budget_multiplier,0) 
                    - nvl(current_amounts_rec.pre_warehouse,0)   * nvl(current_amounts_rec.encumbrance_multiplier,0) 
                    - nvl(current_amounts_rec.pre_encumbrance,0) * nvl(current_amounts_rec.encumbrance_multiplier,0) 
                    - nvl(current_amounts_rec.pre_payable,0)     * nvl(current_amounts_rec.amount_multiplier,0); 
     end if; 
  
   else --no budget is set 
      v_pre_amount :=  nvl(current_amounts_rec.pre_warehouse,0) 
                     + nvl(current_amounts_rec.pre_encumbrance,0) 
                     + nvl(current_amounts_rec.pre_payable,0); 
   end if; 
  
 --return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) 
 --        + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) 
 --         - (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) 
 --         - (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0) 
 --         + nvl(v_pre_amount,0)); 
  
   return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) - 
          (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) - 
          (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0)) 
          + nvl(v_pre_amount,0);  
 
  -- end dph 13037-- 
         
   -- pre 13037 dph change the code was: 
   -- note the subtraction of the pre_payable 
 -- return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) - 
 --         (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) - 
 --         (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0))- 
 --        (nvl(current_amounts_rec.pre_payable,0) * nvl(current_amounts_rec.amount_multiplier,0));   
 
  
  else 
  return 0; 
  end if; 
  close current_amounts_cur;  
  return 0; 
 </multilinecomment> 
END;
		*/
        #endregion
        //ID:346
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual NNumber FGetAccountBalance(JeDataAdapter jeDataElement)
        {
            int rowCount = 0;
            String sqlcurrentAmountsCur = "SELECT ca.account_id, sum(ca.adjustment_cur) adjustment_cur, sum(ca.amount_cur) amount_cur, sum(ca.budget_cur) budget_cur, sum(ca.encumbrance_cur) encumbrance_cur, max(ac_t.encumbrance_multiplier) encumbrance_multiplier, max(ac_t.amount_multiplier) amount_multiplier, max(ac_t.budget_multiplier) budget_multiplier, max(ac.pre_payable) pre_payable, max(ac.pre_budget) pre_budget, max(ac.pre_warehouse) pre_warehouse, max(ac.pre_encumbrance) pre_encumbrance, max(ac.account_type) account_type " +
    " FROM fas.current_amounts ca, shr.accounts ac, shr.account_types ac_t " +
    " WHERE ca.account_id = @P_IN_ACCOUNT_ID AND ca.account_period <= @P_IN_ACCOUNTING_PERIOD AND ac.account_id = ca.account_id AND ac_t.account_type = ac.account_type " +
    " GROUP BY ca.account_id ";
            DataCursor currentAmountsCur = new DataCursor(sqlcurrentAmountsCur);
            String currentAmountsCurInAccountId = string.Empty;
            String currentAmountsCurInAccountingPeriod = string.Empty;
            TableRow currentAmountsRec = null;
            NNumber vPreAmount = 0;
            NNumber vReturnAmount = 0;
            //  dph alio-13043 create common code from fainq05a.fmx and use it; 

            vReturnAmount = StoredProcedures.Fabal01aAvailBalance_(jeDataElement.AccountId, "Y", Model.BatchNoBlock.AccountingPeriod);

            return vReturnAmount;
        }


        #region Original PL/SQL code for Prog Unit BUILD_APPROVAL_CHAIN
        /*
		PROCEDURE build_approval_chain IS
	 v_count number := 0;
begin
	
	
	if :parameter.journal_approval = 'Y' then
  
   
   v_count := nvl(hold.reference_table.count,0);
  
  --message('dphx build approvals user '||user||' count '||v_count);
  --message(' ',no_acknowledge);
   
   
   for i in 1..v_count
   loop
 	
 	  --message('dphx arrray value '||hold.reference_table(i).reference_no);
 	  --message(' ',no_acknowledge);
 	
	   fjapp01a_.setup_order_approvals(hold.reference_table(i).reference_no,
                                     user,
		                                 'Y'); -- Yes AJE approvals are turned on
    
     fjapp01a_.approve_next_level(hold.reference_table(i).reference_no,
                                  user,
		                              'Y');
   end loop;
   
   hold.reference_table.delete;

 end if;


end;
		*/
        #endregion
        //ID:347
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void BuildApprovalChain()
        {
            NNumber vCount = 0;
            if (this.Model.Params.JournalApproval == "Y")
            {
                vCount = Lib.IsNull(Task.Packages.Hold.referenceTable.Count, 0);
                // message('dphx build approvals user '||user||' count '||v_count);
                // message(' ',no_acknowledge);
                for (NInteger i = 1; i <= vCount; i++)
                {
                    Fjapp01a_.SetupOrderApprovals(Task.Packages.Hold.referenceTable[i].ReferenceNo, DbManager.User, "Y");
                    Fjapp01a_.ApproveNextLevel(Task.Packages.Hold.referenceTable[i].ReferenceNo, DbManager.User, "Y");
                }
                Task.Packages.Hold.referenceTable.Clear();
            }
        }


        #region Original PL/SQL code for Prog Unit BUILD_APPROVAL_ARRAY
        /*
		PROCEDURE build_approval_array(reference_no_in varchar2) IS

v_count number      := 0;
v_match varchar2(1) := 'N';

BEGIN

  if :parameter.journal_approval <> 'Y' then
    return;
  end if;
  
  v_count := nvl(hold.reference_table.count,0);
   
  for i in 1..v_count
  loop

  	if  hold.reference_table(i).reference_no = reference_no_in then
  		v_match := 'Y';
  		exit;
  	end if;
  	
  end loop;
   
 
   if  v_match = 'N' then
     v_count := v_count +1;
   	 hold.reference_table(v_count).reference_no := reference_no_in;
 	 end if;
  
END;
		*/
        #endregion
        //ID:349
        /// <summary></summary>
        /// <param name="referenceNoIn"></param>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void BuildApprovalArray(string referenceNoIn)
        {
            NNumber vCount = 0;
            string vMatch = "N";
            if (this.Model.Params.JournalApproval != "Y")
            {
                return;
            }
            vCount = Lib.IsNull(Task.Packages.Hold.referenceTable.Count, 0);
            for (NInteger i = 1; i <= vCount; i++)
            {
                if (Task.Packages.Hold.referenceTable[i].ReferenceNo == referenceNoIn)
                {
                    vMatch = "Y";
                    break;
                }
            }
            if (vMatch == "N")
            {
                vCount = vCount + 1;
                Task.Packages.Hold.referenceTable[vCount].ReferenceNo = int.Parse(referenceNoIn);
            }
        }


        #region Original PL/SQL code for Prog Unit HANDLE_ITEM_PROPERTIES
        /*
		<multilinecomment>*****************************************************************************</multilinecomment>
<multilinecomment> This Program Unit is a component of Object Group Item Property              </multilinecomment>
<multilinecomment> MNN Created 05/30/2003                                                      </multilinecomment>
<multilinecomment> Add a call to When_New_Form_Instance                                        </multilinecomment>
<multilinecomment> See following                                                                             </multilinecomment>
--
--   BEGIN
--      load_parameters;
--      manage_menu;
--      sutl01a.when_new_form_instance; -- Existing code
--	    handle_item_properties;
--   END;                              
--
<multilinecomment>*****************************************************************************</multilinecomment>

PROCEDURE HANDLE_ITEM_PROPERTIES IS

   CURSOR item_properties_cur is 
   select user_item_properties.menu_selection,
          user_item_properties.block_name||'.'||user_item_properties.item_name item,
          nvl(user_item_properties.prop_enabled,'Y')  prop_enabled,   
          nvl(user_item_properties.prop_update,'Y')   prop_update,    
          nvl(user_item_properties.prop_insert,'Y')   prop_insert,    
          nvl(user_item_properties.prop_visible,'Y')  prop_visible,   
          nvl(user_item_properties.prop_conceal,'N')  prop_conceal,   
          user_item_properties.prop_prompt            prop_prompt,
          nvl(user_item_properties.prop_required,'N') prop_required
     from adm.user_item_properties
    where user_item_properties.user_id = USER
      and user_item_properties.menu_selection = :system.current_form||'FMX' --upper('HMEMP01AFMX')
    union 
   select role_item_properties.menu_selection,
          role_item_properties.block_name||'.'||role_item_properties.item_name  item,
          nvl(role_item_properties.prop_enabled,'Y')  prop_enabled,   
          nvl(role_item_properties.prop_update,'Y')   prop_update,    
          nvl(role_item_properties.prop_insert,'Y')   prop_insert,    
          nvl(role_item_properties.prop_visible,'Y')  prop_visible,   
          nvl(role_item_properties.prop_conceal,'N')  prop_conceal,   
          role_item_properties.prop_prompt            prop_prompt,
          nvl(role_item_properties.prop_required,'N') prop_required
     from adm.role_item_properties
    where role_item_properties.role_id = :parameter.qtsi_role_id
      and role_item_properties.menu_selection = :system.current_form||'FMX' --upper('HMEMP01AFMX')
		and not exists (select menu_selection
		                  from adm.user_item_properties
							       where user_item_properties.user_id = USER
							         and user_item_properties.menu_selection = role_item_properties.menu_selection
								       and user_item_properties.block_name = role_item_properties.block_name
								       and user_item_properties.item_name = role_item_properties.item_name)
    union 
   select item_properties.menu_selection,
          item_properties.block_name||'.'||item_properties.item_name item,
          nvl(item_properties.prop_enabled,'Y')  prop_enabled,   
          nvl(item_properties.prop_update,'Y')   prop_update,    
          nvl(item_properties.prop_insert,'Y')   prop_insert,    
          nvl(item_properties.prop_visible,'Y')  prop_visible,   
          nvl(item_properties.prop_conceal,'N')  prop_conceal,   
          item_properties.prop_prompt            prop_prompt,
          nvl(item_properties.prop_required,'N') prop_required
     from adm.item_properties
    where item_properties.menu_selection = :system.current_form||'FMX' --upper('HMEMP01AFMX')
		and not exists (select menu_selection
		                  from adm.user_item_properties
							       where user_item_properties.user_id = USER
							         and user_item_properties.menu_selection = item_properties.menu_selection
								       and user_item_properties.block_name = item_properties.block_name
								       and user_item_properties.item_name = item_properties.item_name)
		and not exists (select menu_selection
		                  from adm.role_item_properties
							       where role_item_properties.role_id = :parameter.qtsi_role_id
                              and role_item_properties.menu_selection = item_properties.menu_selection
								       and role_item_properties.block_name = item_properties.block_name
								       and role_item_properties.item_name = item_properties.item_name)
   order by 3; --Item

   done_with_rec varchar2(1);
   v_counter     number := 0;
BEGIN
   
   hold.required_table.delete;
	 
	 for prop_rec in item_properties_cur loop

      done_with_rec := 'N';
     --message('item '||prop_rec.item); message(' ',no_acknowledge);

      --VISIBLE
      if prop_rec.prop_visible = 'N' and done_with_rec = 'N' then
         set_item_property(prop_rec.item, visible, property_false);
         done_with_rec := 'Y';
--      else
--         set_item_property(prop_rec.item, visible, property_true);
      end if;
      
      --ENABLED
      if prop_rec.prop_enabled = 'N' and done_with_rec = 'N' then
         set_item_property(prop_rec.item, enabled, property_false);
         done_with_rec := 'Y';
--      else
--         set_item_property(prop_rec.item, enabled, property_true);
      end if;
    
--      message('conceal '||prop_rec.prop_conceal||' - '||done_with_rec); message(' ',no_acknowledge);
      -- CONCEAL
      if prop_rec.prop_conceal = 'Y' and done_with_rec = 'N' then
         set_item_property(prop_rec.item, conceal_data, property_true);
         set_item_property(prop_rec.item, update_allowed, property_false);
         set_item_property(prop_rec.item, insert_allowed, property_false);
         done_with_rec := 'Y';
--      else
--         set_item_property(prop_rec.item, conceal_data, property_false);
      end if;
 
      --UPDATE
      if prop_rec.prop_update = 'N' and done_with_rec = 'N' then      
         set_item_property(prop_rec.item, update_allowed, property_false);
--      else
--         set_item_property(prop_rec.item, update_allowed, property_true);
     	end if;
       
      --INSERT
      if prop_rec.prop_insert = 'N' and done_with_rec = 'N' then
         set_item_property(prop_rec.item, insert_allowed, property_false);
--      else
--         set_item_property(prop_rec.item, insert_allowed, property_true);
      end if;

      -- PROMPT
      if prop_rec.prop_prompt is not null then
    	   set_item_property(prop_rec.item, prompt_text,prop_rec.prop_prompt);
      end if;
    
      -- REQUIRED
      if prop_rec.prop_required = 'Y' then

         Set_Item_Property(prop_rec.item, 
                           VISUAL_ATTRIBUTE,
                           'REQUIRED');       
--      else
--         Set_Item_Instance_Property(prop_rec.item, 
--                                    CURRENT_RECORD, 
--                                    VISUAL_ATTRIBUTE,
--                                    'DEFAULT');
      end if;
  
       -- REQUIRED
       -- build an array to handle the required fields.
       -- they will be set after the user tabs from the header block.
       -- note: prop_rec.item includes block
      if prop_rec.prop_required = 'Y' and done_with_rec = 'N' then      
         --Set_Item_Property(prop_rec.item, REQUIRED, PROPERTY_TRUE);
         v_counter := v_counter + 1;
        hold.required_table(v_counter).field_name := prop_rec.item;
      end if;

	 end loop;

END;
		*/
        #endregion
        //ID:350
        /// <summary></summary>
        /// <remarks>
        /// *****************************************************************************
        ///  This Program Unit is a component of Object Group Item Property              
        ///  MNN Created 05/30/2003                                                      
        ///  Add a call to When_New_Form_Instance                                        
        ///  See following                                                                             
        /// 
        ///    BEGIN
        ///       load_parameters;
        ///       manage_menu;
        ///       sutl01a.when_new_form_instance; -- Existing code
        /// 	    handle_item_properties;
        ///    END;                              
        /// 
        /// *****************************************************************************
        /// F2N_STRONG_PRESENTATION_LOGIC : The code of this procedure was identified as containing presentation logic. See documentation for details.
        /// </remarks>
        public virtual void HandleItemProperties()
        {
            int rowCount = 0;
            String sqlitemPropertiesCur = "SELECT user_item_properties.menu_selection, user_item_properties.block_name || '.' || user_item_properties.item_name item, nvl(user_item_properties.prop_enabled, 'Y') prop_enabled, nvl(user_item_properties.prop_update, 'Y') prop_update, nvl(user_item_properties.prop_insert, 'Y') prop_insert, nvl(user_item_properties.prop_visible, 'Y') prop_visible, nvl(user_item_properties.prop_conceal, 'N') prop_conceal, user_item_properties.prop_prompt prop_prompt, nvl(user_item_properties.prop_required, 'N') prop_required " +
    " FROM adm.user_item_properties " +
    " WHERE user_item_properties.user_id = USER AND user_item_properties.menu_selection = @SYSTEM_CURRENT_FORM || 'FMX' " +
    "UNION SELECT role_item_properties.menu_selection, role_item_properties.block_name || '.' || role_item_properties.item_name item, nvl(role_item_properties.prop_enabled, 'Y') prop_enabled, nvl(role_item_properties.prop_update, 'Y') prop_update, nvl(role_item_properties.prop_insert, 'Y') prop_insert, nvl(role_item_properties.prop_visible, 'Y') prop_visible, nvl(role_item_properties.prop_conceal, 'N') prop_conceal, role_item_properties.prop_prompt prop_prompt, nvl(role_item_properties.prop_required, 'N') prop_required " +
    " FROM adm.role_item_properties " +
    " WHERE role_item_properties.role_id = @PARAMETER_QTSI_ROLE_ID AND role_item_properties.menu_selection = @SYSTEM_CURRENT_FORM || 'FMX' AND  NOT EXISTS(SELECT menu_selection " +
        " FROM adm.user_item_properties " +
        " WHERE user_item_properties.user_id = USER AND user_item_properties.menu_selection = role_item_properties.menu_selection AND user_item_properties.block_name = role_item_properties.block_name AND user_item_properties.item_name = role_item_properties.item_name ) " +
    "UNION SELECT item_properties.menu_selection, item_properties.block_name || '.' || item_properties.item_name item, nvl(item_properties.prop_enabled, 'Y') prop_enabled, nvl(item_properties.prop_update, 'Y') prop_update, nvl(item_properties.prop_insert, 'Y') prop_insert, nvl(item_properties.prop_visible, 'Y') prop_visible, nvl(item_properties.prop_conceal, 'N') prop_conceal, item_properties.prop_prompt prop_prompt, nvl(item_properties.prop_required, 'N') prop_required " +
    " FROM adm.item_properties " +
    " WHERE item_properties.menu_selection = @SYSTEM_CURRENT_FORM || 'FMX' AND  NOT EXISTS(SELECT menu_selection " +
        " FROM adm.user_item_properties " +
        " WHERE user_item_properties.user_id = USER AND user_item_properties.menu_selection = item_properties.menu_selection AND user_item_properties.block_name = item_properties.block_name AND user_item_properties.item_name = item_properties.item_name ) AND  NOT EXISTS(SELECT menu_selection " +
        " FROM adm.role_item_properties " +
        " WHERE role_item_properties.role_id = @PARAMETER_QTSI_ROLE_ID AND role_item_properties.menu_selection = item_properties.menu_selection AND role_item_properties.block_name = item_properties.block_name AND role_item_properties.item_name = item_properties.item_name ) " +
    " ORDER BY 3";
            DataCursor itemPropertiesCur = new DataCursor(sqlitemPropertiesCur);
            // Item
            string doneWithRec = string.Empty;
            NNumber vCounter = 0;
            Task.Packages.Hold.requiredTable.Clear();
            //Setting query parameters
            itemPropertiesCur.AddParameter("@SYSTEM_CURRENT_FORM", TaskServices.CurrentForm);
            itemPropertiesCur.AddParameter("@PARAMETER_QTSI_ROLE_ID", this.Model.Params.QtsiRoleId);
            #region loop for cursor itemPropertiesCur
            try
            {
                itemPropertiesCur.Open();
                while (true)
                {
                    TableRow propRec = itemPropertiesCur.FetchRow();
                    if (itemPropertiesCur.NotFound) break;
                    doneWithRec = "N";
                    // message('item '||prop_rec.item); message(' ',no_acknowledge);
                    // VISIBLE
                    if (propRec.GetString("prop_visible") == "N" && doneWithRec == "N")
                    {
                        ItemServices.SetVisible(propRec.GetString("item"), false);
                        doneWithRec = "Y";
                    }
                    // ENABLED
                    if (propRec.GetString("prop_enabled") == "N" && doneWithRec == "N")
                    {
                        ItemServices.SetEnabled(propRec.GetString("item"), false);
                        doneWithRec = "Y";
                    }
                    //       message('conceal '||prop_rec.prop_conceal||' - '||done_with_rec); message(' ',no_acknowledge);
                    //  CONCEAL
                    if (propRec.GetString("prop_conceal") == "Y" && doneWithRec == "N")
                    {
                        ItemServices.SetUpdateAllowed(propRec.GetString("item"), false);
                        ItemServices.SetInsertAllowed(propRec.GetString("item"), false);
                        ItemServices.SetDisplayAsPassword(propRec.GetString("item"), true);
                        doneWithRec = "Y";
                    }
                    // UPDATE
                    if (propRec.GetString("prop_update") == "N" && doneWithRec == "N")
                    {
                        ItemServices.SetUpdateAllowed(propRec.GetString("item"), false);
                    }
                    // INSERT
                    if (propRec.GetString("prop_insert") == "N" && doneWithRec == "N")
                    {
                        ItemServices.SetInsertAllowed(propRec.GetString("item"), false);
                    }
                    //  PROMPT
                    if (!String.IsNullOrEmpty(propRec.GetString("prop_prompt")))
                    {
                        ItemServices.SetPromptText(propRec.GetString("item"), propRec.GetString("prop_prompt"));
                    }
                    //  REQUIRED
                    if (propRec.GetString("prop_required") == "Y")
                    {
                        ItemServices.SetStyleClass(propRec.GetString("item"), "REQUIRED");
                    }
                    //  REQUIRED
                    //  build an array to handle the required fields.
                    //  they will be set after the user tabs from the header block.
                    //  note: prop_rec.item includes block
                    if (propRec.GetString("prop_required") == "Y" && doneWithRec == "N")
                    {
                        // Set_Item_Property(prop_rec.item, REQUIRED, PROPERTY_TRUE);
                        vCounter = vCounter + 1;
                        Task.Packages.Hold.requiredTable[vCounter].FieldName = propRec.GetString("item");
                    }
                }
            }
            finally
            {
                itemPropertiesCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit SPREADSHEET
        /*
		--alio-16900 mfl 20.1
--This procedure will take the data being displayed and will open up in a spreadsheet.

PROCEDURE SPREADSHEET IS

   v_form_name      varchar2(20);
   v_block_name     varchar2(30);
   v_xls_block_name varchar2(30);
   v_row_no         number;
   v_sort_order     number := 1; 
   
   v_column_1   varchar2(200);
   v_column_2   varchar2(200);
   v_column_3   varchar2(200);
   v_column_4   varchar2(200);
   v_column_5   varchar2(200);
   v_column_6   varchar2(200);
   v_column_7   varchar2(200);
   v_column_8   varchar2(200);
   v_column_9   varchar2(200);
   v_column_10   varchar2(200);
   v_column_11   varchar2(200);
   v_column_12   varchar2(200);
   v_column_13   varchar2(200);
   v_column_14   varchar2(200);
   v_column_15   varchar2(200);
   v_column_16   varchar2(200);
   v_column_17   varchar2(200);
   v_column_18   varchar2(200);
   v_column_19   varchar2(200);
   v_column_20   varchar2(200); 
   
   v_report_other varchar2(4000);   
   
   -- the report will only accept 32 characters in an input parm so concatenate a b and c together.
   v_parm_heading varchar2(2000); 

   

BEGIN
   v_form_name := 'FJENT01A';
   
  -- FIRST_RECORD;
 
  -- Set first row with the item names
      	 v_block_name := 'JE_DATA';
      	 v_xls_block_name := 'Journals';
      	 v_column_1 := 'Account Number';
		     v_column_2 := 'Description';
 	       v_column_3 := 'Account Balance';
 	       v_column_4 := 'Debit Amount';
 	       v_column_5 := 'Credit Amount';
 	       v_column_6 := null;
    	   v_column_7 := null;
		     v_column_8 := null;
		     v_column_9 := null;
		     v_column_10 := null;
		     v_column_11 := null;
		     v_column_12 := null;
		     v_column_13 := null;
		     v_column_14 := null;
		     v_column_15 := null;
		     v_column_16 := null;
		     v_column_17 := null;
		     v_column_18 := null;
		     v_column_19 := null;
		     v_column_20 := null;
	  v_row_no := 1;

	  sxinq01a_insert_spreadsheet_(
          v_form_name,
          v_xls_block_name,
          v_sort_order, -- dph alio-17524 added sort option
          :global.qtsi_user_id, 
	        v_row_no,
	        v_column_1,
					v_column_2,
					v_column_3,
					v_column_4,
					v_column_5,
					v_column_6,
					v_column_7,
					v_column_8,
					v_column_9,
					v_column_10,
					v_column_11,
					v_column_12,
					v_column_13,
					v_column_14,
					v_column_15,
					v_column_16,
					v_column_17,
					v_column_18,
					v_column_19,
					v_column_20);
      
      go_block(v_block_name);
      first_record;
         
      LOOP
	     v_row_no := v_row_no + 1;
	     
       v_column_1 := nvl(to_char(:je_data.account_no),' ');
       v_column_2 := nvl(to_char(:je_data.account_desc),' ');
       v_column_3 := nvl(to_char(:je_data.account_balance),'0');
       v_column_4 := nvl(to_char(:je_data.dr_amount),'0');
       v_column_5 := nvl(to_char(:je_data.cr_amount),'0');


	  sxinq01a_insert_spreadsheet_(
	        v_form_name,
	        v_xls_block_name,
	        v_sort_order,
	        :global.qtsi_user_id,
	        v_row_no,
	        v_column_1,
					v_column_2,
					v_column_3,
					v_column_4,
					v_column_5,
					v_column_6,
					v_column_7,
					v_column_8,
					v_column_9,
					v_column_10,
					v_column_11,
					v_column_12,
					v_column_13,
					v_column_14,
					v_column_15,
					v_column_16,
					v_column_17,
					v_column_18,
					v_column_19,
					v_column_20);

      EXIT WHEN :SYSTEM.LAST_RECORD = 'TRUE';
      NEXT_RECORD;
   END LOOP;
   
   GO_BLOCK(v_block_name);   	
   FIRST_RECORD;   

--run report that display in spreadsheet
  :global.run_in_xcl := 'Y';
  v_report_other := null;
  v_report_other  := rpt_append_parms(v_report_other,'parm_block_name',v_xls_block_name);
  v_report_other  := rpt_append_parms(v_report_other,'parm_form_name',v_form_name);
  v_report_other  := rpt_append_parms(v_report_other,'parm_user',:global.qtsi_user_id);

 for i in 1..5 loop
     v_sort_order := 0;
     v_row_no := v_row_no +1;  -- DO NOT START WITH 1!!; SXINQ01A.SQL deletes when row_no = 1 
	   if i = 1 then
       v_parm_heading := 'Run Date: ' || to_char(sysdate, 'MM/DD/YYYY HH:MI AM');
	   elsif i = 2 then
       v_parm_heading := 'Batch Year:  '||nvl(:je_header.batch_year,' ')||
                         '  Batch No: '||nvl(:je_header.batch_no,' ');
                         --'  Debit Total: '||nvl(:je_header.debit_total,'0')||
                        -- '  Credit Total: '||nvl(:je_header.credit_total,'0');

			   if nvl(:parameter.use_order_no,'N') = 'Y' then 
		       v_parm_heading :=  v_parm_heading || '  Order No: '||nvl(:je_header.whs_order_no,' ');
			   end if;
			   
         if :parameter.journal_approval = 'Y' then
          	v_parm_heading := v_parm_heading ||'  Ready for Approval: '||:je_header.ready_for_approval;
         end if; 			   
			   
	   elsif i = 3 then
       v_parm_heading := 'Reference No: '||nvl(:je_header.reference_no,' ')||
                         ' Date: '||nvl(to_char(:je_header.journal_date,'MM-DD-YYYY'),' ')||
                         '  Description: '||nvl(:je_header.journal_description,' ');
                         
         if :parameter.journal_approval = 'Y' then
         	v_parm_heading := v_parm_heading ||'  Status: '||:je_header.approval_status_desc;
         end if;         
	   elsif i = 4 then
	   	 v_parm_heading :='Approval Chain: '||nvl(:je_header.approval_chain,' ');
	   elsif i = 5 then
	   	 v_parm_heading := null; -- blank row between parms and detail
	   	 
	   end if;

   
	   sxinq01a_insert_spreadsheet_(
          v_form_name,
          v_xls_block_name,
          v_sort_order,
          :global.qtsi_user_id, 
	        v_row_no,
	        v_parm_heading,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null);
  end loop;



  RPT_RUN_REPORT( p_report_module      => 'sxinq01a'        -- in VARCHAR2  -- Required. REPORT MODULE FILE to execute w\o extention
                 ,p_report_obj_name    => 'sxinq01a'        -- in VARCHAR2  -- Required. REPORT OBJECT NAME from forms builder
                 ,p_report_otherparam  => v_report_other);  -- Optional. String of hidden user parameters
                                         

  :global.run_in_xcl := 'N';

END;
		*/
        #endregion
        //ID:354
        /// <summary></summary>
        /// <remarks>
        /// alio-16900 mfl 20.1
        /// This procedure will take the data being displayed and will open up in a spreadsheet.
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void Spreadsheet(JeDataAdapter jeDataElement, JeHeaderAdapter jeHeaderElement)
        {
            string vFormName = string.Empty;
            string vBlockName = string.Empty;
            string vXlsBlockName = string.Empty;
            NNumber vRowNo = NNumber.Null;
            NNumber vSortOrder = 1;
            string vColumn1 = string.Empty;
            string vColumn2 = string.Empty;
            string vColumn3 = string.Empty;
            string vColumn4 = string.Empty;
            string vColumn5 = string.Empty;
            string vColumn6 = string.Empty;
            string vColumn7 = string.Empty;
            string vColumn8 = string.Empty;
            string vColumn9 = string.Empty;
            string vColumn10 = string.Empty;
            string vColumn11 = string.Empty;
            string vColumn12 = string.Empty;
            string vColumn13 = string.Empty;
            string vColumn14 = string.Empty;
            string vColumn15 = string.Empty;
            string vColumn16 = string.Empty;
            string vColumn17 = string.Empty;
            string vColumn18 = string.Empty;
            string vColumn19 = string.Empty;
            string vColumn20 = string.Empty;
            string vReportOther = string.Empty;
            //  the report will only accept 32 characters in an input parm so concatenate a b and c together.
            string vParmHeading = string.Empty;
            vFormName = "FJENT01A";
            //  FIRST_RECORD;
            //  Set first row with the item names
            vBlockName = "JE_DATA";
            vXlsBlockName = "Journals";
            vColumn1 = "Account Number";
            vColumn2 = "Description";
            vColumn3 = "Account Balance";
            vColumn4 = "Debit Amount";
            vColumn5 = "Credit Amount";
            vColumn6 = null;
            vColumn7 = null;
            vColumn8 = null;
            vColumn9 = null;
            vColumn10 = null;
            vColumn11 = null;
            vColumn12 = null;
            vColumn13 = null;
            vColumn14 = null;
            vColumn15 = null;
            vColumn16 = null;
            vColumn17 = null;
            vColumn18 = null;
            vColumn19 = null;
            vColumn20 = null;
            vRowNo = 1;

            StoredProcedures.Sxinq01aInsertSpreadsheet_(vFormName, vXlsBlockName, vSortOrder, Globals.QtsiUserId, vRowNo, vColumn1, vColumn2, vColumn3, vColumn4, vColumn5, vColumn6, vColumn7, vColumn8, vColumn9, vColumn10, vColumn11, vColumn12, vColumn13, vColumn14, vColumn15, vColumn16, vColumn17, vColumn18, vColumn19, vColumn20);

            BlockServices.GoBlock(vBlockName);
            BlockServices.FirstRecord();
            while (true)
            {
                vRowNo = vRowNo + 1;
                vColumn1 = Lib.IsNull(Lib.ToChar(jeDataElement.AccountNo.ToString()), " ").ToString();
                vColumn2 = Lib.IsNull(Lib.ToChar(jeDataElement.AccountDesc.ToString()), " ").ToString();
                vColumn3 = Lib.IsNull(Lib.ToChar(jeDataElement.AccountBalance), "0").ToString();
                vColumn4 = Lib.IsNull(Lib.ToChar(jeDataElement.DrAmount), "0").ToString();
                vColumn5 = Lib.IsNull(Lib.ToChar(jeDataElement.CrAmount), "0").ToString();

                StoredProcedures.Sxinq01aInsertSpreadsheet_(vFormName, vXlsBlockName, vSortOrder, Alio.Common.Globals.QtsiUserId, vRowNo, vColumn1, vColumn2, vColumn3, vColumn4, vColumn5, vColumn6, vColumn7, vColumn8, vColumn9, vColumn10, vColumn11, vColumn12, vColumn13, vColumn14, vColumn15, vColumn16, vColumn17, vColumn18, vColumn19, vColumn20);

                if (BlockServices.InLastRecord)
                    break;
                BlockServices.NextRecord();
            }
            BlockServices.GoBlock(vBlockName);
            BlockServices.FirstRecord();
            // run report that display in spreadsheet
            Alio.Common.Globals.RunInXcl = "Y";
            vReportOther = null;
            vReportOther = Task.Libraries.Afilerpt.FRptAppendParms(vReportOther, "parm_block_name", vXlsBlockName);
            vReportOther = Task.Libraries.Afilerpt.FRptAppendParms(vReportOther, "parm_form_name", vFormName);
            vReportOther = Task.Libraries.Afilerpt.FRptAppendParms(vReportOther, "parm_user", Alio.Common.Globals.QtsiUserId);
            for (NInteger i = 1; i <= 5; i++)
            {
                vSortOrder = 0;
                vRowNo = vRowNo + 1;
                //  DO NOT START WITH 1!!; SXINQ01A.SQL deletes when row_no = 1 
                if (i == 1)
                {
                    vParmHeading = "Run Date: " + Lib.ToChar(NDate.Now, "MM/DD/YYYY HH:MI AM").ToString();
                }
                else if (i == 2)
                {
                    vParmHeading = "Batch Year:  " + Lib.IsNull(jeHeaderElement.BatchYear, " ").ToString() + "  Batch No: " + Lib.IsNull(jeHeaderElement.BatchNo, " ").ToString();
                    // '  Debit Total: '||nvl(:je_header.debit_total,'0')||
                    //  '  Credit Total: '||nvl(:je_header.credit_total,'0');
                    if (Lib.IsNull(this.Model.Params.UseOrderNo, "N") == "Y")
                    {
                        vParmHeading = vParmHeading + "  Order No: " + Lib.IsNull(jeHeaderElement.WhsOrderNo, " ").ToString();
                    }
                    if (this.Model.Params.JournalApproval == "Y")
                    {
                        vParmHeading = vParmHeading + "  Ready for Approval: " + jeHeaderElement.ReadyForApproval;
                    }
                }
                else if (i == 3)
                {
                    vParmHeading = "Reference No: " + Lib.IsNull(jeHeaderElement.ReferenceNo, " ").ToString() + " Date: " + Lib.IsNull(Lib.ToChar(jeHeaderElement.JournalDate, "MM-DD-YYYY"), " ").ToString() + "  Description: " + Lib.IsNull(jeHeaderElement.JournalDescription, " ").ToString();
                    if (this.Model.Params.JournalApproval == "Y")
                    {
                        vParmHeading = vParmHeading + "  Status: " + jeHeaderElement.ApprovalStatusDesc;
                    }
                }
                else if (i == 4)
                {
                    vParmHeading = "Approval Chain: " + Lib.IsNull(jeHeaderElement.ApprovalChain, " ").ToString();
                }
                else if (i == 5)
                {
                    vParmHeading = null;
                }

                StoredProcedures.Sxinq01aInsertSpreadsheet_(vFormName, vXlsBlockName, vSortOrder, Globals.QtsiUserId, vRowNo, vParmHeading, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            }
            Task.Libraries.Afilerpt.RptRunReport(pReportModule: "sxinq01a", pReportObjName: "sxinq01a", pReportOtherparam: vReportOther);
            //  Optional. String of hidden user parameters
            Alio.Common.Globals.RunInXcl = "N";
        }




    }
}
