using Alio.Common;
using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjent01a.Model;
using Alio.Libraries.Sutl01a;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Task;
using Foundations.Core.AppSupportLib.UI;
using System;
using System.Collections;

namespace Alio.Forms.Fjent01a.Controllers
{

    public class Fjent01aTaskController : AlioBaseTaskController<Fjent01aTask, Fjent01aModel>
    {

        public Fjent01aTaskController(Fjent01aTask task) : base(task)
        {
        }



        #region event handlers generated from Forms triggers


        #region Original PL/SQL code code for TRIGGER FJENT01A.KEY-EXEQRY
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
        ///	ID:416
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>FJENT01A.KEY-EXEQRY</TriggerInfo>

        [ActionTrigger(Action = "KEY-EXEQRY", Function = KeyFunction.EXECUTE_QUERY)]
        public virtual void Fjent01a_ExecuteQuery()
        {

            BlockServices.ExecuteQuery();
        }


        #region Original PL/SQL code code for TRIGGER FJENT01A.WHEN-WINDOW-CLOSED
        /*
         BEGIN

IF :System.Event_Window = 'DATE_LOV_WINDOW' THEN 

 go_item(date_lov.date_lov_return_item);

ELSE

 sutl01a.close_window;	
END IF; 

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:425
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger FJENT01A.WHEN-WINDOW-CLOSED . See documentation for details.
        ///</remarks>
        /// <TriggerInfo>FJENT01A.WHEN-WINDOW-CLOSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-WINDOW-CLOSED")]
        public virtual void Fjent01a_WhenWindowClosed()
        {
            if (TaskServices.CurrentWindow == "DATE_LOV_WINDOW")
            {
                //ItemServices.GoItem(Task.Libraries.Calendar.DateLov.dateLovReturnItem);
            }
            else
            {
                PkgSutl01a.CloseWindow();
            }
        }


        #region Original PL/SQL code code for TRIGGER FJENT01A.COPYRIGHT
        /*
         --  Copyright (C) 1997 by Quad Two Systems, Inc. 
--  All Rights Reserved. 

--  This material has prepared by and is proprietary 
--  to Quad Two Systems, Inc.  All rights reserved.  
--  Reproduction, distribution, or disclosure in whole 
--  or in part is forbidden except with prior written 
--  permission of Quad Two Systems, Inc. 

begin 
null; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:428
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>FJENT01A.COPYRIGHT</TriggerInfo>

        [ActionTrigger(Action = "COPYRIGHT")]
        public virtual void Fjent01a_Copyright()
        {

            //   Copyright (C) 1997 by Quad Two Systems, Inc. 
            //   All Rights Reserved. 
            //   This material has prepared by and is proprietary 
            //   to Quad Two Systems, Inc.  All rights reserved.  
            //   Reproduction, distribution, or disclosure in whole 
            //   or in part is forbidden except with prior written 
            //   permission of Quad Two Systems, Inc. 
        }


        #region Original PL/SQL code code for TRIGGER FJENT01A.ON-CLEAR-DETAILS
        /*
         -- 
-- Begin default relation program section 
-- 
BEGIN 
Clear_All_Master_Details; 
--  :totals.dr_total := null; 
--  :totals.cr_total := null; 
END; 
-- 
-- End default relation program section 
-- 

        */
        #endregion
        /* IGNORED
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:429
        /// F2N_MASTER_DETAIL_LOGIC : This Trigger was commented out because it contains Master-Detail logic. See documentation for details.
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJENT01A.ON-CLEAR-DETAILS</TriggerInfo>

        [ClearDetails]
        public virtual void Fjent01a_ClearDetails(System.EventArgs args)
        {

            //  
            //  Begin default relation program section 
            //  
            this.Task.Services.ClearAllMasterDetails();
        }

        */

