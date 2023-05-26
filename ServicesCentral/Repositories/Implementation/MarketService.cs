using System;
using ServicesCentral.Data;
using ServicesCentral.Models;
using ServicesCentral.Repositories.Abstract;

namespace ServicesCentral.Repositories.Implementation
{
	public class MarketService : IMarketService
	{
        private readonly ApplicationDBContext ctx;
		public MarketService(ApplicationDBContext ctx)
		{
            this.ctx = ctx;
		}

        public bool Add(Market model)
        {
            try
            {
                ctx.Market.Add(model);
                ctx.SaveChanges();
                foreach (var servicesID in model.Services)
                {
                    var marketGenre = new MarketGenre
                    {
                        MarketID = model.Id,
                        ServicesID = servicesID
                    };
                    ctx.MarketGenre.Add(marketGenre);
                }
                ctx.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetByID(id);
                if (data == null)
                    return false;
                var marketGenres = ctx.MarketGenre.Where(a => a.MarketID == data.Id);
                foreach(var service in marketGenres)
                {
                    ctx.MarketGenre.Remove(service);
                }
                ctx.Market.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Market GetByID(int id)
        {
            return ctx.Market.Find(id);
        }

        public MarketListVm List(string term="", string shop = "", bool paging = false,int currentPage=0)
        {
            var list = ctx.Market.ToList();
            var lservice = ctx.Services.ToList();

            var data = new MarketListVm();

            if (paging)
            {
                //apply pging
                int pageSize = 5;
                int count = lservice.Count;
                int TotalPage = (int)Math.Ceiling(count / (double)pageSize);
                lservice = lservice.Skip((currentPage - 1)*pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPage;
            }

            foreach (var market in list)
            {
                var genres = (from sc in ctx.Services
                              join mg in ctx.MarketGenre
                              on sc.Id equals mg.ServicesID
                              where mg.MarketID == market.Id
                              select sc.GenreName).ToList();
                var genreName = string.Join(',', genres);
                market.ServiceName = genreName;
            }
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.ServiceName.ToLower().Contains(term)).ToList();
            }
            if (!string.IsNullOrEmpty(shop))
            {
                shop = shop.ToLower();
                //list = list.Where(a => a.ShopName.ToLower().StartsWith(shop)).ToList();
                list = list.Where(a => a.ShopName.ToLower().Contains(shop)).ToList();
            }

            data.MarketList = list.AsQueryable();
            data.ServicestList = lservice.AsQueryable();
            data.Shop = shop;
            data.Term = term;
            /*
            var data = new MarketListVm
            {
                MarketList = list.AsQueryable()
            };
            */
            return data;
        }

        public bool Update(Market model)
        {
            try
            {
                //these serviceId are not selected by users and still present should be removed
                var serviceToDeleted = ctx.MarketGenre.Where(a => a.MarketID == model.Id && !model.Services.Contains(a.ServicesID)).ToList();
                foreach(var marketG in serviceToDeleted)
                {
                    ctx.MarketGenre.Remove(marketG);
                }
                foreach(int serviceId in model.Services)
                {
                    var marketGenre = ctx.MarketGenre.FirstOrDefault(a => a.MarketID == model.Id && a.ServicesID == serviceId);
                    if(marketGenre == null)
                    {
                        marketGenre = new MarketGenre { ServicesID = serviceId, MarketID = model.Id };
                        ctx.MarketGenre.Add(marketGenre);
                    }
                }
                //add service id in marketGenre table
                ctx.Market.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetGenreByMarketId(int marketID)
        {
            var genreIds = ctx.MarketGenre.Where(a => a.MarketID == marketID).Select(a => a.ServicesID).ToList();
            return genreIds;
        }

       
    }
}

