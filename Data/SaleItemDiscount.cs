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
        [DataType(DataType.Currency)]
        public decimal AmountDiscounted { get; set; }
        
        public int SaleItem_Id { get; set; }
        public int Discount_Id { get; set; }

        public SaleItem SaleItem { get; set; }
        public Discount Discount { get; set; }
    }
}
