
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Foundations.Core.Types;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Slact01a.Model {

	public partial class Slact01aModel: AbstractComposableModel<Slact01aTask, ParameterCollection> {

		public AccountsManager Accounts  {
			get {
                return Get<AccountsManager>();
			}
		}
		public NickAccountsManager NickAccounts  {
			get {
                return Get<NickAccountsManager>();
			}
		}
		public Controls Controls  {
			get {
                return Get<Controls>();
			}
		}

		public Slact01aModel() : this((Hashtable)null) {
		}
		
		protected Slact01aModel(string name) : base(name) {
        }

		public Slact01aModel(Hashtable parameters) : base(parameters)	{
		}
	}

}

