using CAESGenome.Core.Domain;

namespace CAESGenome.Models
{
    public class RechargeAccountViewModel
    {
        public RechargeAccount RechargeAccount { get; set; }

        public static RechargeAccountViewModel Create(RechargeAccount rechargeAccount = null)
        {
            var viewModel = new RechargeAccountViewModel() {RechargeAccount = rechargeAccount ?? new RechargeAccount() };
            return viewModel;
        }

    }
}