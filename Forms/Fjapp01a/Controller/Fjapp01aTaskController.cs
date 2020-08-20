using Alio.Common;
using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjapp01a.Model;
using Alio.Libraries.Sutl01a;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using System;

namespace Alio.Forms.Fjapp01a.Controllers
{

    public class Fjapp01aTaskController : AlioBaseTaskController<Fjapp01aTask, Fjapp01aModel>
    {

        public Fjapp01aTaskController(Fjapp01aTask task) : base(task)
        {
        }

        #region event handlers generated from Forms triggers


        #region Original PL/SQL code code for TRIGGER FJAPP01A.ON-CLEAR-DETAILS
        /*
         -- 
-- Begin default relation program section 
-- 
BEGIN 
Clear_All_Master_Details; 
--  :totals.dr_total := null; 
--  :totals.cr_total := null; 
END; 
-- 
-- End default relation program section 
-- 
--
-- Begin default relation program section
--
BEGIN
Clear_All_Master_Details;
END;
--
-- End default relation program section
--

        */
        #endregion
        /* IGNORED
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:352
        /// F2N_MASTER_DETAIL_LOGIC : This Trigger was commented out because it contains Master-Detail logic. See documentation for details.
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJAPP01A.ON-CLEAR-DETAILS</TriggerInfo>

        [ClearDetails]
        public virtual void Fjapp01a_ClearDetails(System.EventArgs args)
        {

            //  
            //  Begin default relation program section 
            //  
            this.Task.Services.ClearAllMasterDetails();
            this.Task.Services.ClearAllMasterDetails();
        }

        */

