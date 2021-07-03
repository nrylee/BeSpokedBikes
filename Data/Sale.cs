namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime? SellDateTime { get; set; }
        public int? Customer_Id { get; set; }
        public int? SalesPerson_Id { get; set; }
        public Customer Customer { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public ICollection<SaleItem> Items { get; set; }
    }
}
