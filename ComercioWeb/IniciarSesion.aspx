<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="ComercioWeb.IniciarSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar sesión</title>
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
                        <li class="nav-item dropdown">
                            <div>
                                <a class="nav-link dropdown-toggle active Activo" href="#" id="dropdownUsuarios" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Usuarios</a>
                                <div class="dropdown-menu" aria-labelledby="dropdownUsuarios">
                                    <a class="dropdown-item" href="IniciarSesion.aspx">Iniciar sesión</a>
                                    <a class="dropdown-item" href="RegistrarUsuarios.aspx">Registrarse</a>
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
                    </ul>
                </div>
            </nav>
            <h3>Iniciar sesión</h3>
            <div style="text-align:center;">
                <asp:Label ID="lblErrorCompra" Text="Antes de comprar, debes iniciar sesión o registrarte." Visible="false" runat="server" />
            </div>
            <div class="jumbotron CentrarJumbo">
                <div class="form-group">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Contraseña</label>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <asp:Label ID="lblLogin" Visible="false" Text="Combinación de usuario y contraseña son incorrectos." runat="server" />
                <div style="text-align: center;">
                    <asp:Button ID="btnAceptar" Text="Iniciar sesión" CssClass="btn btn-light" runat="server" OnClick="btnAceptar_Click"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
