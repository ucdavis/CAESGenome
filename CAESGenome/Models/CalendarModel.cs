using System;
using System.Collections.Generic;

namespace CAESGenome.Models
{
    public class CalendarModel
    {
        public DateTime DateTime { get; set; }
        public IEnumerable<DateTime> HighlightDates { get; set; }
    }
}