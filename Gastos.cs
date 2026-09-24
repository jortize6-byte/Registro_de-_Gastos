using System;
using System.Collections.Generic;
using System.Text;

namespace Registro_de__Gastos
{
    internal class Gastos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public decimal Monto { get; set; }
        public string Categoria { get; set; } = "";

        public override string ToString()
        {
            return $"{Id,-3} {Descripcion,-25} Q {Monto,10:N2}  {Categoria}";
        }
    }
}