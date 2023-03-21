using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenSegundoParcial.Models
{
    public class Ubicacion
    {
        public int id { get; set; }
        public byte[] firma { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public byte[] audio { get; set; }
    }
}
