using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Slact01a.Model {

    public partial class NickAccountsAdapter : BaseRowAdapter {
		
		public NickAccountsAdapter() {
		}
		
		public NickAccountsAdapter(NickAccountsManager businessObject) {
			this.BusinessObject = businessObject;
		}


		public NickAccountsAdapter(DataRow row, NickAccountsManager businessObject) {
			this.BusinessObject = businessObject;
			this.Row = row;
		}
		
		public void CopyTo(NickAccountsAdapter target)
		{
			target.Row.ItemArray = this.Row.ItemArray;
		}
		
		
		#region Data Columns
		public NString AccountNickNo {
			get { return GetStringValue("AccountNickNo"); }
            set { SetValue("AccountNickNo", value); }
		}
		public NString UserId {
			get { return GetStringValue("UserId"); }
            set { SetValue("UserId", value); }
		}
		public NString SysGenFlag {
			get { return GetStringValue("SysGenFlag"); }
            set { SetValue("SysGenFlag", value); }
		}
		public NNumber AccountId {
			get { return GetNumberValue("AccountId"); }
            set { SetValue("AccountId", value); }
		}
		#endregion
	}
}
