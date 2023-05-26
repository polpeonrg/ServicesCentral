using System;
using System.ComponentModel.DataAnnotations.Schema;
using ServicesCentral.Models.Domain;
namespace ServicesCentral.Models;

public class ReserveListVm
{
    
    public IQueryable<Reservation> ReservationList { get; set; }
        
}

