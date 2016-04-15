namespace ProyectoCarros.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Carro")]
    public partial class Carro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carro()
        {
            Venta = new HashSet<Venta>();
        }

        [Key]
        public int id_Carro { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(50)]
        public string modelo { get; set; }

            

        public int? id_Marca { get; set; }

        public double? precio { get; set; }

        public virtual Marca Marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
