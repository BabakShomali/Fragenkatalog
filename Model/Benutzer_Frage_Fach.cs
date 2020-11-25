namespace Fragenkatalog.Model
{
    class Benutzer_Frage_Fach
    {
        private int richtig;
        private int falsch;
        private Benutzer benutzer;
        private Frage frage;
        private Fach fach;

        public int Richtig { get { return this.richtig; } set { richtig = value; } }
        public int Falsch { get { return this.falsch; } set { falsch = value; } }
        public Benutzer Benutzer { get { return this.benutzer; } set { benutzer = value; } }
        public Frage Frage { get { return this.frage; } set { frage = value; } }
        public Fach Fach { get { return this.fach; } set { fach = value; } }

        public Benutzer_Frage_Fach(int richtig, int falsch, Benutzer benutzer, Frage frage, Fach fach)
        {
            Richtig = richtig;
            Falsch = falsch;
            Benutzer = benutzer;
            Frage = frage;
            Fach = fach;
        }

    }
}
