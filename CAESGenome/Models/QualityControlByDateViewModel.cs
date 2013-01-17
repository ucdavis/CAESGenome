using System;
using System.Collections.Generic;
using System.Linq;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using UCDArch.Core.PersistanceSupport;

namespace CAESGenome.Models
{
    public class QualityControlByDateViewModel
    {
        public IEnumerable<Barcode> Barcodes { get; set; }
        public DateTime Date { get; set; }

        public static QualityControlByDateViewModel Create(IRepositoryFactory repositoryFactory, DateTime date)
        {
            var viewModel = new QualityControlByDateViewModel()
                {
                    Barcodes = repositoryFactory.BarcodeRepository.Queryable.Where(a => a.DateTimeValidated.Value.Date == date.Date && a.BarcodeFiles.Any()),
                    Date = date
                };

            return viewModel;
        }
    }
}