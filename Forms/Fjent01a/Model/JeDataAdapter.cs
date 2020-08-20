using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fjent01a.Model {

    public partial class JeDataAdapter : BaseRowAdapter {
		
		public JeDataAdapter() {
		}
		
		public JeDataAdapter(JeDataManager businessObject) {
			this.BusinessObject = businessObject;
		}


		public JeDataAdapter(DataRow row, JeDataManager businessObject) {
			this.BusinessObject = businessObject;
			this.Row = row;
		}
		
		public void CopyTo(JeDataAdapter target)
		{
			target.Row.ItemArray = this.Row.ItemArray;
		}
		
		
		#region Data Columns
		public NString AccountNo {
			get { return GetStringValue("AccountNo"); }
            set { SetValue("AccountNo", value); }
		}
		public NNumber DrAmount {
			get { return GetNumberValue("DrAmount"); }
            set { SetValue("DrAmount", value); }
		}
		public NNumber CrAmount {
			get { return GetNumberValue("CrAmount"); }
            set { SetValue("CrAmount", value); }
		}
		public NString ReferenceNo {
			get { return GetStringValue("ReferenceNo"); }
            set { SetValue("ReferenceNo", value); }
		}
		public NNumber LineNo {
			get { return GetNumberValue("LineNo"); }
            set { SetValue("LineNo", value); }
		}
		public NNumber JournalAmount {
			get { return GetNumberValue("JournalAmount"); }
            set { SetValue("JournalAmount", value); }
		}
		public NNumber AccountId {
			get { return GetNumberValue("AccountId"); }
            set { SetValue("AccountId", value); }
		}
		public NNumber DebitCreditFlag {
			get { return GetNumberValue("DebitCreditFlag"); }
            set { SetValue("DebitCreditFlag", value); }
		}
		public NNumber DrTotal {
			get { return GetNumberValue("DrTotal"); }
            set { SetValue("DrTotal", value); }
		}
		public NNumber CrTotal {
			get { return GetNumberValue("CrTotal"); }
            set { SetValue("CrTotal", value); }
		}
		public NString AccountDesc {
			get { return GetStringValue("AccountDesc"); }
            set { SetValue("AccountDesc", value); }
		}
		public NNumber HoldAccountId {
			get { return GetNumberValue("HoldAccountId"); }
            set { SetValue("HoldAccountId", value); }
		}
		public NNumber HoldJournalAmount {
			get { return GetNumberValue("HoldJournalAmount"); }
            set { SetValue("HoldJournalAmount", value); }
		}
		public NNumber HoldDrAmount {
			get { return GetNumberValue("HoldDrAmount"); }
            set { SetValue("HoldDrAmount", value); }
		}
		public NNumber HoldCrAmount {
			get { return GetNumberValue("HoldCrAmount"); }
            set { SetValue("HoldCrAmount", value); }
		}
		public NNumber AccountBalance {
			get { return GetNumberValue("AccountBalance"); }
            set { SetValue("AccountBalance", value); }
		}
		#endregion
	}
}
