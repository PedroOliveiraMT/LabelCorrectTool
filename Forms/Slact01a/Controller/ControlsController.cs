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
using Alio.Forms.Common.DbServices;
using Alio.Libraries.Sutl01a;
using Alio.Libraries.Afilerpt;

using Alio.Forms.Slact01a.Services;
using Alio.Forms.Slact01a.Model;

namespace Alio.Forms.Slact01a.Controllers {


    public class ControlsController : AbstractBlockController<Slact01aTask,Slact01aModel> {
	
		public ControlsController(ITaskController parentController, String name)
            : base(parentController, name)
		{
		}
		
				
				
		#region event handlers generated from Forms triggers
			#region Original PL/SQL code code for TRIGGER CONTROLS.KEY-OTHERS
			/*
			 begin
   null;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:54
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger CONTROLS.KEY-OTHERS . See documentation for details.
			///</remarks>
			/// <TriggerInfo>CONTROLS.KEY-OTHERS</TriggerInfo>

			[ActionTrigger(Action="KEY-OTHERS")]
			public void controls_KeyOthers()
			{
			}

			
			#region Original PL/SQL code code for TRIGGER BUTTON_01.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.button_01;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:25
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>BUTTON_01.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="BUTTON_01")]
			public void button01_buttonClick()
			{
				Task.Libraries.Sutl01a.Sutl01a.Button01();	
			}

			
			#region Original PL/SQL code code for TRIGGER BUTTON_02.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.button_02;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:27
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>BUTTON_02.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="BUTTON_02")]
			public void button02_buttonClick()
			{
				PkgSutl01a.Button02();
			}

			
			#region Original PL/SQL code code for TRIGGER BUTTON_03.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.button_03;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:29
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>BUTTON_03.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="BUTTON_03")]
			public void button03_buttonClick()
			{
				PkgSutl01a.Button03();
			}

			
			#region Original PL/SQL code code for TRIGGER BUTTON_04.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.button_04;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:31
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>BUTTON_04.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="BUTTON_04")]
			public void button04_buttonClick()
			{
				PkgSutl01a.Button04();
			}

			
			#region Original PL/SQL code code for TRIGGER BUTTON_05.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.button_05;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:33
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>BUTTON_05.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="BUTTON_05")]
			public void button05_buttonClick()
			{
				PkgSutl01a.Button05();
			}

			
			#region Original PL/SQL code code for TRIGGER BUTTON_06.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.button_06;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:35
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>BUTTON_06.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="BUTTON_06")]
			public void button06_buttonClick()
			{
				Task.Libraries.Sutl01a.Sutl01a.Button06();
			}

			
			#region Original PL/SQL code code for TRIGGER BUTTON_07.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.button_07;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:37
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>BUTTON_07.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="BUTTON_07")]
			public void button07_buttonClick()
			{
				Task.Libraries.Sutl01a.Sutl01a.Button07();
			}

			
			#region Original PL/SQL code code for TRIGGER BUTTON_08.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.button_08;
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:39
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>BUTTON_08.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="BUTTON_08")]
			public void button08_buttonClick()
			{
				Task.Libraries.Sutl01a.Sutl01a.Button08();
			}

			
			#region Original PL/SQL code code for TRIGGER MAIN_MENU.WHEN-BUTTON-PRESSED
			/*
			 begin
	-- SCLA01A Database Version
  if :system.mode = 'ENTER-QUERY' then
     do_key('exit_form'); -- This cancels the query
  end if;
   
  default_value('*','global.menu_program_start');
  if :global.menu_program_start = 'ammen03a' then
    go_form('ammen03a');
  else
    go_form('ammen01a');
  end if;

end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:41
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>MAIN_MENU.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="MAIN_MENU")]
			public void mainMenu_buttonClick()
			{
				//  SCLA01A Database Version
				if ( TaskServices.Mode == "ENTER-QUERY" )
				{
					TaskServices.ExecuteAction("EXIT");
				}
				Globals.SetDefault("global.menu_program_start", "*");
				if ( Alio.Common.Globals.MenuProgramStart == "ammen03a" )
				{
					TaskServices.GoForm("ammen03a");
				}
				else {
					TaskServices.GoForm("ammen01a");
				}
			}

			
			#region Original PL/SQL code code for TRIGGER CLOSE.WHEN-BUTTON-PRESSED
			/*
			 begin
   sutl01a.close_window;
end;

			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:43
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>CLOSE.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="CLOSE")]
			public void close_buttonClick()
			{
				PkgSutl01a.CloseWindow();
			}

			
			#region Original PL/SQL code code for TRIGGER PROGRAM_LIST.WHEN-LIST-CHANGED
			/*
			 begin
	 execute_trigger('Handle_List_Master');
end;
			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:45
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>PROGRAM_LIST.WHEN-LIST-CHANGED</TriggerInfo>

			[ActionTrigger(Action="WHEN-LIST-CHANGED", Item="PROGRAM_LIST")]
			public void programList_listChange()
			{
				TaskServices.ExecuteAction("Handle_List_Master");
			}

			
			#region Original PL/SQL code code for TRIGGER PROGRAM_LIST.KEY-ENTER
			/*
			 begin
   
   execute_trigger('Handle_List_Master');
   --sutl01a.run_prg(:controls.program_list,1);

end;

			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:46
			/// 
			/// F2N_NOT_SUPPORTED : There is no mapping of trigger PROGRAM_LIST.KEY-ENTER . See documentation for details.
			///</remarks>
			/// <TriggerInfo>PROGRAM_LIST.KEY-ENTER</TriggerInfo>

			[ActionTrigger(Action="KEY-ENTER")]
			public void programList_KeyEnter()
			{
				TaskServices.ExecuteAction("Handle_List_Master");
			}

			
			#region Original PL/SQL code code for TRIGGER GO.WHEN-BUTTON-PRESSED
			/*
			 begin
   
   execute_trigger('Handle_List_Master');
   --sutl01a.run_prg(:controls.program_list,1);

end;

			*/
			#endregion
			/// <summary>
			/// </summary>
			/// <remarks>
			///	ID:48
			/// 
			/// 
			///</remarks>
			/// <TriggerInfo>GO.WHEN-BUTTON-PRESSED</TriggerInfo>

			[ActionTrigger(Action="WHEN-BUTTON-PRESSED", Item="GO")]
			public void go_buttonClick()
			{
				TaskServices.ExecuteAction("Handle_List_Master");
			}

			
		
		#endregion		
		
	}
}
