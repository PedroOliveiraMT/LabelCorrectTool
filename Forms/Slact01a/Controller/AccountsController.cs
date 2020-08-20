using Alio.Forms.Slact01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.AppSupportLib.UI;
using Foundations.Core.Types;
using System;
using System.Data;

namespace Alio.Forms.Slact01a.Controllers
{


    public class AccountsController : AbstractBlockController<Slact01aTask, Slact01aModel>
    {

        public AccountsController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER ACCOUNTS.ON-CHECK-DELETE-MASTER
        /*
         --
-- Begin default relation declare section
--
DECLARE
Dummy_Define VARCHAR2(1);
--
-- Begin NICK_ACCOUNTS detail declare section
--
CURSOR NICK_ACCOUNTS_cur IS      
SELECT 1 FROM SHR.NICK_ACCOUNTS     
WHERE ACCOUNT_ID = :ACCOUNTS.ACCOUNT_ID;
--
-- End NICK_ACCOUNTS detail declare section
--
--
-- End default relation declare section
--
--
-- Begin default relation program section
--
BEGIN
--
-- Begin NICK_ACCOUNTS detail program section
--
OPEN NICK_ACCOUNTS_cur;     
FETCH NICK_ACCOUNTS_cur INTO Dummy_Define;     
IF ( NICK_ACCOUNTS_cur%found ) THEN     
Message('Cannot delete master record when matching detail records exist.');     
CLOSE NICK_ACCOUNTS_cur;     
RAISE Form_Trigger_Failure;     
END IF;
CLOSE NICK_ACCOUNTS_cur;
--
-- End NICK_ACCOUNTS detail program section
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
        ///	ID:14
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>ACCOUNTS.ON-CHECK-DELETE-MASTER</TriggerInfo>

        [DeleteDetails]
        public virtual void accounts_DeleteDetails(RowAdapterEventArgs args)
        {
            // 
            //  Begin default relation declare section
            // 
            AccountsAdapter accountsElement = args.Row as AccountsAdapter;


            int rowCount = 0;
            {
                string dummyDefine = string.Empty;
                String sqlnickAccountsCur = "SELECT 1 " +
" FROM SHR.NICK_ACCOUNTS " +
" WHERE ACCOUNT_ID = @ACCOUNTS_ACCOUNT_ID ";
                DataCursor nickAccountsCur = new DataCursor(sqlnickAccountsCur);
                #region
                try
                {
                    // 
                    //  Begin NICK_ACCOUNTS detail program section
                    // 
                    //Setting query parameters
                    nickAccountsCur.AddParameter("@ACCOUNTS_ACCOUNT_ID", accountsElement.AccountId);
                    nickAccountsCur.Open();
                    ResultSet nickAccountsCurResults = nickAccountsCur.FetchInto();
                    if (nickAccountsCurResults != null)
                    {
                        dummyDefine = nickAccountsCurResults.GetString(0);
                    }
                    if ((nickAccountsCur.Found))
                    {
                        TaskServices.Message("Cannot delete master record when matching detail records exist.");
                        throw new ApplicationException();
                    }
                }
                finally
                {
                    nickAccountsCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNTS.ON-POPULATE-DETAILS
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
-- Begin NICK_ACCOUNTS detail program section
--
IF ( (:ACCOUNTS.ACCOUNT_ID is not null) ) THEN   
rel_id := Find_Relation('ACCOUNTS.ACCOUNTS_NICK_ACCOUNTS');   
Query_Master_Details(rel_id, 'NICK_ACCOUNTS');   
END IF;
--
-- End NICK_ACCOUNTS detail program section
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
        ///	ID:15
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>ACCOUNTS.ON-POPULATE-DETAILS</TriggerInfo>

        [PopulateDetails]
        public virtual void accounts_PopulateDetails(RowAdapterEventArgs args)
        {
            // 
            //  Begin default relation declare section
            // 
            AccountsAdapter accounts = args.Row as AccountsAdapter;

            if (accounts.RowState == DataRowState.Added || accounts.RowState == DataRowState.Detached)
                return;

            Model.NickAccounts.LoadData();
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNTS.PRE-QUERY
        /*
         declare
cursor accounts_id_cur is
select account_id
from shr.nick_accounts
where account_nick_no = :accounts.account_no
   --and (upper(user_id) = upper(:global.qtsi_user_id) or
   and (user_id = :global.qtsi_user_id or
        user_id = '0000');
account_no_var SHR.ACCOUNTS.ACCOUNT_NO%TYPE; -- NJA ALIO 307

begin
if :accounts.account_no is not null then
  open accounts_id_cur;
  fetch accounts_id_cur into :accounts.account_id;
  close accounts_id_cur;
  if :accounts.account_id is not null then
     :accounts.account_no := null;
  end if;
  set_record_property(:system.trigger_record,
                'accounts',
                status,
                query_status);
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
        /// <param name="args"></param>
        /// <TriggerInfo>ACCOUNTS.PRE-QUERY</TriggerInfo>

        [BeforeQuery]
        public virtual void accounts_BeforeQuery(QueryEventArgs args)
        {
            AccountsAdapter accountsElement = this.Model.Accounts.CurrentRowAdapter;


            int rowCount = 0;
            {
                String sqlaccountsIdCur = "SELECT account_id " +
" FROM shr.nick_accounts " +
" WHERE account_nick_no = @ACCOUNTS_ACCOUNT_NO AND (user_id = @GLOBAL_QTSI_USER_ID OR user_id = '0000') ";
                DataCursor accountsIdCur = new DataCursor(sqlaccountsIdCur);
                NNumber accountNoVar = NNumber.Null;
                #region
                try
                {
                    if (!string.IsNullOrEmpty(accountsElement.AccountNo))
                    {
                        //Setting query parameters
                        accountsIdCur.AddParameter("@ACCOUNTS_ACCOUNT_NO", accountsElement.AccountNo);
                        accountsIdCur.AddParameter("@GLOBAL_QTSI_USER_ID", Alio.Common.Globals.QtsiUserId);
                        accountsIdCur.Open();
                        ResultSet accountsIdCurResults = accountsIdCur.FetchInto();
                        if (accountsIdCurResults != null)
                        {
                            accountsElement.AccountId = accountsIdCurResults.GetNumber(0);
                        }
                        if (!accountsElement.AccountId.IsNull)
                        {
                            accountsElement.AccountNo = null;
                        }
                        BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "accounts", RecordStatus.QUERY);
                    }
                }
                finally
                {
                    accountsIdCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER ACCOUNTS.WHEN-NEW-RECORD-INSTANCE
        /*
         begin
if :system.mode = 'ENTER-QUERY' then
:accounts.account_year := :global.account_year;
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
        /// <TriggerInfo>ACCOUNTS.WHEN-NEW-RECORD-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-RECORD-INSTANCE", Function = KeyFunction.RECORD_CHANGE)]
        public virtual void accounts_recordChange()
        {

            AccountsAdapter accountsElement = this.Model.Accounts.CurrentRowAdapter;


            if (TaskServices.Mode == "ENTER-QUERY")
            {
                accountsElement.AccountYear = Alio.Common.Globals.AccountYear;
            }
        }



        #endregion

    }
}
