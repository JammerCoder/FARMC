using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FARMC.Models;

namespace FARMC.Web.ViewModels
{
    public class FisherfolkViewModel : IObjectWithState
    {
        public FisherfolkViewModel()
        {
            FishCatches = new List<FishCatchViewModel>();
            FishCatchesToDelete = new List<int>();
        }

        public int FisherfolkId { get; set; }
        [Required(ErrorMessage = "[Server Halt]: Fisherfolk name is required.")]
        [MaxLength(30, ErrorMessage = "[Server Halt]: Fisherfolk Name should not exceed 30 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "[Server Halt]: Fisherfolk ID is required.")]
        [MaxLength(15, ErrorMessage = "[Server Halt]: Fisherfolk Name should not exceed 15 characters.")]
        public string FisherId { get; set; }
        public string MessageToUser { get; set; }

        public List<FishCatchViewModel> FishCatches { get; set; }
        public List<int> FishCatchesToDelete { get; set; } 

        public ObjectState ObjectState { get; set; }

        

        
    }
}