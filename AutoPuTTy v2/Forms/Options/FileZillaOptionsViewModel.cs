using AutoPuTTY.Properties;
using AutoPuTTY.Utils;

namespace AutoPuTTY.Forms.Options
{
    class FileZillaOptionsViewModel : CommonOptionsViewModel
    {
        public FileZillaOptionsViewModel(XmlHelper xmlHelper)
            : base("filezillapath", "filezillapath", Resources.formOptions_select_filezilla_executable, xmlHelper)
        {
                
        }
    }
}
