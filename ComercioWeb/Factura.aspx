<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="ComercioWeb.Factura" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Factura</title>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./Estilos.css" />
</head>
<body style="background-color: white !important;">
    <form id="form1" runat="server">
        <div class="container" style="border-width: 1px; border-color: black; border-style: solid; margin-top: 10px;">
            <h2 style="text-align: center; padding-top: 15px; padding-bottom: 15px;">Factura</h2>
            <div class="LineaFactura"></div>
            <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                <div class="col" style="text-align: left;">
                    <h6>
                        <strong>Inserte nombre del remitente</strong>
                        <br />
                        <br />
                        Inserte URL de la página web
                    </h6>
                </div>
                <div class="col" style="text-align: right;">
                    <h6>
                        <strong>Numero de factura:</strong> <%=NumeroFactura%>
                        <br />
                        <br />
                        <strong>Fecha de compra:</strong> <%=FechaTransaccion%>
                    </h6>
                </div>
            </div>
            <div class="LineaFactura"></div>
            <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                <div class="col" style="text-align: center;">
                    <h5>
                        <strong>Cliente:</strong><br />
                        <%=NombreCliente%>
                    </h5>
                </div>
                <div class="col" style="text-align: center;">
                    <h5>
                        <strong>Remitente:</strong><br />
                        Inserte nombre de remitente
                    </h5>
                </div>
            </div>
            <div class="row row-cols-4" style="padding-left: 60px; padding-right: 60px; padding-top: 20px;">
                <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px;">
                    <strong>Descripción</strong>
                </div>
                <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px;">
                    <strong>Cantidad</strong>
                </div>
                <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px;">
                    <strong>Precio unitario</strong>
                </div>
                <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px;">
                    <strong>Total</strong>
                </div>
            </div>
            <asp:Repeater ID="rptListaEncontrada" runat="server">
                <ItemTemplate>
                    <div class="row row-cols-4" style="padding-left: 60px; padding-right: 60px;">
                        <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px; border-top-width: 0px;">
                            <%#Eval("Articulo.Articulo.Nombre")%>
                        </div>
                        <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px; border-top-width: 0px;">
                            <%#Eval("Articulo.Cantidad")%>
                        </div>
                        <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px; border-top-width: 0px;">
                            $<%#Eval("Articulo.Articulo.Precio")%>
                        </div>
                        <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-top-width: 0px;">
                            $<%#Convert.ToDouble(Eval("Articulo.Articulo.Precio"))*Convert.ToInt32(Eval("Articulo.Cantidad"))%>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <%--Total--%>
            <div class="row row-cols-4" style="padding-left: 60px; padding-right: 60px; padding-bottom: 50px;">
                <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px; border-top-width: 0px;"></div>
                <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px; border-top-width: 0px; border-left-width: 0px;"></div>
                <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-right-width: 0px; border-top-width: 0px; border-left-width: 0px; text-align: right;">
                    Total
                </div>
                <div class="col col-3" style="border-style: solid; border-color: gray; border-width: 1px; border-top-width: 0px;">
                    $<%=PrecioTotal%>
                </div>
            </div>
            <h4 style="text-align: center; color: black !important;">Gracias por su compra</h4>
        </div>
    </form>
</body>
</html>
