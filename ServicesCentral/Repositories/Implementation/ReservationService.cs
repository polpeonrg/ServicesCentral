using System;
using ServicesCentral.Data;
using ServicesCentral.Models;
using ServicesCentral.Models.Domain;
using ServicesCentral.Repositories.Abstract;

namespace ServicesCentral.Repositories.Implementation
{
	public class ReservationService : IReservationService
    {
        private readonly ApplicationDBContext ctx;
        private readonly IMarketService _marketService;
        private readonly IGenreService _genreService;
        public ReservationService(ApplicationDBContext ctx, IMarketService marketService, IGenreService genreService)
		{
            this.ctx = ctx;
            _marketService = marketService;
            _genreService = genreService;
        }

        public bool Add(Reservation model)
        {
            try
            {
                ctx.Reservation.Add(model);
                ctx.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(Reservation model)
        {
            try
            {
                ctx.Reservation.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Reservation GetByID(int id)
        {
            return ctx.Reservation.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetByID(id);
                if (data == null)
                    return false;
                ctx.Reservation.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ReserveListVm List()
        {
            var list = ctx.Reservation.ToList();
            var data = new ReserveListVm();

            foreach (var shop in list)
            {
                //*** Eng Date Format
                System.Globalization.CultureInfo _cultureTHInfo = new System.Globalization.CultureInfo("en-US"); //th-TH
                DateTime dateThai = Convert.ToDateTime(shop.ReserveDate, _cultureTHInfo);
                shop.ReserveDateTH = dateThai.ToString("dd MMM yyyy", _cultureTHInfo);

                var market = _marketService.GetByID(shop.MarketId);
                var serviceGenres = _marketService.GetGenreByMarketId(market.Id);
                var genreName = "";
                foreach (var lgenre in serviceGenres)
                {
                    var genres = _genreService.GetByID(lgenre);
                    genreName += ", " + genres.GenreName;
                }
                market.ServiceName = genreName == null ? "" : genreName.Remove(0, 2);

                shop.ShopName = market.ShopName;
                shop.ServiceName = market.ServiceName;
                shop.Price = market.Price;
                shop.Description = market.Description;
                shop.ShopImage = market.ShopImage;
                shop.ServiceName = market.ServiceName;
            }
            data.ReservationList = list.AsQueryable();
            return data;
        }

        

    }
}

