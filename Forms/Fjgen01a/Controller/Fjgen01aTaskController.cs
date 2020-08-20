using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjgen01a.Model;
using Alio.Libraries.Sutl01a;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using System;

namespace Alio.Forms.Fjgen01a.Controllers
{

    public class Fjgen01aTaskController : AlioBaseTaskController<Fjgen01aTask, Fjgen01aModel>
    {

        public Fjgen01aTaskController(Fjgen01aTask task) : base(task)
        {
        }



        #region event handlers generated from Forms triggers

        #region Original PL/SQL code code for TRIGGER FJGEN01A.WHEN-NEW-FORM-INSTANCE
        /*
         sutl01a.run_prg('ssutl01a.fmx','2');
if :global.run_program = 'Y' then
sutl01a.when_new_form_instance;   
else
 message ('Invalid counter key, program terminated');
 message (' ',no_acknowledge);
 exit_form(no_commit);
end if;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:165
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJGEN01A.WHEN-NEW-FORM-INSTANCE</TriggerInfo>

        [TaskStarted]
        public virtual void Fjgen01a_TaskStarted(System.EventArgs args)
        {
            try
            {
                //TODO: MISSING FORM (OUT OF PILOT SCOPE)
                //System.Diagnostics.Debugger.Break();
                this.Task.Libraries.Sutl01a.Sutl01a.RunPrg("ssutl01a.fmx", "2");
            }
            catch (Exception ex)
            {
                Alio.Common.Globals.RunProgram = "Y";
            }

            if (Alio.Common.Globals.RunProgram == "Y")
            {
                Task.Libraries.Sutl01a.Sutl01a.WhenNewFormInstance();
            }
            else
            {
                TaskServices.Message("Invalid counter key, program terminated");
                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                TaskServices.ExitTask(TaskServices.NO_COMMIT);
            }
        }

        #endregion

    }

}
