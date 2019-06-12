using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core
{
    public class ConnectionCmdLineGenerator
    {
        private readonly ConnectionDescription _connection;

        public ConnectionCmdLineGenerator(ConnectionDescription connection)
        {
            _connection = connection;
        }

        public string GetCommandLine()
        {
            var stringBuilder = new StringBuilder(); 
            stringBuilder.AppendFormat("\"{0}\"", _connection.GetEffectiveAppPath());

            stringBuilder.Append(" ");
            foreach (var p in _connection.Parameters.OrderBy(p => p.CmdLineOrder))
            {
                var pcmd = p.GetCommandLine();
                if (!string.IsNullOrEmpty(pcmd))
                {
                    stringBuilder.Append(pcmd);

                    var delimiter = p.NextParameterDelimiter ?? _connection.ParameterDelimiter;
                    stringBuilder.Append(delimiter);
                }
            }

            if (_connection.IsPaused)
            {
                stringBuilder.AppendFormat(" &pause");
            }

            return stringBuilder.ToString();
        } 
    }
}
