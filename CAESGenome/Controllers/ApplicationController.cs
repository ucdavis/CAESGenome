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
    }
}
