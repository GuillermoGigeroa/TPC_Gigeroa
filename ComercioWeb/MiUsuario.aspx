<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiUsuario.aspx.cs" Inherits="ComercioWeb.MiUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Mi usuario</title>
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
                        <li class="nav-item" style="width:200px;">
                            <%if (Carrito.ListaElementos.Count() != 0)
                              {%>
                            <a class="nav-link" href="Carrito.aspx">Mi carrito (<%=Carrito.Cantidad()%>) - $<%=Carrito.PrecioTotal()%></a>
                            <%}
                              else
                              {%>
                            <a class="nav-link" href="Carrito.aspx">Mi carrito (<%=Carrito.Cantidad()%>)</a>
                            <%}%>
                        </li>
                        <li class="nav-item">
                            <asp:Button ID="btnActualizar" Text="Actualizar" CssClass="btn btn-success BotonAgregarLight" runat="server" OnClick="btnActualizar_Click"/>
                        </li>
                        <li>
                            <%if (HayUsuarioActivo)
                              {%>
                              <a class="nav-link" style="padding-left:400px;">Usuario: <%=Usuario.Nombres%> <%=Usuario.Apellidos%></a>
                            <%}%>
                        </li>
                    </ul>
                </div>
            </nav>
            <div class="jumbotron">
                <h1 style="text-align:center;">¡Bienvenido <%=Usuario.Nombres%> <%=Usuario.Apellidos%>!</h1>
                <h3 style="padding-top:15px">Mis compras</h3>
                <table class="table table-sm table-secondary">
                    <thead>
                        <tr>
                            <th scope="col"><span style="padding:5px">Numero de factura</span></th>
                            <th scope="col">Fecha de compra</th>
                            <th scope="col">Nombre del artículo</th>
                            <th scope="col">Cantidad</th>
                            <th scope="col">Estado</th>
                        </tr>
                    </thead>
                    <tbody>
                <asp:Repeater ID="rptCompras" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("NumeroFactura")%></td>
                            <td><%#Eval("FechaAccion")%></td>
                            <td><%#Eval("Articulo.Articulo.Nombre")%></td>
                            <td><%#Eval("Articulo.Cantidad")%></td>
                            <td><%#Eval("Estado.Descripcion")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
