namespace De
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductSale")]
    public partial class ProductSale
    {
        [Required]
        [StringLength(150)]
        public string AgentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime SaleDate { get; set; }

        public int ProductCount { get; set; }

        public int Id { get; set; }

        public virtual Agent Agent { get; set; }

        public virtual Product Product { get; set; }
    }
}
