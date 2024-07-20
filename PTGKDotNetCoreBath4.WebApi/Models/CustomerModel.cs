using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTGKDotNetCoreBath4.WebApi.Models
{
    [Table("Tbl_Customer")]
    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; } 
        public string? CustomerName { get; set; }
        public string? PhoneNo { get; set; }   
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? CustomerCode { get; set; }
                     
    }
}
