using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Helpers;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;

namespace CAESGenome.Models
{
    public class QbotViewModel
    {
        public List<JobType> JobTypes { get; set; }
        public JobType JobType { get; set; }
        public QbotPostModel PostModel { get; set; }

        public SelectList RechargeAccounts { get; set; }
        public SelectList PlateTypes { get; set; }
        public SelectList Vectors { get; set; }
        public SelectList Strains { get; set; }

        public SelectList VectorTypes { get; set; }
        public SelectList Antibiotic1 { get; set; }
        public SelectList Antibiotic2 { get; set; }
        public SelectList Bacterias { get; set; }

        // replicating 
        public SelectList DestinationPlateTypes { get; set; }

        // gridding
        public SelectList GriddingPatterns { get; set; }

        public static QbotViewModel Create(IRepositoryFactory repositoryFactory, User user, JobType jobType = null, QbotPostModel postModel = null)
        {
            var viewModel = new QbotViewModel()
                {
                    JobType = jobType,
                    JobTypes = jobType == null ? repositoryFactory.JobTypeRepository.Queryable.Where(a => a.Qbot).ToList() : new List<JobType>(),
                    PostModel = postModel ?? new QbotPostModel()
                };

            if (jobType != null)
            {
                var rid = postModel != null && postModel.RechargeAccount != null ? postModel.RechargeAccount.Id : -1;
                viewModel.RechargeAccounts = new SelectList(user.RechargeAccounts, "Id", "AccountNum", rid);

                var vid = postModel != null && postModel.Vector != null ? postModel.Vector.Id : -1;
                viewModel.Vectors = new SelectList(repositoryFactory.VectorRepository.Queryable.OrderByDescending(a => a.Name), "Id", "Name", vid);

                var sid = postModel != null && postModel.Strain != null ? postModel.Strain.Id : -1;
                viewModel.Strains = new SelectList(repositoryFactory.StrainRepository.Queryable.Where(a => a.Supplied), "Id", "Name", sid);

                var vtid = postModel != null && postModel.VectorType != null ? postModel.VectorType.Id : -1;
                viewModel.VectorTypes = new SelectList(repositoryFactory.VectorTypeRepository.GetAll(), "Id", "Name", vtid);

                var ab1 = postModel != null && postModel.Antibiotic1 != null ? postModel.Antibiotic1.Id : -1;
                viewModel.Antibiotic1 = new SelectList(repositoryFactory.AntibioticRepository.Queryable.OrderBy(a => a.Name), "Id", "Name", ab1);

                var ab2 = postModel != null && postModel.Antibiotic2 != null ? postModel.Antibiotic2.Id : -1;
                viewModel.Antibiotic2 = new SelectList(repositoryFactory.AntibioticRepository.Queryable.OrderBy(a => a.Name), "Id", "Name", ab2);

                var bid = postModel != null && postModel.Bacteria != null ? postModel.Bacteria.Id : -1;
                viewModel.Bacterias = new SelectList(repositoryFactory.BacteriaRepository.GetAll(), "Id", "Name", bid);

                if (jobType.Id == (int)JobTypeIds.QbotColonyPicking)
                {
                    var pid = postModel != null ? postModel.PlateType : null;

                    var pts = new List<SelectListItem>();
                    pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.QTray).ToString(), Text = EnumUtility.GetEnumDescription(Core.Resources.PlateTypes.QTray) });
                    pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.GlycerolStock).ToString(), Text = EnumUtility.GetEnumDescription(Core.Resources.PlateTypes.GlycerolStock) });
                    viewModel.PlateTypes = new SelectList(pts, "Value", "Text", pid);
                }

                if (jobType.Id == (int)JobTypeIds.QbotPlateReplicating || jobType.Id == (int)JobTypeIds.QbotGridding)
                {
                    var pid = postModel != null ? postModel.PlateType : null;

                    var pts = new List<SelectListItem>();
                    pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.NinetySix).ToString(), Text = EnumUtility.GetEnumDescription(Core.Resources.PlateTypes.NinetySix) });
                    pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.ThreeEightyFour).ToString(), Text = EnumUtility.GetEnumDescription(Core.Resources.PlateTypes.ThreeEightyFour) });
                    viewModel.PlateTypes = new SelectList(pts, "Value", "Text", pid);
                }

                if (jobType.Id == (int)JobTypeIds.QbotPlateReplicating)
                {
                    var pid = postModel != null ? postModel.DestinationPlateType : null;

                    var pts = new List<SelectListItem>();
                    pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.NinetySix).ToString(), Text = EnumUtility.GetEnumDescription(Core.Resources.PlateTypes.NinetySix) });
                    pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.ThreeEightyFour).ToString(), Text = EnumUtility.GetEnumDescription(Core.Resources.PlateTypes.ThreeEightyFour) });
                    viewModel.DestinationPlateTypes = new SelectList(pts, "Value", "Text", pid);
                }

                if (jobType.Id == (int)JobTypeIds.QbotGridding)
                {
                    var pid = postModel != null ? postModel.GriddingPattern : null;

                    var pts = new List<SelectListItem>();
                    pts.Add(new SelectListItem(){ Value=((int)GriddingPattern.ThreeXThree).ToString(), Text = EnumUtility.GetEnumDescription(GriddingPattern.ThreeXThree)});
                    pts.Add(new SelectListItem() { Value = ((int)GriddingPattern.FourXFour).ToString(), Text = EnumUtility.GetEnumDescription(GriddingPattern.FourXFour) });
                    pts.Add(new SelectListItem() { Value = ((int)GriddingPattern.FiveXFive).ToString(), Text = EnumUtility.GetEnumDescription(GriddingPattern.FiveXFive) });

                    viewModel.GriddingPatterns = new SelectList(pts, "Value", "Text", pid);
                }
            }

            return viewModel;
        }

        public bool JobTypeSelected()
        {
            return JobType != null;
        }
    }
}