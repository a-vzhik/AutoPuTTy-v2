using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Parameters
{
    public class WinScpEncodedParameter : StringConnectionParameter
    {
        public WinScpEncodedParameter()
        {
            string[] s = { "%", " ", "+", "/", "@", "\"", ":", ";" };

            Transformation = val => OtherHelper.ReplaceU(s, val);
            Value = string.Empty;
        }
    }
}
