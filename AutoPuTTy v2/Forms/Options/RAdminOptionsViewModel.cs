using AutoPuTTY.Properties;
using AutoPuTTY.Utils;

namespace AutoPuTTY.Forms.Options
{
    class RAdminOptionsViewModel : CommonOptionsViewModel
    {
        public RAdminOptionsViewModel(XmlHelper xmlHelper) : 
            base("radminpath", "radminpath", Resources.formOptions_select_radmin_executable, xmlHelper)
        {
        }
    }
}
