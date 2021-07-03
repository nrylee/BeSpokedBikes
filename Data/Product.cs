namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int? Manufacturer_Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }
        [DataType(DataType.Currency)]
        public decimal SalesPrice { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
}
