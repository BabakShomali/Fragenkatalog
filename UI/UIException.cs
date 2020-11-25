using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragenkatalog.UI
{
    class UIException : Exception
    {
        public UIException(string message) : base(message)
        {

        }
    }
}
