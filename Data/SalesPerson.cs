namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class SalesPerson : Person
    {
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? TerminationDate { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<ManagerSalesPerson> SalesPersonManagers { get; set; }
    }
}
