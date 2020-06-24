using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Encriptador
    {
        public string Encriptar(string texto)
        {
            string TextoEncriptado = "";
            bool Toggle = true;
            foreach (char letra in texto)
            {
                if (Toggle)
                {
                    Toggle = false;
                    TextoEncriptado += Convert.ToChar(letra + 2);
                }
                else
                {
                    Toggle = true;
                    TextoEncriptado += Convert.ToChar(letra - 2);
                }
            }
            return TextoEncriptado;
        }
        public string Desencriptar(string textoEncriptado)
        {
            string TextoDesencriptado = "";
            bool Toggle = true;
            foreach (char letra in textoEncriptado)
            {
                if (Toggle)
                {
                    Toggle = false;
                    TextoDesencriptado += Convert.ToChar(letra - 2);
                }
                else
                {
                    Toggle = true;
                    TextoDesencriptado += Convert.ToChar(letra + 2);
                }
            }
            return TextoDesencriptado;
        }
    }
}
