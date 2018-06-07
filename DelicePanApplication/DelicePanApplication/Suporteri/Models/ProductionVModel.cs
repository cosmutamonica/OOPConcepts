using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Suporteri.Models
{
    public class ProductionVModel
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required, StringLength(100), Display(Name = "Denumire")]
        public string ProductName { get; set; }

        [Required, StringLength(10000), Display(Name = "Descriere"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Required, Display(Name = "Pret")]
        [Range(0, 1000, ErrorMessage = "Introduceti un pret valid.")]
        public double? UnitPrice { get; set; }

        [Display(Name = "Cantitate")]
        public int Quantity { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        public string Category { get; set; }
    }
}