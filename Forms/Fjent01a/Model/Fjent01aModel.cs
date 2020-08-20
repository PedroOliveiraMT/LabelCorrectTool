
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Foundations.Core.Types;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjent01a.Model {

	public partial class Fjent01aModel: AbstractComposableModel<Fjent01aTask, Fjent01aModelParameters> {

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
		public BatchNoBlock BatchNoBlock  {
			get {
                return Get<BatchNoBlock>();
			}
		}

		public Fjent01aModel() : this((Hashtable)null) {
		}
		
		protected Fjent01aModel(string name) : base(name) {
        }

		public Fjent01aModel(Hashtable parameters) : base(parameters)	{
		}
	}

	#region Parameters
	public partial class Fjent01aModelParameters: ParameterCollection  {
		public Fjent01aModelParameters() {
		}
		
		public Fjent01aModelParameters(Hashtable p)  {
			this.InitParameters();
			this.Init(p);
		}
		
		protected override void InitParameters() {
			this.AddParameter("QTSI_ROLE_ID", NString.Null);
			this.AddParameter("HOLD_ACCT_ID", NString.Null);
			this.AddParameter("JOURNAL_APPROVAL", NString.Null);
			this.AddParameter("JOURNAL_BUDGET_CHECK", NString.Null);
			this.AddParameter("SHOW_MESSAGE","N");	
			this.AddParameter("USE_ORDER_NO", NString.Null);
			this.AddParameter("PROPERTIES_MENU","N");	
		}

		public NString QtsiRoleId
		{
			get { return NString.Parse(base["QTSI_ROLE_ID"]); }
			set { base["QTSI_ROLE_ID"] = value; }
		}
		public NString HoldAcctId
		{
			get { return NString.Parse(base["HOLD_ACCT_ID"]); }
			set { base["HOLD_ACCT_ID"] = value; }
		}
		public NString JournalApproval
		{
			get { return NString.Parse(base["JOURNAL_APPROVAL"]); }
			set { base["JOURNAL_APPROVAL"] = value; }
		}
		public NString JournalBudgetCheck
		{
			get { return NString.Parse(base["JOURNAL_BUDGET_CHECK"]); }
			set { base["JOURNAL_BUDGET_CHECK"] = value; }
		}
		public NString ShowMessage
		{
			get { return NString.Parse(base["SHOW_MESSAGE"]); }
			set { base["SHOW_MESSAGE"] = value; }
		}
		public NString UseOrderNo
		{
			get { return NString.Parse(base["USE_ORDER_NO"]); }
			set { base["USE_ORDER_NO"] = value; }
		}
		public NString PropertiesMenu
		{
			get { return NString.Parse(base["PROPERTIES_MENU"]); }
			set { base["PROPERTIES_MENU"] = value; }
		}
		
	}		
	#endregion
}

