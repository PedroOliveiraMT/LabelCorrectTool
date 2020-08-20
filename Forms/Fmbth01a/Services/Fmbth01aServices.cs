using Alio.Common;
using Alio.Common.DbServices;
using Alio.Forms.Fmbth01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fmbth01a.Services
{
    public class Fmbth01aServices : AbstractServices<Fmbth01aModel>
    {

        public Fmbth01aServices(Fmbth01aModel model) : base(model)
        {
        }

        public new Fmbth01aTask Task
        {
            get { return (Fmbth01aTask)base.Task; }
        }

        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_PERIOD
        /*
		FUNCTION GET_ACCOUNT_PERIOD RETURN varchar2 IS

  cursor account_period_cur is
  select accounting_period accounting_period
    from shr.account_period_master
   where account_period_master.account_year = :batches.batch_year 
     and to_char(account_period_master.begin_date,'YYYYMMDD')
         <= to_char(:batches.batch_date,'YYYYMMDD') 
     and to_char(account_period_master.end_date,'YYYYMMDD')
         >= to_char(:batches.batch_date,'YYYYMMDD')
     and accounting_period between '01' and '12'  -- jag 9/2/2005
   order by accounting_period desc;

  hold_account_period varchar(2);

BEGIN
	  
	open account_period_cur;
	fetch account_period_cur into hold_account_period;
	close account_period_cur;

  return hold_account_period;
		
END;
		*/
        #endregion
        //ID:267
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual string FGetAccountPeriod(BatchesAdapter batchesElement)
        {
            int rowCount = 0;
            String sqlaccountPeriodCur = "SELECT accounting_period accounting_period " +
    " FROM shr.account_period_master " +
    " WHERE account_period_master.account_year = @BATCHES_BATCH_YEAR AND to_char(account_period_master.begin_date, 'YYYYMMDD') <= to_char(@BATCHES_BATCH_DATE, 'YYYYMMDD') AND to_char(account_period_master.end_date, 'YYYYMMDD') >= to_char(@BATCHES_BATCH_DATE, 'YYYYMMDD') AND accounting_period BETWEEN '01' AND '12' " +
    " ORDER BY accounting_period DESC";
            DataCursor accountPeriodCur = new DataCursor(sqlaccountPeriodCur);
            string holdAccountPeriod = string.Empty;
            #region
            try
            {
                //Setting query parameters
                accountPeriodCur.AddParameter("@BATCHES_BATCH_YEAR", batchesElement.BatchYear);
                accountPeriodCur.AddParameter("@BATCHES_BATCH_DATE", batchesElement.BatchDate);
                accountPeriodCur.Open();
                ResultSet accountPeriodCurResults = accountPeriodCur.FetchInto();
                if (accountPeriodCurResults != null)
                {
                    holdAccountPeriod = accountPeriodCurResults.GetString(0);
                }
                return holdAccountPeriod;
            }
            finally
            {
                accountPeriodCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit PRE_VALIDATE
        /*
		PROCEDURE PRE_VALIDATE IS
	
	alert_status number;
  hold_account_period varchar(2);
	
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

BEGIN
  
  if :batches.batch_no is null then
  	 message('Batch Number is a required field.');
  	 message(' ',no_acknowledge);
  	 raise form_trigger_failure;
  end if;

  if :batches.batch_date is null then
     message('Please Enter a Batch Date');
     message(' ',no_acknowledge);
     raise form_trigger_failure;
  end if;
  
  hold_account_period := get_account_period;
  
	if :batches.account_period <> hold_account_period and
		 :batches.account_period <> '  ' and
		 :batches.account_period is not null then
     alert_status := show_alert('account_period_change');
  
     if alert_status = alert_button1 then
     	  :batches.account_period := hold_account_period;
     end if;
  
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
  
END;
		*/
        #endregion
        //ID:268
        /// <summary></summary>
        /// <remarks>
        /// </remarks>
        public virtual void PreValidate(BatchesAdapter batchesElement)
        {
            int rowCount = 0;
            NNumber alertStatus = NNumber.Null;
            string holdAccountPeriod = string.Empty;
            String sqlyearMasterCur = "SELECT current_accounting_period " +
    " FROM shr.year_master " +
    " WHERE account_year = @BATCHES_BATCH_YEAR ";
            DataCursor yearMasterCur = new DataCursor(sqlyearMasterCur);
            TableRow yearMasterRec = null;
            String sqlaccountPeriodMasterCur = "SELECT begin_date, end_date " +
    " FROM shr.account_period_master " +
    " WHERE account_year = @BATCHES_BATCH_YEAR AND accounting_period = @BATCHES_ACCOUNT_PERIOD ";
            DataCursor accountPeriodMasterCur = new DataCursor(sqlaccountPeriodMasterCur);
            TableRow accountPeriodMasterRec = null;
            #region
            try
            {
                if (string.IsNullOrEmpty(batchesElement.BatchNo))
                {
                    TaskServices.Message("Batch Number is a required field.");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    throw new ApplicationException();
                }
                if (batchesElement.BatchDate.IsNull)
                {
                    TaskServices.Message("Please Enter a Batch Date");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    throw new ApplicationException();
                }
                holdAccountPeriod = FGetAccountPeriod(batchesElement);
                if (batchesElement.AccountPeriod != holdAccountPeriod && batchesElement.AccountPeriod != "  " && !string.IsNullOrEmpty(batchesElement.AccountPeriod))
                {
                    alertStatus = TaskServices.ShowAlert("account_period_change");
                    if (alertStatus == MessageServices.BUTTON1)
                    {
                        batchesElement.AccountPeriod = holdAccountPeriod;
                    }
                }
                //Setting query parameters
                yearMasterCur.AddParameter("@BATCHES_BATCH_YEAR", batchesElement.BatchYear);
                yearMasterCur.Open();
                yearMasterRec = yearMasterCur.FetchRow();
                if (batchesElement.AccountPeriod < yearMasterRec.GetStr("current_accounting_period"))
                {
                    ItemServices.SetIsValid("batches.account_period", false);
                    TaskServices.Message("Please enter a valid accounting period for this year");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    throw new ApplicationException();
                }
                //Setting query parameters
                accountPeriodMasterCur.AddParameter("@BATCHES_BATCH_YEAR", batchesElement.BatchYear);
                accountPeriodMasterCur.AddParameter("@BATCHES_ACCOUNT_PERIOD", batchesElement.AccountPeriod);
                accountPeriodMasterCur.Open();
                accountPeriodMasterRec = accountPeriodMasterCur.FetchRow();
                if (accountPeriodMasterCur.NotFound)
                {
                    TaskServices.Message("Please enter a valid accounting period for this year");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    throw new ApplicationException();
                }
                else if (batchesElement.BatchDate > accountPeriodMasterRec.GetDate("end_date") || batchesElement.BatchDate < accountPeriodMasterRec.GetDate("begin_date"))
                {
                    alertStatus = TaskServices.ShowAlert("account_period_warning");
                    if (alertStatus == MessageServices.BUTTON2)
                    {
                        throw new ApplicationException();
                    }
                }
            }
            finally
            {
                yearMasterCur.Close();
                accountPeriodMasterCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit DATE_CHOOSEN
        /*
		PROCEDURE date_choosen IS
BEGIN
   copy(to_char(date_lov.current_lov_date,'dd-mon-yyyy'), date_lov.date_lov_return_item);
   go_item(date_lov.date_lov_return_item);
   if date_lov.lov_auto_skip = TRUE then
      next_item;
   end if;
END;
		*/
        #endregion
        //ID:269
        /// <summary></summary>
        /// <remarks>
        /// F2N_STRONG_PRESENTATION_LOGIC : The code of this procedure was identified as containing presentation logic. See documentation for details.
        /// </remarks>
        public virtual void DateChoosen()
        {
            System.Diagnostics.Debugger.Break();
            //TaskServices.Copy(Lib.ToChar(Task.Libraries.Calendar.DateLov.currentLovDate, "dd-mon-yyyy"),Task.Libraries.Calendar.DateLov.dateLovReturnItem);
            //ItemServices.GoItem(Task.Libraries.Calendar.DateLov.dateLovReturnItem);
            //if ( Task.Libraries.Calendar.DateLov.lovAutoSkip == true )
            //{
            //	ItemServices.NextItem();
            //}
        }

        #region Original PL/SQL code for Prog Unit VALIDATE_WARRANT_DATES
        /*
		PROCEDURE validate_warrant_dates IS
BEGIN
  	if :parameter.show_warrants in ('DISPLAY','POST') and :batches.warrant_from_date is not null then
		
		if nvl(:batches.warrant_to_date,:batches.warrant_from_date ) < :batches.warrant_from_date then
			 message ('Warrant To Date must be greater than From Date.');
			 message(' ',no_acknowledge);
			 --go_item('warrant_to_date'); not allowed in wvi
			 raise form_trigger_failure;
		end if;
	end if;
END;
		*/
        #endregion
        //ID:271
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void ValidateWarrantDates(BatchesAdapter batchesElement)
        {
            if ((this.Model.Params.ShowWarrants == "DISPLAY" || this.Model.Params.ShowWarrants == "POST") && !batchesElement.WarrantFromDate.IsNull)
            {
                if (Lib.IsNull(batchesElement.WarrantToDate, batchesElement.WarrantFromDate) < batchesElement.WarrantFromDate)
                {
                    TaskServices.Message("Warrant To Date must be greater than From Date.");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    // go_item('warrant_to_date'); not allowed in wvi
                    throw new ApplicationException();
                }
            }
        }


        #region Original PL/SQL code for Prog Unit DEBUG_MSG
        /*
		PROCEDURE debug_msg(p_message varchar2) IS
  v_alert_status    NUMBER := 0;
Begin
  DEFAULT_VALUE('*', 'global.debug_status');
  If :global.debug_status = 'ON' Then
    --Message(substr(p_message,1,200));
    --Message( ' ', no_acknowledge);
    Synchronize;
    SET_ALERT_PROPERTY('debug_message',
                       alert_message_text,
                       substr(p_message,1,200));
    v_alert_status := SHOW_ALERT('debug_message');

    If v_alert_status = ALERT_BUTTON2 Then
    	Turn_debug('OFF');
    End if;

     DEFAULT_VALUE ('*', 'global.write_log_file');
     if upper(:global.write_log_file) = 'ON' then
       insert_error_message_('FRENT01A', 'DEBUG: '||p_message||'. FRENT01A.FMX '||to_char(systimestamp,'HH24:MI:SS:FF3')
                             ,sysdate, To_Number(To_Char(systimestamp, 'yymmddhh24missff3')));
     end if;
  End if;
End;
		*/
        #endregion
        //ID:272
        /// <summary></summary>
        /// <param name="pMessage"></param>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void DebugMsg(string pMessage)
        {
            NNumber vAlertStatus = 0;
            Globals.SetDefault("global.debug_status", "*");
            if (Alio.Common.Globals.DebugStatus == "ON")
            {
                // Message(substr(p_message,1,200));
                // Message( ' ', no_acknowledge);
                TaskServices.Synchronize();
                MessageServices.SetAlertMessageText("debug_message", Lib.Substring(pMessage, 1, 200));
                vAlertStatus = TaskServices.ShowAlert("debug_message");
                if (vAlertStatus == MessageServices.BUTTON2)
                {
                    TurnDebug("OFF");
                }
                Globals.SetDefault("global.write_log_file", "*");
                if (Lib.Upper(Globals.WriteLogFile) == "ON")
                {
                    StoredProcedures.InsertErrorMessage_("FRENT01A", "DEBUG: " + pMessage + ". FRENT01A.FMX " + Lib.ToChar(DateTime.Now, "HH24:MI:SS:FF3").ToString(), NDate.Now, Lib.ToNumber(Lib.ToChar(DateTime.Now, "yymmddhh24missff3")));
                }
            }
        }


        #region Original PL/SQL code for Prog Unit TURN_DEBUG
        /*
		PROCEDURE	Turn_debug(p_value Varchar2 Default 'ON') IS
  -- 01/31/20 psr alio-16722 Created procedure
BEGIN
  DEFAULT_VALUE ('*', 'global.debug_status');
 	:global.debug_status := p_value;
END;
		*/
        #endregion
        //ID:273
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public void TurnDebug()
        {
            TurnDebug("ON");
        }

        /// <summary></summary>
        /// <param name="pValue"></param>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void TurnDebug(string pValue)
        {
            Globals.SetDefault("global.debug_status", "*");
            Alio.Common.Globals.DebugStatus = pValue;
        }




    }
}
