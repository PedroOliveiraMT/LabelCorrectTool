
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Foundations.Core.Types;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjpst01a.Model {

	public partial class Fjpst01aModel: AbstractComposableModel<Fjpst01aTask, ParameterCollection> {

		public BatchesManager Batches  {
			get {
                return Get<BatchesManager>();
			}
		}
		public OkCancel OkCancel  {
			get {
                return Get<OkCancel>();
			}
		}

		public Fjpst01aModel() : this((Hashtable)null) {
		}
		
		protected Fjpst01aModel(string name) : base(name) {
        }

		public Fjpst01aModel(Hashtable parameters) : base(parameters)	{
		}
	}

}

