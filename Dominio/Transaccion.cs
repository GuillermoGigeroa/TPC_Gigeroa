using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Transaccion
    {
        public Transaccion()
        {
            Articulo = new ElementoCarrito();
            Estado = new EstadoTransaccion();
        }
        public int NumeroFactura { get; set; }
        public int ID_Usuario { get; set; }
        public ElementoCarrito Articulo { get; set; }
        public string FechaAccion { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int DNI { get; set; }
        public EstadoTransaccion Estado { get; set; }
        public string Direccion { get; set; }
        public int CodigoPostal { get; set; }
    }
}
