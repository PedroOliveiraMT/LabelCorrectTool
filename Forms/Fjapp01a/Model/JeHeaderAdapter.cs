using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fjapp01a.Model {

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
		public NString BatchYear {
			get { return GetStringValue("BatchYear"); }
            set { SetValue("BatchYear", value); }
		}
		public NString BatchNo {
			get { return GetStringValue("BatchNo"); }
            set { SetValue("BatchNo", value); }
		}
		public NDate BatchDate {
			get { return GetDateValue("BatchDate"); }
            set { SetValue("BatchDate", value); }
		}
		public NString Requestor {
			get { return GetStringValue("Requestor"); }
            set { SetValue("Requestor", value); }
		}
		public NString UserName {
			get { return GetStringValue("UserName"); }
            set { SetValue("UserName", value); }
		}
		public NString DisapprovalMessage {
			get { return GetStringValue("DisapprovalMessage"); }
            set { SetValue("DisapprovalMessage", value); }
		}
		public NString JournalDescription {
			get { return GetStringValue("JournalDescription"); }
            set { SetValue("JournalDescription", value); }
		}
		public NString UserId {
			get { return GetStringValue("UserId"); }
            set { SetValue("UserId", value); }
		}
		public NString AccountingPeriod {
			get { return GetStringValue("AccountingPeriod"); }
            set { SetValue("AccountingPeriod", value); }
		}
		#endregion
	}
}
