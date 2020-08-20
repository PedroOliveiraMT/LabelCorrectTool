using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fjpst01a.Model {

    public partial class BatchesAdapter : BaseRowAdapter {
		
		public BatchesAdapter() {
		}
		
		public BatchesAdapter(BatchesManager businessObject) {
			this.BusinessObject = businessObject;
		}


		public BatchesAdapter(DataRow row, BatchesManager businessObject) {
			this.BusinessObject = businessObject;
			this.Row = row;
		}
		
		public void CopyTo(BatchesAdapter target)
		{
			target.Row.ItemArray = this.Row.ItemArray;
		}
		
		
		#region Data Columns
		public NString BatchYear {
			get { return GetStringValue("BatchYear"); }
            set { SetValue("BatchYear", value); }
		}
		public NString BatchNo {
			get { return GetStringValue("BatchNo"); }
            set { SetValue("BatchNo", value); }
		}
		public NDate DateCreated {
			get { return GetDateValue("DateCreated"); }
            set { SetValue("DateCreated", value); }
		}
		public NDate BatchDate {
			get { return GetDateValue("BatchDate"); }
            set { SetValue("BatchDate", value); }
		}
		public NString BatchDescription {
			get { return GetStringValue("BatchDescription"); }
            set { SetValue("BatchDescription", value); }
		}
		public NString AccountPeriod {
			get { return GetStringValue("AccountPeriod"); }
            set { SetValue("AccountPeriod", value); }
		}
		public NString PostBatch {
			get { return GetStringValue("PostBatch"); }
            set { SetValue("PostBatch", value); }
		}
		public NNumber DebitTotal {
			get { return GetNumberValue("DebitTotal"); }
            set { SetValue("DebitTotal", value); }
		}
		public NNumber CreditTotal {
			get { return GetNumberValue("CreditTotal"); }
            set { SetValue("CreditTotal", value); }
		}
		public NString PostedFlag {
			get { return GetStringValue("PostedFlag"); }
            set { SetValue("PostedFlag", value); }
		}
		public NString UserId {
			get { return GetStringValue("UserId"); }
            set { SetValue("UserId", value); }
		}
		public NString BatchType {
			get { return GetStringValue("BatchType"); }
            set { SetValue("BatchType", value); }
		}
		public NString BatchOrigin {
			get { return GetStringValue("BatchOrigin"); }
            set { SetValue("BatchOrigin", value); }
		}
		public NString AccessFlag {
			get { return GetStringValue("AccessFlag"); }
            set { SetValue("AccessFlag", value); }
		}
		#endregion
	}
}
