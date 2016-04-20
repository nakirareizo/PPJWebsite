<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="PPJWebsite.Pages.Landing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center" width="1000" cellspacing="1">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblTittle" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#3f3f3f" Font-Underline="True" Text="Welcome to Perbadanan Putrajaya Landing Page"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Perbadanan_Putrajaya_Logo.png" Width="500px" Height="500px" />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="auto-style1">
                        <asp:LinkButton ID="lnkbtnDaftarJalan" runat="server" OnClick="lnkbtnDaftarJalan_Click">Daftar Jalan </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lnkbtnSenaraiJalan" runat="server" OnClick="lnkbtnSenaraiJalan_Click">Senarai Semua Jalan / Lokasi </asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="lnkbtnJalanCarianTersedia" runat="server" OnClick="lnkbtnJalanCarianTersedia_Click">Carian Jalan Tersedia</asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lnkbtnTambahJalanTersedia" runat="server" OnClick="lnkbtnTambahJalanTersedia_Click">Tambah Jalan Tersedia</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>