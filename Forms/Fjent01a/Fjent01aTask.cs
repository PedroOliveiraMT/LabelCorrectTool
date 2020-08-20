
	
using System.Collections;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjent01a
{
    public partial class Fjent01aTask : AbstractComposableTask<Alio.Forms.Fjent01a.Services.Fjent01aServices, Alio.Forms.Fjent01a.Model.Fjent01aModel>
	{
			
		public Fjent01aTask () : this(new Alio.Forms.Fjent01a.Model.Fjent01aModel(null))
		{
		}
		
		public Fjent01aTask (Hashtable libraries, Hashtable parameters)
           : this(new Alio.Forms.Fjent01a.Model.Fjent01aModel(parameters)) 
        {
			this.LibraryProvider = new Foundations.Core.AppSupportLib.Util.LibraryProvider(libraries);
        }

        public Fjent01aTask (Hashtable parameters)
            : this(new Alio.Forms.Fjent01a.Model.Fjent01aModel(parameters))
        {
		}

		public Fjent01aTask (Alio.Forms.Fjent01a.Model.Fjent01aModel model) : base(model)
        {
			this.Blocks = new Fjent01aTaskBlocks(this);
            this.Canvases = new Fjent01aTaskCanvases(this);
            this.Windows = new Fjent01aTaskWindows(this);
			this.Libraries = new Fjent01aTaskLibraries(this);
			this.Packages = new Fjent01aTaskPackages(this);
            this.Alerts = new Fjent01aTaskAlerts(this);
            this.Lovs = new Fjent01aTaskLovs(this);
			InitAdditionalComponents();
		}

		partial void InitAdditionalComponents();

		public new Fjent01aTaskBlocks Blocks { get; private set; }
        public Fjent01aTaskCanvases Canvases { get; private set; }
        public Fjent01aTaskWindows Windows { get; private set; }
        public new Fjent01aTaskLibraries Libraries { get; private set; }
        public new Fjent01aTaskPackages Packages { get; private set; }
        public new Fjent01aTaskAlerts Alerts { get; private set; }
        public new Fjent01aTaskLovs Lovs { get; private set; }
        
		public partial class Fjent01aTaskLibraries
        {
            public Fjent01aTaskLibraries(Fjent01aTask task)
            {
				Afilerpt = task.FindLibrary<Libraries.Afilerpt.AfilerptServices>("AFILERPT");
				Sutl01a = task.FindLibrary<Libraries.Sutl01a.Sutl01aServices>("SUTL01A");
				Scla01a = task.FindLibrary<Libraries.Scla01a.Scla01aServices>("SCLA01A");
            }

			public Libraries.Afilerpt.AfilerptServices Afilerpt { get; private set; }
			public Libraries.Sutl01a.Sutl01aServices Sutl01a { get; private set; }
			public Libraries.Scla01a.Scla01aServices Scla01a { get; private set; }
        }
        
		public partial class Fjent01aTaskPackages
        {
            public Fjent01aTaskPackages(Fjent01aTask task)
            {
				ValidateAccount = new Alio.Common.Parts.PkgValidateAccount();
				AccountingArray = new Alio.Common.Parts.PkgAccountingArray();
				Hold = new Alio.Forms.Fjent01a.Services.PkgHold(task.Model);
            }

			public Alio.Common.Parts.PkgValidateAccount ValidateAccount { get; private set; }
			public Alio.Common.Parts.PkgAccountingArray AccountingArray { get; private set; }
			public Alio.Forms.Fjent01a.Services.PkgHold Hold { get; private set; }
        }
	}
}

