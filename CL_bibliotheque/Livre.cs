using System.Globalization;

namespace CL_bibliotheque
{
    public class Livre
    {
        private string _titre;
        private string _auteur;
        private string _editeur;
        private string _isbn;
        private DateTime _datePublication;
        private int _nbrPages;

        public string Titre
        {
            get { return _titre; }
            set { _titre = value; }
        }
        public string Auteur
        {
            get { return _auteur; }
            set { _auteur = value; }
        }
        public string Editeur
        {
            get { return _editeur; }
            set { _editeur = value; }
        }
        public string Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }
        public DateTime DatePublication
        {
            get { return _datePublication; }
            set { _datePublication = value; }
        }
        public int NbrPages
        {
            get { return _nbrPages; }
            set { _nbrPages = value; }
        }

        public Livre() : this("RIEN", "RIEN", "RIEN", "RIEN", "01/01/0001", 0)
        {

        }

        public Livre(string titre, string auteur, string editeur, string isbn, string datePublication, int nombrePages)
        {
            _titre = titre;
            _auteur = auteur;
            _editeur = editeur;
            _isbn = isbn;
            _datePublication = DateTime.ParseExact(datePublication, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            _nbrPages = nombrePages;
        }

        public override string ToString()
        {
            return $"Titre : {Titre}\n" + $"Auteur : {Auteur}\n" + $"Éditeur : {Editeur}\n" + $"ISBN : {Isbn}\n" + $"Date de publication : {DatePublication.ToString("dd/MM/yyyy")}\n" + $"Nombre de pages : {NbrPages}";
        }

    }
}
