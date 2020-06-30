<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="ComercioWeb.Carrito" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mi carrito</title>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./Estilos.css" />
</head>
<body class="Fondo">
    <form id="form1" runat="server">
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
                        <%if (!HayUsuarioActivo)
                            {%>
                        <li class="nav-item dropdown">
                            <div>
                                <a class="nav-link dropdown-toggle" href="#" id="dropdownUsuariosNoUser" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Usuarios</a>
                                <div class="dropdown-menu" aria-labelledby="dropdownUsuariosNoUser">
                                    <a class="dropdown-item" href="IniciarSesion.aspx">Iniciar sesión</a>
                                    <a class="dropdown-item" href="RegistrarUsuarios.aspx">Registrarse</a>
                                </div>
                            </div>
                        </li>
                        <%}
                            else
                            {%>
                        <li class="nav-item dropdown">
                            <div>
                                <a class="nav-link dropdown-toggle" href="#" id="dropdownUsuarios" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Usuarios</a>
                                <div class="dropdown-menu" aria-labelledby="dropdownUsuarios">
                                    <%if (Usuario.TipoUsuario.ID_Tipo == 1)
                                        {%>
                                    <a class="dropdown-item" href="Administrador.aspx">Configuraciones de <%=Usuario.TipoUsuario.Nombre%></a>
                                    <%}%>
                                    <%if (Usuario.TipoUsuario.ID_Tipo == 2)
                                        {%>
                                    <a class="dropdown-item" href="Vendedor.aspx">Configuraciones de <%=Usuario.TipoUsuario.Nombre%></a>
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
                        <%}%>
                        <li class="nav-item active Activo" style="width: 200px;">
                            <%if (MiCarrito.ListaElementos.Count() != 0)
                                {%>
                            <a class="nav-link" href="Carrito.aspx">Mi carrito (<%=MiCarrito.Cantidad()%>) - $<%=MiCarrito.PrecioTotal()%></a>
                            <%}
                                else
                                {%>
                            <a class="nav-link" href="Carrito.aspx">Mi carrito (<%=MiCarrito.Cantidad()%>)</a>
                            <%}%>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
        <div class="container">
            <div class="card-columns" style="margin-left: 0px; margin-right: 0px; width: 800px;">
                <asp:Repeater runat="server" ID="rptListaArticulosEnCarrito">
                    <ItemTemplate>
                        <div class="card MiCard">
                            <img src="<%#Eval("Articulo.URL_Imagen")%>" class="card-img-top ImagenCard" alt="<%#Eval("Articulo.Nombre")%>">
                            <div class="card-body" style="margin-top: -15px;">
                                <h5 class="card-title" style="text-align: center; color: black;"><%#Eval("Articulo.Nombre")%></h5>
                                <p class="card-text" style="text-align: center; color: black; margin-top: -10px; margin-bottom: 5px;">
                                    <i><%#Eval("Articulo.MarcaArticulo.Nombre")%></i>
                                </p>
                                <p class="card-text" style="text-align: center; font-size: large; color: black; margin-top:5px;margin-bottom: 10px;">
                                    <strong><%#Eval("Cantidad")%> x $<%#Convert.ToDouble(Eval("Articulo.Precio"))%></strong>
                                </p>
                                <p class="card-text" style="text-align: center; font-size: x-large; color: black; margin-top: -10px;margin-bottom: 10px;">
                                    <strong>Total: $<%#Convert.ToDouble(Eval("Articulo.Precio"))*Convert.ToInt32(Eval("Cantidad"))%></strong>
                                </p>
                            </div>
                            <div class="container" style="text-align: center; padding-bottom: 15px; margin-top: -20px;">
                                <div class="row">
                                    <div class="col">
                                        <a href="Carrito.aspx?comprar=<%#Eval("ID_Elemento")%>" class="btn btn-success">Comprar</a>
                                    </div>
                                    <div class="col">
                                        <a href="Carrito.aspx?eliminar=<%#Eval("ID_Elemento")%>" class="btn btn-danger">Eliminar</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
