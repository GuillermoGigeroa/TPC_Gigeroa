<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="ComercioWeb.Catalogo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Catálogo</title>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./Estilos.css"/>
</head>
<body class="Fondo">
    <form id="form1" runat="server">
        <div>
            <script type="text/javascript">document.oncontextmenu = function(){return false}</script>
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark Barra">
                <%--<a class="navbar-brand" href="#"><i><b>Nombre de negocio</b></i></a>--%>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link" href="Index.aspx">Inicio</a>
                        </li>
                        <li class="nav-item active Activo">
                            <a class="nav-link" href="Catalogo.aspx">Catálogo</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Usuarios.aspx">Usuarios</a>
                        </li>
                        <li class="nav-item" style="width:150px;">
                            <a class="nav-link" href="Carrito.aspx">Mi carrito (0)</a>
                        </li>
                        <li class="nav-item" style="padding-left: 50px;"></li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="dropdownMarcas" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span style="color: white;"><b>Marcas</b></span>
                            </a>
                            <div class="dropdown-menu" aria-labelledby="dropdownMarcas">
                                <asp:Repeater ID="rptListaMarcas" runat="server">
                                    <ItemTemplate>
                                        <a class="dropdown-item" href="Catalogo.aspx?fmar=<%#Eval("ID_Marca")%>"><%#Eval("Nombre")%></a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="dropdownCategorias" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span style="color: white;"><b>Categorías</b></span>
                            </a>
                            <div class="dropdown-menu" aria-labelledby="dropdownCategorias">
                                <asp:Repeater ID="rptListaCategorias" runat="server">
                                    <ItemTemplate>
                                        <a class="dropdown-item" href="Catalogo.aspx?fcat=<%#Eval("ID_Categoria")%>"><%#Eval("Nombre")%></a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </li>
                        <li>
                            <a href="Catalogo.aspx">
                                <img src="./BotonBorrar.png" class="BotonIcono" alt="Borrar filtro" />
                            </a>
                        </li>
                    </ul>
                    <div class="container">
                        <div class="row">
                            <div class="flex-column" style="padding-left: 325px;"></div>
                            <div class="flex-column">
                                <div class="form-inline">
                                    <asp:TextBox ID="txtBuscar" CssClass="form-control mr-sm-2" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnBuscar" CssClass="btn btn-outline-light my-2 my-sm-0" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </nav>
        </div>
        <div class="container" style="padding-bottom: 30px;">
        </div>
        <div class="container">
            <div class="card-columns" style="margin-left: 0px; margin-right: 0px; width: 800px;">
                <asp:Repeater runat="server" ID="rptListaArticulos">
                    <ItemTemplate>
                        <div class="card MiCard">
                            <img src="<%#Eval("URL_Imagen")%>" class="card-img-top ImagenCard"  alt="<%#Eval("Nombre")%>">
                            <div class="card-body" style="margin-top: -15px;">
                                <h5 class="card-title" style="text-align: center; color: black;"><%#Eval("Nombre")%></h5>
                                <p class="card-text" style="text-align: center; color: black; margin-top: -10px; margin-bottom: 5px;">
                                    <i><%#Eval("MarcaArticulo.Nombre")%></i>
                                </p>
                                <p class="card-text" style="text-align: center; font-size: x-large; color: black; margin-top: -10px;">
                                    <strong>$<%#Convert.ToDouble(Eval("Precio"))%></strong></p>
                                </p>
                            </div>
                            <div class="container" style="text-align: center; padding-bottom: 15px; margin-top: -30px;">
                                <div class="row" style="display: inline-block">
                                    <div class="btn-group">
                                        <a href="Catalogo.aspx?idArt=<%#Eval("ID_Articulo")%>" class="btn btn-dark" style="padding-bottom: 9px; background-color: RGB(179,134,179); border-width: 0px;">Agregar al carrito
                                        </a>
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
