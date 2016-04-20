<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarianJalanTersedia.aspx.cs" Inherits="PPJWebsite.Pages.CarianJalanTersedia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
    <script type="text/javascript">
        function onCalendarShown() {

            var cal = $find("cldrDetails");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function onCalendarHidden() {
            var cal = $find("cldrDetails");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("cldrDetails");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
            //BindGridview();
            //var SelectedRoad = document.getElementById("ddlJalan");
            //var text = SelectedRoad.options[SelectedRoad.selectedIndex].text;
            //var SelectedMonth = document.getElementById("txtBulan");
            //var combinevalue = SelectedRoad.value + " " + SelectedMonth.value;
            //PageMethods.ShowRoadInfo(combinevalue, greetSuccess, greetFailed);
            //function greetSuccess(res) {
            //}
            //function greetFailed(res) {
            //}
            //jQuery.ajax({
            //    url: 'CarianJalanTersedia.aspx/ShowRoadInfo',
            //    type: "POST",
            //    dataType: "json",
            //    contentType: "application/json; charset=utf-8",
            //    success: function (data) {
            //        var result = data.d.result;
            //        $('#yourDropDownID')[0].selectedIndex = result;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <%--        <telerik:RadScriptManager ID="ScriptManager1" runat="server"
        EnableTheming="True">
    </telerik:RadScriptManager>--%>
        <table align="center" width="1005" cellspacing="1" border="1">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlSearch" runat="server" HorizontalAlign="Left">
                                <table align="center" width="1000" cellspacing="1">
                                    <tr>
                                        <td align="left" colspan="3" style="background-color: #c3c2c2" class="auto-style1">

                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Carian Jalan Tersedia"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="200px">
                                            <asp:Label ID="lblLevel" runat="server" Text="Pilih Nama Jalan / Lokasi" />
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlJalan" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" CssClass="lblBold" Text="Pilih Bulan"></asp:Label>
                                            <input type="hidden" id="hdBulan" runat="server" />
                                            <asp:TextBox ID="txtBulan" runat="server" Width="150px"
                                                SkinID="txtNormal"></asp:TextBox>

                                            <asp:CalendarExtender ID="txtBulan_CalendarExtender" runat="server" PopupButtonID="imgbtnBulan" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" BehaviorID="cldrDetails"
                                                Enabled="True" Format="MMM yyyy" TargetControlID="txtBulan">
                                            </asp:CalendarExtender>
                                            <asp:ImageButton ID="imgbtnBulan" runat="server"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Images/Calendar.gif" Style="width: 16px" />

                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlCalendar" runat="server" HorizontalAlign="Left">
                                <table align="center" width="1000" cellspacing="1">
                                    <tr>
                                        <td align="left" style="background-color: #c3c2c2">

                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Jumlah Jalan Tersedia"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView Width="100%" ID="gvRoadList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No record found">
                                                <Columns>
                                                    <asp:BoundField DataField="No" HeaderText="No" />
                                                    <asp:BoundField DataField="NamaJalan" HeaderText="Jalan / Lokasi" />
                                                    <asp:BoundField DataField="JumlahTiang" HeaderText="Jumlah Tiang" />
                                                    <asp:BoundField DataField="Rosak" HeaderText="Jumlah Tiang Rosak" />
                                                    <asp:BoundField DataField="Tersedia" HeaderText="Jumlah Tiang Tersedia" />
                                                    <asp:BoundField DataField="JumlahArm" HeaderText="Jumlah Arm" />
                                                    <asp:BoundField DataField="SaizGegantung" HeaderText="Saiz Gegantung" />
                                                    <asp:BoundField DataField="HargaSeunit" HeaderText="Harga Seunit" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr id="trCldr1" runat="server" visible="false">
                                        <td align="left" style="background-color: #c3c2c2">

                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Jumlah Tiang Tersedia"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trCldr2" runat="server" visible="false">

                                        <td align="center">
                                            <asp:Calendar ID="Calendar1" runat="server" BorderWidth="1px" Width="100%" DayNameFormat="Short" ShowNextPrevMonth="false" NextPrevFormat="FullMonth" ShowGridLines="True">
                                                <TitleStyle Font-Size="9pt" Font-Bold="True" BackColor="White"></TitleStyle>
                                                <SelectedDayStyle BorderColor="Black" BackColor="" />
                                            </asp:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>