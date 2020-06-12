using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class ElementoCarrito
    {
        public ElementoCarrito()
        {
            Articulo = new Articulo();
        }
        public int ID_Elemento
        {
            get => default;
            set
            {
            }
        }

        public Articulo Articulo
        {
            get => default;
            set
            {
            }
        }

        public double Cantidad
        {
            get => default;
            set
            {
            }
        }

        public double Precio()
        {
            return Articulo.Precio * Cantidad;
        }
    }
}