using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fjgen01a.Model {

    public partial class JournalsAdapter : BaseRowAdapter {
		
		public JournalsAdapter() {
		}
		
		public JournalsAdapter(JournalsManager businessObject) {
			this.BusinessObject = businessObject;
		}


		public JournalsAdapter(DataRow row, JournalsManager businessObject) {
			this.BusinessObject = businessObject;
			this.Row = row;
		}
		
		public void CopyTo(JournalsAdapter target)
		{
			target.Row.ItemArray = this.Row.ItemArray;
		}
		
		
		#region Data Columns
		public NString CheckKey {
			get { return GetStringValue("CheckKey"); }
            set { SetValue("CheckKey", value); }
		}
		public NString ClaimNo {
			get { return GetStringValue("ClaimNo"); }
            set { SetValue("ClaimNo", value); }
		}
		public NString ReferenceNo {
			get { return GetStringValue("ReferenceNo"); }
            set { SetValue("ReferenceNo", value); }
		}
		public NNumber ReferenceSeqNo {
			get { return GetNumberValue("ReferenceSeqNo"); }
            set { SetValue("ReferenceSeqNo", value); }
		}
		public NString ReferenceType {
			get { return GetStringValue("ReferenceType"); }
            set { SetValue("ReferenceType", value); }
		}
		public NString BatchYear {
			get { return GetStringValue("BatchYear"); }
            set { SetValue("BatchYear", value); }
		}
		public NString BatchNo {
			get { return GetStringValue("BatchNo"); }
            set { SetValue("BatchNo", value); }
		}
		public NString AccountPeriod {
			get { return GetStringValue("AccountPeriod"); }
            set { SetValue("AccountPeriod", value); }
		}
		public NDate JournalDate {
			get { return GetDateValue("JournalDate"); }
            set { SetValue("JournalDate", value); }
		}
		public NString JournalType {
			get { return GetStringValue("JournalType"); }
            set { SetValue("JournalType", value); }
		}
		public NNumber JournalAmount {
			get { return GetNumberValue("JournalAmount"); }
            set { SetValue("JournalAmount", value); }
		}
		public NNumber DebitCreditFlag {
			get { return GetNumberValue("DebitCreditFlag"); }
            set { SetValue("DebitCreditFlag", value); }
		}
		public NNumber JournalId {
			get { return GetNumberValue("JournalId"); }
            set { SetValue("JournalId", value); }
		}
		public NNumber JournalSeqNo {
			get { return GetNumberValue("JournalSeqNo"); }
            set { SetValue("JournalSeqNo", value); }
		}
		public NString JournalDescription {
			get { return GetStringValue("JournalDescription"); }
            set { SetValue("JournalDescription", value); }
		}
		public NNumber AccountId {
			get { return GetNumberValue("AccountId"); }
            set { SetValue("AccountId", value); }
		}
		public NString AccountType {
			get { return GetStringValue("AccountType"); }
            set { SetValue("AccountType", value); }
		}
		public NString AccountNo {
			get { return GetStringValue("AccountNo"); }
            set { SetValue("AccountNo", value); }
		}
		public NString AccountYear {
			get { return GetStringValue("AccountYear"); }
            set { SetValue("AccountYear", value); }
		}
		public NString AccountDesc {
			get { return GetStringValue("AccountDesc"); }
            set { SetValue("AccountDesc", value); }
		}
		#endregion
	}
}
