<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="WebApp.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="min-height: 80vh;">
        <div class="card shadow-lg p-4 rounded-4" style="width: 100%; max-width: 450px;">
            <div class="card-body text-center">
                <h2 class="card-title fw-bold text-primary mb-4">Tp Promo web</h2>
                <p class="text-muted mb-4">Ingrese el c&oacute;digo de su voucher para acceder al cat&aacute;logo de premios.</p>

                <div class="mb-4 text-start">
                    <label class="form-label fw-semibold">C&oacute;digo del Voucher</label>
                    <asp:TextBox ID="txtVoucher" runat="server" AutoPostBack="true" OnTextChanged="txtVoucher_TextChanged" CssClass="form-control form-control-lg border-primary" placeholder="Ingrese cup&oacute;n..."></asp:TextBox>
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-1 d-block small fw-bold"></asp:Label>
                </div>

                <asp:Button ID="btnVoucher" CssClass="btn btn-primary btn-lg w-100 rounded-pill fw-bold shadow-sm" OnClick="btnVoucher_Click" runat="server" Text="Canjear" />
</asp:Content>
