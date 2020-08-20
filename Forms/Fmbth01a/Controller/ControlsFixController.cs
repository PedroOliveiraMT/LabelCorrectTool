using Alio.Common.Runtime.Controller;
using Alio.Forms.Fmbth01a.Model;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fmbth01a.Controllers
{


    public class ControlsFixController : BaseBlockController<Fmbth01aTask, Fmbth01aModel>
    {

        public ControlsFixController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER QUERY.WHEN-BUTTON-PRESSED
        /*
         declare
save_changes number;
begin
<multilinecomment>   MESSAGE('BLOCK_STATUS: ' || :SYSTEM.BLOCK_STATUS);
if :System.Mode = 'NORMAL' and
  :system.block_status <> 'QUERY' then
   save_changes := show_alert('save_changes');
   if save_changes = alert_button1 then
      do_key('commit_form');
      if not form_failure then
         do_key('enter_query');
      end if;
   elsif save_changes = alert_button2 then
      clear_block(no_validate);
      do_key('enter_query');
   end if; </multilinecomment>
if :system.mode = 'NORMAL' then
   do_key('enter_query');
else
   do_key('execute_query');
end if;
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:129
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>QUERY.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "QUERY")]
        public void query_buttonClick()
        {
            {
                NNumber saveChanges = NNumber.Null;
                //    MESSAGE('BLOCK_STATUS: ' || :SYSTEM.BLOCK_STATUS);
                // if :System.Mode = 'NORMAL' and
                // :system.block_status <> 'QUERY' then
                // save_changes := show_alert('save_changes');
                // if save_changes = alert_button1 then
                // do_key('commit_form');
                // if not form_failure then
                // do_key('enter_query');
                // end if;
                // elsif save_changes = alert_button2 then
                // clear_block(no_validate);
                // do_key('enter_query');
                // end if; 
                if (TaskServices.Mode == "NORMAL")
                {
                    TaskServices.ExecuteAction("SEARCH");
                }
                else
                {
                    TaskServices.ExecuteAction("EXECUTE_QUERY");
                }
            }
        }



        #endregion

    }
}
