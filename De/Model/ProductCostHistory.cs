namespace De
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCostHistory")]
    public partial class ProductCostHistory
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductID { get; set; }

        public DateTime ChangeDate { get; set; }

        public decimal CostValue { get; set; }
    }
}
