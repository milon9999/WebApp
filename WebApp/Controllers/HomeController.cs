using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRateRepository _rateRepository;

        public HomeController(IConfiguration configRoot)
        {
            _rateRepository = new MockRateRepository(configRoot);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new Convertor();
            model.From = model.To = _rateRepository.GetSelectRateList();
            model.Dates = _rateRepository.GetAllHistoryDates();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string dates, string amount)
        {
            var model = new Convertor();
            model.From = model.To = _rateRepository.GetSelectRateList(DateTime.Parse(dates));
            model.Dates = _rateRepository.GetAllHistoryDates();
            model.Amount = Convert.ToDouble(amount);
            return View(model);
        }
    }
}
