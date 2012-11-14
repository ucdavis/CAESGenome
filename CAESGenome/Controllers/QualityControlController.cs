﻿using System;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Repositories;

namespace CAESGenome.Controllers
{
    public class QualityControlController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public QualityControlController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Displays calendars of quality control
        /// </summary>
        /// <param name="month">Leading Month</param>
        /// <param name="year">Leading Year</param>
        /// <param name="direction">Direction to change calendar</param>
        /// <returns></returns>
        public ActionResult Index(int? month, int? year, CalendarDirection? direction = null)
        {
            month = month ?? DateTime.Now.Month;
            year = year ?? DateTime.Now.Year;
            var date = new DateTime(year.Value, month.Value, 1);

            if (direction != null)
            {
                if (direction == CalendarDirection.Left)
                {
                    date = date.AddMonths(-1);
                }
                else
                {
                    if (date.AddMonths(1) < DateTime.Now)
                    {
                        date = date.AddMonths(1);    
                    }
                }
            }

            var barcodes =
                _repositoryFactory.BarcodeRepository.Queryable.Where(
                    a =>
                    a.Done && a.DateTimeValidated.HasValue && a.DateTimeValidated.Value.Date >= date.AddMonths(-2).Date && a.DateTimeValidated.Value < date.AddMonths(1)
                    ).Select(a => a.DateTimeValidated.Value.Date).Distinct();

            ViewBag.Month = date.Month;
            ViewBag.Year = date.Year;

            return View(barcodes);
        }

        public enum CalendarDirection {Left, Right}
    }
}
