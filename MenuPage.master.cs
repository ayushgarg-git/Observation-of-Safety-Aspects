using AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MenuPage : System.Web.UI.MasterPage
{
    dbFunction objDB = new dbFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            //Label UserName = (Label)HeadLoginView.FindControl("LoginName");
            //UserName.Text = Session["NAME"].ToString();
            bindMenuItems();
        }
    }

    protected void bindMenuItems()
    {
        //if (Session["User_Role"].ToString() == "S") //Super Uer
        //{
        //    NavigationMenu.FindItem("Admin").Enabled = true;
        //    NavigationMenu.FindItem("Edit_Data").Enabled = true;
        //    NavigationMenu.FindItem("Report").Enabled = true;
        //    NavigationMenu.FindItem("Status").Enabled = true;
        //}
        //else if ((Session["User_Role"].ToString() == "V") || (Session["User_Role"].ToString() == "E"))  //User_Role - V-View Access; E-Edit Access
        //{
        //    NavigationMenu.FindItem("Admin").Enabled = false;
        //    NavigationMenu.FindItem("Edit_Data").Enabled = true;
        //    NavigationMenu.FindItem("Report").Enabled = true;
        //    NavigationMenu.FindItem("Status").Enabled = false;
        //}
        //else if ((Session["User_Role"].ToString() == "Z"))  //User_Role - Dir & ABove and CM_users employees;  
        //{
        //    NavigationMenu.FindItem("Admin").Enabled = false;
        //    NavigationMenu.FindItem("Edit_Data").Enabled = true;
        //    NavigationMenu.FindItem("Report").Enabled = true;
        //    NavigationMenu.FindItem("Status").Enabled = true;
        //}
        //else if ((Session["User_Role"].ToString() == "ID"))  //User_Role - Independant Dir;  
        //{
        //    NavigationMenu.FindItem("Admin").Enabled = true;
        //    NavigationMenu.FindItem("Admin/Job").Enabled = false;
        //    NavigationMenu.FindItem("Edit_Data").Enabled = true;
        //    NavigationMenu.FindItem("Report").Enabled = true;
        //    NavigationMenu.FindItem("Status").Enabled = false;
        //}

        //Administration Menu Item
        if ((Session["User_Role"].ToString() == "Z") || (Session["User_Role"].ToString() == "V") || (Session["User_Role"].ToString() == "E"))
        {
            MenuItem item = (MenuItem)this.NavigationMenu.FindItem("Reset");
            if (item != null)
            {
                RemoveMenuItem(this.NavigationMenu.Items, item);
            }
        }

        if ((Session["User_Role"].ToString() == "ID") || (Session["User_Role"].ToString() == "Z") || (Session["User_Role"].ToString() == "V") || (Session["User_Role"].ToString() == "E"))
        {
            MenuItem item = (MenuItem)this.NavigationMenu.FindItem("Job");
            if (item != null)
            {
                RemoveMenuItem(this.NavigationMenu.Items, item);
            }
        }

        //Status Menu Item
        if ((Session["User_Role"].ToString() == "ID") || (Session["User_Role"].ToString() == "V") || (Session["User_Role"].ToString() == "E"))
        {
            MenuItem item = (MenuItem)this.NavigationMenu.FindItem("Status");
            if (item != null)
            {
                RemoveMenuItem(this.NavigationMenu.Items, item);
            }
        }
    }
   
    private bool RemoveMenuItem(MenuItemCollection items, MenuItem itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            return true;
        }

        foreach (MenuItem item in items)
        {
            if (RemoveMenuItem(item.ChildItems, itemToRemove))
            {
                return true;
            }
        }
        return false;
    }

    protected void clear_cookies(object sender, EventArgs e)
    {
        HttpCookie aCookie;
        string cookieName;
        int limit = Request.Cookies.Count - 1;
        for (int i = 0; i <= limit; i++)
        {
            cookieName = Request.Cookies[i].Name;
            aCookie = new HttpCookie(cookieName);
            aCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(aCookie);
        }
    }
}
