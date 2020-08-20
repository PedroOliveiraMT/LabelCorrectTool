
	
using System.Collections;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjapp01a
{
    public partial class Fjapp01aTask : AbstractComposableTask<Alio.Forms.Fjapp01a.Services.Fjapp01aServices, Alio.Forms.Fjapp01a.Model.Fjapp01aModel>
	{
			
		public Fjapp01aTask () : this(new Alio.Forms.Fjapp01a.Model.Fjapp01aModel(null))
		{
		}
		
		public Fjapp01aTask (Hashtable libraries, Hashtable parameters)
           : this(new Alio.Forms.Fjapp01a.Model.Fjapp01aModel(parameters)) 
        {
			this.LibraryProvider = new Foundations.Core.AppSupportLib.Util.LibraryProvider(libraries);
        }

        public Fjapp01aTask (Hashtable parameters)
            : this(new Alio.Forms.Fjapp01a.Model.Fjapp01aModel(parameters))
        {
		}

		public Fjapp01aTask (Alio.Forms.Fjapp01a.Model.Fjapp01aModel model) : base(model)
        {
			this.Blocks = new Fjapp01aTaskBlocks(this);
            this.Canvases = new Fjapp01aTaskCanvases(this);
            this.Windows = new Fjapp01aTaskWindows(this);
			this.Libraries = new Fjapp01aTaskLibraries(this);
            this.Alerts = new Fjapp01aTaskAlerts(this);
            this.Lovs = new Fjapp01aTaskLovs(this);
			InitAdditionalComponents();
		}

		partial void InitAdditionalComponents();

		public new Fjapp01aTaskBlocks Blocks { get; private set; }
        public Fjapp01aTaskCanvases Canvases { get; private set; }
        public Fjapp01aTaskWindows Windows { get; private set; }
        public new Fjapp01aTaskLibraries Libraries { get; private set; }
        public new Fjapp01aTaskAlerts Alerts { get; private set; }
        public new Fjapp01aTaskLovs Lovs { get; private set; }
        
		public partial class Fjapp01aTaskLibraries
        {
            public Fjapp01aTaskLibraries(Fjapp01aTask task)
            {
				Afilerpt = task.FindLibrary<Libraries.Afilerpt.AfilerptServices>("AFILERPT");
				Sutl01a = task.FindLibrary<Libraries.Sutl01a.Sutl01aServices>("SUTL01A");
				Scla01a = task.FindLibrary<Libraries.Scla01a.Scla01aServices>("SCLA01A");
            }

			public Libraries.Afilerpt.AfilerptServices Afilerpt { get; private set; }
			public Libraries.Sutl01a.Sutl01aServices Sutl01a { get; private set; }
			public Libraries.Scla01a.Scla01aServices Scla01a { get; private set; }
        }
	}
}

