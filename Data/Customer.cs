namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Customer : Person
    {
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        public ICollection<Sale> Purchases { get; set; }
    }
}
