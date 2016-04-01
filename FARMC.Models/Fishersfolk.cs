using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FARMC.Models
{
    public class Fisherfolk : IObjectWithState
    {
        public Fisherfolk()
        {
            FishCatches = new List<FishCatch>();
        }

        public int FisherfolkId { get; set; }
        public string LastName { get; set; }
        public string FisherId { get; set; }

        public virtual List<FishCatch> FishCatches { get; set; } 

        public ObjectState ObjectState { get; set; }
    }
}
