
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Foundations.Core.Types;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjapp01a.Model {

	public partial class Fjapp01aModel: AbstractComposableModel<Fjapp01aTask, Fjapp01aModelParameters> {

		public JeHeaderManager JeHeader  {
			get {
                return Get<JeHeaderManager>();
			}
		}
		public JeDataManager JeData  {
			get {
                return Get<JeDataManager>();
			}
		}
		public JournalApprovalManager JournalApproval  {
			get {
                return Get<JournalApprovalManager>();
			}
		}

		public Fjapp01aModel() : this((Hashtable)null) {
		}
		
		protected Fjapp01aModel(string name) : base(name) {
        }

		public Fjapp01aModel(Hashtable parameters) : base(parameters)	{
		}
	}

	#region Parameters
	public partial class Fjapp01aModelParameters: ParameterCollection  {
		public Fjapp01aModelParameters() {
		}
		
		public Fjapp01aModelParameters(Hashtable p)  {
			this.InitParameters();
			this.Init(p);
		}
		
		protected override void InitParameters() {
			this.AddParameter("JOURNAL_APPROVAL", NString.Null);
		}

		public NString JournalApproval
		{
			get { return NString.Parse(base["JOURNAL_APPROVAL"]); }
			set { base["JOURNAL_APPROVAL"] = value; }
		}
		
	}		
	#endregion
}

