using System;
using ServicesCentral.Models;

namespace ServicesCentral.Repositories.Abstract
{
	public interface IFileService
    {
		Tuple<int, string> SaveImage(IFormFile imageFile);
		bool DeleteImage(string imageFileName);
		
	}
}

