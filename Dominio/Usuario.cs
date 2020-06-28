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
        public int ID_Usuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int DNI { get; set; }
        public Domicilio Domicilio { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public int Telefono { get; set; }
        public bool Activo { get; set; }
        public int IDListaFavoritos { get; set; }
        public List<Articulo> ListaFavoritos { get; set; }
    }
}