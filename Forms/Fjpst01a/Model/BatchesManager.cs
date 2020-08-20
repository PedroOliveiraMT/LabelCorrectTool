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

namespace Alio.Forms.Fjpst01a.Model{
	public partial class BatchesManager: ComposableDBBusinessObject<BatchesAdapter>
	{
		public BatchesManager(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata)
        {
        }

	}
}

