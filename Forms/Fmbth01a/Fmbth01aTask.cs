
	
using System.Collections;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fmbth01a
{
    public partial class Fmbth01aTask : AbstractComposableTask<Alio.Forms.Fmbth01a.Services.Fmbth01aServices, Alio.Forms.Fmbth01a.Model.Fmbth01aModel>
	{
			
		public Fmbth01aTask () : this(new Alio.Forms.Fmbth01a.Model.Fmbth01aModel(null))
		{
		}
		
		public Fmbth01aTask (Hashtable libraries, Hashtable parameters)
           : this(new Alio.Forms.Fmbth01a.Model.Fmbth01aModel(parameters)) 
        {
			this.LibraryProvider = new Foundations.Core.AppSupportLib.Util.LibraryProvider(libraries);
        }

        public Fmbth01aTask (Hashtable parameters)
            : this(new Alio.Forms.Fmbth01a.Model.Fmbth01aModel(parameters))
        {
		}

		public Fmbth01aTask (Alio.Forms.Fmbth01a.Model.Fmbth01aModel model) : base(model)
        {
			this.Blocks = new Fmbth01aTaskBlocks(this);
            this.Canvases = new Fmbth01aTaskCanvases(this);
            this.Windows = new Fmbth01aTaskWindows(this);
			this.Libraries = new Fmbth01aTaskLibraries(this);
            this.Alerts = new Fmbth01aTaskAlerts(this);
            this.Lovs = new Fmbth01aTaskLovs(this);
			InitAdditionalComponents();
		}

		partial void InitAdditionalComponents();

		public new Fmbth01aTaskBlocks Blocks { get; private set; }
        public Fmbth01aTaskCanvases Canvases { get; private set; }
        public Fmbth01aTaskWindows Windows { get; private set; }
        public new Fmbth01aTaskLibraries Libraries { get; private set; }
        public new Fmbth01aTaskAlerts Alerts { get; private set; }
        public new Fmbth01aTaskLovs Lovs { get; private set; }
        
		public partial class Fmbth01aTaskLibraries
        {
            public Fmbth01aTaskLibraries(Fmbth01aTask task)
            {
				Sutl01a = task.FindLibrary<Libraries.Sutl01a.Sutl01aServices>("SUTL01A");
				Afilerpt = task.FindLibrary<Libraries.Afilerpt.AfilerptServices>("AFILERPT");
				Scla01a = task.FindLibrary<Libraries.Scla01a.Scla01aServices>("SCLA01A");
            }

			public Libraries.Sutl01a.Sutl01aServices Sutl01a { get; private set; }
			public Libraries.Afilerpt.AfilerptServices Afilerpt { get; private set; }
			public Libraries.Scla01a.Scla01aServices Scla01a { get; private set; }
        }
	}
}

