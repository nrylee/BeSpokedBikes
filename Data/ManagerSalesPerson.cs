namespace BeSpokedBikes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public class ManagerSalesPerson
    {
        public int Manager_Id { get; set; }
        public Manager Manager { get; set; }

        public int SalesPerson_Id { get; set; }
        public SalesPerson SalesPerson { get; set; }
    }
}
