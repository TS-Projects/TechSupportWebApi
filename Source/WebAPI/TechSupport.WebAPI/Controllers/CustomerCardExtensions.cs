using System.Linq;
using TechSupport.Data.Models;

namespace TechSupport.WebAPI.Api.CustomerCards.Controllers
{
    public static class CustomerCardExtensions
    {
        public static bool ShouldShowRegistrationForm(this CustomerCard contest)
        {
            // Show registration form if contest password is required
            var showRegistrationForm = contest.HasCustomerCardPassword;

            // Show registration form if contest is official and questions should be asked

            return showRegistrationForm;
        }
    }
}