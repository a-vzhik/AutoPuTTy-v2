using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Parameters
{
    public class PuttyPasswordParameter : StringConnectionParameter
    {
        // for some reason you only have escape \ if it's followed by "
        // will "fix" up to 3 \ in a password like \\\", then screw you with your maniac passwords
        private static readonly string[] passs = { "\"", "\\\\\"", "\\\\\\\\\"", "\\\\\\\\\\\\\"", };
        private static readonly string[] passr = { "\\\"", "\\\\\\\"", "\\\\\\\\\\\"", "\\\\\\\\\\\\\\\"", };

        public PuttyPasswordParameter()
        {
            Transformation = val => OtherHelper.ReplaceA(passs, passr, val);
            IsQuoted = true;
            Name = KnownParameters.Password;
            DisplayName = "Пароль";
            CmdLineName = "-pw";
            Delimiter = " ";
            Value = string.Empty;

        }
    }
}
