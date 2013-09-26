using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;
using UCDArch.Web.ActionResults;
using UCDArch.Web.Helpers;

namespace CAESGenome.Controllers
{
    public class EquipmentController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public EquipmentController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        [Authorize(Roles=RoleNames.User)]
        public ActionResult Reserve(int? id)
        {
            Equipment equipment = null;
            if (id.HasValue)
            {
                equipment = _repositoryFactory.EquipmentRepository.GetNullableById(id.Value);
            }

            var viewModel = EquipmentViewModel.Create(_repositoryFactory, equipment);
            return View(viewModel);
        }

        [Authorize(Roles = RoleNames.User)]
        [HttpPost]
        public ActionResult Reserve(int id, EquipmentReservation equipmentReservation)
        {
            var equipment = _repositoryFactory.EquipmentRepository.GetNullableById(id);

            equipmentReservation.Equipment = equipment;
            equipmentReservation.User = GetCurrentUser(true);

            ModelState.Clear();
            equipmentReservation.TransferValidationMessagesTo(ModelState);

            // check for conflicting reservations
            //if (_repositoryFactory.EquipmentReservationRepository.Queryable.Any(a => a.Start <= equipmentReservation.Start && equipmentReservation.End <= a.End))
            if (_repositoryFactory.EquipmentReservationRepository.Queryable.Any(a => 
                (a.Start <= equipmentReservation.End && a.End >= equipmentReservation.Start && !a.Cancelled)))
            {
                ModelState.AddModelError("EquipmentReservation.Start", "There is a conflict with the requested times.  Please select a new time and try again.");
            }

            if (ModelState.IsValid)
            {
                _repositoryFactory.EquipmentReservationRepository.EnsurePersistent(equipmentReservation);

                Message = "Equipment Reservation has been made.";
                return RedirectToAction("Index", "Authorized");
            }

            var viewModel = EquipmentViewModel.Create(_repositoryFactory, equipment, equipmentReservation);
            return View(viewModel);
        }

        [Authorize(Roles = RoleNames.User)]
        public ActionResult Reservations()
        {
            var user = GetCurrentUser();
            var reservations = _repositoryFactory.EquipmentReservationRepository.Queryable.Where(a => a.User == user).OrderBy(a => a.Start).ToList();
            return View(reservations);
        }

        [Authorize(Roles = RoleNames.User)]
        public ActionResult Cancel(int id)
        {
            var reservation = _repositoryFactory.EquipmentReservationRepository.GetNullableById(id);
            
            if (reservation == null)
            {
                Message = "Unable to load requested reservation.";
                return RedirectToAction("Reservations");
            }

            return View(reservation);
        }

        [Authorize(Roles = RoleNames.User)]
        [HttpPost]
        public ActionResult Cancel(int id, bool? cancel)
        {
            var reservation = _repositoryFactory.EquipmentReservationRepository.GetNullableById(id);

            if (reservation == null)
            {
                Message = "Unable to load requested reservation.";
                return RedirectToAction("Reservations");
            }

            reservation.Cancelled = true;
            _repositoryFactory.EquipmentReservationRepository.EnsurePersistent(reservation);

            Message = "Reservation has been cancelled.";
            return RedirectToAction("Reservations");
        }

        [Authorize(Roles = RoleNames.Staff)]
        public ActionResult Schedule()
        {
            var equipment = _repositoryFactory.EquipmentRepository.Queryable.Where(a => a.Operator == EquipmentOperators.User && a.IsReservable).ToList();
            return View(equipment);
        }

        [Authorize(Roles=RoleNames.Staff)]
        public ActionResult Usage()
        {
            var equipment = _repositoryFactory.EquipmentRepository.Queryable.Where(a => a.Operator == EquipmentOperators.User && a.IsReservable).ToList();
            return View(equipment);
        }

        [Authorize(Roles = RoleNames.Staff)]
        public JsonNetResult LoadUsageDetails(int year, int month)
        {
            var results = new List<UsageDetailModel>();

            var equipment = _repositoryFactory.EquipmentRepository.Queryable.Where(a => a.Operator == EquipmentOperators.User && a.IsReservable);
            foreach(var eq in equipment)
            {
                var details = new UsageDetailModel();
                details.EquipmentName = eq.Name;

                foreach (var res in eq.EquipmentReservations.Where(a => a.Start.Year == year && a.Start.Month == month))
                {
                    details.Reservations.Add(new ReservationDetailModel() {Start = res.Start.ToString(), End = res.End.ToString(), UserId = res.User.Id, UserName = res.User.FullName});
                }

                results.Add(details);
            }
            
            return new JsonNetResult(results);
        }

        [Authorize(Roles = RoleNames.Staff)]
        public ActionResult UserUsageDetails(int id)
        {
            var user = _repositoryFactory.UserRepository.GetNullableById(id);

            if (user == null)
            {
                Message = "Unable to find user.";
                return RedirectToAction("Usage");
            }

            ViewBag.UserName = user.FullName;

            var reservations = _repositoryFactory.EquipmentReservationRepository.Queryable.Where(a => a.User == user && !a.Cancelled).ToList();
            return View(reservations);
        }
    }

    public class UsageDetailModel
    {
        public UsageDetailModel()
        {
            Reservations = new List<ReservationDetailModel>();
        }

        public string EquipmentName { get; set; }
        public List<ReservationDetailModel> Reservations { get; set; }
    }
    public class ReservationDetailModel
    {
        public string Start { get; set; }
        public string End { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
