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

namespace Alio.Forms.Fjapp01a.Model{
	public partial class JeDataManager: ComposableDBBusinessObject<JeDataAdapter>
	{
		public JeDataManager(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata)
        {
        }

		// F2N_TODO item DrTotal assigned to summary SUM;
		// F2N_TODO item CrTotal assigned to summary SUM;
	}
}

