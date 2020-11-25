using System.Collections.Generic;
namespace Fragenkatalog.Model
{
    class Fach
    {
        private uint fach_nr;
        private uint kapazitaet;
        private uint wiederholungsspanne;
        private uint anzahlWiederholungen;
        private uint benutzer_nr;
        private List<Frage> fragenliste = new List<Frage>();

        public uint Fach_nr { get { return this.fach_nr; } set { fach_nr = value; } }
        public uint Kapazitaet { get { return this.kapazitaet; } set { kapazitaet = value; } }
        public uint Wiederholungsspanne { get { return this.wiederholungsspanne; } set { wiederholungsspanne = value; } }
        public uint AnzahlWiederholungen { get { return this.anzahlWiederholungen; } set { anzahlWiederholungen = value; } }
        public uint Benutzer_nr { get { return benutzer_nr; } set { benutzer_nr = value; } }
        public List<Frage> Fragenliste { get { return fragenliste; } }

        public Fach(uint fach_nr, uint kapazitaet, uint anzahlWiederholungen, uint wiederholungsspanne)
        {
            Fach_nr = fach_nr;
            Kapazitaet = kapazitaet;
            Wiederholungsspanne = wiederholungsspanne;
            AnzahlWiederholungen = anzahlWiederholungen;
        }
    }
}
