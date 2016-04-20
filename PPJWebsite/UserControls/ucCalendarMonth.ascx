<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCalendarMonth.ascx.cs" Inherits="PPJWebsite.UserControls.ucCalendarMonth" %>
<table width="100%" runat="server" style="align-content: center">
    <tr>
        <td align="center" colspan="2">
            <asp:Calendar ID="Calendar1" runat="server" BorderWidth="1px" OnDayRender="Calendar1_DayRender" Width="100%" DayNameFormat="Short" ShowNextPrevMonth="false" NextPrevFormat="FullMonth" ShowGridLines="True">
                <TitleStyle Font-Size="9pt" Font-Bold="True" BackColor="White"></TitleStyle>
                <SelectedDayStyle BorderColor="Black" />
            </asp:Calendar>
        </td>
    </tr>
</table>