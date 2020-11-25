namespace Fragenkatalog.Model
{
    class Admin : Benutzer
    {
        public Admin(uint benutzer_nr, string login_name, string email_adresse, string passwort):base(benutzer_nr, login_name, email_adresse, passwort, 1)
        {
            // TODO : eventuell anpassen
        }
    }
}
