using System;
using System.Data;
using System.Threading; 

using Foundations.Core.AppDataLayer;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Composition.Metadata;

using Foundations.Core.Types;

namespace Alio.Forms.Slact01a.Model{
	public partial class AccountsManager: ComposableDBBusinessObject<AccountsAdapter>
	{
		public AccountsManager(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata)
        {
        }

	}
}

