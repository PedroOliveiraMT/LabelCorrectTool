using Alio.Common;
using Alio.Common.DbServices;
using Alio.Common.Runtime.Controller;
using Alio.Forms.Fmbth01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Exceptions;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.AppSupportLib.UI;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fmbth01a.Controllers
{


    public class BatchesController : BaseBlockController<Fmbth01aTask, Fmbth01aModel>
    {

        public BatchesController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER BATCHES.PRE-INSERT
        /*
         BEGIN

:batches.batch_type := (name_in('global.'
                           || get_application_property
                             (calling_form)
                           || '_batch_type'));

pre_validate;

validate_warrant_dates;

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:60
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>BATCHES.PRE-INSERT</TriggerInfo>

        [RowInserting]
        [Before]
        public virtual void batches_RowInserting(RowAdapterEventArgs args)
        {
            BatchesAdapter batchesElement = args.Row as BatchesAdapter;


            batchesElement.BatchType = ((TaskServices.NameIn("global." + TaskServices.CallingForm + "_batch_type")));
            this.Task.Services.PreValidate(batchesElement);
            this.Task.Services.ValidateWarrantDates(batchesElement);
        }


        #region Original PL/SQL code code for TRIGGER BATCHES.PRE-UPDATE
        /*
         BEGIN

pre_validate;

validate_warrant_dates;

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:61
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>BATCHES.PRE-UPDATE</TriggerInfo>

        [RowUpdating]
        [Before]
        public virtual void batches_RowUpdating(RowAdapterEventArgs args)
        {
            BatchesAdapter batchesElement = args.Row as BatchesAdapter;


            this.Task.Services.PreValidate(batchesElement);
            this.Task.Services.ValidateWarrantDates(batchesElement);
        }


        #region Original PL/SQL code code for TRIGGER BATCHES.WHEN-NEW-RECORD-INSTANCE
        /*
         begin

if :batches.batch_no is not null and
  :system.mode = 'ENTER-QUERY' then
  :batches.batch_year := :global.default_year;
else 
  raise form_trigger_failure;	 --prevents creating a record without a batch no
end if;

end;

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:62
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCHES.WHEN-NEW-RECORD-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-RECORD-INSTANCE", Function = KeyFunction.RECORD_CHANGE)]
        [Before]
        public virtual void batches_recordChange()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;

            if (batchesElement != null && !string.IsNullOrEmpty(batchesElement.BatchNo) && TaskServices.Mode == "ENTER-QUERY")
            {
                batchesElement.BatchYear = Alio.Common.Globals.DefaultYear;
            }
            else
            {
                throw new ApplicationException();
            }
        }


        #region Original PL/SQL code code for TRIGGER BATCHES.WHEN-VALIDATE-RECORD
        /*
         null;
--MNN 9-7-00 put this code in pre_validate program unit

<multilinecomment>

declare

cursor year_master_cur is
select current_accounting_period
from shr.year_master
where account_year = :batches.batch_year;
year_master_rec year_master_cur%rowtype;

cursor account_period_master_cur is
select begin_date,
     end_date
from shr.account_period_master
where account_year = :batches.batch_year and
     accounting_period = :batches.account_period;
account_period_master_rec account_period_master_cur%rowtype;

alert_status number;

begin

if :batches.batch_date is null then
  message('Please Enter a Batch Date');
  message(' ',no_acknowledge);
  raise form_trigger_failure;
end if;

open year_master_cur;
fetch year_master_cur into year_master_rec;
close year_master_cur;

if :batches.account_period
  < year_master_rec.current_accounting_period then
  set_item_property('batches.account_period',
                    item_is_valid,
                    property_false);
  message('Please enter a valid accounting period for this year');
  message(' ',no_acknowledge);
  raise form_trigger_failure;
end if;

open account_period_master_cur;
fetch account_period_master_cur into account_period_master_rec;
if account_period_master_cur%notfound then
  message('Please enter a valid accounting period for this year');
  message(' ',no_acknowledge);
  raise form_trigger_failure;
elsif :batches.batch_date > account_period_master_rec.end_date or
  :batches.batch_date < account_period_master_rec.begin_date then
  alert_status := show_alert('account_period_warning');
  if alert_status = alert_button2 then
     raise form_trigger_failure;
  end if;
end if;

end;

</multilinecomment>
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:63
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger BATCHES.WHEN-VALIDATE-RECORD . See documentation for details.
        ///</remarks>
        /// <TriggerInfo>BATCHES.WHEN-VALIDATE-RECORD</TriggerInfo>

        [RecordValidationTrigger]
        public virtual void batches_WhenValidateRecord()
        {
        }


        #region Original PL/SQL code code for TRIGGER BATCHES.POST-QUERY
        /*

if :batches.posted_flag = 'ER' then
 set_item_instance_property
   ('batches.status',current_record,visual_attribute,'status_error');
 :batches.status := 'ERROR';
elsif
 :batches.posted_flag = 'RP' then
 set_item_instance_property
   ('batches.status',current_record,visual_attribute,'status_ready');
 :batches.status := 'READY';
else
 :batches.status := 'ENTRY';
end if;

        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:64
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>BATCHES.POST-QUERY</TriggerInfo>

        [AfterQuery]
        public virtual void batches_AfterQuery(RowAdapterEventArgs args)
        {
            BatchesAdapter batchesElement = args.Row as BatchesAdapter;


            if (batchesElement.PostedFlag == "ER")
            {
                ItemServices.SetStyleClass("batches.status", TaskServices.CursorRecord, "status_error");
                batchesElement.Status = "ERROR";
            }
            else if (batchesElement.PostedFlag == "RP")
            {
                ItemServices.SetStyleClass("batches.status", TaskServices.CursorRecord, "status_ready");
                batchesElement.Status = "READY";
            }
            else
            {
                batchesElement.Status = "ENTRY";
            }
        }


        #region Original PL/SQL code code for TRIGGER ASSIGN_BATCH.WHEN-BUTTON-PRESSED
        /*
         declare

cursor batches_cur  is
select batch_no
 from fas.batches
where batch_no   = :batches.batch_no and
      batch_year = :batches.batch_year;
batches_rec batches_cur%rowtype;

 v_batch_next_key    number;
 v_batch_lookup_key  varchar2(50);

begin

if :batches.batch_no is not null then
  last_record;
  create_record;
end if;

v_batch_lookup_key := 'BATCH_PREFIX_'||:batches.batch_year||'_'||null;

if :batches.batch_no is null then
  loop
     v_batch_next_key := amgen01a_.get_next_counter(v_batch_lookup_key);

     :batches.batch_no :=  lpad(to_char(v_batch_next_key),6,'0');

     open batches_cur ;
     fetch batches_cur into batches_rec;
     exit when batches_cur%notfound;
     close batches_cur; 
  end loop;
  close batches_cur;
end if;

:batches.account_period := get_account_period;

go_item('batches.batch_description');

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:16
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ASSIGN_BATCH.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "ASSIGN_BATCH")]
        public virtual void assignBatch_buttonClick()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
             

            int rowCount = 0;
            {
                String sqlbatchesCur = "SELECT batch_no " +
" FROM fas.batches " +
" WHERE batch_no = @BATCHES_BATCH_NO AND batch_year = @BATCHES_BATCH_YEAR ";
                DataCursor batchesCur = new DataCursor(sqlbatchesCur);
                TableRow batchesRec = null;
                NNumber vBatchNextKey = NNumber.Null;
                string vBatchLookupKey = string.Empty;
                #region
                try
                {
                    if (!string.IsNullOrEmpty(batchesElement.BatchNo))
                    {
                        BlockServices.LastRecord();
                        BlockServices.CreateRecord();
                    }
                    vBatchLookupKey = "BATCH_PREFIX_" + batchesElement.BatchYear + "_";
                    if (string.IsNullOrEmpty(batchesElement.BatchNo))
                    {
                        while (true)
                        {

                            vBatchNextKey = Amgen01a_.GetNextCounter(vBatchLookupKey);

                            batchesElement.BatchNo = Lib.Lpad(Lib.ToChar(vBatchNextKey), 6, "0").ToString();
                            //Setting query parameters
                            batchesCur.AddParameter("@BATCHES_BATCH_NO", batchesElement.BatchNo);
                            batchesCur.AddParameter("@BATCHES_BATCH_YEAR", batchesElement.BatchYear);
                            batchesCur.Open();
                            batchesRec = batchesCur.FetchRow();
                            if (batchesCur.NotFound)
                                break;
                        }
                    }
                    batchesElement.AccountPeriod = this.Task.Services.FGetAccountPeriod(batchesElement);
                    ItemServices.GoItem("batches.batch_description");
                }
                finally
                {
                    batchesCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER BATCH_YEAR.WHEN-VALIDATE-ITEM
        /*
         declare

alert_status number;
hold_account_period varchar(2) := get_account_period;

begin

if :batches.account_period  is null then
 :batches.account_period := hold_account_period;
end if;

if :batches.account_period <> hold_account_period and
     :batches.account_period <> '  ' and
     :batches.account_period is not null then
    alert_status := show_alert('account_period_change');

    if alert_status = alert_button1 then
         :batches.account_period := hold_account_period;
    end if;

end if;



end;   
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:18
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCH_YEAR.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "BATCH_YEAR")]
        public virtual void batchYear_validate()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
            {
                NNumber alertStatus = NNumber.Null;
                string holdAccountPeriod = this.Task.Services.FGetAccountPeriod(batchesElement);
                if (string.IsNullOrEmpty(batchesElement.AccountPeriod))
                {
                    batchesElement.AccountPeriod = holdAccountPeriod;
                }
                if (batchesElement.AccountPeriod != holdAccountPeriod && batchesElement.AccountPeriod != "  " && !string.IsNullOrEmpty(batchesElement.AccountPeriod))
                {
                    alertStatus = TaskServices.ShowAlert("account_period_change");
                    if (alertStatus == MessageServices.BUTTON1)
                    {
                        batchesElement.AccountPeriod = holdAccountPeriod;
                    }
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER BATCH_NO.WHEN-VALIDATE-ITEM
        /*
         declare
v_message1          varchar2(500) := 'New batch prefix ';
v_message2          varchar2(500) := ' will be created.  Do you want to create the prefix?';

v_batch_prefix      varchar2(2);
v_lookup_key        adm.profiles.profile_key%type;
v_count             number :=0;
v_length_user_batch number :=0;
v_alert             number;
v_batch_next_key    number;

  cursor check_prefix(prefix_in varchar2) is
  select count(1)
    from adm.next_keys
    where key_description = prefix_in;

alert_status number;
hold_account_period varchar(2) := get_account_period;



begin


<multilinecomment>***************************************************
alio- 307 stop and limit scope.
Batch No prefix functionality was written and sucessfuly unit tested.
However it is being deferred.
To bring this back -- 
The next_keys_batch is a hidden field with a visable lov button 
the code in the lov wbp trigger will process the batch with a prefix
It was designed as an ad hoc prefix but it needs to be limited to batch_type.

If this comes back I believe this lov should display the batch_type and key limited to batch type.
something like this:
Batch Type       Batch Key
AP            BATCH_PREFIX_:global.default_year_AP 
RE            BATCH_PREFIX_:global.default_year_RE 
AR            open, ready to use
BM            open, ready to use

For the lov list we probably want to create a batch_type table rather than a hard coded list


The adm.profile key ALLOW_PREFIX_BATCHES is read in the wnfi 
it will need to set to YES to generate prefixes.

***************************************************</multilinecomment>   

-- see if the lov was used to set the batch_no
if :batches.next_keys_batch is not null or 
 :batches.batch_no is null then
   :batches.next_keys_batch := null;   
else   

begin

    v_batch_prefix      := substr(:batches.batch_no,1,2);
    v_length_user_batch := nvl(length(:batches.batch_no),0); 

  -- the batch prefix can't be two numbers if it is then this is a batch_no without a 
  -- prefix
if v_batch_prefix is not null then
     v_count := to_number(v_batch_prefix);
   v_count := to_number(substr( v_batch_prefix,1,1)); 
   v_batch_prefix := null;
end if;

  exception when others then

      -- to_number failed so this is an Alpha prefix 
   if :parameter.allow_prefix_batches <> 'YES' THEN
          message ('Batch No nust be numeric.');	
          message(' ',no_acknowledge);
          raise form_trigger_failure; 
   end if;


      v_lookup_key := 'BATCH_PREFIX_'||:batches.batch_year||'_'||v_batch_prefix;
      v_count := 0;
    open check_prefix(v_lookup_key);
   fetch check_prefix into v_count;
   close check_prefix;


      -- if the prefix doesn't exist ask them if we need to create it.
      -- Yes continue, No Stop
      if nvl(v_count,0) = 0 then
            set_alert_property('BATCH_PREFIX',alert_message_text,
                            v_message1||
                            v_batch_prefix||
                            v_message2 );
        v_alert := show_alert('BATCH_PREFIX');
        if v_alert <> ALERT_BUTTON1 then
           raise form_trigger_failure;                       
        end if;

      end if; 
   end; -- internal begin/end 


      if v_length_user_batch = 0 then 

        v_lookup_key     := 'BATCH_PREFIX_'||:batches.batch_year||'_'||v_batch_prefix;
      v_batch_next_key := amgen01a_.get_next_counter(v_lookup_key);


           if v_batch_prefix is null then
              :batches.batch_no := lpad(:batches.batch_no,6,'0');
           else	  
              :batches.batch_no := nvl(v_batch_prefix,'00')||lpad(:batches.batch_no,4,'0');
           end if;
       else
        -- they want to use a value they keyed directly into the form or they used the button and
        -- now this trigger is looking at a generated no.
        -- we know the prefix exists but we don't know for sure that the batch_no is available.

       if length(:batches.batch_no) = 6  then 
         null; 
       else 
           if v_batch_prefix is null then
              :batches.batch_no := lpad(:batches.batch_no,6,'0');
           else	  
              :batches.batch_no := nvl(v_batch_prefix,'00')||lpad(:batches.batch_no,4,'0');
           end if;
       end if;

       v_count := 0;
        select count(1)
         into v_count  
          from fas.batches bm
         where batch_no   =	:batches.batch_no
           and batch_year = :batches.batch_year; 

        if v_count > 0 then
          message ('This Batch No has already been used.');	
          message(' ',no_acknowledge);
          raise form_trigger_failure;

        end if;		


    end if;     

     --:batches.account_period := get_account_period;  --cec alio-307 - 9/6/12 replaced this line with validation to match logic for acct period from rest of form
if :batches.account_period  is null then
 :batches.account_period := hold_account_period;
end if;

if :batches.account_period <> hold_account_period and
     :batches.account_period <> '  ' and
     :batches.account_period is not null then
    alert_status := show_alert('account_period_change');

    if alert_status = alert_button1 then
         :batches.account_period := hold_account_period;
    end if;

end if;
end if; -- LOV set the value   	 

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:20
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCH_NO.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "BATCH_NO")]
        public virtual void batchNo_validate()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
            int rowCount = 0;
            {
                string vMessage1 = "New batch prefix ";
                string vMessage2 = " will be created.  Do you want to create the prefix?";
                string vBatchPrefix = string.Empty;
                NNumber vLookupKey = NNumber.Null;
                NNumber vCount = 0;
                NNumber vLengthUserBatch = 0;
                NNumber vAlert = NNumber.Null;
                NNumber vBatchNextKey = NNumber.Null;
                String sqlcheckPrefix = "SELECT count(1) " +
" FROM adm.next_keys " +
" WHERE key_description = @P_PREFIX_IN ";
                DataCursor checkPrefix = new DataCursor(sqlcheckPrefix);
                String checkPrefixPrefixIn = string.Empty;
                NNumber alertStatus = NNumber.Null;
                string holdAccountPeriod = this.Task.Services.FGetAccountPeriod(batchesElement);
                // ***************************************************
                // alio- 307 stop and limit scope.
                // Batch No prefix functionality was written and sucessfuly unit tested.
                // However it is being deferred.
                // To bring this back --
                // The next_keys_batch is a hidden field with a visable lov button
                // the code in the lov wbp trigger will process the batch with a prefix
                // It was designed as an ad hoc prefix but it needs to be limited to batch_type.
                // If this comes back I believe this lov should display the batch_type and key limited to batch type.
                // something like this:
                // Batch Type       Batch Key
                // AP            BATCH_PREFIX_:global.default_year_AP
                // RE            BATCH_PREFIX_:global.default_year_RE
                // AR            open, ready to use
                // BM            open, ready to use
                // For the lov list we probably want to create a batch_type table rather than a hard coded list
                // The adm.profile key ALLOW_PREFIX_BATCHES is read in the wnfi
                // it will need to set to YES to generate prefixes.
                // ***************************************************
                //  see if the lov was used to set the batch_no
                if (!string.IsNullOrEmpty(batchesElement.NextKeysBatch) || string.IsNullOrEmpty(batchesElement.BatchNo))
                {
                    batchesElement.NextKeysBatch = null;
                }
                else
                {
                    #region
                    try
                    {
                        try
                        {
                            vBatchPrefix = Lib.Substring(batchesElement.BatchNo, 1, 2).ToString();
                            vLengthUserBatch = Lib.IsNull(Lib.Length(batchesElement.BatchNo), 0);
                            //  the batch prefix can't be two numbers if it is then this is a batch_no without a 
                            //  prefix
                            if (!string.IsNullOrEmpty(vBatchPrefix))
                            {
                                vCount = NNumber.ToNumber(vBatchPrefix);
                                vCount = NNumber.ToNumber(Lib.Substring(vBatchPrefix, 1, 1));
                                vBatchPrefix = null;
                            }
                        }
                        catch (TaskControlException) { throw; }
                        catch
                        {
                            //  to_number failed so this is an Alpha prefix 
                            if (this.Model.Params.AllowPrefixBatches != "YES")
                            {
                                TaskServices.Message("Batch No nust be numeric.");
                                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                                throw new ApplicationException();
                            }
                            vLookupKey = "BATCH_PREFIX_" + batchesElement.BatchYear + "_" + vBatchPrefix;
                            vCount = 0;
                            checkPrefixPrefixIn = vLookupKey;
                            //Setting query parameters
                            checkPrefix.AddParameter("@P_PREFIX_IN", checkPrefixPrefixIn);
                            checkPrefix.Open();
                            ResultSet checkPrefixResults = checkPrefix.FetchInto();
                            if (checkPrefixResults != null)
                            {
                                vCount = checkPrefixResults.GetNumber(0);
                            }
                            //  if the prefix doesn't exist ask them if we need to create it.
                            //  Yes continue, No Stop
                            if (Lib.IsNull(vCount, 0) == 0)
                            {
                                MessageServices.SetAlertMessageText("BATCH_PREFIX", vMessage1 + vBatchPrefix + vMessage2);
                                vAlert = TaskServices.ShowAlert("BATCH_PREFIX");
                                if (vAlert != MessageServices.BUTTON1)
                                {
                                    throw new ApplicationException();
                                }
                            }
                        }
                    }
                    finally
                    {
                        checkPrefix.Close();
                    }
                    #endregion
                    //  internal begin/end 
                    if (vLengthUserBatch == 0)
                    {
                        vLookupKey = "BATCH_PREFIX_" + batchesElement.BatchYear + "_" + vBatchPrefix;

                        vBatchNextKey = Amgen01a_.GetNextCounter(vLookupKey);

                        if (string.IsNullOrEmpty(vBatchPrefix))
                        {
                            batchesElement.BatchNo = Lib.Lpad(batchesElement.BatchNo, 6, "0").ToString();
                        }
                        else
                        {
                            batchesElement.BatchNo = Lib.IsNull(vBatchPrefix, "00") + Lib.Lpad(batchesElement.BatchNo, 4, "0");
                        }
                    }
                    else
                    {
                        //  they want to use a value they keyed directly into the form or they used the button and
                        //  now this trigger is looking at a generated no.
                        //  we know the prefix exists but we don't know for sure that the batch_no is available.
                        if (Lib.Length(batchesElement.BatchNo) == 6)
                        {
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(vBatchPrefix))
                            {
                                batchesElement.BatchNo = Lib.Lpad(batchesElement.BatchNo, 6, "0").ToString();
                            }
                            else
                            {
                                batchesElement.BatchNo = Lib.IsNull(vBatchPrefix, "00") + Lib.Lpad(batchesElement.BatchNo, 4, "0");
                            }
                        }
                        vCount = 0;
                        #region Execute data command
                        String sql1 = "SELECT count(1) " +
" FROM fas.batches bm " +
" WHERE batch_no = @BATCHES_BATCH_NO AND batch_year = @BATCHES_BATCH_YEAR ";
                        DataCommand command1 = new DataCommand(sql1);
                        //Setting query parameters
                        command1.AddParameter("@BATCHES_BATCH_NO", batchesElement.BatchNo);
                        command1.AddParameter("@BATCHES_BATCH_YEAR", batchesElement.BatchYear);
                        ResultSet results1 = command1.ExecuteQuery();
                        rowCount = command1.RowCount;
                        if (results1.HasData)
                        {
                            vCount = results1.GetNumber(0);
                        }
                        results1.Close();
                        #endregion
                        if (vCount > 0)
                        {
                            TaskServices.Message("This Batch No has already been used.");
                            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                            throw new ApplicationException();
                        }
                    }
                    // :batches.account_period := get_account_period;  --cec alio-307 - 9/6/12 replaced this line with validation to match logic for acct period from rest of form
                    if (string.IsNullOrEmpty(batchesElement.AccountPeriod))
                    {
                        batchesElement.AccountPeriod = holdAccountPeriod;
                    }
                    if (batchesElement.AccountPeriod != holdAccountPeriod && batchesElement.AccountPeriod != "  " && !string.IsNullOrEmpty(batchesElement.AccountPeriod))
                    {
                        alertStatus = TaskServices.ShowAlert("account_period_change");
                        if (alertStatus == MessageServices.BUTTON1)
                        {
                            batchesElement.AccountPeriod = holdAccountPeriod;
                        }
                    }
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER BATCH_LOV_BTN.WHEN-BUTTON-PRESSED
        /*

declare

v_batch_prefix     varchar2(2) := null;
v_next_key_lookup  varchar2(50);
v_batch_next_key   number;
v_length           number;	

 cursor batches_cur  is
select batch_no
 from fas.batches
where batch_no   = :batches.batch_no and
      batch_year = :batches.batch_year;
batches_rec batches_cur%rowtype;
alert_status number;
hold_account_period varchar(2) := get_account_period;   
begin


go_item('batches.NEXT_KEYS_BATCH');
do_key('list_values');

if :batches.next_keys_batch is not null then

 if length (:batches.next_keys_batch) =  15 then
       v_batch_prefix := null;
       v_length       := 6;
   else
       v_batch_prefix := substr(:batches.next_keys_batch,-2);
       v_length       := 4;
   end if;

 v_next_key_lookup :=  'BATCH_PREFIX_'||:batches.batch_year||'_'||v_batch_prefix;    

     loop
       v_batch_next_key := amgen01a_.get_next_counter(v_next_key_lookup);

      -- lpad the batch_no to 6 if the key is null otherwise append it to the batch 
      :batches.batch_no := v_batch_prefix||lpad(to_char(v_batch_next_key),v_length,'0');

        open batches_cur ;
       fetch batches_cur into batches_rec;
       exit when batches_cur%notfound;
       close batches_cur; 
    end loop;
  close batches_cur;

--:batches.account_period := get_account_period;--cec alio-307 9/6/12 replaced this line with below logic to match rest of form for acct period
if :batches.account_period  is null then
 :batches.account_period := hold_account_period;
end if;

if :batches.account_period <> hold_account_period and
     :batches.account_period <> '  ' and
     :batches.account_period is not null then
    alert_status := show_alert('account_period_change');

    if alert_status = alert_button1 then
         :batches.account_period := hold_account_period;
    end if;

end if;
end if;
end;


        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:22
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCH_LOV_BTN.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "BATCH_NO")]
        public virtual void batchLovBtn_buttonClick()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
            int rowCount = 0;
            {
                string vBatchPrefix = null;
                string vNextKeyLookup = string.Empty;
                NNumber vBatchNextKey = NNumber.Null;
                NNumber vLength = NNumber.Null;
                String sqlbatchesCur = "SELECT batch_no " +
" FROM fas.batches " +
" WHERE batch_no = @BATCHES_BATCH_NO AND batch_year = @BATCHES_BATCH_YEAR ";
                DataCursor batchesCur = new DataCursor(sqlbatchesCur);
                TableRow batchesRec = null;
                NNumber alertStatus = NNumber.Null;
                string holdAccountPeriod = this.Task.Services.FGetAccountPeriod(batchesElement);
                #region
                try
                {
                    ItemServices.GoItem("batches.NEXT_KEYS_BATCH");
                    TaskServices.ExecuteAction("LIST_VALUES");
                    if (!string.IsNullOrEmpty(batchesElement.NextKeysBatch))
                    {
                        if (Lib.Length(batchesElement.NextKeysBatch) == 15)
                        {
                            vBatchPrefix = null;
                            vLength = 6;
                        }
                        else
                        {
                            vBatchPrefix = Lib.Substring(batchesElement.NextKeysBatch, -(2)).ToString();
                            vLength = 4;
                        }
                        vNextKeyLookup = "BATCH_PREFIX_" + batchesElement.BatchYear + "_" + vBatchPrefix;
                        while (true)
                        {
                            vBatchNextKey = Amgen01a_.GetNextCounter(vNextKeyLookup);

                            //  lpad the batch_no to 6 if the key is null otherwise append it to the batch 
                            batchesElement.BatchNo = vBatchPrefix + Lib.Lpad(Lib.ToChar(vBatchNextKey), vLength, "0").ToString();
                            //Setting query parameters
                            batchesCur.AddParameter("@BATCHES_BATCH_NO", batchesElement.BatchNo);
                            batchesCur.AddParameter("@BATCHES_BATCH_YEAR", batchesElement.BatchYear);
                            batchesCur.Open();
                            batchesRec = batchesCur.FetchRow();
                            if (batchesCur.NotFound)
                                break;
                        }
                        // :batches.account_period := get_account_period;--cec alio-307 9/6/12 replaced this line with below logic to match rest of form for acct period
                        if (string.IsNullOrEmpty(batchesElement.AccountPeriod))
                        {
                            batchesElement.AccountPeriod = holdAccountPeriod;
                        }
                        if (batchesElement.AccountPeriod != holdAccountPeriod && batchesElement.AccountPeriod != "  " && !string.IsNullOrEmpty(batchesElement.AccountPeriod))
                        {
                            alertStatus = TaskServices.ShowAlert("account_period_change");
                            if (alertStatus == MessageServices.BUTTON1)
                            {
                                batchesElement.AccountPeriod = holdAccountPeriod;
                            }
                        }
                    }
                }
                finally
                {
                    batchesCur.Close();
                }
                #endregion
            }
        }


        #region Original PL/SQL code code for TRIGGER WARRANT_LOV_BTN.WHEN-BUTTON-PRESSED
        /*

declare

begin
go_item('batches.warrant_no');
do_key('list_values');

end;


        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:25
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>WARRANT_LOV_BTN.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "WARRANT_LOV_BTN")]
        public virtual void warrantLovBtn_buttonClick()
        {
            {
                ItemServices.GoItem("batches.warrant_no");
                TaskServices.ExecuteAction("LIST_VALUES");
            }
        }


        #region Original PL/SQL code code for TRIGGER DATE_CREATED.KEY-LISTVAL
        /*
         BEGIN

date_lov.get_date(sysdate,                      -- initial date
              'batches.date_created',         -- return block.item
              240,                            -- window x position
              60,                             -- window y position
              'Date Created',                 -- window title
              'OK',                           -- ok button label
              'Cancel',                       -- cancel button label
              TRUE,                           -- highlight weekend days
              FALSE,                          -- autoconfirm selection
              FALSE);                         -- autoskip after selection

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:27
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>DATE_CREATED.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "DATE_CREATED", Function = KeyFunction.LIST_VALUES)]
        public virtual void dateCreated_ListValues()
        {
            System.Diagnostics.Debugger.Break();
            //Task.Libraries.Calendar.DateLov.GetDate(NDate.Now, "batches.date_created", 240, 60, "Date Created", "OK", "Cancel", true, false, false);
        }


        #region Original PL/SQL code code for TRIGGER DATE_CREATED.WHEN-MOUSE-DOUBLECLICK
        /*
         begin
 go_block('batches');
 go_record(to_number(:system.cursor_record));
go_item('batches.date_created');
do_key('list_values');
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:28
        /// 
        /// F2N_NOT_SUPPORTED : There is no mapping of trigger DATE_CREATED.WHEN-MOUSE-DOUBLECLICK . See documentation for details.
        ///</remarks>
        /// <TriggerInfo>DATE_CREATED.WHEN-MOUSE-DOUBLECLICK</TriggerInfo>

        [ActionTrigger(Action = "WHEN-MOUSE-DOUBLECLICK", Item = "DATE_CREATED")]
        public virtual void dateCreated_WhenMouseDoubleclick()
        {
            BlockServices.GoBlock("batches");
            BlockServices.SetCurrentRecord(Lib.ToNumber(TaskServices.CursorRecord));
            ItemServices.GoItem("batches.date_created");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER BATCH_DATE.KEY-LISTVAL
        /*
         BEGIN

date_lov.get_date(sysdate,                      -- initial date
              'batches.batch_date',           -- return block.item
              240,                            -- window x position
              60,                             -- window y position
              'Batch Date',                   -- window title
              'OK',                           -- ok button label
              'Cancel',                       -- cancel button label
              TRUE,                           -- highlight weekend days
              FALSE,                          -- autoconfirm selection
              FALSE);                         -- autoskip after selection

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:30
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCH_DATE.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "BATCH_DATE", Function = KeyFunction.LIST_VALUES)]
        public virtual void batchDate_ListValues()
        {
            System.Diagnostics.Debugger.Break();
            //Task.Libraries.Calendar.DateLov.GetDate(NDate.Now, "batches.batch_date", 240, 60, "Batch Date", "OK", "Cancel", true, false, false);
        }


        #region Original PL/SQL code code for TRIGGER BATCH_DATE.WHEN-MOUSE-DOUBLECLICK
        /*
         begin
 go_block('batches');
 go_record(to_number(:system.cursor_record));
go_item('batches.batch_date');
do_key('list_values');
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:31
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCH_DATE.WHEN-MOUSE-DOUBLECLICK</TriggerInfo>

        [ActionTrigger(Action = "WHEN-MOUSE-DOUBLECLICK", Item = "BATCH_DATE")]
        public virtual void batchDate_doubleClick()
        {
            BlockServices.GoBlock("batches");
            BlockServices.SetCurrentRecord(Lib.ToNumber(TaskServices.CursorRecord));
            ItemServices.GoItem("batches.batch_date");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER BATCH_DATE.WHEN-VALIDATE-ITEM
        /*
         declare

alert_status number;
hold_account_period varchar(2) := get_account_period;

begin

if :batches.account_period  is null then
 :batches.account_period := hold_account_period;
end if;

if :batches.account_period <> hold_account_period and
     :batches.account_period <> '  ' and
     :batches.account_period is not null then
    alert_status := show_alert('account_period_change');

    if alert_status = alert_button1 then
         :batches.account_period := hold_account_period;
    end if;

end if;



end;   
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:32
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCH_DATE.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "BATCH_DATE")]
        public virtual void batchDate_validate()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
            {
                NNumber alertStatus = NNumber.Null;
                string holdAccountPeriod = this.Task.Services.FGetAccountPeriod(batchesElement);
                if (string.IsNullOrEmpty(batchesElement.AccountPeriod))
                {
                    batchesElement.AccountPeriod = holdAccountPeriod;
                }
                if (batchesElement.AccountPeriod != holdAccountPeriod && batchesElement.AccountPeriod != "  " && !string.IsNullOrEmpty(batchesElement.AccountPeriod))
                {
                    alertStatus = TaskServices.ShowAlert("account_period_change");
                    if (alertStatus == MessageServices.BUTTON1)
                    {
                        batchesElement.AccountPeriod = holdAccountPeriod;
                    }
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER USE_THIS_BATCH.WHEN-BUTTON-PRESSED
        /*
         declare

calling_form_ varchar2(40) := get_application_property(calling_form);
record_status_ varchar2(7) := :system.record_status;

begin

if :batches.batch_no is null then
  message('You Must have a Batch Number.');
  message(' ',no_acknowledge);
  go_item('batches.assign_batch');
  raise form_trigger_failure;	 
end if;
:batches.posted_flag := 'EN';
:batches.access_flag := 'N';
synchronize; --cec alio-14432 added synchronize to prevent frm-93652 process termination
commit_form;
--   commit;

if :system.form_status = 'QUERY' then
  if 'TRUE' = get_item_property('batches.account_period',
                                item_is_valid) then
     copy(:batches.batch_no,':global.'
                                    || calling_form_
                                    || '_batch_no');
     copy(:batches.batch_year,':global.'
                              || calling_form_
                              || '_batch_year');

     exit_form; -- 08-12-1998 was do_key('exit_form');
--      else
--         exit_form;
  end if;
end if;
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:36
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>USE_THIS_BATCH.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "USE_THIS_BATCH")]
        public virtual void useThisBatch_buttonClick()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
            {
                string callingForm_ = TaskServices.CallingForm;
                string recordStatus_ = TaskServices.RecordStatus;
                if (string.IsNullOrEmpty(batchesElement.BatchNo))
                {
                    TaskServices.Message("You Must have a Batch Number.");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    ItemServices.GoItem("batches.assign_batch");
                    throw new ApplicationException();
                }
                batchesElement.PostedFlag = "EN";
                batchesElement.AccessFlag = "N";
                TaskServices.Synchronize();
                // cec alio-14432 added synchronize to prevent frm-93652 process termination
                TaskServices.Commit();
                //    commit;
                if (TaskServices.FormStatus == "QUERY")
                {
                    if (ItemServices.GetIsValid("batches.account_period"))
                    {
                        TaskServices.Copy(batchesElement.BatchNo, ":global." + callingForm_ + "_batch_no");
                        TaskServices.Copy(batchesElement.BatchYear, ":global." + callingForm_ + "_batch_year");
                        TaskServices.ExitTask();
                    }
                }
            }
        }


        #region Original PL/SQL code code for TRIGGER DETAILS.WHEN-BUTTON-PRESSED
        /*
         begin
display_error;
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:38
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>DETAILS.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "DETAILS")]
        public virtual void details_buttonClick()
        {
            System.Diagnostics.Debugger.Break();
        }


        #region Original PL/SQL code code for TRIGGER ASSIGN_WARRANT.WHEN-BUTTON-PRESSED
        /*
         declare
v_warrant_next_key    number;
v_warrant_lookup_key  varchar2(50);

begin

-- if :batches.batch_no is not null then
--   last_record;
--end if;

v_warrant_lookup_key := 'warrant_NO_'||:batches.batch_year||'_'||null;

if :batches.warrant_no is null then
  v_warrant_next_key  := amgen01a_.get_next_counter(v_warrant_lookup_key);
  :batches.warrant_no := :batches.batch_year|| lpad(to_char(v_warrant_next_key),6,'0'); --ends up in claim_no which is 8
  --:batches.warrant_no := :batches.batch_year||'-'|| lpad(to_char(v_warrant_next_key),5,'0');
end if;

go_item('batches.warrant_from_date');

end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:42
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>ASSIGN_WARRANT.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "ASSIGN_WARRANT")]
        public virtual void assignWarrant_buttonClick()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
            {
                NNumber vWarrantNextKey = NNumber.Null;
                string vWarrantLookupKey = string.Empty;
                //  if :batches.batch_no is not null then
                //    last_record;
                // end if;
                vWarrantLookupKey = "warrant_NO_" + batchesElement.BatchYear + "_";
                if (string.IsNullOrEmpty(batchesElement.WarrantNo))
                {
                    vWarrantNextKey = Amgen01a_.GetNextCounter(vWarrantLookupKey);

                    batchesElement.WarrantNo = batchesElement.BatchYear + Lib.Lpad(Lib.ToChar(vWarrantNextKey), 6, "0").ToString();
                }
                ItemServices.GoItem("batches.warrant_from_date");
            }
        }


        #region Original PL/SQL code code for TRIGGER WARRANT_NO.WHEN-VALIDATE-ITEM
        /*
         declare
v_count number := 0;

begin

null;

<multilinecomment>	
        select count(1)
         into v_count  
          from fas.batches bm
         where warrant_no   =	:batches.warrant_no; 

        if v_count > 0 then
          message ('This Warrant No has already been used.');	
          message(' ',no_acknowledge);
          raise form_trigger_failure;

        end if;		
</multilinecomment>
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:44
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>WARRANT_NO.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "WARRANT_NO")]
        public virtual void warrantNo_validate()
        {
        }


        #region Original PL/SQL code code for TRIGGER WARRANT_FROM_DATE.KEY-LISTVAL
        /*
         BEGIN

date_lov.get_date(sysdate,                      -- initial date
              'batches.warrant_from_date',    -- return block.item
              240,                            -- window x position
              60,                             -- window y position
              'From Date',                    -- window title
              'OK',                           -- ok button label
              'Cancel',                       -- cancel button label
              TRUE,                           -- highlight weekend days
              FALSE,                          -- autoconfirm selection
              FALSE);                         -- autoskip after selection

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:46
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>WARRANT_FROM_DATE.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "WARRANT_FROM_DATE", Function = KeyFunction.LIST_VALUES)]
        public virtual void warrantFromDate_ListValues()
        {
            System.Diagnostics.Debugger.Break();
            //Task.Libraries.Calendar.DateLov.GetDate(NDate.Now, "batches.warrant_from_date", 240, 60, "From Date", "OK", "Cancel", true, false, false);
        }


        #region Original PL/SQL code code for TRIGGER WARRANT_FROM_DATE.WHEN-MOUSE-DOUBLECLICK
        /*
         begin
 go_block('batches');
 go_record(to_number(:system.cursor_record));
go_item('batches.warrant_from_date');
do_key('list_values');
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:47
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>WARRANT_FROM_DATE.WHEN-MOUSE-DOUBLECLICK</TriggerInfo>

        [ActionTrigger(Action = "WHEN-MOUSE-DOUBLECLICK", Item = "WARRANT_FROM_DATE")]
        public virtual void warrantFromDate_doubleClick()
        {
            BlockServices.GoBlock("batches");
            BlockServices.SetCurrentRecord(Lib.ToNumber(TaskServices.CursorRecord));
            ItemServices.GoItem("batches.warrant_from_date");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER WARRANT_TO_DATE.KEY-LISTVAL
        /*
         BEGIN

date_lov.get_date(sysdate,                      -- initial date
              'batches.warrant_to_date',      -- return block.item
              240,                            -- window x position
              60,                             -- window y position
              'To Date',                      -- window title
              'OK',                           -- ok button label
              'Cancel',                       -- cancel button label
              TRUE,                           -- highlight weekend days
              FALSE,                          -- autoconfirm selection
              FALSE);                         -- autoskip after selection

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:49
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>WARRANT_TO_DATE.KEY-LISTVAL</TriggerInfo>

        [ActionTrigger(Action = "KEY-LISTVAL", Item = "WARRANT_TO_DATE", Function = KeyFunction.LIST_VALUES)]
        public virtual void warrantToDate_ListValues()
        {
            System.Diagnostics.Debugger.Break();
            //Task.Libraries.Calendar.DateLov.GetDate(NDate.Now, "batches.warrant_to_date", 240, 60, "To Date", "OK", "Cancel", true, false, false);
        }


        #region Original PL/SQL code code for TRIGGER WARRANT_TO_DATE.WHEN-MOUSE-DOUBLECLICK
        /*
         begin
 go_block('batches');
 go_record(to_number(:system.cursor_record));
go_item('batches.warrant_to_date');
do_key('list_values');
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:50
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>WARRANT_TO_DATE.WHEN-MOUSE-DOUBLECLICK</TriggerInfo>

        [ActionTrigger(Action = "WHEN-MOUSE-DOUBLECLICK", Item = "WARRANT_TO_DATE")]
        public virtual void warrantToDate_doubleClick()
        {
            BlockServices.GoBlock("batches");
            BlockServices.SetCurrentRecord(Lib.ToNumber(TaskServices.CursorRecord));
            ItemServices.GoItem("batches.warrant_to_date");
            TaskServices.ExecuteAction("LIST_VALUES");
        }


        #region Original PL/SQL code code for TRIGGER WARRANT_TO_DATE.WHEN-VALIDATE-ITEM
        /*
         begin

validate_warrant_dates;	


end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:51
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>WARRANT_TO_DATE.WHEN-VALIDATE-ITEM</TriggerInfo>

        [ValidationTrigger(Item = "WARRANT_TO_DATE")]
        public virtual void warrantToDate_validate()
        {
            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
            this.Task.Services.ValidateWarrantDates(batchesElement);
        }


        #region Original PL/SQL code code for TRIGGER RESET_ACTIVE_BATCH.WHEN-BUTTON-PRESSED
        /*
         Begin
sutl01a.run_prg('fmbth03a.fmx');	
End;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:57
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>RESET_ACTIVE_BATCH.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "RESET_ACTIVE_BATCH")]
        public virtual void resetActiveBatch_buttonClick()
        {
            Task.Libraries.Sutl01a.Sutl01a.RunPrg("fmbth03a.fmx");
        }


        #region Original PL/SQL code code for TRIGGER BATCH_MASTER_LISTING.WHEN-BUTTON-PRESSED
        /*
         Declare
-- hold_otherparms varchar2(2000);  -- No parameters to pass to report
report_id	Report_Object;
hold_rep_filename varchar2(200);
hold_rep_path varchar2(200);
hold_rep_server varchar2(50);
Begin
--Turn_debug;
report_id:=FIND_REPORT_OBJECT('fmbth01a');
hold_rep_filename := get_report_object_property('fmbth01a', report_filename);
debug_msg('hold_rep_filename ' || hold_rep_filename);
hold_rep_path := :global.qtsihome || hold_rep_filename;
debug_msg('hold_rep_path ' || hold_rep_path);
hold_rep_server := :global.idi_report_server;
debug_msg('hold_rep_server ' || hold_rep_server);

RPT_RUN_REPORT(p_report_module =>  'fmbth01a',
             p_report_obj_name =>  'fmbth01a',
             --p_report_otherparam  =>  hold_otherparms,  -- No parameters to pass to report
             p_report_param_form  => 'yes');
End;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:59
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCH_MASTER_LISTING.WHEN-BUTTON-PRESSED</TriggerInfo>

        [ActionTrigger(Action = "WHEN-BUTTON-PRESSED", Item = "BATCH_MASTER_LISTING")]
        public virtual void batchMasterListing_buttonClick()
        {
            {
                //  hold_otherparms varchar2(2000);  -- No parameters to pass to report
                Types.ReportObject reportId = null;
                string holdRepFilename = string.Empty;
                string holdRepPath = string.Empty;
                string holdRepServer = string.Empty;
                // Turn_debug;
                Task.Services.DebugMsg("hold_rep_filename " + holdRepFilename);
                holdRepPath = Globals.Qtsihome + holdRepFilename;
                Task.Services.DebugMsg("hold_rep_path " + holdRepPath);
                holdRepServer = Globals.IdiReportServer;
                Task.Services.DebugMsg("hold_rep_server " + holdRepServer);

                Task.Libraries.Afilerpt.RptRunReport(pReportModule: "fmbth01a", pReportObjName: "fmbth01a", pReportOtherparam: "yes");
            }
        }


        [ActionTrigger(Function = KeyFunction.SEARCH)]
        public virtual void Batches_Search()
        {
            SetBlockWhereClause();
            base.SearchAction();
        }

        [ActionTrigger(Function = KeyFunction.EXECUTE_QUERY)]
        public virtual void Batches_ExecuteQuery()
        {
            SetBlockWhereClause();
            base.ExecuteQueryAction();

        }
        private void SetBlockWhereClause()
        {
            if (Globals.GetValue("FilteredByYear").Equals("Y"))
            {
                Globals.SetValue("FilteredByYear", "N");
                BlockServices.SetWhereClause("batches", Globals.GetValue("BatchesFilteredWithoutYear"));
            }
        }
        #endregion
    }
}
