using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fjent03a.Model {

    public partial class RecurJeHeaderAdapter : BaseRowAdapter {
		
		public RecurJeHeaderAdapter() {
		}
		
		public RecurJeHeaderAdapter(RecurJeHeaderManager businessObject) {
			this.BusinessObject = businessObject;
		}


		public RecurJeHeaderAdapter(DataRow row, RecurJeHeaderManager businessObject) {
			this.BusinessObject = businessObject;
			this.Row = row;
		}
		
		public void CopyTo(RecurJeHeaderAdapter target)
		{
			target.Row.ItemArray = this.Row.ItemArray;
		}
		
		
		#region Data Columns
		public NString RecurJournalId {
			get { return GetStringValue("RecurJournalId"); }
            set { SetValue("RecurJournalId", value); }
		}
		public NString RecurJournalDesc {
			get { return GetStringValue("RecurJournalDesc"); }
            set { SetValue("RecurJournalDesc", value); }
		}
		public NString ReferenceNo {
			get { return GetStringValue("ReferenceNo"); }
            set { SetValue("ReferenceNo", value); }
		}
		public NString JournalDescription {
			get { return GetStringValue("JournalDescription"); }
            set { SetValue("JournalDescription", value); }
		}
		public NDate JournalDate {
			get { return GetDateValue("JournalDate"); }
            set { SetValue("JournalDate", value); }
		}
		public NNumber NextLineNo {
			get { return GetNumberValue("NextLineNo"); }
            set { SetValue("NextLineNo", value); }
		}
		public NString ApprovalChain {
			get { return GetStringValue("ApprovalChain"); }
            set { SetValue("ApprovalChain", value); }
		}
		public NString ReadyForApproval {
			get { return GetStringValue("ReadyForApproval"); }
            set { SetValue("ReadyForApproval", value); }
		}
		#endregion
	}
}
