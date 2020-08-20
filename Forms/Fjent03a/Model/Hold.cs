using System;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Composition.Metadata;
using Foundations.Core.Types;


namespace Alio.Forms.Fjent03a.Model {
	public partial class Hold : Foundations.Flavors.Forms.Model.SimpleBusinessObject
	{
		public Hold(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata) {
		}

		public event EventHandler HoldDescChanged;

		private NString _holdDesc;
		public NString HoldDesc {
			get { return _holdDesc; }
            set { 
				_holdDesc = value; 
				HoldDescChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	
	}
}
