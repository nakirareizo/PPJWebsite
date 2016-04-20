<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaftarJalan.aspx.cs" Inherits="PPJWebsite.Pages.DaftarJalan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                                        <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td align="right" colspan="2">
                                        <asp:Label ID="Label2" runat="server" Text="No Rujukan Jalan" />
                                        <asp:Label ID="lblNoRujJalan" runat="server" Text="REJ11231234"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label3" runat="server" Text="Jumlah Tiang" />
                                    </td>
                                    <td align="left" width="100px">
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
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
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label7" runat="server" Text="Jumlah Tiang Tersedia" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label8" runat="server" Text="Harga Kos Seunit" />

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100px">
                                        <asp:Label ID="Label6" runat="server" Text="Jumlah Arm" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label9" runat="server" Text="Harga Seunit" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="4">
                                        <asp:Button ID="Button1" runat="server" Text="Tambah Jalan" Width="100px" />
                                        <asp:Button ID="Button2" runat="server" Text="Kemaskini" Width="100px" />
                                        <asp:Button ID="Button3" runat="server" Text="Batal" Width="100px" />
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
                                        <asp:GridView Width="100%" ID="gvRoadList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No record found">
                                            <Columns>
                                                <asp:BoundField DataField="No" HeaderText="No" SortExpression="No" />
                                                <asp:BoundField DataField="Jalan" HeaderText="Jalan / Lokasi" SortExpression="Jalan" />
                                                <asp:BoundField DataField="JumTiang" HeaderText="Jumlah Tiang" SortExpression="JumTiang" />
                                                <asp:BoundField DataField="JumTiangRosak" HeaderText="Jumlah Tiang Rosak" SortExpression="JumTiangRosak" />
                                                <asp:BoundField DataField="JumTiangSedia" HeaderText="Jumlah Tiang Tersedia" SortExpression="JumTiangSedia" />
                                                <asp:BoundField DataField="JumArm" HeaderText="Jumlah Arm" SortExpression="JumArm" />
                                                <asp:BoundField DataField="Saiz" HeaderText="Saiz Gegantung" SortExpression="Saiz" />
                                                <asp:BoundField DataField="Harga" HeaderText="Harga Seunit" SortExpression="Harga" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkKemas" runat="server" />
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
