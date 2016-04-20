<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TambahJalanTersedia.aspx.cs" Inherits="PPJWebsite.Pages.TambahJalanTersedia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>--%>
    <script type="text/javascript">
        function CheckCountedTiang(evt) {
            var JumlahTiangToApply = document.getElementById("txtJumTiang").value
            var JumlahTiangAvailable = document.getElementById("hdAccumulateAvailableCounted").value
            if (JumlahTiangToApply > JumlahTiangAvailable) {
                alert("Anda tidak boleh tempah tiang lebih dari jumlah tersedia.Tiang Tersedia hanya " + JumlahTiangAvailable + ".");
                return false;
            }
        }
        function isDate(evt) {
            var fromDate = document.getElementById("txtTarikhMula").value
            var toDate = document.getElementById("txtTarikhTamat").value


            var dt1 = parseInt(fromDate.substring(0, 2));
            var mon1 = parseInt(fromDate.substring(3, 5));
            var yr1 = parseInt(fromDate.substring(6, 10));
            var date1 = new Date(yr1, mon1 - 1, dt1);

            var dt2 = parseInt(toDate.substring(0, 2));
            var mon2 = parseInt(toDate.substring(3, 5));
            var yr2 = parseInt(toDate.substring(6, 10));
            var date2 = new Date(yr2, mon2 - 1, dt2);

            var dateRegEx = null;
            dateRegEx = new RegExp(/^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/g);

            if (dateRegEx.test(fromDate)) {
            }
            else {
                alert("Anda masukan Tarikh format yang salah. Sila masukan semula.");
                return false;
            }
            dateRegEx = new RegExp(/^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/g);
            if (dateRegEx.test(toDate)) {
            }
            else {
                alert("Anda masukan Tarikh format yang salah. Sila masukan semula.");
                return false;
            }
            if (yr2 == yr1) {
                var CompareMonth = mon2 - mon1;
                if (mon1 == mon2) {
                    var stDate = new Date(fromDate);
                    var enDate = new Date(toDate);
                    var compDate = enDate - stDate;

                    if (compDate >= 0)
                        return true;
                    else {
                        alert("Tarikh Mula tidak boleh awal dari Tarikh Tamat. Sila masukan semula.");
                        return false;
                    }
                }
                if (CompareMonth < 0) {
                    alert("Tarikh Mula tidak boleh awal dari Tarikh Tamat. Sila masukan semula.");
                    return false;
                }

            }
            else {
                var stDate = new Date(fromDate);
                var enDate = new Date(toDate);
                var compDate = enDate - stDate;
                if (compDate >= 0)
                    return true;
                if (yr2 < yr1) {
                    alert("Tarikh Mula tidak boleh awal dari Tarikh Tamat. Sila masukan semula.");
                    return false;
                }
            }
        }
        function DoPostback() {
            __doPostBack("btnTempah", "OnClick");
        }
        function TextChange(evt) {
            var tbox = document.getElementById("txtJumTiang");
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                tbox.value = "";
                alert('Hanya nombor di benarkan.');
                return false;
            }
            return true;
        }
        function CalculateHarga(tBox) {
            var HargaSeunit = document.getElementById("hdHargaSeunit");
            var JumHarga = document.getElementById("txtJumHarga");
            var JumTiang = tBox.value;
            var Total = JumTiang * HargaSeunit.value;
            JumHarga.value = Total;

        }
        function validate() {
            var ddlJalan = document.getElementById("ddlJalan");
            var Jalan = ddlJalan.options[ddlJalan.selectedIndex].value;
            var hdMula = document.getElementById("hdMula");
            var hdTamat = document.getElementById("hdTamat");
            var txtTarikhMula = document.getElementById("txtTarikhMula");
            var txtTarikhTamat = document.getElementById("txtTarikhTamat");
            hdMula.value = txtTarikhMula.value;
            hdTamat.value = txtTarikhTamat.value;
            __doPostBack("txtTarikhTamat", "TextChanged");
        }
        function SetFocus() {
            var txtTarikhTamat = document.getElementById("txtTarikhTamat");
            txtTarikhTamat.focus();
        }
        //function CheckValues(e) {
        //    var txtTarikhTamat = document.getElementById("txtTarikhTamat");
        //    if (txtTarikhTamat.value == "") {
        //        alert("Sila masukan Tarik Tamat.");
        //        e.preventDefault();
        //        return false;
        //    }
        //}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table align="center" width="1005" cellspacing="1" border="1">
            <tr>
                <td>

                    <asp:Panel ID="pnlSearch" runat="server" HorizontalAlign="Left">
                        <table align="center" width="1000" cellspacing="1">
                            <tr>
                                <td align="left" colspan="4" style="background-color: #c3c2c2" class="auto-style1">

                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Tambah Jalan Tersedia"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="500px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLevel" runat="server" Text="Pilih Nama Jalan / Lokasi" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlJalan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJalan_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="100px">
                                    <asp:Label ID="Label18" runat="server" CssClass="lblBold" Text="Tarikh Mula"></asp:Label>
                                </td>
                                <td colspan="2" width="190px">
                                    <input type="hidden" id="hdTarikhMula" runat="server" />
                                    <asp:TextBox ID="txtTarikhMula" runat="server" Width="150px"
                                        SkinID="txtNormal"></asp:TextBox>

                                    <asp:CalendarExtender ID="txtTarikhMula_CalendarExtender" runat="server" PopupButtonID="imgbtnTarikhMula" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTarikhMula">
                                    </asp:CalendarExtender>
                                    <asp:ImageButton ID="imgbtnTarikhMula" runat="server"
                                        ImageAlign="AbsMiddle" ImageUrl="~/Images/Calendar.gif" Style="width: 16px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Jumlah Tiang"></asp:Label>
                                                <asp:TextBox ID="txtJumTiang" runat="server" onblur="CalculateHarga(this)" onKeyUp="TextChange(this)"></asp:TextBox>
                                            </td>

                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Jumlah Harga"></asp:Label>
                                                <asp:TextBox ID="txtJumHarga" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" CssClass="lblBold" Text="Tarikh Tamat"></asp:Label>
                                </td>
                                <td>
                                    <%-- <asp:TextBox ID="txtTarikhTamat" runat="server" Width="150px"
                                                SkinID="txtNormal" onblur="validate()" OnTextChanged="txtTarikhTamat_TextChanged" ></asp:TextBox>--%>
                                    <asp:TextBox ID="txtTarikhTamat" runat="server" Width="150px"
                                        SkinID="txtNormal"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtTarikhTamat_CalendarExtender1" runat="server" PopupButtonID="imgbtnTarikhTamat"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTarikhTamat">
                                    </asp:CalendarExtender>
                                    <asp:ImageButton ID="imgbtnTarikhTamat" runat="server" onblur="SetFocus();" ImageAlign="AbsMiddle" ImageUrl="~/Images/Calendar.gif" Style="width: 16px" />
                                    <asp:Button ID="btnCarian" runat="server" Text="Cari" OnClientClick="return isDate(this)" Width="70px" OnClick="btnCarian_Click" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnTempah" runat="server" Text="Tambah Jalan" OnClientClick="CheckCountedTiang(this)" OnClick="btnTempah_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlCalendar" runat="server" HorizontalAlign="Left">
                        <table width="1000" cellspacing="1">
                            <tr>
                                <td align="left" style="background-color: #c3c2c2">

                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Maklumat Jalan/Lokasi"></asp:Label>
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
                            <tr runat="server" id="trTitle" visible="false">
                                <td align="center" style="background-color: #c3c2c2; height: 20px; vertical-align: middle; text-align: left">

                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Jumlah Tiang Tersedia"></asp:Label>
                                </td>
                            </tr>
                            <%--         <tr>
                                        <td align="center">

                                            <asp:PlaceHolder runat="server" ID="plc"></asp:PlaceHolder>
                                        </td>
                                    </tr>--%>
                            <tr id="cldr1" runat="server" visible="false">
                                <td align="center">
                                    <asp:Calendar ID="Calendar1" runat="server" BorderWidth="1px" Width="100%" DayNameFormat="Short" ShowNextPrevMonth="false" NextPrevFormat="FullMonth" ShowGridLines="True">
                                        <TitleStyle Font-Size="9pt" Font-Bold="True" BackColor="White"></TitleStyle>
                                        <SelectedDayStyle BorderColor="Black" />
                                    </asp:Calendar>
                                </td>
                            </tr>
                            <tr id="cldr2" runat="server" visible="false">
                                <td align="center">
                                    <asp:Calendar ID="Calendar2" runat="server" BorderWidth="1px" Width="100%" DayNameFormat="Short" ShowNextPrevMonth="false" NextPrevFormat="FullMonth" ShowGridLines="True">
                                        <TitleStyle Font-Size="9pt" Font-Bold="True" BackColor="White"></TitleStyle>
                                        <SelectedDayStyle BorderColor="Black" />
                                    </asp:Calendar>
                                </td>
                            </tr>
                            <tr id="cldr3" runat="server" visible="false">
                                <td align="center">
                                    <asp:Calendar ID="Calendar3" runat="server" BorderWidth="1px" Width="100%" DayNameFormat="Short" ShowNextPrevMonth="false" NextPrevFormat="FullMonth" ShowGridLines="True">
                                        <TitleStyle Font-Size="9pt" Font-Bold="True" BackColor="White"></TitleStyle>
                                        <SelectedDayStyle BorderColor="Black" />
                                    </asp:Calendar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdHargaSeunit" runat="server" />
                                    <asp:HiddenField ID="hdMula" runat="server" />
                                    <asp:HiddenField ID="hdTamat" runat="server" />
                                    <asp:HiddenField ID="hdAccumulateAvailableCounted" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
