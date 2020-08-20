using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjgen01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.AppSupportLib.UI;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fjgen01a.Controllers
{


    public class JournalsController : BaseBlockController<Fjgen01aTask, Fjgen01aModel>
    {

        public JournalsController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER JOURNALS.POST-QUERY
        /*
         declare 
cursor account_info_cur is 
select account_year,
      account_no,
      account_desc,
      account_type,
      account_id
from shr.accounts
where account_id = :journals.account_id;
account_info_rec account_info_cur%rowtype;
begin


open account_info_cur;
fetch account_info_cur into account_info_rec;
close account_info_cur;

:journals.account_year := account_info_rec.account_year;
:journals.account_no := account_info_rec.account_no;
:journals.account_desc := account_info_rec.account_desc;
:journals.account_type := account_info_rec.account_type;
end; 
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:36
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JOURNALS.POST-QUERY</TriggerInfo>

        [AfterQuery]
        public virtual void journals_AfterQuery(RowAdapterEventArgs args)
        {
            JournalsAdapter journalsElement = args.Row as JournalsAdapter;


            int rowCount = 0;
            {
                String sqlaccountInfoCur = "SELECT account_year, account_no, account_desc, account_type, account_id " +
" FROM shr.accounts " +
" WHERE account_id = @JOURNALS_ACCOUNT_ID ";
                DataCursor accountInfoCur = new DataCursor(sqlaccountInfoCur);
                TableRow accountInfoRec = null;
                #region
                try
                {
                    //Setting query parameters
                    accountInfoCur.AddParameter("@JOURNALS_ACCOUNT_ID", journalsElement.AccountId);
                    accountInfoCur.Open();
                    accountInfoRec = accountInfoCur.FetchRow();
                    journalsElement.AccountYear = accountInfoRec.GetNumber("account_year").ToString();
                    journalsElement.AccountNo = accountInfoRec.GetStr("account_no");
                    journalsElement.AccountDesc = accountInfoRec.GetStr("account_desc");
                    journalsElement.AccountType = accountInfoRec.GetStr("account_type");
                }
                finally
                {
                    accountInfoCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER JOURNALS.PRE-QUERY
        /*
         declare 

cursor nick_account_cursor is 
select account_id 
  from shr.nick_accounts
 where :journals.account_no = account_nick_no
  and exists (select account_id --oel alio-847 restrict by acct yr
                    from shr.accounts
                    where account_year= :journals.account_year
                       and accounts.account_id = nick_accounts.account_id);

nick_account_rec nick_account_cursor%rowtype;

cursor account_info_cur is 
select account_year,
      account_no,
      account_desc,
      account_type,
      account_id
from shr.accounts
where account_id = :journals.account_id;
account_info_rec account_info_cur%rowtype;

begin

open nick_account_cursor;
fetch nick_account_cursor into nick_account_rec;
if nick_account_cursor%found then
    :journals.account_id := nick_account_rec.account_id;
end if;
close nick_account_cursor;


 open account_info_cur;
fetch account_info_cur into account_info_rec;
close account_info_cur;

:journals.account_year := account_info_rec.account_year;
:journals.account_no := account_info_rec.account_no;
:journals.account_desc := account_info_rec.account_desc;
:journals.account_type := account_info_rec.account_type;


    set_record_property(:system.trigger_record,
                      'journals',
                      status,
                      query_status);
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:37
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>JOURNALS.PRE-QUERY</TriggerInfo>

        [BeforeQuery]
        public virtual void journals_BeforeQuery(QueryEventArgs args)
        {
            JournalsAdapter journalsElement = this.Model.Journals.CurrentRowAdapter;


            int rowCount = 0;
            {
                String sqlnickAccountCursor = "SELECT account_id " +
" FROM shr.nick_accounts " +
" WHERE @JOURNALS_ACCOUNT_NO = account_nick_no AND  EXISTS(SELECT account_id " +
    " FROM shr.accounts " +
    " WHERE account_year = @JOURNALS_ACCOUNT_YEAR AND accounts.account_id = nick_accounts.account_id ) ";
                DataCursor nickAccountCursor = new DataCursor(sqlnickAccountCursor);
                TableRow nickAccountRec = null;
                String sqlaccountInfoCur = "SELECT account_year, account_no, account_desc, account_type, account_id " +
" FROM shr.accounts " +
" WHERE account_id = @JOURNALS_ACCOUNT_ID ";
                DataCursor accountInfoCur = new DataCursor(sqlaccountInfoCur);
                TableRow accountInfoRec = null;
                #region
                try
                {
                    //Setting query parameters
                    nickAccountCursor.AddParameter("@JOURNALS_ACCOUNT_NO", journalsElement.AccountNo);
                    nickAccountCursor.AddParameter("@JOURNALS_ACCOUNT_YEAR", journalsElement.AccountYear);
                    nickAccountCursor.Open();
                    nickAccountRec = nickAccountCursor.FetchRow();
                    if (nickAccountCursor.Found)
                    {
                        journalsElement.AccountId = nickAccountRec.GetNumber("account_id");
                    }
                    //Setting query parameters
                    accountInfoCur.AddParameter("@JOURNALS_ACCOUNT_ID", journalsElement.AccountId);
                    accountInfoCur.Open();
                    accountInfoRec = accountInfoCur.FetchRow();
                    journalsElement.AccountYear = accountInfoRec.GetNumber("account_year").ToString();
                    journalsElement.AccountNo = accountInfoRec.GetStr("account_no");
                    journalsElement.AccountDesc = accountInfoRec.GetStr("account_desc");
                    journalsElement.AccountType = accountInfoRec.GetStr("account_type");
                    BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "journals", RecordStatus.QUERY);
                }
                finally
                {
                    nickAccountCursor.Close();
                    accountInfoCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER DUP_REC.WHEN-BUTTON-PRESSED
        /*
         begin
do_key('duplicate_record');
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:11
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>DUP_REC.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "DUP_REC")]
        public virtual void dupRec_buttonClick()
        {
            TaskServices.ExecuteAction("DUPLICATE_RECORD");
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.WHEN-VALIDATE-ITEM
        /*
         declare 

cursor nick_account_cursor is 
select account_id 
  from shr.nick_accounts
 where :journals.account_no = account_nick_no
  and exists (select account_id --oel alio-847 restrict by acct yr
                    from shr.accounts
                    where account_year= :journals.account_year
                       and accounts.account_id = nick_accounts.account_id);

nick_account_rec nick_account_cursor%rowtype;

cursor account_info_cur is 
select account_no,
      account_desc,
      account_type
from shr.accounts
where account_id = :journals.account_id;
account_info_rec account_info_cur%rowtype;

begin
open nick_account_cursor;
fetch nick_account_cursor into nick_account_rec;
if nick_account_cursor%found then
if :journals.account_id <> nick_account_rec.account_id then

      :journals.account_id := nick_account_rec.account_id;

    open account_info_cur;
  fetch account_info_cur into account_info_rec;
  close account_info_cur;
  :journals.account_no := account_info_rec.account_no;
  :journals.account_desc := account_info_rec.account_desc;
  :journals.account_type := account_info_rec.account_type;
else
   set_record_property(:system.trigger_record,
                        'journals',
                        status,
                        query_status);
end if;


end if;
close nick_account_cursor;

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
        /// <TriggerInfo>ACCOUNT_NO.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "ACCOUNT_NO")]
        public virtual void accountNo_validate()
        {
            JournalsAdapter journalsElement = this.Model.Journals.CurrentRowAdapter;


            int rowCount = 0;
            {
                String sqlnickAccountCursor = "SELECT account_id " +
" FROM shr.nick_accounts " +
" WHERE @JOURNALS_ACCOUNT_NO = account_nick_no AND  EXISTS(SELECT account_id " +
    " FROM shr.accounts " +
    " WHERE account_year = @JOURNALS_ACCOUNT_YEAR AND accounts.account_id = nick_accounts.account_id ) ";
                DataCursor nickAccountCursor = new DataCursor(sqlnickAccountCursor);
                TableRow nickAccountRec = null;
                String sqlaccountInfoCur = "SELECT account_no, account_desc, account_type " +
" FROM shr.accounts " +
" WHERE account_id = @JOURNALS_ACCOUNT_ID ";
                DataCursor accountInfoCur = new DataCursor(sqlaccountInfoCur);
                TableRow accountInfoRec = null;
                #region
                try
                {
                    //Setting query parameters
                    nickAccountCursor.AddParameter("@JOURNALS_ACCOUNT_NO", journalsElement.AccountNo);
                    nickAccountCursor.AddParameter("@JOURNALS_ACCOUNT_YEAR", journalsElement.AccountYear);
                    nickAccountCursor.Open();
                    nickAccountRec = nickAccountCursor.FetchRow();
                    if (nickAccountCursor.Found)
                    {
                        if (journalsElement.AccountId != nickAccountRec.GetNumber("account_id"))
                        {
                            journalsElement.AccountId = nickAccountRec.GetNumber("account_id");
                            //Setting query parameters
                            accountInfoCur.AddParameter("@JOURNALS_ACCOUNT_ID", journalsElement.AccountId);
                            accountInfoCur.Open();
                            accountInfoRec = accountInfoCur.FetchRow();
                            journalsElement.AccountNo = accountInfoRec.GetStr("account_no");
                            journalsElement.AccountDesc = accountInfoRec.GetStr("account_desc");
                            journalsElement.AccountType = accountInfoRec.GetStr("account_type");
                        }
                        else
                        {
                            BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "journals", RecordStatus.QUERY);
                        }
                    }
                }
                finally
                {
                    nickAccountCursor.Close();
                    accountInfoCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNT_NO.KEY-LISTVAL
        /*
         DECLARE

batch_year     fas.batches.batch_year%type  := :journals.batch_year;   -- nja alio 307     -- Batch/Account Year
bank_no        number(2)    := NULL;                            -- Bank Number OR NULL
trans_desc     varchar2(8)  := 'ALL';                           -- TRANSACTION_DESCRIPTION
debit_credit   number(2)    := NULL;                            -- ( +1, 0, -1)
send_status    varchar2(1)  := '1';                             -- (1)Primary Acct, (2)Secondary Acct, (3)Both 
call_from      varchar2(1)  := 'L';                             -- (L)OV, (T)rigger, (V)alidate
hold_null      varchar2(1);                                     -- Used for Return of only one account
hold_acct_no   shr.accounts.account_no%type;  -- nja alio 307
hold_acct_id   shr.accounts.account_id%type;  -- nja alio 307

BEGIN

if :journals.batch_year is null then
  message('Please select a Batch Year.');
  message(' ',no_Acknowledge);
  raise form_trigger_failure;
end if;

if :system.mode = 'NORMAL' then
  do_key('enter_query');
end if;

if :journals.account_no is not null then
  hold_acct_no := :journals.account_no;
  clear_block;
end if;

validate_account.load_values(batch_year,
                            bank_no,              
                            trans_desc,
                            debit_credit,
                            send_status,
                            call_from,
                            hold_acct_no,                  --:BLOCK_NAME.<Primary Account NO>
                            hold_acct_id,                  --:BLOCK_NAME.<Primary Account ID>
                            hold_null,                     --:BLOCK_NAME.<Secondary Account NO>
                            hold_null                      --:BLOCK_NAME.<Secondary Account ID>
                            );

--message('hold_acct_id - '||hold_acct_id||' = '||hold_acct_no);
--message(' ',no_acknowledge);

if hold_acct_id > 0 then
  set_block_property('journals',
                     default_where,
                     'account_id = '
                   ||''''
                   ||hold_acct_id
                   ||'''');
  execute_query;
  set_block_property('journals',
                    default_where,
                    ' ');
end if;

END;         
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:31
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ACCOUNT_NO.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "ACCOUNT_NO", Function = KeyFunction.LIST_VALUES)]
        public virtual void accountNo_ListValues()
        {
            JournalsAdapter journalsElement = this.Model.Journals.CurrentRowAdapter;


            {
                NNumber batchYear = journalsElement.BatchYear;
                //  nja alio 307     -- Batch/Account Year
                string bankNo = string.Empty;
                //  Bank Number OR NULL
                string transDesc = "ALL";
                //  TRANSACTION_DESCRIPTION
                NNumber debitCredit = NNumber.Null;
                //  ( +1, 0, -1)
                string sendStatus = "1";
                //  (1)Primary Acct, (2)Secondary Acct, (3)Both 
                string callFrom = "L";
                //  (L)OV, (T)rigger, (V)alidate
                string holdNull1 = string.Empty;
                NNumber holdNull2 = NNumber.Default;
                //  Used for Return of only one account
                string holdAcctNo = string.Empty;
                //  nja alio 307
                NNumber holdAcctId = NNumber.Null;
                if (string.IsNullOrEmpty(journalsElement.BatchYear))
                {
                    TaskServices.Message("Please select a Batch Year.");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    throw new ApplicationException();
                }
                if (TaskServices.Mode == "NORMAL")
                {
                    TaskServices.ExecuteAction("SEARCH");
                }
                if (!string.IsNullOrEmpty(journalsElement.AccountNo))
                {
                    holdAcctNo = journalsElement.AccountNo;
                    BlockServices.ClearBlock();
                }

                Task.Packages.ValidateAccount.LoadValues(batchYear, ref bankNo, transDesc, debitCredit, sendStatus, callFrom, ref holdAcctNo, ref holdAcctId, ref holdNull1, ref holdNull2);

                // message('hold_acct_id - '||hold_acct_id||' = '||hold_acct_no);
                // message(' ',no_acknowledge);
                if (holdAcctId > 0)
                {
                    BlockServices.SetWhereClause("journals", "account_id = " + "'" + holdAcctId.ToString().ToString() + "'");
                    BlockServices.ExecuteQuery();
                    BlockServices.SetWhereClause("journals", " ");
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER SHOW_LOV.WHEN-BUTTON-PRESSED
        /*
         BEGIN

if :system.mode = 'NORMAL' then
  do_key('enter_query');
end if;

go_item('journals.account_no');
do_key('list_values');

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:33
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>SHOW_LOV.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "ACCOUNT_NO")]
        public virtual void showLov_buttonClick()
        {
            if (TaskServices.Mode == "NORMAL")
            {
                TaskServices.ExecuteAction("SEARCH");
            }
            ItemServices.GoItem("journals.account_no");
            TaskServices.ExecuteAction("LIST_VALUES");
        }



        #endregion

    }
}
