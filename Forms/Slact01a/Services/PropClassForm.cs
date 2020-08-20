using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Foundations.Core.AppDataLayer;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Util;
using Foundations.Core.AppSupportLib.Util.IO;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Task;
using Foundations.Core.AppSupportLib.Runtime.ControlsModel.Items;
using Foundations.Core.AppSupportLib.Runtime.ControlsModel.Containers;
using Foundations.Flavors.Forms;

using Alio.Forms.Slact01a.Controllers;
using Foundations.Core.Types;
using Alio.Common;
using Alio.Forms.Common.DbServices;
using Alio.Libraries.Sutl01a;
using Alio.Libraries.Afilerpt;
using Alio.Libraries.Webutil;
using Alio.Forms.Slact01a.Model;

namespace Alio.Forms.Slact01a.Services {

	public class PropClassForm{
		
		protected Slact01aModel model_;
		
		public Slact01aModel Model {
			get {
				return model_;
			}
		}
		
		public PropClassForm(Slact01aModel model) {
			this.model_ = model;
		}
		
		#region event handlers generated from Forms triggers
			#region Original PL/SQL code code for TRIGGER FORM.KEY-DOWN
			/*
			 begin
   if :system.last_record <> 'TRUE' then
      next_record;
   end if;   
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:101
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-DOWN</TriggerInfo>
			public virtual void form_MoveDown()
			{
				
				if ( !BlockServices.InLastRecord )
				{
					BlockServices.NextRecord();
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-ENTER
			/*
			 declare
   this_form_type varchar2(30) := name_in('global.' ||
                                     :system.current_form);
   this_record varchar2(30) := :system.cursor_record;
   this_block varchar2(30) := :system.cursor_block;
   prev_block_name varchar2(30) := get_block_property(
                                           this_block,
                                           previous_navigation_block);
   prev_block_id block;
   next_block_name varchar2(30) := get_block_property(
                                           this_block,
                                           next_navigation_block);
   next_block_id block;
   first_block_name varchar2(30) := get_form_property(
                                       :system.current_form,
                                       first_navigation_block);
   record_status varchar2(7);
   first_block boolean := true;
   second_block boolean := false;
begin
--Begin     Set Current Record Status
   if :system.mode <> 'ENTER-QUERY' then
      if :system.cursor_value is not null then
         enter;
         if get_item_property(:system.cursor_item,
                              item_is_valid) = 'FALSE' then
            return;
         end if;
      end if;
   end if;
   record_status := get_record_property(
                       this_record,
                       this_block,
                       status);
--End       Set Current Record Status
   if next_block_name = first_block_name then
      next_block_name := null;
   end if;
   if :system.mode = 'ENTER-QUERY' then
      do_key('execute_query');
      return;
   end if;
   if this_block <> first_block_name then -- If this is not the first block
      first_block := false;
      prev_block_id := find_block(prev_block_name);
      if prev_block_name = first_block_name then
         second_block := true;
      end if;
   end if;
   if next_block_name is not null then
      next_block_id := find_block(next_block_name);
   end if;
   if record_status = 'NEW' then
      if first_block then
         do_key('next_block');
      elsif second_block then
          do_key('commit_form');
          if :system.form_status <> 'QUERY' then
          	raise form_trigger_failure;
          end if;
          do_key('previous_block');
          do_key('clear_block');
          if this_form_type = 'MASTER' then
             do_key('enter_query');
          end if;
      else
         clear_block(do_commit);
         do_key('previous_block');
         do_key('next_record');
      end if;
   elsif id_null(next_block_id) or
      get_block_property(next_block_id,
                         base_table) is null then
      do_key('next_record');
   else
      do_key('next_block');
   end if;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:102
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger FORM.KEY-ENTER . See documentation for details.
			///</remarks>
			/// <TriggerInfo>FORM.KEY-ENTER</TriggerInfo>
			public virtual void form_KeyEnter()
			{
				
				{
					string thisFormType = TaskServices.NameIn("global." + TaskServices.CurrentForm);
					string thisRecord = TaskServices.CursorRecord;
					string thisBlock = TaskServices.CursorBlock;
					string prevBlockName = BlockServices.GetPreviousBlock(thisBlock);
					Foundations.Core.AppSupportLib.Runtime.ControlsModel.BlockDescriptor prevBlockId = null;
					string nextBlockName = BlockServices.GetNextBlock(thisBlock);
					Foundations.Core.AppSupportLib.Runtime.ControlsModel.BlockDescriptor nextBlockId = null;
					string firstBlockName = TaskServices.FindTask(TaskServices.CurrentForm).FirstNavigationBlock;
					string recordStatus = string.Empty;
					NBool firstBlock = true;
					NBool secondBlock = false;
					// Begin     Set Current Record Status
					if ( TaskServices.Mode != "ENTER-QUERY" )
					{
						if ( !string.IsNullOrEmpty(SupportClasses.AppContext.CursorValue) )
						{
							// F2N_TODO: The above condition uses unsupported features. You should eliminate all references to SupportClasses.
							TaskServices.ValidateItem();
							if ( ItemServices.GetIsValid(TaskServices.CursorItem) == false )
							{
								return;
							}
						}
					}
					recordStatus = BlockServices.GetRecordStatus(thisRecord, thisBlock);
					// End       Set Current Record Status
					if ( nextBlockName == firstBlockName )
					{
						nextBlockName = null;
					}
					if ( TaskServices.Mode == "ENTER-QUERY" )
					{
						TaskServices.ExecuteAction("EXECUTE_QUERY");
						return;
					}
					if ( thisBlock != firstBlockName )
					{
						//  If this is not the first block
						firstBlock = false;
						prevBlockId = BlockServices.FindBlock(prevBlockName);
						if ( prevBlockName == firstBlockName )
						{
							secondBlock = true;
						}
					}
					if ( !string.IsNullOrEmpty(nextBlockName) )
					{
						nextBlockId = BlockServices.FindBlock(nextBlockName);
					}
					if ( recordStatus == "NEW" )
					{
						if ( firstBlock )
						{
							TaskServices.ExecuteAction("NEXT_BLOCK");
						}
						else if ( secondBlock ) {
							TaskServices.ExecuteAction("SAVE");
							if ( TaskServices.FormStatus!= "QUERY" )
							{
								throw new ApplicationException();
							}
							TaskServices.ExecuteAction("PREVIOUS_BLOCK");
							TaskServices.ExecuteAction("CLEAR_BLOCK");
							if ( thisFormType == "MASTER" )
							{
								TaskServices.ExecuteAction("SEARCH");
							}
						}
						else {
							BlockServices.ClearBlock(TaskServices.DO_COMMIT);
							TaskServices.ExecuteAction("PREVIOUS_BLOCK");
							TaskServices.ExecuteAction("NEXT_RECORD");
						}
					}
					else if ( (nextBlockId == null) || string.IsNullOrEmpty(SupportClasses.FORMS40.GetBlockProperty(nextBlockId, baseTable)) ) {
						// F2N_TODO: The above condition uses unsupported features. You should eliminate all references to SupportClasses.
						TaskServices.ExecuteAction("NEXT_RECORD");
					}
					else {
						TaskServices.ExecuteAction("NEXT_BLOCK");
					}
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-ENTQRY
			/*
			 begin
   :controls.screen_mode := 'Find';
   enter_query;
   if :system.mode <> 'ENTER-QUERY' then
      :controls.screen_mode := 'Entry';
   end if;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:103
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-ENTQRY</TriggerInfo>
			public virtual void form_Search()
			{
				
				Model.Controls.ScreenMode = "Find";
				BlockServices.EnterQuery();
				if ( TaskServices.Mode != "ENTER-QUERY" )
				{
					Model.Controls.ScreenMode = "Entry";
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-EXEQRY
			/*
			 begin
   execute_query;
<multilinecomment>
   if :global.cancel_query = 'N' then
      :controls.screen_mode := 'Entry';
   else
      :global.cancel_query := 'Y';
   end if;
</multilinecomment>
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:104
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-EXEQRY</TriggerInfo>
			public virtual void form_ExecuteQuery()
			{
				
				BlockServices.ExecuteQuery();
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-EXIT
			/*
			 begin
   if :system.mode = 'ENTER-QUERY' then
      :controls.screen_mode := 'Entry';
      exit_form;
      return;
   end if;
   enter;
--   do_key('commit_form');
   if :system.form_status = 'CHANGED' then
      go_block(get_form_property(:system.current_form,
                             first_block));
      do_key('clear_block');
      if :system.form_status <> 'CHANGED' then
         exit_form;
      end if;
   else
      exit_form(no_validate);
   end if;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:105
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-EXIT</TriggerInfo>
			public virtual void form_Exit()
			{
				
				if ( TaskServices.Mode == "ENTER-QUERY" )
				{
					Model.Controls.ScreenMode = "Entry";
					TaskServices.ExitTask();
					return;
				}
				TaskServices.ValidateItem();
				//    do_key('commit_form');
				if ( TaskServices.FormStatus== "CHANGED" )
				{
					BlockServices.GoBlock(TaskServices.FindTask(TaskServices.CurrentForm).FirstBlock);
					TaskServices.ExecuteAction("CLEAR_BLOCK");
					if ( TaskServices.FormStatus!= "CHANGED" )
					{
						TaskServices.ExitTask();
					}
				}
				else {
					TaskServices.ExitTask(TaskServices.NO_VALIDATE);
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-F2
			/*
			 begin
   if get_item_property('controls.button_02',
                        enabled) = 'TRUE' then
      sutl01a.button_02;
   end if;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:106
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-F2</TriggerInfo>
			public virtual void form_F2()
			{
				
				if ( ItemServices.GetEnabled("controls.button_02") == "TRUE" )
				{
					Task.Libraries.Sutl01a.Sutl01a.Button02();
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-F3
			/*
			 begin
   if get_item_property('controls.button_03',
                        enabled) = 'TRUE' then
      sutl01a.button_03;
   end if;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:107
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-F3</TriggerInfo>
			public virtual void form_F3()
			{
				
				if ( ItemServices.GetEnabled("controls.button_03") == "TRUE" )
				{
					Task.Libraries.Sutl01a.Sutl01a.Button03();
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-F4
			/*
			 begin
   if get_item_property('controls.button_04',
                        enabled) = 'TRUE' then
      sutl01a.button_04;
   end if;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:108
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-F4</TriggerInfo>
			public virtual void form_F4()
			{
				
				if ( ItemServices.GetEnabled("controls.button_04") == "TRUE" )
				{
					Task.Libraries.Sutl01a.Sutl01a.Button04();
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-F5
			/*
			 begin
   if get_item_property('controls.button_05',
                        enabled) = 'TRUE' then
      sutl01a.button_05;
   end if;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:109
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-F5</TriggerInfo>
			public virtual void form_F5()
			{
				
				if ( ItemServices.GetEnabled("controls.button_05") == "TRUE" )
				{
					Task.Libraries.Sutl01a.Sutl01a.Button05();
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.KEY-HELP
			/*
			 begin
   sutl01a.button_01;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:110
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.KEY-HELP</TriggerInfo>
			public virtual void form_Help()
			{
				
				Task.Libraries.Sutl01a.Sutl01a.Button01();
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.ON-ERROR
			/*
			 DECLARE
   
    error_code_      number         := error_code;
    error_type_      varchar2(3)    := error_type;
    error_text_      varchar2(2000) := error_text;
    dbms_error_code_ number         := dbms_error_code;
    dbms_error_text_ varchar2(2000) := dbms_error_text;
    alert_button     number;
    source_type      varchar2(80); 
    source_name      varchar2(80);
    
BEGIN

   set_alert_property('ON_ERROR_CAUTION',title,'Warning:    FRM-'||error_code_||'  ORA'||dbms_error_code_);
   set_alert_property('ON_ERROR_STOP',title,'ERROR:    FRM-'||error_code_||'  ORA'||dbms_error_code_);
   
   if error_code_ = 40401 or
      error_code_ = 40405 or
      error_code_ = 41000 or
      error_code_ = 41009 or  --(MNN act#829 7.23.01)FRM-41009:  Function key not allowed.  Press %s for list of valid keys.
      error_code_ = 40102 or  --(MNN act#831 7.23.01)FRM-40102:  Record must be entered or deleted first(1.  The last record in a block is the current record. 2.  The block is empty. 3.  You are in a new record in the middle of the block created by pressing [Insert Record]. )
      error_code_ = 40200 or
      error_code_ = 40100 or
      error_code_ = 41051 or  --(MNN act#1464 2.5.02)FRM-41051:You cannot create records here. --Eagle. I could not duplicate the problem here, but happened contiuosly at Eagle. It seems the 'Raise form_trigger_failure' command doesn't work there.?
      error_code_ = 41211 or  --JAG SSL error
      error_code_ = 40360 or  --(MNN 12.4.01) FRM-40360: Cannot query records here (Block property Query_Allowed = False)
      error_code_ = 40102 then
      return;

   -- INSERT ERRORS  
   elsif error_code_ = 40508 then --ORACLE Error: unable to Insert Record
      source_type := upper(get_block_property(:SYSTEM.TRIGGER_BLOCK,QUERY_DATA_SOURCE_TYPE));
      source_name := upper(get_block_property(:SYSTEM.TRIGGER_BLOCK,QUERY_DATA_SOURCE_NAME));
      bell;    

      if dbms_error_code_ = -00001 then --ORA-00001	unique constraint table.column violated
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'Block '||:SYSTEM.TRIGGER_BLOCK||' is trying to insert a record into '
            ||source_type||' '||source_name
            ||' which is already in the database.'
            ||' Press Help - Display Errors, contact your support representative with this information.');
         
         alert_button := show_alert('ON_ERROR_STOP');

      elsif dbms_error_code_ = -1400 then --ORA-01400	primary key or mandatory (NOT NULL) column is missing or NULLduring insert
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'Block '||:SYSTEM.TRIGGER_BLOCK||' is trying to insert a record into '
            ||source_type||' '||source_name
            ||' which is incomplete.'
            ||' Press Help - Display Errors, contact your support representative with this information.');

         alert_button := show_alert('ON_ERROR_STOP');

      elsif dbms_error_code_ = -00904 then --ORA-00904: invalid column name
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'Block '||:SYSTEM.TRIGGER_BLOCK||' is trying to insert a record into '
            ||source_type||' '||source_name
            ||'. There is an invalid column name on the insert. '
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');

      else
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'ORA'||dbms_error_code_||': Unable to insert record.'
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');

      end if;

   --UPDATE ERRORS
   elsif error_code_ = 40509 then --ORACLE Error: unable to Update Record
      -- Added to handle update error message - Action #1409 - KLY 02/14/02
      source_type := upper(get_block_property(:SYSTEM.TRIGGER_BLOCK,QUERY_DATA_SOURCE_TYPE));
      source_name := upper(get_block_property(:SYSTEM.TRIGGER_BLOCK,QUERY_DATA_SOURCE_NAME));
      bell;    

      if dbms_error_code_ = -00001 then --ORA-00001	unique constraint table.column violated
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'Block '||:SYSTEM.TRIGGER_BLOCK||' is trying to update a record in '
            ||source_type||' '||source_name
            ||' whose Primary Key is already in the database.'
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');
      
      elsif dbms_error_code_ = -1400 then --ORA-01400	primary key or mandatory (NOT NULL) column is missing or NULL during update
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'Block '||:SYSTEM.TRIGGER_BLOCK||' is trying to update a record in '
            ||source_type||' '||source_name
            ||' which is incomplete.'
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');

      elsif dbms_error_code_ = -00904 then --ORA-00904: invalid column name
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'Block '||:SYSTEM.TRIGGER_BLOCK||' is trying to update a record in '
            ||source_type||' '||source_name
            ||'. There is an invalid column name on the update.'
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');

      else
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'ORA'||dbms_error_code_||': Unable to update record.'
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');

      end if;

   -- QUERY ERRORS
   elsif error_code_ = 40505 then --Unable to Perform Query --MNN Added (12/05/2001)
      source_type := upper(get_block_property(:SYSTEM.TRIGGER_BLOCK,QUERY_DATA_SOURCE_TYPE));
      source_name := upper(get_block_property(:SYSTEM.TRIGGER_BLOCK,QUERY_DATA_SOURCE_NAME));
      bell;    

      if dbms_error_code_ = -00942 then --ORA-00942: table or view does not exist
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'Block '||:SYSTEM.TRIGGER_BLOCK
            ||' Is calling '
            ||source_type||' '||source_name
            ||' which is unavailable or does not exist.'
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');

      elsif dbms_error_code_ = -00904 then --ORA-00904: invalid column name
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'Block '||:SYSTEM.TRIGGER_BLOCK
            ||' Is querying an invalid column on '
            ||source_type||' '||source_name
            ||', please contact your support representative for current Updates.'
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');

      else
         set_alert_property('ON_ERROR_STOP',alert_message_text,
            'FRM-ER-'|| to_char(error_code_)|| ' '|| error_text_
            ||' Press Help - Display Errors, contact your support representative with this information.');
         alert_button := show_alert('ON_ERROR_STOP');

      end if;
   
   -- LOV ERRORS   -- FRM-(30048,30049,30061,30064,90084,30150,30402)
                   -- FRM-(41091,41364,41815,41825,41826,41828,41830)
   elsif error_code_ = 41830 then
      set_alert_property('ON_ERROR_CAUTION',alert_message_text,
            'List of values contains no entries');
      alert_button := show_alert('ON_ERROR_CAUTION');

   elsif error_code_ = 40212 then
      set_alert_property('ON_ERROR_CAUTION',alert_message_text,
            'Invalid Value, either the value is not of proper data type,'
            ||' or it does not match list of values. Please select a valid value.');
      alert_button := show_alert('ON_ERROR_CAUTION');

   --FORMAT ERRORS
   elsif error_code_ = 40202 then --Required field
      source_type := upper(get_block_property(:SYSTEM.TRIGGER_BLOCK,QUERY_DATA_SOURCE_TYPE));
      source_name := upper(get_block_property(:SYSTEM.TRIGGER_BLOCK,QUERY_DATA_SOURCE_NAME));
      bell;

      set_alert_property('ON_ERROR_STOP',alert_message_text,
              initcap(replace(replace(:system.trigger_item,'_',' '),'.',' '))
              ||' is a required field. Please enter a valid value.');
--            'Block '||:SYSTEM.TRIGGER_BLOCK||' is trying to update a record in '
--            ||source_type||' '||source_name
--            ||'. Please enter the required information.' );
      alert_button := show_alert('ON_ERROR_STOP');

   elsif error_code_ = 40209 then -- number format
      set_alert_property('ON_ERROR_CAUTION',alert_message_text, error_text_);
      alert_button := show_alert('ON_ERROR_CAUTION');
   	
   elsif error_code_ = 40207 then -- Date range value
      set_alert_property('ON_ERROR_CAUTION',alert_message_text, error_text_);
      alert_button := show_alert('ON_ERROR_CAUTION');
   	
   elsif error_code_ = 50026 then -- date format
      set_alert_property('ON_ERROR_CAUTION',alert_message_text, error_text_);
      alert_button := show_alert('ON_ERROR_CAUTION');

   elsif error_code_ = 50002 then -- date format/value
      set_alert_property('ON_ERROR_CAUTION',alert_message_text, error_text_);
      alert_button := show_alert('ON_ERROR_CAUTION');

   -- MISC ERRORS
   elsif error_code_ = 41032 then -- Cannot set ENABLED attribute of current item 
      bell;
      set_alert_property('ON_ERROR_STOP',alert_message_text, error_text_);
      alert_button := show_alert('ON_ERROR_STOP');
   	
   -- TRIGGER ERRORS
   elsif error_code_ = 40735 then	
      bell;
      set_alert_property('ON_ERROR_STOP',alert_message_text, error_text_);
      alert_button := show_alert('ON_ERROR_STOP');
   	
   --CATCH ALL OTHERS
   else 
      bell;
      set_alert_property('ON_ERROR_STOP',alert_message_text,
         'FRM-ER-'|| to_char(error_code_)|| ' '|| error_text_
            ||' Press Help - Display Errors, contact your support representative with this information.');
      alert_button := show_alert('ON_ERROR_STOP');

   end if;

   raise form_trigger_failure;

END;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:111
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger FORM.ON-ERROR . See documentation for details.
			///</remarks>
			/// <param name="ex"></param>
			/// <TriggerInfo>FORM.ON-ERROR</TriggerInfo>
			public virtual void form_OnError(Exception ex)
			{
				
				{
					NNumber errorCode_ = SupportClasses.SQLFORMS.ErrorCode();
					string errorType_ = SupportClasses.SQLFORMS.ErrorType();
					string errorText_ = SupportClasses.SQLFORMS.ErrorText();
					NNumber dbmsErrorCode_ = DbManager.ErrorCode;
					string dbmsErrorText_ = DbManager.ErrorMessage;
					NNumber alertButton = NNumber.Null;
					string sourceType = string.Empty;
					string sourceName = string.Empty;
					MessageServices.SetAlertTitle("ON_ERROR_CAUTION", "Warning:    FRM-" + errorCode_.ToString().ToString() + "  ORA" + dbmsErrorCode_.ToString());
					MessageServices.SetAlertTitle("ON_ERROR_STOP", "ERROR:    FRM-" + errorCode_.ToString().ToString() + "  ORA" + dbmsErrorCode_.ToString());
					if ( errorCode_ == 40401 || errorCode_ == 40405 || errorCode_ == 41000 || errorCode_ == 41009 || errorCode_ == 40102 || errorCode_ == 40200 || errorCode_ == 40100 || errorCode_ == 41051 || errorCode_ == 41211 || errorCode_ == 40360 || errorCode_ == 40102 )
					{
						return;
					}
					else if ( errorCode_ == 40508 ) {
						// ORACLE Error: unable to Insert Record
						// F2N_NOT_SUPPORTED : The property "BLOCK's QUERY_DATA_SOURCE_TYPE" is not supported. See documentation for details.
						//						sourceType = Lib.Upper(SupportClasses.FORMS40.GetBlockProperty(TaskServices.TriggerBlock, SupportClasses.Property.QUERY_DATA_SOURCE_TYPE)).ToString();
						System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'BLOCK's QUERY_DATA_SOURCE_TYPE' is not supported. See documentation for details.");
						System.Diagnostics.Debugger.Break();
						sourceName = Lib.Upper(BlockServices.GetQueryDataSourceName(TaskServices.TriggerBlock)).ToString();
						ViewServices.Beep();
						if ( dbmsErrorCode_ == - (00001) )
						{
							// ORA-00001	unique constraint table.column violated
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "Block " + TaskServices.TriggerBlock + " is trying to insert a record into " + sourceType + " " + sourceName + " which is already in the database." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
						else if ( dbmsErrorCode_ == - (1400) ) {
							// ORA-01400	primary key or mandatory (NOT NULL) column is missing or NULLduring insert
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "Block " + TaskServices.TriggerBlock + " is trying to insert a record into " + sourceType + " " + sourceName + " which is incomplete." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
						else if ( dbmsErrorCode_ == - (00904) ) {
							// ORA-00904: invalid column name
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "Block " + TaskServices.TriggerBlock + " is trying to insert a record into " + sourceType + " " + sourceName + ". There is an invalid column name on the insert. " + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
						else {
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "ORA" + dbmsErrorCode_.ToString().ToString() + ": Unable to insert record." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
					}
					else if ( errorCode_ == 40509 ) {
						// ORACLE Error: unable to Update Record
						//  Added to handle update error message - Action #1409 - KLY 02/14/02
						// F2N_NOT_SUPPORTED : The property "BLOCK's QUERY_DATA_SOURCE_TYPE" is not supported. See documentation for details.
						//						sourceType = Lib.Upper(SupportClasses.FORMS40.GetBlockProperty(TaskServices.TriggerBlock, SupportClasses.Property.QUERY_DATA_SOURCE_TYPE)).ToString();
						System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'BLOCK's QUERY_DATA_SOURCE_TYPE' is not supported. See documentation for details.");
						System.Diagnostics.Debugger.Break();
						sourceName = Lib.Upper(BlockServices.GetQueryDataSourceName(TaskServices.TriggerBlock)).ToString();
						ViewServices.Beep();
						if ( dbmsErrorCode_ == - (00001) )
						{
							// ORA-00001	unique constraint table.column violated
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "Block " + TaskServices.TriggerBlock + " is trying to update a record in " + sourceType + " " + sourceName + " whose Primary Key is already in the database." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
						else if ( dbmsErrorCode_ == - (1400) ) {
							// ORA-01400	primary key or mandatory (NOT NULL) column is missing or NULL during update
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "Block " + TaskServices.TriggerBlock + " is trying to update a record in " + sourceType + " " + sourceName + " which is incomplete." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
						else if ( dbmsErrorCode_ == - (00904) ) {
							// ORA-00904: invalid column name
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "Block " + TaskServices.TriggerBlock + " is trying to update a record in " + sourceType + " " + sourceName + ". There is an invalid column name on the update." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
						else {
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "ORA" + dbmsErrorCode_.ToString().ToString() + ": Unable to update record." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
					}
					else if ( errorCode_ == 40505 ) {
						// Unable to Perform Query --MNN Added (12/05/2001)
						// F2N_NOT_SUPPORTED : The property "BLOCK's QUERY_DATA_SOURCE_TYPE" is not supported. See documentation for details.
						//						sourceType = Lib.Upper(SupportClasses.FORMS40.GetBlockProperty(TaskServices.TriggerBlock, SupportClasses.Property.QUERY_DATA_SOURCE_TYPE)).ToString();
						System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'BLOCK's QUERY_DATA_SOURCE_TYPE' is not supported. See documentation for details.");
						System.Diagnostics.Debugger.Break();
						sourceName = Lib.Upper(BlockServices.GetQueryDataSourceName(TaskServices.TriggerBlock)).ToString();
						ViewServices.Beep();
						if ( dbmsErrorCode_ == - (00942) )
						{
							// ORA-00942: table or view does not exist
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "Block " + TaskServices.TriggerBlock + " Is calling " + sourceType + " " + sourceName + " which is unavailable or does not exist." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
						else if ( dbmsErrorCode_ == - (00904) ) {
							// ORA-00904: invalid column name
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "Block " + TaskServices.TriggerBlock + " Is querying an invalid column on " + sourceType + " " + sourceName + ", please contact your support representative for current Updates." + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
						else {
							MessageServices.SetAlertMessageText("ON_ERROR_STOP", "FRM-ER-" + Lib.ToChar(errorCode_).ToString() + " " + errorText_ + " Press Help - Display Errors, contact your support representative with this information.");
							alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
						}
					}
					else if ( errorCode_ == 41830 ) {
						MessageServices.SetAlertMessageText("ON_ERROR_CAUTION", "List of values contains no entries");
						alertButton = TaskServices.ShowAlert("ON_ERROR_CAUTION");
					}
					else if ( errorCode_ == 40212 ) {
						MessageServices.SetAlertMessageText("ON_ERROR_CAUTION", "Invalid Value, either the value is not of proper data type," + " or it does not match list of values. Please select a valid value.");
						alertButton = TaskServices.ShowAlert("ON_ERROR_CAUTION");
					}
					else if ( errorCode_ == 40202 ) {
						// Required field
						// F2N_NOT_SUPPORTED : The property "BLOCK's QUERY_DATA_SOURCE_TYPE" is not supported. See documentation for details.
						//						sourceType = Lib.Upper(SupportClasses.FORMS40.GetBlockProperty(TaskServices.TriggerBlock, SupportClasses.Property.QUERY_DATA_SOURCE_TYPE)).ToString();
						System.Diagnostics.Debug.WriteLine(@"// F2N_NOT_SUPPORTED : The property 'BLOCK's QUERY_DATA_SOURCE_TYPE' is not supported. See documentation for details.");
						System.Diagnostics.Debugger.Break();
						sourceName = Lib.Upper(BlockServices.GetQueryDataSourceName(TaskServices.TriggerBlock)).ToString();
						ViewServices.Beep();
						MessageServices.SetAlertMessageText("ON_ERROR_STOP", Lib.Initcap(TaskServices.TriggerItem.Replace("_", " ").Replace(".", " ")).ToString() + " is a required field. Please enter a valid value.");
						//             'Block '||:SYSTEM.TRIGGER_BLOCK||' is trying to update a record in '
						//             ||source_type||' '||source_name
						//             ||'. Please enter the required information.' );
						alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
					}
					else if ( errorCode_ == 40209 ) {
						//  number format
						MessageServices.SetAlertMessageText("ON_ERROR_CAUTION", errorText_);
						alertButton = TaskServices.ShowAlert("ON_ERROR_CAUTION");
					}
					else if ( errorCode_ == 40207 ) {
						//  Date range value
						MessageServices.SetAlertMessageText("ON_ERROR_CAUTION", errorText_);
						alertButton = TaskServices.ShowAlert("ON_ERROR_CAUTION");
					}
					else if ( errorCode_ == 50026 ) {
						//  date format
						MessageServices.SetAlertMessageText("ON_ERROR_CAUTION", errorText_);
						alertButton = TaskServices.ShowAlert("ON_ERROR_CAUTION");
					}
					else if ( errorCode_ == 50002 ) {
						//  date format/value
						MessageServices.SetAlertMessageText("ON_ERROR_CAUTION", errorText_);
						alertButton = TaskServices.ShowAlert("ON_ERROR_CAUTION");
					}
					else if ( errorCode_ == 41032 ) {
						//  Cannot set ENABLED attribute of current item 
						ViewServices.Beep();
						MessageServices.SetAlertMessageText("ON_ERROR_STOP", errorText_);
						alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
					}
					else if ( errorCode_ == 40735 ) {
						ViewServices.Beep();
						MessageServices.SetAlertMessageText("ON_ERROR_STOP", errorText_);
						alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
					}
					else {
						ViewServices.Beep();
						MessageServices.SetAlertMessageText("ON_ERROR_STOP", "FRM-ER-" + Lib.ToChar(errorCode_).ToString() + " " + errorText_ + " Press Help - Display Errors, contact your support representative with this information.");
						alertButton = TaskServices.ShowAlert("ON_ERROR_STOP");
					}
					throw new ApplicationException();
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.ON-MESSAGE
			/*
			 DECLARE
	
    message_code_ number := message_code;
    message_type_ varchar2(3) := message_type;
    message_text_ varchar2(80) := message_text;
    alert_button number;

BEGIN
	
   if message_code_ = 40352 or
      message_code_ = 40353 or -- Query Canceled(Why Tell Them?!)
      message_code_ = 40400 or
      message_code_ = 40404 or
      message_code_ = 40406 or
      message_code_ = 40407 THEN 
      return;
  
   elsif message_code_ = 40301 or message_code_ = 40350 then
  
      if name_in('global.'|| name_in('system.current_form'))in('MASTERNODET') then
         bell;
         alert_button := show_alert('no_record');
         if alert_button = alert_button1 then
            :global.clear_record := 'Y';
         else
            :global.cancel_query := 'Y';
         end if;
      else
         set_alert_property('ON_MESSAGE',title,'ON-MESSAGE');
         set_alert_property('ON_MESSAGE',alert_message_text,'There were no records found.');
         alert_button := show_alert('ON_MESSAGE');
      end if;
  
   else
      bell;
      set_alert_property('ON_MESSAGE',title,'ON-MESSAGE');
      set_alert_property('ON_MESSAGE',alert_message_text,
         'FRM-MS- '|| to_char(message_code_)||' '|| message_text_);
      alert_button := show_alert('ON_MESSAGE');
  
   end if;
  
END;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:112
			/// 
			/// 
			///</remarks>
			/// <param name="args"></param>
			/// <TriggerInfo>FORM.ON-MESSAGE</TriggerInfo>
			public virtual void form_OnMessage(System.EventArgs args)
			{
				
				{
					NNumber messageCode_ = FormsMessageCode.DecodeMessageCode(MessageServices.MessageCode)();
					string messageType_ = FormsMessageType.DecodeMessageType(MessageServices.MessageType)();
					string messageText_ = MessageServices.MessageText();
					NNumber alertButton = NNumber.Null;
					if ( messageCode_ == 40352 || messageCode_ == 40353 || messageCode_ == 40400 || messageCode_ == 40404 || messageCode_ == 40406 || messageCode_ == 40407 )
					{
						return;
					}
					else if ( messageCode_ == 40301 || messageCode_ == 40350 ) {
						if ( (TaskServices.NameIn("global." + TaskServices.CurrentForm) == "MASTERNODET") )
						{
							ViewServices.Beep();
							alertButton = TaskServices.ShowAlert("no_record");
							if ( alertButton == MessageServices.BUTTON1 )
							{
								Alio.Common.Globals.ClearRecord = "Y";
							}
							else {
								Alio.Common.Globals.CancelQuery = "Y";
							}
						}
						else {
							MessageServices.SetAlertTitle("ON_MESSAGE", "ON-MESSAGE");
							MessageServices.SetAlertMessageText("ON_MESSAGE", "There were no records found.");
							alertButton = TaskServices.ShowAlert("ON_MESSAGE");
						}
					}
					else {
						ViewServices.Beep();
						MessageServices.SetAlertTitle("ON_MESSAGE", "ON-MESSAGE");
						MessageServices.SetAlertMessageText("ON_MESSAGE", "FRM-MS- " + Lib.ToChar(messageCode_).ToString() + " " + messageText_);
						alertButton = TaskServices.ShowAlert("ON_MESSAGE");
					}
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.WHEN-FORM-NAVIGATE
			/*
			 begin
   sutl01a.when_form_navigate;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:113
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger FORM.WHEN-FORM-NAVIGATE . See documentation for details.
			///</remarks>
			/// <TriggerInfo>FORM.WHEN-FORM-NAVIGATE</TriggerInfo>
			public virtual void form_WhenFormNavigate()
			{
				
				Task.Libraries.Sutl01a.Sutl01a.WhenFormNavigate();
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.WHEN-NEW-RECORD-INSTANCE
			/*
			 begin
   if :global.cancel_query = 'Y' then
      :global.cancel_query := 'N';
      if :system.mode = 'ENTER-QUERY' then
         do_key('exit_form');
      end if;
   elsif :global.clear_record = 'Y' then
      :global.clear_record := 'N';
      clear_record;
   end if;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:114
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>FORM.WHEN-NEW-RECORD-INSTANCE</TriggerInfo>
			public virtual void form_recordChange()
			{
				
				if ( Alio.Common.Globals.CancelQuery == "Y" )
				{
					Alio.Common.Globals.CancelQuery = "N";
					if ( TaskServices.Mode == "ENTER-QUERY" )
					{
						TaskServices.ExecuteAction("EXIT");
					}
				}
				else if ( Alio.Common.Globals.ClearRecord == "Y" ) {
					Alio.Common.Globals.ClearRecord = "N";
					BlockServices.ClearRecord();
				}
			}

			
			#region Original PL/SQL code code for TRIGGER FORM.WHEN-WINDOW-CLOSED
			/*
			 BEGIN

     sutl01a.close_window;	
  
END;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:115
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger FORM.WHEN-WINDOW-CLOSED . See documentation for details.
			///</remarks>
			/// <TriggerInfo>FORM.WHEN-WINDOW-CLOSED</TriggerInfo>
			public virtual void form_WhenWindowClosed()
			{
				
				Task.Libraries.Sutl01a.Sutl01a.CloseWindow();
			}

			
		
		#endregion		
		
	}
}
