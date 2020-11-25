namespace Fragenkatalog.Model
{
    class Schueler : Benutzer
    {
        public Schueler(uint benutzer_nr, string login_name, string email_adresse, string passwort) : base(benutzer_nr, login_name, email_adresse, passwort, 3)
        {
            // TODO : eventuell anpassen
        }
    }
}
