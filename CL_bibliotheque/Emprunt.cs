
namespace CL_bibliotheque
{
    public class Emprunt
    {

        private Abonne _abonne;
        private Livre _livre;
        private DateTime _dateEmprunt;
        private DateTime? _dateRetour;

        public Abonne Abonne
        {
            get { return _abonne; }
            set { _abonne = value; }  
        }
        public Livre Livre
        {
            get { return _livre; }
            set { _livre = value; }
        }

        public DateTime DateEmprunt
        {
            get { return _dateEmprunt; }
            set { _dateEmprunt = value; }

        }
        public DateTime? DateRetour
        {
            get { return _dateRetour; }
            set { _dateRetour = value; }
        }

        public Emprunt(Abonne abonne, Livre livre)
        {
            _abonne = abonne;
            _livre = livre;
            _dateEmprunt = DateTime.Now.Date;
            _dateRetour = null;
        }

        public void Retourner()
        {
            DateRetour = DateTime.Now.Date;
        }

        public override string ToString()
        {
            string retour = $"Emprunté par : {_abonne.Prenom} {_abonne.Nom}\n" + $"Titre du livre : {_livre.Titre}\n" + $"Date d'emprunt : {DateEmprunt.ToString("dd/MM/yyyy")}\n";

            if (DateRetour != null){retour += $"Date de retour : {DateRetour.Value.ToString("dd/MM/yyyy")}\n";}
            else{retour += "Livre non retourné\n";}

            return retour;
        }

    }
}
