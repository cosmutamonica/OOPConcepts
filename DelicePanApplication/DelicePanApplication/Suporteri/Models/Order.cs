using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Suporteri.Models
{
    [Table("orders")]
    public class Order
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string userId { get; set; }

        [Display(Name = "Utilizator")]
        public string UserEmail { get; set; }

        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        [Required, Display(Name = "Produs")]
        public string ProductName { get; set; }

        [Required, Display(Name = "Data livrarii")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Data comenzii")]
        public DateTime OrderDate { get; set; }

        [Required, Display(Name = "Livrare")]
        public string DeliveryMethod { get; set; }

        [Required, Display(Name = "Cantitate")]
        [Range(0, 5000, ErrorMessage = "Introduceti o cantitate valida.")]
        public int Quantity { get; set; }

        [Display(Name = "Pret/produs")]        
        public double? UnitPrice { get; set; }

        [Display(Name = "Total")]
        public double? TotalPrice { get; set; }

        public string Category { get; set; }
    }
}