using System;
using ServicesCentral.Data;
using ServicesCentral.Models;
using ServicesCentral.Repositories.Abstract;

namespace ServicesCentral.Repositories.Implementation
{
	public class GenreService : IGenreService
	{
        private readonly ApplicationDBContext ctx;
		public GenreService(ApplicationDBContext ctx)
		{
            this.ctx = ctx;
		}

        public bool Add(Services model)
        {
            try
            {
                ctx.Services.Add(model);
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
                ctx.Services.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Services GetByID(int id)
        {
            return ctx.Services.Find(id);
        }

        public IQueryable<Services> List()
        {
            var data = ctx.Services.AsQueryable();
            return data;
        }

        public bool Update(Services model)
        {
            try
            {
                ctx.Services.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

