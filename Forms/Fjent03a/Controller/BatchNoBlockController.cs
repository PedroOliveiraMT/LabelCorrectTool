using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjent03a.Model;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fjent03a.Controllers
{


    public class BatchNoBlockController : BaseBlockController<Fjent03aTask, Fjent03aModel>
    {

        public BatchNoBlockController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER SHOW_BAL.WHEN-BUTTON-PRESSED
        /*
         declare 
ref_balance number; 
ref_funds_in_balance varchar2(5); 
begin 
--   ref_balance := fjgen01a_.get_balance(:recur_je_header.batch_year 
--                                       ,:recur_je_header.batch_no 
--                                       ,:recur_je_header.reference_no); 
message ('The balance of this reference is: ' 
       || to_char(ref_balance)); 
--   ref_funds_in_balance := qtsi_fj01a.funds_in_balance 
--                           (:recur_je_header.batch_no 
--                           ,:recur_je_header.reference_no 
--                           ,'6'); 
message ('The status of Funds_In_Balance is: ' 
       || ref_funds_in_balance); 
--:batches.account_year); 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:88
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>SHOW_BAL.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "SHOW_BAL")]
        public virtual void showBal_buttonClick()
        {
            {
                NNumber refBalance = NNumber.Null;
                string refFundsInBalance = string.Empty;
                //    ref_balance := fjgen01a_.get_balance(:recur_je_header.batch_year 
                //                                        ,:recur_je_header.batch_no 
                //                                        ,:recur_je_header.reference_no); 
                TaskServices.Message("The balance of this reference is: " + Lib.ToChar(refBalance).ToString());
                //    ref_funds_in_balance := qtsi_fj01a.funds_in_balance 
                //                            (:recur_je_header.batch_no 
                //                            ,:recur_je_header.reference_no 
                //                            ,'6'); 
                TaskServices.Message("The status of Funds_In_Balance is: " + refFundsInBalance);
            }
        }



        #endregion

    }
}
