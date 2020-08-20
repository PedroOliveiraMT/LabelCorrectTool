
	
using System.Collections;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjpst01a
{
    public partial class Fjpst01aTask : AbstractComposableTask<Alio.Forms.Fjpst01a.Services.Fjpst01aServices, Alio.Forms.Fjpst01a.Model.Fjpst01aModel>
	{
			
		public Fjpst01aTask () : this(new Alio.Forms.Fjpst01a.Model.Fjpst01aModel(null))
		{
		}
		
		public Fjpst01aTask (Hashtable libraries, Hashtable parameters)
           : this(new Alio.Forms.Fjpst01a.Model.Fjpst01aModel(parameters)) 
        {
			this.LibraryProvider = new Foundations.Core.AppSupportLib.Util.LibraryProvider(libraries);
        }

        public Fjpst01aTask (Hashtable parameters)
            : this(new Alio.Forms.Fjpst01a.Model.Fjpst01aModel(parameters))
        {
		}

		public Fjpst01aTask (Alio.Forms.Fjpst01a.Model.Fjpst01aModel model) : base(model)
        {
			this.Blocks = new Fjpst01aTaskBlocks(this);
            this.Canvases = new Fjpst01aTaskCanvases(this);
            this.Windows = new Fjpst01aTaskWindows(this);
			this.Libraries = new Fjpst01aTaskLibraries(this);
            this.Alerts = new Fjpst01aTaskAlerts(this);
			InitAdditionalComponents();
		}

		partial void InitAdditionalComponents();

		public new Fjpst01aTaskBlocks Blocks { get; private set; }
        public Fjpst01aTaskCanvases Canvases { get; private set; }
        public Fjpst01aTaskWindows Windows { get; private set; }
        public new Fjpst01aTaskLibraries Libraries { get; private set; }
        public new Fjpst01aTaskAlerts Alerts { get; private set; }
        
		public partial class Fjpst01aTaskLibraries
        {
            public Fjpst01aTaskLibraries(Fjpst01aTask task)
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

