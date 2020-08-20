
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Foundations.Core.Types;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjgen01a.Model {

	public partial class Fjgen01aModel: AbstractComposableModel<Fjgen01aTask, ParameterCollection> {

		public JournalsManager Journals  {
			get {
                return Get<JournalsManager>();
			}
		}

		public Fjgen01aModel() : this((Hashtable)null) {
		}
		
		protected Fjgen01aModel(string name) : base(name) {
        }

		public Fjgen01aModel(Hashtable parameters) : base(parameters)	{
		}
	}

}

