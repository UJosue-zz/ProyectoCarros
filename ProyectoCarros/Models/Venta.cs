namespace ProyectoCarros.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Venta")]
    public partial class Venta
    {
        [Key]
        public int id_Venta { get; set; }

        public int? id_Carro { get; set; }

        public int? id_Cliente { get; set; }

        [StringLength(50)]
        public string fecha_Venta { get; set; }

        public virtual Carro Carro { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
