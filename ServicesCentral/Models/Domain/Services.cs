using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServicesCentral.Models
{
	public class Services
	{
        [Key]
        [DisplayName("Services'ID")]
        public int Id { get; set; }         //property ID is pamary key

        [Required] //ErrorMessage จะแสดงใน span ในไฟล์ view
        [DisplayName("Services genre name")]
        public string? GenreName { get; set; }    //property Name is not null

    }
}

