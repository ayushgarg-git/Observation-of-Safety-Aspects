using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Constants
{
	public Constants()
	{		
	}
    public const string title = "Project Corporate Review System";
    public const string admin_pwd = "1234";
    public const string report_path = "userdata/reports/";
    public const string user_eil = "E";
    public const string user_contractor = "C";
    public const string user_vendor = "V";
    public const string DIV_CODE = "34";
    public const string DEPT_CODE = "57";
    public const string CONCERN_ENGG = "101";
    public const string CONCERN_CONSTR = "102";
    public const string CONCERN_VENDOR = "103";
    public const string CONCERN_CONTRACTOR = "104";
    public const string CONCERN_LICENSOR = "105";
    public const string JOB_STATUS_CLOSED = "C";
    public const string JOB_STATUS_OPEN = "O";

    public const string User_Role_RCM = "R";
    public const string User_Role_Area_Co = "A";
    public const string User_Role_SO = "S";
    public const string User_Role_Cont = "C";

    //public const string loginHistoryQuery = "INSERT INTO PCR_AUDIT_TRAIL (USER_ID, ACTION_DATE, MACHINE_IP, MODULE_NAME, ACTION, SITE_CD) VALUES (:USER_ID, SYSDATE, :MACHINE_IP, :MODULE_NAME, :ACTION, :SITE_CD)";
}