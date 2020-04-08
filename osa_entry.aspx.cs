using AppCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class osa_entry : System.Web.UI.Page
{

    dbFunction objDB = new dbFunction();
    public string user_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        string User = Session["UserId"].ToString().Trim();

        if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            //Response.Redirect("Default.aspx");
        }

        user_role = Session["User_Role"].ToString();


        if (user_role == Constants.User_Role_RCM)
        {
            btn_SO_Save.Visible = false;
            btn_Submit_cont.Visible = false;
            btn_Submit_so.Visible = false;
            btn_so_Accept.Visible = false;
            btn_so_Reject.Visible = false;
            edit_remarks.Visible = false;
            ddlUnit.Enabled = false;
            rtbnYes.Enabled = false;
            rtbnNo.Enabled = false;
            // btn_add.Visible = false;

        }
        else if (user_role == Constants.User_Role_Cont)
        {
            btn_SO_Save.Visible = false;
            btn_Submit_cont.Visible = false;
            btn_Submit_so.Visible = true;
            btn_so_Accept.Visible = false;
            btn_so_Reject.Visible = false;
            btn_rcm_Accept.Visible = false;
            btn_rcm_Reject.Visible = false;
            btn_rcm_edit.Visible = false;
            btn_rcm_save.Visible = false;
            edit_remarks.Visible = false;
            ddlUnit.Enabled = false;
            rtbnYes.Enabled = false;
            rtbnNo.Enabled = false;
        }
        else if (user_role == Constants.User_Role_SO)
        {
            btn_SO_Save.Visible = true;
            btn_Submit_cont.Visible = true;
            btn_Submit_so.Visible = false;
            btn_so_Accept.Visible = true;
            btn_so_Reject.Visible = true;
            btn_rcm_Accept.Visible = false;
            btn_rcm_Reject.Visible = false;
            btn_rcm_edit.Visible = false;
            btn_rcm_save.Visible = false;
        }


        if (!IsPostBack)
        {
            bindRisk_Details();
            Fill_data();
        }


    }

    protected void Fill_data()
    {
        try
        {
            string User = Session["UserId"].ToString().Trim();

            string str_doc_no = "A545-100-OSA-1101";
            string str_rev_no = "0";
            string str_project_id = "A545";
            string str_unit = "100";
            string str_contractor_id = "C001";


            //string default_display_string ;
            //  string time_allowed_unit_string = ddlUnit.SelectedValue.ToString();
            StringBuilder show_time_unit_Query = new StringBuilder();
            StringBuilder show_time_Query = new StringBuilder();
            StringBuilder show_suspension_Query = new StringBuilder();
            StringBuilder check_rcm_accept_Query = new StringBuilder();
            Dictionary<string, string> default_display_ParamList = new Dictionary<string, string>();

            default_display_ParamList.Add(":DOC_NO", str_doc_no);
            default_display_ParamList.Add(":REV_NO", str_rev_no);
            default_display_ParamList.Add(":PROJECT_ID", str_project_id);
            default_display_ParamList.Add(":UNIT", str_unit);
            default_display_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);

            check_rcm_accept_Query.Append("SELECT RCM_ACCEPT_FLAG FROM OSA_MASTER WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
            string check_rcm_accept;
            check_rcm_accept = objDB.executeScalar(check_rcm_accept_Query.ToString(), default_display_ParamList);
            if (check_rcm_accept == "1")
            {
                btn_rcm_Accept.Visible = false;
                btn_rcm_Reject.Visible = false;
                btn_rcm_edit.Visible = false;
                btn_rcm_save.Visible = false;
                txt_Time_allowed.Enabled = false;
                ddlUnit.Enabled = false;
                rtbnYes.Enabled = false;
                rtbnNo.Enabled = false;
            }

            show_time_unit_Query.Append("SELECT TIME_ALLOWED_UNIT FROM OSA_MASTER WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
            show_time_Query.Append("SELECT TIME_ALLOWED FROM OSA_MASTER WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
            show_suspension_Query.Append("SELECT SUSPENSION_OF_WORK FROM OSA_MASTER WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");


            StringBuilder show_SO_remarks_Query = new StringBuilder();

            Dictionary<string, string> show_SO_remarks_ParamList = new Dictionary<string, string>();

            show_SO_remarks_Query.Append("SELECT REMARKS_DESC FROM OSA_REMARKS WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND REMARKS_ROLE_S_R=:REMARKS_ROLE_S_R ");

            show_SO_remarks_ParamList.Add(":DOC_NO", str_doc_no);
            show_SO_remarks_ParamList.Add(":REV_NO", str_rev_no);
            show_SO_remarks_ParamList.Add(":PROJECT_ID", str_project_id);
            show_SO_remarks_ParamList.Add(":UNIT", str_unit);
            show_SO_remarks_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
            show_SO_remarks_ParamList.Add(":REMARKS_ROLE_S_R", Constants.User_Role_SO);

            string SO_remarks;
            SO_remarks = objDB.executeScalar(show_SO_remarks_Query.ToString(), show_SO_remarks_ParamList);
            if (SO_remarks != null)
            {
                //CALL SQL QUERY FOR DEFAULT_DATA;

                string unit = objDB.executeScalar(show_time_unit_Query.ToString(), default_display_ParamList);
                string suspension = objDB.executeScalar(show_suspension_Query.ToString(), default_display_ParamList);
                string time_allowed = objDB.executeScalar(show_time_Query.ToString(), default_display_ParamList);
                //unit check
                ddlUnit.SelectedValue = unit;
                //radio button
                if (suspension == "Y")
                {
                    rtbnYes.Checked = true;
                    rtbnNo.Checked = false;
                }
                else
                {
                    rtbnYes.Checked = false;
                    rtbnNo.Checked = true;
                }

                //time allowed
                txt_Time_allowed.Text = time_allowed;
            }
            txtSO_Remarks.Text = SO_remarks;

            //RCM REMARKS
            StringBuilder show_RCM_remarks_Query = new StringBuilder();
            Dictionary<string, string> show_RCM_remarks_ParamList = new Dictionary<string, string>();

            show_RCM_remarks_Query.Append("SELECT REMARKS_DESC FROM OSA_REMARKS WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND REMARKS_ROLE_S_R=:REMARKS_ROLE_S_R ");

            show_RCM_remarks_ParamList.Add(":DOC_NO", str_doc_no);
            show_RCM_remarks_ParamList.Add(":REV_NO", str_rev_no);
            show_RCM_remarks_ParamList.Add(":PROJECT_ID", str_project_id);
            show_RCM_remarks_ParamList.Add(":UNIT", str_unit);
            show_RCM_remarks_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
            show_RCM_remarks_ParamList.Add(":REMARKS_ROLE_S_R", "R".ToString());

            string RCM_remarks;
            RCM_remarks = objDB.executeScalar(show_RCM_remarks_Query.ToString(), show_RCM_remarks_ParamList);
            txtRCM_Remarks.Text = RCM_remarks;
        }
        catch (Exception ex)
        {
            lbl_err.Text = "Error in populating item group. Error details: " + ex.Message;
            //  ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
        }
    }

    #region Risk_Mitigation_Bind

    protected void bindRisk_Details()
    {

        string user_role = Session["User_Role"].ToString();
        string User = Session["USERID"].ToString().Trim();
        StringBuilder sbQuery = new StringBuilder();
        Dictionary<string, string> paramList = new Dictionary<string, string>();
        sbQuery.Append("select * from OSA_OBSERVATION ");
        //paramList.Add(":JOB_NO_WHR", Job_no);
        //paramList.Add(":MONTH_CODE_WHR", Month_Code);
        //paramList.Add(":YEAR_CODE_WHR", Year_Code);
        objDB.bindGridView(gvRisk, sbQuery.ToString(), paramList);

        if (gvRisk.Rows.Count > 0)
        {
            if (user_role == Constants.User_Role_SO)
            {
                //Add buton Visibele=True
            }
        }
        else
        {
            //Add buton Visibele=False
        }
    }

    protected void gvRisk_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvRisk.EditIndex = e.NewEditIndex;
        bindRisk_Details();
    }

    protected void gvRisk_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvRisk.EditIndex = -1;
        bindRisk_Details();
    }

    protected void gvRisk_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            user_role = Session["User_Role"].ToString();

            lbl_err.Text = "";
            LinkButton senderBtn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)senderBtn.NamingContainer;
            GridView grid = (GridView)row.NamingContainer;
            string User = Session["USERID"].ToString().Trim();
            string str_doc_no = "A545-100-OSA-1101";
            string str_rev_no = "0";
            string str_project_id = "A545";
            string str_unit = "100";
            string str_contractor_id = "C001";

            //if (e.CommandName == "Edit")
            //{
            //    EditHazards.Visible = true;
            //    EditSuggestion.Visible = true;
            //}
            if (e.CommandName == "Update")
            {
                Label Str_OBSERVATION_ID = (Label)row.FindControl("lbl_Obs_id");
                TextBox EditHazards = (TextBox)row.FindControl("hazard_edit");
                TextBox EditSuggestion = (TextBox)row.FindControl("suggestion_edit");
                TextBox EditCorrections = (TextBox)row.FindControl("contractor_report_edit");
                if (string.IsNullOrEmpty(EditHazards.Text) == true)
                {
                    //  lbl_err.Text = "Observations can not be empty";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Observations can not be empty')</script>");
                }
                else
                {
                    StringBuilder sbEdit_Query = new StringBuilder();
                    Dictionary<string, string> Edit_ParamList = new Dictionary<string, string>();
                    //sbEdit_Query.Append(" UPDATE CM_RISK_DETAILS SET RISK_DESC = :Risk_desc, MITIGATION_DESC = :Mitigation_desc, ");
                    //sbEdit_Query.Append(" UPDATED_ON = SYSDATE , UPDATED_BY=:UPDATED_BY ");
                    //sbEdit_Query.Append(" WHERE JOB_NO =:JOB_NO_WHR AND MONTH_CODE = :MONTH_CODE_WHR AND YEAR_CODE =:YEAR_CODE_WHR and RISK_SR_NO= :RISK_SR_NO");
                    if (user_role == "C")
                    {
                        sbEdit_Query.Append("UPDATE OSA_OBSERVATION SET CONT_UPDATE_ON=sysdate,CONT_UPDATE_BY=:CONT_UPDATE_BY,OBSV_DETAILS =:Hazards,OBSV_RECOMMENDATION =:Suggestions,OBSV_CORR_ACT=:Corrections");
                        sbEdit_Query.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND OBSERVATION_ID=:OBSERVATION_ID ");
                        // Edit_ParamList.Add(":CONT_UPDATE_ON","sysdate");
                        Edit_ParamList.Add(":CONT_UPDATE_BY", User.ToString());
                    }
                    else if (user_role == "S")
                    {
                        sbEdit_Query.Append("UPDATE OSA_OBSERVATION SET OBSV_UPDATE_ON=sysdate,OBSV_UPDATE_BY=:OBSV_UPDATE_BY,OBSV_DETAILS =:Hazards,OBSV_RECOMMENDATION =:Suggestions,OBSV_CORR_ACT=:Corrections");
                        sbEdit_Query.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND OBSERVATION_ID=:OBSERVATION_ID ");
                        //  Edit_ParamList.Add(":OBSV_UPDATE_ON", "sysdate");
                        Edit_ParamList.Add(":OBSV_UPDATE_BY", User.ToString());
                    }


                    Edit_ParamList.Add(":Hazards", EditHazards.Text.ToString());
                    Edit_ParamList.Add(":Suggestions", EditSuggestion.Text.ToString());
                    Edit_ParamList.Add(":Corrections", EditCorrections.Text.ToString());

                    Edit_ParamList.Add(":DOC_NO", str_doc_no);
                    Edit_ParamList.Add(":REV_NO", str_rev_no);
                    Edit_ParamList.Add(":PROJECT_ID", str_project_id);
                    Edit_ParamList.Add(":UNIT", str_unit);
                    Edit_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                    Edit_ParamList.Add(":OBSERVATION_ID", Str_OBSERVATION_ID.Text.ToString());
                    int i = objDB.executeNonQuery(sbEdit_Query.ToString(), Edit_ParamList);
                    if (i > 0)
                    {
                        gvRisk.EditIndex = -1;
                        // lbl_err.Text = "Data updated Successfully!!";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Data updated Successfully!!')</script>");
                    }
                    else
                    {
                        // lbl_err.Text = "Error";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                    }
                }
            }
            else if (e.CommandName == "Add")
            {
                gvRisk.ShowFooter = true;
            }
            else if (e.CommandName == "EmptyCancel")
            {
                gvRisk.ShowFooter = false;
            }
            else if (e.CommandName == "Insert")
            {
                //Label CaseRowID = (Label)row.FindControl("lblCaseID");
                TextBox Ftr_Hazards = (TextBox)row.FindControl("txt_hazard_footer");
                TextBox Ftr_Suggestion = (TextBox)row.FindControl("txt_suggestion_footer");
                if (string.IsNullOrEmpty(Ftr_Hazards.Text) == true)
                {
                    //   lbl_err.Text = "Hazards can not be empty";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Hazards can not be empty')</script>");
                }
                else
                {
                    StringBuilder sbFooter_InsertQuery = new StringBuilder();
                    Dictionary<string, string> Footer_insertParamList = new Dictionary<string, string>();
                    sbFooter_InsertQuery.Append("INSERT INTO OSA_OBSERVATION (DOC_NO,REV_NO,PROJECT_ID,UNIT,CONTRACTOR_ID,OBSV_DETAILS, OBSV_RECOMMENDATION, OBSV_ADD_BY, OBSV_ADD_ON, OBSERVATION_ID) VALUES ");
                    sbFooter_InsertQuery.Append("(:DOC_NO,:REV_NO,:PROJECT_ID,:UNIT,:CONTRACTOR_ID,:Hazards,:Suggestions,:Added_by,sysdate,");
                    sbFooter_InsertQuery.Append("(SELECT MAX(OBSERVATION_ID)+1 FROM OSA_OBSERVATION))");
                    //sbFooter_InsertQuery.Append("INSERT INTO CM_RISK_DETAILS (JOB_NO, MONTH_CODE, YEAR_CODE, RISK_DESC, MITIGATION_DESC,RISK_SR_NO, ADDED_DATE, ADDED_BY) VALUES ");
                    //sbFooter_InsertQuery.Append("(:JOB_NO_TEXT,:MONTH_CODE_TEXT,:YEAR_CODE_TEXT,:Risk_desc,:Mitigation_desc, (SELECT NVL (MAX (RISK_SR_NO), 0) + 1 countdata FROM CM_RISK_DETAILS ");
                    //sbFooter_InsertQuery.Append(" WHERE JOB_NO =:JOB_NO_WHR AND MONTH_CODE = :MONTH_CODE_WHR AND YEAR_CODE =:YEAR_CODE_WHR), sysdate, :Added_by)");
                    Footer_insertParamList.Add(":DOC_NO", str_doc_no);
                    Footer_insertParamList.Add(":REV_NO", str_rev_no);
                    Footer_insertParamList.Add(":PROJECT_ID", str_project_id);
                    Footer_insertParamList.Add(":UNIT", str_unit);
                    Footer_insertParamList.Add(":CONTRACTOR_ID", str_contractor_id);

                    Footer_insertParamList.Add(":Hazards", Ftr_Hazards.Text.ToString());
                    Footer_insertParamList.Add(":Suggestions", Ftr_Suggestion.Text.ToString());
                    Footer_insertParamList.Add(":Added_by", User);
                    int i = objDB.executeNonQuery(sbFooter_InsertQuery.ToString(), Footer_insertParamList);
                    if (i > 0)
                    {
                        gvRisk.ShowFooter = false;
                        // lbl_err.Text = "Data Inserted Successfully!!";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Data Inserted Successfully!!')</script>");
                    }
                    else
                    {
                        // lbl_err.Text = "Error";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                    }
                }
            }
            else if (e.CommandName == "EmptyInsert")
            {
                TextBox Empty_Hazards = (TextBox)row.FindControl("txtEmpty_Hazards");
                TextBox Empty_Suggestion = (TextBox)row.FindControl("txtEmpty_Suggestion");
                if (string.IsNullOrEmpty(Empty_Hazards.Text) == true)
                {
                    // lbl_err.Text = "Hazards can not be empty";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Hazards can not be empty')</script>");
                }

                else
                {
                    StringBuilder sbEmpty_InsertQuery = new StringBuilder();
                    Dictionary<string, string> Empty_insertParamList = new Dictionary<string, string>();
                    sbEmpty_InsertQuery.Append("INSERT INTO OSA_OBSERVATION (DOC_NO, REV_NO, PROJECT_ID, UNIT, CONTRACTOR_ID,OBSV_DETAILS, OBSV_RECOMMENDATION, OBSV_ADD_BY, OBSV_ADD_ON, OBSERVATION_ID) VALUES ");
                    sbEmpty_InsertQuery.Append(" (:DOC_NO,:REV_NO,:PROJECT_ID,:UNIT,:CONTRACTOR_ID,:Hazards,:Suggestions,:Added_by,sysdate,1)");

                    Empty_insertParamList.Add(":DOC_NO", str_doc_no);
                    Empty_insertParamList.Add(":REV_NO", str_rev_no);
                    Empty_insertParamList.Add(":PROJECT_ID", str_project_id);
                    Empty_insertParamList.Add(":UNIT", str_unit);
                    Empty_insertParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                    Empty_insertParamList.Add(":Hazards", Empty_Hazards.Text.ToString());
                    Empty_insertParamList.Add(":Suggestions", Empty_Suggestion.Text.ToString());
                    Empty_insertParamList.Add(":Added_by", User);


                    int i = objDB.executeNonQuery(sbEmpty_InsertQuery.ToString(), Empty_insertParamList);
                    if (i > 0)
                    {
                        //  lbl_err.Text = "Data Inserted Successfully!!";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Data Inserted Successfully!!')</script>");
                    }
                    else
                    {
                        //  lbl_err.Text = "Error";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                    }
                }
            }
            else if (e.CommandName == "Delete")
            {
                Label Str_OBSERVATION_ID = (Label)row.FindControl("lbl_Obs_id");
                StringBuilder sbDelete_Query = new StringBuilder();
                Dictionary<string, string> Delete_ParamList = new Dictionary<string, string>();
                sbDelete_Query.Append(" DELETE FROM OSA_OBSERVATION ");
                sbDelete_Query.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND ");
                sbDelete_Query.Append(" OBSERVATION_ID=:OBSERVATION_ID");
                Delete_ParamList.Add(":DOC_NO", str_doc_no);
                Delete_ParamList.Add(":REV_NO", str_rev_no);
                Delete_ParamList.Add(":PROJECT_ID", str_project_id);
                Delete_ParamList.Add(":UNIT", str_unit);
                Delete_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                Delete_ParamList.Add(":OBSERVATION_ID", Str_OBSERVATION_ID.Text.ToString());

                int i = objDB.executeNonQuery(sbDelete_Query.ToString(), Delete_ParamList);
                if (i > 0)
                {
                    //lbl_err.Text = "Data Deleted Successfully!!";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Data Deleted Successfully!!')</script>");
                }
                else
                {
                    //lbl_err.Text = "Error";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                }
            }
            //gvRisk.EditIndex = -1;
            bindRisk_Details();
        }
        catch (Exception ex)
        {
            lbl_err.Text = ex.ToString();
            //  ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
        }
    }

    protected void gvRisk_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //do nothing
    }

    protected void gvRisk_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //do nothing
    }

    protected void gvRisk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
        //    e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
        //}
        string user_role = Session["User_Role"].ToString();
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            TextBox str_suggestion_add = (TextBox)e.Row.FindControl("txtEmpty_Suggestion");
            TextBox str_hazard_add = (TextBox)e.Row.FindControl("txtEmpty_Hazards");

            if (user_role == Constants.User_Role_SO)
            {
                str_suggestion_add.Enabled = true;
                str_hazard_add.Enabled = true;
            }
        }
        if ((e.Row.RowType == DataControlRowType.Header))
        {
            LinkButton lnlbtn_add = (LinkButton)e.Row.FindControl("btn_add");
            if (user_role == Constants.User_Role_Cont)
            {
                lnlbtn_add.Visible = false;
            }
            if (user_role == Constants.User_Role_SO)
            {
                lnlbtn_add.Visible = true;
            }
        }

        //check if the row is a datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            //check if the edit state > 0
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                //do stuff with the row that is being edited

                TextBox str_contractor_report_edit = (TextBox)e.Row.FindControl("contractor_report_edit");
                TextBox str_suggestion_edit = (TextBox)e.Row.FindControl("suggestion_edit");
                TextBox str_hazard_edit = (TextBox)e.Row.FindControl("hazard_edit");

                if (user_role == Constants.User_Role_RCM)
                {
                    //Button visible
                    //  gvRisk.Columns[4].Visible=false;
                    str_contractor_report_edit.Enabled = false;
                    str_suggestion_edit.Enabled = true;
                    str_hazard_edit.Enabled = true;
                }
                else if (user_role == Constants.User_Role_Cont)
                {
                    str_contractor_report_edit.Enabled = true;
                    str_suggestion_edit.Enabled = false;
                    str_hazard_edit.Enabled = false;
                }
                else if (user_role == Constants.User_Role_SO)
                {
                    str_contractor_report_edit.Enabled = false;
                    str_suggestion_edit.Enabled = true;
                    str_hazard_edit.Enabled = true;
                }
            }
            else
            {
                LinkButton lnkbtn_Delete = (LinkButton)e.Row.FindControl("btn_Delete");
                if (user_role == Constants.User_Role_RCM)
                {
                    //hide edit delete column for RCM for all observations
                    gvRisk.Columns[4].Visible = false;
                }
                if (user_role == Constants.User_Role_Cont)
                {
                    lnkbtn_Delete.Visible = false;
                }
                if (user_role == Constants.User_Role_SO)
                {
                    lnkbtn_Delete.Visible = true;
                }
            }
        }
        if ((e.Row.RowType == DataControlRowType.Footer))
        {
            TextBox str_contractor_report_add = (TextBox)e.Row.FindControl("contractor_report_footer");
            TextBox str_suggestion_add = (TextBox)e.Row.FindControl("txt_suggestion_footer");
            TextBox str_hazard_add = (TextBox)e.Row.FindControl("txt_hazard_footer");

            if (user_role == Constants.User_Role_RCM)
            {
                //hide edit delete column for RCM for all observations
                gvRisk.ShowFooter = false;
                str_contractor_report_add.Enabled = false;
                str_suggestion_add.Enabled = false;
                str_hazard_add.Enabled = false;
            }
        }

    }
    public bool Check_Risk_Count()
    {
        bool chk_flag = false;
        StringBuilder sbQuery = new StringBuilder();
        Dictionary<string, string> paramList = new Dictionary<string, string>();
        sbQuery.Append("select count (1) from CM_RISK_DETAILS where JOB_NO = :JOB_NO_WHR AND MONTH_CODE = :MONTH_CODE_WHR AND YEAR_CODE = :YEAR_CODE_WHR ");
        int count = objDB.ExecuteStatementCount(sbQuery.ToString(), paramList);
        if (count == 0)
        {
            chk_flag = true;
        }
        return chk_flag;
    }

    #endregion

    protected void gvRisk_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtEmpty_Hazards_TextChanged(object sender, EventArgs e)
    {

    }
    protected void lnkbtnfootr_add_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Submit_cont_Click(object sender, EventArgs e)
    {

    }
    protected void txt_Time_allowed_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btn_SO_Save_Click(object sender, EventArgs e)
    {
        try
        {

            string User = Session["UserId"].ToString().Trim();
            //  txt_Time_allowed
            if (ddlUnit.SelectedValue == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Please select a unit for time needed')</script>");
            }
            else
            {
                string time_allowed_string = txt_Time_allowed.Text.ToString();
                string time_allowed_unit_string = ddlUnit.SelectedValue.ToString();
                string str_doc_no = "A545-100-OSA-1101";
                string str_rev_no = "0";
                string str_project_id = "A545";
                string str_unit = "100";
                string str_contractor_id = "C001";
                string str_suspension;
                if (rtbnYes.Checked == true)
                    str_suspension = "Y";
                else
                    str_suspension = "N";

                //TIME ALLOWED



                //COUNT OSA MASTER ENTRIES
                StringBuilder count_so_box_Query = new StringBuilder();
                Dictionary<string, string> count_so_box_ParamList = new Dictionary<string, string>();
                count_so_box_Query.Append("SELECT COUNT(1) FROM OSA_MASTER WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
                count_so_box_ParamList.Add(":DOC_NO", str_doc_no);
                count_so_box_ParamList.Add(":REV_NO", str_rev_no);
                count_so_box_ParamList.Add(":PROJECT_ID", str_project_id);
                count_so_box_ParamList.Add(":UNIT", str_unit);
                count_so_box_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);

                int count_so_box = objDB.ExecuteStatementCount(count_so_box_Query.ToString(), count_so_box_ParamList);

                if (count_so_box > 0)
                {
                    StringBuilder time_allowed_Query = new StringBuilder();
                    Dictionary<string, string> time_allowed_ParamList = new Dictionary<string, string>();

                    time_allowed_Query.Append("UPDATE OSA_MASTER SET TIME_ALLOWED=:TIME_ALLOWED,TIME_ALLOWED_UNIT=:TIME_ALLOWED_UNIT,SUSPENSION_OF_WORK=:SUSPENSION,SO_UPDATE_BY=:SO_UPDATE_BY,SO_UPDATE_ON=sysdate");
                    time_allowed_Query.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");

                    time_allowed_ParamList.Add(":TIME_ALLOWED", time_allowed_string);
                    time_allowed_ParamList.Add(":TIME_ALLOWED_UNIT", time_allowed_unit_string);
                    time_allowed_ParamList.Add(":SUSPENSION", str_suspension);
                    time_allowed_ParamList.Add(":SO_UPDATE_BY", User);
                    //  time_allowed_ParamList.Add(":SO_UPDATED_ON", "sysdate".ToString());
                    time_allowed_ParamList.Add(":DOC_NO", str_doc_no);
                    time_allowed_ParamList.Add(":REV_NO", str_rev_no);
                    time_allowed_ParamList.Add(":PROJECT_ID", str_project_id);
                    time_allowed_ParamList.Add(":UNIT", str_unit);
                    time_allowed_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);

                    int i = objDB.executeNonQuery(time_allowed_Query.ToString(), time_allowed_ParamList);
                    if (i > 0)
                    {
                        // lbl_err.Text = "Remarks inserted Successfully!!";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Data inserted Successfully!!')</script>");
                    }
                    else
                    {
                        // lbl_err.Text = "Error";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                    }


                }
                else
                {
                    //Insert
                    StringBuilder time_allowed_Query = new StringBuilder();
                    Dictionary<string, string> time_allowed_ParamList = new Dictionary<string, string>();

                    time_allowed_Query.Append("INSERT INTO OSA_MASTER(DOC_NO,REV_NO,PROJECT_ID,UNIT,CONTRACTOR_ID,TIME_ALLOWED,TIME_ALLOWED_UNIT,SUSPENSION_OF_WORK,SO_ADD_BY,SO_ADD_ON) VALUES");
                    time_allowed_Query.Append("(:DOC_NO,:REV_NO,:PROJECT_ID,:UNIT,:CONTRACTOR_ID,:TIME_ALLOWED,:TIME_ALLOWED_UNIT,:SUSPENSION_OF_WORK,:SO_ADD_BY,sysdate)");

                    time_allowed_ParamList.Add(":DOC_NO", str_doc_no);
                    time_allowed_ParamList.Add(":REV_NO", str_rev_no);
                    time_allowed_ParamList.Add(":PROJECT_ID", str_project_id);
                    time_allowed_ParamList.Add(":UNIT", str_unit);
                    time_allowed_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                    time_allowed_ParamList.Add(":TIME_ALLOWED", time_allowed_string);
                    time_allowed_ParamList.Add(":TIME_ALLOWED_UNIT", time_allowed_unit_string);
                    time_allowed_ParamList.Add(":SUSPENSION_OF_WORK", str_suspension);
                    time_allowed_ParamList.Add(":SO_ADD_BY", User);
                    // time_allowed_ParamList.Add(":SO_ADD_ON", "sysdate".ToString());

                    int i = objDB.executeNonQuery(time_allowed_Query.ToString(), time_allowed_ParamList);
                    if (i > 0)
                    {
                        // lbl_err.Text = "Remarks inserted Successfully!!";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Data inserted Successfully!!')</script>");
                    }
                    else
                    {
                        // lbl_err.Text = "Error";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                    }

                }



                //SO REMARKS
                string str_remarks = txtSO_Remarks.Text.ToString();
                StringBuilder count_remarks_Query = new StringBuilder();
                Dictionary<string, string> count_remarks_ParamList = new Dictionary<string, string>();
                count_remarks_Query.Append("SELECT COUNT(1) FROM OSA_REMARKS WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND REMARKS_ROLE_S_R=:REMARKS_ROLE_S_R");
                count_remarks_ParamList.Add(":DOC_NO", str_doc_no);
                count_remarks_ParamList.Add(":REV_NO", str_rev_no);
                count_remarks_ParamList.Add(":PROJECT_ID", str_project_id);
                count_remarks_ParamList.Add(":UNIT", str_unit);
                count_remarks_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                count_remarks_ParamList.Add(":REMARKS_ROLE_S_R", Constants.User_Role_SO);
                int count_remarks = objDB.ExecuteStatementCount(count_remarks_Query.ToString(), count_remarks_ParamList);
                //string count_remarks_string = objDB.executeScalar(count_remarks_Query.ToString(), count_remarks_ParamList);
                //Int32 count_remarks = Convert.ToInt32(count_remarks_string);
                if (count_remarks > 0)
                {
                    //update

                    StringBuilder update_remarks = new StringBuilder();
                    Dictionary<string, string> update_remarks_ParamList = new Dictionary<string, string>();
                    update_remarks.Append("UPDATE OSA_REMARKS SET REMARKS_DESC=:REMARKS ");
                    update_remarks.Append("WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND REMARKS_ROLE_S_R=:REMARKS_ROLE_S_R");
                    update_remarks_ParamList.Add(":REMARKS", str_remarks);
                    update_remarks_ParamList.Add(":DOC_NO", str_doc_no);
                    update_remarks_ParamList.Add(":REV_NO", str_rev_no);
                    update_remarks_ParamList.Add(":PROJECT_ID", str_project_id);
                    update_remarks_ParamList.Add(":UNIT", str_unit);
                    update_remarks_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                    update_remarks_ParamList.Add(":REMARKS_ROLE_S_R", Constants.User_Role_SO);
                    int i2 = objDB.executeNonQuery(update_remarks.ToString(), update_remarks_ParamList);
                    if (i2 > 0)
                    {
                        // lbl_err.Text = "Remarks updated Successfully!!";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Remarks updated Successfully')</script>");
                    }
                    else
                    {
                        //  lbl_err.Text = "Error";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                    }
                }
                else if (count_remarks == 0)
                {
                    //Insert
                    StringBuilder insert_remarks = new StringBuilder();
                    Dictionary<string, string> insert_remarks_ParamList = new Dictionary<string, string>();
                    insert_remarks.Append("INSERT INTO OSA_REMARKS(DOC_NO,REV_NO,PROJECT_ID,UNIT,CONTRACTOR_ID,REMARKS_DESC,REMARKS_ROLE_S_R) VALUES");
                    insert_remarks.Append("(:DOC_NO,:REV_NO,:PROJECT_ID,:UNIT,:CONTRACTOR_ID,:REMARKS,:REMARKS_ROLE_S_R)");
                    insert_remarks_ParamList.Add(":DOC_NO", str_doc_no);
                    insert_remarks_ParamList.Add(":REV_NO", str_rev_no);
                    insert_remarks_ParamList.Add(":PROJECT_ID", str_project_id);
                    insert_remarks_ParamList.Add(":UNIT", str_unit);
                    insert_remarks_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                    insert_remarks_ParamList.Add(":REMARKS", str_remarks);
                    insert_remarks_ParamList.Add(":REMARKS_ROLE_S_R", Constants.User_Role_SO);
                    //insert_remarks_ParamList.Add(":REMARKS_ROLE_S_R", "S".ToString());

                    int i3 = objDB.executeNonQuery(insert_remarks.ToString(), insert_remarks_ParamList);
                    if (i3 > 0)
                    {
                        // lbl_err.Text = "Remarks inserted Successfully!!";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Remarks inserted Successfully!!')</script>");
                    }
                    else
                    {
                        // lbl_err.Text = "Error";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                    }
                }
                Fill_data();
            }
        }
        catch (Exception ex)
        {
            lbl_err.Text = ex.ToString();
        }
    }
    protected void btn_edit_remarks(object sender, EventArgs e)
    {
        //   save_edit_remarks.Visible = true;
        txtSO_Remarks.Enabled = true;
    }
    protected void btn_save_edit_remarks(object sender, EventArgs e)
    {
        txtSO_Remarks.Enabled = false;
    }



    protected void rtbnYes_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btn_edit_rcm_remarks(object sender, EventArgs e)
    {
        txtRCM_Remarks.Enabled = true;
    }
    protected void btn_rcm_Save_Click(object sender, EventArgs e)
    {
        try
        {

            string str_doc_no = "A545-100-OSA-1101";
            string str_rev_no = "0";
            string str_project_id = "A545";
            string str_unit = "100";
            string str_contractor_id = "C001";
            string str_remarks = txtRCM_Remarks.Text.ToString();
            StringBuilder count_remarks_Query = new StringBuilder();
            Dictionary<string, string> count_remarks_ParamList = new Dictionary<string, string>();
            count_remarks_Query.Append("SELECT COUNT(1) FROM OSA_REMARKS WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND REMARKS_ROLE_S_R=:REMARKS_ROLE_S_R ");
            count_remarks_ParamList.Add(":DOC_NO", str_doc_no);
            count_remarks_ParamList.Add(":REV_NO", str_rev_no);
            count_remarks_ParamList.Add(":PROJECT_ID", str_project_id);
            count_remarks_ParamList.Add(":UNIT", str_unit);
            count_remarks_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
            count_remarks_ParamList.Add(":REMARKS_ROLE_S_R", Constants.User_Role_RCM);
            int count_remarks = objDB.ExecuteStatementCount(count_remarks_Query.ToString(), count_remarks_ParamList);
            if (count_remarks > 0)
            {
                //update

                StringBuilder update_remarks = new StringBuilder();
                Dictionary<string, string> update_remarks_ParamList = new Dictionary<string, string>();
                update_remarks.Append("UPDATE OSA_REMARKS SET REMARKS_DESC=:REMARKS ");
                update_remarks.Append("WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID AND  REMARKS_ROLE_S_R=:REMARKS_ROLE_S_R");
                update_remarks_ParamList.Add(":REMARKS", str_remarks);
                update_remarks_ParamList.Add(":DOC_NO", str_doc_no);
                update_remarks_ParamList.Add(":REV_NO", str_rev_no);
                update_remarks_ParamList.Add(":PROJECT_ID", str_project_id);
                update_remarks_ParamList.Add(":UNIT", str_unit);
                update_remarks_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                update_remarks_ParamList.Add(":REMARKS_ROLE_S_R", Constants.User_Role_RCM);
                int i = objDB.executeNonQuery(update_remarks.ToString(), update_remarks_ParamList);
                if (i > 0)
                {
                    // lbl_err.Text = "Remarks updated Successfully!!";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Remarks updated Successfully')</script>");
                }
                else
                {
                    //  lbl_err.Text = "Error";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                }
            }
            else if (count_remarks == 0)
            {
                //Insert
                StringBuilder insert_remarks = new StringBuilder();
                Dictionary<string, string> insert_remarks_ParamList = new Dictionary<string, string>();
                insert_remarks.Append("INSERT INTO OSA_REMARKS(DOC_NO,REV_NO,PROJECT_ID,UNIT,CONTRACTOR_ID,REMARKS_DESC,REMARKS_ROLE_S_R) VALUES");
                insert_remarks.Append("(:DOC_NO,:REV_NO,:PROJECT_ID,:UNIT,:CONTRACTOR_ID,:REMARKS,:REMARKS_ROLE_S_R)");
                insert_remarks_ParamList.Add(":DOC_NO", str_doc_no);
                insert_remarks_ParamList.Add(":REV_NO", str_rev_no);
                insert_remarks_ParamList.Add(":PROJECT_ID", str_project_id);
                insert_remarks_ParamList.Add(":UNIT", str_unit);
                insert_remarks_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);
                insert_remarks_ParamList.Add(":REMARKS", str_remarks);
                insert_remarks_ParamList.Add(":REMARKS_ROLE_S_R", Constants.User_Role_RCM);
                int i = objDB.executeNonQuery(insert_remarks.ToString(), insert_remarks_ParamList);
                if (i > 0)
                {
                    // lbl_err.Text = "Remarks inserted Successfully!!";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Remarks inserted Successfully!!')</script>");
                }
                else
                {
                    // lbl_err.Text = "Error";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
                }
            }
            Fill_data();
        }
        catch (Exception ex)
        {
            lbl_err.Text = ex.ToString();
        }

    }
    protected void btn_rcm_Accept_Click(object sender, EventArgs e)
    {
        btn_rcm_edit.Visible = false;
        btn_rcm_save.Visible = false;
        btn_rcm_Reject.Visible = false;
        btn_rcm_Accept.Visible = false;
        string str_doc_no = "A545-100-OSA-1101";
        string str_rev_no = "0";
        string str_project_id = "A545";
        string str_unit = "100";
        string str_contractor_id = "C001";
        string User = Session["UserId"].ToString().Trim();
        StringBuilder update_insert = new StringBuilder();
        Dictionary<string, string> update_insert_ParamList = new Dictionary<string, string>();

        update_insert.Append("UPDATE OSA_MASTER SET RCM_ACCEPT_ON=sysdate , RCM_ACCEPT_FLAG=:RCM_ACCEPT_FLAG , RCM_ACCEPT_BY=:RCM_ACCEPT_BY");
        update_insert.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
        update_insert_ParamList.Add(":RCM_ACCEPT_FLAG", "1");
        update_insert_ParamList.Add(":RCM_ACCEPT_BY", User);
        //update_insert_ParamList.Add(":RCM_ACCEPT_BY", );
        //update_insert_ParamList.Add(":RCM_ACCEPT_ON", "sysdate");
        update_insert_ParamList.Add(":DOC_NO", str_doc_no);
        update_insert_ParamList.Add(":REV_NO", str_rev_no);
        update_insert_ParamList.Add(":PROJECT_ID", str_project_id);
        update_insert_ParamList.Add(":UNIT", str_unit);
        update_insert_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);

        int i = objDB.executeNonQuery(update_insert.ToString(), update_insert_ParamList);
        if (i > 0)
        {
            // lbl_err.Text = "Remarks updated Successfully!!";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert(' Accepted ')</script>");
        }
        else
        {
            //  lbl_err.Text = "Error";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
        }
    }
    protected void btn_rcm_Reject_Click(object sender, EventArgs e)
    {
        btn_rcm_Accept.Visible = false;
        btn_rcm_Reject.Visible = false;
        btn_rcm_edit.Visible = false;
        btn_rcm_save.Visible = false;
        string str_doc_no = "A545-100-OSA-1101";
        string str_rev_no = "0";
        string str_project_id = "A545";
        string str_unit = "100";
        string str_contractor_id = "C001";
        string User = Session["UserId"].ToString().Trim();
        StringBuilder update_insert = new StringBuilder();
        Dictionary<string, string> update_insert_ParamList = new Dictionary<string, string>();

        update_insert.Append("UPDATE OSA_MASTER SET RCM_REJECT_ON=sysdate , RCM_REJECT_FLAG=:RCM_REJECT_FLAG , RCM_REJECT_BY=:RCM_REJECT_BY");
        update_insert.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
        update_insert_ParamList.Add(":RCM_REJECT_FLAG", "1");
        update_insert_ParamList.Add(":RCM_REJECT_BY", User);
        update_insert_ParamList.Add(":DOC_NO", str_doc_no);
        update_insert_ParamList.Add(":REV_NO", str_rev_no);
        update_insert_ParamList.Add(":PROJECT_ID", str_project_id);
        update_insert_ParamList.Add(":UNIT", str_unit);
        update_insert_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);

        int i = objDB.executeNonQuery(update_insert.ToString(), update_insert_ParamList);
        if (i > 0)
        {
            // lbl_err.Text = "Remarks updated Successfully!!";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert(' REJECTED ')</script>");
        }
        else
        {
            //  lbl_err.Text = "Error";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
        }
    }
    protected void btn_so_Accept_click(object sender, EventArgs e)
    {
        string str_doc_no = "A545-100-OSA-1101";
        string str_rev_no = "0";
        string str_project_id = "A545";
        string str_unit = "100";
        string str_contractor_id = "C001";
        string User = Session["UserId"].ToString().Trim();
        StringBuilder accept_so_insert = new StringBuilder();
        Dictionary<string, string> accept_so_insert_ParamList = new Dictionary<string, string>();

        accept_so_insert.Append("UPDATE OSA_MASTER SET SO_ACCEPT_ON=sysdate , SO_ACCEPT_FLAG=:SO_ACCEPT_FLAG , SO_ACCEPT_BY=:SO_ACCEPT_BY");
        accept_so_insert.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
        accept_so_insert_ParamList.Add(":SO_ACCEPT_FLAG", "1");
        accept_so_insert_ParamList.Add(":SO_ACCEPT_BY", User);
        accept_so_insert_ParamList.Add(":DOC_NO", str_doc_no);
        accept_so_insert_ParamList.Add(":REV_NO", str_rev_no);
        accept_so_insert_ParamList.Add(":PROJECT_ID", str_project_id);
        accept_so_insert_ParamList.Add(":UNIT", str_unit);
        accept_so_insert_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);

        int i = objDB.executeNonQuery(accept_so_insert.ToString(), accept_so_insert_ParamList);
        if (i > 0)
        {
            // lbl_err.Text = "Remarks updated Successfully!!";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert(' ACCEPTED ')</script>");
        }
        else
        {
            //  lbl_err.Text = "Error";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
        }
    }
    protected void btn_so_Reject_click(object sender, EventArgs e)
    {
        string str_doc_no = "A545-100-OSA-1101";
        string str_rev_no = "0";
        string str_project_id = "A545";
        string str_unit = "100";
        string str_contractor_id = "C001";
        string User = Session["UserId"].ToString().Trim();
        StringBuilder reject_so_insert = new StringBuilder();
        Dictionary<string, string> reject_so_insert_ParamList = new Dictionary<string, string>();

        reject_so_insert.Append("UPDATE OSA_MASTER SET SO_REJECT_ON=sysdate, SO_REJECT_FLAG=:SO_REJECT_FLAG, SO_REJECT_BY=:SO_REJECT_BY");
        reject_so_insert.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
        reject_so_insert_ParamList.Add(":SO_REJECT_FLAG", "1");
        reject_so_insert_ParamList.Add(":SO_REJECT_BY", User);
        reject_so_insert_ParamList.Add(":DOC_NO", str_doc_no);
        reject_so_insert_ParamList.Add(":REV_NO", str_rev_no);
        reject_so_insert_ParamList.Add(":PROJECT_ID", str_project_id);
        reject_so_insert_ParamList.Add(":UNIT", str_unit);
        reject_so_insert_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);

        int i = objDB.executeNonQuery(reject_so_insert.ToString(), reject_so_insert_ParamList);
        if (i > 0)
        {
            // lbl_err.Text = "Remarks updated Successfully!!";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert(' REJECTED ')</script>");
        }
        else
        {
            //  lbl_err.Text = "Error";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
        }
    }
    protected void btn_Submit_so_Click(object sender, EventArgs e)
    {
        string str_doc_no = "A545-100-OSA-1101";
        string str_rev_no = "0";
        string str_project_id = "A545";
        string str_unit = "100";
        string str_contractor_id = "C001";
        string User = Session["UserId"].ToString().Trim();
        StringBuilder cont_submit = new StringBuilder();
        Dictionary<string, string> cont_submit_ParamList = new Dictionary<string, string>();

        cont_submit.Append("UPDATE OSA_MASTER SET CONT_SUBMIT_FLAG=:CONT_SUBMIT_FLAG,CONT_SUBMIT_BY=:CONT_SUBMIT_BY,CONT_SUBMIT_ON=sysdate");
        cont_submit.Append(" WHERE DOC_NO=:DOC_NO AND REV_NO=:REV_NO AND PROJECT_ID=:PROJECT_ID AND UNIT=:UNIT AND CONTRACTOR_ID=:CONTRACTOR_ID");
        cont_submit_ParamList.Add(":CONT_SUBMIT_FLAG", "1");
        cont_submit_ParamList.Add(":CONT_SUBMIT_BY", User);
        //  cont_submit_ParamList.Add(":CONT_SUBMIT_ON","sysdate");
        cont_submit_ParamList.Add(":DOC_NO", str_doc_no);
        cont_submit_ParamList.Add(":REV_NO", str_rev_no);
        cont_submit_ParamList.Add(":PROJECT_ID", str_project_id);
        cont_submit_ParamList.Add(":UNIT", str_unit);
        cont_submit_ParamList.Add(":CONTRACTOR_ID", str_contractor_id);

        int i = objDB.executeNonQuery(cont_submit.ToString(), cont_submit_ParamList);
        if (i > 0)
        {
            // lbl_err.Text = "Remarks updated Successfully!!";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert(' SUBMITTED ')</script>");
        }
        else
        {
            //  lbl_err.Text = "Error";
            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Error')</script>");
        }

    }
}
