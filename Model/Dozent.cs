namespace Fragenkatalog.Model
{
    class Dozent : Benutzer
    {
        public Dozent(uint benutzer_nr, string login_name, string email_adresse, string passwort) : base(benutzer_nr, login_name, email_adresse, passwort, 2)
        {
            // TODO : eventuell anpassen
        }
    }
}
