<%@ Page Title="" Language="C#" MasterPageFile="~/MenuPage.master" AutoEventWireup="true"
    CodeFile="osa_entry.aspx.cs" Inherits="osa_entry" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">
              // SCRIPT FOR THE MOUSE EVENT.
              function MouseEvents(objRef, evt) {
                  if (evt.type == "mouseover") {
                      objRef.style.cursor = 'pointer';
                      objRef.style.backgroundColor = "#EEEED1";
                  }
                  else {
                      if (evt.type == "mouseout") objRef.style.backgroundColor = "#FFF";
                  }
              }
    </script><asp:Label ID="Label39" runat="server" CssClass="myTopHeader" Width="100%" Text="Observation On Safety Aspects" BackColor="#CCCCFF">
    </asp:Label>
    <br />
    <asp:GridView ID="gvRisk" runat="server" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvRisk_RowDataBound" OnRowCancelingEdit="gvRisk_RowCancelingEdit" OnRowEditing="gvRisk_RowEditing"
        OnRowCommand="gvRisk_RowCommand" OnRowUpdating="gvRisk_RowUpdating" OnRowDeleting="gvRisk_RowDeleting" ShowFooter="false" OnSelectedIndexChanged="gvRisk_SelectedIndexChanged">
        <AlternatingRowStyle CssClass="myGridAlternatingItemStyle"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField HeaderText="Sr.No " ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="myGridTextItem" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Details of Lapses/Shortfalls/Hazards Identified">
                <ItemTemplate>
                    <asp:Label runat="server" ID="hazard" Text='<%# Bind("OBSV_DETAILS")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="hazard_edit" TextMode="MultiLine" Rows="3" Width="100%" Text='<%# Bind("OBSV_DETAILS")%>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox runat="server" ID="txt_hazard_footer" TextMode="MultiLine" Rows="3" Width="100%" Height="100%"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle Width="360px" HorizontalAlign="Left" CssClass="myGridTextItem" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Recommended Course of Action">
                <ItemTemplate>
                    <asp:Label runat="server" ID="suggestion" Text='<%# Bind("OBSV_RECOMMENDATION")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="suggestion_edit" TextMode="MultiLine" Rows="2" Width="100%" Text='<%# Bind("OBSV_RECOMMENDATION")%>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox runat="server" ID="txt_suggestion_footer" TextMode="MultiLine" Rows="3" Width="100%" Height="100%"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle Width="360px" HorizontalAlign="Left" CssClass="myGridTextItem" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Corrective Action Report by Contractor">
                <ItemTemplate>
                    <asp:Label runat="server" ID="contractor_report" Text='<%# Bind("OBSV_CORR_ACT")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="contractor_report_edit" TextMode="MultiLine" Rows="2" Width="100%" Text='<%# Bind("OBSV_CORR_ACT")%>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox runat="server" ID="contractor_report_footer" TextMode="MultiLine" Rows="3" Width="100%" Height="100%"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle Width="360px" HorizontalAlign="Left" CssClass="myGridTextItem" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:LinkButton ID="btn_add" runat="server" CommandName="Add" Text="Add observation" ForeColor="blue" Font-Size="X-Small"></asp:LinkButton>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="btn_edit" runat="server" CommandName="Edit" Text="Edit" ForeColor="blue" />
                    <asp:LinkButton ID="btn_Delete" runat="Server" CausesValidation="true" Text="Delete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete Selected Record?')"
                        ForeColor="blue"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btn_Update" runat="server" CommandName="Update" Text="Update" />
                    <asp:LinkButton ID="btn_cancel" runat="server" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="btn_Insert" runat="server" CausesValidation="True" CommandName="Insert"
                        Text="Insert" ToolTip="Insert Milestone desc" Width="100px"></asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnRisk_ftrCancel" CommandName="EmptyCancel" Text="Cancel" runat="server" />
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="myGridTextItem" />
                <FooterStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lbl_Obs_id" runat="server" Text='<%# Bind("OBSERVATION_ID") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle BackColor="#CCCCFF" />
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>No data available
                    </td>
                </tr>
            </table>
            <div id="hideDivDateRemarks" runat="server">
                <table class="myTable">
                    <tr>
                        <td colspan="2" class="myGridHeader">
                            <p>Enter Details of lapses/Shortfalls/Hazards</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ftr_lblHazards" runat="server" Text="Enter Details of lapses/Shortfalls/Hazards" />
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtEmpty_Hazards" runat="server" Rows="2" TextMode="MultiLine" Width="90%" OnTextChanged="txtEmpty_Hazards_TextChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="myGridHeader">
                            <p>Enter Recommended Course of Action</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ftr_lblSuggestion" runat="server" Text="Enter Recommended Course of Action" />
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtEmpty_Suggestion" runat="server" Rows="2" TextMode="MultiLine" Width="90%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:LinkButton ID="lnkbtnfootr_add" CommandName="EmptyInsert" CommandArgument="2_empty" Text="Insert" runat="server" OnClick="lnkbtnfootr_add_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </EmptyDataTemplate>
        <HeaderStyle HorizontalAlign="Center" CssClass="myGridHeader" />
        <RowStyle CssClass="myGridTextItem" Font-Size="Small" />

    </asp:GridView>
    <br />


    <asp:Label ID="lbl_err" runat="server" ForeColor="Red"></asp:Label>
    <asp:Panel ID="pnl_SafetyOfficer" runat="server" GroupingText="" Width="800px" Visible="true">
        <asp:Label ID="Label2" runat="server" Width="100%" CssClass="myTopHeader" Text="Safety Officer Remarks" BackColor="#CCCCFF"></asp:Label>
        <table border="1" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Suspension of Work Required till Resolution"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="rtbnYes" runat="server" Text="Yes" Value="Y" GroupName="suspension_work" OnCheckedChanged="rtbnYes_CheckedChanged" />
                    <asp:RadioButton ID="rtbnNo" runat="server" Text="No" Value="N" GroupName="suspension_work" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Time Allowed for Correction "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Time_allowed" runat="server" Width="100px" OnTextChanged="txt_Time_allowed_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="false" Width="100px">
                        <asp:ListItem Text="Select.." Value="0"></asp:ListItem>
                        <asp:ListItem Text="Days" Value="D"></asp:ListItem>
                        <asp:ListItem Text="Months" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Year" Value="Y"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSO_rem" runat="server" Text="Enter Remarks (If any)"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSO_Remarks" runat="server" TextMode="MultiLine" Rows="5" Width="500px" Enabled="false"></asp:TextBox>

                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Button ID="edit_remarks" runat="server" Text="Edit" ToolTip="Edit Remarks" Visible="true" OnClick="btn_edit_remarks" />
                    &nbsp &nbsp &nbsp &nbsp
                    <asp:Button ID="save_edit_remarks" runat="server" Text="OK" Visible="false" OnClick="btn_save_edit_remarks" />
                </td>
            </tr>--%>
            <tr align="center">
                <td colspan="2">
                    <asp:Button ID="edit_remarks" runat="server" Text="Edit" ToolTip="Edit Remarks" Visible="true" OnClick="btn_edit_remarks" />
                    <asp:Button ID="btn_SO_Save" runat="server" Text="Save" ToolTip="Save data and Remarks" Visible="false" OnClick="btn_SO_Save_Click" />
                    <asp:Button ID="btn_Submit_cont" runat="server" Text="Submit to Contractor" OnClick="btn_Submit_cont_Click" Visible="false" />
                    <asp:Button ID="btn_Submit_so" runat="server" Text="Submit to Safety Officer" Visible="false" OnClick="btn_Submit_so_Click" />

                </td>
            </tr>
            <tr align="center">
                <td colspan="2">
                    <asp:Button ID="btn_so_Accept" runat="server" Text="Approve" ToolTip="Approve observations and submit to RCM for approval" Visible="false" OnClick="btn_so_Accept_click" />
                    <asp:Button ID="btn_so_Reject" runat="server" Text="Reject" ToolTip="Reject observations and send remarks back to Contractor" Visible="false" OnClick="btn_so_Reject_click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnl_RCM" runat="server" Width="550px" Visible="true">
        <asp:Label ID="Label1" runat="server" Width="100%" CssClass="myTopHeader" Text="Verification of Resolution by Issuer/Area Coordinator/RCM(EIL)" BackColor="#CCCCFF"></asp:Label>
        <table border="1" width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblRcm_rem" runat="server" Text="Enter Remarks (If any)"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRCM_Remarks" runat="server" TextMode="MultiLine" Rows="5" Width="390px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr align="center">
                <td colspan="2">
                    <asp:Button ID="btn_rcm_edit" runat="server" Text="Edit" ToolTip="Edit Remarks" Visible="true" OnClick="btn_edit_rcm_remarks" />
                    <asp:Button ID="btn_rcm_save" runat="server" Text="Save" ToolTip="Save Remarks" Visible="true" OnClick="btn_rcm_Save_Click" />
                    <asp:Button ID="btn_rcm_Accept" runat="server" Text="Accept" Visible="true" OnClick="btn_rcm_Accept_Click" />
                    <asp:Button ID="btn_rcm_Reject" runat="server" Text="Reject" Visible="true" OnClick="btn_rcm_Reject_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
