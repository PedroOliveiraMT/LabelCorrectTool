using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fjent01a.Model {

    public partial class JeHeaderAdapter : BaseRowAdapter {
		
		public JeHeaderAdapter() {
		}
		
		public JeHeaderAdapter(JeHeaderManager businessObject) {
			this.BusinessObject = businessObject;
		}


		public JeHeaderAdapter(DataRow row, JeHeaderManager businessObject) {
			this.BusinessObject = businessObject;
			this.Row = row;
		}
		
		public void CopyTo(JeHeaderAdapter target)
		{
			target.Row.ItemArray = this.Row.ItemArray;
		}
		
		
		#region Data Columns
		public NString ReferenceNo {
			get { return GetStringValue("ReferenceNo"); }
            set { SetValue("ReferenceNo", value); }
		}
		public NDate JournalDate {
			get { return GetDateValue("JournalDate"); }
            set { SetValue("JournalDate", value); }
		}
		public NString JournalDescription {
			get { return GetStringValue("JournalDescription"); }
            set { SetValue("JournalDescription", value); }
		}
		public NString ReadyForApproval {
			get { return GetStringValue("ReadyForApproval"); }
            set { SetValue("ReadyForApproval", value); }
		}
		public NString ApprovalStatusDesc {
			get { return GetStringValue("ApprovalStatusDesc"); }
            set { SetValue("ApprovalStatusDesc", value); }
		}
		public NString BatchYear {
			get { return GetStringValue("BatchYear"); }
            set { SetValue("BatchYear", value); }
		}
		public NString BatchNo {
			get { return GetStringValue("BatchNo"); }
            set { SetValue("BatchNo", value); }
		}
		public NNumber DebitTotal {
			get { return GetNumberValue("DebitTotal"); }
            set { SetValue("DebitTotal", value); }
		}
		public NNumber CreditTotal {
			get { return GetNumberValue("CreditTotal"); }
            set { SetValue("CreditTotal", value); }
		}
		public NString WhsOrderNo {
			get { return GetStringValue("WhsOrderNo"); }
            set { SetValue("WhsOrderNo", value); }
		}
		public NNumber NextLineNo {
			get { return GetNumberValue("NextLineNo"); }
            set { SetValue("NextLineNo", value); }
		}
		public NString ApprovalStatus {
			get { return GetStringValue("ApprovalStatus"); }
            set { SetValue("ApprovalStatus", value); }
		}
		public NString SystemOwner {
			get { return GetStringValue("SystemOwner"); }
            set { SetValue("SystemOwner", value); }
		}
		public NString DisplayReferenceNo {
			get { return GetStringValue("DisplayReferenceNo"); }
            set { SetValue("DisplayReferenceNo", value); }
		}
		public NString ApprovalChain {
			get { return GetStringValue("ApprovalChain"); }
            set { SetValue("ApprovalChain", value); }
		}
		#endregion
	}
}
