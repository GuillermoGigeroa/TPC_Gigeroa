using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int ID_Usuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int DNI { get; set; }
        public Domicilio Domicilio { get; set; }
        public Tipo TipoDeUsuario { get; set; }
        public int Telefono { get; set; }
        public bool Activo { get; set; }
        public List<Articulo> Favoritos = new List<Articulo>(); 
    }
}
