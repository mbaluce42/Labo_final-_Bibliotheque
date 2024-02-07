using System.Globalization;
using System;
using System.Windows;
using CL_bibliotheque;
using System.Windows.Controls;
using CL_Utils;

namespace GUI_Bibliotheque
{
    public partial class ModifierLivreWindow : Window
    {
        private Livre livre;

        public bool livreModifie { get; private set; }

        public ModifierLivreWindow(Livre livre)
        {
            InitializeComponent();
            this.livre = livre;

            // Afficher les détails du livre dans les contrôles de l'interface utilisateur
            isbnTextBox.Text = livre.Isbn;
            titreTextBox.Text = livre.Titre;
            auteurTextBox.Text = livre.Auteur;
            editeurTextBox.Text = livre.Editeur;
            dpTextBox.Text = livre.DatePublication.ToString();
            nbpTextBox.Text = livre.NbrPages.ToString();
        }

        private void EnregistrerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                if (string.IsNullOrEmpty(isbnTextBox.Text) || string.IsNullOrEmpty(titreTextBox.Text) || string.IsNullOrEmpty(auteurTextBox.Text) || string.IsNullOrEmpty(editeurTextBox.Text) || string.IsNullOrEmpty(dpTextBox.Text) || string.IsNullOrEmpty(nbpTextBox.Text))
                {
                    throw new BibliothequeException("Veuillez remplir tous les champs avant de modifier le livre."); 
                }

                string isbn = isbnTextBox.Text;
                string titre = titreTextBox.Text;
                string auteur = auteurTextBox.Text;
                string editeur = editeurTextBox.Text;
                string datePublication = dpTextBox.Text;
                int nbrPages = int.Parse(nbpTextBox.Text);

                livre.Isbn = isbn;
                livre.Titre = titre;
                livre.Auteur = auteur;
                livre.Editeur = editeur;
                livre.DatePublication = DateTime.ParseExact(datePublication, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                livre.NbrPages = nbrPages;

                MessageBox.Show("L'abonné a été modifié avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                livreModifie = true;
                Close();
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AnnulerButton_Click(object sender, RoutedEventArgs e)
        {
            livreModifie = false;
            Close();
        }
    }
}
