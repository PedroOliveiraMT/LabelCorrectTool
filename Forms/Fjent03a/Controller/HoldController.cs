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

using Alio.Forms.Fjent03a.Services;
using Alio.Forms.Fjent03a.Model;
using Alio.Common.Runtime.Controller;

namespace Alio.Forms.Fjent03a.Controllers {


    public class HoldController : BaseBlockController<Fjent03aTask,Fjent03aModel> {
	
		public HoldController(ITaskController parentController, String name)
            : base(parentController, name)
		{
		}
		
				
				
		#region event handlers generated from Forms triggers
		
		#endregion		
		
	}
}
