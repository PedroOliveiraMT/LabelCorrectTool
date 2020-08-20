using Alio.Forms.Fjent01a.Model;
using Foundations.Core.AppDataLayer.Data;
using Foundations.Core.AppSupportLib;
using Foundations.Core.AppSupportLib.Runtime;
using Foundations.Core.AppSupportLib.Runtime.Action;
using Foundations.Core.AppSupportLib.Runtime.Controller;

namespace Alio.Forms.Fjent01a.Controllers
{

    public class EditContextController : AbstractMenuController<Fjent01aTask, Fjent01aModel>
    {


        public EditContextController(ITaskController parentController, string name) : base(parentController, name)
        {

        }

        //action methods generated from triggers
        #region Original PL/SQL code code for TRIGGER APPROVE_ITEM.PLSQL_CMD
        /*
        DECLARE

cur_blk    varchar2(100) := substr(get_block_property(:system.cursor_block, DML_DATA_TARGET_NAME),
                            instr(get_block_property(:system.cursor_block, DML_DATA_TARGET_NAME),'.',1,1)+1);
cur_itm    varchar2(100) := nvl(get_item_property(:system.current_block||'.'||:system.current_item, COLUMN_NAME ),
                                get_item_property(:system.current_block||'.'||:system.current_item, ITEM_NAME ));

BEGIN

--MESSAGE('Approve_Item - ');message(' ',no_acknowledge);

if get_menu_item_property('EDIT.approve_item',CHECKED) = 'TRUE' then
 forms_DDL('insert into adm.item_approval '||
           'values ('||''''||cur_blk||''''||', '||''''||cur_itm||''''||')');
else
 forms_DDL('delete adm.item_approval'||
           ' where table_name  = '||''''||cur_blk||''''||
           '   and column_name = '||''''||cur_itm||'''');
end if;

 forms_DDL('commit'); -- adm.item_appropval

if FORM_FAILURE then
     message('Error handling Item_Approval - '||DBMS_ERROR_TEXT);
     message(' ',no_Acknowledge);
end if;

END;
        */
        #endregion
        /// <summary>
        /// </summary>
        /// <remarks>
        ///	ID:
        /// 
        ///</remarks>
        /// <TriggerInfo>APPROVE_ITEM.PLSQL_CMD</TriggerInfo>

        [MenuTrigger(Item = "APPROVE_ITEM")]
        public void itmEditApproveItem_Click()
        {
            string curBlk = Lib.Substring(BlockServices.GetDmlDataTargetName(TaskServices.CursorBlock), Lib.InStr(BlockServices.GetDmlDataTargetName(TaskServices.CursorBlock), ".", 1, 1) + 1);
            string curItm = Lib.IsNull(ItemServices.GetDbColumnName(TaskServices.CurrentBlock + "." + TaskServices.CurrentItem), ItemServices.GetName(TaskServices.CurrentBlock + "." + TaskServices.CurrentItem));
            // MESSAGE('Approve_Item - ');message(' ',no_acknowledge);

            if (MenuServices.GetMenuItemProperty("EDIT.approve_item", MenuItemProperty.CHECKED))
            {
                Lib.FormsDDL("insert into adm.item_approval " + "values (" + "'" + curBlk + "'" + ", " + "'" + curItm + "'" + ")");
            }
            else
            {
                Lib.FormsDDL("delete adm.item_approval" + " where table_name  = " + "'" + curBlk + "'" + "   and column_name = " + "'" + curItm + "'");
            }
            DbManager.Commit();
            //  adm.item_appropval
            if (!TaskServices.ServiceSuccess)
            {
                TaskServices.Message("Error handling Item_Approval - " + DbManager.ErrorMessage);
                TaskServices.Message(" ", TaskServices.NO_ACKNOWLEDGE);
            }
        }

    }

}
