using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServicesCentral.Models
{
    public class Market
    {
        [Key]
        [DisplayName("Market'ID")]
        public int Id { get; set; }         //property ID is pamary key

        [Required] //ErrorMessage จะแสดงใน span ในไฟล์ view
        [DisplayName("Shop Name")]
        public string? ShopName { get; set; }    //property Name is not null

        [Required]
        [Range(0, 9999)]						
        public int Price { get; set; }		//property

        public string? Description { get; set; }

        public string? ShopImage { get; set; } // stores shop image name with extension (eg, image0001.jpg)

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Services { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? ServicesList { get; set; }
        [NotMapped]
        public string? ServiceName { get; set; }
        [NotMapped]
        public MultiSelectList? MultiGenreList { get; set; }
    }
}

