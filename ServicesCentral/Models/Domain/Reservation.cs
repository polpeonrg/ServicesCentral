using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServicesCentral.Models.Domain
{
	public class Reservation
	{
        [Key]
        [DisplayName("Reservation'ID")]
        public int Id { get; set; }

        [Required]
        public DateTime ReserveDate { get; set; }

        [Required]
        [DisplayName("Name")]
        public string? BookerName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Note { get; set; }

        public int MarketId { get; set; }

        [NotMapped]
        public string? ReserveDateTH { get; set; }
        [NotMapped]
        public string? ShopName { get; set; }
        [NotMapped]
        public string? ServiceName { get; set; }
        [NotMapped]
        public int Price { get; set; }
        [NotMapped]
        public string? Description { get; set; }
        [NotMapped]
        public string? ShopImage { get; set; }

    }
}

