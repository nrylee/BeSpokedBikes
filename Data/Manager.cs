namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Manager : Person
    {
        public ICollection<ManagerSalesPerson> ManagerSalesPeople { get; set; }
    }
}
