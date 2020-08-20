
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Foundations.Core.Types;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Ssutl01a.Model {

	public partial class Ssutl01aModel: AbstractComposableModel<Ssutl01aTask, ParameterCollection> {

		public SecurityBlock SecurityBlock  {
			get {
                return Get<SecurityBlock>();
			}
		}

		public Ssutl01aModel() : this((Hashtable)null) {
		}
		
		protected Ssutl01aModel(string name) : base(name) {
        }

		public Ssutl01aModel(Hashtable parameters) : base(parameters)	{
		}
	}

}

