namespace Fragenkatalog.Model
{
    class Rolle
    {
        // Attribute
        private int rolle_nr;
        private string name;

        // Properties
        public int Rolle_nr { 
            get { return this.rolle_nr; } 
            set 
            {
                this.rolle_nr = value; 
            } 
        }
        public string Name 
        { 
            get { return this.name; } 
            set 
            {
                if (value is null)
                    throw new AttributeNotNullException();

                this.name = value; 
            } 
        }

        // vollparametrisierter Konstruktor,
        // da in der Datenbank keine Einträge mit NULL sein dürfen,
        // muss zum Zeitpunkt der Erzeugung eines Objektes ein gültiger
        // Wert in den Attributen gespeichert werden.
        public Rolle(int rolle_nr, string name)
        {
            Rolle_nr = rolle_nr;
            Name = name;
        }
    }
}
