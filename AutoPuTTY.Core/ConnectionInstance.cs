using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core
{
    public class ConnectionInstance
    {
        public string ConnectionTypeName { get; set; }

        public string ConnectionName { get; set; }

        public IList<ConnectionParameterIntance> Parameters { get; set; }
    }
}
