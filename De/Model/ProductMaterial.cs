namespace De
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductMaterial")]
    public partial class ProductMaterial
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaterialID { get; set; }

        public double? Count { get; set; }

        public virtual Material Material { get; set; }
    }
}
