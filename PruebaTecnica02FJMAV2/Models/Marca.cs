using System;
using System.Collections.Generic;

namespace PruebaTecnica02FJMAV2.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Celulares = new HashSet<Celulare>();
        }

        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Celulare> Celulares { get; set; }
    }
}
