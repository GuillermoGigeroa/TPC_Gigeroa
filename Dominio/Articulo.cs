using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Articulo
    {
        public Articulo()
        {
            Categorias = new List<Categoria>();
            Marca = new Marca();
        }
        public int ID_Articulo
        {
            get => default;
            set
            {
            }
        }
        public string Nombre
        {
            get => default;
            set
            {
            }
        }
        public Marca Marca
        {
            get => default;
            set
            {
            }
        }
        public List<Categoria> Categorias
        {
            get => default;
            set
            {
            }
        }
        public string Descripcion
        {
            get => default;
            set
            {
            }
        }
        public bool EsMateriaPrima
        {
            get => default;
            set
            {
            }
        }
        public string URL_Imagen
        {
            get => default;
            set
            {
            }
        }
        public bool Estado
        {
            get => default;
            set
            {
            }
        }
        public double Precio
        {
            get => default;
            set
            {
            }
        }
    }
}