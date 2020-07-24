<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Controlventasystock.aspx.cs" Inherits="ComercioWeb.Controlventasystock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Control de ventas y stock</title>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./Estilos.css" />
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">document.oncontextmenu = function () { return false }</script>
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
                                    <a class="dropdown-item" href="Vendedor.aspx">Configuraciones de vendedor</a>
                                    <a class="dropdown-item" href="Controlventasystock.aspx?admin=true">Control de ventas y stock</a>
                                    <%}%>
                                    <%if (Usuario.TipoUsuario.ID_Tipo == 2)
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
                        <li class="nav-item">
                            <asp:Button ID="btnActualizar" Text="Buscar nuevas ventas" CssClass="btn btn-success BotonAgregarLight" runat="server" OnClick="btnActualizar_Click" />
                        </li>
                        <li>
                            <%if (HayUsuarioActivo)
                                {%>
                            <a class="nav-link" style="padding-left: 400px;">Usuario: <%=Usuario.Nombres%> <%=Usuario.Apellidos%></a>
                            <%}%>
                        </li>
                    </ul>
                </div>
            </nav>
            <div class="jumbotron" style="padding-top: 5px;">
                <h3 style="padding-bottom: 10px;">Control de ventas</h3>
                <div style="text-align: center; padding-bottom: 5px;">
                    <asp:Button ID="btnGuardarVentas" Text="Guardar cambios de estado" CssClass="btn btn-danger BotonAgregar" runat="server" OnClick="btnGuardarVentas_Click" />
                </div>
                <table class="table table-sm table-secondary">
                    <thead>
                        <tr>
                            <th scope="col"><span style="padding: 5px">Numero de factura</span></th>
                            <th scope="col">Nombre del artículo</th>
                            <th scope="col">Cantidad</th>
                            <th scope="col">Fecha de compra</th>
                            <th scope="col">Email</th>
                            <th scope="col">Telefono</th>
                            <th scope="col">Nombres</th>
                            <th scope="col">Apellidos</th>
                            <th scope="col">DNI</th>
                            <th scope="col">Domicilio</th>
                            <th scope="col">Estado</th>
                            <th scope="col">Factura</th>
                            <th scope="col">Modificar</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptVentas" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("NumeroFactura")%></td>
                                    <td><%#Eval("Articulo.Articulo.Nombre")%></td>
                                    <td><%#Eval("Articulo.Cantidad")%></td>
                                    <td><%#Eval("FechaAccion")%></td>
                                    <td><%#Eval("Email")%></td>
                                    <td><%#Eval("Telefono")%></td>
                                    <td><%#Eval("Nombres")%></td>
                                    <td><%#Eval("Apellidos")%></td>
                                    <td><%#Eval("DNI")%></td>
                                    <td><%#Eval("Domicilio.Provincia")+", "+Eval("Domicilio.Ciudad")+", "+Eval("Domicilio.Calle")+" "+Eval("Domicilio.Numero")+" "+Eval("Domicilio.Piso")+" "+Eval("Domicilio.Departamento")+" (CP "+Eval("Domicilio.CodigoPostal")+")\nReferencia: "+Eval("Domicilio.Referencia")%></td>
                                    <td><%#Eval("Estado.Descripcion")%></td>
                                    <td><a href="Factura.aspx?factura=<%#Eval("NumeroFactura")%>" target="_blank">Ver</a></td>
                                    <td>
                                        <a href="Controlventasystock.aspx?fact=<%#Eval("NumeroFactura")%>" class="btn btn-success">Cambiar estado</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container CajaControl">
                            <h3><b>Modificación de estado de facturas</b></h3>
                            <div class="row">
                                <div class="col">
                                    <h4>Numero de factura</h4>
                                    <asp:DropDownList ID="drpFacturas" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="drpFacturas_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col">
                                    <h4>Estado</h4>
                                    <asp:DropDownList ID="drpEstados" AutoPostBack="false" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="Pendiente" Value="1" />
                                        <asp:ListItem Text="En camino" Value="2" />
                                        <asp:ListItem Text="Entregado" Value="3" />
                                        <asp:ListItem Text="Error" Value="4" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col">
                                    <h4><small>Por favor, recuerde guardar los cambios</small></h4>
                                    <div style="text-align: left;">
                                        <asp:Button ID="btnModificarFactura" AutoPostBack="false" Text="Modificar" CssClass="btn btn-danger BotonAgregar" runat="server" OnClick="btnModificarFactura_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <h3 style="padding-bottom: 10px;">Control de stock</h3>
                <div style="text-align: center; padding-bottom: 5px;">
                    <asp:Button ID="btnGuardarStock" Text="Guardar cambios de stock" CssClass="btn btn-danger BotonAgregar" runat="server" OnClick="btnGuardarStock_Click" />
                </div>
                <div class="row" style="padding-bottom: 20px;">
                    <table class="table table-sm table-secondary">
                        <thead>
                            <tr>
                                <th scope="col" style="text-align: center;"><span style="padding: 5px">ID de artículo</span></th>
                                <th scope="col">Nombre</th>
                                <th scope="col">Marca del artículo</th>
                                <th scope="col">Descripción</th>
                                <th scope="col" style="text-align: center;">Precio</th>
                                <th scope="col" style="text-align: center;">Estado</th>
                                <th scope="col" style="text-align: center;">Stock actual</th>
                                <th scope="col" style="text-align: center;">Modificar stock</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptStock" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;"><%#Eval("ID_Articulo")%></td>
                                        <td><%#Eval("Nombre")%></td>
                                        <td><%#Eval("MarcaArticulo.Nombre")%></td>
                                        <td><%#Eval("Descripcion")%></td>
                                        <td style="text-align: center;">$<%#Eval("Precio")%></td>
                                        <td style="text-align: center;"><%#Estado(Convert.ToBoolean(Eval("Estado")))%></td>
                                        <td style="text-align: center;"><%#Eval("Stock")%></td>
                                        <td style="text-align: center;">
                                            <a href="Controlventasystock.aspx?cant=1&idart=<%#Eval("ID_Articulo")%>" class="btn btn-success">+1</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="container CajaControl">
                    <h3><b>Agregar stock de artículos</b></h3>
                    <div class="row">
                        <div class="col">
                            <h4>ID del artículo</h4>
                            <asp:DropDownList ID="drpArticulos" AutoPostBack="false" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <h4>Cantidad</h4>
                            <asp:TextBox ID="txtCantidad" Text="1" type="number" min="1" max="100" CssClass="form-control" runat="server" />
                        </div>
                        <div class="col">
                            <h4><small>Por favor, recuerde guardar los cambios</small></h4>
                            <div style="text-align: left;">
                                <asp:Button ID="btnModificarStock" AutoPostBack="false" Text="Agregar" CssClass="btn btn-danger BotonAgregar" runat="server" OnClick="btnModificarStock_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
