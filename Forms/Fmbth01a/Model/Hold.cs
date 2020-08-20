using System;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Composition.Metadata;
using Foundations.Core.Types;


namespace Alio.Forms.Fmbth01a.Model {
	public partial class Hold : Foundations.Flavors.Forms.Model.SimpleBusinessObject
	{
		public Hold(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata) {
		}

		public event EventHandler AccountPeriodChanged;

		private NString _accountPeriod;
		public NString AccountPeriod {
			get { return _accountPeriod; }
            set { 
				_accountPeriod = value; 
				AccountPeriodChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	
	}
}
