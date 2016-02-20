using System.Linq;
using TechSupport.Data.Models;

namespace TechSupport.WebAPI.Api.CustomerCards.Controllers
{
    public static class CustomerCardExtensions
    {
        public static bool ShouldShowRegistrationForm(this CustomerCard contest, bool isOfficialParticipant)
        {
            // Show registration form if contest password is required
            bool showRegistrationForm = isOfficialParticipant && contest.HasCustomerCardPassword;

            // Show registration form if contest is official and questions should be asked
            if (isOfficialParticipant)
            {
                showRegistrationForm = true;
            }

            return showRegistrationForm;
        }
    }
}