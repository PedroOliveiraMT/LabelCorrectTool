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
using Foundations.Core.AppSupportLib.Exceptions;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Task;
using Foundations.Core.AppSupportLib.Runtime.ControlsModel.Items;
using Foundations.Core.AppSupportLib.Runtime.ControlsModel.Containers;
using Foundations.Flavors.Forms;
using Foundations.Core.Types;

using Alio.Forms.Ssutl01a.Controllers;
using Alio.Common;
using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Common.Parts;
using Alio.Libraries.Sutl01a;
using Alio.Libraries.Afilerpt;
using Alio.Forms.Ssutl01a.Model;

namespace Alio.Forms.Ssutl01a.Services {
	public class Ssutl01aServices : AbstractServices<Ssutl01aModel>{
		
		public Ssutl01aServices(Ssutl01aModel model):base(model)
		{
		}
		
		public new Ssutl01aTask Task
		{
			get { return (Ssutl01aTask)base.Task; }
		}
		
		#region Original PL/SQL code for Prog Unit VALIDATE_COUNTER_KEY
		/*
		PROCEDURE Validate_Counter_Key IS
	-- 09/26/16 psr alio-11584 Created procedure to Validate Counter Key with different algorithm
	--                         Common proc. called by Key_Enter and WBP trigger
	hold_counter_key Varchar2(8);
 --	v_key            Number;
	

BEGIN
		:global.run_program := 'N';	
	IF :security_block.counter_key = '753951' Then -- 05/08/17 psr alio-15253 Changed short cut number
	--IF :security_block.counter_key = '2546' Then -- "alio" short cut number
		:global.run_program := 'Y';	
	ELSE
	
	  hold_counter_key := get_key;

		if :security_block.counter_key = hold_counter_key then
  		:global.run_program := 'Y';	
		end if;
	END IF;

 if :global.run_program = 'Y' then
 	 insert into adm.program_log(
 	   file_name,
 	   user_id,
 	   run_date,
 	   action_code)
 	 values(
 	   'ssutl01a access granted',
 	    user,
 	    sysdate,
 	    'IN');
 	 commit;
 end if;


		<multilinecomment>
		
		IF LENGTH(TRIM(TRANSLATE(substr(:security_block.security_key, 2), ' +-.0123456789', ' '))) is Null Then -- this is a number
			v_key := To_Number(substr(:security_block.security_key, 2));
			hold_counter_key := To_Char(Round(v_key * 3.14, 0));
			If :security_block.counter_key = hold_counter_key then
				:global.run_program := 'Y';	
			Else
				:global.run_program := 'N';
			End if;
		Else
			:global.run_program := 'N';
		End if;
	End if;
		</multilinecomment>
	

	exit_form(no_commit);
END;
		*/
		#endregion
		//ID:82
		/// <summary></summary>
		/// <remarks>
		/// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
		/// </remarks>
		public virtual void ValidateCounterKey()
		{
			int rowCount = 0;
			//  09/26/16 psr alio-11584 Created procedure to Validate Counter Key with different algorithm
			//                          Common proc. called by Key_Enter and WBP trigger
			string holdCounterKey = string.Empty;
			Alio.Common.Globals.RunProgram = "N";
			if ( Model.SecurityBlock.CounterKey == "753951" )
			{
				//  05/08/17 psr alio-15253 Changed short cut number
				// IF :security_block.counter_key = '2546' Then -- "alio" short cut number
				Alio.Common.Globals.RunProgram = "Y";
			}
			else {
				holdCounterKey = FGetKey();
				if ( Model.SecurityBlock.CounterKey == holdCounterKey )
				{
					Alio.Common.Globals.RunProgram = "Y";
				}
			}
			if ( Alio.Common.Globals.RunProgram == "Y" )
			{
				#region Execute data command
				String sql1 = "INSERT INTO adm.program_log " +
	"(file_name, user_id, run_date, action_code)" +
	"VALUES ('ssutl01a access granted', user, sysdate, 'IN')";
				DataCommand command1 = new DataCommand(sql1);
				rowCount = command1.Execute();
				#endregion
TaskServices.Commit();
			}
			// IF LENGTH(TRIM(TRANSLATE(substr(:security_block.security_key, 2), ' +-.0123456789', ' '))) is Null Then -- this is a number
			// v_key := To_Number(substr(:security_block.security_key, 2));
			// hold_counter_key := To_Char(Round(v_key * 3.14, 0));
			// If :security_block.counter_key = hold_counter_key then
			// :global.run_program := 'Y';
			// Else
			// :global.run_program := 'N';
			// End if;
			// Else
			// :global.run_program := 'N';
			// End if;
			// End if;
			TaskServices.ExitTask(TaskServices.NO_COMMIT);
		}

		
		#region Original PL/SQL code for Prog Unit GET_KEY
		/*
		FUNCTION get_key RETURN varchar2 IS
 v_start       number;
 v_increment   number;
 v_day         number := to_char(sysdate,'dd');
 v_month       number := to_char(sysdate,'mm');
 v_counter_key varchar2(10); -- varchar to allow for leading zeros
BEGIN
  
  	v_start := substr((:security_block.security_key * v_day * v_month), 1,10);
    v_increment := substr(v_start,5,1);
    v_increment := ascii(v_increment);

    if substr(v_day,-1) in (0,2,4,6,8) then
      v_counter_key := substr((v_increment||v_start),1,8);
    elsif substr(v_day,-1) in (1,3,5,7,9) then
      v_counter_key := substr((v_start||v_increment),-8);
    end if;
  
   --message('dphx '||v_start||'  key '|| v_counter_key);
   --message (' ',no_acknowledge);
  
    return  v_counter_key;
  
END;
		*/
		#endregion
		//ID:83
		/// <summary></summary>
		/// <remarks>
		/// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
		/// </remarks>
		public virtual string FGetKey()
		{
			NNumber vStart = NNumber.Null;
			NNumber vIncrement = NNumber.Null;
			NNumber vDay = Lib.ToChar(NDate.Now, "dd");
			NNumber vMonth = Lib.ToChar(NDate.Now, "mm");
			string vCounterKey = string.Empty;
			vStart = Lib.Substring(((Model.SecurityBlock.SecurityKey.ToDouble() * vDay * vMonth)), 1, 10);
			vIncrement = Lib.Substring(vStart, 5, 1);
			vIncrement = Lib.Ascii(vIncrement);
			if ( (Lib.Substring(vDay, - (1)) == 0 || Lib.Substring(vDay, - (1)) == 2 || Lib.Substring(vDay, - (1)) == 4 || Lib.Substring(vDay, - (1)) == 6 || Lib.Substring(vDay, - (1)) == 8) )
			{
				vCounterKey = Lib.Substring(((vIncrement + vStart)), 1, 8).ToString();
			}
			else if ( (Lib.Substring(vDay, - (1)) == 1 || Lib.Substring(vDay, - (1)) == 3 || Lib.Substring(vDay, - (1)) == 5 || Lib.Substring(vDay, - (1)) == 7 || Lib.Substring(vDay, - (1)) == 9) ) {
				vCounterKey = Lib.Substring(((vStart + vIncrement)), - (8)).ToString();
			}
			// message('dphx '||v_start||'  key '|| v_counter_key);
			// message (' ',no_acknowledge);
			return vCounterKey;
		}

		
		

	}
}
