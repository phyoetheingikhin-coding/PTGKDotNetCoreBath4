using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTGKDotNetCoreBath4.ConsoleApp.Dto;

[Table("Tbl_Customer")]
public class CustomerDto
{
    [Key]
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNo { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }
    public string CustomerCode { get; set; }
}
