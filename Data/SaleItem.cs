namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class SaleItem
    {
        [Key]
        public int Id { get; set; }
        public int? Sale_Id { get; set; }
        public int? ProductStyle_Id { get; set; }

        public int Quantity { get; set; }
        public decimal PreDiscountPricePer { get; set; }
    }
}
