using Alio.Common.Runtime.Controller;
using Alio.Forms.Fjpst01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using Foundations.Core.AppSupportLib.UI;
using System;

namespace Alio.Forms.Fjpst01a.Controllers
{


    public class BatchesController : BaseBlockController<Fjpst01aTask, Fjpst01aModel>
    {

        public BatchesController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        #region Original PL/SQL code code for TRIGGER BATCHES.WHEN-NEW-RECORD-INSTANCE
        /*
         begin
if :system.mode = 'ENTER-QUERY' then
  :batches.batch_year := :global.default_year;
end if;
end;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:24
        /// 
        /// 
        ///</remarks>
        /// <TriggerInfo>BATCHES.WHEN-NEW-RECORD-INSTANCE</TriggerInfo>

        [ActionTrigger(Action = "WHEN-NEW-RECORD-INSTANCE", Function = KeyFunction.RECORD_CHANGE)]
        [Before]
        public virtual void batches_recordChange()
        {

            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;


            if (TaskServices.Mode == "ENTER-QUERY")
            {
                batchesElement.BatchYear = Alio.Common.Globals.DefaultYear;
            }
        }


        #region Original PL/SQL code code for TRIGGER BATCHES.POST-QUERY
        /*
         BEGIN

 select sum(journal_amount)
   into :batches.debit_total
   from fas.je_header,
        fas.je_data
  where je_header.batch_no   = :batches.batch_no
    and je_header.batch_year = :batches.batch_year
    and je_data.reference_no = je_header.reference_no
    and je_data.debit_credit_flag = '1';

 select sum(journal_amount)
   into :batches.credit_total
   from fas.je_header,
        fas.je_data
  where je_header.batch_no   = :batches.batch_no
    and je_header.batch_year = :batches.batch_year
    and je_data.reference_no = je_header.reference_no
    and je_data.debit_credit_flag = '-1';

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:25
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>BATCHES.POST-QUERY</TriggerInfo>

        [AfterQuery]
        public virtual void batches_AfterQuery(RowAdapterEventArgs args)
        {
            BatchesAdapter batchesElement = args.Row as BatchesAdapter;


            int rowCount = 0;
            #region Execute data command
            String sql1 = "SELECT sum(journal_amount) " +
" FROM fas.je_header, fas.je_data " +
" WHERE je_header.batch_no = @BATCHES_BATCH_NO AND je_header.batch_year = @BATCHES_BATCH_YEAR AND je_data.reference_no = je_header.reference_no AND je_data.debit_credit_flag = '1' ";
            DataCommand command1 = new DataCommand(sql1);
            //Setting query parameters
            command1.AddParameter("@BATCHES_BATCH_NO", batchesElement.BatchNo);
            command1.AddParameter("@BATCHES_BATCH_YEAR", batchesElement.BatchYear);
            ResultSet results1 = command1.ExecuteQuery();
            rowCount = command1.RowCount;
            if (results1.HasData)
            {
                batchesElement.DebitTotal = results1.GetNumber(0);
            }
            results1.Close();
            #endregion
            #region Execute data command
            String sql2 = "SELECT sum(journal_amount) " +
" FROM fas.je_header, fas.je_data " +
" WHERE je_header.batch_no = @BATCHES_BATCH_NO AND je_header.batch_year = @BATCHES_BATCH_YEAR AND je_data.reference_no = je_header.reference_no AND je_data.debit_credit_flag = '-1' ";
            DataCommand command2 = new DataCommand(sql2);
            //Setting query parameters
            command2.AddParameter("@BATCHES_BATCH_NO", batchesElement.BatchNo);
            command2.AddParameter("@BATCHES_BATCH_YEAR", batchesElement.BatchYear);
            ResultSet results2 = command2.ExecuteQuery();
            rowCount = command2.RowCount;
            if (results2.HasData)
            {
                batchesElement.CreditTotal = results2.GetNumber(0);
            }
            results2.Close();
            #endregion
        }


        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>BATCHES.PRE-QUERY</TriggerInfo>

        [BeforeQuery]
        public virtual void batches_BeforeQuery(QueryEventArgs args)
        {
            BatchesAdapter batchesElement = this.Model.Batches.CurrentRowAdapter;
            args.Source.SelectCommandParams.Add(DbManager.DataBaseFactory.CreateDataParameter("BATCHES_BATCH_YEAR", batchesElement.BatchYear));

            args.Source.SelectCommandParams.Add(DbManager.DataBaseFactory.CreateDataParameter("BATCHES_BATCH_NO", batchesElement.BatchNo));

            args.Source.SelectCommandParams.Add(DbManager.DataBaseFactory.CreateDataParameter("GLOBAL_QTSI_SHARE_BATCHES_FLAG", Alio.Common.Globals.QtsiShareBatchesFlag));
        }



        #endregion

    }
}
