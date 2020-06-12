using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Usuario
    {
        public Usuario()
        {
            ListaFavoritos = new List<Articulo>();
            Domicilio = new Domicilio();
            TipoUsuario = new TipoUsuario();
        }
        public int ID_Usuario
        {
            get => default;
            set
            {
            }
        }

        public string Email
        {
            get => default;
            set
            {
            }
        }

        public string Password
        {
            get => default;
            set
            {
            }
        }

        public System.DateTime FechaNacimiento
        {
            get => default;
            set
            {
            }
        }

        public string Nombres
        {
            get => default;
            set
            {
            }
        }

        public string Apellidos
        {
            get => default;
            set
            {
            }
        }

        public int DNI
        {
            get => default;
            set
            {
            }
        }

        public Domicilio Domicilio
        {
            get => default;
            set
            {
            }
        }

        public TipoUsuario TipoUsuario
        {
            get => default;
            set
            {
            }
        }

        public int Telefono
        {
            get => default;
            set
            {
            }
        }

        public bool Activo
        {
            get => default;
            set
            {
            }
        }

        public List<Articulo> ListaFavoritos
        {
            get => default;
            set
            {
            }
        }
    }
}