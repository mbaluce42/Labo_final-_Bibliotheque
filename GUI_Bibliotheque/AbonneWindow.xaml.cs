using CL_bibliotheque;
using CL_Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_Bibliotheque
{
    public partial class AbonneWindow : Window
    {
        public static event EventHandler? BibliothequeMiseAJour;

        private Bibliotheque bibliotheque;

        public AbonneWindow(Bibliotheque bibliotheque)
        {
            InitializeComponent();

            // Récupérer l'instance de Bibliotheque passée depuis MainWindow
            this.bibliotheque = bibliotheque;

            if (bibliotheque.Abonnes != null)
            {
                foreach (Abonne abonne in bibliotheque.Abonnes)
                {
                    abonneDataGrid.Items.Add(abonne);
                }
            }
               
        }

        private void RechercherN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RechercherDN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            { 

                if (string.IsNullOrEmpty(nomTextBox.Text) || string.IsNullOrEmpty(prenomTextBox.Text) || string.IsNullOrEmpty(adresseTextBox.Text) || string.IsNullOrEmpty(dnTextBox.Text)) { throw new BibliothequeException("Veuillez remplir tous les champs avant d'ajouter un abonné."); }

                string nom = nomTextBox.Text;
                string prenom = prenomTextBox.Text;
                string adresse = adresseTextBox.Text;
                string dateNaiss = dnTextBox.Text;

                Abonne abonne = new Abonne(nom, prenom, adresse, dateNaiss);
                bibliotheque.EnregistrerAbonne(abonne);
                abonneDataGrid.Items.Add(abonne);

                BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);

                clearTextBoxes();
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SupprimerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (abonneDataGrid.SelectedItem == null) { throw new BibliothequeException("Veuillez sélectionner un abonné à supprimer."); }

                Abonne abonne = (Abonne)abonneDataGrid.SelectedItem;
                bibliotheque.SupprimerAbonne(abonne);

                abonneDataGrid.Items.Add(abonne);

                MessageBox.Show("Abonné supprimé avec succès.");

                BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (abonneDataGrid.SelectedItem == null) { throw new BibliothequeException("Veuillez sélectionner un abonné à modifier."); }

                Abonne abonne = (Abonne)abonneDataGrid.SelectedItem;

                // Ouvrir la fenêtre de modification avec l'abonné sélectionné
                ModifierAbonneWindow modifierAbonneWindow = new ModifierAbonneWindow(abonne);
                modifierAbonneWindow.Owner = this;
                modifierAbonneWindow.ShowDialog();

                if (modifierAbonneWindow.abonneModifie)
                {
                    bibliotheque.ModifierAbonne(abonne);
                    abonneDataGrid.Items.Refresh();
                    MessageBox.Show("Abonné modifié avec succès.");

                    BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);
                }
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LivreButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);
        }

        private void AbonneWindows_Closing(object sender, CancelEventArgs e)
        {
            // Appel à l'événement BibliothequeMiseAJour
            BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);
        }

        private void clearTextBoxes()
        {
            nomTextBox.Text = "";
            prenomTextBox.Text = "";
            adresseTextBox.Text = "";
            dnTextBox.Text = "";
        }
    }
}
