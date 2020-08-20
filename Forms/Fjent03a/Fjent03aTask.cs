
	
using System.Collections;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjent03a
{
    public partial class Fjent03aTask : AbstractComposableTask<Alio.Forms.Fjent03a.Services.Fjent03aServices, Alio.Forms.Fjent03a.Model.Fjent03aModel>
	{
			
		public Fjent03aTask () : this(new Alio.Forms.Fjent03a.Model.Fjent03aModel(null))
		{
		}
		
		public Fjent03aTask (Hashtable libraries, Hashtable parameters)
           : this(new Alio.Forms.Fjent03a.Model.Fjent03aModel(parameters)) 
        {
			this.LibraryProvider = new Foundations.Core.AppSupportLib.Util.LibraryProvider(libraries);
        }

        public Fjent03aTask (Hashtable parameters)
            : this(new Alio.Forms.Fjent03a.Model.Fjent03aModel(parameters))
        {
		}

		public Fjent03aTask (Alio.Forms.Fjent03a.Model.Fjent03aModel model) : base(model)
        {
			this.Blocks = new Fjent03aTaskBlocks(this);
            this.Canvases = new Fjent03aTaskCanvases(this);
            this.Windows = new Fjent03aTaskWindows(this);
			this.Libraries = new Fjent03aTaskLibraries(this);
			this.Packages = new Fjent03aTaskPackages(this);
            this.Alerts = new Fjent03aTaskAlerts(this);
            this.Lovs = new Fjent03aTaskLovs(this);
			InitAdditionalComponents();
		}

		partial void InitAdditionalComponents();

		public new Fjent03aTaskBlocks Blocks { get; private set; }
        public Fjent03aTaskCanvases Canvases { get; private set; }
        public Fjent03aTaskWindows Windows { get; private set; }
        public new Fjent03aTaskLibraries Libraries { get; private set; }
        public new Fjent03aTaskPackages Packages { get; private set; }
        public new Fjent03aTaskAlerts Alerts { get; private set; }
        public new Fjent03aTaskLovs Lovs { get; private set; }
        
		public partial class Fjent03aTaskLibraries
        {
            public Fjent03aTaskLibraries(Fjent03aTask task)
            {
				Sutl01a = task.FindLibrary<Libraries.Sutl01a.Sutl01aServices>("SUTL01A");
				Afilerpt = task.FindLibrary<Libraries.Afilerpt.AfilerptServices>("AFILERPT");
				Scla01a = task.FindLibrary<Libraries.Scla01a.Scla01aServices>("SCLA01A");
            }

			public Libraries.Sutl01a.Sutl01aServices Sutl01a { get; private set; }
			public Libraries.Afilerpt.AfilerptServices Afilerpt { get; private set; }
			public Libraries.Scla01a.Scla01aServices Scla01a { get; private set; }
        }
        
		public partial class Fjent03aTaskPackages
        {
            public Fjent03aTaskPackages(Fjent03aTask task)
            {
				Hold = new Alio.Forms.Fjent03a.Services.PkgHold(task.Model);
				ValidateAccount = new Alio.Common.Parts.PkgValidateAccount();
				AccountingArray = new Alio.Common.Parts.PkgAccountingArray();
            }

			public Alio.Forms.Fjent03a.Services.PkgHold Hold { get; private set; }
			public Alio.Common.Parts.PkgValidateAccount ValidateAccount { get; private set; }
			public Alio.Common.Parts.PkgAccountingArray AccountingArray { get; private set; }
        }
	}
}

