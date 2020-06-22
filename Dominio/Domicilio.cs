using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Domicilio
    {
        public string Provincia { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Piso { get; set; }
        public int CodigoPostal { get; set; }
        public string Departamento { get; set; }
        public string Referencia { get; set; }
    }
}