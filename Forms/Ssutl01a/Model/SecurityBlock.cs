using System;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Composition.Metadata;
using Foundations.Core.Types;


namespace Alio.Forms.Ssutl01a.Model {
	public partial class SecurityBlock : Foundations.Flavors.Forms.Model.SimpleBusinessObject
	{
		public SecurityBlock(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata) {
		}

		public event EventHandler SecurityDateChanged;
		public event EventHandler SecurityKeyChanged;
		public event EventHandler CounterKeyChanged;
		public event EventHandler WaiverKeyChanged;

		private NDate _securityDate;
		public NDate SecurityDate {
			get { return _securityDate; }
            set { 
				_securityDate = value; 
				SecurityDateChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _securityKey;
		public NString SecurityKey {
			get { return _securityKey; }
            set { 
				_securityKey = value; 
				SecurityKeyChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _counterKey;
		public NString CounterKey {
			get { return _counterKey; }
            set { 
				_counterKey = value; 
				CounterKeyChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _waiverKey;
		public NString WaiverKey {
			get { return _waiverKey; }
            set { 
				_waiverKey = value; 
				WaiverKeyChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	
	}
}
