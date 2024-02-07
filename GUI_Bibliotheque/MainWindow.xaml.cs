using CL_bibliotheque;
using CL_Utils;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GUI_Bibliotheque
{
    public partial class MainWindow : Window
    {
        private Bibliotheque bibliotheque;
        private string fileName = "bibliotheque.json";


        public MainWindow()
        {
            InitializeComponent();

            // Vérifier si le fichier existe
            if (File.Exists(fileName))
            {
                bibliotheque = SerialiseurJson.Deserialiser(fileName) ?? new Bibliotheque();
            }
            else
            {
                bibliotheque = new Bibliotheque();
                SerialiseurJson.Serialiser(bibliotheque, fileName);
            }

            if(bibliotheque.Livres != null)
            {
                foreach (Livre livre in bibliotheque.Livres)
                {
                    livresDataGrid.Items.Add(livre);
                }
            }
                
            AbonneWindow.BibliothequeMiseAJour += Bibliotheque_MiseAJour;
            EmpruntWindow.BibliothequeMiseAJour += Bibliotheque_MiseAJour;
            RequeteWindow.BibliothequeMiseAJour += Bibliotheque_MiseAJour;

            MyRegistryParamManager.PositionX = 50;
            MyRegistryParamManager.PositionY = 50;

            int pos = MyRegistryParamManager.PositionX;

        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Vérifier si tous les champs sont remplis
                if (string.IsNullOrEmpty(isbnTextBox.Text) || string.IsNullOrEmpty(titreTextBox.Text) || string.IsNullOrEmpty(auteurTextBox.Text) || string.IsNullOrEmpty(editeurTextBox.Text) || string.IsNullOrEmpty(dpTextBox.Text) || string.IsNullOrEmpty(nbpTextBox.Text))
                {
                    throw new BibliothequeException("Veuillez remplir tous les champs.");
                }

                string isbn = isbnTextBox.Text;
                string titre = titreTextBox.Text;
                string auteur = auteurTextBox.Text;
                string editeur = editeurTextBox.Text;
                string datePublication = dpTextBox.Text;
                int nombrePages = int.Parse(nbpTextBox.Text);

                Livre livre = new Livre(titre, auteur, editeur, isbn, datePublication, nombrePages);

                bibliotheque.EnregistrerLivre(livre);

                // Mettre à jour la DataGrid
                livresDataGrid.Items.Add(livre);

                MessageBox.Show("Livre ajouté avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                clearTextBoxes();

                SerialiseurJson.Serialiser(bibliotheque, fileName);
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SupprimerButton_Click(object? sender, RoutedEventArgs e)
         {
            try
            {
                    // Vérifie si ligne est selectionnee dans DataGrid
                    if (livresDataGrid.SelectedItem == null) {throw new BibliothequeException("Veuillez sélectionner un livre à supprimer.");}

                 // Récupérer le livre sélectionné
                 Livre livre = (Livre)livresDataGrid.SelectedItem;

                 // Supprimer le livre de la bibliothèque
                 bibliotheque.SupprimerLivre(livre);

                // MAJ la DataGrid
                livresDataGrid.Items.Remove(livre);

                 MessageBox.Show("Livre supprimé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            
                SerialiseurJson.Serialiser(bibliotheque, fileName);

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
                // Vérifier si une ligne est sélectionnée dans la DataGrid
                 if (livresDataGrid.SelectedItem == null) { throw new BibliothequeException("Veuillez sélectionner un livre à modifier."); }

                 // Récup livre selectionne
                 Livre livre = (Livre)livresDataGrid.SelectedItem;

                 // Afficher fenêtre de modification livre
                 ModifierLivreWindow modifierLivreWindow = new ModifierLivreWindow(livre);
                 modifierLivreWindow.Owner = this;
                 modifierLivreWindow.ShowDialog();

                 // MAJ DataGrid si livre a ete modifie
                 if (modifierLivreWindow.livreModifie)
                 {
                     bibliotheque.ModifierLivre(livre);
                    livresDataGrid.Items.Refresh();
                    MessageBox.Show("Livre modifié avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    SerialiseurJson.Serialiser(bibliotheque, fileName);
                }
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
         private void AbonneButton_Click(object sender, RoutedEventArgs e)
         {
            AbonneWindow abonneWindow = new AbonneWindow(bibliotheque);
            abonneWindow.ShowDialog();
        }

         private void EmpruntButton_Click(object sender, RoutedEventArgs e)
         {
            EmpruntWindow empruntWindow = new EmpruntWindow(bibliotheque);
            empruntWindow.ShowDialog();
         }

        private void RequeteButton_Click(object sender, RoutedEventArgs e)
        {
            RequeteWindow requeteWindow = new RequeteWindow(bibliotheque);
            requeteWindow.ShowDialog();
        }

        private void Bibliotheque_MiseAJour(object sender, EventArgs e)
        {
            // Récupérer la nouvelle instance de Bibliotheque
            Bibliotheque newBibliotheque = (Bibliotheque)sender;

            // Remplacer l'instance de Bibliotheque dans MainWindow avec la nouvelle instance
            bibliotheque = newBibliotheque;
            SerialiseurJson.Serialiser(bibliotheque, fileName);
        }

        private void MainWindows_Closing(object sender, CancelEventArgs e)
        {
            SerialiseurJson.Serialiser(bibliotheque, fileName);
        }
        private void clearTextBoxes()
        {
            isbnTextBox.Text = "";
            titreTextBox.Text = "";
            auteurTextBox.Text = "";
            editeurTextBox.Text = "";
            dpTextBox.Text = "";
            nbpTextBox.Text = "";
        }
    }
}
