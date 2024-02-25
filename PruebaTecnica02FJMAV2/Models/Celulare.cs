using System;
using System.Collections.Generic;

namespace PruebaTecnica02FJMAV2.Models
{
    public partial class Celulare
    {
        public int CelularId { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public int MarcaId { get; set; }

        public virtual Marca Marca { get; set; } = null!;
    }
}
