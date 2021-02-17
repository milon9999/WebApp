using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class Convertor
    {
        [DisplayName("From:")]
        public SelectList From { get; set; }
        [DisplayName("To:")]
        public SelectList To { get; set; }
        [DisplayName("Amount:")]
        public double Amount { get; set; }
        [DisplayName("Date:")]
        public SelectList Dates { get; set; }
        [DisplayName("Result:")]
        public double Result { get; set; }
    }
}
