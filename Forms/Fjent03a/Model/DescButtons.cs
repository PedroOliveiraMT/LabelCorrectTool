using System;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Composition.Metadata;
using Foundations.Core.Types;


namespace Alio.Forms.Fjent03a.Model {
	public partial class DescButtons : Foundations.Flavors.Forms.Model.SimpleBusinessObject
	{
		public DescButtons(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata) {
		}

		public event EventHandler DescriptionTypeChanged;

		private NString _descriptionType;
		public NString DescriptionType {
			get { return _descriptionType; }
            set { 
				_descriptionType = value; 
				DescriptionTypeChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	
	}
}
