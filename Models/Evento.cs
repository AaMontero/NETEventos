using System;
using System.Collections.Generic;

namespace EventosServicio.Models
{
    public partial class Evento
    {
        public int Id { get; set; }
        public DateTime FechaEvento { get; set; }
        public string LugarEvento { get; set; } = null!;
        public string DescripcionEvento { get; set; } = null!;
        public decimal Precio { get; set; }
        public bool? Eliminado { get; set; }
    }
}
