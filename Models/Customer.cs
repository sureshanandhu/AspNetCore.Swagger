using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AspNetCore.Swagger.WebApi.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("customer_id")]
        [Required]
        public string CustomerId { get; set; }

        [Column("customer_name")]
        [Required]
        public string CustomerName { get; set; }
    }
}