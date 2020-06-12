﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Carrito
    {
        public Carrito()
        {
            ListaElementos = new List<ElementoCarrito>();
        }
        public int ID_Carrito
        {
            get => default;
            set
            {
            }
        }
        public List<ElementoCarrito> ListaElementos
        {
            get => default;
            set
            {
            }
        }
        public double PrecioTotal()
        {
            double Precio = 0;
            foreach (var elemento in ListaElementos)
            {
                Precio += elemento.Precio();
            }
            return Precio;
        }
    }
}