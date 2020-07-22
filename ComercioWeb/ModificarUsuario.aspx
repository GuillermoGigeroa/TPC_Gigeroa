<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarUsuario.aspx.cs" Inherits="ComercioWeb.ModificarUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Modificar Usuario</title>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./Estilos.css" />
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">document.oncontextmenu = function(){return false}</script>
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
                                    <%if (Usuario.TipoUsuario.ID_Tipo <= 3)
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
                        <li>
                            <%if (HayUsuarioActivo)
                              {%>
                              <a class="nav-link" style="padding-left:575px;">Usuario: <%=Usuario.Nombres%> <%=Usuario.Apellidos%></a>
                            <%}%> 
                        </li>
                    </ul>
                </div>
            </nav>
            <div style="text-align:center;padding-top:20px;padding-bottom:10px;">
                <asp:Label ID="lblActualizado" CssClass="alert alert-success    " Visible="false" Text="Usuario ha sido actualizado." runat="server" />
            </div>
            <h3 style="text-align: center; padding-top: 10px;">Modificar usuario</h3>
            <div class="container" style="margin: auto">
                <div class="jumbotron CentrarJumbo">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col" style="margin-right: -170px; padding-left: 150px;">
                                    <asp:CheckBox ID="chkEmail" AutoPostBack="true" runat="server" OnCheckedChanged="chkEmail_CheckedChanged" />
                                </div>
                                <div class="col" style="margin-left: -150px;margin-bottom:-15px;">
                                    <p>Deseo modificar el email.</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:Label ID="Email" Visible="false" Text="Nuevo email" runat="server" />
                                    <asp:TextBox ID="txtEmail" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <asp:Label ID="Email2" Visible="false" Text="Repita el nuevo email" runat="server" />
                                    <asp:TextBox ID="txtEmail2" Visible="false" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtEmail2_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Label ID="lblEmail" runat="server" Text="Los campos de email son diferentes." Visible="false"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-group" style="padding-top: 10px;">
                        <label for="apellidoInput">Apellido/s</label>
                        <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="nombreInput">Nombre/s</label>
                        <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label>DNI</label>
                                <asp:TextBox ID="txtDNI" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtDNI_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblDNI" Text="El DNI ingresado no es válido." Visible="false" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label>Teléfono</label>
                                <asp:TextBox ID="txtTelefono" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtTelefono_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblTelefono" Text="El teléfono ingresado no es válido." Visible="false" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-group" style="margin-top: 0px; margin-bottom: -10px;">
                        <p style="margin-top: 5px; text-align: center;">Datos de domicilio</p>
                    </div>
                    <div class="LineaPunteada"></div>
                    <div class="form-group" style="padding-top: 10px;">
                        <label>Provincia</label>
                        <asp:DropDownList ID="ListaProvincias" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="row">
                        <div class="col">
                            <label>Ciudad</label>
                            <asp:TextBox ID="txtCiudad" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="col">
                                    <label>Código postal</label>
                                    <asp:TextBox ID="txtCodigoPostal" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtCodigoPostal_TextChanged"></asp:TextBox>
                                </div>
                                <asp:Label ID="lblCodigoPostal" Visible="false" Text="El código postal ingresado es inválido" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row" style="padding-top: 10px;">
                        <div class="col">
                            <label>Calle</label>
                            <asp:TextBox ID="txtCalle" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="col">
                                    <label>Número</label>
                                    <asp:TextBox ID="txtNumero" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtNumero_TextChanged"></asp:TextBox>
                                </div>
                                <asp:Label ID="lblNumero" Visible="false" Text="El numero es inválido." runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row" style="padding-top: 10px;">
                        <div class="col">
                            <label>Piso</label>
                            <asp:TextBox ID="txtPiso" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col">
                            <label>Departamento</label>
                            <asp:TextBox ID="txtDepartamento" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" style="padding-top: 10px;">
                        <label>Referencia de domicilio:</label>
                        <asp:TextBox ID="txtReferencia" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="LineaPunteada"></div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row" style="padding-top:10px;">
                                <div class="col" style="margin-right: -170px; padding-left: 150px;">
                                    <asp:CheckBox ID="chkPassword" AutoPostBack="true" runat="server" OnCheckedChanged="chkPassword_CheckedChanged"/>
                                </div>
                                <div class="col" style="margin-left: -150px;margin-bottom:-15px;">
                                    <p>Deseo modificar la contraseña.</p>
                                </div>
                            </div>
                            <asp:Label ID="Password" Visible="false" Text="Contraseña anterior" runat="server" />
                            <asp:TextBox ID="txtPasswordAnterior" Visible="false" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                            <div class="row" style="padding-top: 10px;">
                                <div class="col">
                                    <asp:Label ID="NuevoPassword" Visible="false" Text="Nueva contraseña" runat="server" />
                                    <asp:TextBox ID="txtPassword" Visible="false" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <asp:Label ID="NuevoPassword2" Visible="false" Text="Repita la nueva contraseña" runat="server" />
                                    <asp:TextBox ID="txtPassword2" Visible="false" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Label ID="lblPassword" Text="Los campos de contraseña no son iguales." Visible="false" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col" style="text-align: center; margin-top: 10px;">
                            <asp:Button ID="btnAceptar" Text="Aceptar" runat="server" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
