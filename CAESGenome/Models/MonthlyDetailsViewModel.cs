using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using CAESGenome.Core.Domain;

namespace CAESGenome.Models
{
    /// <summary>
    /// Class to contain user jobs, plus month and year the corresponding jobs apply to.
    /// </summary>
    public class MonthlyDetailsViewModel
    {
        public List<UserJob> Jobs { get; set; }

        /// <summary>
        /// Slightly tricky method that takes an integer 1-12, and 
        /// returns the first 3 characters of the corresponding month's name Jan-Dec.
        /// </summary>
        private int? _month;
        public object Month
        {
            get
            {
                var retval = string.Empty;

                if (_month != null && _month > 0)
                {
                    var dateFormatter = new System.Globalization.DateTimeFormatInfo();
                    retval = dateFormatter.GetMonthName((int)_month).Substring(0,3);
                }

                return retval;
            }
            set { _month = value as int?; }
        }

        public int Year { get; set; }
    }
}