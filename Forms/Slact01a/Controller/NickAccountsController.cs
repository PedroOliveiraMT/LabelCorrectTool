using Alio.Forms.Slact01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;
using System;

namespace Alio.Forms.Slact01a.Controllers
{


    public class NickAccountsController : AbstractBlockController<Slact01aTask, Slact01aModel>
    {

        public NickAccountsController(ITaskController parentController, String name)
            : base(parentController, name)
        {
        }



        #region event handlers generated from Forms triggers
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:
        /// 
        /// 
        ///</remarks>
        /// <param name="args"></param>
        /// <TriggerInfo>NICK_ACCOUNTS.PRE-QUERY</TriggerInfo>

        [BeforeQuery]
        public virtual void nickAccounts_BeforeQuery(QueryEventArgs args)
        {
            args.Source.SelectCommandParams.Add(DbManager.DataBaseFactory.CreateDataParameter("GLOBAL_QTSI_USER_ID", Alio.Common.Globals.QtsiUserId));
        }



        #endregion

    }
}
