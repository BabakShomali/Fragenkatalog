using Fragenkatalog.Datenhaltung;
using Fragenkatalog.Model;
using Fragenkatalog.UI;

namespace Fragenkatalog
{
    static class AppStatus
    {
        public static Benutzer EingeloggterBenutzer { get; set; }
        public static IDatenhaltung Datenhaltungsadapter { get; set; }
        public static IUserInterface UIAdapter { get; set; }
    }
}
