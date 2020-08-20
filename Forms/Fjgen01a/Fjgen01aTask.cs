
	
using System.Collections;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjgen01a
{
    public partial class Fjgen01aTask : AbstractComposableTask<Alio.Forms.Fjgen01a.Services.Fjgen01aServices, Alio.Forms.Fjgen01a.Model.Fjgen01aModel>
	{
			
		public Fjgen01aTask () : this(new Alio.Forms.Fjgen01a.Model.Fjgen01aModel(null))
		{
		}
		
		public Fjgen01aTask (Hashtable libraries, Hashtable parameters)
           : this(new Alio.Forms.Fjgen01a.Model.Fjgen01aModel(parameters)) 
        {
			this.LibraryProvider = new Foundations.Core.AppSupportLib.Util.LibraryProvider(libraries);
        }

        public Fjgen01aTask (Hashtable parameters)
            : this(new Alio.Forms.Fjgen01a.Model.Fjgen01aModel(parameters))
        {
		}

		public Fjgen01aTask (Alio.Forms.Fjgen01a.Model.Fjgen01aModel model) : base(model)
        {
			this.Blocks = new Fjgen01aTaskBlocks(this);
            this.Canvases = new Fjgen01aTaskCanvases(this);
            this.Windows = new Fjgen01aTaskWindows(this);
			this.Libraries = new Fjgen01aTaskLibraries(this);
			this.Packages = new Fjgen01aTaskPackages(this);
            this.Alerts = new Fjgen01aTaskAlerts(this);
			InitAdditionalComponents();
		}

		partial void InitAdditionalComponents();

		public new Fjgen01aTaskBlocks Blocks { get; private set; }
        public Fjgen01aTaskCanvases Canvases { get; private set; }
        public Fjgen01aTaskWindows Windows { get; private set; }
        public new Fjgen01aTaskLibraries Libraries { get; private set; }
        public new Fjgen01aTaskPackages Packages { get; private set; }
        public new Fjgen01aTaskAlerts Alerts { get; private set; }
        
		public partial class Fjgen01aTaskLibraries
        {
            public Fjgen01aTaskLibraries(Fjgen01aTask task)
            {
				Sutl01a = task.FindLibrary<Libraries.Sutl01a.Sutl01aServices>("SUTL01A");
				Afilerpt = task.FindLibrary<Libraries.Afilerpt.AfilerptServices>("AFILERPT");
				Scla01a = task.FindLibrary<Libraries.Scla01a.Scla01aServices>("SCLA01A");
            }

			public Libraries.Sutl01a.Sutl01aServices Sutl01a { get; private set; }
			public Libraries.Afilerpt.AfilerptServices Afilerpt { get; private set; }
			public Libraries.Scla01a.Scla01aServices Scla01a { get; private set; }
        }
        
		public partial class Fjgen01aTaskPackages
        {
            public Fjgen01aTaskPackages(Fjgen01aTask task)
            {
				ValidateAccount = new Alio.Common.Parts.PkgValidateAccount();
            }

			public Alio.Common.Parts.PkgValidateAccount ValidateAccount { get; private set; }
        }
	}
}

