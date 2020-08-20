using System;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Composition.Metadata;
using Foundations.Core.Types;


namespace Alio.Forms.Slact01a.Model {
	public partial class Controls : Foundations.Flavors.Forms.Model.SimpleBusinessObject
	{
		public Controls(AbstractComposableModel model, BusinessObjectMetadata metadata) : base(model, metadata) {
		}

		public event EventHandler ProgramListChanged;
		public event EventHandler ScreenModeChanged;
		public event EventHandler QtsiFindFlagChanged;
		public event EventHandler QtsiAddFlagChanged;
		public event EventHandler QtsiDeleteFlagChanged;
		public event EventHandler QtsiUpdateFlagChanged;

		private NString _programList;
		public NString ProgramList {
			get { return _programList; }
            set { 
				_programList = value; 
				ProgramListChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _screenMode;
		public NString ScreenMode {
			get { return _screenMode; }
            set { 
				_screenMode = value; 
				ScreenModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _qtsiFindFlag;
		public NString QtsiFindFlag {
			get { return _qtsiFindFlag; }
            set { 
				_qtsiFindFlag = value; 
				QtsiFindFlagChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _qtsiAddFlag;
		public NString QtsiAddFlag {
			get { return _qtsiAddFlag; }
            set { 
				_qtsiAddFlag = value; 
				QtsiAddFlagChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _qtsiDeleteFlag;
		public NString QtsiDeleteFlag {
			get { return _qtsiDeleteFlag; }
            set { 
				_qtsiDeleteFlag = value; 
				QtsiDeleteFlagChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		private NString _qtsiUpdateFlag;
		public NString QtsiUpdateFlag {
			get { return _qtsiUpdateFlag; }
            set { 
				_qtsiUpdateFlag = value; 
				QtsiUpdateFlagChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	
	}
}
