<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vendedor.aspx.cs" Inherits="ComercioWeb.Vendedor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Configuraciones de vendedor</title>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./Estilos.css" />
</head>
<body>
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
                                    <%if (Usuario.TipoUsuario.ID_Tipo < 3)
                                        {%>
                                    <a class="dropdown-item" href="Vendedor.aspx">Configuraciones de vendedor</a>
                                    <a class="dropdown-item" href="Controlventasystock.aspx">Control de ventas y stock</a>
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
                <div class="row ColumnaVendedor" style="margin-top:-15px;">
                    <div class="col">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <h3 style="text-align: left !important; margin-left: 140px;">Nueva categoría</h3>
                                <asp:TextBox ID="txtNuevaCategoria" AutoPostBack="true" placeholder="Ingrese la nueva categoría." CssClass="form-control" runat="server" OnTextChanged="txtNuevaCategoria_TextChanged" />
                                <asp:Label ID="lblCategoria" runat="server" Text="Categoría inválida." Visible="false"></asp:Label>
                                <asp:Label ID="lblCategoriaAgregada" runat="server" Text="Categoría fue agregada correctamente." Visible="false"></asp:Label>
                                <div style="margin: 15px 0px 15px 135px;">
                                    <asp:Button ID="btnAgregarCategoria" Text="Agregar nueva categoría" CssClass="btn btn-dark BotonAgregar" runat="server" OnClick="btnAgregarCategoria_Click" />
                                </div>
                                <div class="LineaPunteada"></div>
                                <div class="row" style="margin-bottom: 15px;">
                                    <div class="col">
                                        <h3 style="text-align: left !important; width: 300px; margin-left: 120px;">Eliminar categorías</h3>
                                        <asp:DropDownList ID="ListaCategorias" AutoPostBack="true" CssClass="form-control ListaDesplegable" runat="server" OnSelectedIndexChanged="ListaCategorias_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col" style="margin-left: 50px; margin-top: 52px;">
                                        <asp:Button ID="btnEliminarCategoria" Text="Eliminar seleccionado" CssClass="btn btn-dark BotonAgregar" runat="server" OnClick="btnEliminarCategoria_Click" />
                                        <asp:Label ID="lblCategoriaEliminada" Text="La categoría ha sido eliminada." Visible="false" runat="server" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="LineaPunteada"></div>
                <div class="row ColumnaVendedor" style="margin-top:-15px;">
                    <div class="col">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <h3 style="text-align: left !important; margin-left: 155px;">Nueva marca</h3>
                                <asp:TextBox ID="txtNuevaMarca" AutoPostBack="true" placeholder="Ingrese la nueva marca." CssClass="form-control" runat="server" OnTextChanged="txtNuevaMarca_TextChanged" />
                                <asp:Label ID="lblMarca" runat="server" Text="Marca inválida." Visible="false"></asp:Label>
                                <asp:Label ID="lblMarcaAgregada" runat="server" Text="Marca fue agregada correctamente." Visible="false"></asp:Label>
                                <div style="margin: 15px 0px 15px 145px;">
                                    <asp:Button ID="btnAgregarMarca" Text="Agregar nueva marca" CssClass="btn btn-dark BotonAgregar" runat="server" OnClick="btnAgregarMarca_Click" />
                                </div>
                                <div class="LineaPunteada"></div>
                                <div class="row" style="margin-bottom: 15px;">
                                    <div class="col">
                                        <h3 style="text-align: left !important; width: 300px; margin-left: 140px;">Eliminar marcas</h3>
                                        <asp:DropDownList ID="ListaMarcas" AutoPostBack="true" CssClass="form-control ListaDesplegable" runat="server" OnSelectedIndexChanged="ListaMarcas_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col" style="margin-left: 50px; margin-top: 52px;">
                                        <asp:Button ID="btnEliminarMarca" Text="Eliminar seleccionado" CssClass="btn btn-dark BotonAgregar" runat="server" OnClick="btnEliminarMarca_Click" />
                                        <asp:Label ID="lblMarcaEliminada" Text="La marca ha sido eliminada correctamente." Visible="false" runat="server" />
                                    </div>
                                </div>
                                <div class="LineaPunteada"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row" style="padding-bottom:20px;">
                    <div class="col" style="text-align:center;">
                        <h3 style="padding-bottom:10px;">Nuevo artículo</h3>
                        <a class="btn btn-dark BotonAgregar" href="AgregarArticulo.aspx">Ir a menú de nuevo artículo</a>
                    </div>
                </div>
                <div class="LineaPunteada"></div>
                <div class="row">
                    <div class="col" style="text-align:center;">
                        <h3 style="padding-bottom:10px;">Modificar artículos</h3>
                        <a class="btn btn-dark BotonAgregar" href="ModificarArticulo.aspx">Ir a menú de modificar artículo</a>
                    </div>
                </div>
                <div class="LineaPunteada"></div>
                <div class="row">
                    <div class="col" style="text-align:center;">
                        <h3 style="padding-bottom:10px;">Control de ventas y stock</h3>
                        <a class="btn btn-dark BotonAgregar" href="Controlventasystock.aspx">Ir al control de ventas y stock</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
