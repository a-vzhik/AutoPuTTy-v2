using AutoPuTTY.Properties;
using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Forms.Options
{
    class TeamViewerOptionsViewModel : CommonOptionsViewModel
    {
        public TeamViewerOptionsViewModel(XmlHelper xmlHelper) 
            : base("teamviewerpath", "teamviewerpath",  Resources.formOptions_select_teamviewer_executable, xmlHelper)
        {

        }
    }
}
