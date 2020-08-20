using Alio.Common;
using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Forms.Slact01a.Model;
using Alio.Libraries.Sutl01a;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Task;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Slact01a.Controllers
{

    public class Slact01aTaskController : AlioBaseTaskController<Slact01aTask, Slact01aModel>
    {

        public Slact01aTaskController(Slact01aTask task) : base(task)
        {
        }



        #region event handlers generated from Forms triggers


        #region Original PL/SQL code code for TRIGGER SLACT01A.WHEN-NEW-FORM-INSTANCE
        /*
         DECLARE

group_status  number;
group_count   number;
v_count       number;
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
  and accounts.account_id = nick_accounts.account_id
  and (:global.account_year = accounts.account_year
       or :global.account_year is null);
account_rec account_no_cur%rowtype;

cursor account_no_by_id_cur (cur_in_account_id in number) is
select accounts.account_no
 from shr.accounts
where account_id = cur_in_account_id;
account_no_by_id_rec account_no_by_id_cur%rowtype;

hold_account_id shr.accounts.account_id%type;

v_exempt_flag adm.users.account_security_exempt%type := 'N';

BEGIN

sutl01a.when_new_form_instance;
lov_id := find_lov('accounts_lov');
default_value('*','global.transaction_desc');

 default_value( '*', 'global.debit_credit_flag');  --cec alio-12030  -only used for certain accounting practices in order to hide some accounts
 if :global.debit_credit_flag = '*' then --cec alio-12030
    :global.debit_credit_flag := null;
 end if;

     -- set debugging globals
default_value('*','global.debug_status');
default_value('*','global.write_log_file');

if :global.transaction_desc = '*' then
  :global.transaction_desc := null;
end if;

select count(1) 
into v_count
from shr.account_security_mask;


IF  v_count = 0 then

set_lov;

ELSE


if  :global.qtsi_user_id in ('ADM','FAS','HRS','SHR','TEA','WHS') then
  v_exempt_flag := 'Y';
else
 select max(nvl(account_security_exempt,'N'))
   into v_exempt_flag 
   from adm.users
  where user_id = :global.qtsi_user_id;
end if;   

<multilinecomment>
message('dph act_year '||:global.account_year||
        ' act_no '||:global.account_no||
        ' qtsi_role_id '||:global.qtsi_role_id||
        ' qtsi_user_id '||:global.qtsi_user_id||
        ' transaction_desc '||:global.transaction_desc||
        ' v_exempt_flag '||v_exempt_flag);
message(' ',no_acknowledge);
</multilinecomment>

         if :global.debug_status = 'ON' then
          message('debug scla01a v_exempt_flag '||v_exempt_flag||
                 ', global.transaction_desc '||:global.transaction_desc||
                 ', global.debit_credit '||:global.debit_credit_flag);
        message(' ',no_acknowledge);
         end if;

if :global.transaction_desc is null then 
 if  v_exempt_flag <> 'Y' then
   set_lov_property(lov_id,group_name,'all_accounts_all');
   set_lov_property(lov_id,title,' Select Accounts - aa');
 else
   set_lov_property(lov_id,group_name,'all_accounts_all_mstr');
   set_lov_property(lov_id,title,' Select Accounts - aam');
 end if;
else
    if  v_exempt_flag <> 'Y' then
    set_lov_property(lov_id,group_name,'all_accounts_all_trans');
    set_lov_property(lov_id,title,' Select Accounts - aat');
    else
      set_lov_property(lov_id,group_name,'all_accounts_all_trans_mstr');
      set_lov_property(lov_id,title,' Select Accounts - aatm');
    end if;
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
          message('debug scla01a hold_account_id '||hold_account_id);
        message(' ',no_acknowledge);
         end if;

if account_no_cur%notfound then

     if :global.debug_status = 'ON' then
           message('debug scla01a - account_no_cur%notfound');
         message(' ',no_acknowledge);
     end if;

     close account_no_cur;
     if :global.transaction_desc is null then
          if  v_exempt_flag <> 'Y' then
          group_id := find_group('all_accounts_like_no');
          else
            group_id := find_group('all_accounts_like_no_mstr');
          end if;	

         group_status := populate_group(group_id);
     else
         if v_exempt_flag <> 'Y' then 	 	
           group_id := find_group('all_accounts_like_no_trans');
         else
            group_id := find_group('all_accounts_like_no_trans_mst');
           end if;	
        group_status := populate_group(group_id);
     end if;

      if :global.debug_status = 'ON' then
           message('debug scla01a - group_status '||group_status);
         message(' ',no_acknowledge);
     end if;   

    if group_status <> 0 then
        set_application_property(cursor_style,'default');
        lov_selected := show_lov(lov_id);
    else
        group_count := get_group_row_count(group_id);

        if :global.debug_status = 'ON' then
              message('debug scla01a - group_count '||group_count);
            message(' ',no_acknowledge);
        end if; 

        if group_count = 1 then
           if :global.transaction_desc is null then
                if v_exempt_flag <> 'Y' then 
                :global.account_id :=
                    get_group_number_cell('all_accounts_like_no.account_id',
                                          1);
                else
                 :global.account_id :=
                    get_group_number_cell('all_accounts_like_no_mstr.account_id',
                                          1);
              end if; 		

           else -- global.transaction_desc is null
              if v_exempt_flag <> 'Y' then     		
              :global.account_id :=
                 get_group_number_cell('all_accounts_like_no_trans.'
                                    || 'account_id',
                                       1);
              else
                 :global.account_id :=
                 get_group_number_cell('all_accounts_like_no_trans_mst.'
                                    || 'account_id',
                                       1);
              end if;	

           end if;-- :global.transaction_desc is null

        elsif group_count > 1 then

           set_lov_property(lov_id,
                            auto_refresh,
                            property_false);
           if :global.transaction_desc is null then
                if v_exempt_flag <> 'Y' then 
                 set_lov_property(lov_id,group_name,'all_accounts_like_no');
                 set_lov_property(lov_id,title,' Select Accounts - aal');
                else
                     set_lov_property(lov_id,group_name,'all_accounts_like_no_mstr');
                     set_lov_property(lov_id,title,' Select Accounts - aalm'); 
                end if;	
           else
                if v_exempt_flag <> 'Y' then	
                set_lov_property(lov_id,group_name,'all_accounts_like_no_trans');
                set_lov_property(lov_id,title,' Select Accounts - aalt');
              else
                set_lov_property(lov_id,group_name,'all_accounts_like_no_trans_mst');	
                set_lov_property(lov_id,title,' Select Accounts - aaltm');  
              end if;
           end if;

           set_application_property(cursor_style,'default');
           lov_selected := show_lov(lov_id);

            if :global.transaction_desc is null then
              if v_exempt_flag <> 'Y' then	
                set_lov_property(lov_id,group_name,'all_accounts_all');
                set_lov_property(lov_id,title,' Select Accounts - aa');
              else
                set_lov_property(lov_id,group_name,'all_accounts_all_mstr');
                set_lov_property(lov_id,title,' Select Accounts - aam');
              end if;
            else
              if v_exempt_flag <> 'Y' then	 	
                set_lov_property(lov_id,group_name,'all_accounts_all_trans');
                set_lov_property(lov_id,title,' Select Accounts - aat');
              else
                set_lov_property(lov_id,group_name,'all_accounts_all_trans_mstr');
                set_lov_property(lov_id,title,' Select Accounts - aatmr');
              end if;
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
             if v_exempt_flag <> 'Y' then
             set_lov_property(lov_id,group_name,'all_accounts_by_no');
             set_lov_property(lov_id,title,' Select Accounts - aab');
             else
                 set_lov_property(lov_id,group_name,'all_accounts_by_no_mstr');
                 set_lov_property(lov_id,title,' Select Accounts - aabm');
             end if;
        else
          if v_exempt_flag <> 'Y' then	 	
             set_lov_property(lov_id,group_name,'all_accounts_by_no_trans');
             set_lov_property(lov_id,title,' Select Accounts - aabt');
          else
             set_lov_property(lov_id,group_name,'all_accounts_by_no_trans_mstr');
             set_lov_property(lov_id,title,' Select Accounts - aabtm');
          end if;
        end if;

        set_application_property(cursor_style,'default');
        lov_selected := show_lov(lov_id);
        if :global.transaction_desc is null then
            if v_exempt_flag <> 'Y' then
            set_lov_property(lov_id,group_name,'all_accounts_all');
            set_lov_property(lov_id,title,' Select Accounts - aa');
            else
                set_lov_property(lov_id,group_name,'all_accounts_all_mstr');
                set_lov_property(lov_id,title,' Select Accounts - aam');
            end if;
        else
           if v_exempt_flag <> 'Y' then 		
             set_lov_property(lov_id,group_name,'all_accounts_all_trans');
             set_lov_property(lov_id,title,' Select Accounts - aat');
           else
             set_lov_property(lov_id,group_name,'all_accounts_all_trans_mstr');
             set_lov_property(lov_id,title,' Select Accounts - aalltm');	
           end if;
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

END IF; -- old or new security  

erase('global.transaction_desc');

if not lov_selected then
  :global.account_id := null;
  exit_form;
end if;

if :global.debug_status = 'ON' then
  message('Account ID = '||:global.account_id);message(' ',no_Acknowledge);
end if;

open account_no_by_id_cur(to_number(:global.account_id));
fetch account_no_by_id_cur into :global.account_no;
close account_no_by_id_cur;

exit_form;

END;

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:263
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>SLACT01A.WHEN-NEW-FORM-INSTANCE</TriggerInfo>

        [TaskStarted]
        public virtual void Slact01a_TaskStarted(System.EventArgs args)
        {

            int rowCount = 0;
            {
                NNumber groupStatus = NNumber.Null;
                NNumber groupCount = NNumber.Null;
                NNumber vCount = NNumber.Null;
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
                NString vExemptFlag = "N";
                #region
                try
                {
                    Task.Libraries.Sutl01a.Sutl01a.WhenNewFormInstance();
                    lovId = LovServices.FindLov("accounts_lov");
                    Globals.SetDefault("global.transaction_desc", "*");
                    Globals.SetDefault("global.debit_credit_flag", "*");
                    // cec alio-12030  -only used for certain accounting practices in order to hide some accounts
                    if (Alio.Common.Globals.DebitCreditFlag == "*")
                    {
                        // cec alio-12030
                        Alio.Common.Globals.DebitCreditFlag = null;
                    }
                    //  set debugging globals
                    Globals.SetDefault("global.debug_status", "*");
                    Globals.SetDefault("global.write_log_file", "*");
                    if (Alio.Common.Globals.TransactionDesc == "*")
                    {
                        Alio.Common.Globals.TransactionDesc = null;
                    }
                    #region Execute data command
                    String sql1 = "SELECT count(1) " +
" FROM shr.account_security_mask ";
                    DataCommand command1 = new DataCommand(sql1);
                    ResultSet results1 = command1.ExecuteQuery();
                    rowCount = command1.RowCount;
                    if (results1.HasData)
                    {
                        vCount = results1.GetNumber(0);
                    }
                    results1.Close();
                    #endregion
                    if (vCount == 0)
                    {
                        this.Task.Services.SetLov();
                    }
                    else
                    {
                        if ((Alio.Common.Globals.QtsiUserId == "ADM" || Alio.Common.Globals.QtsiUserId == "FAS" || Alio.Common.Globals.QtsiUserId == "HRS" || Alio.Common.Globals.QtsiUserId == "SHR" || Alio.Common.Globals.QtsiUserId == "TEA" || Alio.Common.Globals.QtsiUserId == "WHS"))
                        {
                            vExemptFlag = "Y";
                        }
                        else
                        {
                            #region Execute data command
                            String sql2 = "SELECT max(nvl(account_security_exempt, 'N')) " +
" FROM adm.users " +
" WHERE user_id = @GLOBAL_QTSI_USER_ID ";
                            DataCommand command2 = new DataCommand(sql2);
                            //Setting query parameters
                            command2.AddParameter("@GLOBAL_QTSI_USER_ID", Alio.Common.Globals.QtsiUserId);
                            ResultSet results2 = command2.ExecuteQuery();
                            rowCount = command2.RowCount;
                            if (results2.HasData)
                            {
                                vExemptFlag = results2.GetStr(0);
                            }
                            results2.Close();
                            #endregion
                        }
                        // message('dph act_year '||:global.account_year||
                        // ' act_no '||:global.account_no||
                        // ' qtsi_role_id '||:global.qtsi_role_id||
                        // ' qtsi_user_id '||:global.qtsi_user_id||
                        // ' transaction_desc '||:global.transaction_desc||
                        // ' v_exempt_flag '||v_exempt_flag);
                        // message(' ',no_acknowledge);
                        if (Alio.Common.Globals.DebugStatus == "ON")
                        {
                            TaskServices.Message("debug scla01a v_exempt_flag " + vExemptFlag.ToString().ToString() + ", global.transaction_desc " + Alio.Common.Globals.TransactionDesc + ", global.debit_credit " + Alio.Common.Globals.DebitCreditFlag);
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                        }
                        if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                        {
                            if (vExemptFlag != "Y")
                            {
                                LovServices.SetGroupName(lovId, "all_accounts_all");
                                LovServices.SetTitle(lovId, " Select Accounts - aa");
                            }
                            else
                            {
                                LovServices.SetGroupName(lovId, "all_accounts_all_mstr");
                                LovServices.SetTitle(lovId, " Select Accounts - aam");
                            }
                        }
                        else
                        {
                            if (vExemptFlag != "Y")
                            {
                                LovServices.SetGroupName(lovId, "all_accounts_all_trans");
                                LovServices.SetTitle(lovId, " Select Accounts - aat");
                            }
                            else
                            {
                                LovServices.SetGroupName(lovId, "all_accounts_all_trans_mstr");
                                LovServices.SetTitle(lovId, " Select Accounts - aatm");
                            }
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
                                    holdAccountId = Amsec06b_.FindAccount(accountRec.GetInt32("account_no"), Alio.Common.Globals.AccountYear, Alio.Common.Globals.QtsiRoleId, Alio.Common.Globals.QtsiUserId);
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
                                TaskServices.Message("debug scla01a hold_account_id " + holdAccountId.ToString());
                                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                            }
                            if (accountNoCur.NotFound)
                            {
                                if (Alio.Common.Globals.DebugStatus == "ON")
                                {
                                    TaskServices.Message("debug scla01a - account_no_cur%notfound");
                                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                                }
                                if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                                {
                                    if (vExemptFlag != "Y")
                                    {
                                        groupId = ValueSetServices.FindValueSet(AbstractTask.Current.Model, "all_accounts_like_no");
                                    }
                                    else
                                    {
                                        groupId = ValueSetServices.FindValueSet(AbstractTask.Current.Model, "all_accounts_like_no_mstr");
                                    }
                                    groupStatus = ValueSetServices.PopulateValueSet(AbstractTask.Current.Model, groupId);
                                    ;
                                }
                                else
                                {
                                    if (vExemptFlag != "Y")
                                    {
                                        groupId = ValueSetServices.FindValueSet(AbstractTask.Current.Model, "all_accounts_like_no_trans");
                                    }
                                    else
                                    {
                                        groupId = ValueSetServices.FindValueSet(AbstractTask.Current.Model, "all_accounts_like_no_trans_mst");
                                    }
                                    groupStatus = ValueSetServices.PopulateValueSet(AbstractTask.Current.Model, groupId);
                                    ;
                                }
                                if (Alio.Common.Globals.DebugStatus == "ON")
                                {
                                    TaskServices.Message("debug scla01a - group_status " + groupStatus.ToString());
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
                                        TaskServices.Message("debug scla01a - group_count " + groupCount.ToString());
                                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                                    }
                                    if (groupCount == 1)
                                    {
                                        if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                                        {
                                            if (vExemptFlag != "Y")
                                            {
                                                Alio.Common.Globals.AccountId = ValueSetServices.GetNumberValue(AbstractTask.Current.Model, "all_accounts_like_no.account_id", 1).ToString();
                                            }
                                            else
                                            {
                                                Alio.Common.Globals.AccountId = ValueSetServices.GetNumberValue(AbstractTask.Current.Model, "all_accounts_like_no_mstr.account_id", 1).ToString();
                                            }
                                        }
                                        else
                                        {
                                            //  global.transaction_desc is null
                                            if (vExemptFlag != "Y")
                                            {
                                                Alio.Common.Globals.AccountId = ValueSetServices.GetNumberValue(AbstractTask.Current.Model, "all_accounts_like_no_trans." + "account_id", 1).ToString();
                                            }
                                            else
                                            {
                                                Alio.Common.Globals.AccountId = ValueSetServices.GetNumberValue(AbstractTask.Current.Model, "all_accounts_like_no_trans_mst." + "account_id", 1).ToString();
                                            }
                                        }
                                    }
                                    else if (groupCount > 1)
                                    {
                                        if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                                        {
                                            if (vExemptFlag != "Y")
                                            {
                                                LovServices.SetGroupName(lovId, "all_accounts_like_no");
                                                LovServices.SetTitle(lovId, " Select Accounts - aal");
                                            }
                                            else
                                            {
                                                LovServices.SetGroupName(lovId, "all_accounts_like_no_mstr");
                                                LovServices.SetTitle(lovId, " Select Accounts - aalm");
                                            }
                                        }
                                        else
                                        {
                                            if (vExemptFlag != "Y")
                                            {
                                                LovServices.SetGroupName(lovId, "all_accounts_like_no_trans");
                                                LovServices.SetTitle(lovId, " Select Accounts - aalt");
                                            }
                                            else
                                            {
                                                LovServices.SetGroupName(lovId, "all_accounts_like_no_trans_mst");
                                                LovServices.SetTitle(lovId, " Select Accounts - aaltm");
                                            }
                                        }
                                        lovSelected = TaskServices.ShowLov(lovId);
                                        if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                                        {
                                            if (vExemptFlag != "Y")
                                            {
                                                LovServices.SetGroupName(lovId, "all_accounts_all");
                                                LovServices.SetTitle(lovId, " Select Accounts - aa");
                                            }
                                            else
                                            {
                                                LovServices.SetGroupName(lovId, "all_accounts_all_mstr");
                                                LovServices.SetTitle(lovId, " Select Accounts - aam");
                                            }
                                        }
                                        else
                                        {
                                            if (vExemptFlag != "Y")
                                            {
                                                LovServices.SetGroupName(lovId, "all_accounts_all_trans");
                                                LovServices.SetTitle(lovId, " Select Accounts - aat");
                                            }
                                            else
                                            {
                                                LovServices.SetGroupName(lovId, "all_accounts_all_trans_mstr");
                                                LovServices.SetTitle(lovId, " Select Accounts - aatmr");
                                            }
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
                                        if (vExemptFlag != "Y")
                                        {
                                            LovServices.SetGroupName(lovId, "all_accounts_by_no");
                                            LovServices.SetTitle(lovId, " Select Accounts - aab");
                                        }
                                        else
                                        {
                                            LovServices.SetGroupName(lovId, "all_accounts_by_no_mstr");
                                            LovServices.SetTitle(lovId, " Select Accounts - aabm");
                                        }
                                    }
                                    else
                                    {
                                        if (vExemptFlag != "Y")
                                        {
                                            LovServices.SetGroupName(lovId, "all_accounts_by_no_trans");
                                            LovServices.SetTitle(lovId, " Select Accounts - aabt");
                                        }
                                        else
                                        {
                                            LovServices.SetGroupName(lovId, "all_accounts_by_no_trans_mstr");
                                            LovServices.SetTitle(lovId, " Select Accounts - aabtm");
                                        }
                                    }
                                    lovSelected = TaskServices.ShowLov(lovId);
                                    if (string.IsNullOrEmpty(Alio.Common.Globals.TransactionDesc))
                                    {
                                        if (vExemptFlag != "Y")
                                        {
                                            LovServices.SetGroupName(lovId, "all_accounts_all");
                                            LovServices.SetTitle(lovId, " Select Accounts - aa");
                                        }
                                        else
                                        {
                                            LovServices.SetGroupName(lovId, "all_accounts_all_mstr");
                                            LovServices.SetTitle(lovId, " Select Accounts - aam");
                                        }
                                    }
                                    else
                                    {
                                        if (vExemptFlag != "Y")
                                        {
                                            LovServices.SetGroupName(lovId, "all_accounts_all_trans");
                                            LovServices.SetTitle(lovId, " Select Accounts - aat");
                                        }
                                        else
                                        {
                                            LovServices.SetGroupName(lovId, "all_accounts_all_trans_mstr");
                                            LovServices.SetTitle(lovId, " Select Accounts - aalltm");
                                        }
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
                    }
                    //  old or new security  
                    Globals.Remove("global.transaction_desc");
                    if (!(lovSelected))
                    {
                        Alio.Common.Globals.AccountId = null;
                        TaskServices.ExitTask();
                    }
                    if (Alio.Common.Globals.DebugStatus == "ON")
                    {
                        TaskServices.Message("Account ID = " + Alio.Common.Globals.AccountId);
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    }
                    accountNoByIdCurCurInAccountId = Lib.ToNumber(Alio.Common.Globals.AccountId);
                    //Setting query parameters
                    accountNoByIdCur.AddParameter("@P_CUR_IN_ACCOUNT_ID", accountNoByIdCurCurInAccountId);
                    accountNoByIdCur.Open();
                    ResultSet accountNoByIdCurResults = accountNoByIdCur.FetchInto();
                    if (accountNoByIdCurResults != null)
                    {
                        Alio.Common.Globals.AccountNo = accountNoByIdCurResults.GetString(0);
                    }
                    TaskServices.ExitTask();
                }
                finally
                {
                    accountNoCur.Close();
                    accountNoByIdCur.Close();
                }
                #endregion
            }
        }



        #endregion

    }

}
