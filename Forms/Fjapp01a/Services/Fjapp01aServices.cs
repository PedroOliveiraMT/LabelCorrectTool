using Alio.Common.DbServices;
using Alio.Forms.Fjapp01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fjapp01a.Services
{
    public class Fjapp01aServices : AbstractServices<Fjapp01aModel>{
		
		public Fjapp01aServices(Fjapp01aModel model):base(model)
		{
		}
		
		public new Fjapp01aTask Task
		{
			get { return (Fjapp01aTask)base.Task; }
		}
		
		#region Original PL/SQL code for Prog Unit GET_ADDITIONAL_INFO
		/*
		 
PROCEDURE get_additional_info IS 
 
  cursor batches_cur is 
   select user_id, 
          batch_date,
          account_period 
     from fas.batches 
    where batch_no   = :je_header.batch_no 
      and batch_year = :je_header.batch_year; 
   
begin 
	

  for cur_rec in batches_cur
  loop
  --	 :je_header.user_name         := cur_rec.user_id; 
     :je_header.requestor := cur_rec.user_id;                          --alio-16295 mfl 19.2
  	 :je_header.batch_date        := cur_rec.batch_date;
  	 :je_header.accounting_period := cur_rec.account_period;
  end loop;

 
END; 
 
 
  
		*/
		#endregion
		//ID:266
		/// <summary></summary>
		/// <remarks>
		/// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
		/// </remarks>
		public virtual void GetAdditionalInfo(JeHeaderAdapter jeHeaderElement)
		{
			String sqlbatchesCur = "SELECT user_id, batch_date, account_period " +
	" FROM fas.batches " +
	" WHERE batch_no = @JE_HEADER_BATCH_NO AND batch_year = @JE_HEADER_BATCH_YEAR ";
			DataCursor batchesCur = new DataCursor(sqlbatchesCur);
			//Setting query parameters
			batchesCur.AddParameter("@JE_HEADER_BATCH_NO", jeHeaderElement.BatchNo);
			batchesCur.AddParameter("@JE_HEADER_BATCH_YEAR", jeHeaderElement.BatchYear);
			#region loop for cursor batchesCur
			try
			{
				batchesCur.Open();
				while (true)
				{
					TableRow curRec = batchesCur.FetchRow();
					if ( batchesCur.NotFound ) break;
					// 	 :je_header.user_name         := cur_rec.user_id; 
					jeHeaderElement.Requestor = curRec.GetStr("user_id");
					// alio-16295 mfl 19.2
					jeHeaderElement.BatchDate = curRec.GetDate("batch_date");
					jeHeaderElement.AccountingPeriod = curRec.GetStr("account_period");
				}
			}finally{
				batchesCur.Close();
			}
			#endregion
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
		//ID:267
		/// <summary></summary>
		/// <remarks>
		/// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
		/// </remarks>
		public virtual void CheckPackageFailure()
		{
			if ( ! ((TaskServices.ServiceSuccess)) )
			{
				throw new ApplicationException();
			}
		}

		
		#region Original PL/SQL code for Prog Unit QUERY_MASTER_DETAILS
		/*
		PROCEDURE Query_Master_Details(rel_id Relation,detail VARCHAR2) IS 
  oldmsg VARCHAR2(2);  -- Old Message Level Setting 
  reldef VARCHAR2(5);  -- Relation Deferred Setting 
BEGIN 
  -- 
  -- Initialize Local Variable(s) 
  -- 
  reldef := Get_Relation_Property(rel_id, DEFERRED_COORDINATION); 
  oldmsg := :System.Message_Level; 
  -- 
  -- If NOT Deferred, Goto detail and execute the query. 
  -- 
  IF reldef = 'FALSE' THEN 
    Go_Block(detail); 
    Check_Package_Failure; 
    :System.Message_Level := '10'; 
    Execute_Query; 
    :System.Message_Level := oldmsg; 
  ELSE 
    -- 
    -- Relation is deferred, mark the detail block as un-coordinated 
    -- 
    Set_Block_Property(detail, COORDINATION_STATUS, NON_COORDINATED); 
  END IF; 
 
EXCEPTION 
    WHEN Form_Trigger_Failure THEN 
      :System.Message_Level := oldmsg; 
      RAISE; 
END Query_Master_Details; 

		*/
		#endregion
		//ID:268
		/// <summary></summary>
//		/// <param name="relId"></param>
//		/// <param name="detail"></param>
//		/// <remarks>
//		/// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
//		/// F2N_MASTER_DETAIL_LOGIC : This Program Unit was commented out because it contains Master-Detail logic. See documentation for details.
//		/// </remarks>
//		public virtual void QueryMasterDetails(System.Data.DataRelation relId, string detail)
//		{
//			string oldmsg = string.Empty;
//			//  Old Message Level Setting 
//			string reldef = string.Empty;
//			try
//			{
//				//  
//				//  Initialize Local Variable(s) 
//				//  
//				// F2N_NOT_SUPPORTED : The property "RELATION's DEFERRED_COORDINATION" is not supported. See documentation for details.
//				//				reldef = SupportClasses.FORMS40.GetRelationProperty(relId, SupportClasses.Property.DEFERRED_COORDINATION);
//				////
//				System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'RELATION's DEFERRED_COORDINATION' is not supported. See documentation for details.");
//				System.Diagnostics.Debugger.Break();
//				oldmsg = MessageServices.MessageLevel;
//				//  
//				//  If NOT Deferred, Goto detail and execute the query. 
//				//  
//				if ( reldef == "FALSE" )
//				{
//					BlockServices.GoBlock(detail);
//					CheckPackageFailure();
//					// F2N_WARNING : Please validate if you need to surround the message level manipulation with a try/finally block
//					MessageServices.MessageLevel = FormsMessageLevel.DecodeMessageLevel("10");
//					BlockServices.ExecuteQuery();
//					// F2N_WARNING : Please validate if you need to surround the message level manipulation with a try/finally block
//					MessageServices.MessageLevel = FormsMessageLevel.DecodeMessageLevel(oldmsg);
//				}
//				else {
//					//  
//					//  Relation is deferred, mark the detail block as un-coordinated 
//					//  
//					// F2N_NOT_SUPPORTED : The property "BLOCK's COORDINATION_STATUS" is not supported. See documentation for details.
//					//					SupportClasses.FORMS40.SetBlockProperty(detail, SupportClasses.Property.COORDINATION_STATUS, SupportClasses.Constants.NON_COORDINATED);
//					////
//					System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'BLOCK's COORDINATION_STATUS' is not supported. See documentation for details.");
//					System.Diagnostics.Debugger.Break();
//				}
//			}
//			catch(ApplicationException)
//			{
//				// F2N_WARNING : Please validate if you need to surround the message level manipulation with a try/finally block
//				MessageServices.MessageLevel = FormsMessageLevel.DecodeMessageLevel(oldmsg);
//				throw ;
//			}
//		}
//
		
		#region Original PL/SQL code for Prog Unit CLEAR_ALL_MASTER_DETAILS
		/*
		PROCEDURE Clear_All_Master_Details IS 
  mastblk  VARCHAR2(30);  -- Initial Master Block Cusing Coord 
  coordop  VARCHAR2(30);  -- Operation Causing the Coord 
  trigblk  VARCHAR2(30);  -- Cur Block On-Clear-Details Fires On 
  startitm VARCHAR2(61);  -- Item in which cursor started 
  frmstat  VARCHAR2(15);  -- Form Status 
  curblk   VARCHAR2(30);  -- Current Block 
  currel   VARCHAR2(30);  -- Current Relation 
  curdtl   VARCHAR2(30);  -- Current Detail Block 
 
  FUNCTION First_Changed_Block_Below(Master VARCHAR2) 
  RETURN VARCHAR2 IS 
    curblk VARCHAR2(30);  -- Current Block 
    currel VARCHAR2(30);  -- Current Relation 
    retblk VARCHAR2(30);  -- Return Block 
  BEGIN 
    -- 
    -- Initialize Local Vars 
    -- 
    curblk := Master; 
    currel := Get_Block_Property(curblk,  FIRST_MASTER_RELATION); 
    -- 
    -- While there exists another relation for this block 
    -- 
    WHILE currel IS NOT NULL LOOP 
      -- 
      -- Get the name of the detail block 
      -- 
      curblk := Get_Relation_Property(currel, DETAIL_NAME); 
      -- 
      -- If this block has changes, return its name 
      -- 
      IF ( Get_Block_Property(curblk, STATUS) IN('CHANGED','INSERT') ) THEN 
        RETURN curblk; 
      ELSE 
        -- 
        -- No changes, recursively look for changed blocks below 
        -- 
        retblk := First_Changed_Block_Below(curblk); 
        -- 
        -- If some block below is changed, return its name 
        -- 
        IF retblk IS NOT NULL THEN 
          RETURN retblk; 
        ELSE 
          -- 
          -- Consider the next relation 
          -- 
          currel := Get_Relation_Property(currel, NEXT_MASTER_RELATION); 
        END IF; 
      END IF; 
    END LOOP; 
 
    -- 
    -- No changed blocks were found 
    -- 
    RETURN NULL; 
  END First_Changed_Block_Below; 
 
BEGIN 
  -- 
  -- Init Local Vars 
  -- 
  mastblk  := :System.Master_Block; 
  coordop  := :System.Coordination_Operation; 
  trigblk  := :System.Trigger_Block; 
  startitm := :System.Trigger_Item; 
  frmstat  := :System.Form_Status; 
 
  -- 
  -- If the coordination operation is anything but CLEAR_RECORD or 
  -- SYNCHRONIZE_BLOCKS, then continue checking. 
  -- 
  IF coordop NOT IN ('CLEAR_RECORD', 'SYNCHRONIZE_BLOCKS') THEN 
    -- 
    -- If we're processing the driving master block... 
    -- 
    IF mastblk = trigblk THEN 
      -- 
      -- If something in the form is changed, find the 
      -- first changed block below the master 
      -- 
      IF frmstat = 'CHANGED' THEN 
        curblk := First_Changed_Block_Below(mastblk); 
        -- 
        -- If we find a changed block below, go there 
        -- and Ask to commit the changes. 
        -- 
        IF curblk IS NOT NULL THEN 
          Go_Block(curblk); 
          Check_Package_Failure; 
          Clear_Block(ASK_COMMIT); 
          -- 
          -- If user cancels commit dialog, raise error 
          -- 
          IF NOT ( :System.Form_Status = 'QUERY' 
                   OR :System.Block_Status = 'NEW' ) THEN 
            RAISE Form_Trigger_Failure; 
          END IF; 
        END IF; 
      END IF; 
    END IF; 
  END IF; 
 
  -- 
  -- Clear all the detail blocks for this master without 
  -- any further asking to commit. 
  -- 
  currel := Get_Block_Property(trigblk, FIRST_MASTER_RELATION); 
  WHILE currel IS NOT NULL LOOP 
    curdtl := Get_Relation_Property(currel, DETAIL_NAME); 
    IF Get_Block_Property(curdtl, STATUS) <> 'NEW'  THEN 
      Go_Block(curdtl); 
      Check_Package_Failure; 
      Clear_Block(NO_VALIDATE); 
      IF :System.Block_Status <> 'NEW' THEN 
        RAISE Form_Trigger_Failure; 
      END IF; 
    END IF; 
    currel := Get_Relation_Property(currel, NEXT_MASTER_RELATION); 
  END LOOP; 
 
  -- 
  -- Put cursor back where it started 
  -- 
  IF :System.Cursor_Item <> startitm THEN 
    Go_Item(startitm); 
    Check_Package_Failure; 
  END IF; 
 
EXCEPTION 
  WHEN Form_Trigger_Failure THEN 
    IF :System.Cursor_Item <> startitm THEN 
      Go_Item(startitm); 
    END IF; 
    RAISE; 
 
END Clear_All_Master_Details; 

		*/
		#endregion
		//ID:269
		/// <summary></summary>
//		/// <remarks>
//		/// F2N_MASTER_DETAIL_LOGIC : This Program Unit was commented out because it contains Master-Detail logic. See documentation for details.
//		/// </remarks>
//		public virtual void ClearAllMasterDetails()
//		{
//			string mastblk = string.Empty;
//			//  Initial Master Block Cusing Coord 
//			string coordop = string.Empty;
//			//  Operation Causing the Coord 
//			string trigblk = string.Empty;
//			//  Cur Block On-Clear-Details Fires On 
//			string startitm = string.Empty;
//			//  Item in which cursor started 
//			string frmstat = string.Empty;
//			//  Form Status 
//			string curblk = string.Empty;
//			//  Current Block 
//			string currel = string.Empty;
//			//  Current Relation 
//			string curdtl = string.Empty;
//			try
//			{
//				//  
//				//  Init Local Vars 
//				//  
//				mastblk = TaskServices.MasterBlock;
//				coordop = TaskServices.CoordinationOperation;
//				trigblk = TaskServices.TriggerBlock;
//				startitm = TaskServices.TriggerItem;
//				frmstat = TaskServices.FormStatus;
//				//  
//				//  If the coordination operation is anything but CLEAR_RECORD or 
//				//  SYNCHRONIZE_BLOCKS, then continue checking. 
//				//  
//				if ( (coordop != "CLEAR_RECORD" && coordop != "SYNCHRONIZE_BLOCKS") )
//				{
//					//  
//					//  If we're processing the driving master block... 
//					//  
//					if ( mastblk == trigblk )
//					{
//						//  
//						//  If something in the form is changed, find the 
//						//  first changed block below the master 
//						//  
//						if ( frmstat == "CHANGED" )
//						{
//							curblk = ClearAllMasterDetails_FFirstChangedBlockBelow_Local(mastblk);
//							//  
//							//  If we find a changed block below, go there 
//							//  and Ask to commit the changes. 
//							//  
//							if ( !string.IsNullOrEmpty(curblk) )
//							{
//								BlockServices.GoBlock(curblk);
//								CheckPackageFailure();
//								BlockServices.ClearBlock(TaskServices.ASK_COMMIT);
//								//  
//								//  If user cancels commit dialog, raise error 
//								//  
//								if ( ! ((TaskServices.FormStatus== "QUERY" || TaskServices.BlockStatus== "NEW")) )
//								{
//									throw new ApplicationException();
//								}
//							}
//						}
//					}
//				}
//				//  
//				//  Clear all the detail blocks for this master without 
//				//  any further asking to commit. 
//				//  
//				// F2N_NOT_SUPPORTED : The property "BLOCK's FIRST_MASTER_RELATION" is not supported. See documentation for details.
//				//				currel = SupportClasses.FORMS40.GetBlockProperty(trigblk, SupportClasses.Property.FIRST_MASTER_RELATION);
//				////
//				System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'BLOCK's FIRST_MASTER_RELATION' is not supported. See documentation for details.");
//				System.Diagnostics.Debugger.Break();
//				while ( !string.IsNullOrEmpty(currel) ) {
//					// F2N_NOT_SUPPORTED : The property "RELATION's DETAIL_NAME" is not supported. See documentation for details.
//					//					curdtl = SupportClasses.FORMS40.GetRelationProperty(currel, SupportClasses.Property.DETAIL_NAME);
//					////
//					System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'RELATION's DETAIL_NAME' is not supported. See documentation for details.");
//					System.Diagnostics.Debugger.Break();
//					if ( BlockServices.GetStatus(curdtl) != "NEW" )
//					{
//						BlockServices.GoBlock(curdtl);
//						CheckPackageFailure();
//						BlockServices.ClearBlock(TaskServices.NO_VALIDATE);
//						if ( TaskServices.BlockStatus!= "NEW" )
//						{
//							throw new ApplicationException();
//						}
//					}
//					// F2N_NOT_SUPPORTED : The property "RELATION's NEXT_MASTER_RELATION" is not supported. See documentation for details.
//					//					currel = SupportClasses.FORMS40.GetRelationProperty(currel, SupportClasses.Property.NEXT_MASTER_RELATION);
//					////
//					System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'RELATION's NEXT_MASTER_RELATION' is not supported. See documentation for details.");
//					System.Diagnostics.Debugger.Break();
//				}
//				//  
//				//  Put cursor back where it started 
//				//  
//				if ( TaskServices.CursorItem != startitm )
//				{
//					ItemServices.GoItem(startitm);
//					CheckPackageFailure();
//				}
//			}
//			catch(ApplicationException)
//			{
//				if ( TaskServices.CursorItem != startitm )
//				{
//					ItemServices.GoItem(startitm);
//				}
//				throw ;
//			}
//		}
///// <summary></summary>
//		/// <param name="master"></param>
//		/// <remarks>
//		///  Current Detail Block 
//		/// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
//		/// F2N_MASTER_DETAIL_LOGIC : This Program Unit was commented out because it contains Master-Detail logic. See documentation for details.
//		/// </remarks>
//		public virtual string ClearAllMasterDetails_FFirstChangedBlockBelow_Local(string master)
//		{
//			// F2N_WARNING : Variable name was changed from curblk to curblk2. Please review if variable usage is correct.
//			string curblk2 = string.Empty;
//			//  Current Block 
//			// F2N_WARNING : Variable name was changed from currel to currel2. Please review if variable usage is correct.
//			string currel2 = string.Empty;
//			//  Current Relation 
//			string retblk = string.Empty;
//			//  
//			//  Initialize Local Vars 
//			//  
//			curblk2 = master;
//			// F2N_NOT_SUPPORTED : The property "BLOCK's FIRST_MASTER_RELATION" is not supported. See documentation for details.
//			//			currel2 = SupportClasses.FORMS40.GetBlockProperty(curblk2, SupportClasses.Property.FIRST_MASTER_RELATION);
//			////
//			System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'BLOCK's FIRST_MASTER_RELATION' is not supported. See documentation for details.");
//			System.Diagnostics.Debugger.Break();
//			//  
//			//  While there exists another relation for this block 
//			//  
//			while ( !string.IsNullOrEmpty(currel2) ) {
//				//  
//				//  Get the name of the detail block 
//				//  
//				// F2N_NOT_SUPPORTED : The property "RELATION's DETAIL_NAME" is not supported. See documentation for details.
//				//				curblk2 = SupportClasses.FORMS40.GetRelationProperty(currel2, SupportClasses.Property.DETAIL_NAME);
//				////
//				System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'RELATION's DETAIL_NAME' is not supported. See documentation for details.");
//				System.Diagnostics.Debugger.Break();
//				//  
//				//  If this block has changes, return its name 
//				//  
//				if (((BlockServices.GetStatus(curblk2) == "CHANGED" || BlockServices.GetStatus(curblk2) == "INSERT")))
//				{
//					return curblk3;
//				}
//				else {
//					//  
//					//  No changes, recursively look for changed blocks below 
//					//  
//					retblk = ClearAllMasterDetails_FFirstChangedBlockBelow_Local(curblk3);
//					//  
//					//  If some block below is changed, return its name 
//					//  
//					if ( !string.IsNullOrEmpty(retblk) )
//					{
//						return retblk;
//					}
//					else {
//						//  
//						//  Consider the next relation 
//						//  
//						// F2N_NOT_SUPPORTED : The property "RELATION's NEXT_MASTER_RELATION" is not supported. See documentation for details.
//						//						currel4 = SupportClasses.FORMS40.GetRelationProperty(currel4, SupportClasses.Property.NEXT_MASTER_RELATION);
//						////
//						System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'RELATION's NEXT_MASTER_RELATION' is not supported. See documentation for details.");
//						System.Diagnostics.Debugger.Break();
//					}
//				}
//			}
//			//  
//			//  No changed blocks were found 
//			//  
//			return null;
//		}
//
		
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
		//ID:270
		/// <summary></summary>
		/// <remarks>
		/// F2N_STRONG_PRESENTATION_LOGIC : The code of this procedure was identified as containing presentation logic. See documentation for details.
		/// </remarks>
		public virtual void DateChoosen()
		{
            System.Diagnostics.Debugger.Break();
			//TaskServices.Copy(Lib.ToChar(Task.Libraries.Calendar.DateLov.currentLovDate, "dd-mon-yyyy"),Task.Libraries.Calendar.DateLov.dateLovReturnItem);
			//ItemServices.GoItem(Task.Libraries.Calendar.DateLov.dateLovReturnItem);
			//if ( Task.Libraries.Calendar.DateLov.lovAutoSkip == true )
			//{
			//	ItemServices.NextItem();
			//}
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
		//ID:271
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
			if ( Alio.Common.Globals.DebugStatus == "ON" )
			{
				TaskServices.Message(Lib.Substring(pMessage, 1, 200));
				TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
			}
		}

		
		#region Original PL/SQL code for Prog Unit POPULATE_BATCH_TOTALS
		/*
		PROCEDURE populate_batch_totals IS 
 
BEGIN
	null; 
 <multilinecomment>
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
  </multilinecomment>
END;
		*/
		#endregion
		//ID:272
		/// <summary></summary>
		/// <remarks>
		/// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
		/// </remarks>
		public virtual void PopulateBatchTotals()
		{
		}
		
		#region Original PL/SQL code for Prog Unit GET_ACCOUNT_BALANCE
		/*
		FUNCTION get_account_balance RETURN number IS 
 cursor current_amounts_cur (in_account_id varchar2, 
                             in_accounting_period varchar2) is 
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
 
 
 
  
BEGIN 
 
declare 
 hold_balance_out number; 
 hold_host_account_no varchar2(50); 
begin 
 -- this is through period ??? 
 fsact02a_.check_balance (  356830,-- 447213,--account_id, 
                                           hold_balance_out, 
                                           hold_host_account_no 
                                           ); 
 
 
end; 
 
 
 -- dph 
 
  open current_amounts_cur(:je_data.account_id,:je_header.accounting_period); 
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
  <multilinecomment> 
  return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) 
         + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) 
          - (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) 
          - (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0) 
          + nvl(v_pre_amount,0)); 
 </multilinecomment> 
   return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) - 
          (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) - 
          (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0)) 
          + nvl(v_pre_amount,0);  
 
  -- end dph 13037-- 
 <multilinecomment>        
   -- pre 13037 dph change the code was: 
   -- note the subtraction of the pre_payable 
  return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) - 
          (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) - 
          (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0))- 
          (nvl(current_amounts_rec.pre_payable,0) * nvl(current_amounts_rec.amount_multiplier,0));   
  </multilinecomment> 
  
  else 
  return 0; 
  end if; 
  close current_amounts_cur;  
  return 0; 
END;
		*/
		#endregion
		//ID:274
		/// <summary></summary>
		/// <remarks>
		/// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
		/// </remarks>
		public virtual NNumber FGetAccountBalance(JeDataAdapter jeDataElement, JeHeaderAdapter jeHeaderElement)
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
			NNumber defaultAmount = 0;
			#region
			try {
				{
					NNumber holdBalanceOut = NNumber.Null;
					string holdHostAccountNo = string.Empty;
                    //  this is through period ??? 

                    Fsact02a_.CheckBalance(356830, ref holdBalanceOut, ref holdHostAccountNo);
				}
				//  dph 
				currentAmountsCurInAccountId = jeDataElement.AccountId;
				currentAmountsCurInAccountingPeriod = jeHeaderElement.AccountingPeriod;
				//Setting query parameters
				currentAmountsCur.AddParameter("@P_IN_ACCOUNT_ID", currentAmountsCurInAccountId);
				currentAmountsCur.AddParameter("@P_IN_ACCOUNTING_PERIOD", currentAmountsCurInAccountingPeriod);
				currentAmountsCur.Open();
				currentAmountsRec = currentAmountsCur.FetchRow();
				// ---- begin dph 13037 code ------- 
				//  determine pre amount based on code in fainq01a.fmx 
				//  and add it to the current amount 
				if ( currentAmountsCur.Found )
				{
					if ( currentAmountsRec.GetNumber("account_type") >= 77 && currentAmountsRec.GetNumber("account_type") <= 99 )
					{
						if ( (currentAmountsRec.GetNumber("account_type") == 77 || currentAmountsRec.GetNumber("account_type") == 78) )
						{
							vPreAmount = Lib.IsNull(currentAmountsRec.GetString("pre_budget"), defaultAmount) - Lib.IsNull(currentAmountsRec.GetString("pre_warehouse"), defaultAmount) - Lib.IsNull(currentAmountsRec.GetString("pre_encumbrance"), defaultAmount) - Lib.IsNull(currentAmountsRec.GetString("pre_payable"), defaultAmount);
						}
						else {
							vPreAmount = Lib.IsNull(currentAmountsRec.GetString("pre_budget"), defaultAmount) * Lib.IsNull(currentAmountsRec.GetString("budget_multiplier"), defaultAmount) - Lib.IsNull(currentAmountsRec.GetString("pre_warehouse"), defaultAmount) * Lib.IsNull(currentAmountsRec.GetString("encumbrance_multiplier"), defaultAmount) - Lib.IsNull(currentAmountsRec.GetString("pre_encumbrance"), defaultAmount) * Lib.IsNull(currentAmountsRec.GetString("encumbrance_multiplier"), defaultAmount) - Lib.IsNull(currentAmountsRec.GetString("pre_payable"), defaultAmount) * Lib.IsNull(currentAmountsRec.GetString("amount_multiplier"), defaultAmount);
						}
					}
					else {
						// no budget is set 
						vPreAmount = Lib.IsNull(currentAmountsRec.GetString("pre_warehouse"), defaultAmount) + Lib.IsNull(currentAmountsRec.GetString("pre_encumbrance"), defaultAmount) + Lib.IsNull(currentAmountsRec.GetString("pre_payable"), defaultAmount);
					}
					// return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))
					// + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)))
					// - (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0))
					// - (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0)
					// + nvl(v_pre_amount,0));
					return ((Lib.IsNull(currentAmountsRec.GetString("budget_cur"), defaultAmount) * Lib.IsNull(currentAmountsRec.GetString("budget_multiplier"), defaultAmount)) + (Lib.IsNull(currentAmountsRec.GetString("adjustment_cur"), defaultAmount) * Lib.IsNull(currentAmountsRec.GetString("budget_multiplier"), defaultAmount))) - (Lib.IsNull(currentAmountsRec.GetString("encumbrance_cur"), defaultAmount) * Lib.IsNull(currentAmountsRec.GetString("encumbrance_multiplier"), defaultAmount)) - (Lib.IsNull(currentAmountsRec.GetString("amount_cur"), NNumber.ToNumber(defaultAmount)) * Lib.IsNull(currentAmountsRec.GetString("amount_multiplier"), defaultAmount)) + Lib.IsNull(vPreAmount, defaultAmount);
				}
				else {
					return 0;
				}
				return 0;
			}finally{
				currentAmountsCur.Close();
			}
				#endregion
		}

		
		

	}
}
