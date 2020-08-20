
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Foundations.Core.Types;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fmbth01a.Model {

	public partial class Fmbth01aModel: AbstractComposableModel<Fmbth01aTask, Fmbth01aModelParameters> {

		public BatchesManager Batches  {
			get {
                return Get<BatchesManager>();
			}
		}
		public Hold Hold  {
			get {
                return Get<Hold>();
			}
		}

		public Fmbth01aModel() : this((Hashtable)null) {
		}
		
		protected Fmbth01aModel(string name) : base(name) {
        }

		public Fmbth01aModel(Hashtable parameters) : base(parameters)	{
		}
	}

	#region Parameters
	public partial class Fmbth01aModelParameters: ParameterCollection  {
		public Fmbth01aModelParameters() {
		}
		
		public Fmbth01aModelParameters(Hashtable p)  {
			this.InitParameters();
			this.Init(p);
		}
		
		protected override void InitParameters() {
			this.AddParameter("ALLOW_PREFIX_BATCHES","NO");	
			this.AddParameter("SHOW_WARRANTS", NString.Null);
		}

		public NString AllowPrefixBatches
		{
			get { return NString.Parse(base["ALLOW_PREFIX_BATCHES"]); }
			set { base["ALLOW_PREFIX_BATCHES"] = value; }
		}
		public NString ShowWarrants
		{
			get { return NString.Parse(base["SHOW_WARRANTS"]); }
			set { base["SHOW_WARRANTS"] = value; }
		}
		
	}		
	#endregion
}

