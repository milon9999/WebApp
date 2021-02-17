using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public interface IRateRepository
    {
        
        SelectList GetSelectRateList(DateTime? dt = null);
        SelectList GetAllHistoryDates();
    }
}
