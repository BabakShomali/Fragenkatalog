namespace Fragenkatalog.Model
{
    class Frage
    {
        private uint frage_nr;
        private string frage;
        private char loesung;
        private string antwortA;
        private string antwortB;
        private string antwortC;
        private string antwortD;

        public uint Frage_nr { get { return this.frage_nr; } set { frage_nr = value; } }
        public string Frage_ { get { return this.frage; } set { frage = value; } }
        public char Loesung { get { return this.loesung; } set { loesung = value; } }
        public string AntwortA { get { return this.antwortA; } set { antwortA = value; } }
        public string AntwortB { get { return this.antwortB; } set { antwortB = value; } }

        public string AntwortC { get { return this.antwortC; } set { antwortC = value; } }

        public string AntwortD { get { return this.antwortD; } set { antwortD = value; } }

        public Frage(uint frage_nr, string frage, char loesung, string antwortA, string antwortB, string antwortC, string antwortD)
        {
            Frage_nr = frage_nr;
            Frage_ = frage;
            Loesung = loesung;
            AntwortA = antwortA;
            AntwortB = antwortB;
            AntwortC = antwortC;
            AntwortD = antwortD;
        }

    }
}
