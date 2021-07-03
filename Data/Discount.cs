namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public class Discount
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Product")]
        public int? Product_Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PercentageDiscounted { get; set; }

        public virtual Product Product { get; set; }
    }
}
