using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_bibliotheque
{
    public class Personne
    {

        private string _nom;
        private string _prenom;
        private string _adresse;
        private DateTime _dateNaissance;

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }
        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set { _dateNaissance = value; }
        }

        public Personne(string nom, string prenom, string adresse, string dateNaissance)
        {
            _nom = nom;
            _prenom = prenom;
            _adresse = adresse;
            _dateNaissance = DateTime.ParseExact(dateNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            
        }
    }
}
