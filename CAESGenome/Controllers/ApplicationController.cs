using System.Data;
using System.Linq;
using CAESGenome.Core.Domain;
using UCDArch.Web.Controller;
using UCDArch.Web.Attributes;

namespace CAESGenome.Controllers
{
    [Version(MajorVersion = 1)]
    public class ApplicationController : SuperController
    {
        public string ErrorMessage
        {
            get { return TempData[TEMP_DATA_ERROR_MESSAGE_KEY] as string; }
            set { TempData[TEMP_DATA_ERROR_MESSAGE_KEY] = value; }
        }

        private const string TEMP_DATA_ERROR_MESSAGE_KEY = "ErrorMessage";
        private const string UserKey = "UserKey";

        public User GetCurrentUser(bool forceReload = true)
        {
            var user = (User) Session[UserKey];

            if (user == null || forceReload)
            {
                user = Repository.OfType<User>().Queryable.FirstOrDefault(a => a.UserName == CurrentUser.Identity.Name);
                Session[UserKey] = user;
            }

            if (user == null)
            {
                throw new ObjectNotFoundException("User does not exist.");
            }

            return user;
        }
    }
}
