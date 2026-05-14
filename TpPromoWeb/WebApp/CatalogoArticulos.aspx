<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CatalogoArticulos.aspx.cs" Inherits="WebApp.CatalogoArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-5">
        <div class="row mb-5 text-center">
            <div class="col-12">
                <h1 class="display-5 fw-bold text-primary">Cat&aacute;logo de Art&iacute;culos</h1>
                <p class="lead text-muted">Seleccione el art&iacute;culo que desea canjear.</p>
                <h5><asp:Label ID="lbVoucher" runat="server" CssClass="badge bg-success shadow-sm p-2 px-3 rounded-pill"></asp:Label></h5>
            </div>
        </div>

        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            <% foreach (Dominio.Articulo art in listaArticulos) { %>
            <div class="col">
                <div class="card h-100 shadow-sm border-0 rounded-4">
                    <img src="<%: art.Imagenes != null && art.Imagenes.Count > 0 ? art.Imagenes[0].ImagenUrl : "https://via.placeholder.com/300x200?text=Sin+Imagen" %>" 
                         class="card-img-top rounded-top-4" 
                         alt="<%: art.Nombre %>" 
                         style="object-fit: contain; height: 250px; padding: 1.5rem;" 
                         onerror="this.src='https://via.placeholder.com/300x200?text=Sin+Imagen';"/>
                    <div class="card-body d-flex flex-column text-center bg-light rounded-bottom-4">
                        <h5 class="card-title fw-bold text-dark"><%: art.Nombre %></h5>
                        <p class="card-text text-secondary flex-grow-1"><%: art.Descripcion %></p>
                        
                        <asp:Button ID="btnElegir" runat="server" CssClass="btn btn-outline-primary mt-3 w-100 rounded-pill fw-semibold" Text="Elegir Art&iacute;culo" />
                    </div>
                </div>
            </div>
            <% } %>
        </div>
    </div>


</asp:Content>
