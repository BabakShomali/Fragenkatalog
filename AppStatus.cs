using Fragenkatalog.Datenhaltung;
using Fragenkatalog.Model;

namespace Fragenkatalog
{
    static class AppStatus
    {
        public static Benutzer EingeloggterBenutzer { get; set; }
        public static IDatenhaltung Datenhaltungsadapter { get; set; }
    }
}
