using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VodafoneCashApi.Models
{
    public class Transactions
    {
        
        public Guid TransactionId { get; set; } 

        [Required]
        [StringLength(11)]
        [RegularExpression(@"^01[0125][0-9]{8}$")]
        public string NumberId { get; set; } = String.Empty;
        public Decimal CashBefore { get; set; }
        public Decimal CashAfter { get; set; }
        public Decimal TransactionAmount { get; set; } 
        public DateTime Date { get; set; } = DateTime.Now;
    }
}