using System.Collections.Generic;
using System.Linq;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;

namespace CAESGenome.Models
{
    public class EquipmentViewModel
    {
        public List<Equipment> Equipments { get; set; }
        public Equipment Equipment { get; set; }
        public EquipmentReservation EquipmentReservation { get; set; }

        public static EquipmentViewModel Create(IRepositoryFactory repositoryFactory, Equipment equipment = null, EquipmentReservation equipmentReservation = null)
        {
            var viewModel = new EquipmentViewModel()
                {
                    Equipment = equipment,
                    EquipmentReservation = equipmentReservation ?? new EquipmentReservation()
                };

            if (equipment == null)
            {
                viewModel.Equipments = repositoryFactory.EquipmentRepository.Queryable.Where(a => a.Operator.ToLower() == "user").OrderBy(a => a.Name).ToList();
            }

            return viewModel;
        }

    }
}