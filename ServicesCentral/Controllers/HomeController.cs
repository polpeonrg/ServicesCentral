using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesCentral.Data;
using ServicesCentral.Models;
using ServicesCentral.Models.Domain;
using ServicesCentral.Repositories.Abstract;

namespace ServicesCentral.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDBContext _db; //สร้างตัวแทนของฐานข้อมูล
    private readonly IMarketService _marketService;
    private readonly IGenreService _genreService;
    private readonly IReservationService _reservationService;
    public HomeController(ApplicationDBContext db, IMarketService marketService, IGenreService genreService, IReservationService reservationService) //เมื่อคอนโทลเลอร์ตัวนี้ถูกใช้จะเซ็ตค่า _db เป็นApplicationDBContext
    {
        _db = db;
        _marketService = marketService;
        _genreService = genreService;
        _reservationService = reservationService;
    }

    public IActionResult Index(string term = "", string shop = "", int currentPage = 1)
    {
        // var data = _genreService.List();
        var data = _marketService.List(term, shop, true, currentPage);
        return View(data);
    }

    public IActionResult Services(string term="", string shop = "", int currentPage = 1)
    {
        var market = _marketService.List(term,shop, true, currentPage);
        return View(market);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Reserve(int marketId)
    {
        var data = new Reservation();
        var market = _marketService.GetByID(marketId);
        var serviceGenres = _marketService.GetGenreByMarketId(market.Id);
        var genreName = market.ServiceName;
        foreach (var lgenre in serviceGenres)
        {
            var genres = _genreService.GetByID(lgenre);
            genreName += ", " + genres.GenreName;
        }
        market.ServiceName = genreName == null ? "" : genreName.Remove(0, 2);

        data.MarketId = marketId;
        data.ShopName = market.ShopName;
        data.ServiceName =  market.ServiceName;
        data.Price = market.Price;
        data.Description =  market.Description;
        data.ShopImage = market.ShopImage;
        return View(data);
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Reserve(Reservation model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = _reservationService.Add(model);
        if (result)
        {
            TempData["msg"] = "Reserved Successfully";
            return RedirectToAction(nameof(ReserveList));
        }
        else
        {
            TempData["msg"] = "Error on server side";
            return View(model);
        }

    }

    public IActionResult Edit(int id)
    {
        var data = _reservationService.GetByID(id);
        var market = _marketService.GetByID(data.MarketId);
        var serviceGenres = _marketService.GetGenreByMarketId(market.Id);
        var genreName = market.ServiceName;
        foreach (var lgenre in serviceGenres)
        {
            var genres = _genreService.GetByID(lgenre);
            genreName += ", " + genres.GenreName;
        }
        market.ServiceName = genreName == null ? "" : genreName.Remove(0, 2);

        data.ShopName = market.ShopName;
        data.ServiceName = market.ServiceName;
        data.Price = market.Price;
        data.Description = market.Description;
        data.ShopImage = market.ShopImage;
        return View(data);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Reservation model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = _reservationService.Update(model);
        if (result)
        {
            TempData["msg"] = "Update Reserved Successfully";
            return RedirectToAction(nameof(ReserveList));
        }
        else
        {
            TempData["msg"] = "Error on server side";
            return View(model);
        }

    }

    public IActionResult ReserveList()
    {
        var data = this._reservationService.List();
        return View(data);
    }

    public IActionResult Delete(int id)
    {
        var result = _reservationService.Delete(id);
        return RedirectToAction(nameof(ReserveList));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}

