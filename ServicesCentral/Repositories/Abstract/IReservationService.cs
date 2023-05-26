using System;
using ServicesCentral.Models;
using ServicesCentral.Models.Domain;

namespace ServicesCentral.Repositories.Abstract
{
	public interface IReservationService
    {
		bool Add(Reservation model);
		bool Update(Reservation model);
        Reservation GetByID(int id);
		bool Delete(int id);
        ReserveListVm List();
    }
}

