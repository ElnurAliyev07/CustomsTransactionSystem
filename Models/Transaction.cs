using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CustomsTransactionSystem.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Column(TypeName = "nvarchar(13)")]
        [DisplayName("Malın Kodu")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(13, ErrorMessage = "Maximum 13 characters only.")]
        public string ProductCode { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Malın Adı")]
        [Required(ErrorMessage = "This field is required.")]
        public string ProductName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Malın Gəldiyi Ölkə")]
        [Required(ErrorMessage = "This field is required.")]
        public string CountryName { get; set; }
        [Column(TypeName = "nvarchar(13)")]
        [DisplayName("Ölkə Kodu")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(13, ErrorMessage = "Maximum 13 characters only.")]
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int Miqdar { get; set; }
        public DateTime Tarix { get; set; }
    }
}

