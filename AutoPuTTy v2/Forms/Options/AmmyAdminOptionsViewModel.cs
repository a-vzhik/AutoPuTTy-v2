using AutoPuTTY.Properties;
using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Forms.Options
{
    class AmmyAdminOptionsViewModel : CommonOptionsViewModel
    {
        public AmmyAdminOptionsViewModel(XmlHelper xmlHelper)
            : base("ammyadminpath", "ammyadminpath", Resources.formOptions_select_ammyadmin_executable, xmlHelper)
        {     
        }
    }
}
