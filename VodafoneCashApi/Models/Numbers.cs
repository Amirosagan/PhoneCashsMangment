using System.ComponentModel.DataAnnotations;

namespace VodafoneCashApi.Models
{
    public class Numbers
    {
        [Required]
        [StringLength(11)]
        [RegularExpression(@"^01[0125][0-9]{8}$")]
        [Key]
        public string Number { get; set; } = String.Empty;
        public Decimal Amount { get; set; }
        public ICollection<Transactions> Transactions { get; set; } = new List<Transactions>();
    }
}
