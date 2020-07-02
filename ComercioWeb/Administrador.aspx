﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="ComercioWeb.Administrador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Configuraciones de administrador</title>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./Estilos.css" />
</head>
<body class="Fondo">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark Barra">
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link" href="Index.aspx">Inicio</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Catalogo.aspx">Catálogo</a>
                        </li>
                        <li class="nav-item dropdown">
                            <div>
                                <a class="nav-link dropdown-toggle active Activo" href="#" id="dropdownUsuarios" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Usuarios</a>
                                <div class="dropdown-menu" aria-labelledby="dropdownUsuarios">
                                    <%if (Usuario.TipoUsuario.ID_Tipo == 1)
                                        {%>
                                    <a class="dropdown-item" href="Administrador.aspx">Configuraciones de administrador</a>
                                    <%}%>
                                    <%if (Usuario.TipoUsuario.ID_Tipo == 2 || Usuario.TipoUsuario.ID_Tipo == 1)
                                        {%>
                                    <a class="dropdown-item" href="Vendedor.aspx">Configuraciones de vendedor</a>
                                    <%}%>
                                    <%if (Usuario.TipoUsuario.ID_Tipo == 3)
                                        {%>
                                    <a class="dropdown-item" href="MiUsuario.aspx">Mi usuario</a>
                                    <%}%>
                                    <a class="dropdown-item" href="ModificarUsuario.aspx">Modificar datos personales</a>
                                    <a class="dropdown-item" href="Index.aspx?logout=true">Cerrar Sesión</a>
                                </div>
                            </div>
                        </li>
                        <li class="nav-item" style="width: 200px;">
                            <%if (Carrito.ListaElementos.Count() != 0)
                                {%>
                            <a class="nav-link" href="Carrito.aspx">Mi carrito (<%=Carrito.Cantidad()%>) - $<%=Carrito.PrecioTotal()%></a>
                            <%}
                                else
                                {%>
                            <a class="nav-link" href="Carrito.aspx">Mi carrito (<%=Carrito.Cantidad()%>)</a>
                            <%}%>
                        </li>
                        <li>
                            <%if (HayUsuarioActivo)
                                {%>
                            <a class="nav-link" style="padding-left: 575px;">Usuario: <%=Usuario.Nombres%> <%=Usuario.Apellidos%></a>
                            <%}%>
                        </li>
                    </ul>
                </div>
            </nav>
            <div class="jumbotron CentrarJumbo">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col">
                                <h3>Administración de usuarios</h3>
                                <asp:DropDownList ID="ListaUsuarios" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListaUsuarios_SelectedIndexChanged"></asp:DropDownList>
                                <asp:Label ID="lblBlanqueo" Text="Se ha blanqueado el password del usuario a 123" Visible="false" runat="server" />
                                <div style="text-align: center; padding-top: 10px;">
                                    <asp:Button ID="btnBlanquearPassword" Text="Blanquear password" CssClass="btn btn-dark BotonAgregar" runat="server" OnClick="btnBlanquearPassword_Click" />
                                    <asp:Button ID="btnActivar" Text="Activar/Desactivar" CssClass="btn btn-dark BotonAgregar" runat="server" OnClick="btnActivar_Click" />
                                </div>
                            </div>
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblNombre" Text="Nombre" runat="server" />
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblApellido" Text="Apellido" runat="server" />
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblDNI" Text="DNI" runat="server" />
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblEstado" Text="Estado" runat="server" />
                        </div>
                        <div style="text-align: center; padding-bottom: 10px;">
                            <asp:Label ID="lblTipo" Text="Tipo" runat="server" />
                        </div>
                        <div class="LineaPunteada"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col">
                                <asp:DropDownList ID="ListaArticulos" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListaArticulos_SelectedIndexChanged"></asp:DropDownList>
                                <div style="text-align:center;padding-top:10px;padding-bottom:10px;">
                                    <asp:Button ID="btnActivarArticulo" Text="Activar/Desactivar" CssClass="btn btn-dark BotonAgregar" runat="server" OnClick="btnActivarArticulo_Click" />
                                </div>
                            </div>
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblID_Articulo" Text="ID" runat="server" />
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblNombreArticulo" Text="Nombre" runat="server" />
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblMarca" Text="Marca" runat="server" />
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblDescripcion" Text="Descripcion" runat="server" />
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblEstadoArticulo" Text="Estado" runat="server" />
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="lblPrecio" Text="Precio" runat="server" />
                        </div>
                        <div style="text-align: center; padding-bottom: 10px;">
                            <asp:Label ID="lblStock" Text="Stock" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
