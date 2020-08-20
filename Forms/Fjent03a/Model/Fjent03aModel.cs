
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Foundations.Core.Types;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjent03a.Model {

	public partial class Fjent03aModel: AbstractComposableModel<Fjent03aTask, Fjent03aModelParameters> {

		public RecurJeHeaderManager RecurJeHeader  {
			get {
                return Get<RecurJeHeaderManager>();
			}
		}
		public RecurJeDataManager RecurJeData  {
			get {
                return Get<RecurJeDataManager>();
			}
		}
		public DescButtons DescButtons  {
			get {
                return Get<DescButtons>();
			}
		}
		public BatchNoBlock BatchNoBlock  {
			get {
                return Get<BatchNoBlock>();
			}
		}
		public Hold Hold  {
			get {
                return Get<Hold>();
			}
		}

		public Fjent03aModel() : this((Hashtable)null) {
		}
		
		protected Fjent03aModel(string name) : base(name) {
        }

		public Fjent03aModel(Hashtable parameters) : base(parameters)	{
		}
	}

	#region Parameters
	public partial class Fjent03aModelParameters: ParameterCollection  {
		public Fjent03aModelParameters() {
		}
		
		public Fjent03aModelParameters(Hashtable p)  {
			this.InitParameters();
			this.Init(p);
		}
		
		protected override void InitParameters() {
			this.AddParameter("QTSI_ROLE_ID", NString.Null);
			this.AddParameter("JOURNAL_APPROVAL", NString.Null);
			this.AddParameter("HOLD_ACCT_ID", NString.Null);
			this.AddParameter("JOURNAL_BUDGET_CHECK", NString.Null);
			this.AddParameter("PROPERTIES_MENU","N");	
		}

		public NString QtsiRoleId
		{
			get { return NString.Parse(base["QTSI_ROLE_ID"]); }
			set { base["QTSI_ROLE_ID"] = value; }
		}
		public NString JournalApproval
		{
			get { return NString.Parse(base["JOURNAL_APPROVAL"]); }
			set { base["JOURNAL_APPROVAL"] = value; }
		}
		public NString HoldAcctId
		{
			get { return NString.Parse(base["HOLD_ACCT_ID"]); }
			set { base["HOLD_ACCT_ID"] = value; }
		}
		public NString JournalBudgetCheck
		{
			get { return NString.Parse(base["JOURNAL_BUDGET_CHECK"]); }
			set { base["JOURNAL_BUDGET_CHECK"] = value; }
		}
		public NString PropertiesMenu
		{
			get { return NString.Parse(base["PROPERTIES_MENU"]); }
			set { base["PROPERTIES_MENU"] = value; }
		}
		
	}		
	#endregion
}

