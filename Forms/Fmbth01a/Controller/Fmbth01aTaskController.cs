using Alio.Common;
using Alio.Common.Runtime.Controller;
using Alio.Forms.Fmbth01a.Model;
using Alio.Libraries.Sutl01a;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fmbth01a.Controllers
{

    public class Fmbth01aTaskController : AlioBaseTaskController<Fmbth01aTask, Fmbth01aModel>
    {

        public Fmbth01aTaskController(Fmbth01aTask task) : base(task)
        {
        }



        #region event handlers generated from Forms triggers


        #region Original PL/SQL code code for TRIGGER FMBTH01A.WHEN-NEW-FORM-INSTANCE
        /*
         DECLARE

record_found    boolean;
calling_form_   varchar2(40);
hold_batch_type varchar2(2) := '*';
in_batch_types  varchar2(10);
form_name       varchar2(40);
file_name       varchar2(40);
share_batch     varchar2(1);

cursor share_batch_cur is 
select security.share_batches_flag
from adm.security,
     adm.program
where upper(program.file_name) like upper(calling_form_)||'%'
 and security.menu_selection = program.menu_selection
 and security.security_role_id = :global.qtsi_role_id;

 <multilinecomment>  mnn - 09-06-2000 some users have two roles
                              (select role_id
                                 from adm.user_roles
                                where UPPER(user_id) = upper(:global.qtsi_user_id));
 </multilinecomment>

cursor account_period_cur is
select accounting_period
from shr.account_period_master
where account_period_master.account_year = :global.default_year 
 and to_char(account_period_master.begin_date,'YYYYMMDD')
             <= to_char(sysdate,'YYYYMMDD') 
 and to_char(account_period_master.end_date,'YYYYMMDD')
             >= to_char(sysdate,'YYYYMMDD');

cursor batches_cur (cur_in_hold_batch_type in varchar2,
                cur_in_batch_year in varchar2) is
select batch_year,
     batch_no
from fas.batches
where ((cur_in_hold_batch_type = 'JE' 
     and (batch_type = 'JE' or batch_type = 'PR')) 
     or batch_type = cur_in_hold_batch_type) 
 and batches.batch_year = cur_in_batch_year 
 and access_flag <> 'N' 
 and (posted_flag in ('EN','RP','ER'));

batches_rec batches_cur%rowtype;

CURSOR profiles_cur (key_in VARCHAR2)
IS
    SELECT profile_key, profile_data
      FROM adm.profiles
     WHERE profile_key = key_in;


BEGIN
-- 01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
calling_form_ := get_application_property(calling_form);
hold_batch_type := name_in('global.'
                          || calling_form_
                          || '_batch_type');
debug_msg('calling_form_ ' || calling_form_ || ' hold_batch_type ' || hold_batch_type);

-- 01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
for cur_rec in profiles_cur('FMBTH01A SHOW WARRANTS')
loop
  :parameter.show_warrants := cur_rec.profile_data;
end loop;
debug_msg(':parameter.show_warrants ' || :parameter.show_warrants || ' hold_batch_type ' || hold_batch_type);

-- 01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
---- Future batch prefix functionality ----
:parameter.allow_prefix_batches := 'NO';

for cur_rec in profiles_cur('ALLOW_PREFIX_BATCHES')
loop
  :parameter.allow_prefix_batches := cur_rec.profile_data;
end loop;

IF :parameter.allow_prefix_batches = 'YES' then
  SET_ITEM_PROPERTY('batches.batch_lov_btn', 
                    displayed,
                    property_true);

  SET_ITEM_PROPERTY('batches.batch_no', WIDTH, 44);

ELSE
  SET_ITEM_PROPERTY('batches.batch_lov_btn', 
                    displayed,
                    property_false);

  SET_ITEM_PROPERTY('batches.batch_no', WIDTH, 58);

END IF;

IF (:parameter.show_warrants IN ('DISPLAY','D') and hold_batch_type in ('JE','PR','AP','VO','DA')) or
  (:parameter.show_warrants IN ('POST','P') and hold_batch_type in ('AP','VO','DA')) then
  debug_msg('Then');

  SET_ITEM_PROPERTY('batches.assign_warrant', 
                    displayed,
                    property_true);

  SET_ITEM_PROPERTY('batches.warrant_no', 
                    displayed,
                    property_true);

  SET_ITEM_PROPERTY('batches.warrant_no', x_pos, 80);

  SET_ITEM_PROPERTY('batches.status', x_pos, 554);


  SET_ITEM_PROPERTY('batches.warrant_from_date', 
                    displayed,
                    property_true);         

  SET_ITEM_PROPERTY('batches.warrant_to_date', 
                    displayed,
                    property_true); 

  SET_ITEM_PROPERTY('batches.warrant_lov_btn', 
                    displayed,
                    property_true);                               

  SET_ITEM_PROPERTY('batches.user_id', WIDTH, 93);

  SET_ITEM_PROPERTY('batches.status', WIDTH, 45); 

ELSE debug_msg('Else');
  SET_ITEM_PROPERTY('batches.assign_warrant', 
                    displayed,
                    property_false);

  SET_ITEM_PROPERTY('batches.warrant_no', 
                    displayed,
                    property_false);

  SET_ITEM_PROPERTY('batches.warrant_from_date', 
                    displayed,
                    property_false);         

  SET_ITEM_PROPERTY('batches.warrant_to_date', 
                    displayed,
                    property_false);

  SET_ITEM_PROPERTY('batches.warrant_lov_btn', 
                    displayed,
                    property_false);  

  SET_ITEM_PROPERTY('batches.status', x_pos, 82);

  SET_ITEM_PROPERTY('batches.warrant_no', x_pos, 555);

  SET_ITEM_PROPERTY('batches.reset_active_batch', x_pos, 523);    -- 01/31/20 psr alio-16722 Position button
  SET_ITEM_PROPERTY('batches.batch_master_listing', x_pos, 603);  -- 01/31/20 psr alio-16722 Position button


  SET_ITEM_PROPERTY('batches.user_id', WIDTH, 200);
  SET_ITEM_PROPERTY('batches.status',  WIDTH, 58);
  --SET_BLOCK_PROPERTY('BATCHES', BLOCKSCROLLBAR_POSITION, 661, 46);  -- 01/04/17 psr alio-12975 Added to move scroll bar left
  SET_BLOCK_PROPERTY('BATCHES', BLOCKSCROLLBAR_POSITION, 661, 58);  -- 01/31/20 psr alio-16722 Move scroll bar down to make room for buttons
END IF;
Synchronize;  -- 01/04/17 psr alio-12975 Added to refresh form layout
-- End of code Moved up

sutl01a.when_new_form_instance;
<multilinecomment> 01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
calling_form_ := get_application_property(calling_form);
</multilinecomment>

open share_batch_cur;
fetch share_batch_cur into share_batch;
close share_batch_cur;

if share_batch is null then
  share_batch := 'N';
end if;

erase('global.'
    || calling_form_
    || '_batch_no');
erase('global.'
    || calling_form_
    || '_batch_year');

open account_period_cur;
fetch account_period_cur into :hold.account_period;
close account_period_cur;

<multilinecomment> 01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
hold_batch_type := name_in('global.'
                          || calling_form_
                          || '_batch_type');
</multilinecomment>

if hold_batch_type = 'JE' then
  in_batch_types := ''''
                 || 'JE'
                 || ''''
                 || ','
                 || ''''
                 || 'PR'
                 || '''';
else
  in_batch_types := ''''
                 || hold_batch_type
                 || '''';
end if;

if share_batch = 'N' then
  set_block_property('batches'
                    ,default_where
                    ,'batches.batch_type in ('
                    || in_batch_types
                    || ')'
                    || ' and upper(batches.user_id) = '
                    || 'upper(:global.qtsi_user_id)'
                    || ' and batches.batch_year = '
                    || ':global.default_year'
                    || ' and access_flag <> '
                    || ''''
                    || 'N'
                    || ''''
                    || ' and ( posted_flag in ('
                    || ''''
                    || 'EN'
                    || ''''
                    || ','
                    || ''''
                    || 'RP'
                    || ''''
                    || ','
                    || ''''
                    || 'ER'
                    || ''''
                    || '))');
else                          
    set_block_property('batches'
                    ,default_where
                    ,'batches.batch_type in ('
                    || in_batch_types
                    || ')'
                    || ' and batches.batch_year = '
                    || ':global.default_year'
                    || ' and access_flag <> '
                    || ''''
                    || 'N'
                    || ''''
                    || ' and ( posted_flag in ('
                    || ''''
                    || 'EN'
                    || ''''
                    || ','
                    || ''''
                    || 'RP'
                    || ''''
                    || ','
                    || ''''
                    || 'ER'
                    || ''''
                    || '))');
end if;

open batches_cur(hold_batch_type,:global.default_year);
fetch batches_cur into batches_rec;
record_found := batches_cur%found;
close batches_cur;

if record_found and
  calling_form_ <> 'FCVOI01A' then     --mfl alio-11626 19.4 Don't display existing batches when voiding a receipt
  execute_query;
end if;  -- jag 24129


  if share_batch = 'N' then
     set_block_property('batches'
                       ,default_where
                       ,'batches.batch_type in ('
                       || in_batch_types
                       || ')'
                       || ' and upper(batches.user_id) = '
                       || 'upper(:global.qtsi_user_id)'
                       || ' and access_flag <> '
                       || ''''
                       || 'N'
                       || ''''
                       || ' and ( posted_flag in ('
                       || ''''
                       || 'EN'
                       || ''''
                       || ','
                       || ''''
                       || 'RP'
                       || ''''
                       || ','
                       || ''''
                       || 'ER'
                       || ''''
                       || '))');
  else                          
       set_block_property('batches'
                       ,default_where
                       ,'batches.batch_type in ('
                       || in_batch_types
                       || ')'
                       || ' and access_flag <> '
                       || ''''
                       || 'N'
                       || ''''
                       || ' and ( posted_flag in ('
                       || ''''
                       || 'EN'
                       || ''''
                       || ','
                       || ''''
                       || 'RP'
                       || ''''
                       || ','
                       || ''''
                       || 'ER'
                       || ''''
                       || '))');
  end if;      
-- end if;-- JAG 24129

<multilinecomment> 01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
---- Future batch prefix functionality ----
:parameter.allow_prefix_batches := 'NO';

for cur_rec in profiles_cur('ALLOW_PREFIX_BATCHES')
loop
  :parameter.allow_prefix_batches := cur_rec.profile_data;
end loop;

IF :parameter.allow_prefix_batches = 'YES' then
    SET_ITEM_PROPERTY ('batches.batch_lov_btn', 
                        displayed,
                        property_true);

    SET_ITEM_PROPERTY('batches.batch_no', WIDTH, 44);

ELSE
       SET_ITEM_PROPERTY ('batches.batch_lov_btn', 
                         displayed,
                         property_false);

     SET_ITEM_PROPERTY('batches.batch_no', WIDTH, 58);

END IF;

for cur_rec in profiles_cur('FMBTH01A SHOW WARRANTS')
loop
  :parameter.show_warrants := cur_rec.profile_data;
end loop;

--IF :parameter.show_warrants in ('DISPLAY','POST') then
    IF (:parameter.show_warrants IN ('DISPLAY','D') and hold_batch_type in ('JE','PR','AP','VO','DA')) or (:parameter.show_warrants IN ('POST','P') and hold_batch_type in ('AP','VO','DA')) then

    SET_ITEM_PROPERTY ('batches.assign_warrant', 
                        displayed,
                        property_true);

    SET_ITEM_PROPERTY ('batches.warrant_no', 
                        displayed,
                        property_true);

    --SET_ITEM_PROPERTY ('batches.warrant_no', position, 82, 45);
    SET_ITEM_PROPERTY ('batches.warrant_no', x_pos, 80);

    --SET_ITEM_PROPERTY ('batches.status', position, 555, 45);
    SET_ITEM_PROPERTY ('batches.status', x_pos, 554);


    SET_ITEM_PROPERTY ('batches.warrant_from_date', 
                        displayed,
                        property_true);         

    SET_ITEM_PROPERTY ('batches.warrant_to_date', 
                        displayed,
                        property_true); 

    SET_ITEM_PROPERTY ('batches.warrant_lov_btn', 
                        displayed,
                        property_true);                               

    SET_ITEM_PROPERTY('batches.user_id', WIDTH, 93);

    SET_ITEM_PROPERTY('batches.status', WIDTH, 45); 

ELSE
    SET_ITEM_PROPERTY ('batches.assign_warrant', 
                        displayed,
                        property_false);

    SET_ITEM_PROPERTY ('batches.warrant_no', 
                        displayed,
                        property_false);

    SET_ITEM_PROPERTY ('batches.warrant_from_date', 
                        displayed,
                        property_false);         

    SET_ITEM_PROPERTY ('batches.warrant_to_date', 
                        displayed,
                        property_false);

    SET_ITEM_PROPERTY ('batches.warrant_lov_btn', 
                        displayed,
                        property_false);  

    --SET_ITEM_PROPERTY ('batches.status', position, 83, 45);
    SET_ITEM_PROPERTY ('batches.status', x_pos, 82);

    --SET_ITEM_PROPERTY ('batches.warrant_no', position, 557, 45);
    SET_ITEM_PROPERTY ('batches.warrant_no', x_pos, 555);


    SET_ITEM_PROPERTY('batches.user_id', WIDTH, 200);
    SET_ITEM_PROPERTY('batches.status',  WIDTH, 58); 
END IF;
</multilinecomment>

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:356
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>FMBTH01A.WHEN-NEW-FORM-INSTANCE</TriggerInfo>

        [TaskStarted]
        public virtual void Fmbth01a_TaskStarted(System.EventArgs args)
        {

            int rowCount = 0;
            {
                NBool recordFound = NBool.Null;
                string callingForm_ = string.Empty;
                string holdBatchType = "*";
                string inBatchTypes = string.Empty;
                string formName = string.Empty;
                string fileName = string.Empty;
                string shareBatch = string.Empty;
                String sqlshareBatchCur = "SELECT security.share_batches_flag " +
" FROM adm.security, adm.program " +
" WHERE upper(program.file_name) LIKE upper(@P_CALLING_FORM_) || '%' AND security.menu_selection = program.menu_selection AND security.security_role_id = @GLOBAL_QTSI_ROLE_ID ";
                DataCursor shareBatchCur = new DataCursor(sqlshareBatchCur);
                String sqlaccountPeriodCur = "SELECT accounting_period " +
" FROM shr.account_period_master " +
" WHERE account_period_master.account_year = @GLOBAL_DEFAULT_YEAR AND to_char(account_period_master.begin_date, 'YYYYMMDD') <= to_char(sysdate, 'YYYYMMDD') AND to_char(account_period_master.end_date, 'YYYYMMDD') >= to_char(sysdate, 'YYYYMMDD') ";
                DataCursor accountPeriodCur = new DataCursor(sqlaccountPeriodCur);
                String sqlbatchesCur = "SELECT batch_year, batch_no " +
" FROM fas.batches " +
" WHERE ((@P_CUR_IN_HOLD_BATCH_TYPE = 'JE' AND (batch_type = 'JE' OR batch_type = 'PR')) OR batch_type = @P_CUR_IN_HOLD_BATCH_TYPE) AND batches.batch_year = @P_CUR_IN_BATCH_YEAR AND access_flag <> 'N' AND ((posted_flag) IN ('EN', 'RP', 'ER')) ";
                DataCursor batchesCur = new DataCursor(sqlbatchesCur);
                String batchesCurCurInHoldBatchType = string.Empty;
                String batchesCurCurInBatchYear = string.Empty;
                TableRow batchesRec = null;
                String sqlprofilesCur = "SELECT profile_key, profile_data " +
" FROM adm.profiles " +
" WHERE profile_key = @P_KEY_IN ";
                DataCursor profilesCur = new DataCursor(sqlprofilesCur);
                String profilesCurKeyIn = string.Empty;
                #region
                try
                {
                    //  01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
                    callingForm_ = TaskServices.CallingForm;
                    holdBatchType = TaskServices.NameIn("global." + callingForm_ + "_batch_type");
                    this.Task.Services.DebugMsg("calling_form_ " + callingForm_ + " hold_batch_type " + holdBatchType);
                    //  01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
                    profilesCurKeyIn = "FMBTH01A SHOW WARRANTS";
                    //Setting query parameters
                    profilesCur.AddParameter("@P_KEY_IN", profilesCurKeyIn);
                    #region loop for cursor profilesCur
                    try
                    {
                        profilesCur.Open();
                        while (true)
                        {
                            TableRow curRec = profilesCur.FetchRow();
                            if (profilesCur.NotFound) break;
                            this.Model.Params.ShowWarrants = curRec.GetStr("profile_data").ToString();
                        }
                    }
                    finally
                    {
                        profilesCur.Close();
                    }
                    #endregion
                    this.Task.Services.DebugMsg(":parameter.show_warrants " + this.Model.Params.ShowWarrants + " hold_batch_type " + holdBatchType);
                    //  01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
                    // -- Future batch prefix functionality ----
                    this.Model.Params.AllowPrefixBatches = "NO";
                    profilesCurKeyIn = "ALLOW_PREFIX_BATCHES";
                    //Setting query parameters
                    profilesCur.AddParameter("@P_KEY_IN", profilesCurKeyIn);
                    #region loop for cursor profilesCur
                    try
                    {
                        profilesCur.Open();
                        while (true)
                        {
                            TableRow curRec = profilesCur.FetchRow();
                            if (profilesCur.NotFound) break;
                            this.Model.Params.AllowPrefixBatches = curRec.GetStr("profile_data");
                        }
                    }
                    finally
                    {
                        profilesCur.Close();
                    }
                    #endregion
                    if (this.Model.Params.AllowPrefixBatches == "YES")
                    {
                        ItemServices.SetVisible("batches.batch_lov_btn", true);
                        ItemServices.SetWidth("batches.batch_no", 44);
                    }
                    else
                    {
                        ItemServices.SetVisible("batches.batch_lov_btn", false);
                        ItemServices.SetWidth("batches.batch_no", 58);
                    }
                    if (((this.Model.Params.ShowWarrants == "DISPLAY" || this.Model.Params.ShowWarrants == "D") && (holdBatchType == "JE" || holdBatchType == "PR" || holdBatchType == "AP" || holdBatchType == "VO" || holdBatchType == "DA")) || ((this.Model.Params.ShowWarrants == "POST" || this.Model.Params.ShowWarrants == "P") && (holdBatchType == "AP" || holdBatchType == "VO" || holdBatchType == "DA")))
                    {
                        this.Task.Services.DebugMsg("Then");
                        ItemServices.SetVisible("batches.assign_warrant", true);
                        ItemServices.SetVisible("batches.warrant_no", true);
                        ItemServices.SetXPos("batches.warrant_no", 80);
                        ItemServices.SetXPos("batches.status", 554);
                        ItemServices.SetVisible("batches.warrant_from_date", true);
                        ItemServices.SetVisible("batches.warrant_to_date", true);
                        ItemServices.SetVisible("batches.warrant_lov_btn", true);
                        ItemServices.SetWidth("batches.user_id", 93);
                        ItemServices.SetWidth("batches.status", 45);
                    }
                    else
                    {
                        this.Task.Services.DebugMsg("Else");
                        ItemServices.SetVisible("batches.assign_warrant", false);
                        ItemServices.SetVisible("batches.warrant_no", false);
                        ItemServices.SetVisible("batches.warrant_from_date", false);
                        ItemServices.SetVisible("batches.warrant_to_date", false);
                        ItemServices.SetVisible("batches.warrant_lov_btn", false);
                        ItemServices.SetXPos("batches.status", 82);
                        ItemServices.SetXPos("batches.warrant_no", 555);
                        ItemServices.SetXPos("batches.reset_active_batch", 523);
                        //  01/31/20 psr alio-16722 Position button
                        ItemServices.SetXPos("batches.batch_master_listing", 603);
                        //  01/31/20 psr alio-16722 Position button
                        ItemServices.SetWidth("batches.user_id", 200);
                        ItemServices.SetWidth("batches.status", 58);
                    }
                    TaskServices.Synchronize();
                    //  01/04/17 psr alio-12975 Added to refresh form layout
                    //  End of code Moved up
                    Task.Libraries.Sutl01a.Sutl01a.WhenNewFormInstance();
                    //  01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
                    // calling_form_ := get_application_property(calling_form);
                    //Setting query parameters
                    shareBatchCur.AddParameter("@P_CALLING_FORM_", callingForm_);
                    shareBatchCur.AddParameter("@GLOBAL_QTSI_ROLE_ID", Alio.Common.Globals.QtsiRoleId);
                    shareBatchCur.Open();
                    ResultSet shareBatchCurResults = shareBatchCur.FetchInto();
                    if (shareBatchCurResults != null)
                    {
                        shareBatch = shareBatchCurResults.GetString(0);
                    }
                    if (string.IsNullOrEmpty(shareBatch))
                    {
                        shareBatch = "N";
                    }
                    Globals.Remove("global." + callingForm_ + "_batch_no");
                    Globals.Remove("global." + callingForm_ + "_batch_year");
                    //Setting query parameters
                    accountPeriodCur.AddParameter("@GLOBAL_DEFAULT_YEAR", Alio.Common.Globals.DefaultYear);
                    accountPeriodCur.Open();
                    ResultSet accountPeriodCurResults = accountPeriodCur.FetchInto();
                    if (accountPeriodCurResults != null)
                    {
                        Model.Hold.AccountPeriod = accountPeriodCurResults.GetString(0);
                    }
                    //  01/04/17 psr alio-12975 Moved up before sutl01a.when_new_form_instance;
                    // hold_batch_type := name_in('global.'
                    // || calling_form_
                    // || '_batch_type');
                    if (holdBatchType == "JE")
                    {
                        inBatchTypes = "'" + "JE" + "'" + "," + "'" + "PR" + "'";
                    }
                    else
                    {
                        inBatchTypes = "'" + holdBatchType + "'";
                    }
                    if (shareBatch == "N")
                    {
                        BlockServices.SetWhereClause("batches", "batches.batch_type in (" + inBatchTypes + ")" + " and upper(batches.user_id) = " + "upper(':global.qtsi_user_id')" + " and batches.batch_year = " + "':global.default_year'" + " and access_flag <> " + "'" + "N" + "'" + " and ( posted_flag in (" + "'" + "EN" + "'" + "," + "'" + "RP" + "'" + "," + "'" + "ER" + "'" + "))");
                    }
                    else
                    {
                        BlockServices.SetWhereClause("batches", "batches.batch_type in (" + inBatchTypes + ")" + " and batches.batch_year = " + "':global.default_year'" + " and access_flag <> " + "'" + "N" + "'" + " and ( posted_flag in (" + "'" + "EN" + "'" + "," + "'" + "RP" + "'" + "," + "'" + "ER" + "'" + "))");
                    }
                    Globals.SetValue("FilteredByYear", "Y");
                    batchesCurCurInHoldBatchType = holdBatchType;
                    batchesCurCurInBatchYear = Alio.Common.Globals.DefaultYear;
                    //Setting query parameters
                    batchesCur.AddParameter("@P_CUR_IN_HOLD_BATCH_TYPE", batchesCurCurInHoldBatchType);
                    batchesCur.AddParameter("@P_CUR_IN_BATCH_YEAR", batchesCurCurInBatchYear);
                    batchesCur.Open();
                    batchesRec = batchesCur.FetchRow();
                    recordFound = batchesCur.Found;
                    if (recordFound && callingForm_ != "FCVOI01A")
                    {
                        // mfl alio-11626 19.4 Don't display existing batches when voiding a receipt
                        BlockServices.ExecuteQuery();
                    }
                    //  jag 24129
                    var batchesWhereClause = string.Empty;
                    if (shareBatch == "N")
                    {
                        batchesWhereClause = "batches.batch_type in (" + inBatchTypes + ")" + " and upper(batches.user_id) = " + "upper(':global.qtsi_user_id')" + " and access_flag <> " + "'" + "N" + "'" + " and ( posted_flag in (" + "'" + "EN" + "'" + "," + "'" + "RP" + "'" + "," + "'" + "ER" + "'" + "))";
                    }
                    else
                    {
                        batchesWhereClause = "batches.batch_type in (" + inBatchTypes + ")" + " and access_flag <> " + "'" + "N" + "'" + " and ( posted_flag in (" + "'" + "EN" + "'" + "," + "'" + "RP" + "'" + "," + "'" + "ER" + "'" + "))";
                    }
                    Globals.SetValue("BatchesFilteredWithoutYear", batchesWhereClause);
                }
                finally
                {
                    shareBatchCur.Close();
                    accountPeriodCur.Close();
                    batchesCur.Close();
                }
                #endregion
            }
        }



        #endregion

    }

}
