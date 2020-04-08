<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EIL - Login Page</title>
    <link href="css/EILDesign.css" type="text/css" rel="stylesheet" />
    <link href="css/Site.css" type="text/css" rel="stylesheet" />
</head>
<body class="myLoginBody">
    <form id="form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table id="myTopBrandBannerTable" cellspacing="0" cellpadding="0">
                        <tr valign="middle" height="25">
                            <td id="myTopBrandBanner" nowrap align="right">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td nowrap align="right">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="top">
                <td width="100%">
                    <table id="myMiddleBannerTable" cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td width="100%">
                                <table id="myMiddleBrandBannerTable" cellspacing="0" cellpadding="0">
                                    <tr valign="middle" height="25">
                                        <td id="myMiddleBrandBanner" nowrap align="left">
                                            <span class="myLoginTopFrame" id="lblHeader">Observation of Safety Aspects (OSA) System</span></td>
                                        <td id="myMiddleBanner" align="right">
                                            <span class="myLoginTopFrame" id="Span1"></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td id="myMiddleBrandBannerBlank">&nbsp;</td>
                        </tr>
                    </table>
                    <table id="myBottomBannerTable" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <table id="myBottomBrandBannerTable" cellspacing="0" cellpadding="0">
                                    <tr valign="middle" height="25">
                                        <td id="myBottomBanner" align="right">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="Table5" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td valign="middle" align="left" colspan="2">
                    <img height="2" src="" border="0"></td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <img src="images/ref.jpg" width="100%" border="0"></td>
            </tr>
            <tr>
                <td valign="top" width="80%">
                    <p class="myLoginBodyText" align="left">
                        <br />


                        </td>
                <td valign="bottom" align="center">
                    <table class="myLoginText" id="TableBodyRight1" style="height: 100%" cellspacing="0"
                        cellpadding="0" width="100%">
                        <tr style="height: 20px">
                            <td class="myLoginTopLabel" valign="middle" align="center">Sign In
                            </td>
                        </tr>
                        <tr style="width: 100%; height: 60%">
                            <td valign="top">
                                <table id="TableBodyRight" style="height: 100%" cellspacing="0" cellpadding="0" width="100%">

                                    <tr>
                                        <td style="height: 60%" valign="top">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table id="TableLogin" style="height: 100%" cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td class="myLoginLabel" style="height: 31px">&nbsp;User ID
                                                        </td>
                                                        <td class="myLoginLabel" style="height: 31px">
                                                            <asp:TextBox ID="txtUserName" runat="server" MaxLength="4" TabIndex="1" CssClass="myLoginText"
                                                                Style="height: 14px; width: 100px;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="myLoginLabel" style="height: 31px">&nbsp;Password
                                                        </td>
                                                        <td class="myLoginLabel" style="height: 31px">
                                                            <asp:TextBox ID="txtPassword" TabIndex="2" runat="server" TextMode="Password" CssClass="myLoginText"
                                                                MaxLength="20" Style="height: 14px; width: 100px;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="myLoginMessage" colspan="2" align="center">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="myLoginMessage" colspan="2" align="center"></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr style="height: 5%; padding-top: 8px;">
                                        <td style="height: 11.52%" class="myLoginLabel">
                                            <div id="DIV1" align="center">
                                                <asp:Button ID="btnLogin" TabIndex="2" runat="server" CssClass="myLoginButton" Text="Login"
                                                    OnClick="btnLogin_Click" Font-Bold="true"></asp:Button>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr style="height: 5%">
                                        <td class="myLoginLabel" style="height: 10%" colspan="2"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="left">
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td colspan="2">
                    <hr style="width: 100%; height: 1px" width="100%" size="1">
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table border="0">
                        <tr>
                            <td align="left" colspan="2">
                                <table>
                                    <tr align="left">
                                        <td align="left" style="width: 200px" valign="top">
                                            <img height="35" src="Images/eil_logo_01.gif" />
                                        </td>
                                        <td align="left">
                                            <div class="myLoginInfo">
                                                Developed by : Information Technology Services,&nbsp;Engineers India Limited, New
                                            Delhi.
                                            </div>
                                            <p>
                                            </p>
                                            <div class="myLoginInfo">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table border="0">
                        <tr>
                            <td align="right" colspan="2">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="width: 100%; height: 1px" width="100%" size="1">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
