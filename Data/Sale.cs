namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Sale
    {
        [Key]
        public int Id { get; set; }
        
        public int? Customer_Id { get; set; }
        public int? SalesPerson_Id { get; set; }
    }
}
