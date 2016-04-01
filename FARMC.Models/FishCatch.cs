using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FARMC.Models
{
    public class FishCatch : IObjectWithState
    {
        public int FishCatchId { get; set; }
        public string FishName { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }

        public int FisherfolkId { get; set; }
        public Fisherfolk Fisherfolk { get; set; }

        public ObjectState ObjectState { get; set; }
    }
}
