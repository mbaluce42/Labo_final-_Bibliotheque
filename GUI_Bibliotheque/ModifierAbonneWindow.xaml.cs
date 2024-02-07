using System.Globalization;
using System;
using System.Windows;
using CL_bibliotheque;
using CL_Utils;

namespace GUI_Bibliotheque
{
    public partial class ModifierAbonneWindow : Window
    {
        public bool abonneModifie { get; private set; }
        private Abonne abonne;

        public ModifierAbonneWindow(Abonne abonne)
        {
            InitializeComponent();
            this.abonne = abonne;
            // Afficher les informations actuelles de l'abonné dans les champs de texte
            nomTextBox.Text = abonne.Nom;
            prenomTextBox.Text = abonne.Prenom;
            adresseTextBox.Text = abonne.Adresse;
            dnTextBox.Text = abonne.DateNaissance.ToString("dd/MM/yyyy");
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(nomTextBox.Text) || string.IsNullOrEmpty(prenomTextBox.Text) || string.IsNullOrEmpty(adresseTextBox.Text) || string.IsNullOrEmpty(dnTextBox.Text)) { throw new BibliothequeException("Veuillez remplir tous les champs avant de modifier l'abonné."); }

                string nom = nomTextBox.Text;
                string prenom = prenomTextBox.Text;
                string adresse = adresseTextBox.Text;
                string dateNaiss = dnTextBox.Text;

                // Mettre à jour les informations de l'abonné
                abonne.Nom = nom;
                abonne.Prenom = prenom;
                abonne.Adresse = adresse;
                abonne.DateNaissance = DateTime.ParseExact(dateNaiss, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                MessageBox.Show("L'abonné a été modifié avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                abonneModifie = true;
                Close();
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AnnulerButton_Click(object sender, RoutedEventArgs e)
        {
            abonneModifie = false;
            Close();
        }
    }
}
