using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Services;

namespace CAESGenome.Controllers
{
    public class TestController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPhredService _phredService;

        public TestController(IRepositoryFactory repositoryFactory, IPhredService phredService)
        {
            _repositoryFactory = repositoryFactory;
            _phredService = phredService;
        }

        public ActionResult Index()
        {
            //var barcodeFile = _repositoryFactory.BarcodeFileRepository.GetById(12);
            
            //var scoreList = ReadPhredFile(2020717, barcodeFile);

            //int start = 0, end = 0;
            //FindIndexes(scoreList, out start, out end);

            //ViewBag.ScoreListLength = scoreList.Count();
            //ViewBag.ScoreList = scoreList;
            //ViewBag.Start = start;
            //ViewBag.End = end;

            _phredService.ScanFiles();

            return View();
        }

        //private string _storageLocation = ConfigurationManager.AppSettings["StorageLocation"];
        //private int[] ReadPhredFile(int barcode, BarcodeFile barcodeFile)
        //{
        //    var path = string.Format(@"{0}\output\{1}\{2}", _storageLocation, barcode, barcodeFile.ValidationFileName);

        //    if (System.IO.File.Exists(path))
        //    {
        //        var lines = System.IO.File.ReadAllLines(path);
        //        lines[0] = string.Empty;
        //        var line = string.Join(" ", lines);

        //        var values = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        //        var numbers = new int[values.Count()];
        //        for (var i = 0; i < values.Count(); i++)
        //        {
        //            numbers[i] = Convert.ToInt32(values[i].Trim());
        //        }

        //        return numbers;
        //    }

        //    return new int[0];
        //}

        //// fixed values, for finding indexes
        //private const int StartTrigger = 20;
        //private const int WindowSize = 10;
        //private const int EndTrigger = 15;

        //private void FindIndexes(int[] numbers, out int start, out int end)
        //{
        //    start = FindStartIndex(numbers);
        //    end = FindEndIndex(numbers, start);
        //}
        //private int FindStartIndex(int[] numbers)
        //{
        //    var windowStart = 0;
        //    var openWindow = false;
        //    var windowCount = 0;

        //    // find the start index
        //    for (var i = 0; i < numbers.Length; i++)
        //    {
        //        // number meets criteria for open
        //        if (numbers[i] >= StartTrigger)
        //        {
        //            // closed window, open up the window
        //            if (!openWindow)
        //            {
        //                windowStart = i;
        //                openWindow = true;
        //            }
                    
        //            // increase window size
        //            windowCount++;

        //            // window satisfied
        //            if (windowCount == WindowSize)
        //            {
        //                return windowStart;
        //            }
        //        }

        //        // number below trigger, close the window
        //        if (numbers[i] < StartTrigger && openWindow)
        //        {
        //            openWindow = false;
        //            windowCount = 0;
        //        }
        //    }

        //    // found nothing
        //    return 0;
        //}
        //private int FindEndIndex(int[] numbers, int startIndex)
        //{
        //    var windowStart = 0;
        //    var openWindow = false;
        //    var windowCount = 0;

        //    // find the start index to close
        //    for (var i = startIndex; i < numbers.Length; i++ )
        //    {
        //        if (numbers[i] < EndTrigger)
        //        {
        //            // closed window, open up the window
        //            if (!openWindow)
        //            {
        //                windowStart = i;
        //                openWindow = true;
        //            }
                    
        //            // just count to increase window
        //            windowCount++;

        //            // window satisfied
        //            if (windowCount >= WindowSize)
        //            {
        //                return windowStart;
        //            }
        //        }

        //        if (numbers[i] >= EndTrigger && openWindow)
        //        {
        //            openWindow = false;
        //            windowCount = 0;
        //        }
        //    }

        //    return 0;
        //}
    }
}
