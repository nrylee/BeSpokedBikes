namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SalesPerson : Person
    {
        public DateTime? StartDate { get; set; }
        public DateTime? TerminationDate { get; set; }
    }
}
