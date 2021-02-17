using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Modules.WebApp;

namespace WebApp.Models
{
    public class MockRateRepository : IRateRepository
    {
        private readonly FileImporter _fileImpoerter;

        //OnFirstTime contructor collecting data from file
        //TODO: read data from database
        public MockRateRepository(IConfiguration config)
        {
            var dir = config["ImportPath"];
            var daysShowCount = config["DaysShowCount"];
            _fileImpoerter = new FileImporter(dir, daysShowCount);
        }

        public SelectList GetSelectRateList(DateTime? dt = null)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (_fileImpoerter.Dates.Count > 0)
            {
                //OnFirstTime there we are taking first date data list
                if (dt == null)
                {
                    foreach (var rate in _fileImpoerter.Dates.FirstOrDefault().Value)
                    {
                        list.Add(new SelectListItem()
                        {
                            Text = rate.Name.ToString(CultureInfo.CurrentCulture),
                            Value = rate.Value.ToString(CultureInfo.CurrentCulture)
                        });
                    }
                    return new SelectList(list, "Value", "Text");
                }
                //Taking data list by selected date
                foreach (var rate in _fileImpoerter.Dates[(DateTime)dt])
                {
                    list.Add(new SelectListItem()
                    {
                        Text = rate.Name.ToString(CultureInfo.CurrentCulture),
                        Value = rate.Value.ToString(CultureInfo.CurrentCulture)
                    });
                }
            }
            return new SelectList(list, "Value", "Text");
        }

        public SelectList GetAllHistoryDates()
        {
            List<SelectListItem> list = new List<SelectListItem>();


            if (_fileImpoerter.Dates.Count > 0)
            {
                //Collect all dates data
                foreach (var date in _fileImpoerter.Dates)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = date.Key.ToString("yyyy-MM-dd"),
                        Value = date.Key.ToString("yyyy-MM-dd")
                    });
                }
            }
            return new SelectList(list, "Value", "Text");
        }
    }

}
