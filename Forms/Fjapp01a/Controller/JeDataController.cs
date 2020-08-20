using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjapp01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fjapp01a.Controllers
{


    public class JeDataController : BaseBlockController<Fjapp01aTask,Fjapp01aModel> {
	
		public JeDataController(ITaskController parentController, String name)
            : base(parentController, name)
		{
		}
		
				
				
		#region event handlers generated from Forms triggers
			#region Original PL/SQL code code for TRIGGER JE_DATA.POST-QUERY
			/*
			 DECLARE
	 cursor get_account_no_cursor (account_id_in number) is 
   select account_no,
          account_desc
     from shr.accounts
    where account_id = account_id_in;

BEGIN 
   
  
  for cur_rec in get_account_no_cursor(:je_data.account_id)
  loop   
    :je_data.display_account  := cur_rec.account_no||' '||cur_rec.account_desc;  
  end loop;
   
   if :je_data.debit_credit_flag = -1 then 
      :je_data.cr_amount := :je_data.journal_amount; 
   else 
      :je_data.dr_amount := :je_data.journal_amount; 
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
			///	ID:51
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


				int rowCount = 0;
				if(! jeDataElement.CrAmount.IsNull)
					this.crAmount_PostChange(jeDataElement);
				if(! jeDataElement.DrAmount.IsNull)
					this.drAmount_PostChange(jeDataElement);
				if(! jeDataElement.AccountBalance.IsNull)
					this.accountBalance_PostChange(jeDataElement);
				{
					String sqlgetAccountNoCursor = "SELECT account_no, account_desc " +
	" FROM shr.accounts " +
	" WHERE account_id = @P_ACCOUNT_ID_IN ";
					DataCursor getAccountNoCursor = new DataCursor(sqlgetAccountNoCursor);
					NNumber getAccountNoCursorAccountIdIn = NNumber.Null;
					getAccountNoCursorAccountIdIn = jeDataElement.AccountId;
					//Setting query parameters
					getAccountNoCursor.AddParameter("@P_ACCOUNT_ID_IN", getAccountNoCursorAccountIdIn);
					#region loop for cursor getAccountNoCursor
					try
					{
						getAccountNoCursor.Open();
						while (true)
						{
							TableRow curRec = getAccountNoCursor.FetchRow();
							if ( getAccountNoCursor.NotFound ) break;
							jeDataElement.DisplayAccount = curRec.GetStr("account_no") + " " + curRec.GetStr("account_desc");
						}
					}finally{
						getAccountNoCursor.Close();
					}
					#endregion
					if ( jeDataElement.DebitCreditFlag == - (1) )
					{
						jeDataElement.CrAmount = jeDataElement.JournalAmount;
					}
					else {
						jeDataElement.DrAmount = jeDataElement.JournalAmount;
					}
					jeDataElement.AccountBalance = this.Task.Services.FGetAccountBalance(jeDataElement, jeHeaderElement);
					BlockServices.SetRecordStatus(TaskServices.TriggerRecord, "je_data", RecordStatus.QUERY);
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
			///	ID:39
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger ACCOUNT_BALANCE.POST-CHANGE . See documentation for details.
			///</remarks>
			/// <param name="jeDataElement"></param>
			/// <TriggerInfo>ACCOUNT_BALANCE.POST-CHANGE</TriggerInfo>

			[ActionTrigger(Action="POST-CHANGE", Item="ACCOUNT_BALANCE")]
			public virtual void accountBalance_PostChange(JeDataAdapter jeDataElement)
			{
				if ( jeDataElement.DrAmount != 0 )
				{
					jeDataElement.JournalAmount = jeDataElement.DrAmount.ToString();
					jeDataElement.DebitCreditFlag = 1;
				}
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
			///	ID:41
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger DR_AMOUNT.POST-CHANGE . See documentation for details.
			///</remarks>
			/// <param name="jeDataElement"></param>
			/// <TriggerInfo>DR_AMOUNT.POST-CHANGE</TriggerInfo>

			[ActionTrigger(Action="POST-CHANGE", Item="DR_AMOUNT")]
			public virtual void drAmount_PostChange(JeDataAdapter jeDataElement)
			{
				if ( jeDataElement.DrAmount != 0 )
				{
					jeDataElement.JournalAmount = jeDataElement.DrAmount.ToString();
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
			///	ID:43
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger CR_AMOUNT.POST-CHANGE . See documentation for details.
			///</remarks>
			/// <param name="jeDataElement"></param>
			/// <TriggerInfo>CR_AMOUNT.POST-CHANGE</TriggerInfo>

			[ActionTrigger(Action="POST-CHANGE", Item="CR_AMOUNT")]
			public virtual void crAmount_PostChange(JeDataAdapter jeDataElement)
			{
				if ( jeDataElement.CrAmount != 0 )
				{
					jeDataElement.JournalAmount = jeDataElement.CrAmount.ToString();
					jeDataElement.DebitCreditFlag = - (1);
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

			[ValidationTrigger(Item="ACCOUNT_BALANCE")]
			public virtual void accountBalance_validate()
			{
				JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
							this.accountBalance_PostChange(jeDataElement);

			}

			
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>DR_AMOUNT.WHEN-VALIDATE-ITEM</TriggerInfo>

			[ValidationTrigger(Item="DR_AMOUNT")]
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

			[ValidationTrigger(Item="CR_AMOUNT")]
			public virtual void crAmount_validate()
			{
				JeDataAdapter jeDataElement = this.Model.JeData.CurrentRowAdapter;
							this.crAmount_PostChange(jeDataElement);

			}

			
		
		#endregion		
		
	}
}
