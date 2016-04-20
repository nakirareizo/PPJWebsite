<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SenaraiJalan.aspx.cs" Inherits="PPJWebsite.Pages.SenaraiJalan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                                    <td align="left" colspan="2" style="background-color: #c3c2c2">

                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#000000" Text="Senarai Semua Jalan / Lokasi"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100px">
                                        <asp:Label ID="lblLevel" runat="server" Text="Jalan / Lokasi" />
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlJalan" runat="server">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnSearch" runat="server" Text="Carian" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
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

                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Label ID="lblRecord" runat="server"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="Rekod"></asp:Label>
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