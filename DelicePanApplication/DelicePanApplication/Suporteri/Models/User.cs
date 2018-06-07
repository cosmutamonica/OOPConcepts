using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Suporteri.Models
{
    [Table("user")]
    public class User
    {        
        [ScaffoldColumn(false)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Key]      
        [ScaffoldColumn(false)]
        [Display(Name = "UserId")]
        public string UserId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [Column("name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Persoana fizica/juridica")]
        public string Type { get; set; }
    }
}