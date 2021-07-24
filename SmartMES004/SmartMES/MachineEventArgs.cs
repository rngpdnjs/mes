using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES
{
    public class MachineEventArgs : EventArgs
    {
        public string Name;
        public string Time { get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); } }
        public string Desc;
        public string User;
    }
}
