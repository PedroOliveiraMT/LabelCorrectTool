using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Slact01a.Model {

    public partial class AccountsAdapter : BaseRowAdapter {
		
		public AccountsAdapter() {
		}
		
		public AccountsAdapter(AccountsManager businessObject) {
			this.BusinessObject = businessObject;
		}


		public AccountsAdapter(DataRow row, AccountsManager businessObject) {
			this.BusinessObject = businessObject;
			this.Row = row;
		}
		
		public void CopyTo(AccountsAdapter target)
		{
			target.Row.ItemArray = this.Row.ItemArray;
		}
		
		
		#region Data Columns
		public NString AccountNo {
			get { return GetStringValue("AccountNo"); }
            set { SetValue("AccountNo", value); }
		}
		public NNumber AccountId {
			get { return GetNumberValue("AccountId"); }
            set { SetValue("AccountId", value); }
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
