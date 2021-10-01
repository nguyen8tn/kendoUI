using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_WebApp.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [MaxLength(12)]
        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Account Number")]
        [Column(TypeName = "nvarchar(12)")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Beneficiary Name")]
        public string BeneficiaryName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "This field is required!")]
        [Column(TypeName = "nvarchar(11")]
        [Display(Name = "SWIFT Code")]
        public string SWIFTCode { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public int Amount { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/YYYY}")]
        public DateTime Date { get; set; }
    }
}