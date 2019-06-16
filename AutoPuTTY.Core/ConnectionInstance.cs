using System.Collections.Generic;

namespace AutoPuTTY.Core
{
    public class ConnectionInstance
    {
        public string ConnectionTypeName { get; set; }

        public string ConnectionName { get; set; }

        public bool IsAutoCheckEnabled { get; set; }

        public IList<ConnectionParameterIntance> Parameters { get; set; }
    }
}
