<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarUsuarios.aspx.cs" Inherits="ComercioWeb.RegistrarUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registrar Usuarios</title>
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
                        <li class="nav-item active Activo">
                            <a class="nav-link" href="Usuarios.aspx">Usuarios</a>
                        </li>
                        <li class="nav-item" style="width: 150px;">
                            <a class="nav-link" href="Carrito.aspx">Mi carrito (0)</a>
                        </li>
                    </ul>
                </div>
            </nav>
            <div class="container" style="margin: auto">
                <div class="jumbotron CentrarJumbo">
                    <div class="row">
                        <div class="col">
                            <label>Email</label>
                            <input type="text" class="form-control"/>
                        </div>
                        <div class="col">
                            <label>Repita el email</label>
                            <input type="text" class="form-control"/>
                        </div>
                    </div>
                    <div class="row" style="padding-top:10px;">
                        <div class="col">
                            <label>Contraseña</label>
                            <input type="password" class="form-control"/>
                        </div>
                        <div class="col">
                            <label>Repita la contraseña</label>
                            <input type="password" class="form-control"/>
                        </div>
                    </div>
                    <div class="form-group" style="padding-top:10px;">
                        <label for="apellidoInput">Apellido/s</label>
                        <input type="text" class="form-control" id="apellidoInput" />
                    </div>
                    <div class="form-group">
                        <label for="nombreInput">Nombre/s</label>
                        <input type="text" class="form-control" id="nombreInput" />
                    </div>
                    <div class="form-group">
                        <label>DNI</label>
                        <input type="text" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Teléfono</label>
                        <input type="text" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Provincia</label>
                        <select class="form-control">
                            <option>Buenos Aires</option>
                            <option>Otro</option>
                            <option>Otro</option>
                            <option>Otro</option>
                            <option>Otro</option>
                            <option>Otro</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
