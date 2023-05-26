using System;
using ServicesCentral.Models;

namespace ServicesCentral.Repositories.Abstract
{
	public interface IGenreService
	{
		bool Add(Services model);
		bool Update(Services model);
		Services GetByID(int id);
		bool Delete(int id);
		IQueryable<Services> List();
	}
}

