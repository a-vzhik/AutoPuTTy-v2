using AutoPuTTY.Forms.Options;
using AutoPuTTY.Properties;
using AutoPuTTY.Utils;

namespace AutoPuTTY.Forms.Options
{
    internal class FarNetboxOptionsViewModel : CommonOptionsViewModel
    {
        private XmlHelper xmlHelper;

        public FarNetboxOptionsViewModel(XmlHelper xmlHelper) 
            : base("farnetboxpath", "farnetboxpath", Resources.formOptions_select_farnetbox_executable, xmlHelper)
        {
            this.xmlHelper = xmlHelper;
        }
    }
}