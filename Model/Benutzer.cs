namespace Fragenkatalog.Model
{
    abstract class Benutzer
    {
        //# benutzer_nr: int
        //# login_name : string
        //# email_adresse : string
        //# passwort : string

        // Attribute
        protected uint benutzer_nr;
        protected string login_name;
        protected string email_adresse;
        protected string passwort;
        protected uint rollen_nr;

        protected Fach[] fachListe = new Fach[5];

        // Properties
        public virtual Fach[] FachListe
        {
            get { return fachListe; }
            protected set
            {
                if (value is null)
                    throw new AttributeNotNullException(); fachListe = value;
            }
        }

        public virtual uint Benutzer_nr { get { return benutzer_nr; } protected set { benutzer_nr = value; } }

        public virtual string Login_name
        {
            get { return login_name; }
            protected set
            {
                if (value is null)
                    throw new AttributeNotNullException();

                login_name = value;
            }
        }

        public virtual string Email_adresse
        {
            get { return email_adresse; }
            protected set
            {
                if (value is null)
                    throw new AttributeNotNullException();

                email_adresse = value;
            }
        }

        public virtual string Passwort
        {
            get { return passwort; }
            protected set
            {
                if (value is null)
                    throw new AttributeNotNullException();

                passwort = value;
            }
        }

        public virtual uint Rollen_nr { get { return rollen_nr; } protected set { rollen_nr = value; } }

        public Benutzer(uint benutzer_nr, string login_name, string email_adresse, string passwort, uint rollen_nr)
        {
            Benutzer_nr = benutzer_nr;
            Login_name = login_name;
            Email_adresse = email_adresse;
            Passwort = passwort;
            Rollen_nr = rollen_nr;
        }
    }
}
