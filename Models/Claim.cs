using System;
using System.ComponentModel.DataAnnotations;

namespace ContractClaimSystem.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }

        [Required]
        public string Contract { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime ClaimDate { get; set; }

        public string Status { get; set; } = "Pending";

        public string DocumentName { get; set; } // Path of the uploaded document
    }
}
