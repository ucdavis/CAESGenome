using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Models;
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

        [HttpPost]
        public ActionResult Reserve(int id, EquipmentReservation equipmentReservation)
        {
            var equipment = _repositoryFactory.EquipmentRepository.GetNullableById(id);

            equipmentReservation.Equipment = equipment;
            equipmentReservation.User = GetCurrentUser(true);

            ModelState.Clear();
            equipmentReservation.TransferValidationMessagesTo(ModelState);

            // check for conflicting reservations
            if (_repositoryFactory.EquipmentReservationRepository.Queryable.Any(a => a.Start <= equipmentReservation.Start && equipmentReservation.End <= a.End))
            {
                ModelState.AddModelError("EquipmentReservation.Time", "There is a conflict with the requested times.  Please select a new time and try again.");
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

        public ActionResult Reservations()
        {
            var user = GetCurrentUser();
            var reservations = _repositoryFactory.EquipmentReservationRepository.Queryable.Where(a => a.User == user).OrderBy(a => a.Start);
            return View(reservations);
        }

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
    }
}
