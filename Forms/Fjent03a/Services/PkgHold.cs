using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Foundations.Core.AppDataLayer;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Model;
using Foundations.Core.AppSupportLib.Util;
using Foundations.Core.AppSupportLib.Util.IO;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Exceptions;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Task;
using Foundations.Core.AppSupportLib.Runtime.ControlsModel.Items;
using Foundations.Core.AppSupportLib.Runtime.ControlsModel.Containers;
using Foundations.Flavors.Forms;

using Alio.Forms.Fjent03a.Controllers;
using Foundations.Core.Types;
using Alio.Common;
using Alio.Forms.Common.DbServices;
using Alio.Libraries.Sutl01a;
using Alio.Libraries.Afilerpt;
using Alio.Forms.Fjent03a.Model;

namespace Alio.Forms.Fjent03a.Services {

	public class PkgHold{
		
		protected Fjent03aModel model_;
		
		public Fjent03aModel Model {
			get {
				return model_;
			}
		}
		
		public PkgHold(Fjent03aModel model) {
			this.model_ = model;
		}
		
		#region Original PL/SQL code for Package Prog Unit (SPEC) HOLD
		/*
		PACKAGE HOLD IS 
  journal_description varchar2(40);
  
   -- create array for data on :order_item 
   -- this array is for perormance, it reduces database queries 
   type reference_tab_rec is
     record (reference_no  fas.je_header.reference_no%type);
              
   type reference_type is
     table of reference_tab_rec index by binary_integer;
     
     reference_table  reference_type;
     
-- mfl alio-14830 19.2     
-- create array for required fields
type required_tab_rec is
     record (field_name          varchar2(80));
              
   type required_type is
     table of required_tab_rec index by binary_integer;
     
     required_table  required_type;       
     
END;
		*/
		#endregion
				public string journalDescription = string.Empty;
		public ReferenceType referenceTable = new ReferenceType();
		public RequiredType requiredTable = new RequiredType();


		#region inner classes

		public class ReferenceTabRec
		{
			public NNumber ReferenceNo;
		}

		public class ReferenceType : Alio.Common.Types.Table<ReferenceTabRec>
		{
			public ReferenceType() : base(new ReferenceTabRec()) { }
		}

		public class RequiredTabRec
		{
			public string FieldName;
		}

		public class RequiredType : Alio.Common.Types.Table<RequiredTabRec>
		{
			public RequiredType() : base(new RequiredTabRec()) { }
		}

		#endregion



	}
}
