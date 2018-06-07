using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Suporteri.Models
{
    [Table("production")]
    public class Production
    {
        
        public int Id { get; set; }

        [Key]
        public int ProductionId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

    }
}