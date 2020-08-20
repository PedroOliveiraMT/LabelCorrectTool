using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fjapp01a.Model {

    public partial class JournalApprovalAdapter : BaseRowAdapter {
		
		public JournalApprovalAdapter() {
		}
		
		public JournalApprovalAdapter(JournalApprovalManager businessObject) {
			this.BusinessObject = businessObject;
		}


		public JournalApprovalAdapter(DataRow row, JournalApprovalManager businessObject) {
			this.BusinessObject = businessObject;
			this.Row = row;
		}
		
		public void CopyTo(JournalApprovalAdapter target)
		{
			target.Row.ItemArray = this.Row.ItemArray;
		}
		
		
		#region Data Columns
		public NNumber ApproverSequence {
			get { return GetNumberValue("ApproverSequence"); }
            set { SetValue("ApproverSequence", value); }
		}
		public NString ApprovalCode {
			get { return GetStringValue("ApprovalCode"); }
            set { SetValue("ApprovalCode", value); }
		}
		public NString AppCodeDesc {
			get { return GetStringValue("AppCodeDesc"); }
            set { SetValue("AppCodeDesc", value); }
		}
		public NString StatusFlag {
			get { return GetStringValue("StatusFlag"); }
            set { SetValue("StatusFlag", value); }
		}
		public NString StatusDescription {
			get { return GetStringValue("StatusDescription"); }
            set { SetValue("StatusDescription", value); }
		}
		public NDate StatusDate {
			get { return GetDateValue("StatusDate"); }
            set { SetValue("StatusDate", value); }
		}
		public NString UserId {
			get { return GetStringValue("UserId"); }
            set { SetValue("UserId", value); }
		}
		public NString ReferenceNo {
			get { return GetStringValue("ReferenceNo"); }
            set { SetValue("ReferenceNo", value); }
		}
		#endregion
	}
}
