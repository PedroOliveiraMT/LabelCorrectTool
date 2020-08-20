using Alio.Common.DbServices;
using Alio.Forms.Slact01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Task;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Slact01a.Services
{
    public class Slact01aServices : AbstractServices<Slact01aModel>
    {

        public Slact01aServices(Slact01aModel model) : base(model)
        {
        }

        public new Slact01aTask Task
        {
            get { return (Slact01aTask)base.Task; }
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
        //ID:94
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



        #region Original PL/SQL code for Prog Unit SET_LOV
        /*
		PROCEDURE set_lov IS
<multilinecomment>
called when client is not using the new account securtiy
</multilinecomment>  
   group_status  number;
   group_count   number;
   lov_selected  boolean;
   group_id      recordgroup;
   lov_id        lov;
   
   cursor account_no_cur (cur_in_account_no in varchar2) is
   select accounts.account_id,
          accounts.account_no,
          accounts.inactive_flag
     from shr.accounts,
          shr.nick_accounts
    where nick_accounts.account_nick_no = cur_in_account_no 
      and accounts.account_id           = nick_accounts.account_id
      and (:global.account_year         = accounts.account_year
           or :global.account_year is null);
   account_rec account_no_cur%rowtype;
   
   cursor account_no_by_id_cur (cur_in_account_id in number) is
   select accounts.account_no
     from shr.accounts
    where account_id = cur_in_account_id;
   account_no_by_id_rec account_no_by_id_cur%rowtype;

   hold_account_id shr.accounts.account_id%type;

  
BEGIN
	
   lov_id := find_lov('accounts_lov');
 
  if :global.debug_status = 'ON' then
	  message(' set lov act_year '||:global.account_year||
	          ' act_no '||:global.account_no||
	          ' qtsi_role_id '||:global.qtsi_role_id||
	          ' qtsi_user_id '||:global.qtsi_user_id||
	          ' transaction_desc '||:global.transaction_desc);
	  message(' ',no_acknowledge);
  end if;



   if :global.transaction_desc is null then 
       set_lov_property(lov_id,group_name,'all_acts_all_old');
       set_lov_property(lov_id,title,' Select Accounts - ao');
   else
        set_lov_property(lov_id,group_name,'all_acts_all_trans_old');
        set_lov_property(lov_id,title,' Select Accounts - ato');
   end if;

 
if :global.account_no is not null then
      open account_no_cur (:global.account_no);
      loop
         fetch account_no_cur into account_rec;
         if account_no_cur%found then
            hold_account_id :=
               amsec06b_.find_account(account_rec.account_no,
                                      :global.account_year,--year
                                      :global.qtsi_role_id,
                                      :global.qtsi_user_id);
            if hold_account_id > 0 then
               exit;
            end if;
         else
            exit;
         end if;
      end loop;
      
    
    	 if :global.debug_status = 'ON' then
		      message('debug set_lov hold_account_id '||hold_account_id);
     	    message(' ',no_acknowledge);
			 end if;
    
    if account_no_cur%notfound then
   
       	 if :global.debug_status = 'ON' then
		       message('debug set_lov - account_no_cur%notfound');
     	     message(' ',no_acknowledge);
       	 end if;
       	 
         close account_no_cur;
         if :global.transaction_desc is null then
             group_id := find_group('all_acts_like_no_old');
         	 	 group_status := populate_group(group_id);
         else
            group_id := find_group('all_acts_like_no_trans_old');
            group_status := populate_group(group_id);
         end if;
      
          if :global.debug_status = 'ON' then
		       message('debug set_lov - group_status '||group_status);
     	     message(' ',no_acknowledge);
       	 end if;   
         
        if group_status <> 0 then
            set_application_property(cursor_style,'default');
            lov_selected := show_lov(lov_id);
        else
            group_count := get_group_row_count(group_id);
 
            if :global.debug_status = 'ON' then
		          message('debug set_lov - group_count '||group_count);
     	        message(' ',no_acknowledge);
       	    end if; 
 
            if group_count = 1 then
               if :global.transaction_desc is null then
                    :global.account_id :=
                        get_group_number_cell('all_acts_like_no_old.account_id',
                                              1);
 
               else -- global.transaction_desc is null
                  :global.account_id :=
                     get_group_number_cell('all_acts_like_no_trans_old.'
                                        || 'account_id',
                                           1);
               end if;-- :global.transaction_desc is null
            
            elsif group_count > 1 then
               set_lov_property(lov_id,
                                auto_refresh,
                                property_false);
               if :global.transaction_desc is null then
                     set_lov_property(lov_id,group_name,'all_acts_like_no_old');
                     set_lov_property(lov_id,title,' Select Accounts - alo');
               else
                    set_lov_property(lov_id,group_name,'all_acts_like_no_trans_old');
                    set_lov_property(lov_id,title,' Select Accounts - alto');
               end if;
               
               set_application_property(cursor_style,'default');
               lov_selected := show_lov(lov_id);
              
                if :global.transaction_desc is null then
                    set_lov_property(lov_id,group_name,'all_acts_all_old');
                     set_lov_property(lov_id,title,' Select Accounts - ao');
                else
                    set_lov_property(lov_id,group_name,'all_acts_all_trans_old');
                    set_lov_property(lov_id,title,' Select Accounts - ato');
                end if;
            end if; -- group_count = 1
         end if; -- group_status <> 0
      else -- global account_no is not null
  	
         :global.account_id := account_rec.account_id;
         loop
            fetch account_no_cur into account_rec;
            if account_no_cur%found then
               hold_account_id :=
                  amsec06b_.find_account(account_rec.account_no,
                                         :global.account_year,--year
                                         :global.qtsi_role_id,
                                         :global.qtsi_user_id);
               if hold_account_id is not null then
                  exit;
               end if;
            else
               exit;
            end if;
         end loop;
         if account_no_cur%found then
            if :global.transaction_desc is null then
                 set_lov_property(lov_id,group_name,'all_acts_by_no_old');
                 set_lov_property(lov_id,title,' Select Accounts - abo');
            else
                 set_lov_property(lov_id,group_name,'all_acts_by_no_trans_old');
                 set_lov_property(lov_id,title,' Select Accounts - abto');
            end if;
            
            set_application_property(cursor_style,'default');
            lov_selected := show_lov(lov_id);
            if :global.transaction_desc is null then
                set_lov_property(lov_id,group_name,'all_acts_all_old');
                set_lov_property(lov_id,title,' Select Accounts - ao');
           	else
                 set_lov_property(lov_id,group_name,'all_acts_all_trans_old');
                 set_lov_property(lov_id,title,' Select Accounts - ato');
            end if;
         elsif account_rec.inactive_flag = 'Y' then
            set_application_property(cursor_style,'default');
            message('The Account You Have Selected is Currently Inactive');
            message(' ',no_acknowledge);
            lov_selected := show_lov(lov_id);
         end if;
         close account_no_cur;
      end if;
  else
      :global.account_id := null; -- dph added 8/18/05 but WHY is it failing!
      set_application_property(cursor_style,'default');
      lov_selected := show_lov(lov_id);
 
  end if;

  if :global.debug_status = 'ON' then
		 message('debug set_lov - set global.account_id to NULL ');
     message(' ',no_acknowledge);
  end if; 
  
  -- 03/10/06 KLY Ref No 31817 - set global.account_id to null if they Cancel out of the LOV
  if not lov_selected then
     :global.account_id := null;
  end if;

END;
		*/
        #endregion
        //ID:97
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void SetLov()
        {
            int rowCount = 0;
            // called when client is not using the new account securtiy
            NNumber groupStatus = NNumber.Null;
            NNumber groupCount = NNumber.Null;
            NBool lovSelected = NBool.Null;
            ValueSet groupId = null;
            Foundations.Core.AppSupportLib.Runtime.ControlsModel.Lovs.LovDescriptor lovId = null;
            String sqlaccountNoCur = "SELECT accounts.account_id, accounts.account_no, accounts.inactive_flag " +
    " FROM shr.accounts, shr.nick_accounts " +
    " WHERE nick_accounts.account_nick_no = @P_CUR_IN_ACCOUNT_NO AND accounts.account_id = nick_accounts.account_id AND (@GLOBAL_ACCOUNT_YEAR = accounts.account_year OR @GLOBAL_ACCOUNT_YEAR IS NULL) ";
            DataCursor accountNoCur = new DataCursor(sqlaccountNoCur);
            String accountNoCurCurInAccountNo = string.Empty;
            TableRow accountRec = null;
            String sqlaccountNoByIdCur = "SELECT accounts.account_no " +
    " FROM shr.accounts " +
    " WHERE account_id = @P_CUR_IN_ACCOUNT_ID ";
            DataCursor accountNoByIdCur = new DataCursor(sqlaccountNoByIdCur);
            NNumber accountNoByIdCurCurInAccountId = NNumber.Null;
            TableRow accountNoByIdRec = null;
            NNumber holdAccountId = NNumber.Null;
            #region
            try
            {
                lovId = LovServices.FindLov("accounts_lov");
                if (Alio.Common.Globals.DebugStatus == "ON")
                {
                    TaskServices.Message(" set lov act_year " + Alio.Common.Globals.AccountYear + " act_no " + Alio.Common.Globals.AccountNo + " qtsi_role_id " + Alio.Common.Globals.QtsiRoleId + " qtsi_user_id " + Alio.Common.Globals.QtsiUserId + " transaction_desc " + Alio.Common.Globals.TransactionDesc);
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                }
                if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                {
                    LovServices.SetGroupName(lovId, "all_acts_all_old");
                    LovServices.SetTitle(lovId, " Select Accounts - ao");
                }
                else
                {
                    LovServices.SetGroupName(lovId, "all_acts_all_trans_old");
                    LovServices.SetTitle(lovId, " Select Accounts - ato");
                }
                if (!string.IsNullOrEmpty(Alio.Common.Globals.AccountNo))
                {
                    accountNoCurCurInAccountNo = Alio.Common.Globals.AccountNo;
                    //Setting query parameters
                    accountNoCur.AddParameter("@P_CUR_IN_ACCOUNT_NO", accountNoCurCurInAccountNo);
                    accountNoCur.AddParameter("@GLOBAL_ACCOUNT_YEAR", Alio.Common.Globals.AccountYear);
                    accountNoCur.Open();
                    while (true)
                    {
                        accountRec = accountNoCur.FetchRow();
                        if (accountNoCur.Found)
                        {
                            holdAccountId = Amsec06b_.FindAccount(accountRec.GetNumber("account_no"), Alio.Common.Globals.AccountYear, Alio.Common.Globals.QtsiRoleId, Alio.Common.Globals.QtsiUserId);
                            if (holdAccountId > 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (Alio.Common.Globals.DebugStatus == "ON")
                    {
                        TaskServices.Message("debug set_lov hold_account_id " + holdAccountId.ToString());
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    }
                    if (accountNoCur.NotFound)
                    {
                        if (Alio.Common.Globals.DebugStatus == "ON")
                        {
                            TaskServices.Message("debug set_lov - account_no_cur%notfound");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        }
                        if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                        {
                            groupId = ValueSetServices.FindValueSet(AbstractTask.Current.Model, "all_acts_like_no_old");
                            groupStatus = ValueSetServices.PopulateValueSet(AbstractTask.Current.Model, groupId);
                            ;
                        }
                        else
                        {
                            groupId = ValueSetServices.FindValueSet(AbstractTask.Current.Model, "all_acts_like_no_trans_old");
                            groupStatus = ValueSetServices.PopulateValueSet(AbstractTask.Current.Model, groupId);
                            ;
                        }
                        if (Alio.Common.Globals.DebugStatus == "ON")
                        {
                            TaskServices.Message("debug set_lov - group_status " + groupStatus.ToString());
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        }
                        if (groupStatus != 0)
                        {
                            lovSelected = TaskServices.ShowLov(lovId);
                        }
                        else
                        {
                            groupCount = groupId.RowCount;
                            if (Alio.Common.Globals.DebugStatus == "ON")
                            {
                                TaskServices.Message("debug set_lov - group_count " + groupCount.ToString());
                                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                            }
                            if (groupCount == 1)
                            {
                                if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                                {
                                    Alio.Common.Globals.AccountId = ValueSetServices.GetNumberValue(AbstractTask.Current.Model, "all_acts_like_no_old.account_id", 1).ToString();
                                }
                                else
                                {
                                    //  global.transaction_desc is null
                                    Alio.Common.Globals.AccountId = ValueSetServices.GetNumberValue(AbstractTask.Current.Model, "all_acts_like_no_trans_old." + "account_id", 1).ToString();
                                }
                            }
                            else if (groupCount > 1)
                            {
                                if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                                {
                                    LovServices.SetGroupName(lovId, "all_acts_like_no_old");
                                    LovServices.SetTitle(lovId, " Select Accounts - alo");
                                }
                                else
                                {
                                    LovServices.SetGroupName(lovId, "all_acts_like_no_trans_old");
                                    LovServices.SetTitle(lovId, " Select Accounts - alto");
                                }
                                lovSelected = TaskServices.ShowLov(lovId);
                                if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                                {
                                    LovServices.SetGroupName(lovId, "all_acts_all_old");
                                    LovServices.SetTitle(lovId, " Select Accounts - ao");
                                }
                                else
                                {
                                    LovServices.SetGroupName(lovId, "all_acts_all_trans_old");
                                    LovServices.SetTitle(lovId, " Select Accounts - ato");
                                }
                            }
                        }
                    }
                    else
                    {
                        //  global account_no is not null
                        Alio.Common.Globals.AccountId = accountRec.GetNumber("account_id").ToString();
                        while (true)
                        {
                            accountRec = accountNoCur.FetchRow();
                            if (accountNoCur.Found)
                            {
                                holdAccountId = Amsec06b_.FindAccount(accountRec.GetNumber("account_no"), Alio.Common.Globals.AccountYear, Alio.Common.Globals.QtsiRoleId, Alio.Common.Globals.QtsiUserId);
                                if (!holdAccountId.IsNull)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (accountNoCur.Found)
                        {
                            if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                            {
                                LovServices.SetGroupName(lovId, "all_acts_by_no_old");
                                LovServices.SetTitle(lovId, " Select Accounts - abo");
                            }
                            else
                            {
                                LovServices.SetGroupName(lovId, "all_acts_by_no_trans_old");
                                LovServices.SetTitle(lovId, " Select Accounts - abto");
                            }
                            lovSelected = TaskServices.ShowLov(lovId);
                            if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                            {
                                LovServices.SetGroupName(lovId, "all_acts_all_old");
                                LovServices.SetTitle(lovId, " Select Accounts - ao");
                            }
                            else
                            {
                                LovServices.SetGroupName(lovId, "all_acts_all_trans_old");
                                LovServices.SetTitle(lovId, " Select Accounts - ato");
                            }
                        }
                        else if (accountRec.GetNumber("inactive_flag") == "Y")
                        {
                            TaskServices.Message("The Account You Have Selected is Currently Inactive");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                            lovSelected = TaskServices.ShowLov(lovId);
                        }
                    }
                }
                else
                {
                    Alio.Common.Globals.AccountId = null;
                    //  dph added 8/18/05 but WHY is it failing!
                    lovSelected = TaskServices.ShowLov(lovId);
                }
                if (Alio.Common.Globals.DebugStatus == "ON")
                {
                    TaskServices.Message("debug set_lov - set global.account_id to NULL ");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                }
                //  03/10/06 KLY Ref No 31817 - set global.account_id to null if they Cancel out of the LOV
                if (!(lovSelected))
                {
                    Alio.Common.Globals.AccountId = null;
                }
            }
            finally
            {
                accountNoCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit CHANGE_HISTORY
        /*
		<multilinecomment>
 4/22/08 PS- added change_history program unit to form.
 4/22/08 PS- changed the width, height, x and y position on the window, so it does not show the window in 10g.
 09/05/12	        NJA		Alio-307	Checked and if necessary changed the variable sizes/types of batch_no, batch_year, account_id, account_no, po_no, reference_no, vendor_no, check_key, check_no
 07/16/13     RDH    alio-9113  Version 14.1 Removed all upper function(s) from the account_nick_no in the record groups and the pre-query. Removed
                                Decode from all user roles.
 09/04/13	cec		alio-9113		14.1		-changed the record groups from using inline comments to using block comments
 06/12/14 eag   alio-????   15.0.1  -- changed all the record groups to have 50 rpad for accts
 11/19/14	cec		alio-12030	15.1.3	-changed record groups that use transaction descriptions in them to look at transaction_description.diplay_account flag and debit_credit flag
 08/27/15 dph   alio-13195  16.1    added transaction driver to join and removed exists, added sort by seq_no
                                    remove the exists because I was getting an unable to parse form error a form compilation ( not rg save) 
                                    added max and group by for duplicate rows 
 01/07/16 dph   alio-13545  16.3   Remove join by acct_type_in = acct_type_out;  restore legacy record groups to pre-13195 version
 01/14/16 dph   alio-13545  16.3   The legacy record groups need the seq no
</multilinecomment>

PROCEDURE change_history IS
BEGIN
  null;
END;
		*/
        #endregion
        //ID:98
        /// <summary></summary>
        /// <remarks>
        /// 4/22/08 PS- added change_history program unit to form.
        /// 4/22/08 PS- changed the width, height, x and y position on the window, so it does not show the window in 10g.
        /// 09/05/12	        NJA		Alio-307	Checked and if necessary changed the variable sizes/types of batch_no, batch_year, account_id, account_no, po_no, reference_no, vendor_no, check_key, check_no
        /// 07/16/13     RDH    alio-9113  Version 14.1 Removed all upper function(s) from the account_nick_no in the record groups and the pre-query. Removed
        /// Decode from all user roles.
        /// 09/04/13	cec		alio-9113		14.1		-changed the record groups from using inline comments to using block comments
        /// 06/12/14 eag   alio-????   15.0.1  -- changed all the record groups to have 50 rpad for accts
        /// 11/19/14	cec		alio-12030	15.1.3	-changed record groups that use transaction descriptions in them to look at transaction_description.diplay_account flag and debit_credit flag
        /// 08/27/15 dph   alio-13195  16.1    added transaction driver to join and removed exists, added sort by seq_no
        /// remove the exists because I was getting an unable to parse form error a form compilation ( not rg save)
        /// added max and group by for duplicate rows
        /// 01/07/16 dph   alio-13545  16.3   Remove join by acct_type_in = acct_type_out;  restore legacy record groups to pre-13195 version
        /// 01/14/16 dph   alio-13545  16.3   The legacy record groups need the seq no
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void ChangeHistory()
        {
        }




    }
}
