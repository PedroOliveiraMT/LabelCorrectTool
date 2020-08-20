using System;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Composition.Metadata;
using Foundations.Core.Types;


namespace Alio.Forms.Fjent03a.Model {
	public partial class BatchNoBlock : Foundations.Flavors.Forms.Model.SimpleBusinessObject
	{
		public BatchNoBlock(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata) {
		}

		public event EventHandler BatchDateChanged;
		public event EventHandler BatchYearChanged;
		public event EventHandler BatchNoChanged;
		public event EventHandler AccountingPeriodChanged;

		private NDate _batchDate;
		public NDate BatchDate {
			get { return _batchDate; }
            set { 
				_batchDate = value; 
				BatchDateChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _batchYear;
		public NString BatchYear {
			get { return _batchYear; }
            set { 
				_batchYear = value; 
				BatchYearChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _batchNo;
		public NString BatchNo {
			get { return _batchNo; }
            set { 
				_batchNo = value; 
				BatchNoChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _accountingPeriod;
		public NString AccountingPeriod {
			get { return _accountingPeriod; }
            set { 
				_accountingPeriod = value; 
				AccountingPeriodChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	
	}
}
