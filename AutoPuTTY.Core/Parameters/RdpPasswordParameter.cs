using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Parameters
{
    public class RdpPasswordConnectionParameter : StringConnectionParameter
    {
        public RdpPasswordConnectionParameter()
        {
            Transformation = s => CryptHelper.encryptpw(s);
        }
    }
}