        #region Original PL/SQL code code for TRIGGER FJAPP01A.WHEN-NEW-FORM-INSTANCE
        /*
         declare
v_where_clause_approval varchar2(4000);
v_where_clause          varchar2(4000);

begin 


default_value ('*','global.je_reference_no');

sutl01a.when_new_form_instance; 



--==============================================================
 -- load_parameters; 
 -- manage_menu; 
 -- handle_item_properties; 
<multilinecomment>****************************************************************
copied from another form but is any of this needed?
  default_value('*','global.qtsi_share_batches_flag'); 
  default_value('*','global.batch_no'); 
  default_value('*','global.batch_year'); 
  default_value('*','global.trans_id');
  default_value ('*', 'global.debug_status');  

-- If fjapp01a.fmx is launched from another form: 
--     1. Auto query based on batch_year,batch_no and trans_id (ignore user) 
--     2. No approvals/disapprovals should be done 
--     3. Display approval_chain tab on top 

if :global.batch_year <> '*'     and 
  :global.batch_no <> '*' and 
  :global.trans_id <> '*'then 

  set_block_property('je_header', 
                     default_where, 
                     'batch_year = :global.batch_year and '|| 
                     'batch_no   = :global.batch_no'); 
  execute_query; 

  set_block_property('je_header', default_where,  ' '); 

  go_item('je_header.batch_year');   

  set_item_property('je_header.approve',              NAVIGABLE,  PROPERTY_FALSE); 
  set_item_property('je_header.approve',              ENABLED,    PROPERTY_FALSE); 
  set_item_property('je_header.disapprove',           NAVIGABLE,  PROPERTY_FALSE); 
  set_item_property('je_header.disapprove',           ENABLED,    PROPERTY_FALSE); 
  set_item_property('je_header.disapproval_message',  NAVIGABLE,  PROPERTY_FALSE); 
  set_item_property('je_header.disapproval_message',  ENABLED,    PROPERTY_FALSE); 
  set_item_property('je_header.disapproval_edit',     NAVIGABLE,  PROPERTY_FALSE); 
  set_item_property('je_header.disapproval_edit',     ENABLED,    PROPERTY_FALSE); 

  set_canvas_property('CANVAS41', topmost_tab_page, 'APPROVAL_CHAIN_TAB'); 


else 
  -- Only query headers that are waiting for current user to approve 
*********************************************************************************************</multilinecomment>

:parameter.journal_approval        := nvl(trim(adm.get_profile_data('JOURNAL_APPROVAL')),'N');  --mfl alio-14824


go_block('je_header'); 
   v_where_clause :=  'approval_status in ( ''EN'',''DR'' )'||
                     ' and ((nvl(:parameter.journal_approval, ''N'') = ''N'' )'  ||  --mfl alio-14824 19.1
                     '  or (nvl(:parameter.journal_approval, ''N'') = ''Y'' '     || --mfl alio-14824 19.1
                     ' and nvl(release_flag, ''N'') = ''Y'')) '                   || --mfl alio-14824 19.1
                     ' and (reference_no) in'|| 
                     '     ( select ja.reference_no'|| 
                     '       from shr.user_approver_master uam,'|| 
                     '            fas.journal_approval ja'      || 
                     '       where uam.user_id = user'          ||  
                     '        and uam.active_flag = ''Y'' '     || 
                     '        and ja.status_flag  = ''W''  '    || 
                     '        and ja.approval_code = upper(uam.approval_code) )'; 

-- dph alio-12046 added if and approval default where option
if :global.je_reference_no <> '*' then

    v_where_clause_approval :=  'reference_no = :global.je_reference_no'||
                     ' and ((nvl(:parameter.journal_approval, ''N'') = ''N'' )'  ||  --mfl alio-14824 19.1
                     '  or (nvl(:parameter.journal_approval, ''N'') = ''Y'' '     || --mfl alio-14824 19.1
                     ' and nvl(release_flag, ''N'') = ''Y'')) ' ;                    --mfl alio-14824 19.1
   set_block_property('je_header', 
                      default_where, 
                      v_where_clause_approval);
else

   set_block_property('je_header', 
                      default_where, 
                      v_where_clause);

end if;

  execute_query; 


set_block_property('je_header', 
                      default_where, 
                      v_where_clause);

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:353
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJAPP01A.WHEN-NEW-FORM-INSTANCE</TriggerInfo>

        [TaskStarted]
        public virtual void Fjapp01a_TaskStarted(System.EventArgs args)
        {

            {
                string vWhereClauseApproval = string.Empty;
                string vWhereClause = string.Empty;
                Globals.SetDefault("global.je_reference_no", "*");
                Task.Libraries.Sutl01a.Sutl01a.WhenNewFormInstance(() =>
                {
                        // ==============================================================
                        //  load_parameters; 
                        //  manage_menu; 
                        //  handle_item_properties; 
                        // ****************************************************************
                        // copied from another form but is any of this needed?
                        // default_value('*','global.qtsi_share_batches_flag');
                        // default_value('*','global.batch_no');
                        // default_value('*','global.batch_year');
                        // default_value('*','global.trans_id');
                        // default_value ('*', 'global.debug_status');
                        // -- If fjapp01a.fmx is launched from another form:
                        // --     1. Auto query based on batch_year,batch_no and trans_id (ignore user)
                        // --     2. No approvals/disapprovals should be done
                        // --     3. Display approval_chain tab on top
                        // if :global.batch_year <> '*'     and
                        // :global.batch_no <> '*' and
                        // :global.trans_id <> '*'then
                        // set_block_property('je_header',
                        // default_where,
                        // 'batch_year = :global.batch_year and '||
                        // 'batch_no   = :global.batch_no');
                        // execute_query;
                        // set_block_property('je_header', default_where,  ' ');
                        // go_item('je_header.batch_year');
                        // set_item_property('je_header.approve',              NAVIGABLE,  PROPERTY_FALSE);
                        // set_item_property('je_header.approve',              ENABLED,    PROPERTY_FALSE);
                        // set_item_property('je_header.disapprove',           NAVIGABLE,  PROPERTY_FALSE);
                        // set_item_property('je_header.disapprove',           ENABLED,    PROPERTY_FALSE);
                        // set_item_property('je_header.disapproval_message',  NAVIGABLE,  PROPERTY_FALSE);
                        // set_item_property('je_header.disapproval_message',  ENABLED,    PROPERTY_FALSE);
                        // set_item_property('je_header.disapproval_edit',     NAVIGABLE,  PROPERTY_FALSE);
                        // set_item_property('je_header.disapproval_edit',     ENABLED,    PROPERTY_FALSE);
                        // set_canvas_property('CANVAS41', topmost_tab_page, 'APPROVAL_CHAIN_TAB');
                        // else
                        // -- Only query headers that are waiting for current user to approve
                        // *********************************************************************************************

                        Model.Params.JournalApproval = Lib.IsNull(Lib.Trim(StoredProcedures.FGetProfileData("JOURNAL_APPROVAL")), "N").ToString();

                        // mfl alio-14824
                        BlockServices.GoBlock("je_header");
                    vWhereClause = "approval_status in ( 'EN','DR' )" + " and ((nvl(':PARAMETER.JOURNAL_APPROVAL', 'N') = 'N' )" + "  or (nvl(':PARAMETER.JOURNAL_APPROVAL', 'N') = 'Y' " + " and nvl(release_flag, 'N') = 'Y')) " + " and (reference_no) in" + "     ( select ja.reference_no" + "       from shr.user_approver_master uam," + "            fas.journal_approval ja" + "       where uam.user_id = user" + "        and uam.active_flag = 'Y' " + "        and ja.status_flag  = 'W'  " + "        and ja.approval_code = upper(uam.approval_code) )";
                        //  dph alio-12046 added if and approval default where option
                        if (Globals.JeReferenceNo != "*")
                    {
                        vWhereClauseApproval = "reference_no = ':global.je_reference_no'" + " and ((nvl(':parameter.journal_approval', 'N') = 'N' )" + "  or (nvl(':parameter.journal_approval', 'N') = 'Y' " + " and nvl(release_flag, 'N') = 'Y')) ";
                            // mfl alio-14824 19.1
                            BlockServices.SetWhereClause("je_header", vWhereClauseApproval);
                    }
                    else
                    {
                        BlockServices.SetWhereClause("je_header", vWhereClause);
                    }
                    BlockServices.ExecuteQuery();
                    BlockServices.SetWhereClause("je_header", vWhereClause);
                });
            }
        }


        #region Original PL/SQL code code for TRIGGER FJAPP01A.POST-FORM
        /*
         begin 
forms_ddl('begin ' 
        || 'update fas.batches ' 
        || 'set access_flag = ' 
        || '''' 
        || 'Y' 
        || '''' 
        || ' where batch_no = ' 
        || '''' 
        || :je_header.batch_no 
        || '''' 
        || ' and ' 
        || 'batch_year = ' 
        || '''' 
        || :je_header.batch_year 
        || '''' 
        || ';' 
        || 'commit;' 
        || 'end;'); 
end;   
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:354
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FJAPP01A.POST-FORM</TriggerInfo>

        [TaskEnded]
        public virtual void Fjapp01a_TaskEnded(System.EventArgs args)
        {
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;

            Lib.FormsDDL("begin " + "update fas.batches " + "set access_flag = " + "'" + "Y" + "'" + " where batch_no = " + "'" + jeHeaderElement.BatchNo + "'" + " and " + "batch_year = " + "'" + jeHeaderElement.BatchYear + "'" + "; end;");
            DbManager.Commit();
        }


        #region Original PL/SQL code code for TRIGGER FJAPP01A.ON-COMMIT
        /*
         begin 
--   qtsi_util.on_commit; 
if :system.form_status <> 'QUERY' then 
  update fas.batches 
     set posted_flag = 'EN' 
   where batch_no = :je_header.batch_no and 
         batch_year = :je_header.batch_year; 
  commit_form; 
end if; 
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:355
        /// 
        ///</remarks>
        /// <TriggerInfo>FJAPP01A.ON-COMMIT</TriggerInfo>

        [BeforeDatabaseCommit]
        public virtual void Fjapp01a_OnCommit()
        {
            JeHeaderAdapter jeHeaderElement = this.Model.JeHeader.CurrentRowAdapter;


            int rowCount = 0;
            //    qtsi_util.on_commit; 
            if (TaskServices.FormStatus != "QUERY")
            {
                #region Execute data command
                String sql1 = "UPDATE fas.batches " +
"SET posted_flag = 'EN' " +
" WHERE batch_no = @JE_HEADER_BATCH_NO AND batch_year = @JE_HEADER_BATCH_YEAR";
                DataCommand command1 = new DataCommand(sql1);
                //Setting query parameters
                command1.AddParameter("@JE_HEADER_BATCH_NO", jeHeaderElement.BatchNo);
                command1.AddParameter("@JE_HEADER_BATCH_YEAR", jeHeaderElement.BatchYear);
                rowCount = command1.Execute();
                #endregion
                TaskServices.Commit();
            }
        }



        #endregion

    }

}
