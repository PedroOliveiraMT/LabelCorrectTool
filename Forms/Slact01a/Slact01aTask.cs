
	
using System.Collections;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Slact01a
{
    public partial class Slact01aTask : AbstractComposableTask<Alio.Forms.Slact01a.Services.Slact01aServices, Alio.Forms.Slact01a.Model.Slact01aModel>
	{
			
		public Slact01aTask () : this(new Alio.Forms.Slact01a.Model.Slact01aModel(null))
		{
		}
		
		public Slact01aTask (Hashtable libraries, Hashtable parameters)
           : this(new Alio.Forms.Slact01a.Model.Slact01aModel(parameters)) 
        {
			this.LibraryProvider = new Foundations.Core.AppSupportLib.Util.LibraryProvider(libraries);
        }

        public Slact01aTask (Hashtable parameters)
            : this(new Alio.Forms.Slact01a.Model.Slact01aModel(parameters))
        {
		}

		public Slact01aTask (Alio.Forms.Slact01a.Model.Slact01aModel model) : base(model)
        {
			this.Blocks = new Slact01aTaskBlocks(this);
            this.Canvases = new Slact01aTaskCanvases(this);
            this.Windows = new Slact01aTaskWindows(this);
			this.Libraries = new Slact01aTaskLibraries(this);
            this.Alerts = new Slact01aTaskAlerts(this);
            this.Lovs = new Slact01aTaskLovs(this);
			InitAdditionalComponents();
		}

		partial void InitAdditionalComponents();

		public new Slact01aTaskBlocks Blocks { get; private set; }
        public Slact01aTaskCanvases Canvases { get; private set; }
        public Slact01aTaskWindows Windows { get; private set; }
        public new Slact01aTaskLibraries Libraries { get; private set; }
        public new Slact01aTaskAlerts Alerts { get; private set; }
        public new Slact01aTaskLovs Lovs { get; private set; }
        
		public partial class Slact01aTaskLibraries
        {
            public Slact01aTaskLibraries(Slact01aTask task)
            {
				Sutl01a = task.FindLibrary<Libraries.Sutl01a.Sutl01aServices>("SUTL01A");
				Afilerpt = task.FindLibrary<Libraries.Afilerpt.AfilerptServices>("AFILERPT");
            }

			public Libraries.Sutl01a.Sutl01aServices Sutl01a { get; private set; }
			public Libraries.Afilerpt.AfilerptServices Afilerpt { get; private set; }
        }
	}
}

