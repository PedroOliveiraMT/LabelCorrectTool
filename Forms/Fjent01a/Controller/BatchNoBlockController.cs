using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjent01a.Model;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using System;

namespace Alio.Forms.Fjent01a.Controllers
{


    public class BatchNoBlockController : BaseBlockController<Fjent01aTask, Fjent01aModel> {
	
		public BatchNoBlockController(ITaskController parentController, String name)
            : base(parentController, name)
		{
		}
		
				
				
		#region event handlers generated from Forms triggers
		
		#endregion		
		
	}
}
