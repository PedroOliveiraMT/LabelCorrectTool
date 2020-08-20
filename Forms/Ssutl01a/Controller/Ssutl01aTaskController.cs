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

	public class Ssutl01aTaskController: AlioBaseTaskController<Ssutl01aTask, Ssutl01aModel>  {
		
		public Ssutl01aTaskController(Ssutl01aTask task) :base(task)
		{
		}
			
				

		#region event handlers generated from Forms triggers
			#region Original PL/SQL code code for TRIGGER SSUTL01A.WHEN-NEW-FORM-INSTANCE
			/*
			 declare
	 cursor get_waiver_user  is
	 select count(1) count
	   from adm.profiles
	  where profile_key = 'WSC*SS-'||user;  -- profiles with a WSC* prefix are hidden   

begin
   sutl01a.when_new_form_instance;
   
    :security_block.security_key :=  ascii(substr('XX',1,1)) || lpad(to_char(sysdate,'J'),8,'0');


   if user = 'FAS' then
     set_item_property('waiver_key',visible,property_true);
     set_item_property('waiver_key',enabled,property_true);
    :security_block.waiver_key := get_key;
   else
   	 set_item_property('waiver_key',visible,property_false);
     set_item_property('waiver_key',enabled,property_false);
   end if;
 
 
  
    for cur_rec in get_waiver_user
    loop
    	
        
    	if cur_rec.count > 0 then
    		set_item_property('waiver_key',visible,property_true);
    		set_item_property('waiver_key',enabled,property_true);
    		:security_block.waiver_key := get_key;
    	end if;
    	
    end loop;    
    
    
    Erase('global.run_program');
    DEFAULT_VALUE('*', 'global.run_program');   
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:138
			/// 
			/// 
			///</remarks>
			/// <param name="args"></param>
			/// <TriggerInfo>SSUTL01A.WHEN-NEW-FORM-INSTANCE</TriggerInfo>

			[TaskStarted]
			public virtual void Ssutl01a_TaskStarted(System.EventArgs args)
			{
				
				int rowCount = 0;
				{
					String sqlgetWaiverUser = "SELECT count(1) count " +
	" FROM adm.profiles " +
	" WHERE profile_key = 'WSC*SS-' || user ";
					DataCursor getWaiverUser = new DataCursor(sqlgetWaiverUser);
					Task.Libraries.Sutl01a.Sutl01a.WhenNewFormInstance();
					Model.SecurityBlock.SecurityKey = Lib.Ascii(Lib.Substring("XX", 1, 1)).ToString() + Lib.Lpad(Lib.ToChar(NDate.Now, "J"), 8, "0");
					if ( DbManager.User == "FAS" )
					{
						ItemServices.SetVisible("waiver_key", true);
						ItemServices.SetEnabled("waiver_key", true);
						Model.SecurityBlock.WaiverKey = this.Task.Services.FGetKey();
					}
					else {
						ItemServices.SetVisible("waiver_key", false);
						ItemServices.SetEnabled("waiver_key", false);
					}
					#region loop for cursor getWaiverUser
					try
					{
						getWaiverUser.Open();
						while (true)
						{
							TableRow curRec = getWaiverUser.FetchRow();
							if ( getWaiverUser.NotFound ) break;
							if ( curRec.GetNumber("count") > 0 )
							{
								ItemServices.SetVisible("waiver_key", true);
								ItemServices.SetEnabled("waiver_key", true);
								Model.SecurityBlock.WaiverKey = this.Task.Services.FGetKey();
							}
						}
					}finally{
						getWaiverUser.Close();
					}
					#endregion
					Globals.Remove("global.run_program");
					Globals.SetDefault("global.run_program", "*");
				}
			}

			
		
		#endregion		
		
	}
	
}
