using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Parameters
{
    public class VncPasswordConnectionParameter : StringConnectionParameter
    {
        public VncPasswordConnectionParameter()
        {
            Transformation = s => CryptHelper.EncryptPassword(s);
        }
    }
}
