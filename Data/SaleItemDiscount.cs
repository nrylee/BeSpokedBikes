namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public class SaleItemDiscount
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("SaleItem")]
        public int SaleItem_Id { get; set; }
        [ForeignKey("Discount")]
        public int Discount_Id { get; set; }

        public virtual SaleItem SaleItem { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
