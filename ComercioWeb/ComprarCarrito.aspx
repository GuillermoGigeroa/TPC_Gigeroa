<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComprarCarrito.aspx.cs" Inherits="ComercioWeb.ComprarCarrito" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Comprar mi carrito</title>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./Estilos.css" />
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">document.oncontextmenu = function(){return false}</script>
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
                                    <a class="dropdown-item" href="Administrador.aspx">Configuraciones de administrador</a>
                                    <%}%>
                                    <%if (Usuario.TipoUsuario.ID_Tipo < 3)
                                        {%>
                                    <a class="dropdown-item" href="Vendedor.aspx">Configuraciones de vendedor</a>
                                    <a class="dropdown-item" href="Controlventasystock.aspx">Control de ventas y stock</a>
                                    <%}%>
                                    <%if (Usuario.TipoUsuario.ID_Tipo <= 3)
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
                <h3>Comprar los elementos del carrito</h3>
                <div class="row" style="padding-top: 10px;">
                    <div class="col">
                        <h4>Domicilio a entregar:</h4>
                        <div class="alert alert-light DetallesDeCompra" style="text-align: center;" role="alert">
                            <%=Usuario.Domicilio.Provincia%>, 
                            <%=Usuario.Domicilio.Ciudad%>, 
                            <%=Usuario.Domicilio.Calle%>
                            <%=Usuario.Domicilio.Numero%>
                            <%=Usuario.Domicilio.Piso%>
                            <%=Usuario.Domicilio.Departamento%>
                            <br />
                            <br />
                            Referencia:
                            <%=Usuario.Domicilio.Referencia%>
                            <br />
                            <br />
                            <a href="ModificarUsuario.aspx" class="alert-link">Modificar domicilio</a>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class="col">
                        <h4>Detalle de compra:</h4>
                        <div class="alert alert-light DetallesDeCompra" style="text-align: center;" role="alert">
                            <%foreach (Dominio.ElementoCarrito elemento in MiCarrito.ListaElementos)
                                {%>
                            <%=elemento.Cantidad%> x <%=elemento.Articulo.Nombre%> = $<%=Convert.ToDouble(elemento.Articulo.Precio)*Convert.ToDouble(elemento.Cantidad)%>
                            <br />
                            <%}%>
                            <br />
                            Total: $<%=MiCarrito.PrecioTotal()%>
                            <br />
                            <br />
                            <a href="Carrito.aspx" class="alert-link">Eliminar artículos</a>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class="col">
                        <label for="inputState">Medio de pago</label>
                        <select id="inputState" class="form-control">
                            <option>Visa</option>
                            <option>Mastercard</option>
                            <option>Cabal</option>
                            <option>Naranja</option>
                            <option>Maestro</option>
                        </select>
                        <br/>
                        <input type="text" placeholder="Numero de tarjeta" class="form-control" />
                    </div>
                </div>
                <div style="text-align:center;padding-top:15px;">
                    <asp:Button ID="btnComprar" Text="Confirmar compra" CssClass="btn btn-light BotonAgregar" runat="server" OnClick="btnComprar_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
