using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Foundations.Core.AppDataLayer;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.ControlsModel.Items;
using Foundations.Core.AppSupportLib.Runtime.ControlsModel.Containers;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Task;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Exceptions;
using Foundations.Core.AppSupportLib.UI;
using Foundations.Core.AppSupportLib.Util.IO;
using Foundations.Flavors.Forms;
using Foundations.Core.Types;

using Alio.Common;
using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Common.Parts;
using Alio.Libraries.Sutl01a;
using Alio.Libraries.Afilerpt;

using Alio.Forms.Ssutl01a.Services;
using Alio.Forms.Ssutl01a.Model;


namespace Alio.Forms.Ssutl01a.Controllers {


    public class SecurityBlockController : BaseBlockController<Ssutl01aTask,Ssutl01aModel> {
	
		public SecurityBlockController(ITaskController parentController, String name)
            : base(parentController, name)
		{
		}
		
				
				
		#region event handlers generated from Forms triggers
			#region Original PL/SQL code code for TRIGGER SECURITY_BLOCK.KEY-ENTER
			/*
			   Validate_Counter_Key;

<multilinecomment> -- 09/26/16 psr alio-11584 No longer using this code
declare
	
	hold_counter_key varchar2(8);
	hold_digits_1 varchar2(4);
	hold_digits_2 varchar2(4);
	hold_key_1 varchar2(8);
	negative_ix1 number := -1;
	hold_date varchar2(4);
	hold_counter_key_2 varchar2(8);
begin
	hold_digits_1 := substr(:security_block.security_key,1,4);
	hold_digits_2 := substr(:security_block.security_key,5,4);
	hold_key_1 := rpad(to_char(to_number(hold_digits_1 * hold_digits_2)),8,'0');  -- fails due to alpha in security_key
  loop
  	exit when negative_ix1 <  -8;
  	hold_counter_key := hold_counter_key || substr(hold_key_1,negative_ix1,1);
  	negative_ix1 := negative_ix1 + -1;
  end loop;
   hold_date := to_char(:security_block.security_date,'MMDD');
  hold_counter_key_2 := substr(hold_date,4,1) || substr(hold_date,3,1) || '7009' ||
                        substr(hold_date,2,1) || substr(hold_date,1,1); 
	if :security_block.counter_key in ('SECUREQT',hold_counter_key,hold_counter_key_2) then
		 :global.run_program := 'Y';	
	else
		 :global.run_program := 'N';
	end if;
	exit_form(no_commit);
end;
</multilinecomment>
	
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:15
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger SECURITY_BLOCK.KEY-ENTER . See documentation for details.
			///</remarks>
			/// <TriggerInfo>SECURITY_BLOCK.KEY-ENTER</TriggerInfo>

			[ActionTrigger(Action="KEY-ENTER")]
			public virtual void securityBlock_KeyEnter()
			{
				this.Task.Services.ValidateCounterKey();
			}

			
			#region Original PL/SQL code code for TRIGGER PROCESS.WHEN-BUTTON-PRESSED
			/*
			   Validate_Counter_Key;

<multilinecomment> -- 09/26/16 psr alio-11584 No longer using this code
declare
	
	hold_counter_key varchar2(8);
	hold_counter_key_2 varchar2(8);
	hold_digits_1 varchar2(4);
	hold_digits_2 varchar2(4);
	hold_key_1 varchar2(8);
	negative_ix1 number := -1;
	hold_date varchar2(8);
begin
  hold_date := to_char(:security_block.security_date,'MMDD');
 -- hold_counter_key_2 := substr(hold_date,4,1) || substr(hold_date,3,1) || '7009' || -- 10/22 changed per jag
 --                       substr(hold_date,2,1) || substr(hold_date,1,1);
   hold_counter_key_2 := substr(hold_date,4,1) || substr(hold_date,3,1) ||'%' ||
                         substr(hold_date,2,1) || substr(hold_date,1,1); 
  hold_counter_key := trunc((to_number(substr(:security_block.security_key,2,8)) 
                              * to_number(substr(hold_date,3,2)))
                              / to_number(substr(hold_date,1,2))); 
                        
	--if :security_block.counter_key in ('ALLOWIDI',hold_counter_key) or
	if :security_block.counter_key in ('ALLOWIDI',hold_counter_key) or
		 :security_block.counter_key like hold_counter_key_2 then
		 :global.run_program := 'Y';	
	else
		 :global.run_program := 'N';
	end if;
	exit_form(no_commit);
end;
</multilinecomment>
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:13
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>PROCESS.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="PROCESS")]
			public virtual void process_buttonClick()
			{
				this.Task.Services.ValidateCounterKey();
			}

			
		
		#endregion		
		
	}
}
