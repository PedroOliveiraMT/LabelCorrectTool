using System;
using System.Data;
using Foundations.Flavors.Forms.DataLayer;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.Types;

namespace Alio.Forms.Fmbth01a.Model {

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
		public NString Status {
			get { return GetStringValue("Status"); }
            set { SetValue("Status", value); }
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
		public NString PostedFlag {
			get { return GetStringValue("PostedFlag"); }
            set { SetValue("PostedFlag", value); }
		}
		public NString UserId {
			get { return GetStringValue("UserId"); }
            set { SetValue("UserId", value); }
		}
		public NString WarrantNo {
			get { return GetStringValue("WarrantNo"); }
            set { SetValue("WarrantNo", value); }
		}
		public NDate WarrantFromDate {
			get { return GetDateValue("WarrantFromDate"); }
            set { SetValue("WarrantFromDate", value); }
		}
		public NDate WarrantToDate {
			get { return GetDateValue("WarrantToDate"); }
            set { SetValue("WarrantToDate", value); }
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
		public NString NextKeysBatch {
			get { return GetStringValue("NextKeysBatch"); }
            set { SetValue("NextKeysBatch", value); }
		}
		#endregion
	}
}
