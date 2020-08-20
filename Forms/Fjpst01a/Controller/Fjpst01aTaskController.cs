using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjpst01a.Model;
using Alio.Libraries.Sutl01a;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;

namespace Alio.Forms.Fjpst01a.Controllers
{

    public class Fjpst01aTaskController : AlioBaseTaskController<Fjpst01aTask, Fjpst01aModel>
    {

        public Fjpst01aTaskController(Fjpst01aTask task) : base(task)
        {
        }



        #region event handlers generated from Forms triggers


        #region Original PL/SQL code code for TRIGGER FJPST01A.WHEN-NEW-FORM-INSTANCE
        /*
         begin
sutl01a.when_new_form_instance;
do_key('execute_query');
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:153
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJPST01A.WHEN-NEW-FORM-INSTANCE</TriggerInfo>

        [TaskStarted]
        public virtual void Fjpst01a_TaskStarted(System.EventArgs args)
        {

            Task.Libraries.Sutl01a.Sutl01a.WhenNewFormInstance();
            TaskServices.ExecuteAction("EXECUTE_QUERY");
        }



        #endregion

    }

}
