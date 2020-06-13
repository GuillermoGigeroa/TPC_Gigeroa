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
</head>
<body style="background-color: rgb(20,20,20); color: white;">
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
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
                        <li class="nav-item">
                            <a class="nav-link" href="Usuarios.aspx">Usuarios</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Carrito.aspx">
                                <img src="https://image.flaticon.com/icons/svg/777/777205.svg" style="width: 25px; height: 25px; vertical-align: top;" alt="Carrito" />
                            </a>
                        </li>
                        <li class="nav-item active">
                            <a class="nav-link"">(0)</a>
                        </li>
                        <li class="nav-item" style="padding-left:125px;">
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="dropdownMarcas" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <b>Marcas</b>
                            </a>
                            <div class="dropdown-menu" aria-labelledby="dropdownMarcas">
                                <a class="dropdown-item" href="Catalogo.aspx?fmar=Marca1">Marca1</a>
                                <a class="dropdown-item" href="Catalogo.aspx?fmar=Marca2">Marca2</a>
                                <a class="dropdown-item" href="Catalogo.aspx?fmar=Marca3">Marca3</a>
                            </div>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="dropdownCategorias" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <b>Categorías</b>
                            </a>
                            <div class="dropdown-menu" aria-labelledby="dropdownCategorias">
                                <a class="dropdown-item" href="Catalogo.aspx?fcat=Categoria1">Categoria1</a>
                                <a class="dropdown-item" href="Catalogo.aspx?fcat=Categoria2">Categoria2</a>
                                <a class="dropdown-item" href="Catalogo.aspx?fcat=Categoria3">Categoria3</a>
                            </div>
                        </li>
                    </ul>
                    <div class="container">
                        <div class="row">
                            <div class="flex-column" style="padding-left: 400px;"></div>
                            <div class="flex-column">
                                <div class="form-inline">
                                    <asp:TextBox ID="txtBusqueda" CssClass="form-control mr-sm-2" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnBuscar" CssClass="btn btn-outline-light my-2 my-sm-0" runat="server" Text="Buscar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </nav>
        </div>
    </form>
</body>
</html>
