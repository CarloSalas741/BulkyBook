using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public  class ApplicationUser: IdentityUser
    {
        [Required, StringLength(80)]
        public string Name { get; set; }
        [StringLength(300)]
        public string? StreetAdress{ get; set; }
        [StringLength(300)]
        public string? City { get; set; }
        [StringLength(200)]
        public string? State { get; set; }
        [StringLength(30)]
        public string? PostalCode{ get; set; }

        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
    }
}