        #region Original PL/SQL code code for TRIGGER FJENT01A.WHEN-NEW-FORM-INSTANCE
        /*
         declare 

cursor batches_cur is 
select batch_date, 
   account_period 
 from fas.batches 
where batch_no = :batch_no_block.batch_no and 
      batch_year = :batch_no_block.batch_year; 

 CURSOR get_profile_cur  -- dph alio-14278 
  IS
 SELECT max(profile_data) profile_data
   FROM adm.profiles
  WHERE profile_key = 'USE_WHS_ORDER_NO_FOR_JOURNAL_REF';


begin 

-- set debugging globals 
 default_value ('*', 'global.debug_status'); 

:parameter.qtsi_role_id := :GLOBAL.qtsi_role_id;               --mfl alio-14830 19.2

sutl01a.when_new_form_instance;                               

handle_item_properties;  -- uses adm.user_item_properties     --mfl alio-14830 19.2

copy('JE', 'global.' || :system.current_form || '_batch_type'); 
synchronize; 
sutl01a.run_prg('fmbth01a.fmx','3'); 

default_value(null, 
             'global.' 
             || :system.current_form 
             || '_batch_no'); 

if name_in(':global.' 
         || :system.current_form 
         || '_batch_no') is null then 
  exit_form; -- If they didnt choose a batch in Batch Master 
end if; 

:batch_no_block.batch_no := 
              name_in(':global.' 
                     || :system.current_form 
                     || '_batch_no'); 

:batch_no_block.batch_year := 
   name_in(':global.' 
          || :system.current_form 
          || '_batch_year'); 

open batches_cur; 
fetch batches_cur into :batch_no_block.batch_date, :batch_no_block.accounting_period; 
close batches_cur; 

:je_header.journal_date := :batch_no_block.batch_date; 
:je_header.batch_no := 
              name_in(':global.' 
                     || :system.current_form 
                     || '_batch_no'); 
:je_header.batch_year := 
 name_in(':global.' 
        || :system.current_form 
        || '_batch_year'); 


-- dph alio-14278 
for cur_rec in get_profile_cur
loop
:parameter.use_order_no := cur_rec.profile_data;
end loop;

-- 06/12/17 psr alio-15315 Added controlling using of whs_order_no or Reference no assignment button and field.
go_item('je_header.journal_date');  -- Move to someother item

if nvl(:parameter.use_order_no,'N') = 'Y' then 
SET_ITEM_PROPERTY('je_header.whs_order_no',     visible, property_true);
SET_ITEM_PROPERTY('je_header.whs_order_no',     enabled, property_true);

<multilinecomment> 06/13/17 psr alio-15315 Commented changes to assign_reference and reference_no
SET_ITEM_PROPERTY('je_header.assign_reference', visible, property_false);
SET_ITEM_PROPERTY('je_header.assign_reference', enabled, property_false);
SET_ITEM_PROPERTY('je_header.reference_no',     visible, property_false);
SET_ITEM_PROPERTY('je_header.reference_no',     enabled, property_false);
</multilinecomment>

-- 06/13/17 psr alio-15315 Commented do not want to go to je_header.whs_order_no
go_item('je_header.whs_order_no');

else
<multilinecomment> 06/13/17 psr alio-15315 Commented changes to assign_reference and reference_no
SET_ITEM_PROPERTY('je_header.assign_reference', visible, property_true);
SET_ITEM_PROPERTY('je_header.assign_reference', enabled, property_true);
SET_ITEM_PROPERTY('je_header.reference_no',     visible, property_true);
SET_ITEM_PROPERTY('je_header.reference_no',     enabled, property_true);
</multilinecomment>

SET_ITEM_PROPERTY('je_header.whs_order_no',     visible, property_false);
SET_ITEM_PROPERTY('je_header.whs_order_no',     enabled, property_false);

-- 06/13/17 psr alio-15315 Moved go_item() after if nvl(:parameter.use_order_no,'N') = 'Y' 
--go_item('je_header.assign_reference');
end if;
-- 06/13/17 psr alio-15315 Moved go_item() after if nvl(:parameter.use_order_no,'N') = 'Y' 
go_item('je_header.assign_reference');


populate_batch_totals; 
accounting_array.accounting_tab_init; --cec alio-3074 

-- 09/04/14 alio-3074 klb added profileto determine if journal account amounts should be validated against budget 
:parameter.journal_budget_check           := nvl(trim(adm.get_profile_data('JOURNAL_BUDGET_CHECK')),'N'); 

-- dph 3109 
:parameter.journal_approval        := nvl(trim(adm.get_profile_data('JOURNAL_APPROVAL')),'N'); 
hold.reference_table.delete;

if :parameter.journal_approval = 'N' then
SET_ITEM_PROPERTY ('je_header.approval_status_desc', visible,property_false);
SET_ITEM_PROPERTY ('je_header.view_button', visible,property_false);
SET_ITEM_PROPERTY ('je_header.ready_for_approval', visible,property_false);     --mfl alio-14824
end if;   

set_record_property(1 
                  ,'je_header' 
                  ,status 
                  ,new_status); 

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:430
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJENT01A.WHEN-NEW-FORM-INSTANCE</TriggerInfo>

        [TaskStarted]
        public virtual void Fjent01a_TaskStarted(System.EventArgs args)
        {
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;


            int rowCount = 0;
            {
                String sqlbatchesCur = "SELECT batch_date, account_period " +
" FROM fas.batches " +
" WHERE batch_no = @BATCH_NO_BLOCK_BATCH_NO AND batch_year = @BATCH_NO_BLOCK_BATCH_YEAR ";
                DataCursor batchesCur = new DataCursor(sqlbatchesCur);
                String sqlgetProfileCur = "SELECT max(profile_data) profile_data " +
" FROM adm.profiles " +
" WHERE profile_key = 'USE_WHS_ORDER_NO_FOR_JOURNAL_REF' ";
                DataCursor getProfileCur = new DataCursor(sqlgetProfileCur);
                #region
                try
                {
                    //  set debugging globals 
                    Globals.SetDefault("global.debug_status", "*");
                    this.Model.Params.QtsiRoleId = Alio.Common.Globals.QtsiRoleId;
                    // mfl alio-14830 19.2
                    Task.Libraries.Sutl01a.Sutl01a.WhenNewFormInstance();
                    this.Task.Services.HandleItemProperties();
                    //  uses adm.user_item_properties     --mfl alio-14830 19.2
                    TaskServices.Copy("JE", "global." + TaskServices.CurrentForm + "_batch_type");
                    TaskServices.Synchronize();
                    Task.Libraries.Sutl01a.Sutl01a.RunPrg("fmbth01a.fmx", "3");
                    Globals.SetDefault("global." + TaskServices.CurrentForm + "_batch_no", null);
                    if (string.IsNullOrEmpty(TaskServices.NameIn(":global." + TaskServices.CurrentForm + "_batch_no")))
                    {
                        TaskServices.ExitTask();
                    }
                    Model.BatchNoBlock.BatchNo = TaskServices.NameIn(":global." + TaskServices.CurrentForm + "_batch_no");
                    Model.BatchNoBlock.BatchYear = TaskServices.NameIn(":global." + TaskServices.CurrentForm + "_batch_year");
                    //Setting query parameters
                    batchesCur.AddParameter("@BATCH_NO_BLOCK_BATCH_NO", Model.BatchNoBlock.BatchNo);
                    batchesCur.AddParameter("@BATCH_NO_BLOCK_BATCH_YEAR", Model.BatchNoBlock.BatchYear);
                    batchesCur.Open();
                    ResultSet batchesCurResults = batchesCur.FetchInto();
                    if (batchesCurResults != null)
                    {
                        Model.BatchNoBlock.BatchDate = batchesCurResults.GetDate(0);
                        Model.BatchNoBlock.AccountingPeriod = batchesCurResults.GetString(1);
                    }
                    jeHeaderElement.JournalDate = Model.BatchNoBlock.BatchDate;
                    jeHeaderElement.BatchNo = TaskServices.NameIn(":global." + TaskServices.CurrentForm + "_batch_no");
                    jeHeaderElement.BatchYear = TaskServices.NameIn(":global." + TaskServices.CurrentForm + "_batch_year");
                    //  dph alio-14278 
                    #region loop for cursor getProfileCur
                    try
                    {
                        getProfileCur.Open();
                        while (true)
                        {
                            TableRow curRec = getProfileCur.FetchRow();
                            if (getProfileCur.NotFound) break;
                            this.Model.Params.UseOrderNo = curRec.GetString("profile_data");
                        }
                    }
                    finally
                    {
                        getProfileCur.Close();
                    }
                    #endregion
                    //  06/12/17 psr alio-15315 Added controlling using of whs_order_no or Reference no assignment button and field.
                    ItemServices.GoItem("je_header.journal_date");
                    //  Move to someother item
                    if (Lib.IsNull(this.Model.Params.UseOrderNo, "N") == "Y")
                    {
                        ItemServices.SetVisible("je_header.whs_order_no", true);
                        ItemServices.SetEnabled("je_header.whs_order_no", true);
                        //  06/13/17 psr alio-15315 Commented changes to assign_reference and reference_no
                        // SET_ITEM_PROPERTY('je_header.assign_reference', visible, property_false);
                        // SET_ITEM_PROPERTY('je_header.assign_reference', enabled, property_false);
                        // SET_ITEM_PROPERTY('je_header.reference_no',     visible, property_false);
                        // SET_ITEM_PROPERTY('je_header.reference_no',     enabled, property_false);
                        //  06/13/17 psr alio-15315 Commented do not want to go to je_header.whs_order_no
                        ItemServices.GoItem("je_header.whs_order_no");
                    }
                    else
                    {
                        //  06/13/17 psr alio-15315 Commented changes to assign_reference and reference_no
                        // SET_ITEM_PROPERTY('je_header.assign_reference', visible, property_true);
                        // SET_ITEM_PROPERTY('je_header.assign_reference', enabled, property_true);
                        // SET_ITEM_PROPERTY('je_header.reference_no',     visible, property_true);
                        // SET_ITEM_PROPERTY('je_header.reference_no',     enabled, property_true);
                        ItemServices.SetVisible("je_header.whs_order_no", false);
                        ItemServices.SetEnabled("je_header.whs_order_no", false);
                    }
                    //  06/13/17 psr alio-15315 Moved go_item() after if nvl(:parameter.use_order_no,'N') = 'Y' 
                    ItemServices.GoItem("je_header.assign_reference");
                    Task.Services.PopulateBatchTotals(jeHeaderElement);

                    Task.Packages.AccountingArray.AccountingTabInit();

                    // cec alio-3074 
                    //  09/04/14 alio-3074 klb added profileto determine if journal account amounts should be validated against budget 

                    Model.Params.JournalBudgetCheck = Lib.IsNull(Lib.Trim(StoredProcedures.FGetProfileData("JOURNAL_BUDGET_CHECK")), "N").ToString();

                    //  dph 3109 

                    Model.Params.JournalApproval = Lib.IsNull(Lib.Trim(StoredProcedures.FGetProfileData("JOURNAL_APPROVAL")), "N").ToString();

                    Task.Packages.Hold.referenceTable.Clear();
                    if (Model.Params.JournalApproval == "N")
                    {
                        ItemServices.SetVisible("je_header.approval_status_desc", false);
                        ItemServices.SetVisible("je_header.view_button", false);
                        ItemServices.SetVisible("je_header.ready_for_approval", false);
                    }
                    BlockServices.SetRecordStatus(1, "je_header", RecordStatus.NEW);
                }
                finally
                {
                    batchesCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER FJENT01A.POST-FORM
        /*
         begin 

 if :parameter.journal_approval = 'Y' then
 build_approval_chain;
 end if;

forms_ddl('begin ' 
        || 'update fas.batches ' 
        || 'set access_flag = ' 
        || '''' 
        || 'Y' 
        || '''' 
        || ' where batch_no = ' 
        || '''' 
        || :batch_no_block.batch_no 
        || '''' 
        || ' and ' 
        || 'batch_year = ' 
        || '''' 
        || :batch_no_block.batch_year 
        || '''' 
        || ';' 
        || 'commit;' 
        || 'end;'); 


end;   
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:431
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJENT01A.POST-FORM</TriggerInfo>

        [TaskEnded]
        public virtual void Fjent01a_TaskEnded(System.EventArgs args)
        {
            if (this.Model.Params.JournalApproval == "Y")
            {
                this.Task.Services.BuildApprovalChain();
            }
            Lib.FormsDDL("begin " + "update fas.batches " + "set access_flag = " + "'" + "Y" + "'" + " where batch_no = " + "'" + Model.BatchNoBlock.BatchNo + "'" + " and " + "batch_year = " + "'" + Model.BatchNoBlock.BatchYear + "'" + "; end;");
            DbManager.Commit();
        }


        #region Original PL/SQL code code for TRIGGER FJENT01A.ON-COMMIT
        /*
         begin 
--   qtsi_util.on_commit; 
if :system.form_status <> 'QUERY' then 
  update fas.batches 
     set posted_flag = 'EN' 
   where batch_no = :batch_no_block.batch_no and 
         batch_year = :batch_no_block.batch_year; 
  commit_form; 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:432
        /// 
        ///</remarks>
        /// <TriggerInfo>FJENT01A.ON-COMMIT</TriggerInfo>

        [BeforeDatabaseCommit]
        public virtual void Fjent01a_OnCommit()
        {

            int rowCount = 0;
            //    qtsi_util.on_commit; 
            if (TaskServices.FormStatus != "QUERY")
            {
                #region Execute data command
                String sql1 = "UPDATE fas.batches " +
"SET posted_flag = 'EN' " +
" WHERE batch_no = @BATCH_NO_BLOCK_BATCH_NO AND batch_year = @BATCH_NO_BLOCK_BATCH_YEAR";
                DataCommand command1 = new DataCommand(sql1);
                //Setting query parameters
                command1.AddParameter("@BATCH_NO_BLOCK_BATCH_NO", Model.BatchNoBlock.BatchNo);
                command1.AddParameter("@BATCH_NO_BLOCK_BATCH_YEAR", Model.BatchNoBlock.BatchYear);
                rowCount = command1.Execute();
                #endregion
                //TaskServices.Commit();
            }
        }


        #region Original PL/SQL code code for TRIGGER FJENT01A.PRE-POPUP-MENU
        /*
         -- dph added if statement for button security
-- iconic buttons would not work so if the item_type is a button call security from here
-- otherwise process as normal.

DECLARE
v_item_type   varchar2(200);
pl_id         ParamList;

BEGIN

    -- initialize the menu property set approval to non visible
     set_menu_item_property('EDIT.Sep_02',visible,PROPERTY_FALSE);
     set_menu_item_property('EDIT.approve_item',visible,PROPERTY_FALSE);


v_item_type := Get_Item_Property(:system.current_block||'.'||:system.current_item
                  ,ITEM_TYPE);


if v_item_type = 'BUTTON' then
    if :parameter.properties_menu = 'Y'  then

  pl_id := Get_Parameter_List('item_prop');

      IF NOT Id_Null(pl_id) THEN 
    Destroy_Parameter_List( pl_id );
  END IF;

pl_id := Create_Parameter_List('item_prop');

Add_Parameter(pl_id, 'MENU_SELECTION', TEXT_PARAMETER, :system.current_form||'FMX');
Add_Parameter(pl_id, 'BLOCK_NAME',     TEXT_PARAMETER, :system.current_block);
Add_Parameter(pl_id, 'ITEM_NAME',      TEXT_PARAMETER, :system.current_item);
Add_Parameter(pl_id, 'ROLE_ID',        TEXT_PARAMETER, :GLOBAL.qtsi_role_id);
Add_Parameter(pl_id, 'X_POS',          TEXT_PARAMETER, get_item_property(:system.current_block||'.'||:system.current_item, x_pos));
Add_Parameter(pl_id, 'Y_POS',          TEXT_PARAMETER, get_item_property(:system.current_block||'.'||:system.current_item, y_pos));

call_form('APITM01B', 
         NO_HIDE, 
         NO_REPLACE, 
         NO_QUERY_ONLY,
         pl_id); 

-- raise so we don't bring up the broken menu         
raise form_trigger_failure;            
end if;

else	
  Handle_Item_Approval;
end if;	                 	


-- original code
--Handle_Item_Approval;
--
END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:433
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger FJENT01A.PRE-POPUP-MENU . See documentation for details.
        ///</remarks>
        /// <TriggerInfo>FJENT01A.PRE-POPUP-MENU</TriggerInfo>

        [ActionTrigger(Action = "PRE-POPUP-MENU")]
        public virtual void Fjent01a_PrePopupMenu()
        {

            //  dph added if statement for button security
            //  iconic buttons would not work so if the item_type is a button call security from here
            //  otherwise process as normal.
            string vItemType = string.Empty;
            Hashtable plId = null;

            MenuServices.SetMenuItemProperty("EDIT.Sep_02", MenuItemProperty.VISIBLE, false);
            MenuServices.SetMenuItemProperty("EDIT.approve_item", MenuItemProperty.VISIBLE, false);

            vItemType = ItemServices.GetType(TaskServices.CurrentBlock + "." + TaskServices.CurrentItem);
            if (vItemType == "BUTTON")
            {
                if (this.Model.Params.PropertiesMenu == "Y")
                {
                    plId = AbstractTask.Current.Model.Params.GetParameterList("item_prop");
                    if (!((plId == null)))
                    {
                        AbstractTask.Current.Model.Params.DeleteParameterList(plId);
                    }
                    plId = AbstractTask.Current.Model.Params.CreateParameterList("item_prop");
                    AbstractTask.Current.Model.Params.AddParameter(plId, "MENU_SELECTION", TaskServices.CurrentForm + "FMX");
                    AbstractTask.Current.Model.Params.AddParameter(plId, "BLOCK_NAME", TaskServices.CurrentBlock);
                    AbstractTask.Current.Model.Params.AddParameter(plId, "ITEM_NAME", TaskServices.CurrentItem);
                    AbstractTask.Current.Model.Params.AddParameter(plId, "ROLE_ID", Alio.Common.Globals.QtsiRoleId);
                    AbstractTask.Current.Model.Params.AddParameter(plId, "X_POS", ItemServices.GetXPos(TaskServices.CurrentBlock + "." + TaskServices.CurrentItem));
                    AbstractTask.Current.Model.Params.AddParameter(plId, "Y_POS", ItemServices.GetYPos(TaskServices.CurrentBlock + "." + TaskServices.CurrentItem));
                    TaskServices.CallForm(null, AbstractTask.Current, "APITM01B", false, null);
                    //  raise so we don't bring up the broken menu         
                    throw new ApplicationException();
                }
            }
            else
            {

                //handleItemApproval;
                TaskServices.ExecuteAction("Handle_Item_Approval");
            }
        }



        #endregion

    }

}
