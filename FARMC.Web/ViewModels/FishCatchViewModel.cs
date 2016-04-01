using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FARMC.Models;

namespace FARMC.Web.ViewModels
{
    public class FishCatchViewModel : IObjectWithState
    {
        public int FishCatchId { get; set; }
        [Required(ErrorMessage = "[Server Halt]: Fish Name is required.")]
        [MaxLength(20, ErrorMessage = "[Server Halt]: Fish Name should not exceed 20 characters.")]
        [RegularExpression(@"^[A-Za-z]+$",  ErrorMessage = "[Server Halt]: Fish Name should with alphabet characters.")]
        public string FishName { get; set; }
        [Required(ErrorMessage = "[Server Halt]: Fish Catch Quantity is required.")]
        [Range(1,1000000, ErrorMessage = "[Server Halted] Fish Catch Quantity must be between 1-10000000")]
        public decimal Qty { get; set; }
        [Required(ErrorMessage = "[Server Halt]: Unit Price is required.")]
        [Range(1, 1000000, ErrorMessage = "[Server Halted] Unit Price must be between 1-10000000")]
        public decimal UnitPrice { get; set; }

        public int FisherfolkId { get; set; }

        public ObjectState ObjectState { get; set; }
        
    }
}