<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarDetails.ascx.cs" Inherits="PPJWebsite.UserControls.CalendarDetails" %>
<table width="100%" runat="server" style="align-content: center">
    <tr runat="server" style="width: 900px">
        <td align="center" style="background-color: #c3c2c2; height: 20px; vertical-align: middle; text-align: left">

            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Jumlah Tiang Tersedia"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:DataList ID="dlCalendar" runat="server" BorderStyle="None" OnItemDataBound="dlCalendar_ItemDataBound" RepeatColumns="7" RepeatDirection="Horizontal" Width="100%">
                <HeaderTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" style="height: 20px" width="100%">
                        <tr>
                            <td align="center" width="33%">
                                <asp:Label ID="lblMiddle" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table runat="server" id="tblDay" border="0" cellpadding="0" cellspacing="0" style="height: 100px" width="130px">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Data") %>' Width="100%"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
</table>