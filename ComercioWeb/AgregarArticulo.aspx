<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarArticulo.aspx.cs" Inherits="ComercioWeb.AgregarArticulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Agregar artículo</title>
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
            <div style="text-align:center;">
                <asp:Label ID="lblAgregadoCorrectamente" Text="El artículo se ha cargado correctamente." Visible="false" runat="server" />
            </div>
            <div class="container" style="margin: auto">
                <div class="jumbotron CentrarJumbo">
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col">
                            <h3>Nombre del artículo</h3>
                            <asp:TextBox ID="txtNombre" CssClass="form-control" MaxLength="150" placeholder="Ingrese el nombre del artículo" runat="server" OnTextChanged="txtNombre_TextChanged" />
                            <asp:Label ID="lblNombreError" Text="El nombre ingresado es inválido." Visible="false" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col">
                            <h3>Descripción</h3>
                            <asp:TextBox ID="txtDescripcion" CssClass="form-control" MaxLength="150" placeholder="Ingrese la descripción del artículo" runat="server" OnTextChanged="txtDescripcion_TextChanged" />
                            <asp:Label ID="lblDescripcion" Text="La descripción ingresada es inválida." Visible="false" runat="server" />
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col">
                                    <h3>Precio</h3>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 10px;">
                                <div class="row">
                                    <div class="col" style="margin-right:-165px;margin-left:110px;">
                                        <h3 style="text-align:left !important;">$</h3>
                                    </div>
                                    <div class="col" style="padding-top:10px;width:400px;margin-left:10px;">
                                        <asp:TextBox ID="txtPrecioEntero" AutoPostBack="true" Text="0" MaxLength="15" CssClass="form-control" runat="server" OnTextChanged="txtPrecioEntero_TextChanged" />
                                    </div>
                                    <div class="col" style="margin-left:-22px;padding-top:8px;">
                                        <h3 style="text-align: left !important;">,</h3>
                                    </div>
                                    <div class="col" style="margin-left:-170px;padding-top:10px;">
                                        <asp:TextBox ID="txtPrecioDecimales" AutoPostBack="true" Text="00" MaxLength="3" CssClass="form-control" runat="server" OnTextChanged="txtPrecioDecimales_TextChanged" />
                                    </div>
                                </div>
                                <div>
                                    <asp:Label ID="lblPrecio" Text="El precio es inválido." Visible="false" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col">
                            <h3>Marca</h3>
                            <asp:DropDownList ID="ListaMarcas" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="LineaPunteada"></div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row" style="margin-bottom: 10px;">
                                <div class="col">
                                    <h3>Categorías</h3>
                                    <asp:DropDownList ID="ListaCategorias" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListaCategorias_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblAgregarError" Text="Categoría ya ha sido agregada." Visible="false" runat="server" />
                                    <asp:Label ID="lblAgregar" Text="Categoría ha sido agregada." Visible="false" runat="server" />
                                </div>
                            </div>
                            <div style="text-align: center; margin-bottom: 10px;">
                                <asp:Button ID="btnAgregarCategoria" Text="Agregar categoría al artículo" CssClass="btn btn-success BotonAgregar" runat="server" OnClick="btnAgregarCategoria_Click" />
                            </div>
                            <div class="row" style="margin-bottom: 10px;">
                                <div class="col">
                                    <h3>Lista de categorías agregadas</h3>
                                    <asp:DropDownList ID="ListaAgregados" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListaAgregados_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <asp:Label ID="lblEliminar" Text="Categoría ha sido eliminada." Visible="false" runat="server" />
                            <asp:Label ID="lblErrorCategoria" Text="Se debe cargar como mínimo una categoría para el artículo." Visible="false" runat="server" />
                            <div style="text-align: center; margin-bottom: 10px;">
                                <asp:Button ID="btnEliminarCategoria" Text="Eliminar categoría del artículo" CssClass="btn btn-success BotonAgregar" runat="server" OnClick="btnEliminarCategoria_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="LineaPunteada"></div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row" style="margin-bottom: 10px;">
                                <div class="col">
                                    <h3>Imagen</h3>
                                    <asp:TextBox ID="txtURL" MaxLength="1000" placeholder="Ingrese la URL de la imagen" CssClass="form-control" runat="server" OnTextChanged="txtURL_TextChanged" />
                                    <div style="text-align: center; padding-top: 10px;">
                                        <asp:Label ID="lblURL" Text="La dirección URL ingresada es inválida." Visible="false" runat="server"></asp:Label>
                                        <asp:Button ID="btnVistaPrevia" Text="Cargar vista previa" CssClass="btn btn-success BotonAgregar" runat="server" OnClick="btnVistaPrevia_Click" />
                                    </div>
                                    <div style="text-align: center">
                                        <img src="<%=URL%>" alt="" style="width: 253px; height: 253px; padding-top: 10px;" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="LineaPunteada"></div>
                    <div style="text-align:center;">
                        <asp:Button ID="btnAgregar" Text="Agregar artículo al catálogo" CssClass="btn btn-dark BotonAgregar" runat="server" OnClick="btnAgregar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
