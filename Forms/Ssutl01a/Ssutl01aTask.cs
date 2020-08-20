
	
using System.Collections;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Ssutl01a
{
    public partial class Ssutl01aTask : AbstractComposableTask<Alio.Forms.Ssutl01a.Services.Ssutl01aServices, Alio.Forms.Ssutl01a.Model.Ssutl01aModel>
	{
			
		public Ssutl01aTask () : this(new Alio.Forms.Ssutl01a.Model.Ssutl01aModel(null))
		{
		}
		
		public Ssutl01aTask (Hashtable libraries, Hashtable parameters)
           : this(new Alio.Forms.Ssutl01a.Model.Ssutl01aModel(parameters)) 
        {
			this.LibraryProvider = new Foundations.Core.AppSupportLib.Util.LibraryProvider(libraries);
        }

        public Ssutl01aTask (Hashtable parameters)
            : this(new Alio.Forms.Ssutl01a.Model.Ssutl01aModel(parameters))
        {
		}

		public Ssutl01aTask (Alio.Forms.Ssutl01a.Model.Ssutl01aModel model) : base(model)
        {
			this.Blocks = new Ssutl01aTaskBlocks(this);
            this.Canvases = new Ssutl01aTaskCanvases(this);
            this.Windows = new Ssutl01aTaskWindows(this);
			this.Libraries = new Ssutl01aTaskLibraries(this);
            this.Alerts = new Ssutl01aTaskAlerts(this);
			InitAdditionalComponents();
		}

		partial void InitAdditionalComponents();

		public new Ssutl01aTaskBlocks Blocks { get; private set; }
        public Ssutl01aTaskCanvases Canvases { get; private set; }
        public Ssutl01aTaskWindows Windows { get; private set; }
        public new Ssutl01aTaskLibraries Libraries { get; private set; }
        public new Ssutl01aTaskAlerts Alerts { get; private set; }
        
		public partial class Ssutl01aTaskLibraries
        {
            public Ssutl01aTaskLibraries(Ssutl01aTask task)
            {
				Sutl01a = task.FindLibrary<Libraries.Sutl01a.Sutl01aServices>("SUTL01A");
				Afilerpt = task.FindLibrary<Libraries.Afilerpt.AfilerptServices>("AFILERPT");
            }

			public Libraries.Sutl01a.Sutl01aServices Sutl01a { get; private set; }
			public Libraries.Afilerpt.AfilerptServices Afilerpt { get; private set; }
        }
	}
}

