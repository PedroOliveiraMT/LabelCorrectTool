using Alio.Common;
using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjent03a.Model;
using Alio.Libraries.Sutl01a;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Task;
using System;
using System.Collections;

namespace Alio.Forms.Fjent03a.Controllers
{

    public class Fjent03aTaskController : AlioBaseTaskController<Fjent03aTask, Fjent03aModel>
    {

        public Fjent03aTaskController(Fjent03aTask task) : base(task)
        {
        }



        #region event handlers generated from Forms triggers




        #region Original PL/SQL code code for TRIGGER FJENT03A.WHEN-NEW-FORM-INSTANCE
        /*
         declare 
cursor batches_cur is 
select batch_date, 
   account_period 
 from fas.batches 
where batch_no = :batch_no_block.batch_no and 
      batch_year = :batch_no_block.batch_year; 

begin 

default_value ('*', 'global.debug_status'); 
sutl01a.when_new_form_instance; 

handle_item_properties;  -- uses adm.user_item_properties     --mfl alio-14830 19.2

copy('JE' 
   ,'global.' 
   || :system.current_form 
   || '_batch_type'); 
synchronize; 

sutl01a.run_prg('fmbth01a.fmx','3'); 

default_value(null, 
             'global.' 
             || :system.current_form 
             || '_batch_no'); 
if name_in(':global.' 
         || :system.current_form 
         || '_batch_no') is null then 
  exit_form; -- If they didn't choose a batch in Batch Master 
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
-- set debugging globals 
:recur_je_header.journal_date := :batch_no_block.batch_date; 
accounting_array.accounting_tab_init; --cec alio-3074 

-- dph 3109 
:parameter.journal_approval        := nvl(trim(adm.get_profile_data('JOURNAL_APPROVAL')),'N'); 
hold.reference_table.delete;

if :parameter.journal_approval = 'N' then                                               --mfl alio-16668
SET_ITEM_PROPERTY ('recur_je_header.ready_for_approval', visible,property_false);     --mfl alio-16668
end if;                                                                                 --mfl alio-16668

set_record_property(1 
                  ,'recur_je_header' 
                  ,status 
                  ,new_status); 

<multilinecomment> -- ***************************************************************************** 
--02/07/11 KLB alio-3421 Commented out disable of primary key.  Business users do not have 
--         permissions to disable/enable primary keys. Primary key dropped and non-unique one created. 
-- ***************************************************************************** 

--11/30/07 KLB Ref. No 043957 - Disable primary key to prevent prevent dup_value_on_index error 
DECLARE 
   v_pk_result BOOLEAN; 
BEGIN                      
   v_pk_result := set_primary_key_status('DISABLE');    
   IF v_pk_result = FALSE THEN 
      MESSAGE('Primary key on RECUR_JE_DATA could not be disabled.  '|| 
              'You may encounter problems resequencing.'); 
      MESSAGE(' ',no_acknowledge); 
   END IF;                    
END; 
-- *******  02/07/11 KLB alio-3421 end of code commented out***************************  
</multilinecomment>                         

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:420
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJENT03A.WHEN-NEW-FORM-INSTANCE</TriggerInfo>

        [TaskStarted]
        public virtual void Fjent03a_TaskStarted(System.EventArgs args)
        {

            RecurJeHeaderAdapter recurJeHeaderElement = this.Model.RecurJeHeader.CurrentRowAdapter;


            int rowCount = 0;
            {
                String sqlbatchesCur = "SELECT batch_date, account_period " +
" FROM fas.batches " +
" WHERE batch_no = @BATCH_NO_BLOCK_BATCH_NO AND batch_year = @BATCH_NO_BLOCK_BATCH_YEAR ";
                DataCursor batchesCur = new DataCursor(sqlbatchesCur);
                #region
                try
                {
                    Globals.SetDefault("global.debug_status", "*");
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
                    //  set debugging globals 
                    recurJeHeaderElement.JournalDate = Model.BatchNoBlock.BatchDate;
                    Task.Packages.AccountingArray.AccountingTabInit();
                    // cec alio-3074 
                    //  dph 3109 

                    Model.Params.JournalApproval = Lib.IsNull(Lib.Trim(StoredProcedures.FGetProfileData("JOURNAL_APPROVAL")), "N").ToString();


                    Task.Packages.Hold.referenceTable.Clear();
                    if (this.Model.Params.JournalApproval == "N")
                    {
                        // mfl alio-16668
                        ItemServices.SetVisible("recur_je_header.ready_for_approval", false);
                    }
                    // mfl alio-16668
                    BlockServices.SetRecordStatus(1, "recur_je_header", RecordStatus.NEW);
                }
                finally
                {
                    batchesCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER FJENT03A.POST-FORM
        /*
         begin 

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

<multilinecomment> -- ***************************************************************************** 
--02/07/11 KLB alio-3421 Commented out enable of primary key.  Business users do not have 
--         permissions to disable/enable primary keys. Primary key dropped and non-unique one created. 
-- ***************************************************************************** 

--11/30/07 KLB Ref. No 043957 - Enable primary key after changes are made (disabled in WHEN_NEW_FORM_INSTANCE) 
DECLARE 
   v_pk_result BOOLEAN; 
BEGIN                      
   v_pk_result := set_primary_key_status('ENABLE'); 
   IF v_pk_result = FALSE THEN 
      MESSAGE('Primary key on RECUR_JE_DATA could not be enabled.  '|| 
              'Please re-query form and check for duplicate line numbers.'); 
      MESSAGE(' ',no_acknowledge); 

   END IF;                       
END;         
-- *******  02/07/11 KLB alio-3421 end of code commented out***************************  
</multilinecomment>                         


end;   
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:421
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJENT03A.POST-FORM</TriggerInfo>

        [TaskEnded]
        public virtual void Fjent03a_TaskEnded(System.EventArgs args)
        {
            Lib.FormsDDL("begin " + "update fas.batches " + "set access_flag = " + "'" + "Y" + "'" + " where batch_no = " + "'" + Model.BatchNoBlock.BatchNo + "'" + " and " + "batch_year = " + "'" + Model.BatchNoBlock.BatchYear + "'" + "; end;");
            DbManager.Commit();
        }


        #region Original PL/SQL code code for TRIGGER FJENT03A.PRE-POPUP-MENU
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
        ///	ID:422
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger FJENT03A.PRE-POPUP-MENU . See documentation for details.
        ///</remarks>
        /// <TriggerInfo>FJENT03A.PRE-POPUP-MENU</TriggerInfo>

        [ActionTrigger(Action = "PRE-POPUP-MENU")]
        public virtual void Fjent03a_PrePopupMenu()
        {

            //  dph added if statement for button security
            //  iconic buttons would not work so if the item_type is a button call security from here
            //  otherwise process as normal.
            {
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
                    TaskServices.ExecuteAction("Handle_Item_Approval");
                }
            }
        }



        #endregion

    }

}
