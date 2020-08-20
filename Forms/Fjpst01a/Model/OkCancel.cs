using System;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Composition.Metadata;
using Foundations.Core.Types;


namespace Alio.Forms.Fjpst01a.Model {
	public partial class OkCancel : Foundations.Flavors.Forms.Model.SimpleBusinessObject
	{
		public OkCancel(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata) {
		}

		public event EventHandler SelectAllChanged;

		private NString _selectAll;
		public NString SelectAll {
			get { return _selectAll; }
            set { 
				_selectAll = value; 
				SelectAllChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	
	}
}
