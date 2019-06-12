using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core
{
    public class ConnectionGroupInstance
    {
        public string Name { get; set; }

        public IList<ConnectionParameterIntance> Parameters { get; set; }

        public IList<ConnectionInstance> Connections { get; set; }
    }
}
