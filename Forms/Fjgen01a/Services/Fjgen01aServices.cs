using Alio.Forms.Fjgen01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fjgen01a.Services
{
    public class Fjgen01aServices : AbstractServices<Fjgen01aModel>
    {

        public Fjgen01aServices(Fjgen01aModel model) : base(model)
        {
        }

        public new Fjgen01aTask Task
        {
            get { return (Fjgen01aTask)base.Task; }
        }

        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_NO
        /*
		FUNCTION get_account_no (account_id_in number) RETURN varchar2 IS

	cursor get_account_no(cur_in_acct_id number) is
	  select account_no
	    from shr.accounts
	   where account_id = cur_in_acct_id;
	     
	hold_account_no varchar2(50);
	     
begin
	
	open get_account_no(account_id_in);
	fetch get_account_no into hold_account_no;
	close get_account_no;
	
	return hold_account_no;
	
end;

		*/
        #endregion
        //ID:109
        /// <summary></summary>
        /// <param name="getAccountNo"></param>
        /// <remarks>
        /// </remarks>
        public virtual string FGetAccountNo(NNumber accountIdIn)
        {
            int rowCount = 0;
            String sqlgetAccountNo = "SELECT account_no " +
    " FROM shr.accounts " +
    " WHERE account_id = @P_CUR_IN_ACCT_ID ";
            DataCursor getAccountNo = new DataCursor(sqlgetAccountNo);
            NNumber getAccountNoCurInAcctId = NNumber.Null;
            string holdAccountNo = string.Empty;
            #region
            try
            {
                getAccountNoCurInAcctId = accountIdIn;
                //Setting query parameters
                getAccountNo.AddParameter("@P_CUR_IN_ACCT_ID", getAccountNoCurInAcctId);
                getAccountNo.Open();
                ResultSet getAccountNoResults = getAccountNo.FetchInto();
                if (getAccountNoResults != null)
                {
                    holdAccountNo = getAccountNoResults.GetString(0);
                }
                return holdAccountNo;
            }
            finally
            {
                getAccountNo.Close();
            }
            #endregion
        }



    }
}
