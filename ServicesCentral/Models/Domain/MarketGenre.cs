using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServicesCentral.Models
{
    public class MarketGenre
    {
        public int Id { get; set; }
        public int MarketID { get; set; }
        public int ServicesID { get; set; }
    }
}

