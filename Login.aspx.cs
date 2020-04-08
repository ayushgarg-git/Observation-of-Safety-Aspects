using System;
using System.Web.UI;
using System.Text;
using AppCode;
using System.Collections.Generic;
using System.Web.Security;
using System.Data;
using System.DirectoryServices;
using System.Collections;

public partial class Login : System.Web.UI.Page
{
    dbFunction objDB = new dbFunction();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            SetFocus(txtUserName);
            Session.Abandon();
            Session.Clear();
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string userId = txtUserName.Text.ToUpper().ToString().Trim();
        string password = txtPassword.Text.ToString();

        string error = "";

        if (userId.Equals(string.Empty))
        {
            error += "User Id is required.\\n";
        }
        if (password.Equals(string.Empty))
        {
            error += "Password is required.\\n";
        }
        bool valid = false;

        if (error.Equals(string.Empty) && userId.Length <= 4)
        {
            valid = authenticateUser(userId.ToUpper(), password);
            if (valid)
            {
                FormsAuthentication.RedirectFromLoginPage(Session["USERID"].ToString(), false);
            }
            else
            {
                error = "Error: Either You are not authorized or you entered wrong password";
                Common.Show(error);
                //lblError.Text = error.Replace("\\n", "<br/>");
            } 
        }
        else
        {
            error = "Error: Invalid User..Kindly check User ID";
            Common.Show(error);
            //lblError.Text = error.Replace("\\n", "<br/>");
        }
    }

    protected bool authenticateUser(string userId, string password)
    {
        bool isValid = false;
        bool chk_user = false;
        if (userId.Length == 4)
        {
            if (Constants.admin_pwd.Equals(password))
                isValid = true;

            if (isValid)
            {
                chk_user = IS_OSA_user(userId.ToString());
                if (chk_user)
                {
                    Session["USERID"] = userId;
                    return true;
                }
                else
                {
                    Common.Show("Error: Invalid User\n");
                    return false;
                }
            }
            else
            {
                Common.Show("Error: Invalid password\n");
                return false;
            }
        }
        return isValid;
    }

    //Employee is Project Manager or Contract Manager?
    public bool IS_OSA_user(string empno)
    {
        StringBuilder sbQuery = new StringBuilder();
        Dictionary<string, string> paramList = new Dictionary<string, string>();
        sbQuery.Append("SELECT EMP_ROLE FROM OSA_ROLE where EMP_ID=:empno");
        paramList.Add(":empno", empno);

        string user_role=objDB.executeScalar(sbQuery.ToString(), paramList);
        if (!string.IsNullOrEmpty(user_role))
        {
            Session["User_Role"] = user_role;
            return true;
        }
        else
        {
            return false;
        }
    }
}

