using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Security;
using System.Text;

namespace Dominio
{
    public class Carrito
    {
        public Carrito()
        {
            ListaElementos = new List<ElementoCarrito>();
        }
        public List<ElementoCarrito> ListaElementos { get; set; }
        public double PrecioTotal()
        {
            double Precio = 0;
            foreach (var elemento in ListaElementos)
            {
                Precio += elemento.Articulo.Precio * elemento.Cantidad;
            }
            return Precio;
        }
        public int Cantidad()
        {
            int conteo = 0;
            foreach(ElementoCarrito elemento in ListaElementos)
            {
                conteo += elemento.Cantidad;
            }
            return conteo;
        }
        public void AgregarArticulo(Articulo articulo, int cantidad)
        {
            foreach(ElementoCarrito elemento in ListaElementos)
            {
                if(elemento.Articulo.ID_Articulo == articulo.ID_Articulo)
                {
                    elemento.Cantidad += 1;
                    return;
                }
            }
            ElementoCarrito elementoCarrito = new ElementoCarrito();
            elementoCarrito.ID_Elemento = ListaElementos.Count();
            elementoCarrito.Cantidad = cantidad;
            elementoCarrito.Articulo = articulo;
            ListaElementos.Add(elementoCarrito);
        }
    }
}