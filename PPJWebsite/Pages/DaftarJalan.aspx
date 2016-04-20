<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaftarJalan.aspx.cs" Inherits="PPJWebsite.Pages.DaftarJalan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function isFloatNumber(evt) {
            var tbox = document.getElementById(evt.id);
            var pattern = /^-?[0-9]+(.[0-9]{1,2})?$/;
            if (tbox.value.match(pattern) == null) {
                tbox.value = "";
                alert('Nombor dan titik perpuluhan hanya dibenarkan.');
                return false;
            }
        }
        function TextChange(evt) {
            var tbox = document.getElementById(evt.id);
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                tbox.value = "";
                alert('Hanya nombor di benarkan.');
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .auto-style2 {
            height: 30px;
        }

        .auto-style3 {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table align="center" width="1005" cellspacing="1" border="1">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table align="center" width="1000" cellspacing="1">
                                <tr>
                                    <td align="left" colspan="4" style="background-color: #c3c2c2">

                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Daftar / Kemaskini Jalan / Lokasi"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblLevel" runat="server" Text="Nama Jalan / Lokasi" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNamaJalan" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td align="right" colspan="2">
                                        <asp:Label ID="lblNoRujJalan" runat="server" Text="No Rujukan Jalan" Visible="false" />
                                        <asp:Label ID="lblNoRujJalanVal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label3" runat="server" Text="Jumlah Tiang" />
                                    </td>
                                    <td align="left" width="100px">
                                        <asp:TextBox ID="txtJumlahTiang" runat="server" onKeyUp="TextChange(this)"></asp:TextBox>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="Label4" runat="server" Text="Saiz Gegantung" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label5" runat="server" Text="Jumlah Tiang Rosak" />
                                    </td>
                                    <td align="left" width="100px">
                                        <asp:TextBox ID="txtJumTiangRosak" runat="server" onKeyUp="TextChange(this)"></asp:TextBox>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="txtSaizGegantung" Width="250px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label7" runat="server" Text="Jumlah Tiang Tersedia" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtJumTiangTersedia" runat="server" onKeyUp="TextChange(this)"></asp:TextBox>
                                    </td>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label8" runat="server" Text="Harga Kos Seunit" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtKosSeunit" runat="server" ></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revNumber" runat="server" ControlToValidate="txtKosSeunit"
           ErrorMessage="Hanya nombor dan 2 titik perpuluhan sahaja dibenarkan." ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label6" runat="server" Text="Jumlah Arm" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtJumArm" runat="server" onKeyUp="TextChange(this)"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label9" runat="server" Text="Harga Seunit" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtHargaSeuinit" runat="server" ></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHargaSeuinit"
           ErrorMessage="Hanya nombor dan 2 titik perpuluhan sahaja dibenarkan." ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="4">
                                        <asp:Button ID="btnTamJalan" runat="server" Text="Tambah Jalan" Width="100px" OnClick="btnTamJalan_Click" />
                                        <asp:Button ID="btnKemaskini" runat="server" Text="Kemaskini" Width="100px" OnClick="btnKemaskini_Click" />
                                        <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="100px" OnClick="btnBatal_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table align="center" width="1000" cellspacing="1">
                                <tr>
                                    <td align="left" style="background-color: #c3c2c2">

                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Maklumat Jalan / Lokasi"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView Width="100%" ID="gvRoadList" runat="server" AllowPaging="True" OnRowCreated="gvRoadList_RowCreated" OnRowCommand="gvRoadList_RowCommand"
                                            DataKeyNames="NoRujukanJalan" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No record found">
                                            <Columns>
                                                <asp:BoundField DataField="NoRujukanJalan" HeaderText="NoRujukanJalan" Visible="false" />
                                                <asp:BoundField DataField="No" HeaderText="No" SortExpression="No" />
                                                <asp:BoundField DataField="NamaJalan" HeaderText="Jalan / Lokasi" SortExpression="NamaJalan" />
                                                <asp:BoundField DataField="JumlahTiang" HeaderText="Jumlah Tiang" SortExpression="JumTiang" />
                                                <asp:BoundField DataField="Rosak" HeaderText="Jumlah Tiang Rosak" SortExpression="JumTiangRosak" />
                                                <asp:BoundField DataField="Tersedia" HeaderText="Jumlah Tiang Tersedia" SortExpression="JumTiangSedia" />
                                                <asp:BoundField DataField="JumlahArm" HeaderText="Jumlah Arm" SortExpression="JumArm" />
                                                <asp:BoundField DataField="SaizGegantung" HeaderText="Saiz Gegantung" SortExpression="Saiz" />
                                                <asp:BoundField DataField="HargaSeunit" HeaderText="Harga Seunit" SortExpression="Harga" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnView" runat="server">Kemaskini</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>