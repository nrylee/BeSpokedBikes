namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductStyle
    {
        [Key]
        public int Id { get; set; }
        public int? Product_Id { get; set; }
        public string StyleName { get; set; }
        public int QuantityOnHand { get; set; }
    }
}
