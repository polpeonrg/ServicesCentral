using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesCentral.Models
{
	public class MarketListVm
	{
        
        public IQueryable<Market> MarketList { get; set; }
        
        public IQueryable<Services> ServicestList { get; set; }
        
        public int PageSize { get; set; }
        
        public int CurrentPage { get; set; }
        
        public int TotalPages { get; set; }
       
        public string? Term { get; set; }
       
        public string? Shop { get; set; }

        public string? Page { get; set; }
    }
}

