using System;
using ServicesCentral.Models;

namespace ServicesCentral.Repositories.Abstract
{
	public interface IMarketService
    {
		bool Add(Market model);
		bool Update(Market model);
        Market GetByID(int id);
		bool Delete(int id);
		MarketListVm List(string term = "", string shop = "", bool paging = false, int currentPage = 0);
		List<int> GetGenreByMarketId(int marketID);

    }
}

