using Newtonsoft.Json;
using CL_Utils;

namespace CL_bibliotheque
{
    [Serializable]
    public class Bibliotheque
    {
        private List<Livre> _livres;
        private List<Abonne> _abonnes;
        private List<Emprunt> _emprunts;

        public Bibliotheque()
        {
            _livres = new List<Livre>();
            _abonnes = new List<Abonne>();
            _emprunts = new List<Emprunt>();
        }

        [JsonProperty("livres")]
        public List<Livre> Livres
        {
            get { return _livres; }
            set { _livres = value; }
        }

        [JsonProperty("abonnes")]
        public List<Abonne> Abonnes
        {
            get { return _abonnes; }
            set { _abonnes = value; }
        }

        [JsonProperty("emprunts")]
        public List<Emprunt> Emprunts
        {
            get { return _emprunts; }
            set { _emprunts = value; }
        }

        public void EnregistrerAbonne(Abonne abonne)
        {
            _abonnes.Add(abonne);
        }

        public void EnregistrerLivre(Livre livre)
        {
            _livres.Add(livre);
        }

        public void SupprimerLivre(Livre livre)
        {
            if (!_livres.Contains(livre))
            {
                throw new BibliothequeException("Le livre: " + livre.Titre + " n'existe pas.");
            }

            _livres.Remove(livre);
        }

        public void SupprimerAbonne(Abonne abonne)
        {
            if (!_abonnes.Contains(abonne))
            {
                throw new BibliothequeException("L'abonné: " + abonne.Nom + " " + abonne.Prenom + " n'existe pas .");
            }

            _abonnes.Remove(abonne);

        }

        public void ModifierLivre(Livre livre)
        {
            // Vérifier si le livre existe dans la liste des livres
            if (!_livres.Contains(livre))
            {
                throw new BibliothequeException("Le livre a modifer n'existe pas.");
            }

            // Trouver le livre à modifier dans la liste des livres
            var livreAModifier = _livres.FirstOrDefault(l => l.Isbn == livre.Isbn);
            if (livreAModifier != null)
            {
                // MAJ des info du livre avec les nouvelles valeurs
                livreAModifier.Titre = livre.Titre;
                livreAModifier.Auteur = livre.Auteur;
                livreAModifier.Editeur = livre.Editeur;
                livreAModifier.DatePublication = livre.DatePublication;
                livreAModifier.NbrPages = livre.NbrPages;
            }
        }

        public void ModifierAbonne(Abonne abonne)/*le parametre = abonne a modifer*/
        {
            if (!_abonnes.Contains(abonne))
            {
                throw new BibliothequeException("L'abonne a modifer n'existe pas dans la bibliothèque.");
            }
            /*va recup la reference de l'objet grace au FirstOrDefault ducoup MAJ automatique de la liste*/
            var abonneAModifier = _abonnes.FirstOrDefault(a => a.NumeroAbonne == abonne.NumeroAbonne);
            if (abonneAModifier != null)
            {
                // MAJ des info de l'abonne avec les nouvelles valeurs
                abonneAModifier.Prenom = abonne.Prenom;
                abonneAModifier.Nom = abonne.Nom;
                abonneAModifier.Adresse = abonne.Adresse;
                abonneAModifier.DateNaissance = abonne.DateNaissance;
            }

        }
        public void EmprunterLivre(Abonne abonne, Livre livre)
        {
            if (!_livres.Contains(livre))
            {
                throw new BibliothequeException("Le livre: " + livre.Titre + " à emprunter n'existe pas dans la bibliothèque.");
            }

            Emprunt emprunt = new Emprunt(abonne, livre);
            _emprunts.Add(emprunt);
        }

        public void RetournerLivre(Abonne abonne, Livre livre)
        {

            var emprunt = _emprunts.FirstOrDefault(e => e.Abonne == abonne && e.Livre == livre);
            if (emprunt == null)
            {
                throw new BibliothequeException("l'abonne: " + abonne.Nom + "n'a pas emprunté livre: " + livre.Titre);
            }
            emprunt.Retourner();
        }

        public Abonne getAbonneParId(int id)
        {
            Abonne? abonne = _abonnes.FirstOrDefault(a => a.NumeroAbonne == id);
            if (abonne == null)
            {
                throw new BibliothequeException("Aucun abonné trouvé avec le numero : " + id);
            }

            return abonne;
        }

        public Livre getLivreParNom(string nomLivre)
        {
            Livre? livre = _livres.FirstOrDefault(l => l.Titre == nomLivre);

            if (livre == null)
            {
                throw new BibliothequeException("Aucun livre trouvé avec le nom : " + nomLivre);
            }

            return livre;
        }

        public Livre RechercherLivreParISBN(string isbn)
        {
            Livre? temp = _livres.FirstOrDefault(l => l.Isbn == isbn);
            if (temp == null)
            {
                throw new BibliothequeException("Aucnun livre ne correspond a l'isbn entré: " + isbn);
            }

            return temp;
        }

        public List<Livre> RechercherLivreParTitre(string titre)
        {
            var find = _livres.Where(l => l.Titre.ToLower().Contains(titre.ToLower())).ToList();

            if (find == null)
            {
                throw new BibliothequeException("Aucun livre trouvé ne correspond a ce titre: " + titre);
            }

            return find;
        }

        public List<Livre> RechercherLivreParAuteur(string auteur)
        {
            var find = _livres.Where(l => l.Auteur.ToLower().Contains(auteur.ToLower())).ToList();

            if (find == null)
            {
                throw new BibliothequeException("Aucun livre trouvé ne correspond a cette auteur: "+ auteur);
            }

            return find;
        }

        public List<Livre> RechercherLivreParEditeur(string editeur)
        {
            var find = _livres.Where(l => l.Editeur.ToLower().Contains(editeur.ToLower())).ToList();

            if (find == null)
            {
                throw new BibliothequeException("Aucun livre trouvé ne correspond a cette éditeur: " + editeur);
            }
            return find;
        }

        public Abonne RechercherAbonneParNom(string nom)
        {
            Abonne? temp = _abonnes.FirstOrDefault(a => a.Nom.ToLower() == nom.ToLower());
            if (temp == null)
            {
                throw new BibliothequeException("Aucnun abonne ne correspond a ce nom: " + nom);
            }

            return temp;
        }

        public List<Emprunt> RequeteLivresEmpruntesParAbonne(int numeroAbonne)
        {
            var empruntsAbonne = _emprunts.Where(e => e.Abonne.NumeroAbonne == numeroAbonne).ToList();

            if (empruntsAbonne.Count == 0)
            {
                throw new BibliothequeException("Aucun livre emprunté trouvé pour l'abonné avec le numéro : " + numeroAbonne);
            }

            return empruntsAbonne;
        }

        public List<Emprunt> RequeteAbonnesEmpruntParLivre(string titreLivre)
        {
            var empruntsLivre = _emprunts.Where(e => e.Livre.Titre.ToLower() == titreLivre.ToLower()).ToList();

            if (empruntsLivre.Count == 0)
            {
                throw new BibliothequeException("Aucun abonné trouvé pour le livre avec le titre : " + titreLivre);
            }

            return empruntsLivre;
        }


    }
}


