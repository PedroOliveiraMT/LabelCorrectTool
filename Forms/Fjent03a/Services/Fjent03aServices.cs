using Alio.Common.DbServices;
using Alio.Forms.Common.DbServices;
using Alio.Forms.Fjent03a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Composition;
using Foundations.Core.AppSupportLib.Exceptions;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.Types;
using System;

namespace Alio.Forms.Fjent03a.Services
{
    public class Fjent03aServices : AbstractServices<Fjent03aModel>
    {

        public Fjent03aServices(Fjent03aModel model) : base(model)
        {
        }

        public new Fjent03aTask Task
        {
            get { return (Fjent03aTask)base.Task; }
        }

        #region Original PL/SQL code for Prog Unit HANDLE_ITEM_PROPERTIES
        /*
		<multilinecomment>*****************************************************************************</multilinecomment>
<multilinecomment> This Program Unit is a component of Object Group Item Property              </multilinecomment>
<multilinecomment> MNN Created 05/30/2003                                                      </multilinecomment>
<multilinecomment> Add a call to When_New_Form_Instance                                        </multilinecomment>
<multilinecomment> See following                                                                             </multilinecomment>
--
--   BEGIN
--      load_parameters;
--      manage_menu;
--      sutl01a.when_new_form_instance; -- Existing code
--	    handle_item_properties;
--   END;                              
--
<multilinecomment>*****************************************************************************</multilinecomment>

PROCEDURE HANDLE_ITEM_PROPERTIES IS

   CURSOR item_properties_cur is 
   select user_item_properties.menu_selection,
          user_item_properties.block_name||'.'||user_item_properties.item_name item,
          nvl(user_item_properties.prop_enabled,'Y')  prop_enabled,   
          nvl(user_item_properties.prop_update,'Y')   prop_update,    
          nvl(user_item_properties.prop_insert,'Y')   prop_insert,    
          nvl(user_item_properties.prop_visible,'Y')  prop_visible,   
          nvl(user_item_properties.prop_conceal,'N')  prop_conceal,   
          user_item_properties.prop_prompt            prop_prompt,
          nvl(user_item_properties.prop_required,'N') prop_required
     from adm.user_item_properties
    where user_item_properties.user_id = USER
      and user_item_properties.menu_selection = :system.current_form||'FMX' --upper('HMEMP01AFMX')
    union 
   select role_item_properties.menu_selection,
          role_item_properties.block_name||'.'||role_item_properties.item_name  item,
          nvl(role_item_properties.prop_enabled,'Y')  prop_enabled,   
          nvl(role_item_properties.prop_update,'Y')   prop_update,    
          nvl(role_item_properties.prop_insert,'Y')   prop_insert,    
          nvl(role_item_properties.prop_visible,'Y')  prop_visible,   
          nvl(role_item_properties.prop_conceal,'N')  prop_conceal,   
          role_item_properties.prop_prompt            prop_prompt,
          nvl(role_item_properties.prop_required,'N') prop_required
     from adm.role_item_properties
    where role_item_properties.role_id = :parameter.qtsi_role_id
      and role_item_properties.menu_selection = :system.current_form||'FMX' --upper('HMEMP01AFMX')
		and not exists (select menu_selection
		                  from adm.user_item_properties
							       where user_item_properties.user_id = USER
							         and user_item_properties.menu_selection = role_item_properties.menu_selection
								       and user_item_properties.block_name = role_item_properties.block_name
								       and user_item_properties.item_name = role_item_properties.item_name)
    union 
   select item_properties.menu_selection,
          item_properties.block_name||'.'||item_properties.item_name item,
          nvl(item_properties.prop_enabled,'Y')  prop_enabled,   
          nvl(item_properties.prop_update,'Y')   prop_update,    
          nvl(item_properties.prop_insert,'Y')   prop_insert,    
          nvl(item_properties.prop_visible,'Y')  prop_visible,   
          nvl(item_properties.prop_conceal,'N')  prop_conceal,   
          item_properties.prop_prompt            prop_prompt,
          nvl(item_properties.prop_required,'N') prop_required
     from adm.item_properties
    where item_properties.menu_selection = :system.current_form||'FMX' --upper('HMEMP01AFMX')
		and not exists (select menu_selection
		                  from adm.user_item_properties
							       where user_item_properties.user_id = USER
							         and user_item_properties.menu_selection = item_properties.menu_selection
								       and user_item_properties.block_name = item_properties.block_name
								       and user_item_properties.item_name = item_properties.item_name)
		and not exists (select menu_selection
		                  from adm.role_item_properties
							       where role_item_properties.role_id = :parameter.qtsi_role_id
                              and role_item_properties.menu_selection = item_properties.menu_selection
								       and role_item_properties.block_name = item_properties.block_name
								       and role_item_properties.item_name = item_properties.item_name)
   order by 3; --Item

   done_with_rec varchar2(1);
   v_counter     number := 0;
BEGIN
   
   hold.required_table.delete;
	 
	 for prop_rec in item_properties_cur loop

      done_with_rec := 'N';
     --message('item '||prop_rec.item); message(' ',no_acknowledge);

      --VISIBLE
      if prop_rec.prop_visible = 'N' and done_with_rec = 'N' then
         set_item_property(prop_rec.item, visible, property_false);
         done_with_rec := 'Y';
--      else
--         set_item_property(prop_rec.item, visible, property_true);
      end if;
      
      --ENABLED
      if prop_rec.prop_enabled = 'N' and done_with_rec = 'N' then
         set_item_property(prop_rec.item, enabled, property_false);
         done_with_rec := 'Y';
--      else
--         set_item_property(prop_rec.item, enabled, property_true);
      end if;
    
--      message('conceal '||prop_rec.prop_conceal||' - '||done_with_rec); message(' ',no_acknowledge);
      -- CONCEAL
      if prop_rec.prop_conceal = 'Y' and done_with_rec = 'N' then
         set_item_property(prop_rec.item, conceal_data, property_true);
         set_item_property(prop_rec.item, update_allowed, property_false);
         set_item_property(prop_rec.item, insert_allowed, property_false);
         done_with_rec := 'Y';
--      else
--         set_item_property(prop_rec.item, conceal_data, property_false);
      end if;
 
      --UPDATE
      if prop_rec.prop_update = 'N' and done_with_rec = 'N' then      
         set_item_property(prop_rec.item, update_allowed, property_false);
--      else
--         set_item_property(prop_rec.item, update_allowed, property_true);
     	end if;
       
      --INSERT
      if prop_rec.prop_insert = 'N' and done_with_rec = 'N' then
         set_item_property(prop_rec.item, insert_allowed, property_false);
--      else
--         set_item_property(prop_rec.item, insert_allowed, property_true);
      end if;

      -- PROMPT
      if prop_rec.prop_prompt is not null then
    	   set_item_property(prop_rec.item, prompt_text,prop_rec.prop_prompt);
      end if;
    
      -- REQUIRED
      if prop_rec.prop_required = 'Y' then

         Set_Item_Property(prop_rec.item, 
                           VISUAL_ATTRIBUTE,
                           'REQUIRED');       
--      else
--         Set_Item_Instance_Property(prop_rec.item, 
--                                    CURRENT_RECORD, 
--                                    VISUAL_ATTRIBUTE,
--                                    'DEFAULT');
      end if;
  
       -- REQUIRED
       -- build an array to handle the required fields.
       -- they will be set after the user tabs from the header block.
       -- note: prop_rec.item includes block
      if prop_rec.prop_required = 'Y' and done_with_rec = 'N' then      
         --Set_Item_Property(prop_rec.item, REQUIRED, PROPERTY_TRUE);
         v_counter := v_counter + 1;
        hold.required_table(v_counter).field_name := prop_rec.item;
      end if;

	 end loop;

END;
		*/
        #endregion
        //ID:324
        /// <summary></summary>
        /// <remarks>
        /// *****************************************************************************
        ///  This Program Unit is a component of Object Group Item Property              
        ///  MNN Created 05/30/2003                                                      
        ///  Add a call to When_New_Form_Instance                                        
        ///  See following                                                                             
        /// 
        ///    BEGIN
        ///       load_parameters;
        ///       manage_menu;
        ///       sutl01a.when_new_form_instance; -- Existing code
        /// 	    handle_item_properties;
        ///    END;                              
        /// 
        /// *****************************************************************************
        /// F2N_STRONG_PRESENTATION_LOGIC : The code of this procedure was identified as containing presentation logic. See documentation for details.
        /// </remarks>
        public virtual void HandleItemProperties()
        {
            int rowCount = 0;
            String sqlitemPropertiesCur = "SELECT user_item_properties.menu_selection, user_item_properties.block_name || '.' || user_item_properties.item_name item, nvl(user_item_properties.prop_enabled, 'Y') prop_enabled, nvl(user_item_properties.prop_update, 'Y') prop_update, nvl(user_item_properties.prop_insert, 'Y') prop_insert, nvl(user_item_properties.prop_visible, 'Y') prop_visible, nvl(user_item_properties.prop_conceal, 'N') prop_conceal, user_item_properties.prop_prompt prop_prompt, nvl(user_item_properties.prop_required, 'N') prop_required " +
    " FROM adm.user_item_properties " +
    " WHERE user_item_properties.user_id = USER AND user_item_properties.menu_selection = @SYSTEM_CURRENT_FORM || 'FMX' " +
    "UNION SELECT role_item_properties.menu_selection, role_item_properties.block_name || '.' || role_item_properties.item_name item, nvl(role_item_properties.prop_enabled, 'Y') prop_enabled, nvl(role_item_properties.prop_update, 'Y') prop_update, nvl(role_item_properties.prop_insert, 'Y') prop_insert, nvl(role_item_properties.prop_visible, 'Y') prop_visible, nvl(role_item_properties.prop_conceal, 'N') prop_conceal, role_item_properties.prop_prompt prop_prompt, nvl(role_item_properties.prop_required, 'N') prop_required " +
    " FROM adm.role_item_properties " +
    " WHERE role_item_properties.role_id = @PARAMETER_QTSI_ROLE_ID AND role_item_properties.menu_selection = @SYSTEM_CURRENT_FORM || 'FMX' AND  NOT EXISTS(SELECT menu_selection " +
        " FROM adm.user_item_properties " +
        " WHERE user_item_properties.user_id = USER AND user_item_properties.menu_selection = role_item_properties.menu_selection AND user_item_properties.block_name = role_item_properties.block_name AND user_item_properties.item_name = role_item_properties.item_name ) " +
    "UNION SELECT item_properties.menu_selection, item_properties.block_name || '.' || item_properties.item_name item, nvl(item_properties.prop_enabled, 'Y') prop_enabled, nvl(item_properties.prop_update, 'Y') prop_update, nvl(item_properties.prop_insert, 'Y') prop_insert, nvl(item_properties.prop_visible, 'Y') prop_visible, nvl(item_properties.prop_conceal, 'N') prop_conceal, item_properties.prop_prompt prop_prompt, nvl(item_properties.prop_required, 'N') prop_required " +
    " FROM adm.item_properties " +
    " WHERE item_properties.menu_selection = @SYSTEM_CURRENT_FORM || 'FMX' AND  NOT EXISTS(SELECT menu_selection " +
        " FROM adm.user_item_properties " +
        " WHERE user_item_properties.user_id = USER AND user_item_properties.menu_selection = item_properties.menu_selection AND user_item_properties.block_name = item_properties.block_name AND user_item_properties.item_name = item_properties.item_name ) AND  NOT EXISTS(SELECT menu_selection " +
        " FROM adm.role_item_properties " +
        " WHERE role_item_properties.role_id = @PARAMETER_QTSI_ROLE_ID AND role_item_properties.menu_selection = item_properties.menu_selection AND role_item_properties.block_name = item_properties.block_name AND role_item_properties.item_name = item_properties.item_name ) " +
    " ORDER BY 3";
            DataCursor itemPropertiesCur = new DataCursor(sqlitemPropertiesCur);
            // Item
            string doneWithRec = string.Empty;
            NNumber vCounter = 0;
            Task.Packages.Hold.requiredTable.Clear();
            //Setting query parameters
            itemPropertiesCur.AddParameter("@SYSTEM_CURRENT_FORM", TaskServices.CurrentForm);
            itemPropertiesCur.AddParameter("@PARAMETER_QTSI_ROLE_ID", this.Model.Params.QtsiRoleId);
            #region loop for cursor itemPropertiesCur
            try
            {
                itemPropertiesCur.Open();
                while (true)
                {
                    TableRow propRec = itemPropertiesCur.FetchRow();
                    if (itemPropertiesCur.NotFound) break;
                    doneWithRec = "N";
                    // message('item '||prop_rec.item); message(' ',no_acknowledge);
                    // VISIBLE
                    if (propRec.GetString("prop_visible") == "N" && doneWithRec == "N")
                    {
                        ItemServices.SetVisible(propRec.GetString("item"), false);
                        doneWithRec = "Y";
                    }
                    // ENABLED
                    if (propRec.GetString("prop_enabled") == "N" && doneWithRec == "N")
                    {
                        ItemServices.SetEnabled(propRec.GetString("item"), false);
                        doneWithRec = "Y";
                    }
                    //       message('conceal '||prop_rec.prop_conceal||' - '||done_with_rec); message(' ',no_acknowledge);
                    //  CONCEAL
                    if (propRec.GetString("prop_conceal") == "Y" && doneWithRec == "N")
                    {
                        ItemServices.SetUpdateAllowed(propRec.GetString("item"), false);
                        ItemServices.SetInsertAllowed(propRec.GetString("item"), false);
                        ItemServices.SetDisplayAsPassword(propRec.GetString("item"), true);
                        doneWithRec = "Y";
                    }
                    // UPDATE
                    if (propRec.GetString("prop_update") == "N" && doneWithRec == "N")
                    {
                        ItemServices.SetUpdateAllowed(propRec.GetString("item"), false);
                    }
                    // INSERT
                    if (propRec.GetString("prop_insert") == "N" && doneWithRec == "N")
                    {
                        ItemServices.SetInsertAllowed(propRec.GetString("item"), false);
                    }
                    //  PROMPT
                    if (!String.IsNullOrEmpty(propRec.GetString("prop_prompt")))
                    {
                        ItemServices.SetPromptText(propRec.GetString("item"), propRec.GetString("prop_prompt"));
                    }
                    //  REQUIRED
                    if (propRec.GetString("prop_required") == "Y")
                    {
                        ItemServices.SetStyleClass(propRec.GetString("item"), "REQUIRED");
                    }
                    //  REQUIRED
                    //  build an array to handle the required fields.
                    //  they will be set after the user tabs from the header block.
                    //  note: prop_rec.item includes block
                    if (propRec.GetString("prop_required") == "Y" && doneWithRec == "N")
                    {
                        // Set_Item_Property(prop_rec.item, REQUIRED, PROPERTY_TRUE);
                        vCounter = vCounter + 1;
                        Task.Packages.Hold.requiredTable[vCounter].FieldName = propRec.GetString("item");
                    }
                }
            }
            finally
            {
                itemPropertiesCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit MSG_DEBUG
        /*
		-- ----------------------------------------------------------------------------- 
--  Procedure: MSG_DEBUG 
--  Purpose:   Display message if alio system debug flag is 'ON' 
-- ----------------------------------------------------------------------------- 
PROCEDURE MSG_DEBUG (p_message varchar2) IS 
BEGIN 
   if :global.debug_status = 'ON' then 
     message(substr(p_message,1,200));  
     message(' ',no_acknowledge); 
   end if;  
END; 
 

		*/
        #endregion
        //ID:325
        /// <summary></summary>
        /// <param name="pMessage"></param>
        /// <remarks>
        ///  ----------------------------------------------------------------------------- 
        ///   Procedure: MSG_DEBUG 
        ///   Purpose:   Display message if alio system debug flag is 'ON' 
        ///  ----------------------------------------------------------------------------- 
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void MsgDebug(string pMessage)
        {
            if (Alio.Common.Globals.DebugStatus == "ON")
            {
                TaskServices.Message(Lib.Substring(pMessage, 1, 200));
                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
            }
        }


        #region Original PL/SQL code for Prog Unit MESSAGE_DISPLAY
        /*
		-- ----------------------------------------------------------------------------- 
--  Procedure: MESSAGE_DISPLAY 
--  Purpose:   Used to display message using forms MESSAGE built in. 
--             Makes sure message does not exceed maximum length allowed by builtin. 
--             Reduces program lines in main program logic. 
-- ----------------------------------------------------------------------------- 
PROCEDURE MESSAGE_DISPLAY (p_message in varchar2) IS 
BEGIN 
    message(substr(p_message,1,200)); 
    message(' ',no_acknowledge); 
END;
		*/
        #endregion
        //ID:326
        /// <summary></summary>
        /// <param name="pMessage"></param>
        /// <remarks>
        ///  ----------------------------------------------------------------------------- 
        ///   Procedure: MESSAGE_DISPLAY 
        ///   Purpose:   Used to display message using forms MESSAGE built in. 
        ///              Makes sure message does not exceed maximum length allowed by builtin. 
        ///              Reduces program lines in main program logic. 
        ///  ----------------------------------------------------------------------------- 
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void MessageDisplay(string pMessage)
        {
            TaskServices.Message(Lib.Substring(pMessage, 1, 200));
            TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
        }


        #region Original PL/SQL code for Prog Unit JOURNAL_BUDGET_CHECK
        /*
		<multilinecomment> Formatted on 2/26/2015 11:09:53 AM (QP5 v5.163.1008.3004) </multilinecomment> 
-- 
-- PROCEDURE: JOURNAL_BUDGET_CHECK -  alio-3074 09/08/14  klb -copied from fjent02a.fmb and change data block name to :recur_je_data. 
-- 
-- Called from: je_data pre-insert, pre-update 
-- 
-- If  JOURNAL_BUDGET_CHECK profile = 'YES': 
--   1 .Amount validated must have correct sign based on account type and debit or credit (uses fjent01a_debit_credit_amt) 
--   2. Uses alio IDI_LIB.OLB Validate_Budget to alert user if there is insufficient funds 
-- 
-- DEVELOPER NOTE:   If JOURNAL_BUDGET_CHECK is modified, it may need to be modified in fjent01a.fmb, fjent02a.fmb and fjent03a.fmb 
-- 
 
PROCEDURE journal_budget_check 
IS 
   CURSOR acct_type_cur (cur_in_account_id NUMBER) 
   IS 
      SELECT ac.account_type 
        FROM shr.accounts ac 
       WHERE ac.account_id = cur_in_account_id; 
 
   v_acct_type              shr.accounts.account_type%TYPE; 
   validate_balance         BOOLEAN := TRUE; 
   v_dc_amount              NUMBER DEFAULT 0; 
   v_dc_flag                VARCHAR2 (1) DEFAULT NULL; 
   v_hold_dc_amount         NUMBER DEFAULT 0;          --cec alio-3074 12/1/14 
   v_hold_dc_flag           VARCHAR2 (1) DEFAULT NULL; --cec alio-3074 12/1/14 
   v_validate_amount        NUMBER DEFAULT 0; 
   v_trace                  VARCHAR2 (10) DEFAULT NULL; 
   v_hold_validate_amount   NUMBER DEFAULT 0;          --cec alio-3074 12/1/14 
   v_amount                 fas.je_data.journal_amount%TYPE; --cec alio-3074 12/1/14 
BEGIN 
   v_trace := 'JBC-START'; 
 
   IF :recur_je_data.dr_amount <> 0 
   THEN 
      v_dc_amount := :recur_je_data.dr_amount; 
      v_dc_flag := 'D'; 
   ELSIF :recur_je_data.cr_amount <> 0 
   THEN 
      v_dc_amount := :recur_je_data.cr_amount; 
      v_dc_flag := 'C'; 
   END IF; 
 
   --cec alio-3074 12/1/14 
   IF :recur_je_data.hold_dr_amount <> 0 
   THEN 
      v_hold_dc_amount := :recur_je_data.hold_dr_amount; 
      v_hold_dc_flag := 'D'; 
   ELSIF :recur_je_data.hold_cr_amount <> 0 
   THEN 
      v_hold_dc_amount := :recur_je_data.hold_cr_amount; 
      v_hold_dc_flag := 'C'; 
   END IF; 
 
   msg_debug ( 
         'JBC trace: ' 
      || v_trace 
      || ', v_dc_amount: ' 
      || v_dc_amount 
      || ', v_dc_flag:' 
      || v_dc_flag 
      || ', acct no: ' 
      || :recur_je_data.account_no 
      || ', acct_id: ' 
      || :recur_je_data.account_id 
      || ', je amt: ' 
      || :recur_je_data.journal_amount 
      || ', hold acct_id: ' 
      || :recur_je_data.hold_account_id 
      || ', hold je amt: ' 
      || :recur_je_data.hold_journal_amount); 
 
   v_trace := 'JBC2'; 
 
   IF v_dc_amount <> 0 
   THEN 
      v_trace := 'JBC3'; 
 
      IF :parameter.journal_budget_check IS NULL 
      THEN 
         :parameter.journal_budget_check := 
            UPPER ( 
               NVL (TRIM (adm.get_profile_data ('JOURNAL_BUDGET_CHECK')), 
                    'N')); 
      END IF; 
 
 
      <multilinecomment>msg_debug ( 
            'JBC after profile journal_budget_check trace: ' 
         || v_trace 
         || ' profile journal budget: ' 
         || :parameter.journal_budget_check); 
         </multilinecomment> 
 
      v_trace := 'JBC4'; 
 
      IF UPPER (NVL (TRIM (:parameter.journal_budget_check), 'N')) = 'YES' 
      THEN 
         -- Determine amount to validate (reverse sign depending on account type and dc flag) 
         v_trace := 'JBC5'; 
         <multilinecomment>msg_debug ( 
               'JBC get validate amount trace: ' 
            || v_trace 
            || ' account_no: ' 
            || :recur_je_data.account_no 
            || ' account id:' 
            || :recur_je_data.account_id 
            || ' v_dc_amount: ' 
            || v_dc_amount);</multilinecomment> 
 
         v_validate_amount := 
            fjent01a_amt_validate_budget_ (v_dc_amount, -- 09/05/14 alio-3074 klb changed function name 
                                           v_dc_flag, 
                                           NULL, -- account type (looked up using account id passed in) 
                                           :recur_je_data.account_id); 
 
         msg_debug ( 
               'JBC after fjent01a_amt_validate_budget trace: ' 
            || v_trace 
            || ' v_validate_amount: ' 
            || v_validate_amount); 
 
         --cec alio-3074 12/1/14 
         v_trace := 'JBC5b'; 
 
         <multilinecomment>msg_debug ( 
               'JBC5b get validate amount trace: ' 
            || v_trace 
            || ', hold acct id:' 
            || :je_data.hold_account_id 
            || ', v_hold_dc_amount: ' 
            || v_hold_dc_amount);</multilinecomment> 
 
         IF (NVL (:recur_je_data.hold_account_id, 0) = 0) 
         THEN 
            v_hold_validate_amount := 0; 
         ELSE 
            v_hold_validate_amount := 
               fjent01a_amt_validate_budget_ (v_hold_dc_amount, 
                                              v_hold_dc_flag, 
                                              NULL, 
                                              :recur_je_data.hold_account_id); 
         END IF; 
 
 
         msg_debug ( 
               'JBC5b after fjent01a_amt_validate_budget trace: ' 
            || v_trace 
            || ', v_hold_validate_amount: ' 
            || v_hold_validate_amount); 
 
 
         -- Use alio IDI_LIB Validate_budget to determine if amount should be validated and user alerted of insufficient funds 
         v_trace := 'JBC6'; 
 
         <multilinecomment>--cec alio-3074 commented out and changed to using accounting_array logic 
         IF NVL (v_validate_amount, 0) != 0 
         THEN 
            v_trace := 'JBC7'; 
            -- Call IDI_LIB validate_budget program unit (subclassed) 
 
            validate_budget (:recur_je_data.account_id, 
                             v_validate_amount, 
                             :recur_je_data.account_no, 
                             v_program); 
 
            msg_debug ( 
                  'JBC after validate budget trace: ' 
               || v_trace 
               || ' v_validate_amount: ' 
               || v_validate_amount); 
         END IF; 
         </multilinecomment> 
         --------cec alio-3074 12/1/14 begin accounting_array logic:---------- 
         IF (:recur_je_data.account_id IS NOT NULL) 
         THEN 
            --if the account has changed, then reduce the old account amount 
            IF (:recur_je_data.account_id <> :recur_je_data.hold_account_id 
                AND :recur_je_data.hold_account_id IS NOT NULL) 
            THEN 
               msg_debug ( 
                  'jed.wvr - acct changed, calling update accounting tab to reduce old acct id'); 
 
               IF (accounting_array.acct_id_line_exists ( 
                      :recur_je_data.hold_account_id, 
                      :recur_je_data.line_no)) 
               THEN      --don't reduce the amount if it hasn't been added yet 
                  accounting_array.update_accounting_tab ( 
                     :recur_je_data.hold_account_id, 
                     NVL (v_hold_validate_amount, 0) * -1, 
                     NULL, 
                     :recur_je_data.line_no); 
               ELSE 
                  accounting_array.update_accounting_tab ( 
                     :recur_je_data.hold_account_id, 
                     NVL (v_hold_validate_amount, 0) * -1, 
                     NULL, 
                     :recur_je_data.line_no); 
                  accounting_array.acct_validated ( 
                     :recur_je_data.hold_account_id); --cec alio-3074 12/29/14 
               END IF; 
            END IF; 
 
            --if the new amount equals the old amount but the account has changed then the entire amount will need to be 
            --accounted for on the current account 
            IF ( --NVL (v_validate_amount, 0) = NVL (v_hold_validate_amount, 0) AND --cec alio-3074 12/31/14 commented out 
                :recur_je_data.account_id <> 
                   NVL (:recur_je_data.hold_account_id, -1)) 
            THEN 
               v_amount := NVL (v_validate_amount, 0); 
            --else if neither the amounts or the accounts have changed then no amount change needs 
            --to be accounted for on the current amount 
            ELSIF (NVL (v_validate_amount, 0) = 
                      NVL (v_hold_validate_amount, 0) 
                   AND :recur_je_data.account_id = 
                          NVL (:recur_je_data.hold_account_id, -1)) 
            THEN 
               v_amount := 0; 
            ELSE --otherwise, an amount change has occurred and needs to be accounted for on the new account 
               --v_amount := --cec alio-11358 2/26/15 - keep it simple and just reduce the old amount and then do the new amount 
               -- NVL (v_validate_amount, 0) 
               -- - NVL (v_hold_validate_amount, 0); 
               accounting_array.update_accounting_tab ( 
                  :recur_je_data.hold_account_id, 
                  NVL (v_hold_validate_amount, 0) * -1, 
                  NULL, 
                  :recur_je_data.line_no); 
 
               accounting_array.update_accounting_tab ( 
                  :recur_je_data.account_id, 
                  v_validate_amount, 
                  :recur_je_data.account_no, 
                  :recur_je_data.line_no); 
               v_amount := 0; 
            END IF; 
 
            --update the current accounting element 
            accounting_array.update_accounting_tab ( 
               :recur_je_data.account_id, 
               v_amount, 
               :recur_je_data.account_no, 
               :recur_je_data.line_no); 
 
            --cec 11/12/14 -these resets are needed prior to validating the budget so that if the validation 
            --fails, the next time through the wvr process, the correct values are used for validation 
            :recur_je_data.hold_account_id := :recur_je_data.account_id; 
            :recur_je_data.hold_journal_amount := 
               :recur_je_data.journal_amount; 
            :recur_je_data.hold_dr_amount := :recur_je_data.dr_amount; 
            :recur_je_data.hold_cr_amount := :recur_je_data.cr_amount; 
 
            --cec alio-3074 12/4/14 - the following checks to see if the balance should actually be checked 
            --for example, if an account is already in the hole and we are simply adding more funds to it (but not enough 
            --to make it positive) then we don't want the balance checked as it would fail 
            OPEN acct_type_cur (:recur_je_data.account_id); 
 
            FETCH acct_type_cur INTO v_acct_type; 
 
            IF acct_type_cur%NOTFOUND 
            THEN 
               v_acct_type := 0; 
            END IF; 
 
            CLOSE acct_type_cur; 
 
            IF (v_acct_type = 0 
                OR (v_dc_flag = 'D' 
                    AND (   v_acct_type BETWEEN '30' AND '38' 
                         OR v_acct_type BETWEEN '20' AND '28' --cec alio-3074 1/28/15 moved from Cr to Dr 
                         OR v_acct_type BETWEEN '90' AND '99') 
                    OR v_acct_type IN ('78', '79')) 
                OR (v_dc_flag = 'C' 
                    AND (   v_acct_type BETWEEN '10' AND '18' 
                         OR v_acct_type = '77' 
                         OR v_acct_type BETWEEN '80' AND '89'))) 
            THEN 
               null; --accounting_array.validate_budget (v_program); 
            ELSE                    --non balance checking action is occurring 
               accounting_array.acct_validated (:recur_je_data.account_id); --don't validate the account balance later 
            END IF; 
         ELSIF (:recur_je_data.account_id IS NULL 
                AND :recur_je_data.hold_account_id IS NOT NULL) 
         THEN --the user has manually removed the account so the old account needs to have its amount reduced 
            IF (accounting_array.acct_id_line_exists ( 
                   :recur_je_data.hold_account_id, 
                   :recur_je_data.line_no)) 
            THEN         --don't reduce the amount if it hasn't been added yet 
               accounting_array.update_accounting_tab ( 
                  :recur_je_data.hold_account_id, 
                  NVL (v_hold_validate_amount, 0) * -1, 
                  NULL, --don't care about the account no since the amount is being reduced, therefore passing in null 
                  :recur_je_data.line_no); 
            END IF; 
         END IF; 
 
         :recur_je_data.hold_account_id := :recur_je_data.account_id; 
         :recur_je_data.hold_journal_amount := :recur_je_data.journal_amount; 
 
         :recur_je_data.hold_dr_amount := :recur_je_data.dr_amount; 
         :recur_je_data.hold_cr_amount := :recur_je_data.cr_amount; 
         --------cec alio-3074 12/1/14 end accounting_array logic---------- 
 
 
         v_trace := 'JBC8'; 
      END IF; -- if upper(nvl(trim(:parameter.journal_budget_check,'N') = 'YES' 
   END IF; 
 
   v_trace := 'JBC-END'; 
EXCEPTION 
   -- alert button code may raise form trigger failure that does not need another error message displayed 
   WHEN form_trigger_failure 
   THEN 
      RAISE; 
   WHEN OTHERS 
   THEN 
      message_display ( 
            'Error in JOURNAL_BUDGET_CHECK at trace: ' 
         || v_trace 
         || ' for account: ' 
         || :recur_je_data.account_no 
         || ' Error: ' 
         || SQLERRM); 
 
      RAISE; 
END;
		*/
        #endregion
        //ID:327
        /// <summary></summary>
        /// <remarks>
        ///  Formatted on 2/26/2015 11:09:53 AM (QP5 v5.163.1008.3004) 
        ///  
        ///  PROCEDURE: JOURNAL_BUDGET_CHECK -  alio-3074 09/08/14  klb -copied from fjent02a.fmb and change data block name to :recur_je_data. 
        ///  
        ///  Called from: je_data pre-insert, pre-update 
        ///  
        ///  If  JOURNAL_BUDGET_CHECK profile = 'YES': 
        ///    1 .Amount validated must have correct sign based on account type and debit or credit (uses fjent01a_debit_credit_amt) 
        ///    2. Uses alio IDI_LIB.OLB Validate_Budget to alert user if there is insufficient funds 
        ///  
        ///  DEVELOPER NOTE:   If JOURNAL_BUDGET_CHECK is modified, it may need to be modified in fjent01a.fmb, fjent02a.fmb and fjent03a.fmb 
        ///  
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void JournalBudgetCheck(RecurJeDataAdapter recurJeDataElement)
        {
            int rowCount = 0;
            String sqlacctTypeCur = "SELECT ac.account_type " +
    " FROM shr.accounts ac " +
    " WHERE ac.account_id = @P_CUR_IN_ACCOUNT_ID ";
            DataCursor acctTypeCur = new DataCursor(sqlacctTypeCur);
            NNumber acctTypeCurCurInAccountId = NNumber.Null;
            NNumber vAcctType = NNumber.Null;
            NBool validateBalance = true;
            NNumber vDcAmount = 0;
            string vDcFlag = null;
            NNumber vHoldDcAmount = 0;
            // cec alio-3074 12/1/14 
            string vHoldDcFlag = null;
            // cec alio-3074 12/1/14 
            NNumber vValidateAmount = 0;
            string vTrace = null;
            NNumber vHoldValidateAmount = 0;
            // cec alio-3074 12/1/14 
            NNumber vAmount = NNumber.Null;
            #region
            try
            {
                try
                {
                    vTrace = "JBC-START";
                    if (recurJeDataElement.DrAmount != 0)
                    {
                        vDcAmount = recurJeDataElement.DrAmount;
                        vDcFlag = "D";
                    }
                    else if (recurJeDataElement.CrAmount != 0)
                    {
                        vDcAmount = recurJeDataElement.CrAmount;
                        vDcFlag = "C";
                    }
                    // cec alio-3074 12/1/14 
                    if (recurJeDataElement.HoldDrAmount != 0)
                    {
                        vHoldDcAmount = recurJeDataElement.HoldDrAmount;
                        vHoldDcFlag = "D";
                    }
                    else if (recurJeDataElement.HoldCrAmount != 0)
                    {
                        vHoldDcAmount = recurJeDataElement.HoldCrAmount;
                        vHoldDcFlag = "C";
                    }
                    MsgDebug("JBC trace: " + vTrace + ", v_dc_amount: " + vDcAmount.ToString() + ", v_dc_flag:" + vDcFlag + ", acct no: " + recurJeDataElement.AccountNo + ", acct_id: " + recurJeDataElement.AccountId.ToString() + ", je amt: " + recurJeDataElement.JournalAmount.ToString() + ", hold acct_id: " + recurJeDataElement.HoldAccountId.ToString() + ", hold je amt: " + recurJeDataElement.HoldJournalAmount.ToString());
                    vTrace = "JBC2";
                    if (vDcAmount != 0)
                    {
                        vTrace = "JBC3";
                        if (string.IsNullOrEmpty(this.Model.Params.JournalBudgetCheck))
                        {
                            Model.Params.JournalBudgetCheck = Lib.IsNull(Lib.Trim(StoredProcedures.FGetProfileData("JOURNAL_BUDGET_CHECK")), "N");

                        }
                        // msg_debug (
                        // 'JBC after profile journal_budget_check trace: '
                        // || v_trace
                        // || ' profile journal budget: '
                        // || :parameter.journal_budget_check);
                        vTrace = "JBC4";
                        if (Lib.Upper(Lib.IsNull(Lib.Trim(this.Model.Params.JournalBudgetCheck), "N")) == "YES")
                        {
                            //  Determine amount to validate (reverse sign depending on account type and dc flag) 
                            vTrace = "JBC5";
                            // msg_debug (
                            // 'JBC get validate amount trace: '
                            // || v_trace
                            // || ' account_no: '
                            // || :recur_je_data.account_no
                            // || ' account id:'
                            // || :recur_je_data.account_id
                            // || ' v_dc_amount: '
                            // || v_dc_amount);


                            vValidateAmount = StoredProcedures.Fjent01aAmtValidateBudget_(vDcAmount, vDcFlag, null, recurJeDataElement.AccountId);

                            MsgDebug("JBC after fjent01a_amt_validate_budget trace: " + vTrace + " v_validate_amount: " + vValidateAmount.ToString());
                            // cec alio-3074 12/1/14 
                            vTrace = "JBC5b";
                            // msg_debug (
                            // 'JBC5b get validate amount trace: '
                            // || v_trace
                            // || ', hold acct id:'
                            // || :je_data.hold_account_id
                            // || ', v_hold_dc_amount: '
                            // || v_hold_dc_amount);
                            if ((Lib.IsNull(recurJeDataElement.HoldAccountId, 0) == 0))
                            {
                                vHoldValidateAmount = 0;
                            }
                            else
                            {
                                vHoldValidateAmount = StoredProcedures.Fjent01aAmtValidateBudget_(vHoldDcAmount, vHoldDcFlag, null, recurJeDataElement.HoldAccountId);
                            }
                            MsgDebug("JBC5b after fjent01a_amt_validate_budget trace: " + vTrace + ", v_hold_validate_amount: " + vHoldValidateAmount.ToString());
                            //  Use alio IDI_LIB Validate_budget to determine if amount should be validated and user alerted of insufficient funds 
                            vTrace = "JBC6";
                            // --cec alio-3074 commented out and changed to using accounting_array logic
                            // IF NVL (v_validate_amount, 0) != 0
                            // THEN
                            // v_trace := 'JBC7';
                            // -- Call IDI_LIB validate_budget program unit (subclassed)
                            // validate_budget (:recur_je_data.account_id,
                            // v_validate_amount,
                            // :recur_je_data.account_no,
                            // v_program);
                            // msg_debug (
                            // 'JBC after validate budget trace: '
                            // || v_trace
                            // || ' v_validate_amount: '
                            // || v_validate_amount);
                            // END IF;
                            // ------cec alio-3074 12/1/14 begin accounting_array logic:---------- 
                            if ((!recurJeDataElement.AccountId.IsNull))
                            {
                                // if the account has changed, then reduce the old account amount 
                                if ((recurJeDataElement.AccountId != recurJeDataElement.HoldAccountId && !recurJeDataElement.HoldAccountId.IsNull))
                                {
                                    MsgDebug("jed.wvr - acct changed, calling update accounting tab to reduce old acct id");
                                    if ((Task.Packages.AccountingArray.FAcctIdLineExists(recurJeDataElement.HoldAccountId, recurJeDataElement.LineNo)))
                                    {
                                        // don't reduce the amount if it hasn't been added yet 
                                        Task.Packages.AccountingArray.UpdateAccountingTab(recurJeDataElement.HoldAccountId, Lib.IsNull(vHoldValidateAmount, 0) * -(1), null, recurJeDataElement.LineNo);
                                    }
                                    else
                                    {
                                        Task.Packages.AccountingArray.UpdateAccountingTab(recurJeDataElement.HoldAccountId, Lib.IsNull(vHoldValidateAmount, 0) * -(1), null, recurJeDataElement.LineNo);
                                        Task.Packages.AccountingArray.AcctValidated(recurJeDataElement.HoldAccountId);
                                    }
                                }
                                // if the new amount equals the old amount but the account has changed then the entire amount will need to be 
                                // accounted for on the current account 
                                if ((recurJeDataElement.AccountId != Lib.IsNull(recurJeDataElement.HoldAccountId, -(1))))
                                {
                                    vAmount = Lib.IsNull(vValidateAmount, 0);
                                }
                                else if ((Lib.IsNull(vValidateAmount, 0) == Lib.IsNull(vHoldValidateAmount, 0) && recurJeDataElement.AccountId == Lib.IsNull(recurJeDataElement.HoldAccountId, -(1))))
                                {
                                    vAmount = 0;
                                }
                                else
                                {
                                    // otherwise, an amount change has occurred and needs to be accounted for on the new account 
                                    // v_amount := --cec alio-11358 2/26/15 - keep it simple and just reduce the old amount and then do the new amount 
                                    //  NVL (v_validate_amount, 0) 
                                    //  - NVL (v_hold_validate_amount, 0); 
                                    Task.Packages.AccountingArray.UpdateAccountingTab(recurJeDataElement.HoldAccountId, Lib.IsNull(vHoldValidateAmount, 0) * -(1), null, recurJeDataElement.LineNo);
                                    Task.Packages.AccountingArray.UpdateAccountingTab(recurJeDataElement.AccountId, vValidateAmount, recurJeDataElement.AccountNo, recurJeDataElement.LineNo);
                                    vAmount = 0;
                                }
                                // update the current accounting element 
                                Task.Packages.AccountingArray.UpdateAccountingTab(recurJeDataElement.AccountId, vAmount, recurJeDataElement.AccountNo, recurJeDataElement.LineNo);
                                // cec 11/12/14 -these resets are needed prior to validating the budget so that if the validation 
                                // fails, the next time through the wvr process, the correct values are used for validation 
                                recurJeDataElement.HoldAccountId = recurJeDataElement.AccountId;
                                recurJeDataElement.HoldJournalAmount = recurJeDataElement.JournalAmount;
                                recurJeDataElement.HoldDrAmount = recurJeDataElement.DrAmount;
                                recurJeDataElement.HoldCrAmount = recurJeDataElement.CrAmount;
                                // cec alio-3074 12/4/14 - the following checks to see if the balance should actually be checked 
                                // for example, if an account is already in the hole and we are simply adding more funds to it (but not enough 
                                // to make it positive) then we don't want the balance checked as it would fail 
                                acctTypeCurCurInAccountId = recurJeDataElement.AccountId;
                                //Setting query parameters
                                acctTypeCur.AddParameter("@P_CUR_IN_ACCOUNT_ID", acctTypeCurCurInAccountId);
                                acctTypeCur.Open();
                                ResultSet acctTypeCurResults = acctTypeCur.FetchInto();
                                if (acctTypeCurResults != null)
                                {
                                    vAcctType = acctTypeCurResults.GetString(0);
                                }
                                if (acctTypeCur.NotFound)
                                {
                                    vAcctType = 0;
                                }
                                if ((vAcctType == 0 || (vDcFlag == "D" && (vAcctType >= "30" && vAcctType <= "38" || vAcctType >= "20" && vAcctType <= "28" || vAcctType >= "90" && vAcctType <= "99") || (vAcctType == "78" || vAcctType == "79")) || (vDcFlag == "C" && (vAcctType >= "10" && vAcctType <= "18" || vAcctType == "77" || vAcctType >= "80" && vAcctType <= "89"))))
                                {
                                }
                                else
                                {
                                    // non balance checking action is occurring 
                                    Task.Packages.AccountingArray.AcctValidated(recurJeDataElement.AccountId);
                                }
                            }
                            else if ((recurJeDataElement.AccountId.IsNull && !recurJeDataElement.HoldAccountId.IsNull))
                            {
                                // the user has manually removed the account so the old account needs to have its amount reduced 
                                if ((Task.Packages.AccountingArray.FAcctIdLineExists(recurJeDataElement.HoldAccountId, recurJeDataElement.LineNo)))
                                {
                                    // don't reduce the amount if it hasn't been added yet 
                                    Task.Packages.AccountingArray.UpdateAccountingTab(recurJeDataElement.HoldAccountId, Lib.IsNull(vHoldValidateAmount, 0) * -(1), null, recurJeDataElement.LineNo);
                                }
                            }
                            recurJeDataElement.HoldAccountId = recurJeDataElement.AccountId;
                            recurJeDataElement.HoldJournalAmount = recurJeDataElement.JournalAmount;
                            recurJeDataElement.HoldDrAmount = recurJeDataElement.DrAmount;
                            recurJeDataElement.HoldCrAmount = recurJeDataElement.CrAmount;
                            // ------cec alio-3074 12/1/14 end accounting_array logic---------- 
                            vTrace = "JBC8";
                        }
                    }
                    vTrace = "JBC-END";
                }
                catch (ApplicationException)
                {
                    throw;
                }
                catch (TaskControlException) { throw; }
                catch
                {
                    MessageDisplay("Error in JOURNAL_BUDGET_CHECK at trace: " + vTrace + " for account: " + recurJeDataElement.AccountNo + " Error: " + DbManager.ErrorMessage);
                    throw;
                }
            }
            finally
            {
                acctTypeCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit CHECK_PACKAGE_FAILURE
        /*
		Procedure Check_Package_Failure IS 
BEGIN 
  IF NOT ( Form_Success ) THEN 
    RAISE Form_Trigger_Failure; 
  END IF; 
END;
		*/
        #endregion
        //ID:328
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void CheckPackageFailure()
        {
            if (!((TaskServices.ServiceSuccess)))
            {
                throw new ApplicationException();
            }
        }



        #region Original PL/SQL code for Prog Unit GET_BATCH_DESC
        /*
		PROCEDURE get_batch_desc IS 
 
   cursor batch_desc_cur is 
   select batch_description 
     from fas.batches 
    where batch_no   = :batch_no_block.batch_no  
      and batch_year = :batch_no_block.batch_year; 
 
BEGIN 
 
      open  batch_desc_cur; 
      fetch batch_desc_cur into :recur_je_data.journal_description ; 
      close batch_desc_cur;       
   
END;
		*/
        #endregion
        //ID:332
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void GetBatchDesc(RecurJeDataAdapter recurJeDataElement)
        {
            int rowCount = 0;
            String sqlbatchDescCur = "SELECT batch_description " +
    " FROM fas.batches " +
    " WHERE batch_no = @BATCH_NO_BLOCK_BATCH_NO AND batch_year = @BATCH_NO_BLOCK_BATCH_YEAR ";
            DataCursor batchDescCur = new DataCursor(sqlbatchDescCur);
            #region
            try
            {
                //Setting query parameters
                batchDescCur.AddParameter("@BATCH_NO_BLOCK_BATCH_NO", Model.BatchNoBlock.BatchNo);
                batchDescCur.AddParameter("@BATCH_NO_BLOCK_BATCH_YEAR", Model.BatchNoBlock.BatchYear);
                batchDescCur.Open();
                ResultSet batchDescCurResults = batchDescCur.FetchInto();
                if (batchDescCurResults != null)
                {
                    recurJeDataElement.JournalDescription = batchDescCurResults.GetString(0);
                }
            }
            finally
            {
                batchDescCur.Close();
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
        //ID:333
        /// <summary></summary>
        /// <remarks>
        /// F2N_STRONG_PRESENTATION_LOGIC : The code of this procedure was identified as containing presentation logic. See documentation for details.
        /// </remarks>
        public virtual void DateChoosen()
        {
            //TaskServices.Copy(Lib.ToChar(Task.Libraries.Calendar.DateLov.currentLovDate, "dd-mon-yyyy"),Task.Libraries.Calendar.DateLov.dateLovReturnItem);
            //ItemServices.GoItem(Task.Libraries.Calendar.DateLov.dateLovReturnItem);
            //if ( Task.Libraries.Calendar.DateLov.lovAutoSkip == true )
            //{
            //	ItemServices.NextItem();
            //}
        }


        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_NO
        /*
		FUNCTION get_account_no (account_id_in number) RETURN varchar2 IS 
 
 cursor get_account_no(cur_in_acct_id number) is 
   select account_no, inactive_flag 
     from shr.accounts 
    where account_id = cur_in_acct_id; 
      
 hold_account_no SHR.ACCOUNTS.ACCOUNT_NO%TYPE;-- NJA ALIO 307 ADDED THE %TYPE 
 hold_inactive_flag varchar2(1); 
      
begin 
 
 open get_account_no(account_id_in); 
 fetch get_account_no into hold_account_no, hold_inactive_flag; 
 close get_account_no; 
 
 if upper(hold_inactive_flag) = 'Y' then 
  message('Account: '||hold_account_no||' is Inactive. Please only use active accounts.'); 
  message(' ',no_acknowledge); 
  raise form_trigger_failure; 
 else 
 return hold_account_no; 
 end if; 
end; 

		*/
        #endregion
        //ID:335
        /// <summary></summary>
        /// <param name="getAccountNo"></param>
        /// <remarks>
        /// </remarks>
        public virtual string FGetAccountNo(NNumber accountIdIn)
        {
            int rowCount = 0;
            String sqlgetAccountNo = "SELECT account_no, inactive_flag " +
    " FROM shr.accounts " +
    " WHERE account_id = @P_CUR_IN_ACCT_ID ";
            DataCursor getAccountNo = new DataCursor(sqlgetAccountNo);
            NNumber getAccountNoCurInAcctId = NNumber.Null;
            NString holdAccountNo = NString.Null;
            //  NJA ALIO 307 ADDED THE %TYPE 
            string holdInactiveFlag = string.Empty;
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
                    holdAccountNo = getAccountNoResults.GetStr(0);
                    holdInactiveFlag = getAccountNoResults.GetString(1);
                }
                if (Lib.Upper(holdInactiveFlag) == "Y")
                {
                    TaskServices.Message("Account: " + holdAccountNo.ToString().ToString() + " is Inactive. Please only use active accounts.");
                    TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
                    throw new ApplicationException();
                }
                else
                {
                    return holdAccountNo;
                }
            }
            finally
            {
                getAccountNo.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit RESEQUENCE
        /*
		PROCEDURE RESEQUENCE IS 
 
  line_num  number; 
 
BEGIN 
 
   go_block('recur_je_data'); 
   first_record; 
   line_num := 1; 
   loop 
     :recur_je_data.line_no := line_num; 
     exit when :system.last_record = 'TRUE'; 
     next_record; 
     line_num := (line_num + 1); 
   end loop; 
   first_record; 
   
END;
		*/
        #endregion
        //ID:336
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void Resequence(RecurJeDataAdapter recurJeDataElement)
        {
            NNumber lineNum = NNumber.Null;
            BlockServices.GoBlock("recur_je_data");
            BlockServices.FirstRecord();
            lineNum = 1;
            while (true)
            {
                recurJeDataElement.LineNo = lineNum;
                if (BlockServices.InLastRecord)
                    break;
                BlockServices.NextRecord();
                lineNum = ((lineNum + 1));
            }
            BlockServices.FirstRecord();
        }

        #region Original PL/SQL code for Prog Unit SET_PRIMARY_KEY_STATUS
        /*
		<multilinecomment> 
    FUNCTION: set_primary_key_status 
    Reference Number: 043957 - Prevent duplicate value error when resequencing recur_je_data 
    Date: 11/30/07 
    Author: K. Boudreau 
    Problem:  
             Sometimes records in recur_je_data table cannot be resequenced (using fjent03a.fmx) because a new record is trying 
             to use the same value in line_no as an existing record in the database, but the existing record in the database 
             has not yet been updated with a new value.  The error occurs because the column line_no is in the primary key for 
              the tableand it will not allow duplicate values to be assigned to line_no, even temporarily.  Example: 
                        3 records exist with line_no values = 1, 2 and 3 
                        If we try to change the line_no for record 2 to 3 and 3 to 2 the existing PRIMARY KEY prevents 
                        this from occuring and error exception dup_value_in_index is raised. 
 
    Solution: Disable the status of the primary key to allow records to be resequenced.  Enable 
              the primary key before exiting the form. 
    Function is called from WHEN_NEW_FORM_INSTANCE to disable the primary key and POST_FORM to enable it. 
</multilinecomment>              
 
FUNCTION set_primary_key_status 
          (p_status VARCHAR2) 
RETURN 
         BOOLEAN 
IS 
 
         v_sql          VARCHAR2(500) DEFAULT NULL; 
         v_pk_name      VARCHAR2(30) DEFAULT NULL; 
         v_con_status   VARCHAR2(8) DEFAULT NULL; 
BEGIN 
    SELECT  ac.constraint_name 
           ,ac.status 
      INTO  v_pk_name 
           ,v_con_status 
      FROM  ALL_CONS_COLUMNS ACC 
           ,ALL_CONSTRAINTS AC 
    WHERE 
             AC.TABLE_NAME = 'RECUR_JE_DATA' 
         AND AC.CONSTRAINT_TYPE = 'P' 
         AND ACC.COLUMN_NAME = 'LINE_NO' 
         AND AC.CONSTRAINT_NAME = ACC.CONSTRAINT_NAME; 
 
    -- Set status of primary key constraint only if constraint was found 
    -- FORMS_DDL will do implicit commit, so make sure there are no pending changes 
    --   before the constraint status is changed to avoid accidently committing user 
    --   changes without them realizing it. 
    IF     v_pk_name IS NOT NULL             -- constraint must exist 
      AND  v_con_status != p_status          -- constraint status is not what form needs 
     AND  :SYSTEM.FORM_STATUS != 'CHANGED'  -- No outstanding changes to commit 
     THEN 
            v_sql := 'ALTER TABLE fas.recur_je_data '||p_status||' CONSTRAINT '||v_pk_name; 
            FORMS_DDL(v_sql); 
            IF FORM_SUCCESS THEN 
               RETURN TRUE; 
            ELSE 
               RETURN FALSE; 
            END IF;   
            
    ELSE 
      RETURN TRUE; 
    END IF; 
    
       
EXCEPTION 
   WHEN OTHERS THEN 
      RETURN FALSE;     
 
END; 
 

		*/
        #endregion
        //ID:338
        /// <summary></summary>
        /// <param name="pStatus"></param>
        /// <remarks>
        /// FUNCTION: set_primary_key_status
        /// Reference Number: 043957 - Prevent duplicate value error when resequencing recur_je_data
        /// Date: 11/30/07
        /// Author: K. Boudreau
        /// Problem:
        /// Sometimes records in recur_je_data table cannot be resequenced (using fjent03a.fmx) because a new record is trying
        /// to use the same value in line_no as an existing record in the database, but the existing record in the database
        /// has not yet been updated with a new value.  The error occurs because the column line_no is in the primary key for
        /// the tableand it will not allow duplicate values to be assigned to line_no, even temporarily.  Example:
        /// 3 records exist with line_no values = 1, 2 and 3
        /// If we try to change the line_no for record 2 to 3 and 3 to 2 the existing PRIMARY KEY prevents
        /// this from occuring and error exception dup_value_in_index is raised.
        /// Solution: Disable the status of the primary key to allow records to be resequenced.  Enable
        /// the primary key before exiting the form.
        /// Function is called from WHEN_NEW_FORM_INSTANCE to disable the primary key and POST_FORM to enable it.
        /// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual NBool FSetPrimaryKeyStatus(string pStatus)
        {
            int rowCount = 0;
            string vSql = null;
            string vPkName = null;
            string vConStatus = null;
            try
            {
                #region Execute data command
                String sql1 = "SELECT ac.constraint_name, ac.status " +
    " FROM ALL_CONS_COLUMNS ACC, ALL_CONSTRAINTS AC " +
    " WHERE AC.TABLE_NAME = 'RECUR_JE_DATA' AND AC.CONSTRAINT_TYPE = 'P' AND ACC.COLUMN_NAME = 'LINE_NO' AND AC.CONSTRAINT_NAME = ACC.CONSTRAINT_NAME ";
                DataCommand command1 = new DataCommand(sql1);
                ResultSet results1 = command1.ExecuteQuery();
                rowCount = command1.RowCount;
                if (results1.HasData)
                {
                    vPkName = results1.GetString(0);
                    vConStatus = results1.GetString(1);
                }
                results1.Close();
                #endregion
                //  Set status of primary key constraint only if constraint was found 
                //  FORMS_DDL will do implicit commit, so make sure there are no pending changes 
                //    before the constraint status is changed to avoid accidently committing user 
                //    changes without them realizing it. 
                if (!string.IsNullOrEmpty(vPkName) && vConStatus != pStatus && TaskServices.FormStatus != "CHANGED")
                {
                    vSql = "ALTER TABLE fas.recur_je_data " + pStatus + " CONSTRAINT " + vPkName;
                    Lib.FormsDDL(vSql);
                    if (TaskServices.ServiceSuccess)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (TaskControlException) { throw; }
            catch
            {
                return false;
            }
        }


        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_BALANCE_OLD
        /*
		FUNCTION get_account_balance_old RETURN number IS 
 cursor current_amounts_cur (in_account_id varchar2, in_accounting_period varchar2) is 
    select ca.account_id, 
           sum(ca.adjustment_cur) adjustment_cur, 
           sum(ca.amount_cur) amount_cur, 
           sum(ca.budget_cur) budget_cur, 
           sum(ca.encumbrance_cur) encumbrance_cur, 
           max(ac_t.encumbrance_multiplier) encumbrance_multiplier, 
           max(ac_t.amount_multiplier) amount_multiplier, 
           max(ac_t.budget_multiplier) budget_multiplier, 
           max(ac.pre_payable) pre_payable 
      from fas.current_amounts ca, 
               shr.accounts ac, 
               shr.account_types ac_t 
     where ca.account_id = in_account_id 
       and ca.account_period <= in_accounting_period 
       and ac.account_id = ca.account_id 
       and ac_t.account_type = ac.account_type 
  group by ca.account_id; 
  current_amounts_rec current_amounts_cur%rowtype; 
 
BEGIN 
 
  open current_amounts_cur(:recur_je_data.account_id,:batch_no_block.accounting_period); 
  fetch current_amounts_cur into current_amounts_rec; 
  if current_amounts_cur%found then 
  return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) - 
          (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) - 
          (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0))- 
          (nvl(current_amounts_rec.pre_payable,0) * nvl(current_amounts_rec.amount_multiplier,0));   
  else 
  return 0; 
  end if; 
  close current_amounts_cur;  
  return 0; 
END;
		*/
        #endregion
        //ID:344
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual NNumber FGetAccountBalanceOld(RecurJeDataAdapter recurJeDataElement)
        {
            int rowCount = 0;
            String sqlcurrentAmountsCur = "SELECT ca.account_id, sum(ca.adjustment_cur) adjustment_cur, sum(ca.amount_cur) amount_cur, sum(ca.budget_cur) budget_cur, sum(ca.encumbrance_cur) encumbrance_cur, max(ac_t.encumbrance_multiplier) encumbrance_multiplier, max(ac_t.amount_multiplier) amount_multiplier, max(ac_t.budget_multiplier) budget_multiplier, max(ac.pre_payable) pre_payable " +
    " FROM fas.current_amounts ca, shr.accounts ac, shr.account_types ac_t " +
    " WHERE ca.account_id = @P_IN_ACCOUNT_ID AND ca.account_period <= @P_IN_ACCOUNTING_PERIOD AND ac.account_id = ca.account_id AND ac_t.account_type = ac.account_type " +
    " GROUP BY ca.account_id ";
            DataCursor currentAmountsCur = new DataCursor(sqlcurrentAmountsCur);
            String currentAmountsCurInAccountId = string.Empty;
            String currentAmountsCurInAccountingPeriod = string.Empty;
            TableRow currentAmountsRec = null;
            #region
            try
            {
                currentAmountsCurInAccountId = recurJeDataElement.AccountId;
                currentAmountsCurInAccountingPeriod = Model.BatchNoBlock.AccountingPeriod;
                //Setting query parameters
                currentAmountsCur.AddParameter("@P_IN_ACCOUNT_ID", currentAmountsCurInAccountId);
                currentAmountsCur.AddParameter("@P_IN_ACCOUNTING_PERIOD", currentAmountsCurInAccountingPeriod);
                currentAmountsCur.Open();
                currentAmountsRec = currentAmountsCur.FetchRow();
                if (currentAmountsCur.Found)
                {
                    return ((Lib.IsNull(currentAmountsRec.GetString("budget_cur"), 0) * Lib.IsNull(currentAmountsRec.GetString("budget_multiplier"), 0)) + (Lib.IsNull(currentAmountsRec.GetString("adjustment_cur"), 0) * Lib.IsNull(currentAmountsRec.GetString("budget_multiplier"), 0))) - (Lib.IsNull(currentAmountsRec.GetString("encumbrance_cur"), 0) * Lib.IsNull(currentAmountsRec.GetString("encumbrance_multiplier"), 0)) - (Lib.IsNull(currentAmountsRec.GetString("amount_cur"), 0) * Lib.IsNull(currentAmountsRec.GetString("amount_multiplier"), 0)) - (Lib.IsNull(currentAmountsRec.GetString("pre_payable"), 0) * Lib.IsNull(currentAmountsRec.GetString("amount_multiplier"), 0));
                }
                else
                {
                    return 0;
                }
                return 0;
            }
            finally
            {
                currentAmountsCur.Close();
            }
            #endregion
        }


        #region Original PL/SQL code for Prog Unit BUILD_APPROVAL_CHAIN
        /*
		PROCEDURE build_approval_chain IS
	 v_count number := 0;
begin
	
  v_count := nvl(hold.reference_table.count,0);
 
	if :parameter.journal_approval = 'Y' then
   for i in 1..v_count
   loop
	   fjapp01a_.setup_order_approvals(hold.reference_table(i).reference_no,
                                     user,
		                                 'Y'); -- Yes AJE approvals are turned on
	 
	   fjapp01a_.approve_next_level(hold.reference_table(i).reference_no,
                                  user,
		                              'Y');
	 end loop;
 end if;

end;
		*/
        #endregion
        //ID:345
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void BuildApprovalChain()
        {
            NNumber vCount = 0;
            vCount = Lib.IsNull(Task.Packages.Hold.referenceTable.Count, 0);
            if (this.Model.Params.JournalApproval == "Y")
            {
                for (NInteger i = 1; i <= vCount; i++)
                {
                    Fjapp01a_.SetupOrderApprovals(Task.Packages.Hold.referenceTable[i].ReferenceNo, DbManager.User, "Y");
                    Fjapp01a_.ApproveNextLevel(Task.Packages.Hold.referenceTable[i].ReferenceNo, DbManager.User, "Y");
                }
            }
        }


        #region Original PL/SQL code for Prog Unit BUILD_APPROVAL_ARRAY
        /*
		PROCEDURE build_approval_array(reference_no_in varchar2) IS

v_count number      := 0;
v_match varchar2(1) := 'N';

BEGIN

  if :parameter.journal_approval <> 'Y' then
    return;
  end if;
  
  v_count := nvl(hold.reference_table.count,0);
   
  for i in 1..v_count
  loop

  	if  hold.reference_table(i).reference_no = reference_no_in then
  		v_match := 'Y';
  		exit;
  	end if;
  	
  end loop;
   
 
   if  v_match = 'N' then
   	v_count := v_count +1;
   	 hold.reference_table(v_count).reference_no := reference_no_in;
 	 end if;
  
END;
		*/
        #endregion
        //ID:346
        /// <summary></summary>
        /// <param name="referenceNoIn"></param>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this procedure was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual void BuildApprovalArray(string referenceNoIn)
        {
            NNumber vCount = 0;
            string vMatch = "N";
            if (this.Model.Params.JournalApproval != "Y")
            {
                return;
            }
            vCount = Lib.IsNull(Task.Packages.Hold.referenceTable.Count, 0);
            for (NInteger i = 1; i <= vCount; i++)
            {
                if (Task.Packages.Hold.referenceTable[i].ReferenceNo == referenceNoIn)
                {
                    vMatch = "Y";
                    break;
                }
            }
            if (vMatch == "N")
            {
                vCount = vCount + 1;
                Task.Packages.Hold.referenceTable[vCount].ReferenceNo = referenceNoIn;
            }
        }


        #region Original PL/SQL code for Prog Unit GET_ACCOUNT_BALANCE
        /*
		FUNCTION get_account_balance RETURN number IS 
 cursor current_amounts_cur (in_account_id varchar2, in_accounting_period varchar2) is 
    select ca.account_id, 
           sum(ca.adjustment_cur) adjustment_cur, 
           sum(ca.amount_cur) amount_cur, 
           sum(ca.budget_cur) budget_cur, 
           sum(ca.encumbrance_cur) encumbrance_cur, 
           max(ac_t.encumbrance_multiplier) encumbrance_multiplier, 
           max(ac_t.amount_multiplier) amount_multiplier, 
           max(ac_t.budget_multiplier) budget_multiplier, 
           max(ac.pre_payable)   pre_payable, 
           max( ac.pre_budget)   pre_budget, 
           max(ac.pre_warehouse) pre_warehouse, 
           max(ac.pre_encumbrance)pre_encumbrance, 
           max(ac.account_type)   account_type 
      from fas.current_amounts ca, 
               shr.accounts ac, 
               shr.account_types ac_t 
     where ca.account_id = in_account_id 
       and ca.account_period <= in_accounting_period 
       and ac.account_id = ca.account_id 
       and ac_t.account_type = ac.account_type 
  group by ca.account_id; 
  current_amounts_rec current_amounts_cur%rowtype; 
 
  v_pre_amount    number := 0; 
  v_return_amount number := 0;
 
 
 
BEGIN 

 -- dph alio-13043 create common code from fainq05a.fmx and use it; 
 v_return_amount := 
 fabal01a_avail_balance_ (:recur_je_data.account_id,
                          'Y', --include_encubrance
                          :batch_no_block.accounting_period);
                          
 return v_return_amount;
 
 <multilinecomment> 13043 comment out below 
 
  open current_amounts_cur(:recur_je_data.account_id,:batch_no_block.accounting_period); 
  fetch current_amounts_cur into current_amounts_rec; 
 
  ------ begin dph 13037 code ------- 
 -- determine pre amount based on code in fainq01a.fmx 
 -- and add it to the current amount 
  if current_amounts_cur%found then 
 
   if current_amounts_rec.account_type between '77' and '99' then 
     if current_amounts_rec.account_type in( '77' , '78') then 
      
       v_pre_amount := nvl(current_amounts_rec.pre_budget,0) 
                     - nvl(current_amounts_rec.pre_warehouse,0) 
                     - nvl(current_amounts_rec.pre_encumbrance,0) 
                     - nvl(current_amounts_rec.pre_payable,0); 
     else 
      v_pre_amount := nvl(current_amounts_rec.pre_budget,0)      * nvl(current_amounts_rec.budget_multiplier,0) 
                    - nvl(current_amounts_rec.pre_warehouse,0)   * nvl(current_amounts_rec.encumbrance_multiplier,0) 
                    - nvl(current_amounts_rec.pre_encumbrance,0) * nvl(current_amounts_rec.encumbrance_multiplier,0) 
                    - nvl(current_amounts_rec.pre_payable,0)     * nvl(current_amounts_rec.amount_multiplier,0); 
     end if; 
  
   else --no budget is set 
      v_pre_amount :=  nvl(current_amounts_rec.pre_warehouse,0) 
                     + nvl(current_amounts_rec.pre_encumbrance,0) 
                     + nvl(current_amounts_rec.pre_payable,0); 
   end if; 
  
 -- return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) 
 --        + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) 
 --         - (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) 
 --         - (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0) 
 --         + nvl(v_pre_amount,0)); 
  
   return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) - 
          (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) - 
          (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0)) 
          + nvl(v_pre_amount,0);  
 
  -- end dph 13037-- 
        
   -- pre 13037 dph change the code was: 
   -- note the subtraction of the pre_payable 
 -- return ((nvl(current_amounts_rec.budget_cur,0) * nvl(current_amounts_rec.budget_multiplier,0)) + (nvl(current_amounts_rec.adjustment_cur,0) * nvl(current_amounts_rec.budget_multiplier,0))) - 
 --         (nvl(current_amounts_rec.encumbrance_cur,0) * nvl(current_amounts_rec.encumbrance_multiplier,0)) - 
 --         (nvl(current_amounts_rec.amount_cur,0) * nvl(current_amounts_rec.amount_multiplier,0))- 
 --         (nvl(current_amounts_rec.pre_payable,0) * nvl(current_amounts_rec.amount_multiplier,0));   
   
  
  else 
  return 0; 
  end if; 
  close current_amounts_cur;  
  return 0; 
</multilinecomment>
END;
		*/
        #endregion
        //ID:347
        /// <summary></summary>
        /// <remarks>
        /// F2N_PURE_BUSINESS_LOGIC : The code of this function was identified as containing business logic. See documentation for details.
        /// </remarks>
        public virtual NNumber FGetAccountBalance(RecurJeDataAdapter recurJeDataElement)
        {
            int rowCount = 0;
            String sqlcurrentAmountsCur = "SELECT ca.account_id, sum(ca.adjustment_cur) adjustment_cur, sum(ca.amount_cur) amount_cur, sum(ca.budget_cur) budget_cur, sum(ca.encumbrance_cur) encumbrance_cur, max(ac_t.encumbrance_multiplier) encumbrance_multiplier, max(ac_t.amount_multiplier) amount_multiplier, max(ac_t.budget_multiplier) budget_multiplier, max(ac.pre_payable) pre_payable, max(ac.pre_budget) pre_budget, max(ac.pre_warehouse) pre_warehouse, max(ac.pre_encumbrance) pre_encumbrance, max(ac.account_type) account_type " +
    " FROM fas.current_amounts ca, shr.accounts ac, shr.account_types ac_t " +
    " WHERE ca.account_id = @P_IN_ACCOUNT_ID AND ca.account_period <= @P_IN_ACCOUNTING_PERIOD AND ac.account_id = ca.account_id AND ac_t.account_type = ac.account_type " +
    " GROUP BY ca.account_id ";
            DataCursor currentAmountsCur = new DataCursor(sqlcurrentAmountsCur);
            String currentAmountsCurInAccountId = string.Empty;
            String currentAmountsCurInAccountingPeriod = string.Empty;
            TableRow currentAmountsRec = null;
            NNumber vPreAmount = 0;
            NNumber vReturnAmount = 0;
            //  dph alio-13043 create common code from fainq05a.fmx and use it; 

            vReturnAmount = StoredProcedures.Fabal01aAvailBalance_(recurJeDataElement.AccountId, "Y", Model.BatchNoBlock.AccountingPeriod);

            return vReturnAmount;
        }




    }
}
