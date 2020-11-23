using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/12 20:54:32
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.ORCart
{
    public class MenuTreeMagr
    {
        public static string GetMenuName(sys_menu menu)
        {
            string MenuName = "";
            if (menu != null && menu.ChildMenu.Count > 0)
            {
                MenuName = GetMenuName(menu.ChildMenu[0]);
            }
            else
                MenuName = menu.menu_name;
            return MenuName;
        }
    }
    public interface IMenuMsgSend
    {
        object Content { get; set; }
        void sendMsg(string code, string MsgType = "");
        void MenuSelected(string menuName);
    }
}
