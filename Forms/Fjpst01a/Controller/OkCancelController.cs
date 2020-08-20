using Alio.Common.Runtime.Controller;
using Alio.Forms.Common.DbServices;
using Alio.Forms.Fjpst01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Exceptions;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fjpst01a.Controllers
{


    public class OkCancelController : BaseBlockController<Fjpst01aTask, Fjpst01aModel>
    {

        public OkCancelController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER SELECT_ALL.WHEN-CHECKBOX-CHANGED
        /*

go_block('batches');
first_record;

if :OK_CANCEL.select_all = 'Y' then
  loop
    :batches.post_batch := 'Y';
    IF :System.Last_Record = 'TRUE' THEN 
       exit;
    ELSE
         next_record;
    END IF;
  end loop;
else
  loop
    :batches.post_batch := 'N';
    IF :System.Last_Record = 'TRUE' THEN 
       exit;
    ELSE
         next_record;
    END IF;
  end loop;
end if;

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:28
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>SELECT_ALL.WHEN-CHECKBOX-CHANGED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-CHECKBOX-CHANGED", Item = "SELECT_ALL")]
        public virtual void selectAll_checkBoxChange()
        {
            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;

            BlockServices.GoBlock("batches");
            BlockServices.FirstRecord();
            if (Model.OkCancel.SelectAll == "Y")
            {
                while (true)
                {
                    batchesElement.PostBatch = "Y";
                    if (BlockServices.InLastRecord)
                    {
                        break;
                    }
                    else
                    {
                        BlockServices.NextRecord();
                    }
                }
            }
            else
            {
                while (true)
                {
                    batchesElement.PostBatch = "N";
                    if (BlockServices.InLastRecord)
                    {
                        break;
                    }
                    else
                    {
                        BlockServices.NextRecord();
                    }
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER POST.WHEN-BUTTON-PRESSED
        /*
         declare
v_count number;
display_process_message varchar2(1);

begin
go_block('batches');
set_application_property(cursor_style,'BUSY');
display_process_message := 'N';
first_record;
loop
  if :batches.post_batch = 'Y' then
     fjpst01a_.post_je_data (:batches.batch_year
                            ,:batches.batch_no);
     display_process_message := 'Y';
  end if;
  exit when :system.last_record = 'TRUE';
  next_record;
end loop;

set_application_property(cursor_style,
                       'DEFAULT');

if display_process_message = 'Y' then
  message('Process Complete');
  message(' ',no_acknowledge);
end if;

go_block('ok_cancel');
clear_block;

go_block('Batches');
clear_block;
--   execute_query;

select count(*)
 into v_count
 from fas.batches
where batch_type in ('JE','PR') 
  and posted_flag = 'RP';

if v_count > 0 then
  execute_query;
else
  do_key('exit_form');
end if;

-- set_item_property('ok_cancel.post',
--                  enabled,
--                   property_false);
--set_item_property('ok_cancel.cancel',
--                  label,
--                  'Close');
--exit_form(do_commit);

exception    
when others then
  set_application_property(cursor_style,
                           'DEFAULT');
  message(sqlerrm);
  message(' ', no_acknowledge);
  raise form_trigger_failure;
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
        /// <TriggerInfo>POST.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "POST")]
        public virtual void post_buttonClick()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;


            int rowCount = 0;
            {
                NNumber vCount = NNumber.Null;
                string displayProcessMessage = string.Empty;
                try
                {
                    BlockServices.GoBlock("batches");
                    displayProcessMessage = "N";
                    BlockServices.FirstRecord();
                    while (true)
                    {
                        if (batchesElement.PostBatch == "Y")
                        {
                            Fjpst01a_.PostJeData(batchesElement.BatchYear, batchesElement.BatchNo);

                            displayProcessMessage = "Y";
                        }
                        if (BlockServices.InLastRecord)
                            break;
                        BlockServices.NextRecord();
                    }
                    if (displayProcessMessage == "Y")
                    {
                        TaskServices.Message("Process Complete");
                        TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    }
                    BlockServices.GoBlock("ok_cancel");
                    BlockServices.ClearBlock();
                    BlockServices.GoBlock("Batches");
                    BlockServices.ClearBlock();
                    //    execute_query;
                    #region Execute data command
                    String sql1 = "SELECT count(*) " +
" FROM fas.batches " +
" WHERE (batch_type) IN ('JE', 'PR') AND posted_flag = 'RP' ";
                    DataCommand command1 = new DataCommand(sql1);
                    ResultSet results1 = command1.ExecuteQuery();
                    rowCount = command1.RowCount;
                    if (results1.HasData)
                    {
                        vCount = results1.GetNumber(0);
                    }
                    results1.Close();
                    #endregion
                    if (vCount > 0)
                    {
                        BlockServices.ExecuteQuery();
                    }
                    else
                    {
                        TaskServices.ExecuteAction("EXIT");
                    }
                }
                catch (TaskControlException) { throw; }
                catch
                {
                    TaskServices.Message(DbManager.ErrorMessage);
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    throw new ApplicationException();
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER CANCEL.WHEN-BUTTON-PRESSED
        /*
         begin
do_key('exit_form');
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:32
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>CANCEL.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "CANCEL")]
        public virtual void cancel_buttonClick()
        {
            TaskServices.ExecuteAction("EXIT");
        }



        #endregion

    }
}
