using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesCentral.Models;
using ServicesCentral.Data;
using ServicesCentral.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServicesCentral.Controllers
{
    public class MarketController : Controller
    {
        // GET: /<controller>/
        private readonly IMarketService _marketService;
        private readonly IFileService _fileService;
        private readonly IGenreService _genreService;

        public MarketController(IMarketService MarketService, IFileService fileService, IGenreService genreService)
        {
            _marketService = MarketService;
            _fileService = fileService;
            _genreService = genreService;
        }

        public IActionResult Add()
        {
            var model = new Market();
            model.ServicesList = _genreService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString()});
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Market model)
        {
            model.ServicesList = _genreService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            if (!ModelState.IsValid)
                return View(model);
              if(model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.ShopImage = imageName;
            }
            var result = _marketService.Add(model);
           if(result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
           else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        public IActionResult Edit(int id)
        {
            var model = _marketService.GetByID(id);
            var selectGenres = _marketService.GetGenreByMarketId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genreService.List(), "Id", "GenreName", selectGenres);
            model.MultiGenreList = multiGenreList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Market model)
        {
            var selectGenres = _marketService.GetGenreByMarketId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genreService.List(), "Id", "ServiceName", selectGenres);
            model.MultiGenreList = multiGenreList;
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.ShopImage = imageName;
            }
            var result = _marketService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(MarketList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult MarketList()
        {
            var data = this._marketService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _marketService.Delete(id);
            return RedirectToAction(nameof(MarketList));
        }

    }
}

