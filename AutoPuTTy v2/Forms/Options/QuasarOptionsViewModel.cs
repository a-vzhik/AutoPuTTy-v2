using AutoPuTTY.Forms.Options;
using AutoPuTTY.Properties;
using AutoPuTTY.Utils;

namespace AutoPuTTY.Forms.Options
{
    class QuasarOptionsViewModel : CommonOptionsViewModel
    {
        private XmlHelper xmlHelper;

        public QuasarOptionsViewModel(XmlHelper xmlHelper)
            : base("quasarpath", "quasarpath", Resources.formOptions_select_quasar_executable, xmlHelper)
        {
            this.xmlHelper = xmlHelper;
        }
    }
}