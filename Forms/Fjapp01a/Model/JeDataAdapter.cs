using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fjapp01a.Model {

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
		public NString DisplayAccount {
			get { return GetStringValue("DisplayAccount"); }
            set { SetValue("DisplayAccount", value); }
		}
		public NNumber AccountBalance {
			get { return GetNumberValue("AccountBalance"); }
            set { SetValue("AccountBalance", value); }
		}
		public NNumber DrAmount {
			get { return GetNumberValue("DrAmount"); }
            set { SetValue("DrAmount", value); }
		}
		public NNumber CrAmount {
			get { return GetNumberValue("CrAmount"); }
            set { SetValue("CrAmount", value); }
		}
		public NNumber DrTotal {
			get { return GetNumberValue("DrTotal"); }
            set { SetValue("DrTotal", value); }
		}
		public NNumber CrTotal {
			get { return GetNumberValue("CrTotal"); }
            set { SetValue("CrTotal", value); }
		}
		public NString ReferenceNo {
			get { return GetStringValue("ReferenceNo"); }
            set { SetValue("ReferenceNo", value); }
		}
		public NString JournalAmount {
			get { return GetStringValue("JournalAmount"); }
            set { SetValue("JournalAmount", value); }
		}
		public NNumber LineNo {
			get { return GetNumberValue("LineNo"); }
            set { SetValue("LineNo", value); }
		}
		public NNumber AccountId {
			get { return GetNumberValue("AccountId"); }
            set { SetValue("AccountId", value); }
		}
		public NNumber DebitCreditFlag {
			get { return GetNumberValue("DebitCreditFlag"); }
            set { SetValue("DebitCreditFlag", value); }
		}
		#endregion
	}
}
